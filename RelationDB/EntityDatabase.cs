using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationDB
{
    internal class EntityDatabase : DbContext
    {
        public DbSet<Team> Teams { get; set; }///Teams in DB
        public DbSet<Player> Players { get; set; }

        public DbSet<User> Users { get; set; }

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
                .WithMany(t => t.Players)
            .HasForeignKey(p => p.TeamId);

            //modelBuilder.Entity<Player>().HasOne<Team>().WithMany()
            //    .HasForeignKey(p => p.TeamId).HasConstraintName("FK_Player_Team").OnDelete(DeleteBehavior.Restrict);


            //modelBuilder.Entity<Player>().Property(p => p.TeamId).HasColumnName("player_team_FK");

           


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
        [Key]
        [ForeignKey(nameof(User))]
        public int Id { get; set; }
        public double Raiting { get; set; }
         public User User { get; set; }

        ///Як додати foreign key???


        //////1.Створюємо foreign key з назвою класс TeamId
        
      
       public int TeamId { get; set; }///автоматически создаст foreign key  відобразиться в базі


        ///2.  
        ///

        /////Щоб створити в БД Foreign Key TeamInfoKey
       // public int TeamInfoKey { get; set; }


        ////навыгаційна властивість на стороні С# щоб гравець знав про свою команду
      
        
       //[ForeignKey("TeamId")]////  data anotation --  atributes
        public Team Team { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
       


    }
}
