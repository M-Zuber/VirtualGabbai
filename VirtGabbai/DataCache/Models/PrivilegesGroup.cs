using Framework;
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

        public override bool Equals(object obj)
        {
            PrivilegesGroup groupComparing = (PrivilegesGroup)obj;
            return ((this.ID == groupComparing.ID) &&
                    (this.GroupName == groupComparing.GroupName) &&
                    (this.Privileges.SameAs(groupComparing.Privileges)));
        }

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString()
        {
            string privilegeGroupString = this.GroupName;

            foreach (Privilege CurrPrivilege in this.Privileges)
            {
                privilegeGroupString += "\n" + CurrPrivilege.ToString();
            }

            return privilegeGroupString;
        }
    }
}
