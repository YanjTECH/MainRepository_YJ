using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LTMvcModels;
using LTMvcBLL;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

namespace LTMvc.Controllers.Api
{
    public class GetNewsListController : ApiController
    {
        public string GetNewsListByCatalogId(string id) {
            List<NewsInfo> newsList = new NewsInfoManage().GetNewsInfo(id);

            string json = "";
            json = NewsInfoToJson(newsList);
            return json;
        }

        public string NewsInfoToJson(List<NewsInfo> newsList)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (NewsInfo newsInfo in newsList)
            {
                sb.Append("{");
                sb.Append("'id':'");
                sb.Append(newsInfo.id);
                sb.Append("','news_title':'");
                sb.Append(newsInfo.news_title);
                sb.Append("','catalog_name':'");
                sb.Append(newsInfo.catalog_name);
                sb.Append("','news_summary':'");
                sb.Append(newsInfo.news_summary);
                sb.Append("'},");
            }
            sb.Remove(sb.ToString().LastIndexOf(','), 1);
            sb.Append("]");
            return sb.ToString();
        }
    }
}
