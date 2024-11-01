using System.ComponentModel.DataAnnotations.Schema;

namespace Master_Details_With_Crud.Models
{
    public class StudentCourse
    {
        public int StudentCourseId { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        //nev
        public virtual Course? Course { get; set; }
        public virtual Student? Student { get; set; }
    }
}
