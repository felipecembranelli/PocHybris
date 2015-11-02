using GitHubSample.Data;
using System.Data.Entity;

namespace GitHubSample.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private GitHubSampleEntities dataContext;
        public DbContext Get()
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
