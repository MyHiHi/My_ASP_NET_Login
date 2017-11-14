using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Visual_ASp
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string path = context.Request.MapPath("register.html");
            string html = System.IO.File.ReadAllText(path);
            string name=context.Request.Form["name"];
            string pwd=context.Request.Form["pwd"];
            UserDal userdal = new UserDal();
            if (userdal.UserRegister(name, pwd)) {

                context.Response.Write("<script type='text/javascript'> alert('注册成功!进入登录页面')</script>");
                context.Response.Redirect("index.html");
            }

            else
            {
                context.Response.Write("<script type='text/javascript'> alert('注册失败，请重新注册!')</script>");
                
                context.Response.Redirect(html);

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