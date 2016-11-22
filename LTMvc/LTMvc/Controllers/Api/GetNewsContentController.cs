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
    public class GetNewsContentController : ApiController
    {
        public string GetNewsContentByNewsId(string id)
        {
            NewsContent newsContent = new NewsInfoManage().GetNewsContent(id);

            string json = "";
            json = NewsContentToJson(newsContent);
              return json;
        }

        public string NewsContentToJson(NewsContent newsContent)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            sb.Append("{");
            sb.Append("'id':'");
            sb.Append(newsContent.id);
            sb.Append("','news_id':'");
            sb.Append(newsContent.news_id);
            sb.Append("','news_info':'");
            sb.Append(newsContent.news_info);
            sb.Append("'}");
            sb.Append("]");
            return sb.ToString();
        }
    }
}
