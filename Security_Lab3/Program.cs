using System;
using System.Collections.Generic;
using Security_Lab3.Decryptors;
using Security_Lab3.Models;
using Security_Lab3.Services;

namespace Security_Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Lcg
            var casinoService = new CasinoService();
            var results = new List<PlayResult>();
            for (int i = 0; i < 3; i++)
            {
                var playResult = casinoService.Play("171717").Result;
                results.Add(playResult);
            }

            var lcgParams = LcgDecryptor.AttackMultiplier(
                results[2].RealNumber,
                results[1].RealNumber,
                results[0].RealNumber,
                new LcgParams());

            Console.ReadKey();
        }
    }
}