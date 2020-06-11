using EFCoreOnDeleteTest.MOdel;
using Microsoft.EntityFrameworkCore;

namespace EFCoreOnDeleteTest.Data
{
    public class EFCoreOnDeleteTestContext : DbContext
    {
        public EFCoreOnDeleteTestContext (DbContextOptions<EFCoreOnDeleteTestContext> options)
            : base(options)
        {
        }

        public DbSet<Grade> Grades { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<UserPolicy> UserPolicies { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grade>()
                  .HasMany(p => p.Students)
                  .WithOne(t => t.Grade)
                  .HasForeignKey(t => t.GradeId)
                  .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                 .HasOne(p => p.UserPolicy)
                 .WithMany(t => t.Users)
                 .HasForeignKey(t => t.UserPolicyId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(p => p.User)
                .WithMany(t => t.UserRoles)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserRole>()
              .HasOne(p => p.Role)
              .WithMany(t => t.UserRoles)
              .HasForeignKey(t => t.RoleId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
