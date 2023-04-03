
using Logic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace LibraryProjBeta3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public enum Ui
    {
        Manger, Customer
    }

    public partial class MainWindow : Window
    {
        LibraryColection items;
        bool exit = false;

        public MainWindow()
        {
            InitializeComponent();
            items = new LibraryColection();
            //To ReStart Over
            //UploadNew();
            try
            {
                Load();
               // UploadNew();
            }
            catch (Exception)
            {

                UploadNew();
            }
            items.TimeToReturn = 14;

            //items.LateRutrunMessage(items.TimeToReturn);
            DateTime midnight = DateTime.Today.AddDays(1);
            TimeSpan timeToMidnight = midnight - DateTime.Now;
            int interval = (int)timeToMidnight.TotalMilliseconds;
            Timer timer = new Timer(interval);
            timer.AutoReset = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();


        }
        public MainWindow(LibraryColection i)
        {
            InitializeComponent();
            items = i;
        }





        public void UploadNew()
        {
            Customer c1 = new Customer("yuval", 22, "123456789");
            Customer c2 = new Customer("omer", 22, "123456789");
            Customer c3 = new Customer("ido", 22, "123456789");
            Customer c4 = new Customer("amit", 22, "123456789");
            Customer c5 = new Customer("idan", 22, "123456789");
            Customer c6 = new Customer("ron", 22, "123456789");
            Customer c7 = new Customer("ziv", 22, "123456789");
            Customer c8 = new Customer("dan", 22, "123456789");
            Customer c9 = new Customer("shlomo", 22, "123456789");
            Customer c10 = new Customer("ben", 22, "123456789");
            items.Customers.Add(c1);
            items.Customers.Add(c2);
            items.Customers.Add(c3);
            items.Customers.Add(c4);
            items.Customers.Add(c5);
            items.Customers.Add(c6);
            items.Customers.Add(c7);
            items.Customers.Add(c8);
            items.Customers.Add(c9);
            items.Customers.Add(c10);
            Book B1 = new Book("The Catcher in the Rye", "J D Salinger", "Little Brown and Company", new DateTime(1951, 07, 16), Genre.Adventure, 9.99);
            Book B2 = new Book("The Great Gatsby", "F Scott Fitzgerald", "Scribner", new DateTime(1925, 04, 10), Genre.Historicalfiction, 15.99);
            Book B3 = new Book("To Kill a Mockingbird", "Harper Lee", "Grand Central Publishing", new DateTime(1960, 07, 11), Genre.Fantasy, 15.99);
            Book B4 = new Book("TheEnd", "idan", "End Comp", new DateTime(2007, 05, 12), Genre.Adventure, 88.90);
            Book B5 = new Book("1984", "George Orwell", "Signet", new DateTime(1949, 06, 08), Genre.Dystopian, 9.99);
            Book B6 = new Book("Pride and Prejudice", "Jane Austen", "Penguin Classics", new DateTime(1813, 01, 28), Genre.Romance, 8.99);
            items.AddItem(B1); items.AddItem(B2); items.AddItem(B3); items.AddItem(B4); items.AddItem(B5); items.AddItem(B6);

            Jornal j1 = new Jornal("Science", "American Association for the Advancement of Science", new DateTime(1880, 10, 18), Genre.Scientific, 35.00);
            Jornal j2 = new Jornal("Nature", "Nature Publishing Group", new DateTime(1869, 11, 04), Genre.Scientific, 32.00);
            Jornal j3 = new Jornal("The Lancet", "Elsevier", new DateTime(1823, 10, 05), Genre.Medical, 45.00);
            Jornal j4 = new Jornal("The New England Journal of Medicine", "Massachusetts Medical Society", new DateTime(1812, 01, 18), Genre.Medical, 50.00);
            Jornal j5 = new Jornal("Harvard Business Review", "Harvard Business Press", new DateTime(1922, 04, 02), Genre.Business, 30.00);
            items.AddItem(j1); items.AddItem(j2); items.AddItem(j3); items.AddItem(j4); items.AddItem(j5);
            Save();

        }
        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            items.LateRutrunMessage(items.TimeToReturn);
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


        private void btn_Manger_Click(object sender, RoutedEventArgs e)
        {
            exit = true;
            MangerWindow m1 = new MangerWindow(items);
            m1.WindowState = this.WindowState;
            this.Close();
            m1.ShowDialog();
        }

        private void btn_Customer_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)this.FindResource("SlideIn");
            sb.Begin();
            // Cust_Panal.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            exit = true;
            StringBuilder sb = new StringBuilder();
            string name = Name_box.Text;
            string age = Age_box.Text;
            string password = Password_box.Password;
            if (rdb_Login.IsChecked == true)
            {
                Login(name, password);
            }
            if (rdb_Register.IsChecked == true)
            {
                Register(sb, name, age, password);

            }

        }
        public void ShowBoxes()
        {
            stack_Boxes.Visibility = Visibility.Visible;
        }


        private void rdb_Login_Checked(object sender, RoutedEventArgs e)
        {
            ShowBoxes();
            Age_box.Visibility = Visibility.Collapsed;
        }

        private void rdb_Register_Checked(object sender, RoutedEventArgs e)
        {
            ShowBoxes();
            Age_box.Visibility = Visibility.Visible;
        }


        public bool Check(StringBuilder sb, string name, string age, string pass)
        {
            Checker c1 = new Checker(sb);
            if (c1.CheckStringName(name, "Please enter a valid name") & c1.CheckAge(age) & c1.CheckPassword(pass))
                return true;
            else
                return false;

        }
        public Customer CheckLogin(string name, string password)
        {
            foreach (Customer item in items.Customers)
            {
                if (name == item.Name && password == item.Password)
                    return item;
            }
            return null;
        }
        public void Register(StringBuilder sb, string name, string age, string password)
        {
            if (Check(sb, name, age, password))
            {
                Customer c = new Customer(name, int.Parse(age), password);
                if (!items.CheckIfNameExist(name))
                {
                    Message m1 = new Message("Welcome", $"Hello {c.Name}\nWelcome To Wan Shi Tong's Library\nWe Hope You Enjoy our fine Collection");
                    items.Customers.Add(c);
                    items.MessageCustomer(c, m1);
                    CustomerWindow c1 = new CustomerWindow(items, c);
                    c1.WindowState = this.WindowState;
                    this.Close();
                    c1.ShowDialog();

                }
                else
                    MessageBox.Show("User Already exist", "User exist", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
                Error_box.Text = sb.ToString();
        }

        public void Login(string name, string password)
        {
            Customer temp = CheckLogin(name, password);
            if (temp != null)
            {
                CustomerWindow c1 = new CustomerWindow(items, temp);
                c1.WindowState = this.WindowState;
                this.Close();
                c1.ShowDialog();

            }
            else
                MessageBox.Show("Password or user name are invalid", "User Login", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)this.FindResource("SlideOut");
            sb.Begin();

            // RegistrationForm.Visibility = Visibility.Hidden;
        }

        private void Name_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            name_block.Visibility = Visibility.Collapsed;
        }

        private void Age_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            age_block.Visibility = Visibility.Collapsed;
        }

        private void Password_box_PasswordChanged(object sender, RoutedEventArgs e)
        {
            password_block.Visibility = Visibility.Collapsed;
        }

        private void Name_box_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Name_box.Text == "")
            {
                name_block.Visibility = Visibility.Visible;
                name_block.Text = "Enter your Name";
            }
        }

        private void Age_box_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Age_box.Text == "")
            {
                age_block.Visibility = Visibility.Visible;
                age_block.Text = "Enter your Age";
            }
        }



        private void Password_box_LostFocus_1(object sender, RoutedEventArgs e)
        {
            if (Password_box.Password == "")
            {
                password_block.Visibility = Visibility.Visible;
                password_block.Text = "Password";
            }
        }
    }
}
