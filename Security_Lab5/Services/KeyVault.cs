using System.IO;
using NSec.Cryptography;

namespace Security_Lab5.Services
{
    public class KeyVault : IKeyVault
    {
        public KeyVault()
        {
                
        }

        public void CreateNewFile()
        {
            using var key = Key.Create(
                AeadAlgorithm.ChaCha20Poly1305, 
                new KeyCreationParameters 
                { 
                    ExportPolicy = KeyExportPolicies.AllowPlaintextExport 
                });
            var keyBlob = key.Export(KeyBlobFormat.NSecSymmetricKey);
                
            File.WriteAllBytes("key.nsec", keyBlob);
        }

        public byte[] Get()
        {
            var key = File.ReadAllBytes("key.nsec");
            
            return key;
        }
    }
}