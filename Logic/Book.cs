using System;

namespace Logic
{
    [Serializable]
    public class Book : Item, IAuthrable
    {
        public string Auther { get; set; }
        public Book(string name, string athor, string poublisher, DateTime published, Genre genre, double loanPrice) : base(name, poublisher, published, genre, loanPrice)
        {
            Auther = athor;
        }
        public override string Info() => $"Author: {Auther}\n" + base.Info();

        public override Book Clone()
        {
            Book Temp = new Book(this.Name, this.Auther, this.PoublisherCompany, this.Published, this.Genre, this.LoanPrice);
            Temp.LoanDate = new DateTime();
            return Temp;
        }
        public override Book Edit(Item i)
        {
            Book temp = i as Book;
            if (temp != null)
            {
                this.Name = temp.Name;
                this.PoublisherCompany = temp.PoublisherCompany;
                this.Published = temp.Published;
                this.Genre = temp.Genre;
                this.LoanPrice = temp.LoanPrice;
                this.Auther = temp.Auther;
                return this;
            }

            throw new InvalidCastException();
        }
    }
}
