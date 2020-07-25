using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEshop.Controllers
{
    public class HomeController : Controller
    {
        EshopUnitOfWork db = new EshopUnitOfWork();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Slider()
        {
            DateTime dt = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
            return PartialView(db.SliderRepository.GetAll(s=>s.IsActive&& s.StartDate<=dt&& s.EndDate>=dt));
        }

        public ActionResult AboutUs()
        {
            return View();
            //123
                }
    }
}