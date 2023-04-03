using Logic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LibraryProjBeta3
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Page
    {
        LibraryColection items;
        List<TextBlock> BlockList;
        public Edit(LibraryColection items)
        {

            InitializeComponent();
            this.items = items;
            ShowGener();
            foreach (Item item in items.ItemsList)
            {
                lb_items.Items.Add(item);
            }
            foreach (Item item in items.LoanedItems)
            {
                lb_items.Items.Add(item);
            }
        }
        public Edit(Item i, LibraryColection items)
        {

            InitializeComponent();
            this.items = items;
            btn_Choose.Visibility = Visibility.Collapsed;
            ShowGener();
            ShowItem(i);

        }
        public void ShowGener()
        {
            foreach (Genre Gener in (Genre[])Enum.GetValues(typeof(Genre)))
            {
                lb_Genre.Items.Add(Gener);
            }
        }
        public void ShowItem(Item temp)
        {
            if (temp is IAuthrable)
            {
                Book b = (Book)temp;
                box_Author.Text = b.Auther;
            }
            else
            {
                box_Author.Visibility = Visibility.Collapsed;
                blk_Auther.Visibility = Visibility.Collapsed;
            }

            box_Name.Text = temp.Name;
            box_Company.Text = temp.PoublisherCompany;
            box_Price.Text = $"{temp.LoanPrice}";
            // box_Genre.Text = $"{temp.Genre}";
            combo_Gener.Items.Add(temp.Genre);
            box_Date.Text = $"{temp.Published:d}";

            if (lb_items.Items.Count == 0)
            {
                lb_items.Items.Add(temp);
                lb_items.SelectedIndex = 0;
            }

        }
        private void btn_Choose_Click(object sender, RoutedEventArgs e)
        {
            if (lb_items.SelectedIndex > -1)
            {
                Item temp = lb_items.SelectedItem as Item;

                combo_Gener.Items.Clear();
                ShowItem(temp);
            }
        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {

            if (lb_items.SelectedIndex > -1)
            {
                Item temp = (Item)lb_items.SelectedItem;

                EditItem(temp);
                btn_Choose_Click(sender, e);
                // ShowList();
            }
            else
            {
                block_Edit.Foreground = new SolidColorBrush(Colors.DarkRed);
                block_Edit.Text = "Please Choose an item from the list";
            }
        }
        public void EditItem(Item temp)
        {
            StringBuilder sb = new StringBuilder();
            Checker c1 = new Checker(sb);
            string name, author, publisherCo, price;

            name = box_Name.Text;
            author = box_Author.Text;
            publisherCo = box_Company.Text;
            price = box_Price.Text;
            DateTime d = c1.CheckdateNotNull(datePicker);
            Genre g = c1.GetGener(lb_Genre);
            if (d.Year == 1)
                d = temp.Published;
            if (g == 0)
                g = temp.Genre;
            if (temp.GetType() == typeof(Book))
            {
                if (c1.CheckAll(d, name, publisherCo, price, g, author))
                {
                    Book b = new Book(name, author, publisherCo, d, g, double.Parse(price));
                    temp.Edit(b);
                    EditSucsses();

                }
                else
                    EditFailed(sb);
            }
            else if (temp.GetType() == typeof(Jornal))
            {
                if (c1.CheckAll(d, name, publisherCo, price, g))
                {
                    Jornal b = new Jornal(name, publisherCo, d, g, double.Parse(price));
                    temp.Edit(b);
                    EditSucsses();

                }
                else
                    EditFailed(sb);
            }

        }

        public void EditFailed(StringBuilder sb)
        {
            block_Edit.Foreground = new SolidColorBrush(Colors.DarkRed);
            block_Edit.Text = sb.ToString();
            BlockList = CheckWhichFailed(sb);
            foreach (TextBlock item in BlockList)
            {
                item.Foreground = new SolidColorBrush(Colors.DarkRed);
            }
        }
        public void EditSucsses()
        {
            block_Edit.Foreground = new SolidColorBrush(Colors.DarkGreen);
            block_Edit.Text = "Edit Was Sucsess";
            if (BlockList != null)
            {
                foreach (TextBlock item in BlockList)
                {
                    item.Foreground = new SolidColorBrush(Colors.White);
                }
            }

            combo_Gener.Items.Clear();
        }
        public void ShowList()
        {
            lb_items.Items.Clear();
            foreach (Item item in items.ItemsList)
            {
                lb_items.Items.Add(item);
            }
            foreach (Item item in items.LoanedItems)
            {
                lb_items.Items.Add(item);
            }
        }
        public List<TextBlock> CheckWhichFailed(StringBuilder sb)
        {
            List<TextBlock> BlockList = new List<TextBlock>();
            string erros = sb.ToString();
            if (erros.Length > 0)
            {
                if (erros.Contains("Author"))
                    BlockList.Add(blk_Auther);
                if (erros.Contains("book"))
                    BlockList.Add(blk_Name);
                if (erros.Contains("Company"))
                    BlockList.Add(blk_Company);
                if (erros.Contains("Price"))
                    BlockList.Add(blk_Price);
                if (erros.Contains("Gener"))
                    BlockList.Add(blk_Genre);
                if (erros.Contains("Date"))
                    BlockList.Add(blk_Date);

                return BlockList;
            }
            else
                return BlockList;


        }

        private void lb_items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Item temp = lb_items.SelectedItem as Item;

            combo_Gener.Items.Clear();
            ShowItem(temp);
        }
    }
}
