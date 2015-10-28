using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubSample.Model
{
    public class GitHubRepoDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string language { get; set; }
        public GitHubUserDTO owner { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class GitHubRepoJsonDTO
    {
        [JsonProperty("total_count")]
        public string TotalCount { get; set; }

        [JsonProperty("incomplete_results")]
        public bool IncompleteResults { get; set; }

        [JsonProperty("items")]
        public GitHubRepoDTO[] Items { get; set; }
    }
}
