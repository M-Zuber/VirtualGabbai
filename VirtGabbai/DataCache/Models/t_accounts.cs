using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class t_accounts
    {
        public t_accounts()
        {
            this.t_donations = new List<t_donations>();
        }

        public int C_id { get; set; }
        public int person_id { get; set; }
        public Nullable<int> monthly_total { get; set; }
        public Nullable<System.DateTime> last_month_paid { get; set; }
        public virtual t_people t_people { get; set; }
        public virtual ICollection<t_donations> t_donations { get; set; }
    }
}
