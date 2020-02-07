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
            return _context.Students.Include(s => s.User).ToList();
        }

        public IEnumerable<Student> GetUnverifiedStudents()
        {
            return _context.Students.Where(s => s.IsVerified == false).Include(u => u.User).ToList();
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
            _context.Students.Update(student);
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
            if (_context.DBUsers.SingleOrDefault(user => user.UserName == login) == null)
            {
                return true;
            }

            return false;
        }

        public bool IsEmailUnique(string email)
        {
            if (_context.DBUsers.SingleOrDefault(user => user.Email == email) == null)
            {
                return true;
            }

            return false;
        }

        public IEnumerable<Course> GetCoursesList()
        {
            return _context.Courses.Include(m => m.Modules).ToList();
        }

        public Course GetCourseById(int id)
        {
            return _context.Courses.Include(u => u.User).Include(m => m.Modules).ThenInclude(t => t.Tasks).SingleOrDefault(c => c.CourseId == id);
        }

        public IEnumerable<Module> GetScheduleModulesList()
        {
            return _context.Modules.Where(module => DateTime.Compare(module.Date, DateTime.Now) > 0).Include(module => module.Course).ToList();
        }

        public IEnumerable<Student> GetRatingStudents(int year, string courseId)
        {
            if(courseId == null)
            {
                return _context.Students.Include(s => s.User).Include(l => l.Listeners).ThenInclude(c => c.Course).ToList();
            }
            else
            {
                return _context.Students.Where(student => student.IsVerified == true)
                        .Include(s => s.User).Include(l => l.Listeners).ThenInclude(c => c.Course)
                        .ThenInclude(y => y.Year == year)
                        .ToList();
            }
        }
    }
}
