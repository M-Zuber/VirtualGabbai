using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class Account
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public DateTime? LastMonthlyPaymentDate { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();
    }
}
