using System.ComponentModel.DataAnnotations;

namespace Shop.Api.ViewModels.Users;

public class ChangePasswordViewModel
{
    [Display(Name = "کلمه عبور فعلی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MinLength(5, ErrorMessage = "کلمه عبور فعلی باید بیشتر از 5 کارکتر باشد")]

    public string CurrentPassword { get; set; }

    [Display(Name = "کلمه عبور جدید")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MinLength(5, ErrorMessage = "کلمه عبور جدید باید بیشتر از 5 کارکتر باشد")]
    public string Password { get; set; }

    [Required]
    [Display(Name = "تکرار کلمه عبور")]
    [Compare(nameof(Password),ErrorMessage = "کلمه های عبور یکسان نیستند")]
    public string ConfirmPassword { get; set; }
}