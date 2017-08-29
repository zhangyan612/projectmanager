using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ProjectManager.Models;

namespace ProjectManager.DAL
{
    public class PMContext : DbContext
    {
        public PMContext() : base("PMContext") { }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}