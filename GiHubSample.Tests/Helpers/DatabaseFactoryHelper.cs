using GitHubSample.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GitHubSample.Tests.Helpers
{
    /// <summary>
    /// Concrete class that implements interfaces to be injected in EF context,
    /// simulating in-memory database
    /// </summary>
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
