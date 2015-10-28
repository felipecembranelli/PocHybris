﻿using GiHubSample.Web.ViewModels;
using GitHubSample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiHubSample.Web.Mapper
{
    static class EntityMapper
    {
        public static List<GitHubRepoViewModel> MapListToViewModelList(IEnumerable<GitHubRepo> repos)
        {
            List<GitHubRepoViewModel> userRepositoriesVM = new List<GitHubRepoViewModel>();

            foreach (var r in repos)
            {
                GitHubRepoViewModel vm = new GitHubRepoViewModel();
                vm.Name = r.Name;
                vm.Description = r.Description;
                vm.GitHubRepoId = r.GitHubRepoId;
                vm.OwnerLogin = r.OwnerName;

                userRepositoriesVM.Add(vm);
            }

            return userRepositoriesVM;
        }

        public static GitHubRepoViewModel MapToViewModel(GitHubRepo repo, IEnumerable<GitHubUserDTO> contributors)
        {
            GitHubRepoViewModel vm = new GitHubRepoViewModel();
            vm.Name = repo.Name;
            vm.Description = repo.Description;
            vm.Language = repo.Language;
            //vm.Updated_at = repoDetail.UpdatedAt;
            vm.OwnerLogin = repo.OwnerName;
            vm.OwnerAvatarUrl = repo.OwnerAvatarUrl;
            vm.GitHubRepoId = repo.GitHubRepoId;
            //vm.IsFavoriteRepo = favorityFlag;
            if (contributors != null)
                vm.Contributors = contributors.ToList();

            return vm;
        }

        public static GitHubRepo MapToModel(GitHubRepoViewModel vm)
        {
            GitHubRepo repoModel = new GitHubRepo();
            repoModel.Name = vm.Name;
            repoModel.Description = vm.Description;
            repoModel.Language = vm.Language;
            repoModel.OwnerAvatarUrl = vm.OwnerAvatarUrl;
            repoModel.OwnerName = vm.OwnerLogin;
            repoModel.GitHubRepoId = vm.GitHubRepoId;

            return repoModel;
        }
    }
}