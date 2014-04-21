using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MessageBoardDAL
{
    internal static class DataRowExtensions
    {
        public static bool IsDbNull(this DataRow datarow, string propertyName)
        {
            return Convert.IsDBNull(datarow[propertyName]);
        }

        public static int ToInt32(this DataRow datarow, string propertyName)
        {
            return Convert.ToInt32(datarow[propertyName]);
        }

        public static int? ToNullableInt32(this DataRow datarow, string propertyName)
        {
            return datarow.IsNull(propertyName)
                   ? (int?)null
                   : (int?)Convert.ToInt32(datarow[propertyName]);
        }

        public static string ToString(this DataRow datarow, string propertyName)
        {
            return
                datarow.IsNull(propertyName)
                ? ""
                : Convert.ToString(datarow[propertyName]);
        }
        public static DateTime ToDateTime(this DataRow datarow, string propertyName)
        {
            return Convert.ToDateTime(datarow[propertyName]);
        }
    }
}
