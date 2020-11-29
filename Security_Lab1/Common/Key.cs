namespace Security_Lab1.Common
{
    public class Key
    {
        public static string Expand(string key, string text)
        {
            var expandedKey = string.Empty;

            while (expandedKey.Length < text.Length)
            {
                expandedKey += key;
            }

            return expandedKey;
        }
        
        public static byte[] Expand(byte key, string text)
        {
            var expandedKey = new byte[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                expandedKey[i] = key;
            }

            return expandedKey;
        }
    }
}