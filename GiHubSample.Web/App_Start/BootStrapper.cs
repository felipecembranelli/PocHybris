using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiHubSample.Web.App_Start
{
    public static class Bootstrapper
    {
        public static void Run()
        {

            // Configure AutoMapper (Entity Mapper)
            //AutoMapperConfiguration.Configure();

            // IOC configuration
            AutoFacConfig.ConfigureContainer();
        }

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