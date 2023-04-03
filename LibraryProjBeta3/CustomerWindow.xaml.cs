using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibraryProjBeta3
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        LibraryColection items;
        Customer Cust;
        bool exit = false;
        public CustomerWindow(LibraryColection i, Customer cust)
        {
            InitializeComponent();
            items = i;
            Cust = cust;
            Cust_Name_box.Text = "Welcome\n" + Cust.Name;
        }

        private void bnt_displayItem_Click(object sender, RoutedEventArgs e)
        {
            DisplayInfo d1 = new DisplayInfo(items, Ui.Customer, Cust);
            MangerFrame.Content = d1;
        }

        private void bnt_searchItem_Click(object sender, RoutedEventArgs e)
        {
            SearchPage d1 = new SearchPage(items, MangerFrame, Ui.Customer, Cust);

            MangerFrame.Content = d1;
        }

        private void btn_Loan_Click(object sender, RoutedEventArgs e)
        {
            UserCart Uc = new UserCart(items, Cust);
            MangerFrame.Content = Uc;
        }

        private void btn_UserInfo_Click(object sender, RoutedEventArgs e)
        {
            UserInfo User = new UserInfo(Cust, items);
            MangerFrame.Content = User;
        }
        private void mainPage_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Cust.Cart.Count == 0)
                exit = true;
            MainWindow mp = new MainWindow(items);
            this.Close();
            mp.WindowState = this.WindowState;
            mp.Show();
        }

        private void btn_Message_Click(object sender, RoutedEventArgs e)
        {
            Messges m1 = new Messges(items, Cust);
            MangerFrame.Content = m1;
        }

        public void Save()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream("TempDataBase.dat", FileMode.Create);
            bf.Serialize(fs, items);
            fs.Close();
        }
        public void Load()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream("TempDataBase.dat", FileMode.Open);
            items = (LibraryColection)bf.Deserialize(fs);
            fs.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!exit)
            {
                MessageBoxResult result;
                if (Cust.Cart.Count == 0)
                {
                    result = MessageBox.Show("Are You Sure You Wish To exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
                }
                else
                {
                    result = MessageBox.Show("You Still Have items in your Cart, if you leave now they will be deleted do you wish to exit?", "Cart", MessageBoxButton.YesNo, MessageBoxImage.Question);
                }

                if (result == MessageBoxResult.Yes)
                {
                    RemoveAllCart();
                    Save();
                    e.Cancel = false;

                }
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        public void RemoveAllCart()
        {
            Item[] temp = new Item[Cust.Cart.Count];         
            temp = Cust.Cart.ToArray();
            for (int i = 0; i < temp.Length; i++)
            {
                items.RemoveFromListCart(temp[i], Cust);
            }
        }
    }
}
