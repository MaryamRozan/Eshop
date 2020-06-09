using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataLayer;

namespace MyEshop.Areas.Admin.Controllers
{
 
    public class UsersController : Controller
    {
        private EshopUnitOfWork db = new EshopUnitOfWork();

        // GET: Admin/Users
        public ActionResult Index()
        {
            // var users = db.Users.Include(u => u.Roles);
            return View(db.AccountRepository.GetAll());
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.AccountRepository.GetById(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.RolesRepository.GetAll(), "RolID", "RoleTitle");
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,RoleID,UserName,Email,Password,IsActive")] Users users)
        {
            if (ModelState.IsValid)
            {
                users.ActiveCode = Guid.NewGuid().ToString();
                users.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(users.Password,"MD5");
                users.RegisterDate = DateTime.Now;
                db.AccountRepository.Insert(users);
                db.Save();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(db.RolesRepository.GetAll(), "RolID", "RoleTitle", users.RoleID);
            return View(users);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.AccountRepository.GetById(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.RolesRepository.GetAll(), "RolID", "RoleTitle", users.RoleID);
            return View(users);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,RoleID,UserName,Email,Password,ActiveCode,IsActive,RegisterDate")] Users users)
        {
            if (ModelState.IsValid)
            {
                users.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(users.Password, "MD5");

                db.AccountRepository.Update(users);
                db.Save();
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(db.RolesRepository.GetAll(), "RolID", "RoleTitle", users.RoleID);
            return View(users);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.AccountRepository.GetById(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.AccountRepository.GetById(id);
            db.AccountRepository.Delete(users);
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
