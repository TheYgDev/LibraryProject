using System;

namespace Logic
{
    [Serializable]
    public class Jornal : Item
    {
        public Jornal(string name, string poublisher, DateTime published, Genre genre, double loanPrice) : base(name, poublisher, published, genre, loanPrice)
        { }

        public override Jornal Clone()
        {
            Jornal Temp = new Jornal(this.Name, this.PoublisherCompany, this.Published, this.Genre, this.LoanPrice);
            Temp.LoanDate = new DateTime();
            return Temp;
        }
        public override Jornal Edit(Item i)
        {
            Jornal temp = i as Jornal;
            if (temp != null)
            {
                this.Name = temp.Name;
                this.PoublisherCompany = temp.PoublisherCompany;
                this.Published = temp.Published;
                this.Genre = temp.Genre;
                this.LoanPrice = temp.LoanPrice;
                return this;
            }

            throw new InvalidCastException();
        }

    }
}
