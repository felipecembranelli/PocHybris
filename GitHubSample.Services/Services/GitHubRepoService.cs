using GitHubSample.Data.Infrastructure;
using GitHubSample.Data.Repository;
using GitHubSample.Model;
using GitHubSample.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubSample.Services
{
    public class GitHubRepoService : IGitHubRepoService
    {
        private readonly IGitHubRepoRepository gitHubRepoRepository;
        private readonly IUnitOfWork unitOfWork;

        public GitHubRepoService(IGitHubRepoRepository gitHubRepoRepository, 
                                    IUnitOfWork unitOfWork)
        {
            
            this.gitHubRepoRepository = gitHubRepoRepository;
            this.unitOfWork = unitOfWork;
        }

        #region favorities methods

        public IEnumerable<GitHubRepo> GetAllFavorities()
        {
            return this.gitHubRepoRepository.GetAll();
        }

        public GitHubRepo MarkAsFavorite(GitHubRepo repository)
        {
            if (repository == null)
                return null;

            try
            {
                var ret = this.gitHubRepoRepository.AddandReturn(repository);

                this.unitOfWork.Commit();

                return ret;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UnMarkAsFavorite(GitHubRepo repository)
        {
            if (repository == null)
                return;

            try
            {
                this.gitHubRepoRepository.UnMarkAsFavorite(repository);

                this.unitOfWork.Commit();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsFavoriteRepo(int gitHubRepoId)
        {
            try
            {
                return this.gitHubRepoRepository.IsFavoriteRepo(gitHubRepoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region GitHub services API

        public IEnumerable<GitHubRepo> GetUserRepositories(string userName)
        {
            try
            {
                return this.gitHubRepoRepository.GetUserRepositories(userName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<GitHubRepo> SearchByRepoName(string query)
        {
            try
            {
                return this.gitHubRepoRepository.SearchRepositories(query);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public GitHubRepo GetRepoByName(string owner, string repoName)
        {
            try
            {
                return this.gitHubRepoRepository.GetRepoByName(owner, repoName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<GitHubUserDTO> GetRepoContributors(string owner, string repoName)
        {
            try
            {
                return this.gitHubRepoRepository.GetRepoContributors(owner, repoName);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
