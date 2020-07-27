using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.ViewModels;

namespace MyEshop.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            EshopUnitOfWork db = new EshopUnitOfWork();
           var visit= db.VisitSiteRepository.GetAll();
            DateTime dtNow = DateTime.Now.Date;
            DateTime dtYesterday = DateTime.Now.Date.AddDays(-1);
            VisitSiteViewModel visitsite = new VisitSiteViewModel() {
                TodayVisit = visit.Count(v=>v.Date==dtNow),
                YesterdayVisit=visit.Count(v=>v.Date==dtYesterday),
                SumVisit=visit.Count(),
                OnlineUser=int.Parse(HttpContext.Application["Online"].ToString())
            };

            return View(visitsite);
        }
    }
}