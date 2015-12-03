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

        public override bool Equals(object obj)
        {
            Privilege comparedPrivilege = (Privilege)obj;
            return ((ID == comparedPrivilege.ID) &&
                    (Name == comparedPrivilege.Name));
        }

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => Name;
    }
}
