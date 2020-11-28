using System;
using System.Globalization;
using System.Linq;
using Security_Lab1.Encoders;
using Security_Lab1.Models;

namespace Security_Lab1.Decoders
{
    public class XorDecoder
    {
        public static DecryptionResult Decrypt(string text)
        {
            DecryptionResult decryptionResult = null;
            var letters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            for (int i = 0; i < 256; i++)
            {
                var possibleKey = (char)i;
                var possibleMessage = XorEncoder.Encrypt(text, possibleKey);

                var matchesCount = 0;
                foreach (var symbol in possibleMessage)
                {
                    if (letters.Contains(symbol))
                    {
                        matchesCount++;
                    }
                }

                if (decryptionResult == null || matchesCount > decryptionResult.MatchesCount)
                {
                    decryptionResult = new DecryptionResult()
                    {
                        Key = possibleKey,
                        DecryptedText = possibleMessage,
                        MatchesCount = matchesCount
                    };
                }
            }

            return decryptionResult;
        }
    }
}