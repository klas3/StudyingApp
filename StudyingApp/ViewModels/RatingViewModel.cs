using StudyingApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace StudyingApp.ViewModels
{
    public class RatingViewModel
    {
        public ICollection<int> Years { get; set; }
        public ICollection<Course> Courses { get; set; }
        public IEnumerable<Student> Students { get; set; }

        public int Year { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
    }
}
