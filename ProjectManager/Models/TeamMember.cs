using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        //public Guid Id { get; set; }
        //public string Name { get; set; }
        public Guid TeamId { get; set; }
        public Guid UserId { get; set; }

        public virtual Team Team { get; set; }
        //public virtual ApplicationUser User { get; set; }
    }
}