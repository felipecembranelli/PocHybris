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
    public class GitHubRepoRepository: Infrastructure.RepositoryBase<GitHubRepo>, IGitHubRepoRepository
    {

        public GitHubRepoRepository(IDatabaseFactory databaseFactory)
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

            var repoList = new List<GitHubRepo>();

            try
            {
                string jsonString = GitHubApiWrapper.CallRestService(string.Format("https://api.github.com/search/repositories?q={0}", query));

                var jsonObjectDto = JsonConvert.DeserializeObject<GitHubRepoJsonDTO>(jsonString);

                foreach (var repo in jsonObjectDto.Items.ToList())
                {
                    repoList.Add(this.MapDtoToModel(repo));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return repoList;
        }

        public GitHubRepo GetRepoByName(string owner, string repoName)
        {
            var repoModel = new GitHubRepo();
            try
            {
                string json = GitHubApiWrapper.CallRestService(string.Format("https://api.github.com/repos/{0}/{1}", owner, repoName));

                var jsonDto = JsonConvert.DeserializeObject<GitHubRepoDTO>(json);

                repoModel = this.MapDtoToModel(jsonDto);
            }
            catch (Exception)
            {
                throw;
            }

            return repoModel;
        }

        public IEnumerable<GitHubRepo> GetUserRepositories()
        {
            var repoList = new List<GitHubRepo>();
            try
            {
                string json = GitHubApiWrapper.CallRestService("https://api.github.com/users/felipecembranelli/repos");

                var jsonDto = JsonConvert.DeserializeObject<List<GitHubRepoDTO>>(json);

                foreach (var repo in jsonDto.ToList())
                {
                    repoList.Add(this.MapDtoToModel(repo));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return repoList;
        }

        public IEnumerable<GitHubUserDTO> GetRepoContributors(string owner, string repoName)
        {
            var jsonObject = new List<GitHubUserDTO>();
            try
            {
                string json = GitHubApiWrapper.CallRestService(string.Format("https://api.github.com/repos/{0}/{1}/contributors", owner, repoName));

                jsonObject = JsonConvert.DeserializeObject<List<GitHubUserDTO>>(json);
            }
            catch (Exception)
            {
                throw;
            }

            return jsonObject;
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

        #endregion
    }
}