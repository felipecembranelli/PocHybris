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

namespace GitHubSample.Data.Repository
{
    public class GitHubRepoRepository: IGitHubRepoRepository
    {

        //public async Task<string> GetMyRepositories()
        //{
        //    string ret = string.Empty;

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://api.github.com/users/felipecembranelli/repos");
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        // New code:
        //        HttpResponseMessage response = await client.GetAsync("https://api.github.com/users/felipecembranelli/repos");

        //        if (response.IsSuccessStatusCode)
        //        {
        //            GitHubRepo product = await response.Content.ReadAsAsync<GitHubRepo>();

        //            ret = product.Description;

        //            //return ret;
        //        }
        //    }

        //    return ret;
        //}

        //public List<GitHubRepo> GetUserRepositories()
        //{
        //    var jsonObject = new List<GitHubRepo>();
        //    try
        //    {
        //        WebClient wc = new WebClient();

        //        wc.Headers["User-Agent"] = "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
        //                                       "(compatible; MSIE 6.0; Windows NT 5.1; " +
        //                                       ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        //        string json = wc.DownloadString("https://api.github.com/users/felipecembranelli/repos");

        //        jsonObject = JsonConvert.DeserializeObject<List<GitHubRepo>>(json);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    return jsonObject;
        //}

        public GitHubRepoJson SearchRepositories(string query)
        {
            var jsonObject = new GitHubRepoJson();
            try
            {
                WebClient wc = new WebClient();

                wc.Headers["User-Agent"] = "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
                                               "(compatible; MSIE 6.0; Windows NT 5.1; " +
                                               ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";

                string json = wc.DownloadString(string.Format("https://api.github.com/search/repositories?q={0}",query));

                jsonObject = JsonConvert.DeserializeObject<GitHubRepoJson>(json);
            }
            catch (Exception)
            {
                throw;
            }

            return jsonObject;
        }

        public IEnumerable<GitHubRepo> GetUserRepositories()
        {
            var jsonObject = new List<GitHubRepo>();
            try
            {
                WebClient wc = new WebClient();

                wc.Headers["User-Agent"] = "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
                                               "(compatible; MSIE 6.0; Windows NT 5.1; " +
                                               ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";

                string json = wc.DownloadString("https://api.github.com/users/felipecembranelli/repos");

                jsonObject = JsonConvert.DeserializeObject<List<GitHubRepo>>(json);
            }
            catch (Exception)
            {
                throw;
            }

            return jsonObject;
        }
    }
}