using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubSample.Data.Infrastructure;
using GitHubSample.Model;

namespace GitHubSample.Data.Repository
{

    public interface IPracticeRequirementRepository : IRepository<PracticeRequirement>
    {
        IEnumerable<PracticeRequirement> GetByRequirementsByPracticeId(int practiceId);
    }
}
 