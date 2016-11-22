using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LTMvcModels
{
    /// <summary>
    /// 用户登陆信息类
    /// </summary>

    [Serializable]
    public class UserAccount
    {
        public string user_id { get; set; }
        public string user_name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string user_pwd { get; set; }
    }
}
