﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using StudyingApp.Models;
using StudyingApp.Repositories;
using StudyingApp.ViewModels;


namespace StudyingApp.Controllers
{
    public class AdminController : Controller
    {
        private IRepository _repository;

        public AdminController(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            IEnumerable<Student> students = _repository.GetStudentsList(false);
            return View(students);
        }

        public IActionResult VerifyStudentFail()
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
    }
}