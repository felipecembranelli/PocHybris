using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tinder4Jobs.DTO
{
    public class JobListDTO
    {
        public string Total { get; set; }

        public LinkedinJobDTO[] Values { get; set; }

    }

    public class LinkedinJobListDTO
    {
        public FacetsDTO Facets { get; set; }

        public JobListDTO Jobs { get; set; }

        public string NumResults { get; set; }

    }

    public class FacetsDTO
    {
        public string Total { get; set; }
    }
}