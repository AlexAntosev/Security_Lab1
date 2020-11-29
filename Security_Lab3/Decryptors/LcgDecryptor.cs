using Security_Lab3.Models;

namespace Security_Lab3.Decryptors
{
    public static class LcgDecryptor
    {
        public static LcgParams AttackIncrement(long result, long previousResult, LcgParams lcgParams)
        {
            lcgParams.Increment = (result - previousResult * lcgParams.Multiplier) % lcgParams.Module;

            return lcgParams;
        }
        
        public static LcgParams AttackMultiplier(long result, long previousResult, long prePreviousResult, LcgParams lcgParams)
        {
            lcgParams.Multiplier = (result - previousResult) *
                ReverseMod(previousResult - prePreviousResult, lcgParams.Module) % lcgParams.Module;

            return AttackIncrement(result, previousResult, lcgParams);
        }

        private static long ReverseMod(long b, long n)
        {
            var (g, x, res) = Reverse(b, n);
            if (g == 1)
            {
                return x % n;
            }

            return res;
        }
        
        private static (long, long, long) Reverse(long a, long b)
        {
            if (a == 0)
            {
                return (b, 0, 1);
            }

            var (g, x, y) = Reverse(b % a, a);
            return (g, y - (b / a) * x, x);
        }
    }
}