using ProjectManager.DAL;
using ProjectManager.Models;
using ProjectManager.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.Services
{
    public class ProjectTaskService
    {
        private static PMContext db = new PMContext();

        public static List<ProjectTaskList> GetProjectTasks(Guid projectId)
        {
            var tasks = db.Tasks.Where(x => x.ProjectId == projectId).ToList();

            List<ProjectTaskList> projectList = new List<ProjectTaskList>();

            foreach (var t in tasks)
            {
                ProjectTaskList projectTask = new ProjectTaskList();
                ObjectMapper.Convert(t, projectTask);

                projectTask.Status = t.Board.Name;

                projectList.Add(projectTask);
            }

            return projectList;
        }
    }
}