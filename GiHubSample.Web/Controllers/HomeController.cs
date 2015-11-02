using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GitHubSample.Services;
using GitHubSample.Services.IServices;
using GiHubSample.Web.ViewModels;
using GitHubSample.Model;
using GiHubSample.Web.Mapper;
using GiHubSample.Web.App_Start;

namespace GiHubSample.Web.Controllers
{
    [HandleError(View = "Error")]
    public class HomeController :  BaseController
    {
        private readonly IGitHubRepoService gitHubservice;
        private string defaultUserRepository;

        public HomeController(IGitHubRepoService service)
        {
            this.gitHubservice = service;
            defaultUserRepository = Bootstrapper.GetDefaultGitHubRepo();
        }

        public string DefaultUserRepository
        {
            get
            {
                return defaultUserRepository;
            }
            set
            {
                defaultUserRepository = value;
            }
        }

        /// <summary>
        /// Returns list of "my repositories", based on web.config "defaultUserRepository" configuation.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (this.defaultUserRepository == string.Empty)
                throw new ArgumentNullException("GitHub default repository not configured.");

            var userRepositories = this.gitHubservice.GetUserRepositories(this.defaultUserRepository);

            ViewBag.PageTitle = "My Repositories: " + this.DefaultUserRepository;

            return View("ListRepositories", EntityMapper.MapListToViewModelList(userRepositories));
        }

        /// <summary>
        /// Returns github repo details, calling online github api
        /// </summary>
        /// <param name="gitHubRepoId"></param>
        /// <param name="repoName"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public ActionResult Detail(int gitHubRepoId, string repoName, string owner)
        {
            var repoDetail = this.gitHubservice.GetRepoByName(owner,repoName);

            // get repo contributors
            var contributors = this.gitHubservice.GetRepoContributors(owner, repoName);

            // Verify if it is already marked as favorite
            var favorityFlag = this.gitHubservice.IsFavoriteRepo(gitHubRepoId);

            var vm = EntityMapper.MapToViewModel(repoDetail, contributors);

            if (vm!=null)
                vm.IsFavoriteRepo = favorityFlag;

            return View("RepoDetail", vm);
        }

        /// <summary>
        /// Returns list of github repositories that matchs the search criteria
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Search(string searchString)
        {
            var repositories = this.gitHubservice.SearchByRepoName(searchString);

            ViewBag.PageTitle = "Repositories";

            return View("ListRepositories", EntityMapper.MapListToViewModelList(repositories));
        }

        /// <summary>
        /// Mark a repository as favority, salving into local database
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="IsFavoriteRepo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MarkAsFavorite(GitHubRepoViewModel repo, string IsFavoriteRepo)
        {

            if (IsFavoriteRepo == "true")
            {
                this.gitHubservice.MarkAsFavorite(EntityMapper.MapToModel(repo));
            }
            else
                this.gitHubservice.UnMarkAsFavorite(EntityMapper.MapToModel(repo));

            return RedirectToAction("ListFavoritiesRepos");
        }

        /// <summary>
        /// List all local saved repositories (favorities repositories)
        /// </summary>
        /// <returns></returns>
        public ActionResult ListFavoritiesRepos()
        {
            var userRepositories = this.gitHubservice.GetAllFavorities();

            ViewBag.PageTitle = "Favorites Repositories";

            return View("ListRepositories", EntityMapper.MapListToViewModelList(userRepositories));
        }
    }
}