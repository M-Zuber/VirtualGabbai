using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace DataCache.Models
{
    public partial class User
    {
        #region Object Methods

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

        public override string ToString() => "User UserName: " + this.UserName + "\nEmail: " + this.Email +
        "\n" + this.PrivilegeGroup.ToString();

        #endregion
    }
}
