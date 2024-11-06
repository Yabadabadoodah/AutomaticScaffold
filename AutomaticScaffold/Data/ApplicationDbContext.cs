using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AutomaticScaffold.Models;

namespace AutomaticScaffold.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AutomaticScaffold.Models.Stocks> Stocks { get; set; } = default!;
    }
}
