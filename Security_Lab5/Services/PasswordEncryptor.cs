using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using NSec.Cryptography;

namespace Security_Lab5.Services
{
    public static class PasswordEncryptor
    {
        public static string HashPassword(string password, byte[] salt)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashedPassword;
        }
        
        public static string EncryptSensitiveData(string data, byte[] salt, byte[] keyBytesArray)
        {
            try
            {
                var dataBytesArray = Encoding.Default.GetBytes(data);
            
                var key = Key.Import(AeadAlgorithm.ChaCha20Poly1305, keyBytesArray, KeyBlobFormat.NSecSymmetricKey);
                var nonce = new Nonce(salt, 0);
                var encryptedData = AeadAlgorithm.ChaCha20Poly1305.Encrypt(key, nonce, null, dataBytesArray);

                var hashedEncryptedData = HexToBytesConverter.BytesArrayToHexString(encryptedData);

                return hashedEncryptedData;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public static string DecryptSensitiveData(string encryptedData, byte[] salt, byte[] keyBytesArray)
        {
            var dataBytesArray = HexToBytesConverter.HexStringToBytesArray(encryptedData);
            
            var key = Key.Import(AeadAlgorithm.ChaCha20Poly1305, keyBytesArray, KeyBlobFormat.NSecSymmetricKey);
            var nonce = new Nonce(salt, 0);
            var decryptedData = AeadAlgorithm.ChaCha20Poly1305.Decrypt(key, nonce, null, dataBytesArray, out var plaintext);

            if (decryptedData)
            {
                var hashedEncryptedData = HexToBytesConverter.BytesArrayToHexString(plaintext);

                return hashedEncryptedData;
            }
           
            throw new Exception("Decryption failed");
        }
        
        public static byte[] CreateSalt(int length)
        {
            var salt = new byte[length];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            return salt;
        }
    }
}