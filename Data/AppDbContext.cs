

using Microsoft.EntityFrameworkCore;
using SchoolRegistrationForm.Models;

namespace SchoolRegistrationForm.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Register> Questions { get; set; }
    }
}