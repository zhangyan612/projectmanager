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
using ProjectManager.DAL.Services;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace ProjectManager.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private PMContext db = new PMContext();

        private readonly IProjectsService projectService;
        private readonly IUserService userService;

        //private IUserMailer userMailer = new UserMailer();
        //public IUserMailer UserMailer
        //{
        //    get { return userMailer; }
        //    set { userMailer = value; }
        //}

        public ProjectsController(IProjectsService projectService, IUserService userService)
        {
            this.projectService = projectService;
            this.userService = userService;
        }
        // GET: Projects
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var projects = projectService.GetProjectByUser(userId).OrderByDescending(g => g.CreatedDate).ToList();

            return View(projects);
        }

        public ActionResult GetAllUsers()
        {
            var users = userService.GetUsers();
            var json = JsonConvert.SerializeObject(users);
            return Content(json);
        }

        public ActionResult AddUser(Guid ProjectId, string userId)
        {
            var user = userService.GetUser(userId);
            var json = JsonConvert.SerializeObject(user);
            return Content(json);
        }

        public ActionResult RemoveUser(Guid ProjectId, string userId)
        {
            var users = userService.GetUsers();
            var json = JsonConvert.SerializeObject(users);
            return Content(json);
        }


        // GET: Projects/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Project project = projectService.GetProject(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name");
            return View();
        }

        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Desc,Public,CreatedDate,TeamId")] Project project)
        {
            var userId = User.Identity.GetUserId();
            project.Id = Guid.NewGuid();

            if (ModelState.IsValid)
            {
                projectService.CreateProject(project, userId);
                return RedirectToAction("Project", "Board", new { id = project.Id });
                // return RedirectToAction("Details", new { id = project.Id });
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = projectService.GetProject(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,OwnerId,Desc,Public,CreatedDate")] Project project)
        {
            if (ModelState.IsValid)
            {
                projectService.UpdateProject(project);
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = projectService.GetProject(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            projectService.DeleteProject(id);
            //Project project = db.Projects.Find(id);
            //db.Projects.Remove(project);
            //db.SaveChanges();
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
