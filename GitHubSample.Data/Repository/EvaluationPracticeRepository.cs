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
    public class EvaluationPracticeRepository : Infrastructure.RepositoryBase<EvaluationPractice>, IEvaluationPracticeRepository
    {

        public EvaluationPracticeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}