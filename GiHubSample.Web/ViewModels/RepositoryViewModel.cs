using GitHubSample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiHubSample.Web.ViewModels
{
    public class GitHubRepoViewModel
    {
        private string _description;

        public int GitHubRepoId { get; set; }
        public string Description
        {
            get { return _description; }
            set
            {
                if (value != null)
                {
                    if (value.Length > 30)
                    {
                        _description = value.Substring(0, 30) + "...";
                    }
                }
            }
        }
        public string Language { get; set; }
        public int Watchers { get; set; }
        public bool Fork { get; set; }
        public string Homepage { get; set; }
        public string Git_url { get; set; }
        public string Clone_url { get; set; }
        public string Pushed_at { get; set; }
        public int Size { get; set; }
        public string Ccreated_at { get; set; }
        public string Updated_at { get; set; }
        public string Html_url { get; set; }
        public string Name { get; set; }
        public string OwnerLogin { get; set; }
        public string OwnerAvatarUrl { get; set; }
        public List<GitHubUserDTO> Contributors { get; set; }
        public bool IsFavoriteRepo { get; set; }
    }
}
