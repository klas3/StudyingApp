using StudyingApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudyingApp.ViewModels
{
    public class MarkViewModel
    {
        [Display(Name = "Студент")]
        [Required(ErrorMessage = "Оберіть студента")]
        public virtual Listeners Listeners { get; set; }


        [Display(Name = "Оцінка")]
        public int Mark { get; set; }

    }
}
