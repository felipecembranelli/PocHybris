using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubSample.Model
{
    public class Owner
    {
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
        public string login { get; set; }
        public string url { get; set; }
        public string gravatar_id { get; set; }
        public int id { get; set; }
    }
}
