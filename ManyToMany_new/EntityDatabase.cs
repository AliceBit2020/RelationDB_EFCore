using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManyToMany
{
    internal class EntityDatabase : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public EntityDatabase()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Course>()
                .HasMany(sc => sc.StudentList)
                .WithMany(s => s.CoursesList)
                .UsingEntity("StudentCourse",
               s => s.HasOne(typeof(Student)).WithMany().HasForeignKey("StudentId").HasPrincipalKey(nameof(Student.Id)),
               c => c.HasOne(typeof(Course)).WithMany().HasForeignKey("CourseId").HasPrincipalKey(nameof(Course.Id)),
               sc => sc.HasKey("StudentId", "CourseId"));

            modelBuilder.Entity<Student>()
               .HasMany(sc => sc.CoursesList)
               .WithMany(s => s.StudentList)
               .UsingEntity("StudentCourse",
               s => s.HasOne(typeof(Student)).WithMany().HasForeignKey("StudentId").HasPrincipalKey(nameof(Student.Id)),
               c=>c.HasOne(typeof(Course)).WithMany().HasForeignKey("CourseId").HasPrincipalKey(nameof(Course.Id)),
               sc=>sc.HasKey("StudentId", "CourseId"));

            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-O6DMGPJ\SQLEXPRESS;Database=TestDB_ManyToManynew;TrustServerCertificate=true;Trusted_Connection=True;");
        }
    }


    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> StudentList { get; set; }

        public Course()
        {
            StudentList = new List<Student>();
        }
    }
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Course> CoursesList { get; set; }

        public Student()
        {
            CoursesList = new List<Course>();
        }
    }
    
}
