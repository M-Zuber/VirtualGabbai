using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class PrivilegesGroup
    {
        public int ID { get; set; }
        public string GroupName { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Privilege> Privileges { get; set; } = new List<Privilege>();

        public static PrivilegesGroup Createt_privilege_groups(int _Id) => new PrivilegesGroup { ID = _Id };
    }
}
