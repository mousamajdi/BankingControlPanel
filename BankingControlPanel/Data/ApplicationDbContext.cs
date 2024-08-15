using BankingControlPanel.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankingControlPanel.Data
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<QueryParams> QueryParams { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed roles
            var adminRoleId = Guid.NewGuid().ToString();
            var userRoleId = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = userRoleId, Name = "User", NormalizedName = "USER" }
            );

            // Seed an admin user
            var adminUserId = Guid.NewGuid().ToString();
            var adminUser = new ApplicationUser
            {
                Id = adminUserId,
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            adminUser.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(adminUser, "Admin@123");
            builder.Entity<ApplicationUser>().HasData(adminUser);
            // Assign admin user to the Admin role
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                }
            );

            // Seed clients
            builder.Entity<Client>().HasData(
                new Client
                {
                    Id = 1,
                    FirstName = "Mousa",
                    LastName = "Mousa",
                    Email = "mousa.majdi@gmail.com",
                    PersonalId = "401441456",
                    MobileNumber = "+970569375987",
                    Sex = "Male",
                    Country = "Palestine",
                    City = "Ramallah",
                    Street = "123 Main St",
                    ZipCode = "00970"
                }
            );

            // Seed accounts
            builder.Entity<Account>().HasData(
                new Account
                {
                    Id = 1,
                    AccountNumber = "ACC123456789",
                    Balance = 1000.00m,
                    ClientId = 1
                }
            );
        }
    }
}