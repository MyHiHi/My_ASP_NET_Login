using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace Visual_ASp
{
    /// <summary>
    /// Delete 的摘要说明
    /// </summary>
    public class Delete : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            String id=context.Request["id"];
            int showId = int.Parse(id);
            string sql = "delete from User_info where id=@id";
            SqlParameter[] ps = { new SqlParameter("@id", showId) };
            int result = SqlHelper.ExecuteNonQuery(sql, ps);
            if (result > 0)
            {
                context.Response.Redirect("loginHandler_extern.ashx");
            }
            else
            {
                context.Response.Write("删除失败!");
                
            }
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