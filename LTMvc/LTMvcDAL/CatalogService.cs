using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LTMvcModels;

namespace LTMvcDAL
{
    public class CatalogService
    {
        #region 获取jstree的json数据
        public string GetTreeJson()
        {
            List<TreeNode> treeNode = GetTreeNode();
            TreeNode Tree = GenerateTreeRoot(treeNode);
            string jsonStr = Tree.ToJsonTreeString();
            return jsonStr;
        }

        /// <summary>
        /// 获取树结构数据
        /// </summary>
        /// <returns></returns>
        public List<TreeNode> GetTreeNode()
        {
            string sql = "WITH datasource AS(SELECT * , id AS px1, CAST(id AS  nvarchar(4000)) AS px2 FROM T_Catalog WHERE catalog_name = '{0}'";
            sql += "UNION ALL SELECT a.*, b.px1, b.px2+LTRIM(a.id) FROM T_Catalog AS a JOIN datasource AS b ON a.catalog_pid = b.id)";
            sql += "SELECT * FROM datasource ORDER BY px1, px2";

            sql = string.Format(sql, "新闻管理");

            SqlDataReader objReader = SqlHelper.GetReader(sql);
            List<TreeNode> listCatalog = new List<TreeNode>();

            while (objReader.Read())
            {
                listCatalog.Add(new TreeNode()
                {
                    Id = objReader["id"].ToString(),
                    Text = objReader["catalog_name"].ToString(),
                    Pid = objReader["catalog_pid"].ToString()
                });
            }
            objReader.Close();
            return listCatalog;
        }




        /// <summary>
        /// 生成一个根节点的树
        /// </summary>
        /// <param name="nodeList">节点列表，包含未连接的树节点，节点中给出id,pid,text字段</param>
        /// <returns></returns>
        public TreeNode GenerateTreeRoot(List<TreeNode> nodeList)
        {
            TreeNode root = new TreeNode();
            TreeNode cNode;
            TreeNode chNode;
            TreeNode pNode;
            Stack<TreeNode> stack = new Stack<TreeNode>();
            while (nodeList.Count > 0)
            {
                cNode = nodeList[0];
                nodeList.Remove(cNode);
                stack.Push(cNode);
                while (cNode != null)
                {
                    cNode = stack.Pop();
                    if ((chNode = getChildren(cNode, nodeList)) != null)
                    {
                        stack.Push(cNode);
                        nodeList.Remove(chNode);
                        stack.Push(chNode);
                    }
                    else
                    {
                        if (stack.Count > 0)
                        {
                            pNode = stack.Pop();
                            pNode.Children.Add(cNode);

                            stack.Push(pNode);
                        }
                        else
                        {
                            if ((pNode = getParent(cNode, nodeList)) != null)
                            {
                                nodeList.Remove(pNode);
                                stack.Push(pNode);
                                pNode.Children.Add(cNode);
                            }
                            else
                            {
                                root.Children.Add(cNode);
                                cNode = null;
                            }
                        }
                    }
                }
            }
            return root;
        }

        public TreeNode getChildren(TreeNode node, List<TreeNode> list)
        {
            return list.Find(delegate (TreeNode n) { return n.Pid == node.Id; });
        }
        public TreeNode getParent(TreeNode node, List<TreeNode> list)
        {
            return list.Find(delegate (TreeNode n) { return n.Id == node.Pid; });
        }
        #endregion


        #region 文件夹增删改

        /// <summary>
        /// 新增文件夹
        /// </summary>
        /// <param name="objCatalog"></param>
        /// <returns></returns>
        public int AddCatalog(CatalogInfo objCatalog)
        {
            //生成sql语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("INSERT INTO T_Catalog  (id, catalog_name, catalog_pid, create_time, creater_name, updated_time, updater_name)");
            sqlBuilder.Append("VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')");

            //解析对象
            string sql = string.Format(sqlBuilder.ToString(), objCatalog.id, objCatalog.catalog_name, objCatalog.catalog_pid,
                objCatalog.create_time, objCatalog.creater_name, objCatalog.updated_time, objCatalog.updater_name);

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

        public int ModifyCatalog(CatalogInfo objCatalog)
        {
            //生成sql语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("UPDATE T_Catalog  SET catalog_name = '{0}', updated_time = '{1}', updater_name = '{2}' WHERE id = '{3}'");

            //解析对象
            string sql = string.Format(sqlBuilder.ToString(),objCatalog.catalog_name, objCatalog.updated_time, objCatalog.updater_name, objCatalog.id);

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
    }
}
