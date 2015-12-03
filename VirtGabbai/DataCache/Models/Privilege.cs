using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class Privilege
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PrivilegesGroup> PrivilegesGroup { get; set; } = new List<PrivilegesGroup>();

        public static Privilege Createt_privileges(int privilegeIndex) => new Privilege { ID = privilegeIndex };
    }
}
