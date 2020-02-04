using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudyingApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace StudyingApp.Data
{
    public class StudiyingAppContext : IdentityDbContext<User>
    {
        public StudiyingAppContext(DbContextOptions<StudiyingAppContext> options) : base(options)
        {
        }

        public DbSet<User> DBUsers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Listeners> Listeners { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(user => user.Course)
                .WithOne(course => course.User)
                .HasForeignKey<Course>(course => course.UserId);

            modelBuilder.Entity<User>()
                .HasOne(user => user.Student)
                .WithOne(student => student.User)
                .HasForeignKey<Student>(student => student.UserId);
        }
    }    
}
        



