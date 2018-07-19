using ProjectManager.DAL;
using ProjectManager.Models;
using ProjectManager.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                projectTask.AssignedUserId = t.AssignedTo.Select(a => a.UserProfileId).ToList();


                projectTask.AssignedFirstUser = projectTask.AssignedUserId.Count > 0 ?
                    db.Users.Find(projectTask.AssignedUserId.First()).UserName : null;

                projectList.Add(projectTask);
            }

            return projectList;
        }

        public static TaskDescription GetTaskDescription(int id)
        {
            var task = db.TaskDescription.Where(x => x.Id == id).First();
            return task;
        }

        public static void SaveTaskDescription(int id, string desc, string user)
        {
            var task = db.Tasks.Where(x => x.Id == id).First();

            if(task.DescriptionId != null)
            {
                var existingDesc = db.TaskDescription.Where(x => x.Id == task.DescriptionId).First();
                existingDesc.LastModifiedBy = user;
                existingDesc.Description = desc;
                existingDesc.LastModifiedDate = DateTime.Now;
                db.Entry(existingDesc).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                TaskDescription description = new TaskDescription()
                {
                    Description = desc,
                    LastModifiedBy = user,
                    LastModifiedDate = DateTime.Now
                };

                db.TaskDescription.Add(description);
                db.SaveChanges();

                int newId = description.Id;
                task.DescriptionId = newId;
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}