using System;
using System.Collections.Generic;
using System.Text;

namespace url.shortener.data
{
    public static class Constants
    {
        public static string AlphaNumeric => "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        public static string AlphaLowerNumeric => "abcdefghijklmnopqrstuvwxyz0123456789";
        public static int AlphaLowerNumericLength => AlphaLowerNumeric.Length;

        public static string BaseUrl => "http://gkama.it/";

        /*
         * Span constants
         */
        public static ReadOnlySpan<char> AlphaNumericSpan => "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".AsSpan();
        public static ReadOnlySpan<char> AlphaLowerNumericSpan => "abcdefghijklmnopqrstuvwxyz0123456789".AsSpan();

        public static ReadOnlySpan<char> BaseUrlSpan => "http://gkama.it/".AsSpan();
    }
}
