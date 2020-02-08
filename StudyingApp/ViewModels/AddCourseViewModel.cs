using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudyingApp.ViewModels
{
    public class AddCourseViewModel
    {
        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Введіть назву курсу")]
        public string Name { get; set; }

        [Display(Name = "Рік початку")]
        [Required(ErrorMessage = "Введіть рік початку")]
        public int Year { get; set; }

        [Display(Name = "Дата початку")]
        [Required(ErrorMessage = "Введіть дату початку")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Дата кінця")]
        [Required(ErrorMessage = "Введіть дату кінця")]
        public DateTime EndDate { get; set; }
    }
}
