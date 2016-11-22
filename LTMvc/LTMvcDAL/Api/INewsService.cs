using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTMvcModels;
using LTMvcDAL;
using System.Data.SqlClient;

namespace LTMvcDAL
{
    public class INewsService
    {
        public List<INewsInfo> GetNew(string sql)
        {
            SqlDataReader objNews = SqlHelper.GetReader(sql);

            List<INewsInfo> listINews = new List<INewsInfo>();
            while (objNews.Read())
            {
                listINews.Add(new INewsInfo()
                {
                    id = objNews["id"].ToString(),
                    news_catalog = objNews["catalog_name"].ToString(),
                    news_summary = objNews["news_summary"].ToString(),
                    news_content = objNews["news_info"].ToString(),
                    publish_date = Convert.ToDateTime(objNews["publish_date"]),
                    publisher_name = objNews["updater_name"].ToString()
                });
            }
            objNews.Close();

            return listINews;
        }

        /// <summary>
        /// 获取所有新闻信息
        /// </summary>
        /// <returns></returns>
        public List<INewsInfo> GetAllNew()
        {
            string sql = "SELECT * FROM V_News ORDER BY id";
            return GetNew(sql);
        }

        /// <summary>
        /// 单条件查询
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public List<INewsInfo> GetNewsByWhere(string fieldName, string whereValue)
        {
            string sql = "SELECT * FROM V_News WHERE {0} = '{1}' ORDER BY id";

            sql = string.Format(sql, fieldName, whereValue);

            return GetNew(sql);
        }

        /// <summary>
        /// 单条件模糊查询
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="whereValue">查询条件</param>
        /// <returns></returns>
        public List<INewsInfo> GetNewsLikeWhere(string fieldName, string whereValue)
        {
            string sql = "SELECT * FROM V_News WHERE {0} LIKE '%{1}%' ORDER BY id";

            sql = string.Format(sql, fieldName, whereValue);

            return GetNew(sql);
        }
    }
}
