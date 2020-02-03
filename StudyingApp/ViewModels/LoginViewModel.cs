using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudyingApp.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Ел. пошта")]
        [Required(ErrorMessage = "Введіть вашу електронну пошту")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Введіть ваш пароль")]
        public string Password { get; set; }
    }
}
