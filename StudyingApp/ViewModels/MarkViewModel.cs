using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using StudyingApp.Models;

namespace StudyingApp.ViewModels
{
    public class MarkViewModel
    {
        [Display(Name = "Модуль")]
        [Required(ErrorMessage = "Оберіть модуль")]
        public virtual ICollection<Module> Modules { get; set; }
       

        [Display(Name = "Студент")]
        [Required(ErrorMessage = "Оберіть студента")]
        public virtual ICollection<Listeners> Listeners { get; set; }


        [Display(Name = "Оцінка за лабораторну")]
        [Range(0, 100)]
        [Required(ErrorMessage = "Внесіть оцінку в діапазоні від 0 до 100")]
        public int LabMark { get; set; }

        [Display(Name = "Оцінка за тест")]
        [Range(0, 25)]
        [Required(ErrorMessage = "Внесіть оцінку в діапазоні від 0 до 25")]
        public int TestMark { get; set; }
    }
}
