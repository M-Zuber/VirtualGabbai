using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace LocalTypes
{
    public class User
    {
        #region Properties

        public int _Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public MailAddress Email { get; set; }

        public PrivilegesGroup UserGroup { get; set; }

        #endregion

        #region C'tor

        public User(int _id, string userName, string password, string email, PrivilegesGroup userGroup)
        {
            this._Id = _id;
            this.UserName = userName;
            this.Password = password;
            this.Email = new MailAddress(email);
            this.UserGroup = userGroup;
        }

        #endregion

        #region Object Methods

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion
    }
}
