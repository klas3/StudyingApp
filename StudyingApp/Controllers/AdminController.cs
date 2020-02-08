using Microsoft.AspNetCore.Mvc;
using StudyingApp.Models;
using StudyingApp.Repositories;
using System.Collections.Generic;
using StudyingApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudyingApp.Controllers
{
    public class AdminController : Controller
    {
        private const string studentRoleName = "Student";
        private const string teacherRoleName = "Teacher";

        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        private IRepository _repository;

        public AdminController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IRepository repository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _repository = repository;
        }

        public IActionResult Index()
        {
            IEnumerable<Student> students = _repository.GetStudentsList(false);
            return View(students);
        }

        public IActionResult VerifyStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VerifyStudentPost(int id)
        {
            var student = _repository.GetStudentById(id);

            if(student != null)
            {
                _repository.VerifyStudent(student);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult BlockStudentPost(int id)
        {
            var student = _repository.GetStudentById(id);

            if (student != null)
            {
                _repository.BlockStudent(student);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult EditStudent(int id)
        {
            Student student = _repository.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("EditStudent")]
        public async Task<IActionResult> EditStudentPost(int id)
        {
            var studentToUpdate = _repository.GetStudentById(id);
            bool isUpdatedStudent = await TryUpdateModelAsync<Student>(
                studentToUpdate,
                "",
                s => s.University,
                s => s.Faculty,
                s => s.UniversityCourse,
                s => s.Skills
                );
            if (isUpdatedStudent == true)
            {
                _repository.SaveChanges();
                return RedirectToAction("Students", "Home");
            }

            return View(studentToUpdate);
        }

        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost, ActionName("CreateStudent")]
        public async Task<IActionResult> CreateStudentPost(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_repository.IsEmailUnique(model.Email))
                {
                    if (_repository.IsLoginUnique(model.Login))
                    {
                        User user = new User
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            MiddleName = model.MiddleName,
                            Email = model.Email,
                            UserName = model.Login
                        };

                        var result = await _userManager.CreateAsync(user, model.Password);

                        if (result.Succeeded)
                        {
                            Student student = new Student
                            {
                                UserId = user.Id,
                                University = model.University,
                                Faculty = model.Faculty,
                                UniversityCourse = int.Parse(model.Course),
                                Skills = model.Skills,
                                IsVerified = true
                            };

                            _repository.CreateStudent(student);

                            bool roleExists = await _roleManager.RoleExistsAsync(studentRoleName);
                            if (!roleExists)
                            {
                                await _roleManager.CreateAsync(new IdentityRole(studentRoleName));
                            }

                            await _userManager.AddToRoleAsync(user, studentRoleName);

                            if (!string.IsNullOrWhiteSpace(user.Email))
                            {
                                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
                                return RedirectToAction("Index", "Admin");
                            }
                        }
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Введений Email вже зареєстрований іншим користувачем");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Введений логін вже використовується");
                }
            }
            return View();
        }


    }
}