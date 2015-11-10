using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PocHybris.Data.HybrisAPI
{
    public static class HybrisApiWrapper
    {
        public static string CallRestService(string url)
        {
            try
            {
                return CallAuthenticatedRestService(url);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                    throw new Exception(ex.Message);
                else
                    throw ex;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string CallAuthenticatedRestService(string url)
        {
            try
            {
                return CallAuthenticatedRestServiceWebRequest(url);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                    throw new Exception(ex.Message);
                else
                    throw ex;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string CallRestServiceWebClient(string url)
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

        private static string CallRestServiceWebRequest(string url)
        {
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = "GET";

            webrequest.UserAgent = "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
                                               "(compatible; MSIE 6.0; Windows NT 5.1; " +
                                               ".NET CLR 1.1.4322; .NET CLR 2.0.50727)"; 

            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();

            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");

            StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);

            string result = string.Empty;

            result = responseStream.ReadToEnd();

            webresponse.Close();

            return result;
        }

        private static string CallAuthenticatedRestServiceWebRequest(string url)
        {
            WebRequest webrequest = WebRequest.Create(url);
            webrequest.Method = WebRequestMethods.Http.Get;
            NetworkCredential networkCredential = new NetworkCredential("admin", "nimda"); // logon in format "domain\username"
            CredentialCache myCredentialCache = new CredentialCache { { new Uri(url), "Basic", networkCredential } };
            webrequest.PreAuthenticate = true;
            webrequest.Credentials = myCredentialCache;

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
