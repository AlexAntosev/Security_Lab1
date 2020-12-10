using System.Threading.Tasks;
using Security_Lab5.Models;

namespace Security_Lab5.Services
{
    public interface ISensitiveDataService
    {
        Task Post(UserModel userModel, string creditCard);

        Task<string> Get(string email);
    }
}