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

        public DateTime DonationDate 
        {
            get
            {
                return this.DonationDate;
            }
            set
            {
                if (value == null)
                {
                    //LOG
                    this.DonationDate = DateTime.Today;
                }
            }
        }

        public DateTime PaymentDate { get; set; }

        public bool DonationPaid { get; set; }

        public string Comments { get; set; }

        #endregion

        #region C'tor

        public Donation(){}

        public Donation(int id, string reason, double amount, DateTime donationDate, DateTime paymentDate, string comments)
        {
            this._Id = id;
            this.Reason = reason;
            this.DonationDate = donationDate;
            this.PaymentDate = paymentDate;

            if (paymentDate == null)
            {
                this.DonationPaid = false;
            }
            else
            {
                this.DonationPaid = true;
            }

            this.Comments = comments;
        }

        #endregion

        #region Object Methods

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
