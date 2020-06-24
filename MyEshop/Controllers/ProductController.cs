using DataLayer;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
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


        public ActionResult ShowComments(int id) {
            return PartialView(db.CommentRepository.GetAll(c=> c.ProductID==id));
        }

        

        public ActionResult CreateComments(int id) {

            return PartialView(new Product_Comment() {
            ProductID=id
            });

        }




        [HttpPost]
        public ActionResult CreateComment(Product_Comment productComment)
        {
            if (ModelState.IsValid)
            {
                productComment.CreateDate = DateTime.Now;
                db.CommentRepository.Insert(productComment);
                db.Save();

                return PartialView("ShowComments", db.CommentRepository.GetAll(c => c.ProductID == productComment.ProductID));

            }
            return RedirectToAction("Index");
        }

    }

}


