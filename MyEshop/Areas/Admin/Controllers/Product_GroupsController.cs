using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace MyEshop.Areas.Admin.Controllers
{
    public class Product_GroupsController : Controller
    {
        EshopUnitOfWork db = new EshopUnitOfWork();
        // GET: Admin/Product_Groups
        public ActionResult Index()
        {
           //var product_Groups = db.Product_Groups.Include(p => p.Product_Groups2);
            return View();
        }

        public ActionResult ListGroups() {
            return PartialView(db.ProductGroupRepository.GetAll(p => p.ParenID == null));
        }
       
        // GET: Admin/Product_Groups/Create
        public ActionResult Create(int? id)
        {

            return PartialView(new Product_Groups()
            {
                ParenID = id
            }); ;
        }

        // POST: Admin/Product_Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,GroupTitle,ParenID")] Product_Groups product_Groups)
        {
            if (ModelState.IsValid)
            {
                db.ProductGroupRepository.Insert(product_Groups);
                db.Save();
                return PartialView("ListGroups", db.ProductGroupRepository.GetAll(p => p.ParenID == null));
            }

            ViewBag.ParenID = new SelectList(db.ProductGroupRepository.GetAll(), "GroupID", "GroupTitle", product_Groups.ParenID);
            return View(product_Groups);
        }

        // GET: Admin/Product_Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Groups product_Groups = db.ProductGroupRepository.GetById(id);
            if (product_Groups == null)
            {
                return HttpNotFound();
            }
            return PartialView(product_Groups);
        }

        // POST: Admin/Product_Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,GroupTitle,ParenID")] Product_Groups product_Groups)
        {
            if (ModelState.IsValid)
            {
                db.ProductGroupRepository.Update(product_Groups);
                db.Save();
                return PartialView("ListGroups", db.ProductGroupRepository.GetAll(p => p.ParenID == null));
            }
            ViewBag.ParenID = new SelectList(db.ProductGroupRepository.GetAll(), "GroupID", "GroupTitle", product_Groups.ParenID);
            return View(product_Groups);
        }



        // GET: Admin/Product_Groups/Delete/5
        public ActionResult Delete(int id)
        {
            
            Product_Groups product_Groups = db.ProductGroupRepository.GetById(id);
            if (product_Groups == null)
            {
                return HttpNotFound();
            }
            if (product_Groups.Product_Groups1.Any()) {
                foreach (var item in db.ProductGroupRepository.GetAll(p => p.ParenID == id)) {
                    db.ProductGroupRepository.Delete(item);
                }
            }
            db.ProductGroupRepository.Delete(product_Groups);
            db.Save();
            return RedirectToAction("Index");
        }

        // POST: Admin/Product_Groups/Delete/5
        
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
