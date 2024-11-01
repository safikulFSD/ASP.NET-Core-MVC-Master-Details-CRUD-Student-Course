using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Master_Details_With_Crud.Models.ViewModels
{
    public class StudentVM
    {
        public int StudentId { get; set; }
        [Required, StringLength(50), Display(Name = "Student Name")]
        public string StudentName { get; set; } = default!;
        [Required, Display(Name = "Date Of Birth"), Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string? Image { get; set; }
        public IFormFile? ImagePath { get; set; }
        public bool Fresher { get; set; }
        public List<int> CourseList { get; set; } = new List<int>();
    }
}
