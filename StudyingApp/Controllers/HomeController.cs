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

        [Authorize]
        public IActionResult Rating()
        {
            IEnumerable<Student> students = _repository.GetRatingStudents(2019, 2);
            return View(students);
        }

        [Authorize]
        public IActionResult Course(int id)
        {
            var course = _repository.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
