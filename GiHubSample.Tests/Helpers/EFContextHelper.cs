using GitHubSample.Data.Infrastructure;
using GitHubSample.Model;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;

namespace GitHubSample.Tests.Helpers
{
    /// <summary>
    /// Fake context simulante in-memory database (EF) for unit testing
    /// </summary>
    public class EFContextHelper : DbContext
    {
        public EFContextHelper()
            : base("Name=TestContext")
        {

        }
        public EFContextHelper(EntityConnection connection)
            : base(connection, true)
        {
        }

        public DbSet<GitHubRepo> GitHubRepo { get; set; }

    }
}
