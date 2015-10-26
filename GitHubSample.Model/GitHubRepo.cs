using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubSample.Model
{
    public class GitHubRepo
    {
        public string description { get; set; }
        public bool has_wiki { get; set; }
        public string svn_url { get; set; }
        public int open_issues { get; set; }
        public string language { get; set; }
        public int watchers { get; set; }
        public bool fork { get; set; }
        public string homepage { get; set; }
        public string git_url { get; set; }
        public string clone_url { get; set; }
        public string pushed_at { get; set; }
        public int size { get; set; }
        public bool @private { get; set; }
        public string created_at { get; set; }
        public string html_url { get; set; }
        public Owner owner { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public object mirror_url { get; set; }
        public bool has_downloads { get; set; }
        public bool has_issues { get; set; }
        public string ssh_url { get; set; }
        public string updated_at { get; set; }
        public int id { get; set; }
        public int forks { get; set; }
    }

    public class GitHubRepoJson
    {
        [JsonProperty("total_count")]
        public string TotalCount { get; set; }

        [JsonProperty("incomplete_results")]
        public bool IncompleteResults { get; set; }

        [JsonProperty("items")]
        public GitHubRepo[] Items { get; set; }
    }
}
