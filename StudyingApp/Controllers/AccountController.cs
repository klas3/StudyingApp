using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudyingApp.Models;
using StudyingApp.Repositories;
using StudyingApp.ViewModels;

namespace StudyingApp.Controllers
{
    public class AccountController : Controller
    {
        private const string unverifiedRoleName = "Unverified";


        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        private IRepository _repository;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IRepository repository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _repository = repository;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost, ActionName("Login")]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.Login, loginModel.Password, loginModel.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Failed to Login");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(_repository.IsLoginUnique(model.Login))
                {
                    if(_repository.IsEmailUnique(model.Email))
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

                        if(result.Succeeded)
                        {
                            Student student = new Student
                            {
                                UserId = user.Id,
                                University = model.University,
                                Faculty = model.Faculty,
                                UniversityCourse = int.Parse(model.Course),
                                Skills = model.Skills,
                                IsVerified = false
                            };

                            _repository.CreateStudent(student);

                            bool roleExists = await _roleManager.RoleExistsAsync(unverifiedRoleName);
                            if (!roleExists)
                            {
                                await _roleManager.CreateAsync(new IdentityRole(unverifiedRoleName));
                            }

                            await _userManager.AddToRoleAsync(user, unverifiedRoleName);

                            if (!string.IsNullOrWhiteSpace(user.Email))
                            {
                                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
                            }
                    
                            var resultSignIn = await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);
                            if (resultSignIn.Succeeded)
                            {
                                return RedirectToAction("Index", "Home");
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