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

namespace ProjectManager.Controllers
{
    public class TeamProjectsController : Controller
    {
        private PMContext db = new PMContext();

        // GET: TeamProjects
        public ActionResult Index()
        {
            var teamProjects = db.TeamProjects.Include(t => t.Project).Include(t => t.Team);
            return View(teamProjects.ToList());
        }

        // GET: TeamProjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamProject teamProject = db.TeamProjects.Find(id);
            if (teamProject == null)
            {
                return HttpNotFound();
            }
            return View(teamProject);
        }

        // GET: TeamProjects/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name");
            return View();
        }

        // POST: TeamProjects/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TeamId,ProjectId")] TeamProject teamProject)
        {
            if (ModelState.IsValid)
            {
                db.TeamProjects.Add(teamProject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", teamProject.ProjectId);
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", teamProject.TeamId);
            return View(teamProject);
        }

        // GET: TeamProjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamProject teamProject = db.TeamProjects.Find(id);
            if (teamProject == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", teamProject.ProjectId);
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", teamProject.TeamId);
            return View(teamProject);
        }

        // POST: TeamProjects/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TeamId,ProjectId")] TeamProject teamProject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamProject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", teamProject.ProjectId);
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", teamProject.TeamId);
            return View(teamProject);
        }

        // GET: TeamProjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamProject teamProject = db.TeamProjects.Find(id);
            if (teamProject == null)
            {
                return HttpNotFound();
            }
            return View(teamProject);
        }

        // POST: TeamProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeamProject teamProject = db.TeamProjects.Find(id);
            db.TeamProjects.Remove(teamProject);
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
