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
            var o = obj as Account;
            if (o == null)
            {
                return false;
            }

            return ID == o.ID &&
                   LastMonthlyPaymentDate.Equals(o.LastMonthlyPaymentDate) &&
                   PersonID == o.PersonID &&
                   Enumerable.SequenceEqual(Donations, o.Donations);
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
            // TODO string should relect the absence of any donations
            return $"Total owed for the monthly payment: \"{MonthlyPaymentTotal}\"\n" +
                              $"Last month the monthly payment was made: \"{LastMonthlyPaymentDate?.Month}\"\n" +
                              $"Donations:\n{donations}";
        }

        public override int GetHashCode() => base.GetHashCode();
        // TODO can i use an anon obej to get smae hashcode - try it - test it
        // the reason not to use tostring is the complication of it. - also it is prone to change

        //TODO maybe these should also take into account the DatePaid prop. - does it make a diff if Paid prop becomes calculated?
        private static List<Donation> GetUnpaidDonations(IEnumerable<Donation> allDonations) => allDonations.Where(d => !d.Paid).ToList();

        private static List<Donation> GetPaidDonations(IEnumerable<Donation> allDonations) => allDonations.Where(d => d.Paid).ToList();
    }
}
