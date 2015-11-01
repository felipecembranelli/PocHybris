using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GiHubSample.Web.Controllers;
using GitHubSample.Services.IServices;
using Moq;
using GitHubSample.Data.Repository;
using GitHubSample.Model;
using GiHubSample.Web.ViewModels;

namespace GiHubSample.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {

        // testar retorno de view (ListRepositories) (verificar importancia disso !!!)  - ok
        // testar se action retorna a lista de repos (userRepositories)
        // testar retorno de view (Detail) - ok
        // testar se action retorna repo detail
        // testar retorno de view (search) - ok
        // testar se action retorna a lista de repos com base no search (userRepositories)
        // testar retorno de view (ListFavoritiesRepos) - ok
        // testar model is ivalid ? (MarkAsFavorite)
        // testar se action retorna repo (userRepositories)

        [TestMethod]
        public void IndexAction_ReturnsListRepositoriesView()
        {
            // Arrange
            //var mockGitHubRepository = new Mock<IGitHubRepoRepository>();
            var mockGitHubService = new Mock<IGitHubRepoService>();

            HomeController controller = new HomeController(mockGitHubService.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("ListRepositories", result.ViewName);
        }

        [TestMethod]
        public void DetailAction_ReturnsRepoDetailView()
        {
            // Arrange
            //var mockGitHubRepository = new Mock<IGitHubRepoRepository>();
            var mockGitHubService = new Mock<IGitHubRepoService>();

            HomeController controller = new HomeController(mockGitHubService.Object);

            // Act
            ViewResult result = controller.Detail(1,"sampleRepo","ownerSample") as ViewResult;

            // Assert
            Assert.AreEqual("RepoDetail", result.ViewName);
        }

        [TestMethod]
        public void SearchAction_ReturnsListRepositoriesView()
        {
            // Arrange
            //var mockGitHubRepository = new Mock<IGitHubRepoRepository>();
            var mockGitHubService = new Mock<IGitHubRepoService>();

            HomeController controller = new HomeController(mockGitHubService.Object);

            // Act
            ViewResult result = controller.Search("repoNameSample") as ViewResult;

            // Assert
            Assert.AreEqual("ListRepositories", result.ViewName);
        }

        [TestMethod]
        public void ListFavoritiesReposAction_ReturnsListRepositoriesView()
        {
            // Arrange
            //var mockGitHubRepository = new Mock<IGitHubRepoRepository>();
            var mockGitHubService = new Mock<IGitHubRepoService>();

            HomeController controller = new HomeController(mockGitHubService.Object);

            // Act
            ViewResult result = controller.ListFavoritiesRepos() as ViewResult;

            // Assert
            Assert.AreEqual("ListRepositories", result.ViewName);
        }

        [TestMethod]
        public void IndexAction_ReturnsUserRepositoriesList()
        {
            // Arrange
            var mockGitHubService = new Mock<IGitHubRepoService>(MockBehavior.Strict);
            
            HomeController controller = new HomeController(mockGitHubService.Object);
            var userName = controller.DefaultUserRepository;

            // create a list of repositories to return 
            var userRepositories = GitHubRepoRepositoryMockHelper.GenerateFakeRepos(userName);

            mockGitHubService.Setup(s => s.GetUserRepositories(userName)).Returns(userRepositories);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            mockGitHubService.Verify(t => t.GetUserRepositories(userName));
            var userRepoList = (IEnumerable<GitHubRepoViewModel>)result.ViewData.Model;
            Assert.IsTrue(userRepoList.Count()>0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IndexAction_IfDefaultUserRepositoyIsNull_ReturnsException()
        {
            // Arrange
            var mockGitHubService = new Mock<IGitHubRepoService>();

            HomeController controller = new HomeController(mockGitHubService.Object);

            // force empty configuration
            var userName = string.Empty;        
            controller.DefaultUserRepository = userName;

            // create a list of repositories to return 
            var userRepositories = GitHubRepoRepositoryMockHelper.GenerateFakeRepos(userName);

            mockGitHubService.Setup(s => s.GetUserRepositories(userName)).Returns(userRepositories);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            // automatic assert
        }

        [TestMethod]
        public void DetailAction_IfRepoIsValid_ReturnsRepositoryDetail()
        {
            // Arrange
            var mockGitHubService = new Mock<IGitHubRepoService>(MockBehavior.Strict);

            HomeController controller = new HomeController(mockGitHubService.Object);

            var gitHubRepoIdFake = 123;
            var ownerFake = "owner1";
            var repoNameFake = "repoFake1";

            // create fake repository detail
            var repoDetail = GitHubRepoRepositoryMockHelper.GetRepoByName(ownerFake, repoNameFake);
            var repoContributors = GitHubRepoRepositoryMockHelper.GetRepoContributors(ownerFake, repoNameFake);

            mockGitHubService.Setup(s => s.GetRepoByName(ownerFake, repoNameFake)).Returns(repoDetail);
            mockGitHubService.Setup(s => s.GetRepoContributors(ownerFake, repoNameFake)).Returns(repoContributors);
            mockGitHubService.Setup(s => s.IsFavoriteRepo(gitHubRepoIdFake)).Returns(false);

            // Act
            var result = controller.Detail(gitHubRepoIdFake, repoNameFake, ownerFake) as ViewResult;

            // Assert
            var userRepoDetail = (GitHubRepoViewModel)result.ViewData.Model;
            Assert.IsNotNull(userRepoDetail);
        }

        [TestMethod]
        public void DetailAction_IfRepoNameIsNotValid_ReturnsNull()
        {
            // Arrange
            var mockGitHubService = new Mock<IGitHubRepoService>();

            HomeController controller = new HomeController(mockGitHubService.Object);

            var gitHubRepoIdFake = 123;
            var ownerFake = "owner1";
            string repoNameFake = null;

            // create fake repository detail
            var repoDetail = GitHubRepoRepositoryMockHelper.GetRepoByName(ownerFake, repoNameFake);
            var repoContributors = GitHubRepoRepositoryMockHelper.GetRepoContributors(ownerFake, repoNameFake);

            mockGitHubService.Setup(s => s.GetRepoByName(ownerFake, repoNameFake)).Returns(repoDetail);
            mockGitHubService.Setup(s => s.GetRepoContributors(ownerFake, repoNameFake)).Returns(repoContributors);
            mockGitHubService.Setup(s => s.IsFavoriteRepo(gitHubRepoIdFake)).Returns(false);

            // Act
            var result = controller.Detail(gitHubRepoIdFake, repoNameFake, ownerFake) as ViewResult;

            // Assert
            var userRepoDetail = (GitHubRepoViewModel)result.ViewData.Model;
            Assert.IsNull(userRepoDetail);
        }

        [TestMethod]
        public void DetailAction_IfOwnerIsNotValid_ReturnsNull()
        {
            // Arrange
            var mockGitHubService = new Mock<IGitHubRepoService>();

            HomeController controller = new HomeController(mockGitHubService.Object);

            var gitHubRepoIdFake = 123;
            string ownerFake = null;
            var repoNameFake = "repoFake1";

            // create fake repository detail
            var repoDetail = GitHubRepoRepositoryMockHelper.GetRepoByName(ownerFake, repoNameFake);
            var repoContributors = GitHubRepoRepositoryMockHelper.GetRepoContributors(ownerFake, repoNameFake);

            mockGitHubService.Setup(s => s.GetRepoByName(ownerFake, repoNameFake)).Returns(repoDetail);
            mockGitHubService.Setup(s => s.GetRepoContributors(ownerFake, repoNameFake)).Returns(repoContributors);
            mockGitHubService.Setup(s => s.IsFavoriteRepo(gitHubRepoIdFake)).Returns(false);

            // Act
            var result = controller.Detail(gitHubRepoIdFake, repoNameFake, ownerFake) as ViewResult;

            // Assert
            var userRepoDetail = (GitHubRepoViewModel)result.ViewData.Model;
            Assert.IsNull(userRepoDetail);
        }

        [TestMethod]
        public void DetailAction_IfRepoIdIsNotValid_ReturnsFavorityFlagFalse()
        {
            // Arrange
            var mockGitHubService = new Mock<IGitHubRepoService>();

            HomeController controller = new HomeController(mockGitHubService.Object);

            var gitHubRepoIdFake = -1;
            var ownerFake = "owner1";
            var repoNameFake = "repoFake1";

            // create fake repository detail
            var repoDetail = GitHubRepoRepositoryMockHelper.GetRepoByName(ownerFake, repoNameFake);
            var repoContributors = GitHubRepoRepositoryMockHelper.GetRepoContributors(ownerFake, repoNameFake);

            mockGitHubService.Setup(s => s.GetRepoByName(ownerFake, repoNameFake)).Returns(repoDetail);
            mockGitHubService.Setup(s => s.GetRepoContributors(ownerFake, repoNameFake)).Returns(repoContributors);
            mockGitHubService.Setup(s => s.IsFavoriteRepo(gitHubRepoIdFake)).Returns(false);

            // Act
            var result = controller.Detail(-1, repoNameFake, ownerFake) as ViewResult;

            // Assert
            var userRepoDetail = (GitHubRepoViewModel)result.ViewData.Model;
            Assert.AreEqual(userRepoDetail.IsFavoriteRepo,false);
        }

        [TestMethod]
        public void ListFavoritiesReposAction_ReturnsRepositoriesList()
        {
            // Arrange
            var mockGitHubService = new Mock<IGitHubRepoService>();

            HomeController controller = new HomeController(mockGitHubService.Object);

            // create a list of repositories to return 
            var userRepositories = GitHubRepoRepositoryMockHelper.GenerateFakeRepos("fakeUserName");

            mockGitHubService.Setup(s => s.GetAllFavorities()).Returns(userRepositories);

            // Act
            var result = controller.ListFavoritiesRepos() as ViewResult;

            // Assert
            var userRepoList = (IEnumerable<GitHubRepoViewModel>)result.ViewData.Model;
            Assert.IsTrue(userRepoList.Count() == userRepositories.Count());
        }

        [TestMethod]
        public void ListFavoritiesReposAction_ReturnsEmptyRepositoryList()
        {
            // Arrange
            var mockGitHubService = new Mock<IGitHubRepoService>();

            HomeController controller = new HomeController(mockGitHubService.Object);

            mockGitHubService.Setup(s => s.GetAllFavorities()).Returns(new List<GitHubRepo>());

            // Act
            var result = controller.ListFavoritiesRepos() as ViewResult;

            // Assert
            var userRepoList = (IEnumerable<GitHubRepoViewModel>)result.ViewData.Model;
            Assert.IsTrue(userRepoList.Count() == 0);
        }

        [TestMethod]
        public void ListFavoritiesReposAction_ReturnsNullRepositoryList()
        {
            // Arrange
            var mockGitHubService = new Mock<IGitHubRepoService>();

            HomeController controller = new HomeController(mockGitHubService.Object);

            List<GitHubRepo> favRepolist = null;

            mockGitHubService.Setup(s => s.GetAllFavorities()).Returns(favRepolist);

            // Act
            var result = controller.ListFavoritiesRepos() as ViewResult;

            // Assert
            var userRepoList = (IEnumerable<GitHubRepoViewModel>)result.ViewData.Model;
            Assert.IsNull(userRepoList);
        }

        [TestMethod]
        public void SearchAction_ValidRepoName_ReturnsRepositoriesList()
        {
            // Arrange
            var mockGitHubService = new Mock<IGitHubRepoService>();

            HomeController controller = new HomeController(mockGitHubService.Object);
            var fakeRepoName = "fakeRepoName";

            // create a list of repositories to return 
            var userRepositories = GitHubRepoRepositoryMockHelper.GenerateFakeRepos(fakeRepoName);

            mockGitHubService.Setup(s => s.SearchByRepoName(fakeRepoName)).Returns(userRepositories);

            // Act
            var result = controller.Search(fakeRepoName) as ViewResult;

            // Assert
            var userRepoList = (IEnumerable<GitHubRepoViewModel>)result.ViewData.Model;
            Assert.IsTrue(userRepoList.Count() > 0);
        }

        [TestMethod]
        public void SearchAction_InvalidRepoName_ReturnsRepositoriesList()
        {
            // Arrange
            var mockGitHubService = new Mock<IGitHubRepoService>();

            HomeController controller = new HomeController(mockGitHubService.Object);
            string fakeRepoName = null;
            IEnumerable<GitHubRepo> userRepositories = null;

            mockGitHubService.Setup(s => s.SearchByRepoName(fakeRepoName)).Returns(userRepositories);

            // Act
            var result = controller.Search(fakeRepoName) as ViewResult;

            // Assert
            var userRepoList = (IEnumerable<GitHubRepoViewModel>)result.ViewData.Model;
            Assert.IsNull(userRepoList);
        }

        [TestMethod]
        public void MarkAsFavoriteAction_MarkFavorite_ReturnsView()
        {
            // Arrange
            var mockGitHubService = new Mock<IGitHubRepoService>();

            HomeController controller = new HomeController(mockGitHubService.Object);

            // create fake repo
            GitHubRepo repo = GitHubRepoRepositoryMockHelper.GetRepoByName("ownerFake1", "repoNameFake1");

            // convert to view model
            GitHubRepoViewModel repoVm = GitHubRepoRepositoryMockHelper.MapToViewModel(repo, null);
            string IsFavoriteRepo = "true";

            // Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.MarkAsFavorite(repoVm, IsFavoriteRepo);

            // Assert
            mockGitHubService.Verify(t => t.MarkAsFavorite(It.IsAny<GitHubRepo>()));
            Assert.IsTrue(result.RouteValues["action"].ToString()== "ListFavoritiesRepos");
        }

        [TestMethod]
        public void MarkAsFavoriteAction_UnMarkFavorite_ReturnsView()
        {
            // Arrange
            var mockGitHubService = new Mock<IGitHubRepoService>();

            HomeController controller = new HomeController(mockGitHubService.Object);

            // create fake repo
            GitHubRepo repo = GitHubRepoRepositoryMockHelper.GetRepoByName("ownerFake1", "repoNameFake1");

            // convert to view model
            GitHubRepoViewModel repoVm = GitHubRepoRepositoryMockHelper.MapToViewModel(repo, null);
            string IsFavoriteRepo = "false";

            // Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.MarkAsFavorite(repoVm, IsFavoriteRepo);

            // Assert
            mockGitHubService.Verify(t => t.UnMarkAsFavorite(It.IsAny<GitHubRepo>()));

            Assert.IsTrue(result.RouteValues["action"].ToString() == "ListFavoritiesRepos");
        }

        

    }
}
