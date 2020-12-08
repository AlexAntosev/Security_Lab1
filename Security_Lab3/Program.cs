using System;
using System.Collections.Generic;
using Security_Lab3.Decryptors;
using Security_Lab3.Encryptors;
using Security_Lab3.Models;
using Security_Lab3.Services;

namespace Security_Lab3
{
    class Program
    {
        private const string AccountId = "171718";
        
        static void Main(string[] args)
        {
            // Lcg
            var casinoService = new CasinoService();
            var results = new List<PlayResult>();
            var account = casinoService.CreateAccount(AccountId).Result;
            
            for (var i = 0; i < 3; i++)
            {
                var playResult = casinoService.Play(AccountId, 17).Result;
                results.Add(playResult);
            }

            var lcgParams = LcgDecryptor.AttackMultiplier(
                results[2].RealNumber,
                results[1].RealNumber,
                results[0].RealNumber,
                new LcgParams());

            var result = LcgEncryptor.Next(results[2].RealNumber, lcgParams);
            var successfulResult = casinoService.Play(AccountId, result).Result;

            Console.WriteLine(successfulResult.Message);
        }
    }
}