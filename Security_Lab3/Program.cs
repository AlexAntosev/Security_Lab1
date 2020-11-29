using System;
using Security_Lab3.Services;

namespace Security_Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Lcg
            var casinoService = new CasinoService();
            var playResult = casinoService.Play("171717").Result;

            Console.ReadKey();
        }
    }
}