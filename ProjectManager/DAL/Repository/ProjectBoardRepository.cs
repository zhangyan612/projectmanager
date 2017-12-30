using ProjectManager.Models;

namespace ProjectManager.DAL.Repository
{
    public class ProjectBoardRepository : RepositoryBase<ProjectBoard>, IProjectBoardRepository
    {
        public ProjectBoardRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IProjectBoardRepository : IRepository<ProjectBoard>
    {
    }
}