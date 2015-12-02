using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class t_privilege_groups
    {
        public t_privilege_groups()
        {
            this.t_users = new List<t_users>();
            this.t_privileges = new List<t_zl_privileges>();
        }

        public int C_id { get; set; }
        public string group_name { get; set; }
        public virtual ICollection<t_users> t_users { get; set; }
        public virtual ICollection<t_zl_privileges> t_privileges { get; set; }

        public static t_privilege_groups Createt_privilege_groups(int _Id) => new t_privilege_groups { C_id = _Id };
    }
}
