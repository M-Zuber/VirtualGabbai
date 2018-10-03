using System.Collections.Generic;
using System.Linq;

namespace Framework
{
    public static class Extensions
    {
        /// <summary>
        /// Checks that the items in the source and the value are the same byVal,
        /// the lists must match exactly
        /// </summary>
        /// <typeparam name="TSource">Type of items in the list</typeparam>
        /// <param name="source">The list being compared against</param>
        /// <param name="value">The list checking for</param>
        /// <returns>True on a full 1:1 match, otherwise false</returns>
        public static bool SameAs<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> value)
        {
            if (source == null && value == null)
            {
                return true;
            }

            if (source == null || value == null)
            {
                return false;
            }

            var sourceList = source.ToList();
            var valueList = value.ToList();
            if (sourceList.Count != valueList.Count)
            {
                return false;
            }

            for (var i = 0; i < sourceList.Count; i++)
            {
                if (!sourceList.Contains(valueList[i]) || !valueList.Contains(sourceList[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Contains<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> value)
        {
            //TODO try and catch -in catch throw exception (new or otherwise) size exceeeded if size is different
            var valueList = value.ToList();
            var sourceList = source.ToList();

            return valueList.All(t => sourceList.Contains(t));
        }
    }
}
