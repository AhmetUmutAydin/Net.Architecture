using System;
using System.Linq;

namespace Net.Architecture.Core.Utilities.Generator
{
    public static class RandomStringGenerator
    {
        public static string RandomString(int length)
        {
            const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789_?=)(/&%+^'!*-}][{#";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
}
