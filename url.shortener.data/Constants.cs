using System;
using System.Collections.Generic;
using System.Text;

namespace url.shortener.data
{
    public static class Constants
    {
        public static string AlphaNumeric => "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        public static int AlphaNumericLength => AlphaNumeric.Length;

        public static Random Random => new Random();
    }
}
