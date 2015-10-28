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

namespace GitHubSample.Data.Repository
{
    public class GitHubRepoRepositoryMock: Infrastructure.RepositoryBase<GitHubRepo>, IGitHubRepoRepository
    {

        public GitHubRepoRepositoryMock(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        
        #region EF wrapper

        public void UnMarkAsFavorite(GitHubRepo repository)
        {
            var entity = base.GetAll().Where(r => r.GitHubRepoId == repository.GitHubRepoId).FirstOrDefault();

            base.Delete(entity);

        }

        public bool IsFavoriteRepo(int gitHubRepoId)
        {
            var entity = base.GetAll().Where(r => r.GitHubRepoId == gitHubRepoId).FirstOrDefault();

            if (entity != null)
                return true;
            else
                return false;

        }

        #endregion

        #region GitHub api wrapper

        public IEnumerable<GitHubRepo> SearchRepositories(string query)
        {
            return this.GenerateFakeRepos();
        }

        public GitHubRepo GetRepoByName(string owner, string repoName)
        {
            // MOCK
            string desc = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknow";
            string avatar_url = "https://avatars1.githubusercontent.com/u/167455?v=3&s=400";

            GitHubRepo repoModel = new GitHubRepo();
            repoModel.Name = repoName;
            repoModel.Description = desc;
            repoModel.Language = "javascript";
            //model.UpdatedAt = "01/10/2015";
            repoModel.OwnerName = owner;
            repoModel.OwnerAvatarUrl = avatar_url;
            repoModel.GitHubRepoId = 1;

            return repoModel;
        }

        public IEnumerable<GitHubRepo> GetUserRepositories()
        {
            
            return this.GenerateFakeRepos();
        }

        public IEnumerable<GitHubUserDTO> GetRepoContributors(string owner, string repoName)
        {
            var contribList = new List<GitHubUserDTO>();

            string contrib_avatar_url = "https://avatars0.githubusercontent.com/u/954031?v=3&s=400";

            contribList.Add(new GitHubUserDTO() { Login = "contrib1", AvatarUrl = contrib_avatar_url });
            contribList.Add(new GitHubUserDTO() { Login = "contrib1", AvatarUrl = contrib_avatar_url });
            contribList.Add(new GitHubUserDTO() { Login = "contrib1", AvatarUrl = contrib_avatar_url });
            contribList.Add(new GitHubUserDTO() { Login = "contrib1", AvatarUrl = contrib_avatar_url });
            contribList.Add(new GitHubUserDTO() { Login = "contrib1", AvatarUrl = contrib_avatar_url });

            return contribList;
        }

        private GitHubSample.Model.GitHubRepo MapDtoToModel(GitHubRepoDTO dto)
        {
            GitHubSample.Model.GitHubRepo repoModel = new GitHubRepo();
            repoModel.Name = dto.name;
            repoModel.Description = dto.description;
            repoModel.GitHubRepoId = dto.id;
            repoModel.Language = dto.language;
            repoModel.OwnerAvatarUrl = dto.owner.AvatarUrl;
            repoModel.OwnerName = dto.owner.Login;
            //repoModel.UpdatedAt = new System.DateTime(repo.updated_at);

            return repoModel;
        }

        private IEnumerable<GitHubRepo> GenerateFakeRepos()
        {
            var repoList = new List<GitHubRepo>();

            string avatarUrl = "https://avatars0.githubusercontent.com/u/954031?v=3&s=400";

            string desc = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknow";

            List<GitHubRepoDTO> userRepositoriesDTO = new List<GitHubRepoDTO>() {
                    new GitHubRepoDTO() { name = "Repository 1", description=desc, id = 1, owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 2", description=desc, id=2 , owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 3", description=desc, id=3 , owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 4", description=desc, id=4 , owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 5", description=desc, id=5 , owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 6", description=desc, id = 6, owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 7", description=desc, id=7 , owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 8", description=desc, id=8 , owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 9", description=desc, id=9 , owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 10", description=desc, id=10 , owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 11", description=desc, id = 11, owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 12", description=desc, id=12 , owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 13", description=desc, id=13 , owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 14", description=desc, id=14 , owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 15", description=desc, id=15 , owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 16", description=desc, id = 16, owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 17", description=desc, id=17 , owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 18", description=desc, id=18 , owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 19", description=desc, id=19 , owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 20", description=desc, id=20 , owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 21", description=desc, id = 21, owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 22", description=desc, id=22 , owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 23", description=desc, id=23 , owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 24", description=desc, id=24 , owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } },
                    new GitHubRepoDTO() { name = "Repository 25", description=desc, id=25 , owner = new GitHubUserDTO() { Login="owner1", AvatarUrl= avatarUrl } }
            };

            foreach (var repo in userRepositoriesDTO)
            {
                repoList.Add(this.MapDtoToModel(repo));
            }

            return repoList;
        }

        #endregion
    }
}