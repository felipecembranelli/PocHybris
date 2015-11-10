using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PocHybris.Services;
using PocHybris.Services.IServices;
using PocHybris.Web.ViewModels;
using PocHybris.Model;
using PocHybris.Web.Mapper;
using PocHybris.Web.App_Start;

namespace PocHybris.Web.Controllers
{
    [HandleError(View = "Error")]
    public class HomeController :  BaseController
    {
        private readonly IProductCatalogService productCatalogservice;
        //private string defaultUserRepository;

        public HomeController(IProductCatalogService service)
        {
            this.productCatalogservice = service;
        }

        public ActionResult Index()
        {
            var productList = this.productCatalogservice.ListAll();

            return View("ListProducts", productList);
        }

    }
}