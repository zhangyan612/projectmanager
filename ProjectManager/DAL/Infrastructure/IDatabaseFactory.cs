using System;
using ProjectManager.Models;

namespace ProjectManager.DAL
{
    public interface IDatabaseFactory : IDisposable
    {
        PMContext Get();
    }
}
