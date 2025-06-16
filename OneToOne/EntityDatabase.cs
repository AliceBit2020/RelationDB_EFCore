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
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public EntityDatabase()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=relationsdb;Trusted_Connection=True;");
        }
    }

    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Player> Players { get; set; } // гравці команди
    }

    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }  // команда гравця
    }
}
