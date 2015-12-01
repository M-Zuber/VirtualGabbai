using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class t_privilege_groups
    {
        public t_privilege_groups()
        {
            this.t_users = new List<t_users>();
            this.t_privileges = new List<t_privileges>();
        }

        public int C_id { get; set; }
        public string group_name { get; set; }
        public virtual ICollection<t_users> t_users { get; set; }
        public virtual ICollection<t_privileges> t_privileges { get; set; }
    }
}
