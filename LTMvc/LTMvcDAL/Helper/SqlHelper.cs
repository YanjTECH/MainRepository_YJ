using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LTMvcDAL
{

    /// <summary>   
    /// SqlServer数据访问帮助类   
    /// </summary>  
    class SqlHelper
    {
        private static readonly string connString = ConfigurationManager.ConnectionStrings["connString"].ToString();

        /// <summary>
        /// 执行增删改方法
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 执行单一结果查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object GetSingleResult(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 执行一个结果集的查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        ///// <summary>
        ///// 数据库连接访问名.默认为connString
        ///// </summary>
        //private static string ConnStrName = "connString";

        ///// <summary>
        ///// 通过web.config
        ///// </summary>
        ///// <param name="connectionStringName">数据库连接名</param>
        //public SqlHelper(string connectionStringName)
        //{
        //    ConnStrName = connectionStringName;
        //}

        //#region 数据库连接

        ///// <summary>
        ///// 得到一个有效的数据库连接字符串
        ///// </summary>
        ///// <returns></returns>
        //public static string GetConnString()
        //{
        //    return ConfigurationManager.ConnectionStrings[ConnStrName].ToString();
        //}

        ///// <summary>
        ///// 得到一个有效的数据库连接对象 
        ///// </summary>
        ///// <returns></returns>
        //public static SqlConnection GetConnection()
        //{
        //    SqlConnection _dbConn = new SqlConnection(SqlHelper.GetConnString());
        //    return _dbConn;
        //}
        //#endregion

        //#region ExecuteNonQuery命令

        ///// <summary>
        ///// 执行增删改方法
        ///// </summary>
        ///// <param name="sql"></param>
        ///// <returns></returns>
        //public static int ExecuteNonQuery(string sql)
        //{
        //    SqlConnection conn = SqlHelper.GetConnection();
        //    SqlCommand cmd = new SqlCommand(sql, conn);
        //    try
        //    {
        //        conn.Open();
        //        return cmd.ExecuteNonQuery();
        //    }
        //    catch(SqlException sqlExp)
        //    {
        //        throw sqlExp;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}
        //#endregion


        //#region ExcuteReader 数据阅读器

        //public static SqlDataReader GetReader(string sql)
        //{
        //    string connString = ConfigurationManager.ConnectionStrings["connString"].ToString();
        //    SqlConnection conn = new SqlConnection(connString);
        //    //SqlConnection conn = SqlHelper.GetConnection();
        //    SqlCommand cmd = new SqlCommand(sql,conn);
        //    try
        //    {
        //        conn.Open();
        //        //return cmd.ExecuteReader();
        //        return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //    }
        //    catch(SqlException sqlExp)
        //    {

        //        throw sqlExp;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}
        //#endregion
    }

}
