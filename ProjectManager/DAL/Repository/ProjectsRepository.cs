using ProjectManager.Models;

namespace ProjectManager.DAL.Repository
{
    public class ProjectsRepository : RepositoryBase<Project>, IProjectsRepository
    {
        public ProjectsRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IProjectsRepository : IRepository<Project>
    {
    }
}