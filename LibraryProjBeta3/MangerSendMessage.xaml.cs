using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
    /// Interaction logic for MangerSendMessage.xaml
    /// </summary>
    public partial class MangerSendMessage : Page
    {
        LibraryColection items;
        public MangerSendMessage(LibraryColection i)
        {
            InitializeComponent();
            items = i;
            showList();
        }
        public void showList()
        {
            lb_cust.Items.Clear();

            foreach (Customer item in items.Customers)
            {
                lb_cust.Items.Add(item);
            }

        }

   
        private void Search_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            lb_cust.Items.Clear(); // Clear the ListBox before adding filtered items

            try
            {
                if (string.IsNullOrWhiteSpace(Search_box.Text))
                {
                    showList(); // Show the full list of items when the search box is empty
                }
                else
                {
                    List<Customer> filteredCustomers = items.MSearchCustomer(Search_box.Text);

                    foreach (Customer customer in filteredCustomers)
                    {
                        lb_cust.Items.Add(customer);
                    }
                }
            }
            catch (ItemNotFoundException e1)
            {
                lb_cust.Items.Add(e1.Message);

            }

        }

        private void lb_cust_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_cust.SelectedIndex > -1)
            {
                Customer c = lb_cust.SelectedItem as Customer;
                custName_Box.Text = c.Name;
            }
        }

        private void btn_send_Click(object sender, RoutedEventArgs e)
        {
            if (lb_cust.SelectedIndex == -1)
            {
                MessageBox.Show("Please Choose A Customer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrWhiteSpace(Title_box.Text) && Info_box.Text.Length < 2)
            {
                MessageBox.Show("Please fill The Message Title Correctliy", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrWhiteSpace(Info_box.Text) && Info_box.Text.Length < 2)
            {
                MessageBox.Show("Please fill The Message Info Correctliy", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Customer c = lb_cust.SelectedItem as Customer;
                string title = Title_box.Text;
                string info = Info_box.Text;
                Message m = new Message(title, info);
                items.MessageCustomer(c, m);
                MessageBox.Show("Message Was Send Sucsesfully", "Sent", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                ClearAll();
            }
        }
        public void ClearAll()
        {
            Title_box.Text = "";
            Info_box.Text = "";
            custName_Box.Text = "";
            lb_cust.SelectedIndex = -1;
        }
    }
}
