using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class Donation
    {
        public int ID { get; set; }
        public int AccountID { get; set; }
        public string Reason { get; set; }
        public double Amount { get; set; }
        public DateTime DonationDate { get; set; }
        public DateTime? DatePaid { get; set; }
        public bool Paid { get; set; }
        public string Comments { get; set; }
        public virtual Account Accounts { get; set; }

        public static Donation Createt_donations(int _Id, int accountNumber, string reason, double amount, DateTime donationDate, bool v) => new Donation { ID = _Id, AccountID = accountNumber, Reason = reason, Amount = amount, DonationDate = donationDate, Paid = v};

        public override string ToString()
        {
            string returnString = "Donated for: \"" + this.Reason +
                                  "\" Amount donated: \"" + this.Amount +
                                  "\" Date donated: \"" + this.DonationDate.ToString("dd/MM/yyyy") + "\"";
            if (this.Comments != string.Empty)
            {
                returnString += " Comments: \"" + this.Comments + "\"";

            }
            return returnString;
        }

        public override bool Equals(object obj)
        {
            Donation donationToCompare = (Donation)obj;

            if ((donationToCompare.Comments == null) && (this.Comments != null))
            {
                return false;
            }
            return ((this.ID == donationToCompare.ID) &&
                    (this.Amount == donationToCompare.Amount) &&
                    (this.Comments == donationToCompare.Comments) &&
                    (this.DonationDate == donationToCompare.DonationDate) &&
                    (this.Reason == donationToCompare.Reason));
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
