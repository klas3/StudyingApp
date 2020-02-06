using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudyingApp.Models;

namespace StudyingApp.Repositories
{
    public interface IRepository
    {
        IEnumerable<Student> GetStudentsList();
        Student GetStudentById(int id);
        void CreateStudent(Student student);
        void VerifyStudent(Student student);
        void DeleteStudentById(int id);
        bool IsLoginUnique(string login);
        IEnumerable<Course> GetCoursesList();
        bool IsEmailUnique(string email);
        Course GetCourseById(int id);

    }
}
