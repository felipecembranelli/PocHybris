using GitHubSample.Data;

namespace GitHubSample.Data.Infrastructure
{
public class DatabaseFactory : Disposable, IDatabaseFactory
{
    private GitHubSampleEntities dataContext;
    public GitHubSampleEntities Get()
    {
        return dataContext ?? (dataContext = new GitHubSampleEntities());
    }
    protected override void DisposeCore()
    {
        if (dataContext != null)
            dataContext.Dispose();
    }
}
}
