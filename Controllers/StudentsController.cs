using Master_Details_With_Crud.Models;
using Master_Details_With_Crud.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Master_Details_With_Crud.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IWebHostEnvironment _he;
        private readonly StudentDbContext _context;

        public StudentsController(IWebHostEnvironment _he, StudentDbContext _context)
        {
            this._he = _he;
            this._context = _context;
        }
        
        public async Task<IActionResult> Index()
        {
            return View( await _context.Students.Include(x=>x.StudentSubjects).ThenInclude(y=>y.Course).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult AddNewCourses(int? id)
        {
          
            ViewBag.course = new SelectList(_context.Courses, "CourseId", "CourseName", id.ToString() ??"");
            return PartialView("_AddNewCourses");
        }
        [HttpPost]
        public async Task<IActionResult> Create(StudentVM studentVM, int[] courseId)
        {
            if (ModelState.IsValid)
            {
                Student student = new Student()
                {
                    StudentName = studentVM.StudentName,
                    DateOfBirth = studentVM.DateOfBirth,
                    Phone = studentVM.Phone,
                    Fresher = studentVM.Fresher
                };
                //image
                var file = studentVM.ImagePath;
                string webroot = _he.WebRootPath;
                string folder = "Images";
                string imgFileName = DateTime.Now.Ticks.ToString() + "_" + Path.GetFileName(studentVM.ImagePath.FileName);
                string fileToSave = Path.Combine(webroot, folder, imgFileName);

                if (file != null)
                {
                    using (var stream = new FileStream(fileToSave, FileMode.Create))
                    {
                        studentVM.ImagePath.CopyTo(stream);
                        student.Image = "/" + folder + "/" + imgFileName;
                    }
                }
                foreach (var item in courseId)
                {
                    StudentCourse studentCourse = new StudentCourse()
                    {
                        Student = student,
                        StudentId = student.StudentId,
                        CourseId = item
                    };
                    _context.StudentCourses.Add(studentCourse);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.StudentId == id);

            StudentVM studentVM = new StudentVM()
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                DateOfBirth = student.DateOfBirth,
                Phone = student.Phone,
                Image = student.Image,
                Fresher = student.Fresher
            };
            var existCourse = _context.StudentCourses.Where(x => x.StudentId == id).ToList();
            foreach (var item in existCourse)
            {
                studentVM.CourseList.Add(item.CourseId);
            }
            return View(studentVM);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(CandidateVM candidateVM, int[] SkillId)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Candidate candidate = new Candidate()
        //        {
        //            CandidateId = candidateVM.CandidateId,
        //            CandidateName = candidateVM.CandidateName,
        //            DateOfBirth = candidateVM.DateOfBirth,
        //            Phone = candidateVM.Phone,
        //            Fresher = candidateVM.Fresher,
        //            Image = candidateVM.Image
        //        };
        //        var file = candidateVM.ImagePath;
        //        string existImg = candidateVM.Image;

        //        if (file != null)
        //        {
        //            string webroot = _he.WebRootPath;
        //            string folder = "Images";
        //            string imgFileName = DateTime.Now.Ticks.ToString() + "_" + Path.GetFileName(candidateVM.ImagePath.FileName);
        //            string fileToSave = Path.Combine(webroot, folder, imgFileName);
        //            using (var stream = new FileStream(fileToSave, FileMode.Create))
        //            {
        //                candidateVM.ImagePath.CopyTo(stream);
        //                candidate.Image = "/" + folder + "/" + imgFileName;
        //            }

        //        }
        //        else
        //        {
        //            candidate.Image = existImg;
        //        }

        //        var existSkill = _context.CandidateSkills.Where(x => x.CandidateId == candidate.CandidateId).ToList();
        //        //Remove
        //        foreach (var item in existSkill)
        //        {
        //            _context.CandidateSkills.Remove(item);
        //        }
        //        //Add
        //        foreach (var item in SkillId)
        //        {
        //            CandidateSkill candidateSkill = new CandidateSkill()
        //            {
        //                CandidateId = candidate.CandidateId,
        //                SkillId = item
        //            };
        //            _context.CandidateSkills.Add(candidateSkill);
        //        }
        //        _context.Update(candidate);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View();
        //}

        public async Task<IActionResult> Delete(int ? id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.StudentId == id);
            var exitCourse = _context.StudentCourses.Where(x=>x.StudentId == id).ToList();
            foreach (var item in exitCourse)
            {
                _context.StudentCourses.Remove(item);
            }
            _context.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //public async Task<IActionResult> Delete(int? id)
        //{
        //    var candidate = await _context.Candidates.FirstOrDefaultAsync(x => x.CandidateId == id);
        //    var existSkill = _context.CandidateSkills.Where(x => x.CandidateId == id).ToList();
        //    foreach (var item in existSkill)
        //    {
        //        _context.CandidateSkills.Remove(item);
        //    }

        //    _context.Remove(candidate);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
