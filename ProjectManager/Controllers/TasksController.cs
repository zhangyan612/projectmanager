using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectManager.DAL;
using ProjectManager.Models;
using Newtonsoft.Json;
using ProjectManager.Services;

namespace ProjectManager.Controllers
{
    /// <summary>
    /// This is more for admin to manage all tasks
    /// </summary>
    [Authorize]
    public class TasksController : Controller
    {
        private PMContext db = new PMContext();

        // GET: Tasks
        public ActionResult Index()
        {
            var tasks = db.Tasks;
            return View(tasks.ToList());
        }

        public ActionResult Project(Guid id)
        {
            var tasks = db.Tasks.Where(x => x.ProjectId == id);
            ViewBag.ProjectId = id;
            return View(tasks.ToList());
        }

        public ActionResult Test()
        {
            return View();
        }


        public string UserList()
        {
            var users = db.Users.ToList();
            return JsonConvert.SerializeObject(users);
        }


        public string GetTasks(Guid id)
        {
            var tasks = ProjectTaskService.GetProjectTasks(id);
            string json = JsonConvert.SerializeObject(tasks);
            return json;
        }

        public ActionResult SaveTasks(Task task)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            db.Tasks.Add(task);
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }


        [HttpGet]
        public string TaskDescription(int id)
        {
            var task = ProjectTaskService.GetTaskDescription(id);
            string json = JsonConvert.SerializeObject(task);
            return json;
        }

        [HttpPost]
        public void SaveDescription(int id, string desc)
        {
            var user = User.Identity.Name;
            ProjectTaskService.SaveTaskDescription(id, desc, user);
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        //// GET: Tasks/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
        //    return View();
        //}

        //// POST: Tasks/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Text,StartDate,Duration,Progress,SortOrder,Type,ParentId,ProjectId")] Task task)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Tasks.Add(task);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", task.ProjectId);
        //    return View(task);
        //}

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", task.ProjectId);
            return View(task);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,StartDate,Duration,Progress,SortOrder,Type,ParentId,ProjectId")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", task.ProjectId);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
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
