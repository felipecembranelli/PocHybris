using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubSample.Data.Infrastructure;
using GitHubSample.Model;

namespace GitHubSample.Data.Repository
{

    public interface IGitHubRepoRepository
    {
        IEnumerable<GitHubRepo> GetUserRepositories();
        GitHubRepoJson SearchRepositories(string query);
    }
}
  