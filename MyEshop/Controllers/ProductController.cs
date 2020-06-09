using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEshop.Controllers
{
    public class ProductController : Controller
    {
        EshopUnitOfWork db = new EshopUnitOfWork();
        // GET: Product
        public ActionResult ShowProductGroups()
        {
            return PartialView(db.ProductGroupRepository.GetAll());
        }
    }
}