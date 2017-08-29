using ProjectManager.Models;

namespace ProjectManager.DAL
{
public class DatabaseFactory : Disposable, IDatabaseFactory
{
    private PMContext dataContext;
    public PMContext Get()
    {
        return dataContext ?? (dataContext = new PMContext());
    }
    protected override void DisposeCore()
    {
        if (dataContext != null)
            dataContext.Dispose();
    }
}
}
