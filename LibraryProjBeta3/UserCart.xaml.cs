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
    /// Interaction logic for UserCart.xaml
    /// </summary>
    public partial class UserCart : Page
    {
        LibraryColection items;
        Customer Cust;
        public UserCart(LibraryColection lb, Customer c)
        {
            InitializeComponent();
            items = lb;
            Cust = c;
            ShowCart();
        }

        private void ShowCart()
        {
            Lb_Cart.Items.Clear();
            foreach (Item item in Cust.Cart)
            {
                Lb_Cart.Items.Add(item);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Sure = "Are you sure you want To Delete this item From the cart?";
            MessageBoxResult result;
            if (Lb_Cart.SelectedIndex > -1)
            {
                Item temp = (Item)Lb_Cart.SelectedItem;
                result = MessageBox.Show(Sure, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    items.RemoveFromListCart(temp, Cust);
                    ShowCart();
                }
            }
        }
        private double CheckTotal()
        {
            double i = 0;
            foreach (Item item in Cust.Cart)
            {
                i += item.LoanPrice;
            }
            return i;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            double price = CheckTotal();
            
            if (price > 0)
            {
                string Confirm = $"The Total of your cart is {price:c} would you like to contniue?";
                 Item temp = (Item)Lb_Cart.SelectedItem;
                result = MessageBox.Show(Confirm, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {

                    foreach (var item in Lb_Cart.Items)
                    {
                        Item c = item as Item;
                        Message m = new Message($"{c.Name} Purches",$"{Cust.Name} Thank You for purchesing at our library\n" +
                            $"Please dont forget To return the item in Time\nYou can find all nessery inforamtion in your info Card\nHave fun reading");
                        items.CustomerLoanItem((Item)item, Cust,m);
                    }
                    
                    ShowCart();
                    result = MessageBox.Show("Thank You for buying at Wan Shi Tong's Library", "Enjoy", MessageBoxButton.OK);
                }
            }
            else
                result = MessageBox.Show("You Have no items in your cart", "No Items", MessageBoxButton.OK, MessageBoxImage.Stop);
        }
    }
}
