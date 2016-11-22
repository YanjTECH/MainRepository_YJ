using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTMvcModels;
using LTMvcDAL;

namespace LTMvcBLL
{
    public class INewsManager
    {
        public List<INewsInfo> GetAllNews()
        {
            return new INewsService().GetAllNew();
        }

        public List<INewsInfo> GetNewsById(string whereValue)
        {
            return new INewsService().GetNewsByWhere("id",whereValue);
        }

        public List<INewsInfo> GetNewsLikeTitle(string whereValue)
        {
            return new INewsService().GetNewsLikeWhere("news_title", whereValue);
        }

        public List<INewsInfo> GetNewsByCatalog(string whereValue)
        {
            return new INewsService().GetNewsByWhere("news_title", whereValue);
        }

        public List<INewsInfo> GetNewsLikePublish(string whereValue)
        {
            return new INewsService().GetNewsLikeWhere("publish_date", whereValue);
        }
    }
}
