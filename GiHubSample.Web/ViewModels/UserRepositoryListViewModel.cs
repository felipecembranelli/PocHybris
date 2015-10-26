using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiHubSample.Web.ViewModels
{
    public class UserRepositoryListViewModel
    {
        public string description { get; set; }
        public string language { get; set; }
        public int watchers { get; set; }
        public bool fork { get; set; }
        public string homepage { get; set; }
        public string git_url { get; set; }
        public string clone_url { get; set; }
        public string pushed_at { get; set; }
        public int size { get; set; }
        public string created_at { get; set; }
        public string html_url { get; set; }
        public string name { get; set; }
    }
}
