using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DataLayer
{
    public class RegisterViewModel {

        [Display(Name ="نام کاربری")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name ="ایمیل")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage ="ایمیل معتبر نمی باشد")]
        public string Email { get; set; }

        [Display(Name ="رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="تکرار رمز عبور صحیح نمی باشد")]
        public string RePassword { get; set; }
    }


    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage ="ایمیل معتبر نمی باشد")]
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }

        [Display(Name ="مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }


    public class ForgetPasswordViewModel
    {

        [EmailAddress(ErrorMessage = "ایمیل معتبر نمی باشد")]
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Email { get; set; }

    }

    public class RecoveryPasswordViewModel {


        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "تکرار رمز عبور صحیح نمی باشد")]
        public string RePassword { get; set; }
    }

    public class ChangePasswordViewModel {

         [Display(Name = "رمز عبور فعلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "تکرار رمز عبور صحیح نمی باشد")]
        public string RePassword { get; set; }
    }

}
