using adapthub_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace adapthub_api
{
    public class DataContext : IdentityDbContext<Customer>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=adapthub");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().ToTable("Users");
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Moderator> Moderators { get; set; }
        public DbSet<JobRequest> JobRequests { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<IdentityUserClaim<string>> UserClaims { get; set; }
    }
}
