using Microsoft.EntityFrameworkCore;

namespace ManyToMany
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (EntityDatabase db = new EntityDatabase())
            {
                Student s1 = new Student { Name = "Tom" };
                Student s2 = new Student { Name = "Alice" };
                Student s3 = new Student { Name = "Bob" };
                db.Students.AddRange(new List<Student> { s1, s2, s3 });

                Course c1 = new Course { Name = "Алгоритми" };
                Course c2 = new Course { Name = "Основи програмування" };
                db.Courses.AddRange(new List<Course> { c1, c2 });

                db.SaveChanges();

                // додаємо до студентів курси
                s1.StudentCourses.Add(new StudentCourse { CourseId = c1.Id, StudentId = s1.Id });
                s2.StudentCourses.Add(new StudentCourse { CourseId = c1.Id, StudentId = s2.Id });
                s2.StudentCourses.Add(new StudentCourse { CourseId = c2.Id, StudentId = s2.Id });
                db.SaveChanges();

                var courses = db.Courses.Include(c => c.StudentCourses).ThenInclude(sc => sc.Student).ToList();
                // виводимо всі курси
                foreach (var c in courses)
                {
                    Console.WriteLine($"\n Course: {c.Name}");
                    // виводимо всіх студентів для цього курсу
                    var students = c.StudentCourses.Select(sc => sc.Student).ToList();
                    foreach (Student s in students)
                        Console.WriteLine($"{s.Name}");
                }
            }
        }
    }
}