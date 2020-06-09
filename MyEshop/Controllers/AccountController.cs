using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyEshop.Controllers
{
    public class AccountController : Controller
    {
        EshopUnitOfWork db = new EshopUnitOfWork();

        // GET: Account
        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }

        [Route("Register")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                if (!db.UserRepository.IsDuplicatedUser(register))
                {
                    Users user = new Users()
                    {
                        RoleID = 1,
                        Email = register.Email.Trim().ToLower(),
                        UserName = register.UserName.Trim().ToLower(),
                        Password = FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password, "MD5"),
                        ActiveCode = Guid.NewGuid().ToString(),
                        IsActive = false,
                        RegisterDate = DateTime.Now
                    };

                    db.AccountRepository.Insert(user);
                    db.Save();

                    string body = PartialToStringClass.RenderPartialView("ManageEmail", "ActivationEmail", user);
                    SendEmail.Send(user.Email, "فعالسازی حساب کاربری", body);
                    return View("SuccessRegister", user);
                }
                else
                {
                    ModelState.AddModelError("Email", "ایمیل وارد شده تکراری است");
                    ModelState.AddModelError("UserName", "نام کاربری وارد شده تکراری است");
                }
            }

            return View(register);
        }


        public ActionResult ActivationUser(string id)
            {
            Users user= db.UserRepository.GetByActiveCode(id);
            if (user == null) {
                return HttpNotFound();
            }
            else {
                user.IsActive = true;
                user.ActiveCode = Guid.NewGuid().ToString();
                db.Save();
                ViewBag.UserName = user.UserName;
            return View();
            }
        }

        [Route("Login")]
        public ActionResult Login()
        {
            return View();

        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginViewModel login, string ReturnUrl = "/")
        {
            if (ModelState.IsValid) {
                var HashPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "MD5");
                Users user = db.UserRepository.GetExistUser(login.Email,HashPassword);
                if ( user!= null) {
                    if (user.IsActive) {
                        FormsAuthentication.SetAuthCookie(user.UserName,login.RememberMe);
                        return Redirect(ReturnUrl);
                    }
                    else { ModelState.AddModelError("Email", "حساب کاربری شما فعال نمی باشد");
                       
                    }
                }
                else {
                    ModelState.AddModelError("Email", "کاربری با این مشخصات یافت نشد");
                    ModelState.AddModelError("Password", "کاربری با این مشخصات یافت نشد");
                 
                }
            }
           
                return View(login);
            
        }


        public ActionResult LoggOff() {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        [Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [Route("ForgotPassword")]
        [HttpPost]
        public ActionResult ForgotPassword(ForgetPasswordViewModel forgetPassword)
        {
            if (ModelState.IsValid) {
                Users user = db.UserRepository.GetByEmail(forgetPassword.Email);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        string body = PartialToStringClass.RenderPartialView("ManageEmail", "RecoveryPassword", user);
                        SendEmail.Send(user.Email, "بازیابی کلمه عبور", body);
                        return View("SuccessForgotPassword", user);
                    }
                    else { ModelState.AddModelError("Email", "حساب کاربری شما فعال نمی باشد"); }
                }
                else
                { ModelState.AddModelError("Email","کاربری با این ایمیل یافت نشد"); }
               
            }
            
            return View(forgetPassword);
        }

       
        public ActionResult RecoveryPassword(string id)
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult RecoveryPassword(string id,RecoveryPasswordViewModel recoveryPassword) {
            if (ModelState.IsValid)
            {
                Users user = db.UserRepository.GetByActiveCode(id);
                if (user == null)
                {
                    HttpNotFound();
                }
                string hashePassword = FormsAuthentication.HashPasswordForStoringInConfigFile(recoveryPassword.Password, "MD5");
                user.Password = hashePassword;
                user.ActiveCode = Guid.NewGuid().ToString();
                db.Save();
                return Redirect("/Login?recovery=true");
            }
            return View();

        }
    }
}