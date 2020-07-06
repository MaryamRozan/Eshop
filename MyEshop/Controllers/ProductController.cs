using DataLayer;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;
using System.Web.UI;

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
        public ActionResult CreateComments(Product_Comment productComment)
        {
            if (ModelState.IsValid)
            {
                productComment.CreateDate = DateTime.Now;
                db.CommentRepository.Insert(productComment);
                db.Save();

                return PartialView("ShowComments", db.CommentRepository.GetAll(c => c.ProductID == productComment.ProductID));

            }
            return PartialView(productComment);
        }

        [Route("Archive")]
        public ActionResult ShowArchive(int pageId = 1, string title = "", int minPrice = 0, int maxPrice = 0, List<int> selectedGroups = null)
        {
            ViewBag.Groups = db.ProductGroupRepository.GetAll().ToList();
            ViewBag.productTitle = title;
            ViewBag.minPrice = minPrice;
            ViewBag.maxPrice = maxPrice;
            ViewBag.pageId = pageId;
            ViewBag.selectGroup = selectedGroups;
            List<Products> list = new List<Products>();
            if (selectedGroups != null && selectedGroups.Any())
            {
                foreach (int group in selectedGroups)
                {
                    list.AddRange(db.SelectedGroupRepository.GetAll(g => g.GroupID == group).Select(g => g.Products).ToList());
                }
                list = list.Distinct().ToList();
            }
            else
            {
                list.AddRange(db.SelectedGroupRepository.GetAll().Select(p=>p.Products).ToList());
                list = list.Distinct().ToList();
            }


            if (title != "")
            {
                list = list.Where(p => p.ProductTitle.Contains(title)).ToList();
            }
            if (minPrice > 0)
            {
                list = list.Where(p => p.Price >= minPrice).ToList();
            }
            if (maxPrice > 0)
            {
                list = list.Where(p => p.Price <= maxPrice).ToList();
            }

            //Pagging
            int take = 9;
            int skip = (pageId - 1) * 1;
            ViewBag.pageCount = list.Count()/take;
            return View(list.OrderByDescending(p=>p.CreateDate).Skip(skip).Take(take).ToList());
        }
    }

}


