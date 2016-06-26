using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class EditInfoViewModel
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(100, ErrorMessage = "Пароль должен содержать хотя бы {2} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Введите пароль")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Введите новый пароль")]
        [StringLength(100, ErrorMessage = "Пароль должен содержать хотя бы {2} сомволов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Введите новый пароль")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Повторите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        //[Required(ErrorMessage = "Enter your password")]
        //[StringLength(100, ErrorMessage = "The password must contain at least {2} characters", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Enter your password")]
        //public string NewPassword { get; set; }

        //[Required(ErrorMessage = "Confirm the password")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm the password")]
        //[Compare("NewPassword", ErrorMessage = "Passwords must match")]
        //public string ConfirmPassword { get; set; }
    }
}