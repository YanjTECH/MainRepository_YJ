using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTMvcModels;
using System.Data;
using System.Data.SqlClient;

namespace LTMvcDAL
{
    public class UserAccountService
    {
        /// <summary>
        /// 根据账号和密码进行登录，返回用户名
        /// </summary>
        /// <param name="objUser">用户信息</param>
        /// <returns></returns>
       public UserAccount UserLogin(UserAccount objUser)
        {
            string sql = "select user_name from T_UserInfo where user_id = '{0}' and user_pwd = {1} and is_del = 0";
            sql = string.Format(sql, objUser.user_id, objUser.user_pwd);
            try
            {
                SqlDataReader objReader = SqlHelper.GetReader(sql); 
                if (objReader.Read())
                {
                    objUser.user_name = objReader["user_name"].ToString();
                    objReader.Close();
                }
                else
                {
                    objUser = null;
                }
            }
            catch(Exception sqlExp)
            {
                throw new Exception("应用程序和数据库连接出现问题！" + sqlExp.Message);
            }
            return objUser;
        }
    }
}
