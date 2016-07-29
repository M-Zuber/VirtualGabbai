using Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataCache.Models
{
    public partial class Account
    {
        public Account()
        {

        }

        public Account(int id, decimal monthlyPaymentAmount, DateTime? lastMonthlyPaymentDate, IEnumerable<Donation> donations)
        {
            ID = id;
            MonthlyPaymentAmount = monthlyPaymentAmount;
            LastMonthlyPaymentDate = lastMonthlyPaymentDate;
            Donations = donations.ToList();
        }

        public int ID { get; set; }
        public int PersonID { get; set; }
        public decimal MonthlyPaymentAmount { get; set; }
        public DateTime? LastMonthlyPaymentDate { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();
        public decimal MonthlyPaymentTotal
        {
            get
            {
                decimal monthsOwedFor = 0;
                if (LastMonthlyPaymentDate != null)
                {
                    DateTime firstBillableDate = new DateTime(LastMonthlyPaymentDate.Value.AddDays(1).Ticks);
                    while (firstBillableDate <= DateTime.Today)
                    {
                        monthsOwedFor += decimal.Divide(1, DateTime.DaysInMonth(firstBillableDate.Year, firstBillableDate.Month));
                        firstBillableDate = firstBillableDate.AddDays(1);
                    }
                }

                return monthsOwedFor * MonthlyPaymentAmount;
            }
        }
        public List<Donation> UnpaidDonations => GetUnpaidDonations();
        public List<Donation> PaidDonations => GetPaidDonations();

        public override bool Equals(object obj)
        {
            var other = obj as Account;
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return ReferenceEquals(this, other) ||
                   (ID == other.ID &&
                   (LastMonthlyPaymentDate == null || LastMonthlyPaymentDate.Equals(other.LastMonthlyPaymentDate)) &&
                    PersonID == other.PersonID &&
                    MonthlyPaymentAmount == other.MonthlyPaymentAmount &&
                    Enumerable.SequenceEqual(Donations ?? new List<Donation>(), other.Donations ?? new List<Donation>()));
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
            //TODO should include information on amount paying every month

            return $"Total owed for the monthly payment: '{MonthlyPaymentTotal}'\n" +
                              $"Last month the monthly payment was made: '{LastMonthlyPaymentDate?.Month}'\n" +
                              $"Donations:\n{donations}";
        }

        public override int GetHashCode() => ToString().GetHashCode();

        //TODO maybe these should also take into account the DatePaid prop. - does it make a diff if Paid prop becomes calculated?
        private List<Donation> GetUnpaidDonations() => Donations?.Where(d => !d.Paid).ToList() ?? new List<Donation>();

        private List<Donation> GetPaidDonations() => Donations?.Where(d => d.Paid).ToList() ?? new List<Donation>();
    }
}
