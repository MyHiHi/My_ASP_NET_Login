using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
namespace Visual_ASp
{
    /// <summary>
    /// loginHandler_extern 的摘要说明
    /// </summary>
    public class loginHandler_extern : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            //拼接html
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><head></head><body><a href='addInfo.html'>添加</a><br/>");
            //拼接table
            sb.Append("<table> <tr><th>编号</th><th>姓名</th><th>年龄</th>"+
            "<th>电话号码</th><th>公司</th><th>住址</th><th>操作</th></tr>");
            string sql = "select * from User_info";
            SqlDataReader read = SqlHelper.ExecuteReader(sql,null);
            while (read.Read()) {

                sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>"+
                "<a href='showDetailHandler.ashx?id={0}'>详情</a>"+
                "&nbsp;&nbsp;<a onclick='return confirm(\"是否要删除?\")' href='Delete.ashx?id={0}'>删除</a>"+
                "&nbsp;&nbsp;<a href='EditHandler.ashx?id={0}'>修改</a></td</tr>",read["Id"],read["Name"],
                    read["Age"],read["Number"],read["Company"],read["Adress"]);
            
            }
            sb.Append("</table>");
            sb.Append("</body></html>");
            context.Response.Write(sb.ToString());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}