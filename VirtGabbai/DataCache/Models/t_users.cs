using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class t_users
    {
        public int C_id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public int privileges_group { get; set; }
        public virtual t_privilege_groups t_privilege_groups { get; set; }
    }
}
