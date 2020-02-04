using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyingApp.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public string University { get; set; }

        public string Faculty { get; set; }

        public int UniversityCourse { get; set; }

        public string Skills { get; set; }

        public bool IsVerified { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Listeners> Listeners { get; set; }
    }
}
