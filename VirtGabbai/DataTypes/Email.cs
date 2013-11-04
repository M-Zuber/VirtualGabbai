using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTypes
{
    public class Email
    {
        #region Properties

        public string Name { get; set; }

        public string Domain { get; set; }

        public string Suffix { get; set; }

        #endregion

        #region C'tor
        
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
