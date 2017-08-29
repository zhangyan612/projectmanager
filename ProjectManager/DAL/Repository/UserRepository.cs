using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectManager.Models;

namespace ProjectManager.DAL.Repository
{
    public class UserRepository : RepositoryBase<ApplicationUser>, IUserRepository
    {
        public UserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IUserRepository : IRepository<ApplicationUser>
    {

    }
}