using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PromDresses.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Dress> Dresses { get; set; }
        public DbSet<Accessorie> Accessories { get; set; }
        public DbSet<Collection>Collections { get; set; }
        public DbSet<OrderDress> OrderDresses { get; set; }
        public DbSet<OrderAccessorie> OrderAccessories { get; set; }
        
    }
}
