using System;

namespace DataCache.Models
{
    public class Donation
    {
        public Donation()
        {

        }

        public Donation(int id, string reason, double amount, DateTime donationDate, string comments, DateTime? datePaid = null, bool paid = false)
        {
            Id = id;
            Reason = reason;
            Amount = amount;
            DonationDate = donationDate;
            Comments = comments;
            DatePaid = datePaid;
            Paid = paid;
        }

        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Reason { get; set; }
        public double Amount { get; set; }
        public DateTime DonationDate { get; set; }
        public DateTime? DatePaid { get; set; }
        //TODO should this be a calculated property? if paidDate has value && earlier or equal to today && ((after donation date)??)
        public bool Paid { get; set; }
        public string Comments { get; set; }
        public virtual Account Account { get; set; }

        public override string ToString()
        {
            var returnString = $"Donated for: \"{Reason}\"" +
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
            if (!(obj is Donation other))
            {
                return false;
            }
            return ReferenceEquals(this, other)
                   || (Id == other.Id
                   && Math.Abs(Amount - other.Amount) < 0
                   && Comments == other.Comments
                   && DonationDate == other.DonationDate
                   && Reason == other.Reason);
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
