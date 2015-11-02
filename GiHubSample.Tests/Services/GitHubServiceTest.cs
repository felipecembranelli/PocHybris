using Microsoft.VisualStudio.TestTools.UnitTesting;
using GitHubSample.Data.Infrastructure;
using Moq;
using GitHubSample.Data.Repository;
using GitHubSample.Services;
using GitHubSample.Model;
using System.Collections.Generic;
using GitHubSample.Tests.Helpers;

namespace GitHubSample.Tests.Services
{
    [TestClass]
    public class GitHubServiceTest
    {
        [TestMethod]
        public void GetAllFavoritiesService_ReturnsRepositoriesList()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            // create a list of repositories to return 
            var userRepositories = (List<GitHubRepo>)MockHelper.GenerateFakeRepos("fakeUserName");

            mockGitHubRepoRepository.Setup(s => s.GetAll()).Returns(userRepositories);

            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = (List<GitHubRepo>)svc.GetAllFavorities();

            // Assert
            Assert.IsTrue(result.Count == userRepositories.Count);
        }

        [TestMethod]
        public void GetAllFavoritiesService_ReturnsEmptyRepositoryList()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockGitHubRepoRepository.Setup(s => s.GetAll()).Returns(new List<GitHubRepo>());

            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = (List<GitHubRepo>)svc.GetAllFavorities();

            // Assert
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void GetAllFavoritiesService_ReturnsNullRepositoryList()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            List<GitHubRepo> favRepolist = null;

            mockGitHubRepoRepository.Setup(s => s.GetAll()).Returns(favRepolist);

            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = (List<GitHubRepo>)svc.GetAllFavorities();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void MarkAsFavoriteService_PutValidRepoInRepository()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            // create new repo fake
            var repo = MockHelper.GetRepoByName("owner1", "repoNameFake1");

            int newId = 26;
            mockGitHubRepoRepository.Setup(s => s.AddandReturn(repo)).Returns((GitHubRepo g) => 
            {
                g.Id = newId;
                return g;
            });

            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            svc.MarkAsFavorite(repo);

            // Assert
            Assert.AreEqual(newId, repo.Id);
            mockUnitOfWork.Verify(m=>m.Commit(), Times.Once);
        }

        [TestMethod]
        public void MarkAsFavoriteService_PutInValidRepoInRepository()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            // create new repo fake
            GitHubRepo repo = null;

            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            var repoReturned = svc.MarkAsFavorite(repo);

            // Assert
            Assert.IsNull(repoReturned);
            mockUnitOfWork.Verify(m => m.Commit(), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void MarkAsFavoriteService_ReturnsException()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            // create new repo fake
            GitHubRepo repo = new GitHubRepo();

            mockGitHubRepoRepository.Setup(s => s.AddandReturn(repo)).Throws(new System.Exception("Fake exception"));

            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            var repoReturned = svc.MarkAsFavorite(repo);

            // Assert
            // automatic
        }

        [TestMethod]
        public void UnMarkAsFavoriteService_ValidRepoInRepository()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            // create new repo fake
            var repo = MockHelper.GetRepoByName("owner1", "repoNameFake1");

            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            svc.UnMarkAsFavorite(repo);

            // Assert
            mockUnitOfWork.Verify(m => m.Commit(), Times.Once);
        }

        [TestMethod]
        public void UnMarkAsFavoriteService_InvalidRepoInRepository()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            // create invalid repo
            GitHubRepo repo = null;

            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            svc.UnMarkAsFavorite(repo);

            // Assert
            mockUnitOfWork.Verify(m => m.Commit(), Times.Never);
        }

        [TestMethod]
        public void IsFavoriteRepoService_ValidRepo_Favorite_ReturnsTrue()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            // create new repo fake
            var repo = MockHelper.GetRepoByName("owner1", "repoNameFake1");

            mockGitHubRepoRepository.Setup(s => s.IsFavoriteRepo(repo.Id)).Returns(true);

            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = svc.IsFavoriteRepo(repo.Id);

            // Assert
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void IsFavoriteRepoService_ValidRepo_NotFavorite_ReturnsFalse()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            // create new repo fake
            var repo = MockHelper.GetRepoByName("owner1", "repoNameFake1");

            mockGitHubRepoRepository.Setup(s => s.IsFavoriteRepo(repo.Id)).Returns(false);

            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = svc.IsFavoriteRepo(repo.Id);

            // Assert
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void IsFavoriteRepoService_InvalidRepo_ReturnsFalse()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            // create invalid repo
            int repo = -1;

            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = svc.IsFavoriteRepo(repo);

            // Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void IsFavoriteRepoService_ReturnsException()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            int repo = -1;

            // force exception
            mockGitHubRepoRepository.Setup(s => s.IsFavoriteRepo(repo)).Throws(new System.Exception("Fake Exception"));

            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = svc.IsFavoriteRepo(repo);

            // Assert
            // automatic
        }

        [TestMethod]
        public void GetUserRepositoriesService_ReturnsRepositoriesList()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            // create a list of repositories to return 
            var owner = "fakeUserName";
            var userRepositories = (List<GitHubRepo>)MockHelper.GenerateFakeRepos(owner);

            mockGitHubRepoRepository.Setup(s => s.GetUserRepositories(owner)).Returns(userRepositories);

            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = (List<GitHubRepo>)svc.GetUserRepositories(owner);

            // Assert
            Assert.IsTrue(result.Count == userRepositories.Count);
        }

        [TestMethod]
        public void GetUserRepositoriesService_ifUserNameIsNull_ReturnsNull()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            // create a list of repositories to return 
            string owner = null;
            List<GitHubRepo> userRepositories = null;

            mockGitHubRepoRepository.Setup(s => s.GetUserRepositories(owner)).Returns(userRepositories);

            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = (List<GitHubRepo>)svc.GetUserRepositories(null);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void GetUserRepositoriesService_ReturnsException()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            // create a list of repositories to return 
            string owner = null;

            mockGitHubRepoRepository.Setup(s => s.GetUserRepositories(owner)).Throws(new System.Exception("Fake exception"));

            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = (List<GitHubRepo>)svc.GetUserRepositories(null);

            // Assert
            // automatic
        }

        [TestMethod]
        public void SearchByRepoNameService_ReturnsRepositoriesList()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            // create a list of repositories to return 
            var owner = "fakeUserName";
            string query = "repoXPTO";
            var userRepositories = (List<GitHubRepo>)MockHelper.GenerateFakeRepos(owner);

            mockGitHubRepoRepository.Setup(s => s.SearchRepositories(query)).Returns(userRepositories);

            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = (List<GitHubRepo>)svc.SearchByRepoName(query);

            // Assert
            Assert.IsTrue(result.Count == userRepositories.Count);
        }

        [TestMethod]
        public void SearchByRepoNameService_ifUserNameIsNull_ReturnsNull()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            // create a list of repositories to return 
            string query = null;
            List<GitHubRepo> userRepositories = null;

            mockGitHubRepoRepository.Setup(s => s.SearchRepositories(query)).Returns(userRepositories);

            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = (List<GitHubRepo>)svc.SearchByRepoName(query);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void SearchByRepoNameService_ReturnsException()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            // create a list of repositories to return 
            string query = null;

            mockGitHubRepoRepository.Setup(s => s.SearchRepositories(query)).Throws(new System.Exception("Fake exception"));

            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = (List<GitHubRepo>)svc.SearchByRepoName(query);

            // Assert
            // automatic
        }

        [TestMethod]
        public void GetRepoByNameService_ReturnsRepo()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            // create a fake repo to return 
            var owner = "fakeOwner";
            var repoName = "fakeRepoName";
            var fakeRepo = (GitHubRepo)MockHelper.GetRepoByName(owner, repoName);

            mockGitHubRepoRepository.Setup(s => s.GetRepoByName(owner, repoName)).Returns(fakeRepo);

            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = svc.GetRepoByName(owner, repoName);

            // Assert
            Assert.AreEqual(result.Id , fakeRepo.Id);
        }

        [TestMethod]
        public void GetRepoByNameService_ifInvalidName_ReturnsNull()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            // create fake repo to return 
            GitHubRepo fakeRepo = null;

            mockGitHubRepoRepository.Setup(s => s.GetRepoByName(null, null)).Returns(fakeRepo);

            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = svc.GetRepoByName(null, null);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void GetRepoByNameService_ReturnsException()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockGitHubRepoRepository.Setup(s => s.GetRepoByName(null, null)).Throws(new System.Exception("Fake exception"));
            
            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = svc.GetRepoByName(null, null);

            // Assert
            // automatic
        }

        [TestMethod]
        public void GetRepoContributorsService_ReturnsRepositoriesList()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            // create a list of contribs to return 
            var owner = "fakeUserName";
            string repoName = "repoXPTO";
            var contribList = (List<GitHubUserDTO>)MockHelper.GetRepoContributors(owner, repoName);

            mockGitHubRepoRepository.Setup(s => s.GetRepoContributors(owner, repoName)).Returns(contribList);

            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = (List<GitHubUserDTO>)svc.GetRepoContributors(owner, repoName);

            // Assert
            Assert.IsTrue(result.Count == contribList.Count);
        }

        [TestMethod]
        public void GetRepoContributorsService_ifRepoIsInvalid_ReturnsNull()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            // create a list of contribs to return 
            var owner = "fakeUserName";
            string repoName = null;
            IEnumerable<GitHubUserDTO> contribList = null;

            mockGitHubRepoRepository.Setup(s => s.GetRepoContributors(owner, repoName)).Returns(contribList);

            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = (List<GitHubUserDTO>)svc.GetRepoContributors(owner, repoName);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void GetRepoContributorsService_ReturnsException()
        {
            // Arrange
            var mockGitHubRepoRepository = new Mock<IGitHubRepoRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            // create a list of contribs to return 
            var owner = "fakeUserName";
            string repoName = null;

            mockGitHubRepoRepository.Setup(s => s.GetRepoContributors(owner, repoName)).Throws(new System.Exception("Fake exception"));


            var svc = new GitHubRepoService(mockGitHubRepoRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = (List<GitHubUserDTO>)svc.GetRepoContributors(owner, repoName);

            // Assert
            // automatic
        }

        //[TestMethod]
        //public void GetUserRepositories()
        //{
        //    //// arrange
        //    //GitHubSample.Data.Repository.GitHubRepoRepository repo = new GitHubSample.Data.Repository.GitHubRepoRepository();
        //    //IUnitOfWork unitOfWork = new UnitOfWork(new DatabaseFactory());
        //    //GitHubSample.Services.GitHubRepoService svc = new GitHubSample.Services.GitHubRepoService(repo, unitOfWork);

        //    //// act
        //    //var ret = svc.GetUserRepositories();


        //    //// assert
        //    //Assert.IsNotNull(ret);

        //}

        //[TestMethod]
        //public void SearchRepositories()
        //{
        //    //// arrange
        //    //string repositoryName = "MerakiCaptivePortal";
        //    //int expected = 1;
        //    //GitHubSample.Data.Repository.GitHubRepoRepository repo = new GitHubSample.Data.Repository.GitHubRepoRepository();
        //    //IUnitOfWork unitOfWork = new UnitOfWork(new DatabaseFactory());
        //    //GitHubSample.Services.GitHubRepoService svc = new GitHubSample.Services.GitHubRepoService(repo, unitOfWork);

        //    //// act
        //    //var ret = svc.SearchByRepoName(repositoryName);

        //    ////assert
        //    //Assert.AreEqual(expected, ret.Items.Length, "Repositório não encontrado.");

        //}
    }
}
