using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyEshop.Areas.UserPanel.Controllers
{
   
    public class AccountController : Controller
    {
        EshopUnitOfWork db = new EshopUnitOfWork();
        // GET: UserPanel/Account
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
       
        public ActionResult ChangePassword(ChangePasswordViewModel changePassword)
        {
            if (ModelState.IsValid) {
                Users user = db.UserRepository.GetByUsername(User.Identity.Name);
                string oldPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(changePassword.OldPassword, "MD5");
                
                if (user.Password==oldPassword) {
                    user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(changePassword.Password, "MD5");
                    db.Save();
                    ViewBag.Success = true;
                } 
                else { ModelState.AddModelError("OldPassword", "رمز عبور فعلی صحیح نمی باشد");
                    
                }
            }
            return View();
        }
    }
}