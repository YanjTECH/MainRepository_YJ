using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTMvcModels
{
    public class CatalogInfo
    {
        public string id { get; set; }
        public string catalog_name { get; set; }
        public string catalog_pid { get; set; }
        public string creater_name { get; set; }
        public DateTime create_time { get; set; }
        public string updater_name { get; set; }
        public DateTime updated_time { get; set; }
    }
}
