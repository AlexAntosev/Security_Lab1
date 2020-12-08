using Microsoft.EntityFrameworkCore;
using Security_Lab5.Models;

namespace Security_Lab5.DataContext
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions contextOptions)
            : base(contextOptions)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
    }
}