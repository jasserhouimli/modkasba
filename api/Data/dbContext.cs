using Microsoft.EntityFrameworkCore;
using api.Models;
namespace api.Data
{
    public class dbContext : DbContext
    {
        private readonly DbContextOptions<dbContext> _options;

        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {
            _options = options;
        }

        public DbSet<User> users { get; set; }
    }
}
