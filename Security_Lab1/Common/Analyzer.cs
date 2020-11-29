using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Security_Lab1.Models;
using Security_Lab1.Operators;

namespace Security_Lab1.Common
{
    public class Analyzer
    {
        public static DecryptionResult AnalyzeResult(List<Dictionary<string, string>> bruteForceResults, byte[] cipherBytes)
        {
            var fullKey = AnalyzeKey(bruteForceResults);

            // Use key!
            var expandedKey = Key.Expand(fullKey, cipherBytes);
            var plainTextAsBytes = Xor.CompareByteArrays(cipherBytes, expandedKey);
            var plaintext = Encoding.UTF8.GetString(plainTextAsBytes);

            return new DecryptionResult
            {
                RepeatingKey = Encoding.ASCII.GetString(fullKey),
                DecryptedText = plaintext
            };
        }
        
        private static byte[] AnalyzeKey(List<Dictionary<string, string>> bruteForceResults)
        {
            var fullKey = new byte[bruteForceResults.Count];

            for (int i = 0; i < bruteForceResults.Count; i++)
            {
                byte key = 0;
                double currentHighestScore = 0;

                foreach (var attempt in bruteForceResults[i])
                {
                    var rating = EnglishRating(attempt.Value);
                    if (currentHighestScore <= rating)
                    {
                        key = Convert.ToByte(attempt.Key);
                        currentHighestScore = rating;
                    }
                }

                fullKey[i] = key;
            }

            return fullKey;
        }

        private static double EnglishRating(string text)
        {
            var chars = text.ToUpper().GroupBy(c => c).Select(g => new {g.Key, Count = g.Count()});

            double coefficient = 0;

            foreach (var c in chars)
            {
                if (LettersFrequency.TryGetValue(c.Key, out var freq))
                {
                    coefficient += Math.Sqrt(freq * c.Count / text.Length);
                }
            }

            return coefficient;
        }

        // http://pi.math.cornell.edu/~mec/2003-2004/cryptography/subs/frequencies.html (including space character)
        private static readonly Dictionary<char, double> LettersFrequency = new Dictionary<char, double>
        {
            {'E', 12.02},
            {'T', 9.10},
            {'A', 8.12},
            {'O', 7.68},
            {'I', 7.31},
            {'N', 6.95},
            {'S', 6.28},
            {'R', 6.02},
            {'H', 5.92},
            {'D', 4.32},
            {'L', 3.98},
            {'U', 2.88},
            {'C', 2.71},
            {'M', 2.61},
            {'F', 2.30},
            {'Y', 2.11},
            {'W', 2.09},
            {'G', 2.03},
            {'P', 1.82},
            {'B', 1.49},
            {'V', 1.11},
            {'K', 0.69},
            {'X', 0.17},
            {'Q', 0.11},
            {'J', 0.10},
            {'Z', 0.07},
            {' ', 0.19}
        };
    }
}