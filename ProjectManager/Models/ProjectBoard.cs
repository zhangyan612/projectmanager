using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
    public class ProjectBoard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public string cssClass { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }

        public Guid ProjectId { get; set; }
        //public virtual Project Project { get; set; }
    }
}