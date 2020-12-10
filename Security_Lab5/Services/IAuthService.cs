using System.Threading.Tasks;
using Security_Lab5.Controllers;
using Security_Lab5.Models;

namespace Security_Lab5.Services
{
    public interface IAuthService
    {
        Task<bool> Register(UserModel userModel);
        
        Task<UserModel> Login(UserModel userModel);
    }
}