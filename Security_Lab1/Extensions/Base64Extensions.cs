using System;
using System.Collections.Generic;
using System.Linq;

namespace Security_Lab1.Extensions
{
    public static class Base64Extensions
    {
        public static byte[] DecodeBytes(string value)
        {
            var bits = string.Empty;

            // get bits (value to sextet to bits)
            foreach (var b in value.TrimEnd('='))
            {
                var indexOf = Array.IndexOf(Base64Lookup, b);
                bits += Convert.ToString(indexOf, 2).PadLeft(6, '0');
            }

            // bits to bytes
            var taken = 0;
            var octets = new List<byte>();

            while (taken < bits.Length)
            {
                var octet = bits.Skip(taken).Take(8).Aggregate(string.Empty, (c, c1) => c + c1);

                if (octet.Length == 8)
                {
                    octets.Add(Convert.ToByte(octet, 2));
                }

                taken += 8;
            }

            return octets.ToArray();
        }
        
        private static readonly char[] Base64Lookup =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/'
        };
    }
}