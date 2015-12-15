using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class Privilege
    {
        public Privilege()
        {

        }
        public Privilege(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PrivilegesGroup> PrivilegesGroup { get; set; } = new List<PrivilegesGroup>();

        public static Privilege Createt_privileges(int privilegeIndex) => new Privilege { ID = privilegeIndex };

        public override bool Equals(object obj)
        {
            Privilege other = obj as Privilege;

            if(ReferenceEquals(null, other))
            {
                return false;
            }

            return ReferenceEquals(this, other) ||
                   (ID == other.ID &&
                    Name == other.Name);
        }

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => Name;
    }
}
