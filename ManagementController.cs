using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OntapASP.Models;

namespace OntapASP.Controllers
{
    public class ManagementController : Controller
    {
        private Company db = new Company();

        [ChildActionOnly]
        public PartialViewResult CataMenu()
        {
            var list = db.Departments.ToList();
            return PartialView(list);
        }

        [Route("emps/empbydept/{deptid}")]
        public ActionResult ListByDeptid(int deptid)
        {
            var list = db.Employees.Where(u => u.deptid == deptid).ToList();
            return View(list);
        }

        // GET: Management
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Department);
            return View(employees.ToList());
        }

        // GET: Management/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Management/Create
        public ActionResult Create()
        {
            ViewBag.deptid = new SelectList(db.Departments, "deptid", "deptname");
            return View();
        }

        // POST: Management/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            try
            {
                db.Employees.Add(emp);
                db.SaveChanges();
                return Json(new { result = true, JsonRequestBehavior.AllowGet });
            }
                
            catch(Exception ex)
            {
                return Json(new {result = false,error = ex.Message});
            }
        }

        // GET: Management/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.deptid = new SelectList(db.Departments, "deptid", "deptname", employee.deptid);
            return View(employee);
        }

        // POST: Management/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit( Employee emp)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { result = true, JsonRequestBehavior.AllowGet });
                }
                catch (Exception ex)
                {
                    return Json(new {result=false,error = ex.Message});
                }
                
            }
            ViewBag.deptid = new SelectList(db.Departments, "deptid", "deptname", emp.deptid);
            return View(emp);
        }

        // GET: Management/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Management/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Employee employee = db.Employees.Find(id);
                db.Employees.Remove(employee);
                db.SaveChanges();
                return Json(new { result = true, JsonRequestBehavior.AllowGet });
            }
            catch(Exception ex)
            {
                return Json(new { result = false, error = ex.Message });
            }
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
