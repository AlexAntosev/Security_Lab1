using System;
using System.Text;
using System.Threading.Tasks;
using NSec.Cryptography;
using Security_Lab5.DataContext;
using Security_Lab5.Models;

namespace Security_Lab5.Services
{
    public class SensitiveDataService : ISensitiveDataService
    {
        private readonly IUserRepository _userRepository;
        private readonly byte[] _key;

        public SensitiveDataService(IUserRepository userRepository, IKeyVault keyVault)
        {
            _userRepository = userRepository;
            _key = keyVault.Get();
        }
        public async Task Post(UserModel userModel, string creditCard)
        {
            var user = await _userRepository.Get(userModel.Username);

            if (user != null)
            {
                var salt = PasswordEncryptor.CreateSalt(ChaCha20Poly1305.ChaCha20Poly1305.NonceSize);
                var hashedCreditCard = PasswordEncryptor.EncryptSensitiveData(creditCard, salt, _key);

                user.CreditCardHash = hashedCreditCard;
                user.CreditCardSalt = HexToBytesConverter.BytesArrayToHexString(salt);
                
                await _userRepository.Update(user);
            }
        }
        
        public async Task<string> Get(string email)
        {
            var user = await _userRepository.Get(email);

            if (user != null)
            {
                var salt = HexToBytesConverter.HexStringToBytesArray(user.CreditCardSalt);
                var hashedCreditCard = PasswordEncryptor.DecryptSensitiveData(user.CreditCardHash, salt, _key);

                var creditCard = HexToBytesConverter.HexStringToBytesArray(hashedCreditCard);

                return Encoding.Default.GetString(creditCard);
            }
            
            throw new Exception("Decryption failed");
        }
    }
}