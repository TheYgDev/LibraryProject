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
    /// Interaction logic for Messges.xaml
    /// </summary>
    public partial class Messges : Page
    {
        Customer cust;
        LibraryColection item;
        public Messges(LibraryColection item, Customer c)
        {
            InitializeComponent();
            this.item = item;
            cust = c;
            showMessages();

        }
        public void showMessages()
        {
            lb_message.Items.Clear();
            foreach (Message item in cust.Messages)
            {
                lb_message.Items.Add(item);
            }
            if (lb_message.Items.Count == 0)
            {
                lb_message.IsEnabled = false;
                lb_message.Items.Add("No Messages");
            }
        }

        private void lb_message_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_message.SelectedIndex > -1)
            {
                Message m = (Message)lb_message.SelectedItem;
                box_message.Text = m.MessageInfo();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (lb_message.SelectedIndex > -1)
            {
                Message m = (Message)lb_message.SelectedItem;
                cust.DeleteMessage(m);
                showMessages();
            }
        }
    }
}
