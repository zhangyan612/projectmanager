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

        public string BoardItemData(int id, Guid pid, string bName, string text, string action)
        {
            string json = "";
            if(action == "Create")
            {
                Task item = boardService.CreateBoardItem(id, pid, bName, text);
                json = JsonConvert.SerializeObject(item);
            }
            if (action == "Edit")
            {
                Task item = boardService.EditBoardItem(id, text);
                json = JsonConvert.SerializeObject(item);
            }
            return json;
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


        // POST: Board/UpdateBoardItem
        [HttpPost]
        public string UpdateBoardItem(int id, int targetId)
        {
            boardService.UpdateBoardItem(id, targetId);
            return "Success";
        }

        // POST: Board/UpdateBoardItem
        [HttpPost]
        public string DeleteItem(int id)
        {
            boardService.DeleteBoardItem(id);
            return "Success";
        }

    }
}
