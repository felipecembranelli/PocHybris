using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using PocHybris.Data.Repository;
using System.Collections.Generic;
using PocHybris.Model;
using System.Data.Entity.Core.EntityClient;
using Effort.DataLoaders;
using System.Text;
using System.IO;
using Moq;
using System.Net;
using PocHybris.Data.HybrisAPI;

namespace PocHybris.Tests.Repositories
{
    [TestClass]
    public class ProductCatalogRepositoryTest
    {
        ProductCatalogRepository objRepo;

        [TestInitialize]
        public void Initialize()
        {
            // create repository
            objRepo = new ProductCatalogRepository(new Data.Infrastructure.DatabaseFactory());
        }

        [TestMethod]
        public void ProductCatalogRepository_GetAll_ReturnsAllProducts()
        {
            //Act
            var result = (ProductCatalogRootDTO)objRepo.ListAll();

            //Assert
            Assert.IsNotNull(result);
        }

    }
}
