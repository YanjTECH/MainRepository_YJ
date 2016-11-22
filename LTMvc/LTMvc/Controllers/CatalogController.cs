using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTMvcBLL;
using LTMvcModels;
using System.Web.Script.Serialization;

namespace LTMvc.Controllers
{
    public class CatalogController : Controller
    {
        // GET: Catalog
        public string GetTreeJson()
        {
            return new CatalogManager().GetTreeJson();
        }

        public string AddCatalog(string name, string pid)
        {
            //1.获取数据并完成封装
            CatalogInfo objCatalog = new CatalogInfo()
            {
                id = Guid.NewGuid().ToString(),
                catalog_name = name,
                catalog_pid = pid,
                creater_name = "管理员",
                create_time = DateTime.Now,
                updater_name = "管理员",
                updated_time = DateTime.Now
            };

            //2.调用业务逻辑
            int result = new CatalogManager().AddCatalog(objCatalog);

            return GetTreeJson();
        }


        public string ModifyCatalog(string name, string id)
        {
            //1.获取数据并完成封装
            CatalogInfo objCatalog = new CatalogInfo()
            {
                id = id,
                catalog_name = name,
                updater_name = "管理员",
                updated_time = DateTime.Now
            };

            //2.调用业务逻辑
            int result = new CatalogManager().ModifyCatalog(objCatalog);

            return GetTreeJson();
        }
    }

}