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
        public static DecryptionResult Attack(string hexCipher)
        {
            var bestKeySize = FindKeyLength(hexCipher);
            byte[] cipherBytes = HexConverter.HexStringToBytes(hexCipher);
            
            var blocksOfKeySize = cipherBytes.CreateMatrix(bestKeySize);
            
            var transposedBlocks = blocksOfKeySize.Transpose();

            var bruteForceResults = transposedBlocks
                .Select(x => SingleByteXorAttacker.AttackHex(HexConverter.BytesToHexString(x)))
                .ToList();

            return Analyzer.AnalyzeResult(bruteForceResults, cipherBytes);
        }
        
        public static int FindKeyLength(string cipher)
        {
            byte[] cipherText = HexConverter.HexStringToBytes(cipher);

            var keySizeResults = new Dictionary<int, int>();

            for (var keySize = 2; keySize <= 40; keySize++)
            {
                var hammingDistance = 0;
                var numberOfHams = 0;

                for (int i = 1; i < cipherText.Length / keySize; i++)
                {
                    var firstKeySizeBytes = cipherText.Skip(keySize * (i - 1)).Take(keySize).ToArray();
                    var secondKeySizeBytes = cipherText.Skip(keySize * i).Take(keySize).ToArray();

                    hammingDistance += firstKeySizeBytes.GetHammingDistance(secondKeySizeBytes);
                    numberOfHams++;
                }

                var normalizedDistance = hammingDistance / numberOfHams / keySize;
                keySizeResults.Add(keySize, normalizedDistance);
            }

            var orderedResults = keySizeResults.OrderBy(x => x.Value);
            var bestKeySize = orderedResults.First().Key;

            return bestKeySize;
        }
    }
}