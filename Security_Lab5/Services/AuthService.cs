using System;
using System.Threading.Tasks;
using Security_Lab5.DataContext;
using Security_Lab5.Models;

namespace Security_Lab5.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        
        private const int SaltLength = 16;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> Register(UserModel userModel)
        {
            var salt = PasswordEncryptor.CreateSalt(SaltLength);
            var hashedPassword = PasswordEncryptor.HashPassword(userModel.Password, salt);
            
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = userModel.Username,
                PasswordHash = hashedPassword,
                PasswordSalt = HexToBytesConverter.BytesArrayToHexString(salt)
            };
            
            await _userRepository.Add(user);

            return true;
        }

        public async Task<UserModel> Login(UserModel userModel)
        {
            var userEntity =  await _userRepository.Get(userModel.Username);
            
            if (userEntity != null)
            {
                var salt = HexToBytesConverter.HexStringToBytesArray(userEntity.PasswordSalt);
                var hashedPassword = PasswordEncryptor.HashPassword(userModel.Password, salt);
                if (hashedPassword == userEntity.PasswordHash)
                {
                    userModel.Token = "Fake-Token";
                    
                    return userModel;
                }
            }

            throw new Exception("Login failed");
        }
    }
}