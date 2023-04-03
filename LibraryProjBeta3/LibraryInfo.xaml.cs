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
    /// Interaction logic for LibraryInfo.xaml
    /// </summary>
    public partial class LibraryInfo : Page
    {
        LibraryColection items;
        Customer Cust;
        Dictionary<Item, Customer> Late;
       
        public LibraryInfo(LibraryColection i)
        {
            InitializeComponent();
            items = i;
            ShowList();
            quan_List.IsEnabled = false;
            amount_list.IsEnabled = false;
            rent_items.Items.Add("UnRented");
            rent_items.Items.Add("Rented");
            Late = items.LateReturn(items.TimeToReturn);
        }
        public LibraryInfo(LibraryColection i,Customer c) : this (i)
        {
            Cust = c;
        }

        public void ShowList()
        {
            Dictionary<Type, int> dict = items.TypesCheck();
            item_List.Items.Add("All Items");
            quan_List.Items.Add(items.AllItems.Count);
            foreach (var item in dict)
            {
                item_List.Items.Add(item.Key.Name);
                quan_List.Items.Add(item.Value);
            }


        }


        private void item_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            res_List.Items.Clear();
            amount_list.Items.Clear();
            ShowResults(item_List.SelectedItem.ToString());
            Set_AllAmounts(item_List.SelectedItem.ToString());
        }
        public void ShowResults(string TypeName)
        {
            res_List.Items.Clear();
            foreach (var item in items.AllItems)
            {
                if (TypeName == "All Items")
                    res_List.Items.Add(item);
                else if (item.GetType().Name == TypeName)
                    res_List.Items.Add(item);
            }
        }

        private void rent_items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (item_List.SelectedIndex > -1)
                showStatusOnListBox(rent_items.SelectedIndex);
        }


        private void showStatusOnListBox(int indx)
        {
            res_List.Items.Clear();
            if (indx == 0)
            {
                foreach (var item in items.ItemsList)
                {
                    if (item_List.SelectedItem.ToString() == "All Items")
                        res_List.Items.Add(item);
                    if (item_List.SelectedItem.ToString() == item.GetType().Name)
                        res_List.Items.Add(item);
                }
            }
            else if (indx == 1)
            {
                foreach (var item in items.LoanedItems)
                {
                    if (item_List.SelectedItem.ToString() == "All Items")
                        res_List.Items.Add(item);
                    if (item_List.SelectedItem.ToString() == item.GetType().Name)
                        res_List.Items.Add(item);
                }
            }
        }


        public void Set_AllAmounts(string typename)
        {

            if (typename == "All Items")
            {
                amount_list.Items.Add(items.ItemsList.Count);
                amount_list.Items.Add(items.LoanedItems.Count);
            }
            else
            {
                amount_list.Items.Add(Count_UnRentedByType(typename));
                amount_list.Items.Add(Count_RentedByType(typename));
            }

        }
        private int Count_UnRentedByType(string typeName)
        {
            int cnt = 0;
            foreach (var item in items.ItemsList)
            {
                if (item.GetType().Name == typeName)
                    cnt++;
            }
            return cnt;
        }
        private int Count_RentedByType(string typeName)
        {
            int cnt = 0;
            foreach (var item in items.LoanedItems)
            {
                if (item.GetType().Name == typeName)
                    cnt++;
            }
            return cnt;
        }

        private void res_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(res_List.SelectedIndex > -1)
            {

                string text;
                Item temp = (Item)res_List.SelectedItem;
                if (temp.LoanDate.Year != 1)
                {
                    Days_box.Foreground = new SolidColorBrush(Colors.White);
                    DateTime AsstimedReturn = temp.LoanDate.AddDays(items.TimeToReturn);
                    Customer c = items.CheckWhoHas(temp);
                    text = $"Loaned By :\n{c.Name}\n";
                    text += $" Lates Return By:\n{AsstimedReturn:d}";
                    Days_box.Text = text;
            
                    DateTime Late = DateTime.Now;
                    DateTime max = temp.LoanDate.AddDays(items.TimeToReturn);
                    if (Late > max)
                    {
                        Days_box.Foreground = new SolidColorBrush(Colors.DarkRed);
                        Days_box.Text += $"{c.Name} is Late To rturn";
                    }
                   
                    
                        
                    

                }
            }
        }
    }
}
