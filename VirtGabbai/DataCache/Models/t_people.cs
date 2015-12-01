using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class t_people
    {
        public t_people()
        {
            this.t_accounts = new List<t_accounts>();
            this.t_phone_numbers = new List<t_phone_numbers>();
            this.t_yahrtziehts = new List<t_yahrtziehts>();
        }

        public int C_id { get; set; }
        public string email { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string address { get; set; }
        public Nullable<bool> member { get; set; }
        public virtual ICollection<t_accounts> t_accounts { get; set; }
        public virtual ICollection<t_phone_numbers> t_phone_numbers { get; set; }
        public virtual ICollection<t_yahrtziehts> t_yahrtziehts { get; set; }
    }
}
