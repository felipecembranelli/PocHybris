using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using GitHubSample.Model;
using GitHubSample.Data.Infrastructure;

namespace GitHubSample.Data.Repository
{
    public class ProcessPerspectiveRepository : Infrastructure.RepositoryBase<ProcessPerspective>, IProcessPerspectiveRepository
    {

        public ProcessPerspectiveRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}