using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.ViewModels;

namespace MyEshop.Controllers
{
    public class CompareController : Controller
    {
        EshopUnitOfWork db = new EshopUnitOfWork();
        // GET: Compare
        public ActionResult Index()
        {
            //List<CompareListViewModel> compareList = new List<CompareListViewModel>();

            //if (Session["Compare"] != null) {
            //    List<CompareItem> list = new List<CompareItem>();
            //    list =(List<CompareItem>)Session["Compare"];
            //    foreach (var item in list)
            //    {
            //        string[] product = db.ProductFeaturesRepository.GetAll(p => p.ProductID == item.ProductID).Select(p =>p.Features.FeatureTitle).ToArray();
            //       var p_Features = db.ProductFeaturesRepository.GetAll(p => p.ProductID == item.ProductID).Select(p => p.Value).ToArray();
            //        compareList.Add(new CompareListViewModel()
            //        {
            //            ProductID = item.ProductID,
            //            ImageName = item.ImageName,
            //            Title = item.Title,
            //            Features = db.ProductFeaturesRepository.GetAll(p => p.ProductID == item.ProductID).Select(p => p.Features.FeatureTitle).ToArray(),
            //            ProductFeatures = p_Features
            //        });

            //    }
            //}
            List<CompareItem> list = new List<CompareItem>();

            List<Features> features = new List<Features>();
            List<Product_Features> product_Features = new List<Product_Features>();
            if (Session["Compare"] != null)
            { list = (List<CompareItem>)Session["Compare"]; }
            foreach(var item in list) { 
            features.AddRange(db.ProductFeaturesRepository.GetAll(p => p.ProductID == item.ProductID).Select(p => p.Features).ToList());
            product_Features.AddRange(db.ProductFeaturesRepository.GetAll(p=>p.ProductID==item.ProductID).ToList());
            }
            ViewBag.features = features.Distinct().ToList();
            ViewBag.product_Features = product_Features;
            return View(list);
        }

 

        public ActionResult AddToCompare(int id) {
            List<CompareItem> list = new List<CompareItem>();
            if (Session["Compare"] != null) {
                list =Session["Compare"] as List<CompareItem>;
            }
            if (!list.Any(p => p.ProductID == id))
            {
                var product = db.ProductRepository.GetAll(p => p.ProductID == id).Select(p => new { p.ProductTitle, p.ImageName }).Single();
                list.Add(new CompareItem() {
                ProductID=id,
                Title= product.ProductTitle,
                ImageName= product.ImageName
                });
            }

            Session["Compare"] = list;
            return PartialView("CompareList",list);
        }

        public ActionResult CompareList() {
            List<CompareItem> list = new List<CompareItem>();
            if (Session["Compare"] != null) {
            list=(List<CompareItem>)Session["Compare"];
            }
            return PartialView(list);
        }

        public ActionResult DeleteFromCompare(int id) {
            List<CompareItem> list = new List<CompareItem>();
            if (Session["Compare"] != null) {
            list=(List<CompareItem>)Session["Compare"];
                int index = list.FindIndex(l => l.ProductID == id);
                list.RemoveAt(index);
                Session["Compare"] = list;
            }

            return PartialView("CompareList", list);
        }
    }
}