using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudyingApp.Models;
using StudyingApp.Repositories;
using StudyingApp.ViewModels;

namespace StudyingApp.Controllers
{
    [Authorize (Roles = "Student, Teacher, Admin, Unverified")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IRepository _repository;

        public HomeController(ILogger<HomeController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [Authorize(Roles = "Student, Teacher, Admin, Unverified")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Student, Teacher, Admin")]
        public IActionResult Schedule()
        {
            IEnumerable<Module> modules = _repository.GetScheduleModulesList();
            return View(modules);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Students()
        {
            IEnumerable<Student> students = _repository.GetStudentsList(true);
            return View(students);
        }

        [Authorize(Roles = "Student, Teacher, Admin")]
        public IActionResult Courses()
        {
            IEnumerable<Course> courses = _repository.GetCoursesList();
            return View(courses);
        }

        [Authorize(Roles = "Student, Teacher, Admin")]
        [HttpGet]
        public IActionResult Rating()
        {
            IEnumerable<Student> students = _repository.GetRatingStudents(DateTime.Now.Year, 0, 0);

            List<int> years = new List<int>();
            List<Course> courses = new List<Course>();

            foreach (Student student in students)
            {
                foreach (Listeners listener in student.Listeners)
                {
                    if (!years.Contains(listener.Course.Year))
                    {
                        years.Add(listener.Course.Year);
                    }

                    if (!courses.Contains(listener.Course))
                    {
                        courses.Add(listener.Course);
                    }
                }
            }

            RatingViewModel model = new RatingViewModel
            {
                Students = students,
                Years = years,
                Courses = courses
            };

            return View(model);
        }

        [Authorize(Roles = "Student, Teacher, Admin")]
        [HttpPost]
        public IActionResult Rating(RatingViewModel viewModel)
        {
            IEnumerable<Student> students = _repository.GetRatingStudents(viewModel.Year, viewModel.CourseId, viewModel.StudentId);

            if (viewModel.Year != 0)
            {
                viewModel.Year = DateTime.Now.Year;
                return View(CreateRatingModel(viewModel, students));
            }
            else
            {
                ModelState.AddModelError("", "Оберіть рік перегляду!");
                return View(CreateRatingModel(viewModel, students));
            }
        }

        public IActionResult Course(int id)
        {
            var course = _repository.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpGet]
        public IActionResult AddCourse()
        {
            return View();
        }
       
        [HttpPost]
        public IActionResult AddCourse(AddCourseViewModel model)
        {
            if(ModelState.IsValid)
            {
                if(DateTime.Compare(model.StartDate, model.EndDate) < 0) 
                {
                    Course course = new Course
                    {
                        Name = model.Name,
                        Year = model.Year,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate
                    };

                    _repository.CreateCourse(course);
                    _repository.CreateListenersForCourse(_repository.GetLastCourseId());

                    return RedirectToAction("Courses");
                }
                else
                {
                    ModelState.AddModelError("", "Дата початку має бути раніше за дату кінця");
                    return View();
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult EditCourse(int id)
        {
            Course course = _repository.GetCourseById(id);
            return View("AddCourse", course);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher, Admin")]
        public IActionResult AddMark(int id)
        {
            ViewBag.Students = _repository.GetStudentsList(true);
            ViewBag.TaskId = id;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Teacher, Admin")]
        public IActionResult AddMark(MarkViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                _repository.CreateMark(new Mark
                {
                    TaskId = id,
                    MarkValue = model.Mark,
                    StudentId = model.StudentId
                });

                return RedirectToAction("Courses");
            }

            return View();
        }

        [HttpGet]
        public IActionResult AddModule(int id)
        {
            ViewBag.CourseId = id;
            return View();
        }

        [HttpPost, ActionName("AddModule")]
        public IActionResult AddModulePost(ModuleViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                Module module = new Module
                {
                    ModuleName = model.ModuleName,
                    IsTest = model.IsTest,
                    IsLab = model.IsLab,
                    Date = model.Date,
                    CourseId = id
                };

                _repository.CreateModule(module);
                _repository.AddTest(_repository.GetLastModuleId());

                return RedirectToAction("Courses");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public RatingViewModel CreateRatingModel(RatingViewModel viewModel, IEnumerable<Student> students)
        {
            List<int> years = new List<int>();
            List<Course> courses = new List<Course>();

            foreach (Student student in students)
            {
                foreach (Listeners listener in student.Listeners)
                {
                    if (!years.Contains(listener.Course.Year))
                    {
                        years.Add(listener.Course.Year);
                    }

                    if (!courses.Contains(listener.Course))
                    {
                        courses.Add(listener.Course);
                    }
                }
            }

            return new RatingViewModel
            {
                Students = students,
                Years = years,
                Courses = courses
            };
        }
    }
}
