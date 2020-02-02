using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyingApp.Models
{
    public class Mark
    {
        public int MarkId { get; set; }

        public int MarkValue { get; set; }

        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
    }
}
