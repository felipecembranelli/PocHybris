using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocHybris.Data.HybrisAPI
{
    public class Image
    {
        public string imageType { get; set; }
        public string format { get; set; }
        public string altText { get; set; }
        public string url { get; set; }
    }

    public class Product
    {
        public double averageRating { get; set; }
        public bool purchasable { get; set; }
        public string name { get; set; }
        public List<object> baseOptions { get; set; }
        public string code { get; set; }
        public string url { get; set; }
        public string manufacturer { get; set; }
        public List<Image> images { get; set; }
        public string variantType { get; set; }
    }

    public class ProductCatalogRootDTO
    {
        public string id { get; set; }
        public string lastModified { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public List<object> subcategories { get; set; }
        public List<Product> products { get; set; }
    }

}
