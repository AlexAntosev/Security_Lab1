using Security_Lab1.Converters;

namespace Security_Lab1.Operators
{
    public static class Xor
    {
        public static byte CompareBytes(byte x, byte y)
        {
            var xorData = (byte) (x ^ y);
            
            return xorData;
        }
        
        public static byte[] CompareByteArrays(byte[] x, byte[] y)
        {
            var xorData = new byte[x.Length];
    
            for (var i = 0; i < x.Length; i++)
                xorData[i] = (byte) (x[i] ^ y[i]);
        
            return xorData;
        }
        
        public static string CompareStrings(string x, string y)
        {
            var xBytes = HexToBytesConverter.HexStringToBytesArray(x);
            var yBytes = HexToBytesConverter.HexStringToBytesArray(y);

            var xorData = CompareByteArrays(xBytes, yBytes);
            
            return HexToBytesConverter.BytesArrayToHexString(xorData);
        }
    }
}