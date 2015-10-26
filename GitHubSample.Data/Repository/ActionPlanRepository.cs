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
    public class ActionPlanRepository : Infrastructure.RepositoryBase<ActionPlan>, IActionPlanRepository
    {

        public ActionPlanRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}