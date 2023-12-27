using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DCTTECHNOLOGIES.Model;

namespace DCTTECHNOLOGIES.Controllers
{
    public class QueriesController : Controller
    {
        private DCTDBEntities db = new DCTDBEntities();

        // GET: QueriesTbls
        public ActionResult Index()
        {
            return View(db.QueriesTbls.ToList());
        }

        // GET: QueriesTbls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QueriesTbl queriesTbl = db.QueriesTbls.Find(id);
            if (queriesTbl == null)
            {
                return HttpNotFound();
            }
            return View(queriesTbl);
        }

        // GET: QueriesTbls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QueriesTbls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Email,PhoneNumber,Address,Comments,UploadFile")] QueriesTbl queriesTbl, HttpPostedFileBase file)
        {
                try
                {
                    // Handle file upload logic here
                    if (file != null && file.ContentLength > 0)
                    {
                        string fileExtension = Path.GetExtension(file.FileName);
                        string[] allowedExtensions = { ".pdf", ".doc", ".docx", ".png", ".jpg", ".jpeg" };

                        if (allowedExtensions.Contains(fileExtension))
                        {
                            var maxFileSize = 5 * 1024 * 1024; // 5MB
                            if (file.ContentLength > maxFileSize)
                            {
                                ViewBag.Message = "File size exceeds the maximum limit of 5MB.";
                                return View();
                            }
                            // Save the file to the server or perform any other necessary operations
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/Content/UploadedFiles"), fileName);
                            file.SaveAs(path);

                            // Display success message
                            ViewBag.Message = "Query submitted successfully!";
                        }
                        else
                        {
                            ViewBag.Message = "Only pdf, png, jpg, doc and docx files are allowed.";

                        }
                    }
                    else
                    {
                        // Display error message
                        ViewBag.Message = "Please select a file to upload.";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
            if (ModelState.IsValid)
            {
                queriesTbl.UploadFile = Path.GetFileName(file.FileName);
                db.QueriesTbls.Add(queriesTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(queriesTbl);
        }

        // GET: QueriesTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QueriesTbl queriesTbl = db.QueriesTbls.Find(id);
            if (queriesTbl == null)
            {
                return HttpNotFound();
            }
            return View(queriesTbl);
        }

        // POST: QueriesTbls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Email,PhoneNumber,Address,Comments,UploadFile")] QueriesTbl queriesTbl, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Handle file upload logic here
                    if (file != null && file.ContentLength > 0)
                    {
                        string fileExtension = Path.GetExtension(file.FileName);
                        string[] allowedExtensions = { ".pdf", ".doc", ".docx", ".png", ".jpg", ".jpeg" };

                        if (allowedExtensions.Contains(fileExtension))
                        {
                            var maxFileSize = 5 * 1024 * 1024; // 5MB
                            if (file.ContentLength > maxFileSize)
                            {
                                ViewBag.Message = "File size exceeds the maximum limit of 5MB.";
                                return View();
                            }


                            // Save the file to the server or perform any other necessary operations
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/Content/UploadedFiles"), fileName);
                            file.SaveAs(path);

                            // Display success message
                            ViewBag.Message = "Query submitted successfully!";
                        }
                        else
                        {
                            ViewBag.Message = "Only pdf, png, jpg, doc and docx files are allowed.";

                        }
                    }
                    else
                    {
                        // Display error message
                        ViewBag.Message = "Please select a file to upload.";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
                queriesTbl.UploadFile = Path.GetFileName(file.FileName);
                db.Entry(queriesTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(queriesTbl);
        }

        // GET: QueriesTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QueriesTbl queriesTbl = db.QueriesTbls.Find(id);
            if (queriesTbl == null)
            {
                return HttpNotFound();
            }
            return View(queriesTbl);
        }

        // POST: QueriesTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QueriesTbl queriesTbl = db.QueriesTbls.Find(id);
            db.QueriesTbls.Remove(queriesTbl);
            db.SaveChanges();
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
