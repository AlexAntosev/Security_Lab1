using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Security_Lab1.Converters
{
    public class HexToBytesConverter
    {
        public static byte[] HexStringToBytesArray(string hex)
        {
            return  Regex.Split(hex, "(?<=\\G..)(?!$)")
                .Select(x => Convert.ToByte(x, 16)).ToArray();
        }

        public static string BytesArrayToHexString(byte[] bytes)
        {
            return BitConverter.ToString(bytes)
                .Replace("-", string.Empty).ToLower();
        }
    }
}