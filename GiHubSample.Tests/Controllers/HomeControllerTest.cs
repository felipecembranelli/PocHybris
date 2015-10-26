using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GiHubSample.Web;
using GiHubSample.Web.Controllers;
using GitHubSample.Services.IServices;
using GitHubSample.Data.Infrastructure;

namespace GiHubSample.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            GitHubSample.Data.Repository.GitHubRepoRepository repo = new GitHubSample.Data.Repository.GitHubRepoRepository();
            IUnitOfWork unitOfWork = new UnitOfWork(new DatabaseFactory());
            IGitHubRepoService svc = new GitHubSample.Services.GitHubRepoService(repo, unitOfWork);
            HomeController controller = new HomeController(svc);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
