using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    [Serializable]
    public class Customer
    {
        public List<Item> Cart { get; set; }
        public List<Item> Loaned { get; set; }
        public List<Message> Messages { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
        public Customer(string name, int age, string password)
        {
            Name = name;
            Age = age;
            Password = password;
            Cart = new List<Item>();
            Loaned = new List<Item>();
            Messages = new List<Message>();
        }
        public void LoanItem(Item item)
        {
            if (Loaned.Contains(item))
                throw new DuplicationException();
            else
                Loaned.Add(item);
        }
        public void ReturnItem(Item temp)
        {
            if (!Loaned.Contains(temp))
                throw new ItemNotFoundException();
            else
                Loaned.Remove(temp);
        }
        public void AddToCart(Item temp)
        {
            if (!Cart.Contains(temp))
                Cart.Add(temp);
            else
                throw new DuplicationException("Already in Cart");
        }
        public void RemoveFromCart(Item temp)
        {
            if (Cart.Contains(temp))
                Cart.Remove(temp);
            else
                throw new ItemNotFoundException();
        }

        public void DeleteMessage(Message message)
        {
            if (Messages.Contains(message))
                Messages.Remove(message);
            else
                throw new ItemNotFoundException("Message Not Found");
        }
        public void AddMessage(Message message)
        {
            Messages.Add(message);
        }

        public override string ToString()
        {
            return $"Name: {Name}\nAge: {Age}";
        }
    }
}
