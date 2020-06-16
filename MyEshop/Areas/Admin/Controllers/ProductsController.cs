using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using DataLayer;
using InsertShowImage;
using KooyWebApp_MVC.Classes;

namespace MyEshop.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private EshopUnitOfWork db = new EshopUnitOfWork();

        // GET: Admin/Products
        public ActionResult Index()
        {
            return View(db.ProductRepository.GetAll());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.ProductRepository.GetById(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.Groups = db.ProductGroupRepository.GetAll();
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductTitle,ShortDescription,Text,Price,ImageName")] Products products, List<int> SelectedGroups, HttpPostedFileBase imageProduct, string tags)
        {
            if (ModelState.IsValid)
            {
                if (SelectedGroups == null)
                {
                    ViewBag.NoSelectedgroup = "true";
                    ViewBag.Groups = db.ProductGroupRepository.GetAll();
                    return View(products);
                }
                products.ImageName = "no-image.png";
                if (imageProduct != null && imageProduct.IsImage())
                {
                    products.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imageProduct.FileName);
                    imageProduct.SaveAs(Server.MapPath("/Images/ProductImages/" + products.ImageName));
                    ImageResizer image = new ImageResizer();
                    image.Resize(Server.MapPath("/Images/ProductImages/" + products.ImageName), Server.MapPath("/Images/ProductImages/Thumb/" + products.ImageName));
                }
                products.CreateDate = DateTime.Now;
                db.ProductRepository.Insert(products);
                foreach (int selectedGroup in SelectedGroups)
                {
                    db.SelectedGroupRepository.Insert(new SelectedProductGroup()
                    {
                        ProductID = products.ProductID,
                        GroupID = selectedGroup
                    });
                }
                if (!string.IsNullOrEmpty(tags))
                {
                    string[] Tags = tags.Split('،');
                    foreach (string tag in Tags)
                    {
                        db.TagsRepository.Insert(new Tags()
                        {
                            ProductID = products.ProductID,
                            Tag = tag.Trim()
                        });
                    }
                }
                db.Save();

                return RedirectToAction("Index");
            }
            ViewBag.Groups = db.ProductGroupRepository.GetAll();
            return View(products);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.ProductRepository.GetById(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.Groups = db.ProductGroupRepository.GetAll();
            ViewBag.SelectedGroups = products.SelectedProductGroup.ToList();
            ViewBag.Tags = string.Join("،", products.Tags.Select(t => t.Tag));
            return View(products);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductTitle,ShortDescription,Text,Price,ImageName,CreateDate")] Products products, List<int> SelectedGroups, HttpPostedFileBase imageProduct, string tags)
        {
            if (ModelState.IsValid)
            {
                if (imageProduct != null && imageProduct.IsImage()) {
                    if (products.ImageName != "no-image.png") {
                        System.IO.File.Delete(Server.MapPath("~/Images/ProductImages/" + products.ImageName));
                        System.IO.File.Delete(Server.MapPath("~/Images/ProductImages/Thumb/"+products.ImageName));
                    }
                    products.ImageName = Guid.NewGuid().ToString()+Path.GetExtension(imageProduct.FileName);
                  
                    imageProduct.SaveAs(Server.MapPath("/Images/ProductImages/" + products.ImageName));
                    ImageResizer image = new ImageResizer();
                    image.Resize(Server.MapPath("/Images/ProductImages/" + products.ImageName), Server.MapPath("/Images/ProductImages/Thumb/" + products.ImageName));
                }
                db.ProductRepository.Update(products);

               List<Tags> tag= db.TagsRepository.GetAll(t=> t.ProductID==products.ProductID).ToList();
                foreach(var t in tag) { db.TagsRepository.Delete(t); }
                if (!string.IsNullOrEmpty(tags)) {
                    string[] Tags = tags.Split('،');
                    foreach (var T in Tags) {
                        db.TagsRepository.Insert(new Tags()
                        {
                            ProductID = products.ProductID,
                            Tag = T.Trim()
                        }) ;
                    }
                }

                List<SelectedProductGroup> selectedGroup = db.SelectedGroupRepository.GetAll(s=> s.ProductID==products.ProductID).ToList();
                foreach (var g in selectedGroup) {
                    db.SelectedGroupRepository.Delete(g);
                }

                foreach(var s in SelectedGroups)
                {
                    db.SelectedGroupRepository.Insert(new SelectedProductGroup() {
                    ProductID=products.ProductID,
                    GroupID=s
                    });
                }

                db.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Groups = db.ProductGroupRepository.GetAll();
            ViewBag.SelectedGroups = products.SelectedProductGroup.ToList();
            ViewBag.Tags = string.Join("،", products.Tags.Select(t => t.Tag));
            return View(products);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.ProductRepository.GetById(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.ProductRepository.GetById(id);
            db.ProductRepository.Delete(products);
            db.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Gallery(int id) {
            ViewBag.gallery = db.GalleryRepository.GetAll(g => g.ProductID == id);
            return View(new Product_Galleries()
            {
                ProductID = id
            }) ;
        }

        [HttpPost]
        public ActionResult Gallery(Product_Galleries gallery,HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {
                if(imgUp!=null&& imgUp.IsImage())
                {
                    gallery.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imgUp.FileName);
                    imgUp.SaveAs(Server.MapPath("/Images/ProductImages/" + gallery.ImageName));
                    ImageResizer resizer = new ImageResizer();
                    resizer.Resize(Server.MapPath("/Images/ProductImages/" + gallery.ImageName),Server.MapPath( "/Images/ProductImages/Thumb/" + gallery.ImageName));

                }
                db.GalleryRepository.Insert(gallery);
                db.Save();

            }

            return RedirectToAction("Gallery", new { id = gallery.ProductID });
        }

        public ActionResult DeleteGallery(int id) {

           var gallery= db.GalleryRepository.GetById(id);
            System.IO.File.Delete(Server.MapPath("~/Images/ProductImages/" + gallery.ImageName));
            System.IO.File.Delete(Server.MapPath("~/Images/ProductImages/Thumb/" + gallery.ImageName));
            db.GalleryRepository.Delete(gallery);
            db.Save();
            return RedirectToAction("Gallery",new {id=gallery.ProductID});
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
