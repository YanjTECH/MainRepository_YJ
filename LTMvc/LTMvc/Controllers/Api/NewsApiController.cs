using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LTMvcBLL;
using LTMvcModels;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace LTMvc.Controllers
{
    public class NewsApiController : ApiController
    {
        //[AcceptVerbs("get","post")]
        public string GetAllNews()
        {
            List<INewsInfo> newsInfo = new INewsManager().GetAllNews();
            string strList = JsonConvert.SerializeObject(newsInfo);

            return strList;
        }

        //[AcceptVerbs("get", "post")]
        public List<INewsInfo> GetNewsByID(string id)
        {
            List<INewsInfo> newsInfo = new INewsManager().GetNewsById(id);
            return newsInfo;
        }

        //[AcceptVerbs("get","post")]
        public List<INewsInfo> GetNewsByCatalog(string catalogName)
        {
            List<INewsInfo> newsInfo = new INewsManager().GetNewsByCatalog(catalogName);
            return newsInfo;
        }

        //[AcceptVerbs("get", "post")]
        public List<INewsInfo> GetNewsByTitle(string titleName)
        {
            List<INewsInfo> newsInfo = new INewsManager().GetNewsLikeTitle(titleName);
            return newsInfo;
        }


        //[AcceptVerbs("get", "post")]
        public List<INewsInfo> GetNewsByPublishedDate(string publishDate)
        {
            List<INewsInfo> newsInfo = new INewsManager().GetNewsLikePublish(publishDate);
            return newsInfo;
        }
    }
}
