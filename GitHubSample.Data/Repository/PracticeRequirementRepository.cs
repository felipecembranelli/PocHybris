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
    public class PracticeRequirementeRepository : Infrastructure.RepositoryBase<PracticeRequirement>, IPracticeRequirementRepository
    {

        public PracticeRequirementeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IEnumerable<PracticeRequirement> GetByRequirementsByPracticeId(int practiceId)
        {
            return base.GetAll().Where(p => p.PracticeId == practiceId);
        }
    }
}