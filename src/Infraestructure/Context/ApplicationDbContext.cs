using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<RfidCard> RfidCards { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Roles> Roles { get; set; }

        public DbSet<UserRoles> UserRoles { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }        

        public DbSet<RegisterLog> RegisterLogs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Students = Set<Student>();
            RfidCards = Set<RfidCard>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.Cascade;

            base.OnModelCreating(modelBuilder);
        }
    }
}
