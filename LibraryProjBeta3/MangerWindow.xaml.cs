using Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Interaction logic for MangerWindow.xaml
    /// </summary>
    public partial class MangerWindow : Window
    {
        LibraryColection items;
        bool exit = false;
        public MangerWindow(LibraryColection i)
        {
            items = i;
            InitializeComponent();
            SearchPage d1 = new SearchPage(items, MangerFrame, Ui.Manger);
            MangerFrame.Content = d1;
        }



        private void bnt_createItem_Click(object sender, RoutedEventArgs e)
        {
            Create c1 = new Create(items);
            MangerFrame.Content = c1;

        }
        private void bnt_searchItem_Click(object sender, RoutedEventArgs e)
        {
            SearchPage d1 = new SearchPage(items, MangerFrame, Ui.Manger);

            MangerFrame.Content = d1;
        }
        private void bnt_displayItem_Click(object sender, RoutedEventArgs e)
        {
            DisplayInfo d1 = new DisplayInfo(items, Ui.Manger);

            MangerFrame.Content = d1;

        }
        private void bnt_libraryInfo_Click(object sender, RoutedEventArgs e)
        {
            LibraryInfo li = new LibraryInfo(items);
            MangerFrame.Content = li;
        }
        private void bnt_Edit_Click(object sender, RoutedEventArgs e)
        {
            Edit e1 = new Edit(items);
            MangerFrame.Content = e1;
        }
        private void mainPage_btn_Click(object sender, RoutedEventArgs e)
        {
            exit = true;
            MainWindow mp = new MainWindow(items);
            this.Close();
            mp.WindowState = this.WindowState;
            mp.Show();
        }
        private void btn_sendMessage_Click(object sender, RoutedEventArgs e)
        {
            MangerSendMessage sm = new MangerSendMessage(items);
            MangerFrame.Content = sm;
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
                var m = MessageBox.Show("Are You Sure You Wish To exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (m == MessageBoxResult.Yes)
                {
                    Save();
                    e.Cancel = false;
                }
                if (m == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }

}
