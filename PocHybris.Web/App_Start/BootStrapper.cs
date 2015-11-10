using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocHybris.Web.App_Start
{
    public static class Bootstrapper
    {
        public static void Run()
        {

            // IOC configuration
            AutoFacConfig.ConfigureContainer();
        }

        /// <summary>
        /// Get default user repository from web.config.
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultGitHubRepo()
        {
            string defaultRepo = string.Empty;

            try
            {
                defaultRepo = System.Configuration.ConfigurationManager.AppSettings["DefaultGitHubRepo"].ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }

            return defaultRepo;
        }
    }
}