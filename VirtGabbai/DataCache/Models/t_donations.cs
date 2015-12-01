using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class t_donations
    {
        public int C_id { get; set; }
        public int account_id { get; set; }
        public string reason { get; set; }
        public double amount { get; set; }
        public System.DateTime date_donated { get; set; }
        public Nullable<System.DateTime> date_paid { get; set; }
        public bool paid { get; set; }
        public string comments { get; set; }
        public virtual t_accounts t_accounts { get; set; }
    }
}
