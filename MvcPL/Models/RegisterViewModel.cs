using System;
using System.ComponentModel.DataAnnotations;


namespace MvcPL.Models
{
   
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            RoleId = 3;
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Введите e-mail")]
        [Required(ErrorMessage = "Поле не может быть пустым!")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный email")]
        public string Email { get; set; }

        [Display(Name = "Введите логин")]
        [Required(ErrorMessage = "Поле не может быть пустым!")]
        //[RegularExpression("[A-Za-z]", ErrorMessage = "Некорректный логин")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(100, ErrorMessage = "Пароль должен содержать хотя бы {2} символа", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Введите пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Повторите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли должны создавать")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Введите цифры с изображения")]
        public string Captcha { get; set; }

        [DataType(DataType.Date)]
        public DateTime AddedDate { get; set; }
        public string AvatarPath { get; set; }

        public int RoleId { get; set; }
        
    }
}