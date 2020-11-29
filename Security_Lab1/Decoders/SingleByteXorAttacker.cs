using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Security_Lab1.Common;
using Security_Lab1.Converters;
using Security_Lab1.Encoders;
using Security_Lab1.Models;
using Security_Lab1.Operators;

namespace Security_Lab1.Decoders
{
    public class SingleByteXorAttacker
    {
        public static Dictionary<string, string> AttackHex(string cipher)
        {
            var outputs = new Dictionary<string, string>();
            foreach (var key in Enumerable.Range(0, 127))
            {
                var keyAsHex = Convert.ToString(key, 16);
                var expandedKey = Key.Expand(keyAsHex, cipher);
                
                var outputHex = Xor.CompareStrings(cipher, expandedKey);
                var outputBytes = HexConverter.HexStringToBytes(outputHex);
                outputs.Add(keyAsHex, Encoding.ASCII.GetString(outputBytes));
            }

            return outputs;
        }
        
        public static Dictionary<string, string> AttackBytes(string cipher)
        {
            byte[] bytesCipher = Encoding.ASCII.GetBytes(cipher);
            var outputs = new Dictionary<string, string>();
            foreach (byte key in Enumerable.Range(0, 127))
            {
                var expandedKey = Key.Expand(key, cipher);
                
                var outputBytes = Xor.CompareByteArrays(bytesCipher, expandedKey);
                outputs.Add(key.ToString(), Encoding.ASCII.GetString(outputBytes));
            }

            return outputs;
        }
    }
}