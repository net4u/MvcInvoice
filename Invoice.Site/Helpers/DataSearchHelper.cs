using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Invoice.Site.Helpers
{
    public static class DataSearchHelper
    {
        public static string GenerateDataSearch(object myObject)
        {
            string stringObject = string.Empty;
            bool serialize = true;
            object propertyValue = null;
            PropertyInfo[] propertyInfos;
            propertyInfos = myObject.GetType().GetProperties(BindingFlags.Public);
            // sort properties by name
            Array.Sort(propertyInfos,
                    delegate(PropertyInfo propertyInfo1, PropertyInfo propertyInfo2)
                    { return propertyInfo1.Name.CompareTo(propertyInfo2.Name); });

            // write property names
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                PropertyAttributes att = propertyInfo.Attributes;
                object[] attrs = propertyInfo.GetCustomAttributes(true);
                serialize = true;

                if (serialize)
                {
                    propertyInfo.GetValue(propertyValue);
                    stringObject = propertyValue == null ? stringObject : propertyValue.ToString();
                }
            }

            return stringObject;
        }
    }
}