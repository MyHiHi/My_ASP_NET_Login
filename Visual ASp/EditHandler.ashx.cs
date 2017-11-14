using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Data.SqlClient;
namespace Visual_ASp
{
    /// <summary>
    /// EditHandler 的摘要说明
    /// </summary>
    public class EditHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            int id = int.Parse(context.Request["id"]);
            string sql = "select * from User_info where Id=@id";
            SqlParameter[] ps = { new SqlParameter("@id",id)};
            DataTable dt = SqlHelper.ExecuteDataTable(sql,ps);
            string strResult =
                File.ReadAllText(context.Request.MapPath("Edit.html"));
            strResult = strResult.Replace("@name",dt.Rows[0]["Name"].ToString());
            strResult = strResult.Replace("@age",dt.Rows[0]["Age"].ToString());
            strResult = strResult.Replace("@number",dt.Rows[0]["Number"].ToString());
            strResult = strResult.Replace("@company",dt.Rows[0]["Company"].ToString());
            strResult = strResult.Replace("@adress", dt.Rows[0]["Adress"].ToString());
            strResult = strResult.Replace("@Id",dt.Rows[0]["Id"].ToString());
            context.Response.Write(strResult);     
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