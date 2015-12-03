using Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataCache.Models
{
    public partial class Account
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public DateTime? LastMonthlyPaymentDate { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();
        public int MonthlyPaymentTotal
        {
            get
            {
                //TODO help-wanted/first-timers-only make this calculation more accurate.
                //TODO null check this thing
                var monthesOwedFor = (int)(DateTime.Now - LastMonthlyPaymentDate)?.TotalDays / 30;

                return monthesOwedFor * Globals.MONTHLY_PAYMENT_AMOUNT;
            }
        }
        public List<Donation> UnpaidDonations => GetUnpaidDonations(Donations);
        public List<Donation> PaidDonations => GetPaidDonations(Donations);

        public override bool Equals(object obj)
        {
            Account accountToCompare = (Account)obj;

            bool allDonationsEqual = ((UnpaidDonations.SameAs(accountToCompare.UnpaidDonations)) &&
                                      (PaidDonations.SameAs(accountToCompare.PaidDonations)));
            return ((allDonationsEqual) &&
                    (ID == accountToCompare.ID) &&
                    (LastMonthlyPaymentDate == accountToCompare.LastMonthlyPaymentDate) &&
                    (MonthlyPaymentTotal == accountToCompare.MonthlyPaymentTotal));
        }

        public override string ToString()
        {
            string donations = "";
            foreach (Donation CurrDonation in UnpaidDonations)
            {
                donations += CurrDonation.ToString();
                donations += "\n";
            }

            foreach (Donation CurrPaidDonation in PaidDonations)
            {
                donations += CurrPaidDonation.ToString();
                donations += "\n";
            }
            if (donations.Length > 1)
            {
                donations = donations.Remove(donations.Length - 1);
            }

            // TODO string should represent whether LastMonthlyPaymentDate is null
            return $"Total owed for the monthly payment: \"{MonthlyPaymentTotal}\"\n" +
                              $"Last month the monthly payment was made: \"{LastMonthlyPaymentDate?.Month}\"\n" +
                              $"Donations:\n{donations}";
        }

        public override int GetHashCode() => base.GetHashCode();

        private static List<Donation> GetUnpaidDonations(IEnumerable<Donation> allDonations) => allDonations.Where(d => !d.Paid).ToList();

        private static List<Donation> GetPaidDonations(IEnumerable<Donation> allDonations) => allDonations.Where(d => d.Paid).ToList();
    }
}
