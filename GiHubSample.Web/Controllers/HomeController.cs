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

            // Map to ViewModel
            //List<GitHubRepoViewModel> userRepositoriesVM = new List<GitHubRepoViewModel>();

            //foreach (var r in userRepositories)
            //{
            //    GitHubRepoViewModel vm = new GitHubRepoViewModel();
            //    vm.Name = r.Name;
            //    vm.Description = r.Description;
            //    vm.GitHubRepoId = r.Id;
            //    vm.OwnerLogin = r.OwnerName;

            //    userRepositoriesVM.Add(vm);
            //}

            ViewBag.PageTitle = "My Repositories";

            return View("ListRepositories", EntityMapper.MapListToViewModelList(userRepositories));
        }

        public ActionResult Detail(int gitHubRepoId, string repoName, string owner)
        {
            var repoDetail = this.gitHubservice.GetRepoByName(owner,repoName);

            // get repo contributors
            var contributors = this.gitHubservice.GetRepoContributors(owner, repoName);

            // create view model
            //RepositoryViewModel vm = new RepositoryViewModel();
            //vm.name = repoDetail.name;
            //vm.description = repoDetail.description;
            //vm.contributors = contributors.ToList();


            // Verify if it is already marked as favorite
            var favorityFlag = this.gitHubservice.IsFavoriteRepo(gitHubRepoId);

            //// MOCK
            //string desc = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknow";
            //string avatar_url = "https://avatars1.githubusercontent.com/u/167455?v=3&s=400";
            //string contrib_avatar_url = "https://avatars0.githubusercontent.com/u/954031?v=3&s=400";

            var vm = EntityMapper.MapToViewModel(repoDetail, contributors);
            //vm.GitHubRepoId = gitHubRepoId;
            vm.IsFavoriteRepo = favorityFlag;

            //vm.Name = repoDetail.Name;
            //vm.Description = repoDetail.Description;
            //vm.Language = repoDetail.Language;
            //vm.Updated_at = repoDetail.UpdatedAt;
            //vm.OwnerLogin = repoDetail.OwnerName;
            //vm.OwnerAvatarUrl = repoDetail.OwnerAvatarUrl;

            //vm.Contributors = contributors.ToList();

            //vm.Contributors = new List<Owner>() { new Owner() {Login = "fulano", AvatarUrl = contrib_avatar_url },
            //                                      new Owner() {Login = "beltrano", AvatarUrl = contrib_avatar_url }
            //                                    };

            return View("RepoDetail", vm);
        }

        [HttpPost]
        public ActionResult Search(string searchString)
        {
            var repositories = this.gitHubservice.SearchByRepoName(searchString);

            ViewBag.PageTitle = "Repositories";

            return View("ListRepositories", EntityMapper.MapListToViewModelList(repositories));
        }

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

        public ActionResult ListFavoritiesRepos()
        {
            var userRepositories = this.gitHubservice.GetAllFavorities();

            //// Map to ViewModel
            //List<GitHubRepoViewModel> userRepositoriesVM = new List<GitHubRepoViewModel>();

            //foreach (var r in userRepositories)
            //{
            //    GitHubRepoViewModel vm = new GitHubRepoViewModel();
            //    vm.Name = r.Name;
            //    vm.Description = r.Description;
            //    vm.GitHubRepoId = r.Id;

            //    userRepositoriesVM.Add(vm);
            //}

            ViewBag.PageTitle = "Favorites Repositories";

            return View("ListRepositories", this.MapListToViewModelList(userRepositories));
        }

     
    }
}