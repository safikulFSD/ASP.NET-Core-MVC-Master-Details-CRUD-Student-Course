using Microsoft.EntityFrameworkCore;

namespace Master_Details_With_Crud.Models
{
    public class StudentDbContext:DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options):base(options)
        {
            
        }
        public DbSet<Course> Courses { get; set; } 
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
    }
}
