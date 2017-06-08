using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Invoice.Site.Extensions
{
    public static class EnumExtensions
    {
        public static SelectList ToSelectList<TEnum>(this TEnum myEnum)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new { Id = (int)Enum.Parse(typeof(TEnum), e.ToString()), Name = e.ToString() };
            return new SelectList(values, "Id", "Name", myEnum);
        }

    }
}