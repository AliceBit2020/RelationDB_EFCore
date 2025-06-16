using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationDB
{
    internal class EntityDatabase : DbContext
    {
        public DbSet<Team> Teams { get; set; }///Teams in DB
        public DbSet<Player> Players { get; set; }

        public EntityDatabase()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-O6DMGPJ\SQLEXPRESS;Database=TestDB_Teams;TrustServerCertificate=true;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            /////  по навыгаційним властивостям FluentApi
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players);
                //.HasForeignKey(p => p.TeamInfoKey);

        }
    }

    public class Team  ///1:N в одній команді багао гравців
    {
        public int Id { get; set; }
        public string Name { get; set; }


        ///Навігаційна властивість - колекція гравців  на С#
        public List<Player> Players { get; set; }
    }

    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }


        ///Як додати foreign key???


        //////1.Створюємо навігаційну властивість з назвою класс TeamId
      //  public int TeamId { get; set; }///автоматически создаст foreign key  відобразиться в базі


        ///2.  
        ///

        /////Щоб створити в БД Foreign Key TeamInfoKey
       // public int TeamInfoKey { get; set; }


        ////навыгаційна властивість на стороні С# щоб гравець знав про свою команду
      
        
      //  [ForeignKey("TeamInfoKey")]////  data anotation --  atributes
        public Team Team { get; set; }
    }
}
