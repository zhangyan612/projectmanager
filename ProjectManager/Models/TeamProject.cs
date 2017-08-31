using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
    public class TeamProject
    {
        public int Id { get; set; }
        public Guid TeamId { get; set; }
        public Guid ProjectId { get; set; }

        public virtual Team Team { get; set; }
        public virtual Project Project { get; set; }
    }
}