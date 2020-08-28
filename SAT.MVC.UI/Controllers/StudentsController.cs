using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SAT.MVC.DATA.EF;
using PagedList;
using PagedList.Mvc;

namespace SAT.MVC.UI.Controllers
{
    public class StudentsController : Controller
    {
        private SATEntities db = new SATEntities();

        // GET: Students
        //public ActionResult Index()
        //{
        //    var students = db.Students.Include(s => s.StudentStatus);
        //    return View(students.ToList());
        //}

        public ViewResult Index(int page = 1)
        {
            int pageSize = 5;

            var stu = db.Students.OrderBy(x => x.LastName).ToList();
            return View(stu.ToPagedList(page, pageSize));
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [Authorize(Roles = "Admin")]
        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "StatusName");
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,FirstName,LastName,DOB,Major,SSID,HasScholarship,Email,Address,City,State,Zip,EmergencyContact,EmergencyPhone,StudentPhone,StudentImage")] Student student, HttpPostedFileBase StudentImage)
        {
            if (ModelState.IsValid)
            {
                #region File Upload

                string imgName = "noimg.jpg";
                if (StudentImage != null)
                {
                    imgName = StudentImage.FileName;
                    string ext = imgName.Substring(imgName.LastIndexOf("."));
                    string[] goodExtensions = { "jpeg", "jpg", "gif", "png" };

                    if (goodExtensions.Contains(ext.ToLower()) && (StudentImage.ContentLength < 4194304))
                    {
                        imgName = Guid.NewGuid() + ext;
                        StudentImage.SaveAs(Server.MapPath("~/Content/images/" + imgName));
                    }
                    else
                    {
                        imgName = "noimg.jpg";
                    }
                }

                student.StudentImage = imgName;

                #endregion

                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "StatusName", student.SSID);
            return View(student);
        }

        [Authorize(Roles = "Admin")]
        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "StatusName", student.SSID);
            return View(student);
        }

        [Authorize(Roles = "Admin")]
        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,FirstName,LastName,DOB,Major,SSID,HasScholarship,Email,Address,City,State,Zip,EmergencyContact,EmergencyPhone,StudentPhone,StudentImage")] Student student, HttpPostedFileBase StudentImage)
        {
            if (ModelState.IsValid)
            {
                #region File Upload

                string imgName = "noimg.jpg";
                if (StudentImage != null)
                {
                    imgName = StudentImage.FileName;
                    string ext = imgName.Substring(imgName.LastIndexOf("."));
                    string[] goodExtensions = { "jpeg", "jpg", "gif", "png" };

                    if (goodExtensions.Contains(ext.ToLower()) && (StudentImage.ContentLength < 4194304))
                    {
                        imgName = Guid.NewGuid() + ext;
                        StudentImage.SaveAs(Server.MapPath("~/Content/images/" + imgName));
                    }

                    if (student.StudentImage != null && student.StudentImage != "noimg.jpg")
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/images/" + Session["currentImage"].ToString()));
                    }
                }

                student.StudentImage = imgName;

                #endregion

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "StatusName", student.SSID);
            return View(student);
        }

        [Authorize(Roles = "Admin")]
        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [Authorize(Roles = "Admin")]
        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);

            if (student.StudentImage != null && student.StudentImage != "noimg.jpg")
            {
                System.IO.File.Delete(Server.MapPath("~/Content/images/" + Session["currentImage"].ToString()));
            }
            
            db.Students.Remove(student);
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
