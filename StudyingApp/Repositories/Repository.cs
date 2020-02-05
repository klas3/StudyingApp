using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudyingApp.Data;
using StudyingApp.Models;

namespace StudyingApp.Repositories
{
    public class Repository : IRepository
    {
        private StudiyingAppContext _context;

        public Repository(StudiyingAppContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> GetStudentsList()
        {
            return _context.Students.ToList();
        }

        public Student GetStudentById(int id)
        {
            return _context.Students.SingleOrDefault(s => s.StudentId == id);
        }

        public void CreateStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void VerifyStudent(Student student)
        {
            student.IsVerified = true;
            _context.SaveChanges();
        }

        public void DeleteStudentById(int id)
        {
            var student = _context.Students.SingleOrDefault(s => s.StudentId == id);
            _context.Students.Remove(student);
            _context.SaveChanges();
        }

        public bool IsLoginUnique(string login)
        {
            if(_context.DBUsers.SingleOrDefault(user => user.UserName == login) == null)
            {
                return true;
            }

            return false;
        }
    }
}
