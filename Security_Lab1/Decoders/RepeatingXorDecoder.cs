using System;
using System.Collections.Generic;
using System.Linq;

namespace Security_Lab1.Decoders
{
    public class RepeatingXorDecoder
    {
        public static string AttackRepeatingXor(string text)
        {
            var keysize = 3;
            var parts = new List<string>();

            for (int i = 0; i < 3; i++)
            {
                var part = XorDecoder.Decrypt(SplitByInterval(text, 3, i));
                parts.Add(part.DecryptedText);
            }

            var result = "";
            for (int i = 0; i < text.Length / 3; i++)
            {
                result += parts[0][i];
                result += parts[1][i];
                result += parts[2][i];
            }

            return result;
        }

        public static string SplitByInterval(string text, int interval, int start)
        {
            var res = "";
            for (int i = start; i < text.Length; i+=interval)
            {
                res += text[i];
            }

            return res;
        }

        // private static object FindKeyLength(string text, int minLength = 2, int maxLength = 30)
        // {
        //     var key = ScoreKeySize()
        // }
        //
        // private static object ScoreKeySize(int possibleKeySize, string text)
        // {
        //     var sliceSize = 2 * possibleKeySize;
        //     var measurement = text.Length / sliceSize + 1;
        //
        //     var score = 0;
        //     for (int i = 0; i < measurement; i++)
        //     {
        //         var s = sliceSize;
        //         var k = possibleKeySize;
        //         var slice1 = 
        //     }
        // }
        
        public static int GetHammingDistance(string s, string t)
        {
            if (s.Length != t.Length)
            {
                throw new Exception("Strings must be equal length");
            }

            int distance =
                s.ToCharArray()
                    .Zip(t.ToCharArray(), (c1, c2) => new { c1, c2 })
                    .Count(m => m.c1 != m.c2);

            return distance;
        }
        
        public static string FromHexToChars(string text)
        {
            var fromHex = "";
            for (int i = 0; i < text.Length; i+=2)
            {
                fromHex += Convert.ToChar(Convert.ToInt32($"{text[i]}{text[i + 1]}", 16));
            }
            
            return fromHex;
        }
    }
}