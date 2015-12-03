using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework;

namespace DataCache.Models
{
    public partial class PrivilegesGroup
    {
        #region Object Methods

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

        #endregion
    }
}
