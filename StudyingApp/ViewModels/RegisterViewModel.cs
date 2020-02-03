using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudyingApp.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Прізвище")]
        [Required(ErrorMessage = "Введіть ваше прізвище")]
        public string LastName { get; set; }

        [Display(Name = "Ім'я")]
        [Required(ErrorMessage = "Введіть ваше ім'я")]
        public string FirstName { get; set; }

        [Display(Name = "По батькові")]
        [Required(ErrorMessage = "Введіть ваше по батькові")]
        public string MiddleName { get; set; }

        [Display(Name = "Ел. пошта")]
        [Required(ErrorMessage = "Введіть вашу електронну пошту")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Введіть ваш пароль")]
        public string Password { get; set; }

        [Display(Name = "Університет")]
        [Required(ErrorMessage = "Введіть назву вашого університету")]
        public string University{ get; set; }

        [Display(Name = "Факультет")]
        [Required(ErrorMessage = "Введіть назву вашого факультету")]
        public string Faculty { get; set; }

        [Display(Name = "Курс")]
        [Required(ErrorMessage = "Оберіть свій курс")]
        public string Course { get; set; }

        [Display(Name = "Вміння в галузі ІТ")]
        [Required(ErrorMessage = "Поділіться своїми вміннями")]
        public string Skills { get; set; }

    }
}
