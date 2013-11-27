using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework;

namespace LocalTypes
{
    public class PrivilegesGroup
    {
        #region Properties

        public int _Id { get; set; }

        public string GroupName { get; set; }
        
        public List<Privilege> Privileges{ get; set; }

        #endregion

        #region C'tor

        public PrivilegesGroup(int id, string groupName ,List<Privilege> privileges)
        {
            this._Id = id;
            this.GroupName = groupName;
            this.Privileges = privileges;
        }

        #endregion

        #region Object Methods

        public override bool Equals(object obj)
        {
            PrivilegesGroup groupComparing = (PrivilegesGroup)obj;
            return ((this._Id == groupComparing._Id) &&
                    (this.GroupName == groupComparing.GroupName) &&
                    (this.Privileges.SameAs(groupComparing.Privileges)));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

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
