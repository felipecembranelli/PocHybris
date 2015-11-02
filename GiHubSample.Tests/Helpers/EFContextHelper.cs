using GitHubSample.Data.Infrastructure;
using GitHubSample.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubSample.Tests.Helpers
{

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

    public class DatabaseFactoryHelper : Disposable, IDatabaseFactory
    {
        private EFContextHelper dataContext;

        public DatabaseFactoryHelper(EFContextHelper dtContext)
        {
            dataContext = dtContext;
        }

        public DbContext Get()
        {
            return dataContext ?? (dataContext = new EFContextHelper());
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }

}
