﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using ProjectManager.Models;

namespace ProjectManager.DAL
{
    public class PMInitializer : DropCreateDatabaseIfModelChanges<PMContext>
    {
        protected override void Seed(PMContext context)
        {
            base.Seed(context);
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