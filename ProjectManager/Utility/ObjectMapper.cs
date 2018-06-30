using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ProjectManager.Utility
{
    public class ObjectMapper
    {
        public static void Convert<T, G>(T from , G to) where T : new()
        {
            var type = to.GetType();
            var properties = type.GetProperties();
            foreach(PropertyInfo property in properties)
            {
                var name = property.Name;
                var sourceProperty = from.GetType().GetProperty(name);
                if (sourceProperty != null)
                {
                    var value = sourceProperty.GetValue(from, null);
                    if (value != null)
                    {
                        property.SetValue(to, value, null);
                    }
                }
            }
        }
    }
}