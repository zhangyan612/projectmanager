using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
    public class ProjectTaskList
    {
        public int Id { get; set; }
        public Guid ProjectId { get; set; }
        public int? BoardId { get; set; }
        public string Text { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public decimal Progress { get; set; }
        public int SortOrder { get; set; }
        public string Type { get; set; }
        public int? ParentId { get; set; }
        public bool Active { get; set; }
        public string Status { get; set; }
        public int? DescriptionId { get; set; }
        public List<TaskAssignment> AssignedUserList { get; set; }
    }
}