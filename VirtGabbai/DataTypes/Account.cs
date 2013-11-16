using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework;

namespace LocalTypes
{
    public class Account
    {
        #region Properties

        public int _Id { get; set; }

        public int MonthlyPaymentTotal { get; set; }

        public DateTime LastMonthlyPaymentDate { get; set; }

        public List<Donation> UnpaidDonations { get; set; }

        public List<PaidDonation> PaidDonations { get; set; }

        #endregion

        #region C'tor

        public Account(int id, int monthlyPaymentTotal, DateTime lastMonthlyPaymentDate, List<Donation> allDonations)
        {
            this._Id = id;
            this.MonthlyPaymentTotal = monthlyPaymentTotal;
            this.LastMonthlyPaymentDate = lastMonthlyPaymentDate;
            this.UnpaidDonations = GetUnpaidDonations(allDonations);
            this.PaidDonations = GetPaidDonations(allDonations);
        }

        #endregion

        #region Object Methods

        public override bool Equals(object obj)
        {
            Account accountToCompare = (Account)obj;

            bool allDonationsEqual = ((this.UnpaidDonations.Contains(accountToCompare.UnpaidDonations)) &&
                                      (this.PaidDonations.Contains(accountToCompare.PaidDonations)));
            return ((allDonationsEqual) &&
                    (this._Id == accountToCompare._Id) &&
                    (this.LastMonthlyPaymentDate == accountToCompare.LastMonthlyPaymentDate) &&
                    (this.MonthlyPaymentTotal == accountToCompare.MonthlyPaymentTotal));
        }

        public override string ToString()
        {
            string donations = "";
            foreach (Donation CurrDonation in this.UnpaidDonations)
            {
                donations += CurrDonation.ToString();
                donations += "\n";
            }

            foreach (PaidDonation CurrPaidDonation in this.PaidDonations)
            {
                donations += CurrPaidDonation.ToString();
                donations += "\n";
            }
            if (donations.Length > 1)
            {
                donations = donations.Remove(donations.Length - 1); 
            }
            return "Total owed for the monthly payment: \"" + this.MonthlyPaymentTotal + "\"\n" +
                              "Last month the monthly payment was made: \"" + this.LastMonthlyPaymentDate.Month + "\"\n" +
                              "Donations:\n" + donations;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #region Other Methods

        private List<Donation> GetUnpaidDonations(List<Donation> allDonations)
        {
            List<Donation> filteredList = new List<Donation>();

            foreach (Donation CurrDonation in allDonations)
            {
                if (!(CurrDonation is PaidDonation))
                {
                    filteredList.Add(CurrDonation);
                }
            }

            return filteredList;
        }

        private List<PaidDonation> GetPaidDonations(List<Donation> allDonations)
        {
            List<PaidDonation> filteredList = new List<PaidDonation>();

            foreach (Donation CurrDonation in allDonations)
            {
                if (CurrDonation is PaidDonation)
                {
                    filteredList.Add(CurrDonation as PaidDonation);
                }
            }

            return filteredList;
        }

        #endregion
    }
}
