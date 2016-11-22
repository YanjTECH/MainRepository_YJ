using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTMvcModels
{
    /// <summary>
    /// 新闻接口数据类
    /// </summary>
    public class INewsInfo
    {
        public string id { get; set; }
        public string news_title { get; set; }
        public string news_catalog { get; set; }
        public string news_summary { get; set; }
        public string news_content { get; set; }
        public DateTime publish_date { get; set; }
        public string publisher_name { get; set; }
        public int view_times { get; set; }
    }
}
