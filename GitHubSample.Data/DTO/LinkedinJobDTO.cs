using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tinder4Jobs.DTO
{
    public class LinkedinJobDTO
    {
        public LinkedinCompanyDTO Company { get; set; }

        public string DescriptionSnippet { get; set; }

        public string Id { get; set; }

        public LinkedinJobPosterDTO JobPoster { get; set; }

        public string LocationDescription { get; set; }

    }
}