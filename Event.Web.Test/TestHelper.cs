using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace Event.Web.Test
{
    public static class TestHelper
    {
        public static List<Attribute> GetAttributesOnMethod<T>(this T c, string methodName)
        {
            var type = c.GetType();
            var methodInfo = type.GetMethod(methodName);
            if (methodInfo == null)
            {
                Assert.Fail($"{methodName} method is missing");
            }

            return methodInfo.GetCustomAttributes().ToList();
        }

        public static Attribute FindAttribute<T>(this List<Attribute> attributes)
        {
            return attributes.SingleOrDefault(t => t.GetType() == typeof(T));
        }
    }
}
