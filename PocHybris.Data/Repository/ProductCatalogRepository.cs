using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using PocHybris.Model;
using PocHybris.Data.Infrastructure;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PocHybris.Data.HybrisAPI;

namespace PocHybris.Data.Repository
{
    public class ProductCatalogRepository: Infrastructure.RepositoryBase<ProductCatalogRootDTO>, IProductCatalogRepository
    {

        public ProductCatalogRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        #region Hybris api wrapper

        /// <summary>
        /// Search github looking for query criteria, return a list of repositories
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ProductCatalogRootDTO ListAll()
        {

            var url = "http://localhost:9001/rest/v1/electronics/catalogs/electronicsProductCatalog/Online/categories/brands?options=PRODUCTS";

            try
            {
                string jsonString = HybrisAPI.HybrisApiWrapper.CallAuthenticatedRestService(url);

                var jsonObjectDto = JsonConvert.DeserializeObject<ProductCatalogRootDTO>(jsonString);

                return jsonObjectDto;

            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion

        //#region Helper

        ///// <summary>
        ///// Map entity from DTO to model
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //private PocHybris.Model.GitHubRepo MapDtoToModel(GitHubRepoDTO dto)
        //{
        //    PocHybris.Model.GitHubRepo repoModel = new GitHubRepo();

        //    if (dto == null)
        //        return null;

        //    repoModel.Name = dto.name;
        //    repoModel.Description = dto.description;
        //    repoModel.GitHubRepoId = dto.id;
        //    repoModel.Language = dto.language;
        //    repoModel.OwnerAvatarUrl = dto.owner.AvatarUrl;
        //    repoModel.OwnerName = dto.owner.Login;
        //    repoModel.UpdatedAt = ConvertToDateTime(dto.updated_at);

        //    return repoModel;
        //}

        ///// <summary>
        ///// Format date received from github api
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //private System.DateTime? ConvertToDateTime(string value)
        //{
        //    DateTime dt;

        //    try
        //    {
        //        dt = DateTime.ParseExact(value, "yyyy-MM-ddTHH:mm:ssZ", null);
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //    return dt;
        //}
        //#endregion
    }
}