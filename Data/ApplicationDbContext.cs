using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppDemo3.Models;

namespace WebAppDemo3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebAppDemo3.Models.Book> Book { get; set; }
        public DbSet<WebAppDemo3.Models.Category> Category { get; set; }
        public DbSet<WebAppDemo3.Models.Order> Order { get; set; }
    }
}