using System.ComponentModel.DataAnnotations;

namespace Master_Details_With_Crud.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        [Required, StringLength(50), Display(Name = "Course Name")]
        public string CourseName { get; set; } = default!;

        public virtual ICollection<StudentCourse> StudentCourses { get; set; }=new List<StudentCourse>();
    }
}
