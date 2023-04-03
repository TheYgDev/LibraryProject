using System;
using System.ComponentModel;
using System.Reflection;
using System.Security.Policy;

namespace Logic
{
    public interface IAuthrable
    {
        public string Auther { get; set; }
    }
    [Flags]
    public enum Genre
    {
        Fantasy = 1,
        Adventure = 2,
        Romance = 4,
        Contemporary = 8,
        Dystopian = 16,
        Mystery = 32,
        Horror = 64,
        Thriller = 128,
        Scientific = 256,
        Historicalfiction = 512,
        ScienceFiction = 1024,
        Childrens = 2048,
        Business = 4096,
        Medical =8192,
        
    }
    public enum ItemType
    {
        Book,
        Journel,
        Newspapers,
        Magazines,
        Poetry,
        Essays
    }
    [Serializable]
    public abstract class Item
    {
        public Item(string name, string poublisher, DateTime published, Genre genre, double loanPrice)
        {
            Id = Guid.NewGuid();
            Name = name;
            PoublisherCompany = poublisher;
            Published = published;
            Genre = genre;
            LoanPrice = loanPrice;
        }



        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PoublisherCompany { get; set; }
        public DateTime Published { get; set; }
        public Genre Genre { get; set; }
        public DateTime LoanDate { get; set; }
        public double LoanPrice { get; set; }      

        public virtual string Info() => $"Item Name: {Name}\nGenre: {Genre}\nPublisher: {PoublisherCompany}\n" +
             $"Loan Price: {LoanPrice:c}\nPublished: {Published:d}";
        public override string ToString() => $"{Name}  :  {LoanPrice:c}";
        public void Loan(DateTime Loned) => LoanDate = Loned;
        public abstract Item Clone();
        public abstract Item Edit(Item i);


    }


}
