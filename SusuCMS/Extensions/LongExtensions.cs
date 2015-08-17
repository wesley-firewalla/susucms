﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusuCMS
{
    public static class LongExtensions
    {
        private static readonly List<string> units = new List<string>(5) { "B", "KB", "MB", "GB", "TB" };

        public static string ToFriendlySizeString(this long bytes)
        {
            var somethingMoreFriendly = TryForTheNextUnit(bytes, units[0]);
            var roundingPlaces = units[0] == somethingMoreFriendly.Item2 ? 0 : units.IndexOf(somethingMoreFriendly.Item2) - 1;

            return string.Format("{0} {1}", Math.Round(somethingMoreFriendly.Item1, roundingPlaces), somethingMoreFriendly.Item2);
        }

        private static Tuple<double, string> TryForTheNextUnit(double size, string unit)
        {
            var indexOfUnit = units.IndexOf(unit);

            if (size > 1024 && indexOfUnit < units.Count - 1)
            {
                size = size / 1024;
                unit = units[indexOfUnit + 1];

                return TryForTheNextUnit(size, unit);
            }

            return new Tuple<double, string>(size, unit);
        }
    }
}
