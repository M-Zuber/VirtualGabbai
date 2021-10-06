namespace DataCache.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int PrivilegesGroupId { get; set; }
        public virtual PrivilegesGroup PrivilegeGroup { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is User other))
            {
                return false;
            }

            return ReferenceEquals(this, other)
                   || (Id == other.Id
                   && UserName == other.UserName
                   && Password == other.Password
                   && Email?.Equals(other.Email) != false
                   && PrivilegeGroup?.Equals(other.PrivilegeGroup) != false);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => $"UserName: {UserName}\nEmail: {Email}\n{PrivilegeGroup}";
    }
}
