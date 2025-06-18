using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneToOne
{
    internal class EntityDatabase : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public EntityDatabase()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-O6DMGPJ\SQLEXPRESS;Database=TestDB_OneToOne;TrustServerCertificate=true;Trusted_Connection=True;");
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public UserProfile Profile { get; set; }
    }

    public class UserProfile
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }

  
}
