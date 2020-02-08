using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyingApp.ViewModels
{
    public class ModuleViewModel
    {
        public string ModuleName { get; set; }

        public bool IsTest { get; set; }

        public bool IsLab { get; set; }

        public DateTime Date { get; set; }

    }
}
