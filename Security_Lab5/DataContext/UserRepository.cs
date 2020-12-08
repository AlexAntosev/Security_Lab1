using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Security_Lab5.Models;

namespace Security_Lab5.DataContext
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDataContext _dbContext;

        public UserRepository(ApplicationDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(User model)
        {
            await _dbContext.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> Get(string email)
        {
            var entity = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return entity;
        }
    }
}