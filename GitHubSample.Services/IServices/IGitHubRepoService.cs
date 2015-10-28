using GitHubSample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubSample.Services.IServices
{
    public interface IGitHubRepoService
	{
        IEnumerable<GitHubRepo> GetAllFavorities();
        void MarkAsFavorite(GitHubRepo repository);
        void UnMarkAsFavorite(GitHubRepo repository);
        bool IsFavoriteRepo(int gitHubRepoId);

        IEnumerable<GitHubRepo> GetUserRepositories();
        IEnumerable<GitHubRepo> SearchByRepoName(string query);
        GitHubRepo GetRepoByName(string owner, string repoName);
        IEnumerable<GitHubUserDTO> GetRepoContributors(string owner, string repoName);
    } 
}
