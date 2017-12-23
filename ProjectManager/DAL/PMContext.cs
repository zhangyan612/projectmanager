using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ProjectManager.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjectManager.DAL.Configuration;
using System.Data.Entity.Infrastructure;

namespace ProjectManager.DAL
{
    public class PMContext : IdentityDbContext<ApplicationUser>
    {
        public PMContext() : base("PMContext")
        {
            Database.SetInitializer<PMContext>(new CreateDatabaseIfNotExists<PMContext>());
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<Project> Projects { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Remove<IncludeMetadataConvention>();

            //modelBuilder.Configurations.Add(new CommentConfiguration());
            //modelBuilder.Configurations.Add(new CommentUserConfiguration());
            //modelBuilder.Configurations.Add(new FocusConfiguration());
            //modelBuilder.Configurations.Add(new FollowRequestConfiguration());
            //modelBuilder.Configurations.Add(new FollowUserConfiguration());
            //modelBuilder.Configurations.Add(new GoalConfiguration());
            //modelBuilder.Configurations.Add(new GoalStatusConfiguration());
            //modelBuilder.Configurations.Add(new GroupCommentConfiguration());
            //modelBuilder.Configurations.Add(new GroupCommentUserConfguration());
            //modelBuilder.Configurations.Add(new GroupConfiguration());
            //modelBuilder.Configurations.Add(new GroupGoalConfiguration());
            //modelBuilder.Configurations.Add(new GroupInvitationConfiguration());
            //modelBuilder.Configurations.Add(new GroupRequestConfiguration());
            //modelBuilder.Configurations.Add(new GroupUpdateSupportConfiguration());
            //modelBuilder.Configurations.Add(new GroupUpdateUserConfiguration());
            //modelBuilder.Configurations.Add(new GroupUserConfiguration());
            //modelBuilder.Configurations.Add(new MetricConfiguration());
            //modelBuilder.Configurations.Add(new RegistrationTokenConfiguration());
            //modelBuilder.Configurations.Add(new SupportConfiguration());
            //modelBuilder.Configurations.Add(new SupportInvitationConfiguration());
            //modelBuilder.Configurations.Add(new UpdateConfiguration());
            //modelBuilder.Configurations.Add(new UpdateSupportConfiguration());
            modelBuilder.Configurations.Add(new UserProfileConfiguration());

        }

        public System.Data.Entity.DbSet<ProjectManager.Models.Team> Teams { get; set; }

        public System.Data.Entity.DbSet<ProjectManager.Models.TeamMember> TeamMembers { get; set; }

        public System.Data.Entity.DbSet<ProjectManager.Models.TeamProject> TeamProjects { get; set; }
    }
}