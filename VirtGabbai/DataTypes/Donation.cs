using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTypes
{
    public class Donation
    {
        #region Properties

        public int _Id { get; set; }

        public string Reason { get; set; }

        public double Amount { get; set; }

        public DateTime DonationDate { get; set; }

        public DateTime PaymentDate { get; set; }

        public bool DonationPaid { get; private set; }

        public string Comments { get; set; }

        #endregion

        #region C'tor

        public Donation(){}

        public Donation(int id, string reason, double amount, DateTime donationDate, DateTime paymentDate, string comments)
        {
            this._Id = id;
            this.Amount = amount;
            this.Reason = reason;
            this.DonationDate = donationDate;
            this.PaymentDate = paymentDate;
            this.DonationPaid = true;
            this.Comments = comments;
        }

        public Donation(int id, string reason, double amount, DateTime donationDate, string comments)
        {
            this._Id = id;
            this.Amount = amount;
            this.Reason = reason;
            this.DonationDate = donationDate;
            this.DonationPaid = false;
            this.Comments = comments;
        }

        #endregion

        #region Object Methods

        public override string ToString()
        {
            string returnString = "Donated for: \"" + this.Reason +
                                  "\" Amount donated: \"" + this.Amount +
                                  "\" Date donated: \"" + this.DonationDate.ToString("dd/MM/yyyy") +"\"";
            if (this.Comments != string.Empty)
            {
                returnString += " Comments: \"" + this.Comments + "\"";
                
            }
            if (this.DonationPaid)
            {
                returnString += " Date paid: \"" + this.PaymentDate.ToString("dd/MM/yyyy") + "\"";
            }
            return returnString;
        }

        public override bool Equals(object obj)
        {
            Donation donationToCompare = (Donation)obj;

            if (donationToCompare.Comments == null)
            {
                donationToCompare.Comments = "";
            }
            return ((this._Id == donationToCompare._Id) &&
                    (this.Amount == donationToCompare.Amount) &&
                    (this.Comments == donationToCompare.Comments) &&
                    (this.DonationDate == donationToCompare.DonationDate) &&
                    (this.DonationPaid == donationToCompare.DonationPaid) &&
                    (this.PaymentDate == donationToCompare.PaymentDate) &&
                    (this.Reason == donationToCompare.Reason));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
