using DataLayer;
using MoreLinq;
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

        public ActionResult LastProduct()
        {

            return PartialView(db.ProductRepository.GetAll().OrderByDescending(g => g.CreateDate).Take(6));
        }

        public ActionResult BestSellingproducts() {

            return PartialView(db.ProductRepository.GetAll().OrderByDescending(p=> p.Sold).Take(6));
        }

        [Route("ShowProductDetail/{id}")]
        public ActionResult ShowProductDetail(int id) {
            Products products = db.ProductRepository.GetById(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.features = products.Product_Features.DistinctBy(f => f.FeatureID).Select(f => new ShowProductFeatureViewModel() {
                FeatureTitle = f.Features.FeatureTitle,
                Values = db.ProductFeaturesRepository.GetAll(pf => pf.FeatureID == f.FeatureID && pf.ProductID==f.ProductID).Select(pf=> pf.Value).ToList()
            }).ToList();

           
            return View(products);
        }
    }

}