using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;


namespace LibraryProjBeta3
{
    /// <summary>
    /// Interaction logic for UserInfo.xaml
    /// </summary>
    public partial class UserInfo : Page
    {
        Customer cust;
        LibraryColection items;
        
        public UserInfo(Customer c, LibraryColection i)
        {
            InitializeComponent();
            items = i;
            cust = c;
            ShowInfo();
            ShowLoanedItems();
            
        }
        public void ShowLoanedItems()
        {
            Lb_Loaned.Items.Clear();
            foreach (Item item in cust.Loaned)
            {
                Lb_Loaned.Items.Add(item);
            }
        }
        public void ShowInfo()
        {
            password_Box.Text = "";
            name_Box.Text = cust.Name;
            age_Box.Text = cust.Age.ToString();
            if (Info_Stack.IsEnabled == true)
                pass_Box.Text = cust.Password;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (password_Box.Text == cust.Password)
            {
                save_editBtn.Visibility = Visibility.Visible;
                Info_Stack.IsEnabled = true;
                age_Box.IsEnabled = false;
                ShowInfo();
                Storyboard sb = (Storyboard)this.FindResource("SlideIn");
                sb.Begin();
            }
            else
            {
                MessageBox.Show("Incorrect Password");
            }
        }


        private void return_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Lb_Loaned.SelectedIndex > -1)
            {
               Item returned = (Item)Lb_Loaned.SelectedItem;
                Message m;
                if (items.IsItemLate(returned, items.TimeToReturn))
                {
                    m = new Message($"{returned.Name} Returend", $"{cust.Name} Hello\nWe Notiched that you didnt return you item in time\nPlease make sure To keep track of your time left to return items" +
                        $"\nYou can find all that info in your Info Card\nRegardLess We Hope you enjoyed The Time spent with the book");
                }
                else
                    m = new Message($"{returned.Name} Returend", $"{cust.Name} Hello\nThank you for Retunring The item in time\nWe Hope you enjoyed The Book And Hope To see you again soon");
                items.ReturnItem(returned,cust,m);
                ShowLoanedItems();
            }
            else
                MessageBox.Show("Please choose an item to return");
        }

        private void Lb_Loaned_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Lb_Loaned.SelectedIndex > -1)
            {
                Item temp = (Item)Lb_Loaned.SelectedItem;
                DateTime Late = DateTime.Now;
                DateTime max = temp.LoanDate.AddDays(items.TimeToReturn);
                if (Late < max)
                {
                    return_Box.Foreground = new SolidColorBrush(Colors.White);
                    return_Box.Text = $"You must Retun This item Until\n{temp.LoanDate.AddDays(items.TimeToReturn)}";
                }
                else
                {
                    return_Box.Foreground = new SolidColorBrush(Colors.DarkRed);
                    return_Box.Text = $"You are Late To rturn\nPlease Return this item Imidatly";
                }
            }
        }
        private void save_editBtn_Click(object sender, RoutedEventArgs e)
        {
            
            StringBuilder sb = new StringBuilder();
            Checker c1 = new Checker(sb);           
            string name = name_Box.Text;                      
            string password = pass_Box.Text;
            if (name != cust.Name && items.CheckIfNameExist(name))
            {
                MessageBox.Show("A customer with that name already exists. Please choose a different name.");
                return;
            }
            if (c1.CheckPassword(cust.Password, password))
            {
                MessageBox.Show("The new password must be different from the old one.");
                return;
            }
            cust.Name = name;
            cust.Password = password;
            MessageBox.Show("Edits Changeds");
            Info_Stack.IsEnabled = false;
            pass_Box.Text = "";
            Storyboard so = (Storyboard)this.FindResource("SlideOut");
            so.Begin();

        }
    
    }
}
