using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Visual_ASp
{
    /// <summary>
    /// LoginHandler1 的摘要说明
    /// </summary>
    public class LoginHandler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string path = context.Request.MapPath("index.html");
            string html = System.IO.File.ReadAllText(path);
            string name=context.Request.Form["name"];
            string pwd=context.Request.Form["pwd"];
            string s=context.Request.Form["isRemember"];
            string _vs=context.Request.Form["_viewstate"];
            bool ispostback = !string.IsNullOrEmpty(_vs);
            UserDal userdal = new UserDal();
            HttpCookie cookies=context.Request.Cookies["Login"];

            if (cookies != null && cookies.HasKeys) {
                context.Response.Write("welcome my pages!");
                string name1=cookies["Name"];
                string pwd1=cookies["Pwd"];
                html = html.Replace("@name",name1).Replace("@pwd",pwd1);
              //  context.Response.Write(html);
                context.Response.Redirect("loginHandler_extern.ashx");
            }
                /*
            else if (cookies == null) {
                html = html.Replace("@name","").Replace("@pwd","");
            }
                 */
            else if (userdal.UserLogin(name, pwd))
            {
                context.Response.Write("登录成功！");
                if (ispostback)
                {
                    if (s != null)
                    {
                        cookies = new HttpCookie("Login");
                        cookies.Values.Add("Name", name);
                        cookies.Values.Add("Pwd", pwd);
                        cookies.Expires = System.DateTime.Now.AddYears(1);
                        HttpContext.Current.Response.Cookies.Add(cookies);
                        context.Response.Cookies.Add(cookies);
                    }
                }
                context.Response.Redirect("loginHandler_extern.ashx");
            }
            else
            {
                // html = html.Replace("@name", name).Replace("@msg","登录失败").Replace("pwd","");
                context.Response.Write("登录失败!");
                context.Response.Redirect("register.html");
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