using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubSample.Data.Infrastructure;
using GitHubSample.Model;

namespace GitHubSample.Data.Repository
{

    public interface IGitHubRepoRepository: IRepository<GitHubRepo>
    {
        #region GitHub api wrapper

        IEnumerable<GitHubRepo> GetUserRepositories();
        IEnumerable<GitHubRepo> SearchRepositories(string query);
        GitHubRepo GetRepoByName(string owner, string repoName);
        IEnumerable<GitHubUserDTO> GetRepoContributors(string owner, string repoName);

        #endregion

        #region GitHub EF wrapper

        void UnMarkAsFavorite(GitHubRepo repository);
        bool IsFavoriteRepo(int gitHubRepoId);

        #endregion
    }
}
  