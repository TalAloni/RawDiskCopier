/* Copyright (C) 2016-2024 Tal Aloni <tal.aloni.il@gmail.com>. All rights reserved.
 * 
 * You can redistribute this program and/or modify it under the terms of
 * the GNU Lesser Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 */
using System;
using System.Drawing;

namespace RawDiskCopier
{
    public class UIHelper
    {
        public static Color GetColor(BlockStatus status)
        {
            switch (status)
            {
                case BlockStatus.Untested:
                    return Color.DarkGray;
                case BlockStatus.OK:
                    return Color.LightGreen;
                case BlockStatus.Damaged:
                    return Color.Red;
                default:
                    return Color.Maroon;
            }
        }

        public static string GetSizeString(long value)
        {
            string[] suffixes = { " B", "KiB", "MiB", "GiB", "TiB", "PiB", "EiB" };
            int suffixIndex = 0;
            while (value > 9999 && suffixIndex < suffixes.Length - 1)
            {
                value = value / 1024;
                suffixIndex++;
            }

            return String.Format("{0} {1}", value.ToString(), suffixes[suffixIndex]);
        }
    }
}
