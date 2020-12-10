using NSec.Cryptography;

namespace Security_Lab5.Services
{
    public class KeyVault : IKeyVault
    {
        public KeyVault()
        {
            using var key = Key.Create(
                AeadAlgorithm.ChaCha20Poly1305, 
                new KeyCreationParameters 
                { 
                    ExportPolicy = KeyExportPolicies.AllowPlaintextExport 
                });
            _key = key.Export(KeyBlobFormat.NSecSymmetricKey);
        }
        
        private byte[] _key;

        public byte[] Get()
        {
            return _key;
        }
    }
}