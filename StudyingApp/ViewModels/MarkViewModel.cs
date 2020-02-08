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
        public virtual int StudentId { get; set; }


        [Display(Name = "Оцінка")]
        [Required(ErrorMessage = "Виставіть оцінку")]
        public int Mark { get; set; }

    }
}
