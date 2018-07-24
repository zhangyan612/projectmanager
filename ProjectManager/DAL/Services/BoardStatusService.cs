using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.DAL.Services
{
    public class BoardStatusService
    {
        public static Task CreateTaskRule(Task task, string newBoardName)
        {
            switch (newBoardName)
            {
                case "Backlog":
                    task.Active = false;
                    break;
                case "To Do":
                    task.Active = false;
                    break;
                case "In Progress":
                    task.StartDate = DateTime.Today;
                    task.Active = true;
                    break;
                case "Completed":
                    task.StartDate = DateTime.Today.AddDays(-3);
                    task.Duration = 3;
                    task.Active = true;
                    task.Progress = 1;
                    break;
                default:
                    break;
            }
            return task;
        }

        public static Task UpdateTaskRule(Task task, string newBoardName)
        {

            switch (newBoardName)
            {
                case "Backlog":
                    task.Active = false;
                    break;
                case "To Do":
                    task.Active = false;
                    break;
                case "In Progress":
                    //task.StartDate = DateTime.Today;
                    task.Active = true;
                    break;
                case "Completed":
                    var dayDiff = (DateTime.Today - task.StartDate).Days;
                    task.Duration = dayDiff == 0 ? 1 : dayDiff;
                    task.Active = true;
                    task.Progress = 1;
                    break;
                default:
                    break;
            }

            return task;
        }
    }
}