using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace Visual_ASp
{
    public class UserDal
    {
        /// <summary>
        /// 将用户密码进行加密
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetMd5Hash(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            //对明文进行MD加密，生成一个字节数组
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes and create a string.
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data  and format each one as a hexadecimal string.
            //将生成的密文进行转换，每个字节转换为两位16进制字符
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            //返回构造的16进制字符串
            return sBuilder.ToString();
        }
        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool UserLogin(string name,string password)
        {           
            string sql = "select count(*) from users where username=@name and password = @password";
            SqlParameter[] pms = new SqlParameter[] { new SqlParameter("@name", name), new SqlParameter("@password", password) };
           if ((int)SqlHelper.ExecuteScalar(sql,pms)>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 使用SqlDataReader实现
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool UserLoginWithDataReader(string name,string password)
        {
            string sql = "select  *  from users where username=@name and password = @password";
            SqlParameter[] pms = new SqlParameter[] { new SqlParameter("@name", name), new SqlParameter("@password", password) };
            //使用SqlHelper的ExecuteReader方法，该方法返回一个SqlDataReader对象
            SqlDataReader reader = SqlHelper.ExecuteReader(sql, pms);            
                //如果返回的SqlDataReader中包含数据，说明输入的用户名和密码正确
                if(reader.HasRows)
                {
                    return true;
                }
                //如果返回的SqlDataReader中不包含数据，说明输入的用户名和密码不正确
                else
                {
                    return false;
                }            
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool UserRegister(string name,string password)
        {
            string sql = "insert into users (username,password) values(@name,@password)";           
            SqlParameter[] pms = new SqlParameter[] { new SqlParameter("@name", name), new SqlParameter("@password", password) };
            if(SqlHelper.ExecuteNonQuery(sql,pms)>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }    
    }
}