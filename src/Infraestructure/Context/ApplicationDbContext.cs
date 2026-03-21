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

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Habilitation> Habilitations { get; set; }

        public DbSet<TeacherHabilitation> TeacherHabilitations { get; set; }

        public DbSet<Classes> Classes { get; set; }

        public DbSet<Classroom> Classrooms { get; set; }

        public DbSet<Holiday> Holidays { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Students = Set<Student>();
            RfidCards = Set<RfidCard>();
            Users = Set<User>();
            Roles = Set<Roles>();
            UserRoles = Set<UserRoles>();
            RefreshTokens = Set<RefreshToken>();
            RegisterLogs = Set<RegisterLog>();
            Teachers = Set<Teacher>();
            Habilitations = Set<Habilitation>();
            TeacherHabilitations = Set<TeacherHabilitation>();
            Classes = Set<Classes>();
            Classrooms = Set<Classroom>();
            Holidays = Set<Holiday>();
            Schedules = Set<Schedule>();
            Attendances = Set<Attendance>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Cascade;

            base.OnModelCreating(modelBuilder);
        }
    }
}
