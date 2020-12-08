using System;
using System.Threading.Tasks;
using Security_Lab5.Controllers;
using Security_Lab5.DataContext;
using Security_Lab5.Models;

namespace Security_Lab5.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> Register(UserModel userModel)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = userModel.Email,
                PasswordHash = userModel.Password,
                PasswordSalt = "123"
            };
            
            await _userRepository.Add(user);

            return true;
        }

        public async Task<bool> Login(UserModel userModel)
        {
            var userEntity =  await _userRepository.Get(userModel.Email);

            if (userEntity.PasswordHash == userModel.Password)
            {
                return true;
            }

            return false;
        }
    }
}