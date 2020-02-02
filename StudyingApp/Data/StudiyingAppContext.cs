using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudyingApp.Models;



namespace StudyingApp.Data
{
    public class StudiyingAppContext : DbContext
    {
        public StudiyingAppContext(DbContextOptions<StudiyingAppContext> options) : base(options)
        {
        }

        public DbSet<User> Users {get; set;}
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Listeners> Listeners { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FirstName = "TestU",
                    LastName = "UTest",
                    Login = "userlogin",
                    Password = "password",
                    UserRole = Role.Student
                }) ;
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    StudentId = 1,
                    University = "none",
                    Faculty = "none",
                    UniversityCourse = 1,
                    Skills = "low",
                    IsVerified = true,
                    UserId = 1,
                }) ;
        }
    }    
}
        



