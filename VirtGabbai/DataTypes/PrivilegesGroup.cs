using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalTypes
{
    public class PrivilegesGroup
    {
        #region Properties

        public int _Id { get; set; }

        public List<Privilege> Privileges{ get; set; }

        #endregion

        #region C'tor

        public PrivilegesGroup(int id, List<Privilege> privileges)
        {
            this._Id = id;
            this.Privileges = privileges;
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

        #region Other Methods

        public string GroupsPrivilegesToDbString()
        {
            return "";
        }

        #endregion
    }
}
