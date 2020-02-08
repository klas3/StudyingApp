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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IRepository _repository;

        public HomeController(ILogger<HomeController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
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

        [Authorize]
        public IActionResult Students()
        {
            IEnumerable<Student> students = _repository.GetStudentsList(true);
            return View(students);
        }

        [Authorize]
        public IActionResult Courses()
        {
            IEnumerable<Course> courses = _repository.GetCoursesList();
            return View(courses);
        }

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

        [Authorize]
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
                viewModel.Year = DateTime.Now.Year;
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

        //[HttpGet]
        //public IActionResult AddMark()
        //{
        //    return View();
        //}

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
