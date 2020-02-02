using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;


namespace StudyingApp.Models
{
    public class Module
    {
        public int ModuleId { get; set; }

        public string ModuleName { get; set; }

        public bool IsTest { get; set; }

        public bool IsLab { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
