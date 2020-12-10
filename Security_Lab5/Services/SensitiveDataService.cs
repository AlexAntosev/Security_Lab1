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
        private readonly IKeyVault _keyVault;

        public SensitiveDataService(IUserRepository userRepository, IKeyVault keyVault)
        {
            _userRepository = userRepository;
            _keyVault = keyVault;
        }
        public async Task Post(UserModel userModel, string creditCard)
        {
            var user = await _userRepository.Get(userModel.Username);

            if (user != null)
            {
                var salt = PasswordEncryptor.CreateSalt(ChaCha20Poly1305.ChaCha20Poly1305.NonceSize);
                var key = _keyVault.Get();
                var hashedCreditCard = PasswordEncryptor.EncryptSensitiveData(creditCard, salt, key);

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
                var key = _keyVault.Get();
                var hashedCreditCard = PasswordEncryptor.DecryptSensitiveData(user.CreditCardHash, salt, key);

                var creditCard = HexToBytesConverter.HexStringToBytesArray(hashedCreditCard);

                return Encoding.Default.GetString(creditCard);
            }
            
            throw new Exception("Decryption failed");
        }
    }
}