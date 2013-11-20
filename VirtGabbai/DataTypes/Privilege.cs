using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalTypes
{
    public class Privilege
    {
        #region Properties

        public int _Id { get; set; }

        public string PrivilegeName { get; set; }
        
        #endregion

        #region C'tor

        public Privilege(int id, string privilegeName)
        {
            this._Id = id;
            this.PrivilegeName = privilegeName;
        }

        #endregion

        #region Object Methods

        public override bool Equals(object obj)
        {
            Privilege comparedPrivilege = (Privilege)obj;
            return ((this._Id == comparedPrivilege._Id) &&
                    (this.PrivilegeName == comparedPrivilege.PrivilegeName));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return this.PrivilegeName;
        }

        #endregion
    }
}
