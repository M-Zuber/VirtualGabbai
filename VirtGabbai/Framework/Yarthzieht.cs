using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework
{
    /// <summary>
    /// Entity representation of a yarthzieht
    /// </summary>
    public class Yarthzieht
    {
        #region Properties

        /// <summary>
        /// The yarthzieht id in the databse
        /// </summary>
        public int _Id { get; set; }

        /// <summary>
        /// The date of the yarthzieht
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// How the deceased was related
        /// </summary>
        public string Relation { get; set; }

        #endregion
    }
}
