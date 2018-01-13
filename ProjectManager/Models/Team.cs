using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<TeamMember> Members { get; set; }
    }
}