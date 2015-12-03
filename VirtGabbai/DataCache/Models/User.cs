using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int PrivilegesGroupID { get; set; }
        public virtual PrivilegesGroup PrivilegeGroup { get; set; }

        public static User Createt_users(int _Id1, string userName, string password, string address, int _Id2) => new User { ID = _Id1, UserName = userName, Password = password, Email = address, PrivilegesGroupID = _Id2 };

        public override bool Equals(object obj)
        {
            User comparedUser = (User)obj;

            return ((this.ID == comparedUser.ID) &&
                    (this.UserName == comparedUser.UserName) &&
                    (this.Password == comparedUser.Password) &&
                    (this.Email.Equals(comparedUser.Email)) &&
                    (this.PrivilegeGroup.Equals(comparedUser.PrivilegeGroup)));
        }

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => "User UserName: " + this.UserName + "\nEmail: " + this.Email + "\n" + this.PrivilegeGroup.ToString();
    }
}
