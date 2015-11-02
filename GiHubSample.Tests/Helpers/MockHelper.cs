using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using GitHubSample.Model;
using GitHubSample.Data.Infrastructure;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GitHubSample.Data.GitHubAPI;
using GiHubSample.Web.ViewModels;

namespace GitHubSample.Tests.Helpers
{
    public static class MockHelper
    {

        #region Mock Helper

        public static IEnumerable<GitHubRepo> GenerateFakeRepos(string userName)
        {
            var repoList = new List<GitHubRepo>();

            string avatarUrl = "https://avatars0.githubusercontent.com/u/954031?v=3&s=400";

            string desc = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknow";

            List<GitHubRepoDTO> userRepositoriesDTO = new List<GitHubRepoDTO>() {
                    new GitHubRepoDTO() { name = "Repository 1", description=desc, id = 1, owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 2", description=desc, id=2 , owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 3", description=desc, id=3 , owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 4", description=desc, id=4 , owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 5", description=desc, id=5 , owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 6", description=desc, id = 6, owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 7", description=desc, id=7 , owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 8", description=desc, id=8 , owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 9", description=desc, id=9 , owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 10", description=desc, id=10 , owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 11", description=desc, id = 11, owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 12", description=desc, id=12 , owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 13", description=desc, id=13 , owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 14", description=desc, id=14 , owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 15", description=desc, id=15 , owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 16", description=desc, id = 16, owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 17", description=desc, id=17 , owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 18", description=desc, id=18 , owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 19", description=desc, id=19 , owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 20", description=desc, id=20 , owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 21", description=desc, id = 21, owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 22", description=desc, id=22 , owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 23", description=desc, id=23 , owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 24", description=desc, id=24 , owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 25", description=desc, id=25 , owner = new GitHubUserDTO() { Login=userName, AvatarUrl= avatarUrl } }
            };

            foreach (var repo in userRepositoriesDTO)
            {
                repoList.Add(MapDtoToModel(repo));
            }

            return repoList;
        }

        public static GitHubRepo GetRepoByName(string owner, string repoName)
        {
            if ((owner == null) || (repoName == null))
                return null;

            // MOCK
            string desc = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknow";
            string avatar_url = "https://avatars1.githubusercontent.com/u/167455?v=3&s=400";

            GitHubRepo repoModel = new GitHubRepo();
            repoModel.Name = repoName;
            repoModel.Description = desc;
            repoModel.Language = "javascript";
            repoModel.UpdatedAt = new DateTime(2015, 1, 25);
            repoModel.OwnerName = owner;
            repoModel.OwnerAvatarUrl = avatar_url;
            repoModel.GitHubRepoId = 1;

            return repoModel;
        }

        public static IEnumerable<GitHubUserDTO> GetRepoContributors(string owner, string repoName)
        {
            if ((owner == null) || (repoName == null))
                return null;

            var contribList = new List<GitHubUserDTO>();

            string contrib_avatar_url = "https://avatars0.githubusercontent.com/u/954031?v=3&s=400";

            contribList.Add(new GitHubUserDTO() { Login = owner, AvatarUrl = contrib_avatar_url });
            contribList.Add(new GitHubUserDTO() { Login = owner, AvatarUrl = contrib_avatar_url });
            contribList.Add(new GitHubUserDTO() { Login = owner, AvatarUrl = contrib_avatar_url });
            contribList.Add(new GitHubUserDTO() { Login = owner, AvatarUrl = contrib_avatar_url });
            contribList.Add(new GitHubUserDTO() { Login = owner, AvatarUrl = contrib_avatar_url });

            return contribList;
        }

        public static GitHubSample.Model.GitHubRepo MapDtoToModel(GitHubRepoDTO dto)
        {
            GitHubSample.Model.GitHubRepo repoModel = new GitHubRepo();
            repoModel.Name = dto.name;
            repoModel.Description = dto.description;
            repoModel.GitHubRepoId = dto.id;
            repoModel.Language = dto.language;
            repoModel.OwnerAvatarUrl = dto.owner.AvatarUrl;
            repoModel.OwnerName = dto.owner.Login;
            repoModel.UpdatedAt = ConvertToDateTime(dto.updated_at);

            return repoModel;
        }

        public static System.DateTime? ConvertToDateTime(string value)
        {
            DateTime dt;

            try
            {
                dt = DateTime.ParseExact(value, "yyyy-MM-ddTHH:mm:ssZ", null);
            }
            catch (Exception)
            {
                return null;
            }

            return dt;
        }

        public static GitHubRepoViewModel MapToViewModel(GitHubRepo repo, IEnumerable<GitHubUserDTO> contributors)
        {
            if (repo == null)
                return null;

            GitHubRepoViewModel vm = new GitHubRepoViewModel();
            vm.Name = repo.Name;
            vm.Description = repo.Description;
            vm.Language = repo.Language;
            vm.Updated_at = repo.UpdatedAt.ToString();
            vm.OwnerLogin = repo.OwnerName;
            vm.OwnerAvatarUrl = repo.OwnerAvatarUrl;
            vm.GitHubRepoId = repo.GitHubRepoId;
            if (contributors != null)
                vm.Contributors = contributors.ToList();

            return vm;
        }

        #endregion
    }
}