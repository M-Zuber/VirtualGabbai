using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalTypes
{
    public class PaidDonation :Donation
    {
        #region Properties

        public DateTime PaymentDate { get; set; }

        #endregion

        #region C'tor

        public PaidDonation(int id, string reason, double amount, DateTime donationDate,
            string comments, DateTime paymentDate) : base(id,reason,amount,donationDate,comments)
        {
            this.PaymentDate = paymentDate;
        }

        public PaidDonation(Donation donationPaying, DateTime paymentDate):
            this(donationPaying._Id, donationPaying.Reason, donationPaying.Amount, donationPaying.DonationDate,
                    donationPaying.Comments, paymentDate)
        {
        }

        #endregion

        #region Object Methods

        public override bool Equals(object obj)
        {
            PaidDonation comparingDonation = (PaidDonation)obj;
            return ((base.Equals(obj)) && this.PaymentDate == comparingDonation.PaymentDate);
        }

        public override string ToString()
        {
            return base.ToString() + " Paid on: \"" + PaymentDate.ToString("dd/MM/yyyy") + "\"";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
