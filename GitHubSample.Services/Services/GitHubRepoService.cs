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

        #region favorities methods from local database

        /// <summary>
        /// Return all favorities repositories from local database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GitHubRepo> GetAllFavorities()
        {
            return this.gitHubRepoRepository.GetAll();
        }

        /// <summary>
        /// Mark repository as favorite, adding to local database
        /// </summary>
        /// <param name="repository"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Unmark repository as favorite, removing from local database
        /// </summary>
        /// <param name="repository"></param>
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

        /// <summary>
        ///  Verify if repository is marked as favorite or not
        /// </summary>
        /// <param name="gitHubRepoId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Return all user repositories from github
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Search github looking for query criteria, return a list of repositories
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Return a github repository throught its owner and repository name
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repoName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Return a list of repository's contributors
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repoName"></param>
        /// <returns></returns>
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
