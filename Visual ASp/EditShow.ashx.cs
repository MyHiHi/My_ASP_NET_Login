using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
namespace Visual_ASp
{
    /// <summary>
    /// EditShow 的摘要说明
    /// </summary>
    public class EditShow : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            int id = int.Parse(context.Request["hidId"]);
            string name=context.Request["name"];
            string age=context.Request["age"];
            string number=context.Request["number"];
            string company=context.Request["company"];
            string adress=context.Request["adress"];
            string sql = "update User_info set Name=@name,Age=@age,Number=@number,Company=@company,Adress=@adress where Id=@id";
            SqlParameter[] ps ={
                                  new SqlParameter("@id",id),
                                  new SqlParameter("@name",name),
                                  new SqlParameter("@age",age),
                                  new SqlParameter("@number",number),
                                  new SqlParameter("@company",company),
                                  new SqlParameter("@adress",adress)
                              };
            int result = SqlHelper.ExecuteNonQuery(sql,ps);
            if (result > 0)
            {
                context.Response.Redirect("loginHandler_extern.ashx");
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