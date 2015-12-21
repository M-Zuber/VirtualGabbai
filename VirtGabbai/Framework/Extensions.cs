using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            if ((source == null && value != null) || (source != null && value == null))
            {
                return false;
            }
            if (source.Count() != value.Count())
            {
                return false;
            }
            
            for (int i = 0; i < source.Count(); i++)
            {
                if ((!source.Contains(value.ElementAt(i))) || (!value.Contains(source.ElementAt(i))))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Contains<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> value)
        {
            //TODO try and catch -in catch throw exception (new or otherwise) size exceeeded if size is different
            for (int i = 0; i < value.Count(); i++)
            {
                //TODO is it enough to check only this direction?
                if ((!source.Contains(value.ElementAt(i))))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
