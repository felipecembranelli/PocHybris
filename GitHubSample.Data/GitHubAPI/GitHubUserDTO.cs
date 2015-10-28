using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubSample.Model
{
    public class GitHubUserDTO
    {
        public int Id { get; set; }
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
        public string Login { get; set; }
        public string Url { get; set; }
        public string Gravatar_id { get; set; }
    }
}
