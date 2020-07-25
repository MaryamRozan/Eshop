using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using KooyWebApp_MVC.Classes;

namespace MyEshop.Areas.Admin.Controllers
{
    public class SlidersController : Controller
    {
         EshopUnitOfWork db = new EshopUnitOfWork();

        // GET: Admin/Sliders
        public ActionResult Index()
        {
            return View(db.SliderRepository.GetAll());
        }

        // GET: Admin/Sliders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.SliderRepository.GetById(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Admin/Sliders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Sliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SlideID,Title,Url,Imagename,StartDate,EndDate,IsActive")] Slider slider,HttpPostedFileBase ImgUp)
        {

            if (ModelState.IsValid)
            {
                if(ImgUp==null || !ImgUp.IsImage()) {
                    ModelState.AddModelError("Imagename", "لطفا تصویر را با فرمت اعلامی آپلود کنید");
                    return View(slider);
                }
                slider.Imagename = Guid.NewGuid() + Path.GetExtension(ImgUp.FileName);
               ImgUp.SaveAs(Server.MapPath("/Images/Sliders/" + slider.Imagename));
                db.SliderRepository.Insert(slider);
                db.Save();
                return RedirectToAction("Index");
            }

            return View(slider);
        }

        // GET: Admin/Sliders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.SliderRepository.GetById(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Admin/Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SlideID,Title,Url,Imagename,StartDate,EndDate,IsActive")] Slider slider,HttpPostedFileBase ImgUp)
        {
            if (ModelState.IsValid)
            {
                if (ImgUp != null && ImgUp.IsImage()) {
                    System.IO.File.Delete(Server.MapPath("/Images/Sliders/" + slider.Imagename));
                    slider.Imagename = Guid.NewGuid() + Path.GetExtension(ImgUp.FileName);
                    ImgUp.SaveAs(Server.MapPath("/Images/Sliders/" + slider.Imagename));
                }
                db.SliderRepository.Update(slider);
                db.Save();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: Admin/Sliders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.SliderRepository.GetById(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Admin/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = db.SliderRepository.GetById(id);
            System.IO.File.Delete(Server.MapPath("/Images/Sliders/"+slider.Imagename));
            db.SliderRepository.Delete(slider);
            db.Save();
            return RedirectToAction("Index");
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
