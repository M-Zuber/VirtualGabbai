using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class t_privileges
    {
        public t_privileges()
        {
            this.t_privilege_groups = new List<t_privilege_groups>();
        }

        public int C_id { get; set; }
        public string privilege_name { get; set; }
        public virtual ICollection<t_privilege_groups> t_privilege_groups { get; set; }
    }
}
