﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElDorado
{
    public static class Extensions
    {
        public static long ToUnixTime(this DateTime dateTime)
        {
            var date = dateTime.ToUniversalTime();
            var ts = date - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            return Convert.ToInt64(ts.TotalSeconds);
        }

        public static IList<IList<object>> Pad(this IList<IList<object>> target)
        {
            var maxColumns = target.Max(r => r.Count);

            foreach (var row in target)
            {
                for (int i = row.Count; i < maxColumns; i++)
                    row.Add(string.Empty);
            }

            return target;
        }

        public static bool LooselyMatches(this string target, string matchCandidate)
        {
            return target.ToLower().Trim() == matchCandidate.ToLower().Replace("  ", " ");
        }

        public static DateTime? SafeToDateTime(this object target)
        {
            return DateTime.TryParse(target?.ToString(), out DateTime result) ? result : (DateTime?)null;
        }
            
    }
}
