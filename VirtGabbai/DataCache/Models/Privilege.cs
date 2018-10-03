using System.Collections.Generic;

namespace DataCache.Models
{
    public class Privilege
    {
        public Privilege()
        {

        }

        public Privilege(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PrivilegesGroup> PrivilegesGroup { get; set; } = new List<PrivilegesGroup>();

        public override bool Equals(object obj)
        {
            if(!(obj is Privilege other))
            {
                return false;
            }

            return ReferenceEquals(this, other) || (Id == other.Id && Name == other.Name);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => Name;
    }
}
