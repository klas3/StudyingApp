using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudyingApp.ViewModels
{
    public class ModuleViewModel
    {
        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Введіть дату кінця")]
        public string ModuleName { get; set; }

        [Display(Name = "Тест")]
        public bool IsTest { get; set; }

        [Display(Name = "Лабораторна")]
        public bool IsLab { get; set; }

        [Display(Name = "Дата")]
        [Required(ErrorMessage = "Введіть дату")]
        public DateTime Date { get; set; }

        public int CourseId { get; set; }
    }
}
