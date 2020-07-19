using Microsoft.EntityFrameworkCore;
using PS.Users.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Users.Repositories.Contexts
{
    public class UsersContext : DbContext
    {
        public UsersContext()
        {
        }

        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=PS.Users;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).UseIdentityColumn();
                entity.Property(e => e.Username).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
                entity.Ignore(e => e.Password);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.PasswordSalt).IsRequired();
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId);
                entity.Property(e => e.RoleId).UseIdentityColumn();
                entity.Property(e => e.Name).HasMaxLength(50).IsRequired();

                entity.HasData(
                    new Role { RoleId = 1, Name = "Admin" }
                );
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
                entity.HasOne(e => e.User).WithMany(u => u.UserRole).HasForeignKey(e => e.UserId);
                entity.HasOne(e => e.Role).WithMany(r => r.UserRole).HasForeignKey(e => e.RoleId);
            });
        }
    }
}
