using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTMvcModels
{
    [Serializable]
    public class ImageInfo
    {
        public string image_id { get; set; }
        public string image_name { get; set; }
        public string image_url { get; set; }
        public string image_owner { get; set; }
        public string image_type { get; set; }
        public string image_res { get; set; }
        public string image_size { get; set; }

        public DateTime create_time { get; set; }
        public string creater_name { get; set; }
        public DateTime updated_time { get; set; }
        public string updater_name { get; set; }
    }
}
