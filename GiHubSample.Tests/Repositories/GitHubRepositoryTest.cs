using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GiHubSample.Web.Tests.Repositories
{
    [TestClass]
    public class GitHubRepositoryTest
    {
        [TestMethod]
        public void GetUserRepositories()
        {
            GitHubSample.Data.Repository.GitHubRepoRepository repo = new GitHubSample.Data.Repository.GitHubRepoRepository();

            var ret = repo.GetUserRepositories();

            Assert.IsNotNull(ret);

        }

        [TestMethod]
        public void SearchRepositories()
        {
            // arrange
            string repositoryName = "MerakiCaptivePortal";
            int expected = 1;
            GitHubSample.Data.Repository.GitHubRepoRepository repo = new GitHubSample.Data.Repository.GitHubRepoRepository();

            // act
            var ret = repo.SearchRepositories(repositoryName);

            //assert
            Assert.AreEqual(expected, ret.Items.Length, "Repositório não encontrado.");

        }
    }
}
