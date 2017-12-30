using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public string Desc { get; set; }
        public bool Public { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<TeamProject> TeamProjects { get; set; }
        public virtual ICollection<ProjectBoard> Boards { get; set; }

        //public Project()
        //{
        //    CreatedDate = DateTime.Now;
        //}
    }
}