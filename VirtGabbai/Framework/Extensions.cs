using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework
{
    public static class Extensions
    {
        public static bool Contains<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> value)
        {
            if (source.Count() != value.Count())
            {
                return false;
            }
            else
            {
                for (int i = 0; i < source.Count(); i++)
                {
                    if ((!source.Contains(value.ElementAt(i))) || (!value.Contains(source.ElementAt(i))))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
