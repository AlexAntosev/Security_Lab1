using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Security_Lab1.Converters;
using Security_Lab1.Models;
using Security_Lab1.Operators;

namespace Security_Lab1.Common
{
    public class Analyzer
    {
        public static string AnalyzeKey(List<Dictionary<string, string>> bruteForceResults)
        {
            var fullKey = string.Empty;
            
            foreach (var result in bruteForceResults)
            {
                string key = null;
                double currentHighestScore = 0;

                foreach (var attempt in result)
                {
                    var rating = Analyzer.EnglishRating(attempt.Value);
                    if (currentHighestScore <= rating)
                    {
                        key = attempt.Key;
                        currentHighestScore = rating;
                    }
                }

                fullKey += key;
            }

            return fullKey;
        }
        
        public static DecryptionResult AnalyzeResult(List<Dictionary<string, string>> bruteForceResults, byte[] cipherBytes)
        {
            var fullKey = AnalyzeKey(bruteForceResults);

            // Use key!
            var expandedKey = Key.Expand(fullKey, HexConverter.BytesToHexString(cipherBytes));
            var plainTextAsBytes = Xor.CompareByteArrays(cipherBytes, HexConverter.HexStringToBytes(expandedKey));
            var plaintext = Encoding.UTF8.GetString(plainTextAsBytes);

            return new DecryptionResult
            {
                RepeatingKey = fullKey,
                DecryptedText = plaintext
            };
        }
        
        public static DecryptionResult AnalyzeSingleResult(List<Dictionary<string, string>> bruteForceResults, byte[] cipherBytes)
        {
            var fullKey = Convert.ToByte(Convert.ToInt32(AnalyzeKey(bruteForceResults)));

            // Use key!
            var expandedKey = Key.Expand(fullKey, HexConverter.BytesToHexString(cipherBytes));
            var plainTextAsBytes = Xor.CompareByteArrays(cipherBytes, expandedKey);
            var plaintext = Encoding.UTF8.GetString(plainTextAsBytes);

            return new DecryptionResult
            {
                Key = (char)fullKey,
                DecryptedText = plaintext
            };
        }
        
        public static double EnglishRating(string text)
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