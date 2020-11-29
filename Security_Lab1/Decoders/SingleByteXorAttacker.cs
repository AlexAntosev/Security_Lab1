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
        public static Dictionary<string, string> Attack(byte[] cipher)
        {
            var outputs = new Dictionary<string, string>();
            foreach (byte key in Enumerable.Range(0, 127))
            {
                var expandedKey = Key.Expand(key, cipher);
                
                var outputBytes = Xor.CompareByteArrays(cipher, expandedKey);
                outputs.Add(key.ToString(), Encoding.ASCII.GetString(outputBytes));
            }

            return outputs;
        }
    }
}