using ProjectManager.DAL;
using ProjectManager.DAL.Services;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace ProjectManager.Controllers
{
    public class BoardController : Controller
    {
        /// <summary>
        /// View board by project id
        /// add board to project
        /// delete single board
        /// </summary>
        /// <returns></returns>
        //private PMContext db = new PMContext();

        private readonly IProjectBoardService boardService;

        public BoardController(IProjectBoardService boardService)
        {
            this.boardService = boardService;
        }

        // Add board

        // GET: Board
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Kanban()
        {
            return View();
        }

        public ActionResult Project(string id)
        {
            ViewBag.ID = id;
            return View();
        }

        public Task AddBoardItem(int id, Guid pid, string bName, string text)
        {
            Task item = boardService.CreateBoardItem(id, pid, bName, text);
            return item;
        }

        // GET: Board/Details/5
        public string Details(Guid pid)
        {
            List<ProjectBoard> existing = boardService.GetBoardByProject(pid);
            if(existing.Count == 0)
            {
                existing = boardService.initBoard(pid);
            }
            string json = JsonConvert.SerializeObject(existing);

            return json;
        }

        // GET: Board/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Board/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Board/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Board/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Board/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Board/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
