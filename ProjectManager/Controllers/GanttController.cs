using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using ProjectManager.DAL;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    [AllowAnonymous]
    public class GanttController : Controller
    {
        private readonly PMContext db = new PMContext();
        /// <summary>
        /// Get Gantt data as JSON
        /// </summary>
        /// <returns>JsonResult</returns>
        [HttpGet]
        public JsonResult Data(string ProjectId)
        {
            Guid PId = Guid.Parse(ProjectId);
            var jsonData = new
            {
                // create tasks array
                data = (
                    from t in db.Tasks.AsEnumerable().Where(a => a.ProjectId == PId && a.Active == true)
                    select new
                    {
                        id = t.Id,
                        text = t.Text,
                        start_date = t.StartDate.ToString("u"),
                        duration = t.Duration,
                        order = t.SortOrder,
                        progress = t.Progress,
                        open = true,
                        parent = t.ParentId,
                        type = (t.Type != null) ? t.Type : String.Empty
                    }
                ).ToList(),
                // create links array
                links = (
                    from l in db.Links.AsEnumerable()
                    select new
                    {
                        id = l.Id,
                        source = l.SourceTaskId,
                        target = l.TargetTaskId,
                        type = l.Type
                    }
                ).ToList()
            };

            return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// Update Gantt tasks/links: insert/update/delete 
        /// </summary>
        /// <param name="form">Gantt data</param>
        /// <returns>XML response</returns>
        [HttpPost]
        public ContentResult Save(FormCollection form, string ProjectId)
        {
            var dataActions = GanttRequest.Parse(form, Request.QueryString["gantt_mode"], ProjectId);
            try
            {
                foreach (var ganttData in dataActions)
                {
                    switch (ganttData.Mode)
                    {
                        case GanttMode.Tasks:
                            UpdateTasks(ganttData);
                            break;
                        case GanttMode.Links:
                            UpdateLinks(ganttData);
                            break;
                    }
                }
                db.SaveChanges();
            }
            catch
            {
                // return error to client if something went wrong
                dataActions.ForEach(g => { g.Action = GanttAction.Error; });
            }
            return GanttRespose(dataActions);
        }

        /// <summary>
        /// Update gantt tasks
        /// </summary>
        /// <param name="ganttData">GanttData object</param>
        private void UpdateTasks(GanttRequest ganttData)
        {
            switch (ganttData.Action)
            {
                case GanttAction.Inserted:
                    // add new gantt task entity
                    ganttData = DecideBoard(ganttData);
                    db.Tasks.Add(ganttData.UpdatedTask);
                    break;
                case GanttAction.Deleted:
                    // remove gantt tasks
                    db.Tasks.Remove(db.Tasks.Find(ganttData.SourceId));
                    break;
                case GanttAction.Updated:
                    // update gantt task
                    ganttData = DecideBoard(ganttData);
                    db.Entry(db.Tasks.Find(ganttData.UpdatedTask.Id)).CurrentValues.SetValues(ganttData.UpdatedTask);
                    break;
                default:
                    ganttData.Action = GanttAction.Error;
                    break;
            }
        }

        /// <summary>
        /// Update gantt links
        /// </summary>
        /// <param name="ganttData">GanttData object</param>
        private void UpdateLinks(GanttRequest ganttData)
        {
            switch (ganttData.Action)
            {
                case GanttAction.Inserted:
                    // add new gantt link
                    db.Links.Add(ganttData.UpdatedLink);
                    break;
                case GanttAction.Deleted:
                    // remove gantt link
                    db.Links.Remove(db.Links.Find(ganttData.SourceId));
                    break;
                case GanttAction.Updated:
                    // update gantt link
                    db.Entry(db.Links.Find(ganttData.UpdatedLink.Id)).CurrentValues.SetValues(ganttData.UpdatedLink);
                    break;
                default:
                    ganttData.Action = GanttAction.Error;
                    break;
            }
        }

        /// <summary>
        /// Create XML response for gantt
        /// </summary>
        /// <param name="ganttData">Gantt data</param>
        /// <returns>XML response</returns>
        private ContentResult GanttRespose(List<GanttRequest> ganttDataCollection)
        {
            var actions = new List<XElement>();
            foreach (var ganttData in ganttDataCollection)
            {
                var action = new XElement("action");
                action.SetAttributeValue("type", ganttData.Action.ToString().ToLower());
                action.SetAttributeValue("sid", ganttData.SourceId);
                if(ganttData.Action != GanttAction.Deleted)
                    action.SetAttributeValue("tid", (ganttData.Mode == GanttMode.Tasks) ? ganttData.UpdatedTask.Id : ganttData.UpdatedLink.Id);
                actions.Add(action);
            }

            var data = new XDocument(new XElement("data", actions));
            data.Declaration = new XDeclaration("1.0", "utf-8", "true");
            return Content(data.ToString(), "text/xml");
        }

        private GanttRequest DecideBoard(GanttRequest ganttData)
        {
            var boards = db.Projects.Find(ganttData.UpdatedTask.ProjectId).Boards;
            //var boards = db.Boards.Where(a => a.ProjectId == ganttData.UpdatedTask.ProjectId);
            var progressBoard = boards.Where(b => b.Name == "In Progress").First();
            var toDoBoard = boards.Where(b => b.Name == "To Do").First();
            var completeBoard = boards.Where(b => b.Name == "Completed").First();
            var progress = ganttData.UpdatedTask.Progress;

            if (progress == 1)
            {
                ganttData.UpdatedTask.BoardId = completeBoard.Id;
            }
            else if(progress > 0 && progress < 1)
            {
                ganttData.UpdatedTask.BoardId = progressBoard.Id;
            }
            else
            {
                if (ganttData.UpdatedTask.StartDate >= DateTime.Today)
                {
                    ganttData.UpdatedTask.BoardId = toDoBoard.Id;
                }
                else
                {
                    ganttData.UpdatedTask.BoardId = progressBoard.Id;
                }
            }
            return ganttData;
        }
    }
}