using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Web;
namespace Visual_ASp
{
    /// <summary>
    /// addInfoHandler2 的摘要说明
    /// </summary>
    public class addInfoHandler2 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string name=context.Request.Form["name"];
            string age = context.Request.Form["age"];
            string number = context.Request.Form["number"];
            string company = context.Request.Form["company"];
            string adress = context.Request.Form["adress"];
            string sql = "insert into User_info(Name,Age,Number,Company,Adress) values(@name,@age,@number,@company,@adress)";
            SqlParameter[] ps ={
                             new SqlParameter("@name",name),
                             new SqlParameter("@age",age),
                             new SqlParameter("@number",number),
                             new SqlParameter("@company",company),
                             new SqlParameter("@adress",adress),
                             };
            int result = SqlHelper.ExecuteNonQuery(sql,ps);
            if (result > 0)
            {
                context.Response.Redirect("loginHandler_extern.ashx");
            }
            else {

                context.Response.Write("<script type='text/javascript'> alert('添加失败!')</script>");
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