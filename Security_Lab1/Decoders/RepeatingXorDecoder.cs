using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Security_Lab1.Common;
using Security_Lab1.Converters;
using Security_Lab1.Extensions;
using Security_Lab1.Models;
using Security_Lab1.Operators;

namespace Security_Lab1.Decoders
{
    public class RepeatingXorDecoder
    {
        public static List<Dictionary<string, string>> Attack(byte[] cipher)
        {
            var bestKeySize = FindBestKeySize(cipher);
            var blocksOfKeySize = cipher.CreateMatrix(bestKeySize);
            var transposedBlocks = blocksOfKeySize.Transpose();

            var bruteForceResults = transposedBlocks
                .Select(x => SingleByteXorAttacker.Attack(x))
                .ToList();

            return bruteForceResults;
        }
        
        public static int FindBestKeySize(byte[] cipher)
        {
            var keySizes = new Dictionary<int, int>();
            for (var keySize = 2; keySize <= 40; keySize++)
            {
                var hammingDistance = 0;
                var numberOfHams = 0;

                for (int i = 1; i < cipher.Length / keySize; i++)
                {
                    var firstKeySizeBytes = cipher.Skip(keySize * (i - 1)).Take(keySize).ToArray();
                    var secondKeySizeBytes = cipher.Skip(keySize * i).Take(keySize).ToArray();

                    hammingDistance += firstKeySizeBytes.GetHammingDistance(secondKeySizeBytes);
                    numberOfHams++;
                }

                var normalizedDistance = hammingDistance / numberOfHams / keySize;
                keySizes.Add(keySize, normalizedDistance);
            }

            var orderedResults = keySizes.OrderBy(x => x.Value);
            var bestKeySize = orderedResults.First().Key;

            return bestKeySize;
        }
    }
}