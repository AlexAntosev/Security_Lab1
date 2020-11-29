using System;
using System.Collections.Generic;
using System.Linq;
using Security_Lab1.Operators;

namespace Security_Lab1.Extensions
{
    public static class ByteExtensions
    {
        public static int GetHammingDistance(this byte[] x, byte[] y) {
            if (x.Length != y.Length)
            {
                throw new ArgumentException("values must be same length");
            }

            var distance = 0;

            for (var i = 0; i < x.Length; i++)
            {
                var value = (int)Xor.CompareBytes(x[i], y[i]);

                while (value != 0)
                {
                    distance++;
                    value &= value - 1;
                }
            }

            return distance;
        }   
        
        public static byte[][] CreateMatrix(this byte[] source, int size)
        {
            var taken = 0;
            var output = new List<byte[]>();
            var enumeratedSource = source.ToArray();

            while (taken < enumeratedSource.Length)
            {
                output.Add(enumeratedSource.Skip(taken).Take(size).ToArray());
                taken += size;
            }

            return output.ToArray();
        }
        
        public static byte[][] Transpose(this byte[][] source)
        {
            var transposedBlocks = new List<List<byte>>();

            for (var i = 0; i <= source.Length; i++)
            {
                foreach (var block in source.ToList())
                {
                    if (i < block.Length)
                    {
                        if (transposedBlocks.ElementAtOrDefault(i) == null)
                            transposedBlocks.Add(new List<byte>());

                        transposedBlocks[i].Add(block[i]);
                    }
                }
            }

            return transposedBlocks.Select(x => x.ToArray()).ToArray();
        }
    }
}