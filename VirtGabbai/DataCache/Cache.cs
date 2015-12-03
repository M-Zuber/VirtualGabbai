using DataCache.Models;

namespace DataCache
{
    /// <summary>
    /// Allows access to the cached data - but only a single (and constant) instance of it
    ///  -[the code can create another one but it will be a completly different set of data]-
    /// </summary>
    public static class Cache
    {
        #region Data Members

        // Data members
        private static ZeraLeviContext m_dsDataSet = new ZeraLeviContext();

        #endregion

        #region Properties

        /// <summary>
        /// Allows outside access to the main data set
        ///  - without letting the user access it any other way
        /// </summary>
        public static ZeraLeviContext CacheData => m_dsDataSet;

        #endregion
    }
}