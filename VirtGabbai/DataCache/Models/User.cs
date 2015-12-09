using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class User
    {
        public User()
        {

        }
        public User(int id, string userName, string password, string email, PrivilegesGroup privilegeGroup)
        {
            ID = id;
            UserName = userName;
            Password = password;
            Email = email;
            PrivilegeGroup = privilegeGroup;
        }

        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int PrivilegesGroupID { get; set; }
        public virtual PrivilegesGroup PrivilegeGroup { get; set; }

        public static User Createt_users(int _Id1, string userName, string password, string address, int _Id2) => new User { ID = _Id1, UserName = userName, Password = password, Email = address, PrivilegesGroupID = _Id2 };

        public override bool Equals(object obj)
        {
            User other = obj as User;

            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return ReferenceEquals(this, other) ||
                   (ID == other.ID &&
                    UserName == other.UserName &&
                    Password == other.Password &&
                    Email.Equals(other.Email) &&
                    PrivilegeGroup.Equals(other.PrivilegeGroup));
        }

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => $"UserName: {UserName}\nEmail: {Email}\n{PrivilegeGroup.ToString()}";
    }
}
