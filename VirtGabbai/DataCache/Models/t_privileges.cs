using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class t_zl_privileges
    {
        public t_zl_privileges()
        {
            this.t_privilege_groups = new List<t_privilege_groups>();
        }

        public int C_id { get; set; }
        public string privilege_name { get; set; }
        public virtual ICollection<t_privilege_groups> t_privilege_groups { get; set; }

        public static t_zl_privileges Createt_privileges(int privilegeIndex) => new t_zl_privileges { C_id = privilegeIndex };
    }
}
