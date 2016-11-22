using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTMvcDAL;
using LTMvcModels;
using System.Web;

namespace LTMvcBLL
{
    public class UserAccountManage
    {
        private UserAccountService objUserService = new UserAccountService();

        public UserAccount UserLogin(UserAccount objUser)
        {
            objUser = objUserService.UserLogin(objUser);

            if(objUser != null)
            {
                //将登录对象保存Session
                HttpContext.Current.Session["CurrentAdmin"] = objUser.user_name;
            }
            return objUser;
        }
    }
}
