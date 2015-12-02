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

        public static t_users Createt_users(int _Id1, string userName, string password, string address, int _Id2) => new t_users { C_id = _Id1, name = userName, password = password, email = address, privileges_group = _Id2 };
    }
}
