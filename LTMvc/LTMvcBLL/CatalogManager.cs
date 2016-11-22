using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTMvcDAL;
using LTMvcModels;

namespace LTMvcBLL
{
    public class CatalogManager
    {
        public string GetTreeJson()
        {
            return new CatalogService().GetTreeJson();
        }


        public int AddCatalog(CatalogInfo objCatalog)
        {
            return new CatalogService().AddCatalog(objCatalog);
        }

        public int ModifyCatalog(CatalogInfo objCatalog)
        {
            return new CatalogService().ModifyCatalog(objCatalog);
        }
    }
}
