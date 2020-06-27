using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEshop.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index(string q)
        {
            EshopUnitOfWork db = new EshopUnitOfWork();
            List<Products> list = new List<Products>();
            list.AddRange(db.TagsRepository.GetAll(t=> t.Tag==q).Select(p=> p.Products).ToList());
            list.AddRange(db.ProductRepository.GetAll(p => p.ProductTitle.Contains(q) || p.ShortDescription.Contains(q) || p.Text.Contains(q)).ToList());
            ViewBag.search = q;
            return View(list.Distinct());
        }
    }
}