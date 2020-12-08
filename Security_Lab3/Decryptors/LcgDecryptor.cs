using Security_Lab3.Models;

namespace Security_Lab3.Decryptors
{
    public static class LcgDecryptor
    {
        public static LcgParams AttackIncrement(long secondResult, long firstResult, LcgParams lcgParams)
        {
            lcgParams.Increment = (secondResult - firstResult * lcgParams.Multiplier) % lcgParams.Module;

            return lcgParams;
        }
        
        public static LcgParams AttackMultiplier(long thirdResult, long secondResult, long firstResult, LcgParams lcgParams)
        {
            lcgParams.Multiplier = (thirdResult - secondResult) *
                RevertMod((secondResult - firstResult), lcgParams.Module) % lcgParams.Module;

            return AttackIncrement(thirdResult, secondResult, lcgParams);
        }
        
        private static long RevertMod(long number, long mod)
        {
            if (mod == 1)
            {
                return 0;
            }
            
            var m0 = mod;
            (long a, long b) = (1, 0);

            while (number > 1)
            {
                var res = number / mod;
                (number, mod) = (mod, number % mod);
                (a, b) = (b, a - res * b);
            }
            return a < 0 ? a + m0 : a;
        }
    }
}