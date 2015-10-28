﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GitHubSample.Data.GitHubAPI
{
    public static class GitHubApiWrapper
    {
        //const string API_SERVICE_END_POINT = "https://api.github.com";

        //public string GetWordFromDictionary(long index)
        //{
        //    try
        //    {
        //        string url = string.Format(API_SERVICE_END_POINT, index);

        //        return CallRestService(url);

        //    }
        //    catch (WebException ex)
        //    {
        //        if (ex.Status == WebExceptionStatus.ProtocolError)
        //            return null;
        //        else
        //            throw ex;
        //    }
        //}

        public static string CallRestService(string url)
        {
            return CallRestServiceWebRequest(url);
        }

        public static string CallRestServiceWebClient(string url)
        {
            var jsonString = string.Empty;

            try
            {
                WebClient wc = new WebClient();

                wc.Headers["User-Agent"] = "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
                                               "(compatible; MSIE 6.0; Windows NT 5.1; " +
                                               ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";

                jsonString = wc.DownloadString(url);

            }
            catch (Exception)
            {
                throw;
            }

            return jsonString;
        }


        public static string CallRestServiceWebRequest(string url)
        {
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = "GET";

            webrequest.UserAgent = "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
                                               "(compatible; MSIE 6.0; Windows NT 5.1; " +
                                               ".NET CLR 1.1.4322; .NET CLR 2.0.50727)"; 

            //webrequest.Headers.Add("User-Agent", "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
            //                                   "(compatible; MSIE 6.0; Windows NT 5.1; " +
            //                                   ".NET CLR 1.1.4322; .NET CLR 2.0.50727)");


            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();

            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");

            StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);

            string result = string.Empty;

            result = responseStream.ReadToEnd();

            webresponse.Close();

            return result;
        }



    }
}
