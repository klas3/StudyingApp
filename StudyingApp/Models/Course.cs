using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyingApp.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public int TeacherId { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
