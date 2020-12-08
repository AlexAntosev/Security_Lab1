using Security_Lab3.Models;

namespace Security_Lab3.Encryptors
{
    public static class LcgEncryptor
    {
        public static long Next(long lastResult, LcgParams lcgParams)
        {
            var result = (lcgParams.Multiplier * lastResult + lcgParams.Increment) % lcgParams.Module;

            return result;
        }
    }
}