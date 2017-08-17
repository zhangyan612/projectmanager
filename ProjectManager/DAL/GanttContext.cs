using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ProjectManager.Models;

namespace ProjectManager.DAL
{
    public class GanttContext : DbContext
    {
        public GanttContext() : base("GanttContext") { }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Link> Links { get; set; }
    }
}