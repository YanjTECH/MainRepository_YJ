using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTMvcModels;
using LTMvcDAL;
using System.Data.SqlClient;
using System.Web;
using System.Net.Sockets;
using System.Net;

namespace LTMvcDAL
{
    public class NewsInfoService
    {
        #region 查询新闻信息
        
        /// <summary>
        /// 获取新闻条目列表信息
        /// </summary>
        /// <param name="catalogId">文件夹编码</param>
        /// <returns></returns>
        public List<NewsInfo> GetNewsInfo(string catalogId)
        {

            string sql = "WITH datasource AS(SELECT * , id AS px1, CAST(id AS  nvarchar(4000)) AS px2 FROM T_Catalog WHERE {0} ";
            sql += "UNION ALL SELECT a.*, b.px1, b.px2+LTRIM(a.id) FROM T_Catalog AS a JOIN datasource AS b ON a.catalog_pid = b.id) ";
            sql += "SELECT * FROM T_News WHERE catalog_id IN (SELECT id FROM datasource AS a WHERE a.id = T_News.catalog_id) AND is_del = 'FALSE'";
            //sql += "SELECT * FROM T_News AS a INNER JOIN datasource AS b ON a.catalog_id = b.id AND a.is_del = 'FALSE'";
            string sqlWhere = (catalogId == null ? "catalog_name = '新闻管理'" : "id = '" + catalogId + "'");

            sql = string.Format(sql, sqlWhere);

            List<NewsInfo> newsInfo = GetNewsList(sql);

            return newsInfo;
        }

        /// <summary>
        /// 获取新闻条目详情信息
        /// </summary>
        /// <param name="news_id"></param>
        /// <returns></returns>
        public List<NewsInfo> GetNewsDetails(string id) {
            string sql = "SELECT * FROM T_News WHERE id = '{0}' AND is_del = 'FALSE'";
            sql = string.Format(sql, id);

            List<NewsInfo> newsInfo = GetNewsList(sql);

            return newsInfo;
        }


        public List<NewsInfo> GetNewsList(string sql)
        {
            SqlDataReader objNews = SqlHelper.GetReader(sql);
            List<NewsInfo> listNews = new List<NewsInfo>();
            while (objNews.Read())
            {
                listNews.Add(new NewsInfo()
                {
                    id = objNews["id"].ToString(),
                    news_title = objNews["news_title"].ToString(),
                    catalog_name = objNews["catalog_name"].ToString(),
                    catalog_id = objNews["catalog_id"].ToString(),
                    news_summary = objNews["news_summary"].ToString(),
                    is_published = Convert.ToBoolean(objNews["is_published"]),
                    publish_date = Convert.ToDateTime(objNews["publish_date"]),
                    write_date = Convert.ToDateTime(objNews["write_date"]),
                    view_times = (int)objNews["view_times"],
                    create_time = Convert.ToDateTime(objNews["create_time"]),
                    creater_name = objNews["creater_name"].ToString(),
                    updated_time = Convert.ToDateTime(objNews["updated_time"]),
                    updater_name = objNews["updater_name"].ToString()
                });
            }
            objNews.Close();
            return listNews;
        }

        #endregion

        #region 新增新闻信息
        public int AddNewsInfo(NewsInfo objNewsInfo)
        {
            //生成sql语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("INSERT INTO T_News  (id, news_title, catalog_name, catalog_id,news_summary, is_published, publish_date, write_date, view_times, is_del, create_time, creater_name, updated_time, updater_name)");
            sqlBuilder.Append("VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}')");

            //解析对象
            string sql = string.Format(sqlBuilder.ToString(), objNewsInfo.id, objNewsInfo.news_title, objNewsInfo.catalog_name, objNewsInfo.catalog_id, objNewsInfo.news_summary,
                                       objNewsInfo.is_published, objNewsInfo.publish_date, objNewsInfo.write_date, objNewsInfo.view_times, false, objNewsInfo.create_time,
                                       objNewsInfo.creater_name, objNewsInfo.updated_time, objNewsInfo.updater_name);

            //执行sql语句
            try
            {
                return Convert.ToInt32(SqlHelper.ExecuteNonQuery(sql));
            }
            catch (Exception ex)
            {

                throw new Exception("数据库操作出现异常！具体信息：\r\n" + ex.Message);
            }
        }


        public int AddNewsContent(NewsContent objNewsContent)
        {
            //生成sql语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("INSERT INTO T_NewsInfo  (id, news_id, news_info, is_del, create_time, creater_name, updated_time, updater_name)");
            sqlBuilder.Append("VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')");

            //解析对象
            string sql = string.Format(sqlBuilder.ToString(), objNewsContent.id, objNewsContent.news_id, objNewsContent.news_info, false,
                                       objNewsContent.create_time, objNewsContent.creater_name, objNewsContent.updated_time, objNewsContent.updater_name);

            //执行sql语句
            try
            {
                return Convert.ToInt32(SqlHelper.ExecuteNonQuery(sql));
            }
            catch (Exception ex)
            {

                throw new Exception("数据库操作出现异常！具体信息：\r\n" + ex.Message);
            }
        }
        #endregion


        #region 修改新闻信息
        public int ModifyNewsInfo(NewsInfo objNewsInfo)
        {
            //生成sql语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("UPDATE T_News SET news_title = '{0}',  news_summary = '{1}', updated_time = '{2}', updater_name = '{3}' WHERE id = '{4}'");

            //解析对象
            string sql = string.Format(sqlBuilder.ToString(), objNewsInfo.news_title, objNewsInfo.news_summary, objNewsInfo.updated_time, objNewsInfo.updater_name, objNewsInfo.id);

            //执行sql语句
            try
            {
                return Convert.ToInt32(SqlHelper.ExecuteNonQuery(sql));
            }
            catch (Exception ex)
            {

                throw new Exception("数据库操作出现异常！具体信息：\r\n" + ex.Message);
            }
        }
        #endregion


        #region 删除新闻信息
        public int DelNewsInfo(string[] objIdArray)
        {
            //生成sql语句
            string strWhere = "";
            if (objIdArray.Length == 1) {
                strWhere = "id = '" + objIdArray[0] + "'";
            }
            else {
                for (int i = 0; i < (objIdArray.Length - 2); i++) {
                    strWhere += "id = '" + objIdArray[i] + "' OR ";
                }
                strWhere += ("id = '" + objIdArray[objIdArray.Length-2] + "'");
            }

            string sql = "UPDATE T_News SET is_del = 'TRUE' WHERE ";
            sql += strWhere;


            //执行sql语句
            try
            {
                return Convert.ToInt32(SqlHelper.ExecuteNonQuery(sql));
            }
            catch (Exception ex)
            {

                throw new Exception("数据库操作出现异常！具体信息：\r\n" + ex.Message);
            }
        }
        #endregion

        #region 发布新闻信息
        public int PublishNews(string[] objIdArray)
        {
            //生成sql语句
            string strWhere = "";
            if (objIdArray.Length == 1)
            {
                strWhere = "id = '" + objIdArray[0] + "'";
            }
            else
            {
                for (int i = 0; i < (objIdArray.Length - 2); i++)
                {
                    strWhere += "id = '" + objIdArray[i] + "' OR ";
                }
                strWhere += ("id = '" + objIdArray[objIdArray.Length - 2] + "'");
            }

            string sql = "UPDATE T_News SET is_published = 'TRUE', publish_date = '{0}' WHERE ";
            sql += strWhere;

            sql = string.Format(sql, DateTime.Now);

            //执行sql语句
            try
            {
                return Convert.ToInt32(SqlHelper.ExecuteNonQuery(sql));
            }
            catch (Exception ex)
            {

                throw new Exception("数据库操作出现异常！具体信息：\r\n" + ex.Message);
            }
        }
        #endregion

        #region 通过news_id获取新闻内容信息
        public NewsContent GetNewsContent(string news_id)
        {
            string sql = "SELECT * FROM T_NewsInfo WHERE news_id = '" + news_id + "'";
            NewsContent objNewsContent = new NewsContent();
            try
            {
                SqlDataReader objReader = SqlHelper.GetReader(sql);
                while (objReader.Read())
                {
                    objNewsContent.news_id = objReader["news_id"].ToString();
                    objNewsContent.news_info = objReader["news_info"].ToString();
                }
                objReader.Close();

                return objNewsContent;
            }
            catch (Exception ex)
            {

                throw new Exception("数据库操作出现异常！具体信息：\r\n" + ex.Message);
            }
        }

        #endregion


        #region 保存新闻内容
        public int SaveNewsContent(NewsContent objNewsContent)
        {
            string sql = "UPDATE T_NewsInfo SET news_info = '" + objNewsContent.news_info + "' WHERE news_id = '" + objNewsContent.news_id + "'";
            try
            {
                return SqlHelper.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("数据库操作出现异常！具体信息：\r\n" + ex.Message);
            }
        }
        #endregion


        #region 上传图片

        public string UploadImgFile(HttpPostedFileBase file, ImageInfo imgInfo, string savePath)
        {

            try
            {
                file.SaveAs(savePath);
                UploadImgToDB(imgInfo);
                return imgInfo.image_url;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message); 
            }
        }


        /// <summary>
        /// 将图片信息存到数据库
        /// </summary>
        /// <param name="objImgInfo"></param>
        /// <returns></returns>

        public int UploadImgToDB(ImageInfo objImgInfo)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("INSERT INTO T_ImageInfo (id, image_name, image_url, image_owner, image_description, is_del, image_type, image_res, image_size, create_time, creater_name, updated_time, updater_name)");
            sqlBuilder.Append("VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}')");


            string sql = string.Format(sqlBuilder.ToString(), objImgInfo.image_id, objImgInfo.image_name, objImgInfo.image_url,
                                        "", "", false, objImgInfo.image_type, objImgInfo.image_res, objImgInfo.image_size,
                                        objImgInfo.create_time, objImgInfo.creater_name, objImgInfo.updated_time, objImgInfo.updater_name);

            try
            {
                return SqlHelper.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {

                throw new Exception("数据库操作出现异常！具体信息：\r\n" + ex.Message);
            }
        }
        #endregion

    }
}
