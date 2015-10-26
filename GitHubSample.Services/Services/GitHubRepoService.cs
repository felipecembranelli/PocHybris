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

        public IEnumerable<GitHubRepo> GetAll()
        {
            throw new NotImplementedException();
        }

        public GitHubRepo GetById(int id)
        {
            throw new NotImplementedException();
        }

        //public IEnumerable<Model.GitHubRepo> GetAll()
        //{
        //    return this.gitHubRepoRepository.GetAll();
        //}

        //public Model.GitHubRepo GetById(int id)
        //{
        //    return this.gitHubRepoRepository.GetById(id);
        //}

        public IEnumerable<GitHubRepo> GetUserRepositories()
        {
            return this.gitHubRepoRepository.GetUserRepositories();
        }

        public GitHubRepoJson SearchByRepoName(string query)
        {
            return this.gitHubRepoRepository.SearchRepositories(query);
        }

    }
}
