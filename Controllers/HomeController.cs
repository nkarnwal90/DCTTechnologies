using DCTTECHNOLOGIES.Model;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCTTECHNOLOGIES.Controllers
{
    public class HomeController : Controller
    {
        private DCTDBEntities DCTDB = new DCTDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(QueriesTbl queries, HttpPostedFileBase file)
        {
            int ID = 0;
            //int ID = queries.ID;
            //string Name = queries.Name;
            //string Email = queries.Email;
            //int PhoneNumber = (int)queries.PhoneNumber;
            //string Address = queries.Address;
            //string Comments = queries.Comments;
            //string UploadFile = queries.UploadFile;
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
            if (queries.ID==0)
            {
                ID++;
                queries.ID = ID;
            }
                DCTDB.QueriesTbls.Add(queries);
                DCTDB.SaveChanges();
                return View("~/Views/Home/Query.cshtml");
                /*return RedirectToAction("Query")*/
        }
    }
}