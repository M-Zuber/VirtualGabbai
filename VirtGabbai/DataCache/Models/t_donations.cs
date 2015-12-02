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

        public static t_donations Createt_donations(int _Id, int accountNumber, string reason, double amount, DateTime donationDate, bool v) => new t_donations { C_id = _Id, account_id = accountNumber, reason = reason, amount = amount, date_donated = donationDate, paid = v };
    }
}
