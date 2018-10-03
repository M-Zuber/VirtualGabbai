using System;
using System.Collections.Generic;
using System.Linq;

namespace DataCache.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public decimal MonthlyPaymentAmount { get; set; }
        public DateTime? LastMonthlyPaymentDate { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();

        public decimal MonthlyPaymentTotal
        {
            get
            {
                var monthsOwedFor = 0;
                if (LastMonthlyPaymentDate != null)
                {
                    var iterationDate = new DateTime(LastMonthlyPaymentDate.Value.Ticks);

                    monthsOwedFor = NumberOfMonthsBetweenTwoDates(iterationDate, DateTime.Today);
                }

                return monthsOwedFor * MonthlyPaymentAmount;
            }
        }

        private static int NumberOfMonthsBetweenTwoDates(DateTime startDateTime, DateTime endDateTime)
        {
            return ((endDateTime.Year - startDateTime.Year) * 12) + (endDateTime.Month - startDateTime.Month);
        }

        public List<Donation> UnpaidDonations => GetUnpaidDonations();
        public List<Donation> PaidDonations => GetPaidDonations();

        public override bool Equals(object obj)
        {
            if (!(obj is Account other))
            {
                return false;
            }

            return ReferenceEquals(this, other)
                   || (Id == other.Id
                   && (LastMonthlyPaymentDate == null || LastMonthlyPaymentDate.Equals(other.LastMonthlyPaymentDate))
                   && PersonId == other.PersonId
                   && MonthlyPaymentAmount == other.MonthlyPaymentAmount
                   && (Donations ?? new List<Donation>()).SequenceEqual(other.Donations ?? new List<Donation>()));
        }

        public override string ToString()
        {
            var donations = "";
            foreach (var curDonation in UnpaidDonations)
            {
                donations += curDonation.ToString();
                donations += "\n";
            }

            foreach (var curPaidDonation in PaidDonations)
            {
                donations += curPaidDonation.ToString();
                donations += "\n";
            }
            if (donations.Length > 1)
            {
                donations = donations.Remove(donations.Length - 1);
            }

            // TODO string should represent whether LastMonthlyPaymentDate is null
            // TODO string should reflect the absence of any donations
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
