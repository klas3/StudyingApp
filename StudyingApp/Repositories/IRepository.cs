﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudyingApp.Models;

namespace StudyingApp.Repositories
{
    public interface IRepository
    {
        IEnumerable<Student> GetStudentsList(bool isVerified);
        Student GetStudentById(int id);
        void CreateStudent(Student student);
        void VerifyStudent(Student student);
        void BlockStudent(Student student);
        void DeleteStudentById(int id);
        bool IsLoginUnique(string login);
        IEnumerable<Course> GetCoursesList();
        bool IsEmailUnique(string email);
        Course GetCourseById(int id);
        void CreateModule(Module module);
        void CreateUser(User user);
        void SaveChanges();
        IEnumerable<Module> GetScheduleModulesList();
        IEnumerable<Student> GetRatingStudents(int? year, int? courseId, int? studentId);
        void CreateCourse(Course course);
        void CreateMark(Mark mark);
        void CreateListenersForCourse(int id);
        int GetLastCourseId();
        int GetLastModuleId();
        void AddTest(int moduleId);
    }
}
