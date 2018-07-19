namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ProjectManager.Models;
    using ProjectManager.DAL;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<PMContext>
    {
        /// <summary>
        /// How to use migration
        /// Add-Migration SomeName
        /// Update-Database
        /// </summary>
        public Configuration()
        {
            //AutomaticMigrationsEnabled = false;
            AutomaticMigrationsEnabled = false;
            //AutomaticMigrationDataLossAllowed = true;

            ContextKey = "ProjectManager.DAL.PMContext";
        }

        //protected override void Seed(PMContext context)
        //{
        //    List<Task> tasks = new List<Task>()
        //    {
        //        new Task() { Id = 1, Text = "Project #2", StartDate = DateTime.Today.AddDays(-3), Duration = 18, SortOrder = 10, Progress = 0.4m, ParentId = null },
        //        new Task() { Id = 2, Text = "Task #1", StartDate = DateTime.Today.AddDays(-2), Duration = 8, SortOrder = 10, Progress = 0.6m, ParentId = 1 },
        //        new Task() { Id = 3, Text = "Task #2", StartDate = DateTime.Today.AddDays(-1), Duration = 8, SortOrder = 20, Progress = 0.6m, ParentId = 1 }
        //    };

        //    tasks.ForEach(s => context.Tasks.Add(s));
        //    context.SaveChanges();

        //    List<Link> links = new List<Link>()
        //    {
        //        new Link() { Id = 1, SourceTaskId = 1, TargetTaskId = 2, Type = "1" },
        //        new Link() { Id = 2, SourceTaskId = 2, TargetTaskId = 3, Type = "0" }
        //    };

        //    links.ForEach(s => context.Links.Add(s));
        //    context.SaveChanges();
        //}
    }
}
