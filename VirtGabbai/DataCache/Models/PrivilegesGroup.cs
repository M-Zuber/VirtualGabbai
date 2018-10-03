using Framework;
using System.Collections.Generic;

namespace DataCache.Models
{
    public class PrivilegesGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Privilege> Privileges { get; set; } = new List<Privilege>();

        public override bool Equals(object obj)
        {
            if (!(obj is PrivilegesGroup other))
            {
                return false;
            }

            return ReferenceEquals(this, other)
                   || (Id == other.Id
                   && GroupName == other.GroupName
                   && Privileges.SameAs(other.Privileges));
        }

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString()
        {
            var privilegeGroupString = GroupName;

            foreach (var curPrivilege in Privileges)
            {
                privilegeGroupString += "\n" + curPrivilege;
            }

            return privilegeGroupString;
        }
    }
}
