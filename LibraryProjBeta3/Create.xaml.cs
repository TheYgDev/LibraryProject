using Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
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
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : Page
    {
        LibraryColection items;
        public Create(LibraryColection i)
        {
            InitializeComponent();
            items = i;
            InitializeComponent();
            foreach (Genre Gener in (Genre[])Enum.GetValues(typeof(Genre)))
            {
                lb_items.Items.Add(Gener);
            }
        }

        private void btn_sumbit_Click(object sender, RoutedEventArgs e)
        {
            if (rdb_Book.IsChecked == true)
            {
                CreateItem();
                Storyboard so = (Storyboard)this.FindResource("SlideOut");
                so.Begin();
            }
            else if (rdb_Jornal.IsChecked == true)
            {
                CreateItem();
                Storyboard so = (Storyboard)this.FindResource("SlideOut");
                so.Begin();
            }
            else
            {
                Errors1.Foreground = new SolidColorBrush(Colors.DarkRed);
                Errors1.Text = "Please Chose Which item you wish to create";

            }

        }
        private void rdb_Jornal_Checked(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)this.FindResource("SlideIn");
            sb.Begin();
            txtbox_Author.Visibility = Visibility.Collapsed;
            txt_Author.Visibility = Visibility.Collapsed;
            Errors1.Text = "";

        }

        private void rdb_Book_Checked(object sender, RoutedEventArgs e)
        {

            Storyboard sb = (Storyboard)this.FindResource("SlideIn");
            sb.Begin();
            txtbox_Author.Visibility = Visibility.Visible;
            txt_Author.Visibility = Visibility.Visible;
            Errors1.Text = "";

        }
        public void CreateItem()
        {
            StringBuilder sb = new StringBuilder();
            Checker c1 = new Checker(sb);
            string name, author, publisherCo, price;

            name = txtbox_BookName.Text;
            author = txtbox_Author.Text;
            publisherCo = txtbox_PublisherCompeny.Text;
            price = txtbox_Price.Text;
            DateTime d = c1.Checkdate(DP);
            Genre g = c1.GetGener(lb_items);

            if (rdb_Book.IsChecked == true)
            {
                if (c1.CheckAll(d, name, publisherCo, price, g, author))
                {
                    Book b = new Book(name, author, publisherCo, d, g, double.Parse(price));
                    items.AddItem(b);
                    ItemAdded(b, sb);
                }
                else
                    ItemFailed(sb);

            }
            else if (rdb_Jornal.IsChecked == true)
            {
                if (c1.CheckAll(d, name, publisherCo, price, g))
                {
                    Jornal b = new Jornal(name, publisherCo, d, g, double.Parse(price));
                    items.AddItem(b);
                    ItemAdded(b, sb);
                }
                else
                {
                    ItemFailed(sb);
                }
            }

        }

        public void ItemFailed(StringBuilder Errors)
        {
            Errors1.Foreground = new SolidColorBrush(Colors.DarkRed);
            Errors1.Text = Errors.ToString();
            List<TextBlock> BlockList = CheckWhichFailed(Errors);
            foreach (TextBlock item in BlockList)
            {
                item.Foreground = new SolidColorBrush(Colors.DarkRed);
            }
        }
        public void ItemAdded(Item i, StringBuilder Errors)
        {
            Errors1.Foreground = new SolidColorBrush(Colors.DarkGreen);
            Errors.AppendLine($"{i.Name} Added To Library");
            Errors1.Text = Errors.ToString();
            ClearAll();

        }

        public List<TextBlock> CheckWhichFailed(StringBuilder sb)
        {
            List<TextBlock> BlockList = new List<TextBlock>();
            string erros = sb.ToString();
            if (erros.Length > 0)
            {
                if (erros.Contains("Author"))
                    BlockList.Add(txt_Author);
                if (erros.Contains("Name"))
                    BlockList.Add(txt_BookName);
                if (erros.Contains("Company"))
                    BlockList.Add(txt_PublisherCompeny);
                if (erros.Contains("Price"))
                    BlockList.Add(txt_Price);
                if (erros.Contains("Gener"))
                    BlockList.Add(txt_text2);
                if (erros.Contains("Date"))
                    BlockList.Add(text_txt2);
                if (erros.Contains("create"))
                    BlockList.Add(text_txt);


                return BlockList;
            }
            else
                return BlockList;


        }

        public void ClearAll()
        {
            rdb_Book.IsChecked = false;
            rdb_Jornal.IsChecked = false;
            lb_items.UnselectAll();
            DP.SelectedDate = null;
            txtbox_BookName.Text = "";
            txtbox_Author.Text = "";
            txtbox_PublisherCompeny.Text = "";
            txtbox_Price.Text = "";
        }

    }
}
