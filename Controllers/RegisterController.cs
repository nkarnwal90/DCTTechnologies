using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DCTTECHNOLOGIES.Model;

namespace DCTTECHNOLOGIES.Controllers
{
    public class RegisterController : Controller
    {
        private DCTDBEntities DCTDB = new DCTDBEntities();

        // GET: Register
        public ActionResult Index()
        {
            return View(DCTDB.RegisterTbls.ToList());
        }

        // GET: Register/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterTbl registerTbl = DCTDB.RegisterTbls.Find(id);
            if (registerTbl == null)
            {
                return HttpNotFound();
            }
            return View(registerTbl);
        }

        // GET: Register/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Register/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Password,Gender,PhoneNumber,Email,Address")] RegisterTbl registerTbl)
        {
            if (ModelState.IsValid)
            {
                DCTDB.RegisterTbls.Add(registerTbl);
                DCTDB.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(registerTbl);
        }

        // GET: Register/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterTbl registerTbl = DCTDB.RegisterTbls.Find(id);
            if (registerTbl == null)
            {
                return HttpNotFound();
            }
            return View(registerTbl);
        }

        // POST: Register/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Password,Gender,PhoneNumber,Email,Address")] RegisterTbl registerTbl)
        {
            if (ModelState.IsValid)
            {
                DCTDB.Entry(registerTbl).State = EntityState.Modified;
                DCTDB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(registerTbl);
        }

        // GET: Register/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterTbl registerTbl = DCTDB.RegisterTbls.Find(id);
            if (registerTbl == null)
            {
                return HttpNotFound();
            }
            return View(registerTbl);
        }

        // POST: Register/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegisterTbl registerTbl = DCTDB.RegisterTbls.Find(id);
            DCTDB.RegisterTbls.Remove(registerTbl);
            DCTDB.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(RegisterTbl model)
        {
                // Check if EmailID or PhoneNumber and password match
                var user = DCTDB.RegisterTbls.FirstOrDefault(u => (u.Email == model.Email || u.PhoneNumber == model.PhoneNumber) && u.Password == model.Password);

                if (user != null)
                {

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt. Please check your Email and password.");
                }
            // If we got this far, something failed, redisplay the form
            return View(model);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DCTDB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
