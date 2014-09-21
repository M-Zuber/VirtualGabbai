using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.UnitTests.Extensions
{
    public static class Extensions
    {
        #region Private Object
        
        #endregion

        #region Private Type
        
        public static T InvokeStaticPrivateMethod<T>(this object obj, Type objectType, string methodName, params object[] args)
        {
            PrivateType pobj = new PrivateType(objectType);
            return (T)pobj.InvokeStatic(methodName,args);
        }

        #endregion
    }
}
