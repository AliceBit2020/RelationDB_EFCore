using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loading_data
{
    internal class EntityDatabase : DbContext
    {
        /// <summary>
        /// DbSet<> --- колекції данних
        /// </summary>
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public EntityDatabase()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-O6DMGPJ\SQLEXPRESS;Database=TestDB_TeamRelation;TrustServerCertificate=true;Trusted_Connection=True;");
        }
    }

    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int StadiumId { get; set; }///Id foreign key
        public Stadium Stadium { get; set; } //навігаційна властивість стадіон команди

        public List<Player> Players { get; set; }// /навігаційна властивість/ гравці команди
    }

    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int TeamId { get; set; }///Id foreign key
        public Team Team { get; set; }  // навігаційна властивість  команда гравця

        public int CountryId { get; set; }////Id foreign key
        public Country Country { get; set; }    //навігаційна властивість   країна гравця
    }

    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CapitalId { get; set; }///Id foreign key
        public City Capital { get; set; }  //навігаційна властивість столиця країни

        public List<Player> Players { get; set; }
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Stadium
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
