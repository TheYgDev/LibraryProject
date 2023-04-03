using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace Logic
{
    [Serializable]
    public class LibraryColection
    {
        List<Item> items = new List<Item>();
        List<Item> loanedItems = new List<Item>();
        List<Item> allItems = new List<Item>();
        List<Item> incart = new List<Item>();
        List<Customer> customer = new List<Customer>();
        Dictionary<Item, Customer> whoTook = new Dictionary<Item, Customer>();
        public short TimeToReturn { get; set; }
        public List<Item> InCart
        {
            get { return incart; }
        }
        public Dictionary<Item, Customer> WhoTook { get { return whoTook; } }
        public List<Customer> Customers { get { return customer; } }
        public List<Item> ItemsList { get { return items; } }
        public List<Item> LoanedItems { get { return loanedItems; } }
        public List<Item> AllItems
        {
            get
            {
                allItems = new List<Item>();
                foreach (Item item in items)
                    allItems.Add(item);

                foreach (Item item in loanedItems)
                    allItems.Add(item);
                return allItems;
            }
        }

        public LibraryColection()
        {
            //TimeToReturn = 14;
        }
        // ########## Edit Items ##########

        public void EditItem(Item item, Item Edited)
        {
            if (item.GetType() == Edited.GetType())
            {
                item.Edit(Edited);
            }
            else
                throw new InvalidCastException();
        }
        public void EditItem(Item item, string newTitle, string publisherComp, double loanPrice, DateTime published, Genre genre, string author)
        {
            foreach (Item tempItem in items)
            {
                if (tempItem == item)
                {
                    if (newTitle != null) item.Name = newTitle;
                    if (publisherComp != null) item.PoublisherCompany = publisherComp;
                    if (loanPrice > 0) item.LoanPrice = loanPrice;
                    if (published != new DateTime()) item.Published = published;
                    if (genre > 0) item.Genre = genre;

                    if (item is IAuthrable)
                    {
                        IAuthrable authorable = (IAuthrable)item;
                        if (author != null) authorable.Auther = author;
                    }

                }
            }
        }
        public void AddItem(Item item)
        {
            if (item == null)
                throw new ArgumentException();
            else if ((items.Contains(item)))
                throw new DuplicationException();
            else
                items.Add(item);

        }

        public void Delete(Item item)
        {

            if (item == null)
                throw new ArgumentException();
            else if (items.Contains(item)) items.Remove(item);

            else
                throw new ItemNotFoundException();

        }

        public string Display(Item it)
        {
            if (it != null)
                return it.Info();


            throw new ArgumentException();
        }

        public Item CopyItem(Item item)
        {
            if (item == null)
                throw new ArgumentNullException();
            Item copy = item.Clone();
            return copy;
        }


        // ########## Search Items ##########
        public List<Item> SearchByName(string name)
        {
            if (name != null)
            {
                List<Item> Results = new List<Item>();

                foreach (Item item in items)
                {
                    if (item.Name.ToLower().Contains(name.ToLower()))
                        Results.Add(item);
                }
                if (Results.Count >= 1) return Results;
            }

            throw new ItemNotFoundException();
        }
        public List<Item> SearchByPublisherTM(string Publisher)
        {
            if (Publisher != null)
            {
                List<Item> Results = new List<Item>();

                foreach (Item item in items)
                {
                    if (item.PoublisherCompany.ToLower().Contains(Publisher.ToLower()))
                        Results.Add(item);
                }
                if (Results.Count >= 1) return Results;
            }
            // Exptions "NoBookWasFound"
            throw new ItemNotFoundException();
        }
        public List<Item> SearchByAuthor(string name)
        {
            if (name != null)
            {
                List<Item> Results = new List<Item>();
                foreach (Item item in items)
                {
                    if (item is IAuthrable)
                    {
                        IAuthrable temp = (IAuthrable)item;
                        if (temp.Auther.ToLower().Contains(name.ToLower()))
                            Results.Add(item);
                    }
                }
                if (Results.Count >= 1) return Results;
            }

            throw new ItemNotFoundException();
        }

        public List<Item> SearchByGenre(Genre genre)
        {
            if (genre != 0)
            {
                List<Item> Results = new List<Item>();
                foreach (Item item in items)
                {
                    if (item.Genre == (genre | item.Genre))
                        Results.Add(item);

                }
                if (Results.Count >= 1)
                    return Results;
            }

            throw new ItemNotFoundException();
        }
        public List<Item> SearchByDateTime(DateTime Time)
        {
            List<Item> Results = new List<Item>();
            foreach (Item item in items)
            {
                if (item.Published.Year == Time.Year)
                    Results.Add(item);
            }
            if (Results.Count >= 1) return Results;


            throw new ItemNotFoundException();
        }



        // ########## Loan Items ##########
        public void Loan(Item i, Customer c)
        {
            if (i == null)
                throw new ArgumentException();
            else if ((!items.Contains(i)))
                throw new ItemNotFoundException();
            else if (i.LoanDate.Year != 1)
                throw new ItemBorrowedException();
            else
            {
                if (Customers.Contains(c))
                {
                    i.Loan(DateTime.Now);
                    whoTook.Add(i, c);
                    LoanedItems.Add(i);
                    items.Remove(i);
                    c.LoanItem(i);
                }
                else
                    throw new ItemNotFoundException("Customer Not Found");
            }
        }

        public void ReturnItem(Item i, Customer c)
        {
            if (i == null)
                throw new ArgumentException();
            else if ((!LoanedItems.Contains(i)))
                throw new ItemNotFoundException();

            else
            {
                i.LoanDate = new DateTime();
                LoanedItems.Remove(i);
                items.Add(i);
                c.ReturnItem(i);
            }
        }


        // in case you wish to send message when book loaned or returend
        public void Loan(Item i, Customer c,Message message)
        {
            if (i == null)
                throw new ArgumentException();
            else if ((!items.Contains(i)))
                throw new ItemNotFoundException();
            else if (i.LoanDate.Year != 1)
                throw new ItemBorrowedException();
            else
            {
                if (Customers.Contains(c))
                {
                    i.Loan(DateTime.Now);
                    whoTook.Add(i, c);
                    LoanedItems.Add(i);
                    items.Remove(i);
                    c.LoanItem(i);
                    c.AddMessage(message);
                }
                else
                    throw new ItemNotFoundException("Customer Not Found");
            }
        }
        public void ReturnItem(Item i, Customer c,Message message)
        {
            if (i == null)
                throw new ArgumentException();
            else if ((!LoanedItems.Contains(i)))
                throw new ItemNotFoundException();

            else
            {
                i.LoanDate = new DateTime();
                LoanedItems.Remove(i);
                items.Add(i);
                c.ReturnItem(i);
                c.AddMessage(message);
            }
        }

        public Dictionary<Item, Customer> LateReturn(short Late)
        {
            Dictionary<Item, Customer> dict = new Dictionary<Item, Customer>();

            foreach (var item in whoTook)
            {
                DateTime dLate = DateTime.Now;
                DateTime max = item.Key.LoanDate.AddDays(Late);
                if (dLate > max)
                {
                    dict.Add(item.Key, item.Value);
                }
            }
            return dict;

        }

        public bool IsItemLate(Item item,int time)
        {
            DateTime dLate = DateTime.Now;
            DateTime max = item.LoanDate.AddDays(time);
            if (dLate > max)
            {
                return true;
            }
            return false;
        }
        // ########## How ManyEach Items ##########
        public string LoanCount() => $"There are : {LoanedItems.Count} Loaned";


        public Dictionary<Type, int> TypesCheck()
        {
            Dictionary<Type, int> dict = new Dictionary<Type, int>();

            foreach (Item item in AllItems)
            {
                if (!dict.ContainsKey(item.GetType()))
                    dict.Add(item.GetType(), 1);
                else if (dict.ContainsKey(item.GetType()))
                    dict[item.GetType()] += 1;
            }
            return dict;
        }

        public string ItemCount()
        {
            Dictionary<Type, int> dict = TypesCheck();
            StringBuilder sb = new StringBuilder();

            foreach (var item in dict)
                sb.AppendLine($"{item.Key}  -Amount  {item.Value}");

            if (sb.Length > 0)
                return sb.ToString();
            else
                return "The List is Empty";
        }

        //########## Customer ##########

        public void AddToListCart(Item temp, Customer c)
        {
            if (ItemsList.Contains(temp))
            {
                c.AddToCart(temp);
                InCart.Add(temp);
            }
            else
                throw new ItemNotFoundException();

        }

        public void RemoveFromListCart(Item temp, Customer c)
        {
            if (InCart.Contains(temp) && c.Cart.Contains(temp))
            {
                c.RemoveFromCart(temp);
                InCart.Remove(temp);
            }
            else
                throw new ItemNotFoundException();
        }
        public void CustomerLoanItem(Item temp, Customer c)
        {
            if (InCart.Contains(temp) && c.Cart.Contains(temp))
            {
                c.RemoveFromCart(temp);
                InCart.Remove(temp);
                Loan(temp, c);
            }
            else
                throw new ItemNotFoundException();
        }
        public void CustomerLoanItem(Item temp, Customer c,Message m)
        {
            if (InCart.Contains(temp) && c.Cart.Contains(temp))
            {
                c.RemoveFromCart(temp);
                InCart.Remove(temp);
                Loan(temp, c,m);
            }
            else
                throw new ItemNotFoundException();
        }

        public bool CheckIfNameExist(string c)
        {
            foreach (Customer item in Customers)
            {
                if (c == item.Name)
                {
                    return true;
                }
            }
            return false;
        }



        //########## Manger ##########
        // Manger Search
        public List<Item> MSearchByName(string name)
        {
            if (name != null)
            {
                List<Item> Results = new List<Item>();

                foreach (Item item in AllItems)
                {
                    if (item.Name.ToLower().Contains(name.ToLower()))
                        Results.Add(item);
                }
                if (Results.Count >= 1) return Results;
            }

            throw new ItemNotFoundException();
        }

        public List<Customer> MSearchCustomer(string name)
        {
            if (name != null)
            {
                List<Customer> Results = new List<Customer>();

                foreach (Customer item in Customers)
                {
                    if (item.Name.ToLower().Contains(name.ToLower()))
                        Results.Add(item);
                }
                if (Results.Count >= 1) return Results;
            }

            throw new ItemNotFoundException("Customer Not Found");
        }
        public List<Item> MSearchByPublisherTM(string Publisher)
        {
            if (Publisher != null)
            {
                List<Item> Results = new List<Item>();

                foreach (Item item in AllItems)
                {
                    if (item.PoublisherCompany.ToLower().Contains(Publisher.ToLower()))
                        Results.Add(item);
                }
                if (Results.Count >= 1) return Results;
            }
            // Exptions "NoBookWasFound"
            throw new ItemNotFoundException();
        }
        public List<Item> MSearchByAuthor(string name)
        {
            if (name != null)
            {
                List<Item> Results = new List<Item>();
                foreach (Item item in AllItems)
                {
                    if (item is IAuthrable)
                    {
                        IAuthrable temp = (IAuthrable)item;
                        if (temp.Auther.ToLower().Contains(name.ToLower()))
                            Results.Add(item);
                    }
                }
                if (Results.Count >= 1) return Results;
            }

            throw new ItemNotFoundException();
        }

        public List<Item> MSearchByGenre(Genre genre)
        {
            if (genre != 0)
            {
                List<Item> Results = new List<Item>();
                foreach (Item item in AllItems)
                {
                    if (item.Genre == (genre | item.Genre))
                        Results.Add(item);

                }
                if (Results.Count >= 1)
                    return Results;
            }

            throw new ItemNotFoundException();
        }
        public List<Item> MSearchByDateTime(DateTime Time)
        {
            List<Item> Results = new List<Item>();
            foreach (Item item in AllItems)
            {
                if (item.Published.Year == Time.Year)
                    Results.Add(item);
            }
            if (Results.Count >= 1) return Results;


            throw new ItemNotFoundException();
        }
        public Customer CheckWhoHas(Item temp)
        {
            if (loanedItems.Contains(temp))
            {
                foreach (Customer item in Customers)
                {
                    if (item.Loaned.Contains(temp))
                        return item;
                }
            }
            throw new ItemNotFoundException("Item Not Loaned");
        }

        // Messages from manger to customer
        public void MessageAllCust(Message message)
        {
            foreach (Customer item in Customers)
            {
                item.AddMessage(message);
            }
        }
        public void MessageCustomer(Customer c, Message message)
        {
            c.AddMessage(message);
        }
        public void LateRutrunMessage(short Time)
        {
            Message d1;
            foreach (var item in LateReturn(Time))
            {
                d1 = new Message("Late Return", $"{item.Value.Name}\nYou are Late To return {item.Key.Name}\nPlease Return It imdiatly ");
                item.Value.AddMessage(d1);
            }
        }


        // ########## ToString ##########
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Item item in items)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString();
        }




    }
}
