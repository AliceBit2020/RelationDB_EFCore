using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2_DeleteBehavior
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
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-O6DMGPJ\SQLEXPRESS;Database=relationsDeletedb;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }

    public class Team////main
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Player> Players { get; set; } = new List<Player>();
    }

    public class Player//related
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? TeamId { get; set; } // зовнішній ключ
        public Team? Team { get; set; }  // навігаційна властівість
    }
}
