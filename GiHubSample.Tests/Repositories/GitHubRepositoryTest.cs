using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using GitHubSample.Tests.Helpers;
using GitHubSample.Data.Repository;
using System.Collections.Generic;
using GitHubSample.Model;
using System.Data.Entity.Core.EntityClient;
using Effort.DataLoaders;
using System.Text;
using System.IO;
using Moq;
using System.Net;

namespace GitHubSample.Tests.Repositories
{
    [TestClass]
    public class GitHubRepositoryTest
    {
        EFContextHelper databaseContext;
        GitHubRepoRepository objRepo;

        [TestInitialize]
        public void Initialize()
        {

            EntityConnection connection = Effort.EntityConnectionFactory.CreateTransient("name=TestContext");

            databaseContext = new EFContextHelper(connection);

            // Seed in-memory database with some data
            var listRepos = MockHelper.GenerateFakeRepos("felipecembranelli");
            databaseContext.GitHubRepo.AddRange(listRepos);
            databaseContext.SaveChanges();

            // create repository
            objRepo = new GitHubRepoRepository(new DatabaseFactoryHelper(databaseContext));

        }

        [TestMethod]
        public void GitHubRepoRepository_GetAll_ReturnsAllReposFromRepository()
        {
            //Act
            var result = ((List<GitHubRepo>)objRepo.GetAll());

            //Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(25, result.Count);
            Assert.AreEqual("Repository 1", result[0].Name);
            Assert.AreEqual("Repository 2", result[1].Name);
            Assert.AreEqual("Repository 3", result[2].Name);
        }

        [TestMethod]
        public void GitHubRepoRepository_UnMarkAsFavorite_RemoveEntityFromRepository()
        {
            //Arrange
            var repo = this.objRepo.Get(r => r.Id==1);

            var favoriteRepoBefore = objRepo.IsFavoriteRepo(repo.Id);

            //Act
            objRepo.UnMarkAsFavorite(repo);
            databaseContext.SaveChanges();

            var favoriteRepoAfter = objRepo.IsFavoriteRepo(repo.Id);

            //Assert
            Assert.IsFalse(favoriteRepoAfter);
        }

        [TestMethod]
        public void GitHubRepoRepository_IsFavoriteRepo_ReturnsTrue()
        {
            //Arrange
            var repo = this.objRepo.Get(r => r.Id == 1);

            //Act
            var result = objRepo.IsFavoriteRepo(repo.GitHubRepoId);


            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GitHubRepoRepository_MarkAsFavorite_RemoveEntityFromRepository()
        {
            //Arrange
            var repo = MockHelper.GetRepoByName("newOwner","newRepo");
            var expectedNumberOfFavorities = ((List<GitHubRepo>)objRepo.GetAll()).Count;

            //Act
            var result = objRepo.AddandReturn(repo);
            databaseContext.SaveChanges();
            var actualNumberOfFavorities = ((List<GitHubRepo>)objRepo.GetAll()).Count;

            //Assert
            Assert.AreEqual(expectedNumberOfFavorities + 1, actualNumberOfFavorities);
        }

        //[TestMethod]
        //public void Create_should_create_request_and_respond_with_stream()
        //{
        //    // arrange
        //    var expected = "response content";
        //    var expectedBytes = Encoding.UTF8.GetBytes(expected);
        //    var responseStream = new MemoryStream();
        //    responseStream.Write(expectedBytes, 0, expectedBytes.Length);
        //    responseStream.Seek(0, SeekOrigin.Begin);

        //    var response = new Mock<HttpWebResponse>();
        //    response.Setup(c => c.GetResponseStream()).Returns(responseStream);

        //    var request = new Mock<HttpWebRequest>();
        //    request.Setup(c => c.GetResponse()).Returns(response.Object);

        //    var factory = new Mock<IHttpWebRequestFactory>();
        //    factory.Setup(c => c.Create(It.IsAny<string>()))
        //        .Returns(request.Object);

        //    // act
        //    var actualRequest = factory.Object.Create("http://www.google.com");
        //    actualRequest.Method = WebRequestMethods.Http.Get;

        //    string actual;

        //    using (var httpWebResponse = (HttpWebResponse)actualRequest.GetResponse())
        //    {
        //        using (var streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
        //        {
        //            actual = streamReader.ReadToEnd();
        //        }
        //    }


        //    // assert
        //    actual.Should().Be(expected);
        //}

        //public interface IHttpWebRequestFactory
        //{
        //    HttpWebRequest Create(string uri);
        //}


    }
}
