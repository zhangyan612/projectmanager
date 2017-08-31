using ProjectManager.DAL.Repository;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManager.DAL.Services
{

    public interface IProjectsService
    {
        Project GetProject(Guid? id);
        List<Project> GetProjectByUser(string userid);

        void CreateProject(Project project, string userId);
        void UpdateProject(Project project);
        void DeleteProject(Guid id);
        void SaveProject();
    }

    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository projectsRepository;
        private readonly IUnitOfWork unitOfWork;
        public ProjectsService(IProjectsRepository projectsRepository, IUnitOfWork unitOfWork)
        {
            this.projectsRepository = projectsRepository;
            this.unitOfWork = unitOfWork;
        }

        public Project GetProject(Guid? id)
        {
            var project = projectsRepository.Get(u => u.Id == id);
            return project;
        }

        public List<Project> GetProjectByUser(string userid)
        {
            var project = projectsRepository.GetMany(u => u.OwnerId == userid).ToList();
            return project;
        }

        public void CreateProject(Project project, string userId)
        {
            project.Id = Guid.NewGuid();
            project.CreatedDate = DateTime.Now;
            project.OwnerId = userId;
            projectsRepository.Add(project);
            SaveProject();
        }            

        public void UpdateProject(Project project)
        {
            projectsRepository.Update(project);
            SaveProject();
        }

        public void DeleteProject(Guid id)
        {
            var p = projectsRepository.GetById(id.ToString());
            projectsRepository.Delete(p);
            SaveProject();
        }

        public void SaveProject()
        {
            unitOfWork.Commit();
        }

    }

    //public interface IUserProfileService
    //{
    //    UserProfile GetProfile(int id);
    //    UserProfile GetUser(string userid);
    //    UserProfile GetUserByEmail(string email);

    //    void CreateUserProfile(string userId);
    //    void UpdateUserProfile(UserProfile user);
    //    void SaveUserProfile();
    //}
    //public class UserProfileService : IUserProfileService
    //{
    //    private readonly IUserProfileRepository userProfileRepository;
    //    private readonly IUnitOfWork unitOfWork;

    //    public UserProfileService(IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWork)
    //    {
    //        this.userProfileRepository = userProfileRepository;
    //        this.unitOfWork = unitOfWork;
    //    }

    //    public UserProfile GetProfile(int id)
    //    {
    //        var userprofile = userProfileRepository.Get(u => u.UserProfileId == id);
    //        return userprofile;
    //    }
    //    public UserProfile GetUser(string userid)
    //    {
    //        var userprofile = userProfileRepository.Get(u => u.UserId == userid);
    //        return userprofile;
    //    }

    //    public UserProfile GetUserByEmail(string email)
    //    {
    //        var userProfile = userProfileRepository.Get(u => u.Email == email);
    //        return userProfile;
    //    }

    //    public void CreateUserProfile(string userId)
    //    {

    //        UserProfile newUserProfile = new UserProfile();
    //        newUserProfile.UserId = userId;
    //        userProfileRepository.Add(newUserProfile);
    //        SaveUserProfile();
    //    }
    //    public void UpdateUserProfile(UserProfile user)
    //    {
    //        userProfileRepository.Update(user);
    //        SaveUserProfile();
    //    }
    //    public void SaveUserProfile()
    //    {
    //        unitOfWork.Commit();
    //    }
    //}
}