using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocHybris.Data.HybrisAPI;

namespace PocHybris.Services.IServices
{
    public interface IProductCatalogService
	{
        PocHybris.Data.HybrisAPI.ProductCatalogRootDTO ListAll();
    } 
}
