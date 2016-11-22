using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTMvcModels
{
    public class TreeNode
    {
        public TreeNode()
        {
            m_Id = String.Empty;
            m_Pid = String.Empty;
            m_Text = String.Empty;
            m_Children = new List<TreeNode>();
        }

        public TreeNode(string id, string pid, string text)
        {
            m_Id = id;
            m_Pid = pid;
            m_Text = text;
            m_Children = new List<TreeNode>();
        }

        private string m_Id;
        public string Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        private string m_Pid;
        public string Pid
        {
            get { return m_Pid; }
            set { m_Pid = value; }
        }

        private string m_Text;
        public string Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }
        private List<TreeNode> m_Children;
        public List<TreeNode> Children
        {
            get { return m_Children; }
            set { m_Children = value; }
        }
        public bool HasChildren 
        {
            get {
                if (this.Children != null)
                    return m_Children.Count > 0 ? true : false;
                else
                    return false;
            }
        }

        /// <summary>
        /// 生成根节点的json格式字符串
        /// </summary>
        /// <returns></returns>
        public string ToJsonTreeString()
        {
            if (!this.HasChildren)
                return "";
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (TreeNode node in this.Children)
            {
                sb.Append("{");
                sb.Append("'id':'");
                sb.Append(node.Id);
                sb.Append("','text':'");
                sb.Append(node.Text);
                sb.Append("',");
                //有孩子节点时添加children字段,否则令leaf字段为true
                if (node.HasChildren)
                {
                    sb.Append("'children':");
                    sb.Append(node.ToJsonTreeString());
                }
                else
                {
                    sb.Append("'leaf':true");
                }
                sb.Append("},");
            }
            //去掉最后一个逗号
            if(this.Children.Count>0)
                sb.Remove(sb.ToString().LastIndexOf(','), 1);
            sb.Append("]");
            return sb.ToString();
        }
    }
}
