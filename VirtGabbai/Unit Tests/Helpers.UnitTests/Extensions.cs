using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Infrastructure;

namespace Helpers.UnitTests.Extensions
{
    public static class Extensions
    {
        #region Private Object

        public static object InvokePrivateMethod<T, K>(this BaseDataAccess<T, K> obj, string methodName, params object[] args)
        {
            PrivateObject pobj = new PrivateObject(obj);
            return (object)pobj.Invoke(methodName, args);
        }

        #endregion

        #region Private Type

        public static object InvokeStaticPrivateMethod<T, K>(this BaseDataAccess<T, K> obj, string methodName, params object[] args)
        {
            PrivateType pobj = new PrivateType(obj.GetType());
            return (object)pobj.InvokeStatic(methodName,args);
        }

        #endregion
    }
}
