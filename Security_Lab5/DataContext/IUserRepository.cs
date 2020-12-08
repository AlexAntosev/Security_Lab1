using System.Threading.Tasks;
using Security_Lab5.Models;

namespace Security_Lab5.DataContext
{
    public interface IUserRepository
    {
        Task Add(User model);

        Task<User> Get(string email);
    }
}