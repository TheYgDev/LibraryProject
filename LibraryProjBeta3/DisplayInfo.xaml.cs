using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryProjBeta3
{
    /// <summary>
    /// Interaction logic for DisplayInfo.xaml
    /// </summary>
    public partial class DisplayInfo : Page
    {
        LibraryColection items;
        Frame mainFrame;
        Item temp;
        Customer Cust;
        Ui Ui;
        public DisplayInfo(LibraryColection i, Ui ui)
        {
            items = i;
            InitializeComponent();
            DisplayStack.Visibility = Visibility.Visible;
            Ui = ui;
            ShowList();
        }
        public DisplayInfo(LibraryColection it, Item i, Frame f, Ui ui)
        {

            InitializeComponent();
            items = it;
            Ui = ui;
            if (Ui == Ui.Manger)
                SearchStack.Visibility = Visibility.Visible;
            else
                Loan_btn.Visibility = Visibility.Visible;
            temp = i;
            mainFrame = f;
            displayBox.Text = i.Info();

        }
        public DisplayInfo(LibraryColection it, Item i, Frame f, Ui ui, Customer c) : this(it, i, f, ui)
        {
            Cust = c;
        }
        public DisplayInfo(LibraryColection it, Ui ui, Customer c) : this(it, ui)
        {
            Cust = c;
        }


        public void ShowList()
        {
            displayBox.Text = "";
            lb_items.Items.Clear();

            if (Ui == Ui.Customer)
            {
                Loan_btn.Visibility = Visibility.Visible;
                foreach (var item in items.ItemsList)
                {
                    lb_items.Items.Add(item);
                }
            }
            else if (Ui == Ui.Manger)
            {
                foreach (var item in items.AllItems)
                {

                    lb_items.Items.Add(item);
                }

            }
        }

        public bool ShowInCart(Item i)
        {
            if (items.InCart.Contains(i))
                return true;
            else
                return false;
        }

        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            Edit e1 = new Edit(temp, items);
            mainFrame.Content = e1;

        }

        private void Remove_btn_Click(object sender, RoutedEventArgs e)
        {
            string Sure = "Are you sure you want to delete this?";
            string Loand = "Cannot Delete item while loand";
            MessageBoxResult result;
            if (temp.LoanDate != new DateTime())
            {
                result = MessageBox.Show(Loand, "Loaned Item", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                result = MessageBox.Show(Sure, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    items.Delete(temp);
                    SearchPage s1 = new SearchPage(items, mainFrame, Ui.Manger);
                    mainFrame.Content = s1;

                }
            }
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            AddMulty_btn.Visibility = Visibility.Visible;
            add_Box.Visibility = Visibility.Visible;
            Add_btn.IsEnabled = false;
        }
        public bool CheckAmount(string num, int Max)
        {
            int amount;
            if (int.TryParse(num, out amount) && amount > 0 && amount <= Max)
            {
                return true;
            }
            return false;
        }

        private void AddMulty_btn_Click(object sender, RoutedEventArgs e)
        {
            int max = 10;
            string StringNum = add_Box.Text;
            if (CheckAmount(StringNum, max))
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to Add {StringNum} copys?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    int num = int.Parse(StringNum);
                    for (int i = 0; i < num; i++)
                    {
                        Item copy = items.CopyItem(temp);
                        items.AddItem(copy);
                    }
                    SearchPage s1 = new SearchPage(items, mainFrame, Ui.Manger);
                    mainFrame.Content = s1;
                }
            }
            else
                add_Box.Text = "Please enter a valid number < 10";
        }

        private void lb_items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                displayBox.Text = items.Display((Item)lb_items.SelectedItem);
            }
            catch (Exception)
            {
                displayBox.Text = "";

            }

        }

        private void Loan_btn_Click(object sender, RoutedEventArgs e)
        {
            Item item;
            if (temp == null)
            {
                item = (Item)lb_items.SelectedItem;
            }
            else
                item = temp;

            string Sure = "Are you sure you want To Add this item to the cart?";
            MessageBoxResult result;
            if (CheckNotInCart(item))
            {
                result = MessageBox.Show(Sure, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    //Cust.Cart.Add(item);
                    items.AddToListCart(item,Cust);
                    if (mainFrame != null)
                    {
                        SearchPage s1 = new SearchPage(items, mainFrame, Ui.Customer, Cust);
                        mainFrame.Content = s1;
                    }
                    else
                        ShowList();

                }
            }
            else
                result = MessageBox.Show("Item Already in someones cart", "In Cart", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private bool CheckNotInCart(Item t)
        {
            if (items.InCart.Contains(t))
                return false;
            return true;
        }
    }
}


