using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Web.Security;
using System.Security.Cryptography;
using System.Data;
namespace Visual_ASp
{
    /// <summary>
    /// showDetailHandler 的摘要说明
    /// </summary>
    public class showDetailHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            StringBuilder sb = new StringBuilder();
            string id=context.Request.QueryString["id"];
            int showId = int.Parse(id);
            string sql = "select * from User_info where Id=@id";
            SqlParameter[] ps = { new SqlParameter("@Id",showId)};
            DataTable dt = SqlHelper.ExecuteDataTable(sql,ps);
            sb.AppendFormat("<tr><td>编号:</td><td>{0}</td></tr>",dt.Rows[0]["Id"]);
            sb.AppendFormat("<tr><td>姓名:</td><td>{0}</td></tr>", dt.Rows[0]["Name"]);
            sb.AppendFormat("<tr><td>年龄:</td><td>{0}</td></tr>",dt.Rows[0]["Age"]);
            sb.AppendFormat("<tr><td>电话号码：</td><td>{0}</td></tr>", dt.Rows[0]["Number"]);
            sb.AppendFormat("<tr><td>公司：</td><td>{0}</td></tr>", dt.Rows[0]["Company"]);
            sb.AppendFormat("<tr><td>地址：</td><td>{0}</td></tr>", dt.Rows[0]["Adress"]);
            string path = context.Request.MapPath("ShowDetail.html");
            string textTemp = System.IO.File.ReadAllText(path);
            string result = textTemp.Replace("@trBody",sb.ToString());//////?????????
            context.Response.Write(result);
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