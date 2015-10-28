using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GitHubSample.Data.Infrastructure;

namespace GiHubSample.Web.Tests.Repositories
{
    [TestClass]
    public class GitHubServiceTest
    {
        [TestMethod]
        public void GetUserRepositories()
        {
            //// arrange
            //GitHubSample.Data.Repository.GitHubRepoRepository repo = new GitHubSample.Data.Repository.GitHubRepoRepository();
            //IUnitOfWork unitOfWork = new UnitOfWork(new DatabaseFactory());
            //GitHubSample.Services.GitHubRepoService svc = new GitHubSample.Services.GitHubRepoService(repo, unitOfWork);

            //// act
            //var ret = svc.GetUserRepositories();


            //// assert
            //Assert.IsNotNull(ret);

        }

        [TestMethod]
        public void SearchRepositories()
        {
            //// arrange
            //string repositoryName = "MerakiCaptivePortal";
            //int expected = 1;
            //GitHubSample.Data.Repository.GitHubRepoRepository repo = new GitHubSample.Data.Repository.GitHubRepoRepository();
            //IUnitOfWork unitOfWork = new UnitOfWork(new DatabaseFactory());
            //GitHubSample.Services.GitHubRepoService svc = new GitHubSample.Services.GitHubRepoService(repo, unitOfWork);

            //// act
            //var ret = svc.SearchByRepoName(repositoryName);

            ////assert
            //Assert.AreEqual(expected, ret.Items.Length, "Repositório não encontrado.");

        }
    }
}
