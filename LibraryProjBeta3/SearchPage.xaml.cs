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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryProjBeta3
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        LibraryColection items;
        Frame mainFrame;
        Customer Cust;
        Ui Ui;
        public SearchPage(LibraryColection i, Frame f, Ui g)
        {
            Ui = g;
            items = i;
            InitializeComponent();
            foreach (Genre Gener in (Genre[])Enum.GetValues(typeof(Genre)))
            {
                Gener_items.Items.Add(Gener);
            }
            showList();
       
            mainFrame = f;

        }
        public SearchPage(LibraryColection i, Frame f, Ui g , Customer c):this (i,f,g)
        {
            Cust = c;
        }


        public void showList()
        {
            if (Ui == Ui.Manger)
            {
                foreach (Item item in items.AllItems)
                {
                    lb_items.Items.Add(item);
                }
            }
            else if (Ui == Ui.Customer)
            {
                foreach (Item item in items.ItemsList)
                {
                    lb_items.Items.Add(item);
                }
            }
        }

        private void rdb_ItemName_Checked(object sender, RoutedEventArgs e)
        {
            searchBar.Text = string.Empty;
        }

        private void rdb_authorName_Checked(object sender, RoutedEventArgs e)
        {
            searchBar.Text = string.Empty;
        }

        private void rdb_Comp_Checked(object sender, RoutedEventArgs e)
        {
            searchBar.Text = string.Empty;
        }

        private void rdb_Genre_Checked(object sender, RoutedEventArgs e)
        {
            searchBar.Text = string.Empty;
            Gener_items.Visibility = Visibility.Visible;
            block_Search.Visibility = Visibility.Collapsed;
            searchBar.Visibility = Visibility.Collapsed;
        }

        private void rdb_Genre_Unchecked(object sender, RoutedEventArgs e)
        {
            block_Search.Visibility = Visibility.Visible;
            searchBar.Visibility = Visibility.Visible;
            Gener_items.Visibility = Visibility.Collapsed;
        }
        private void rdb_Year_Checked(object sender, RoutedEventArgs e)
        {
            searchBar.Text = string.Empty;

        }


        private void rdb_Year_Unchecked(object sender, RoutedEventArgs e)
        {

        }
        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            lb_items.Items.Clear();
            string name = searchBar.Text;
            StringBuilder sb = new StringBuilder("Not In Use");
            Checker c1 = new Checker(sb);
            List<Item> i = new List<Item>();
            try
            {
                if (Ui == Ui.Manger)
                {
                    if (rdb_authorName.IsChecked == true)
                        i = items.MSearchByAuthor(name);
                    else if (rdb_Comp.IsChecked == true)
                        i = items.MSearchByPublisherTM(name);
                    else if (rdb_ItemName.IsChecked == true)
                        i = items.MSearchByName(name);
                    else if (rdb_Genre.IsChecked == true)
                        i = items.MSearchByGenre(c1.GetGener(Gener_items));
                    else if (rdb_Year.IsChecked == true)
                        i = items.MSearchByDateTime(GetYear(name));
                    else
                        lb_items.Items.Add("Please Chose Search Paramter");
                }
                else if (Ui == Ui.Customer)
                {
                    if (rdb_authorName.IsChecked == true)
                        i = items.SearchByAuthor(name);
                    else if (rdb_Comp.IsChecked == true)
                        i = items.SearchByPublisherTM(name);
                    else if (rdb_ItemName.IsChecked == true)
                        i = items.SearchByName(name);
                    else if (rdb_Genre.IsChecked == true)
                        i = items.SearchByGenre(c1.GetGener(Gener_items));
                    else if (rdb_Year.IsChecked == true)
                        i = items.SearchByDateTime(GetYear(name));
                    else
                        lb_items.Items.Add("Please Chose Search Paramter");
                }



            }
            catch (ItemNotFoundException mes)
            {
                lb_items.Items.Add(mes.Message);

            }
            catch (Exception)
            {
                lb_items.Items.Add("Somethig Went Wrong\nTry Again");
            }

            foreach (Item item in i)
                lb_items.Items.Add(item);

        }

        public DateTime GetYear(string sYear)
        {
            DateTime year;
            if (isYearValid(sYear))
            {
                year = new DateTime(int.Parse(sYear), 1, 1);
            }
            else
                year = new DateTime();

            return year;
        }
        public bool isYearValid(string sYear)
        {
            int year;
            if (!int.TryParse(sYear, out year) && sYear.Length != 4)
                return false;
            return true;
        }

        private void Display_Click(object sender, RoutedEventArgs e)
        {

            if (checkitem())
                Move((Item)lb_items.SelectedItem);


        }
        private void Move(Item temp)
        {
            DisplayInfo d1;
            if (Ui == Ui.Manger)            
              d1 = new DisplayInfo(items, temp, mainFrame, Ui);           
            else
                d1 = new DisplayInfo(items, temp, mainFrame, Ui,Cust);

            mainFrame.Content = d1;
        }
        public bool checkitem()
        {
            if (lb_items.SelectedIndex > -1) { return true; }
            return false;
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)this.FindResource("SlideIn");
            sb.Begin();
            DownButton.Visibility = Visibility.Collapsed;
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)this.FindResource("SlideOut");
            sb.Begin();
            DownButton.Visibility = Visibility.Visible;
        }
    }
}
