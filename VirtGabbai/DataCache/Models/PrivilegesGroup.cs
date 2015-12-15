using Framework;
using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class PrivilegesGroup
    {
        public PrivilegesGroup()
        {

        }
        public PrivilegesGroup(int id, string groupName, List<Privilege> privileges)
        {
            ID = id;
            GroupName = groupName;
            Privileges = privileges;
        }

        public int ID { get; set; }
        public string GroupName { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Privilege> Privileges { get; set; } = new List<Privilege>();

        public static PrivilegesGroup Createt_privilege_groups(int _Id) => new PrivilegesGroup { ID = _Id };

        public override bool Equals(object obj)
        {
            PrivilegesGroup other = obj as PrivilegesGroup;

            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return ReferenceEquals(this, other) ||
                   (ID == other.ID &&
                    GroupName == other.GroupName &&
                    Privileges.SameAs(other.Privileges));
        }

        public override int GetHashCode() => ID.GetHashCode();

        public override string ToString()
        {
            string privilegeGroupString = GroupName;

            foreach (Privilege CurrPrivilege in Privileges)
            {
                privilegeGroupString += "\n" + CurrPrivilege.ToString();
            }

            return privilegeGroupString;
        }
    }
}
