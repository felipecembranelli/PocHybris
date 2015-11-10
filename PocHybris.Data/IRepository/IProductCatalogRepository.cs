using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocHybris.Data.Infrastructure;
using PocHybris.Model;
using PocHybris.Data.HybrisAPI;

namespace PocHybris.Data.Repository
{

    public interface IProductCatalogRepository: IRepository<ProductCatalogRootDTO>
    {
        PocHybris.Data.HybrisAPI.ProductCatalogRootDTO ListAll();
    }
}
  