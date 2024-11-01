using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Master_Details_With_Crud.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required,StringLength(50),Display(Name = "Student Name")]
        public string StudentName { get; set; } = default!;
        [Required, Display(Name = "Date Of Birth"), Column(TypeName ="date"),DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime DateOfBirth { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string? Image { get; set; } 
        public bool Fresher { get; set; }

        public virtual ICollection<StudentCourse> StudentSubjects { get; set; } = new List<StudentCourse>();
    }
}
