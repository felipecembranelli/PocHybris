using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GitHubSample.Services;
using GitHubSample.Services.IServices;

namespace GiHubSample.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGitHubRepoService gitHubservice;

        public HomeController(IGitHubRepoService service)
        {
            this.gitHubservice = service;
        }

        public ActionResult Index()
        {
            var userRepositories = this.gitHubservice.GetUserRepositories();

            return View(userRepositories);
        }

        public ActionResult Search()
        {
            var userRepositories = this.gitHubservice.SearchByRepoName("Meraki");

            return View("Index",userRepositories);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}