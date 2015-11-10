using PocHybris.Data.Infrastructure;
using PocHybris.Data.Repository;
using PocHybris.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocHybris.Services
{
    public class ProductCatalogService : IProductCatalogService
    {
        private readonly IProductCatalogRepository productCatalogRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProductCatalogService(IProductCatalogRepository repo,
                                    IUnitOfWork unitOfWork)
        {

            this.productCatalogRepository = repo;
            this.unitOfWork = unitOfWork;
        }

        #region Hybris services API

        public PocHybris.Data.HybrisAPI.ProductCatalogRootDTO ListAll()
        {
            try
            {
                return this.productCatalogRepository.ListAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
