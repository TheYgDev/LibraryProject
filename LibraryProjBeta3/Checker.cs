using Logic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace LibraryProjBeta3
{
    class Checker
    {
        StringBuilder Errors = new StringBuilder();
        public Checker(StringBuilder errors)
        {
            Errors = errors;
        }
        public Genre GetGener(ListBox lb_items)
        {
            List<Genre> selectedGenres = new List<Genre>();
            Genre g = 0;
            foreach (object item in lb_items.SelectedItems)
                selectedGenres.Add((Genre)item);

            foreach (Genre item in selectedGenres)
                g |= item;

            return g;
        }
        public bool CheckAll(DateTime t, string bookname, string poublisherCompany, string loanPrice, Genre g)
        {
            bool Valid = true;

            if (t.Year == 1) { Valid = false; }
            if (!CheckString(bookname, "Enter valid book name")) { Valid = false; }
            if (!CheckString(poublisherCompany, "Enter valid Publisher Company name")) { Valid = false; }
            if (!CheckGener(g)) { Valid = false; }
            if (!CheckPrice(loanPrice)) { Valid = false; }
            return Valid;

        }
        public bool CheckAll(DateTime t, string bookname, string poublisherCompany, string loanPrice, Genre g, string auther)
        {
            bool aut = true;
            if (!CheckAll(t, bookname, poublisherCompany, loanPrice, g))
                aut = false;

            if (!CheckStringName(auther, "Please Enter a Valid Author Name"))
            {
                aut = false;
            }
            return aut;
        }
        public bool CheckStringName(string name, string ErrorMassage)
        {
            if (!Regex.IsMatch(name, @"^[\sa-zA-Z]+$") || !CheckSpace(name) || name.Length < 2)
            {
                Errors.AppendLine(ErrorMassage);
                return false;
            }

            return true;
        }
        public bool CheckStringName(string name)
        {
            if (CheckStringName(name, "Please enter a valid name"))
                return true;
            return false;
        }
        public bool CheckString(string name, string ErrorMassage)
        {
            if (!Regex.IsMatch(name, @"^[\sa-zA-Z0-9]+$") || !CheckSpace(name) || name.Length < 2)
            {
                Errors.AppendLine(ErrorMassage);
                return false;
            }

            return true;
        }
        public bool CheckSpace(string name)
        {
            if (name[0] == ' ' || name[name.Length - 1] == ' ')
                return false;
            for (int i = 0; i < name.Length - 1; i++)
            {
                if (name[i] == ' ' && name[i + 1] == ' ')
                    return false;
            }
            return true;

        }


        public bool CheckGener(Genre g)
        {
            if (g > 0)
                return true;

            Errors.AppendLine("Enter valid Gener");
            return false;
        }
        public bool CheckPrice(string price)
        {
            if (double.TryParse(price, out double p))
                return true;
            Errors.AppendLine("Please Enetr a Valid Price");
            return false;
        }
        public bool CheckAge(string Age)
        {
            if (int.TryParse(Age, out int p) && p > 9 && p < 100)
                return true;
            Errors.AppendLine("Please Enetr a Valid Age");
            return false;
        }

        public DateTime Checkdate(DatePicker DP)
        {
            DateTime? selectedDate = DP.SelectedDate;
            if (selectedDate != null && selectedDate <= DateTime.Now)
            {
                DateTime date = selectedDate.Value;
                return (DateTime)selectedDate;
            }
            Errors.AppendLine("Enter Valid Date");
            return new DateTime();
        }

        public DateTime CheckdateNotNull(DatePicker DP)
        {
            DateTime? selectedDate = DP.SelectedDate;
            if (selectedDate != null && selectedDate <= DateTime.Now)
            {
                DateTime date = selectedDate.Value;
                return (DateTime)selectedDate;
            }
            if (selectedDate != null)
                Errors.AppendLine("Enter Valid Date");
            return new DateTime();
        }
        public bool CheckPassword(string oldPassword, string newPassword)
        {
            if (oldPassword != newPassword)
            {
                if (PassLength(newPassword))
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                Errors.AppendLine("Please enter a Diffrent Password Then what you had");
                return false;
            }
        }
        public bool CheckPassword(string newPassword)
        {
            if (PassLength(newPassword))           
                return true;           
            else
                return false;
        }

        private bool PassLength(string pass)
        {
            if (pass.Length < 4)
            {
                Errors.AppendLine("Please enter a 8 letters Password");
                return false;
            }
            else
                return true;
        }

    


    }
}
