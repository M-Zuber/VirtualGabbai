using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class Donation
    {
        public Donation()
        {

        }

        public Donation(int id, string reason, double amount, DateTime donationDate, string comments, DateTime? datePaid = null, bool paid = false)
        {
            ID = id;
            Reason = reason;
            Amount = amount;
            DonationDate = donationDate;
            Comments = comments;
            DatePaid = datePaid;
            Paid = paid;
        }

        public int ID { get; set; }
        public int AccountID { get; set; }
        public string Reason { get; set; }
        public double Amount { get; set; }
        public DateTime DonationDate { get; set; }
        public DateTime? DatePaid { get; set; }
        //TODO should this be a calculated property? if paidDate has value && earlier or equal to today && ((after dontaiondate)??)
        public bool Paid { get; set; }
        public string Comments { get; set; }
        public virtual Account Accounts { get; set; }

        public static Donation Createt_donations(int _Id, int accountNumber, string reason, double amount, DateTime donationDate, bool v) => new Donation { ID = _Id, AccountID = accountNumber, Reason = reason, Amount = amount, DonationDate = donationDate, Paid = v};

        public override string ToString()
        {
            string returnString = $"Donated for: \"{Reason}\"" +
                                  $" Amount donated: \"{Amount}\"" +
                                  $" Date donated: \"{DonationDate.ToString("dd/MM/yyyy")}\"";
            if (!string.IsNullOrWhiteSpace(Comments))
            {
                returnString = $"{returnString} Comments: \"{Comments}\"";

            }
            return returnString;
        }

        public override bool Equals(object obj)
        {
            Donation other = obj as Donation;

            if (ReferenceEquals(null, other))
            {
                return false;
            }
            return ReferenceEquals(this, other) ||
                   (ID == other.ID &&
                    Amount == other.Amount &&
                    Comments == other.Comments &&
                    DonationDate == other.DonationDate &&
                    Reason == other.Reason);
        }

        public override int GetHashCode() => ID.GetHashCode();
    }
}
