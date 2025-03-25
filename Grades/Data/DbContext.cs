
using Microsoft.EntityFrameworkCore;
using Grades.Models;
namespace Grades.Data
{
    public class MySQLDbContext : DbContext
    {
        public MySQLDbContext(
                  DbContextOptions<MySQLDbContext> options) : base(options) { }
        public DbSet<Subject_> subjects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
