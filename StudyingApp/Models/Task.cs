﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyingApp.Models
{
    public class Task
    {
        public int TaskId { get; set; }

        public int ModuleId { get; set; }

        public TaskTypes Type { get; set; }

        public string Descrition { get; set; }
    }
}
