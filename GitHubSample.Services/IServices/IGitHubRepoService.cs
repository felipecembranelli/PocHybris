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
        IEnumerable<GitHubRepo> GetAll();
        IEnumerable<GitHubRepo> GetUserRepositories();
        GitHubRepoJson SearchByRepoName(string query);
        GitHubRepo GetById(int id);
	} 
}
