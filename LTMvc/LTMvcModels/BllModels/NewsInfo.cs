using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace LTMvcModels
{
    /// <summary>
    /// 新闻信息类
    /// </summary>

    [Serializable]
    public class NewsInfo
    {
        public string id { get; set; }
        public string news_title { get; set; }
        public string catalog_name { get; set; }
        public string catalog_id { get; set; }
        public string news_summary { get; set; }
        public bool is_published { get; set; }
        public DateTime publish_date { get; set; }
        public DateTime write_date { get; set; }
        public int view_times { get; set; }
        public DateTime create_time { get; set; }
        public string creater_name { get; set; }
        public DateTime updated_time { get; set; }
        public string updater_name { get; set; }
    }
}
