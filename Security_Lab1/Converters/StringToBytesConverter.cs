using System.Text;

namespace Security_Lab1.Converters
{
    public static class StringToBytesConverter
    {
        public static byte[] StringToBytesArray(string value)
        {
            return Encoding.ASCII.GetBytes(value);
        }
    }
}