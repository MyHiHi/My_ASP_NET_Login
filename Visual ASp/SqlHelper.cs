using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Visual_ASp
{
    static public class SqlHelper
    {
        private  static readonly string constr = ConfigurationManager.ConnectionStrings["connectionStr"].ConnectionString;
               
        /// <summary>
        /// 最终版本，传入一个sql查询命令和SqlParameter参数数组,执行后返回单个值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pms"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] pms)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(sql,con))
                {                  
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    con.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
        /// <summary>
        /// 执行增、删、改操作
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pms"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql,params SqlParameter[] pms)
        {
            //使用using关键字定义一个范围，在范围结束时自动调用这个类实例的Dispose处理对象。
            using (SqlConnection con = new SqlConnection(constr))
            {
                //创建执行Sql命令对象
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    //判断是否传递了sql参数
                    if(pms !=null)
                    {
                        //将参数添加到Parameters集合中
                        cmd.Parameters.AddRange(pms);
                    }
                    //执行命令之前先打开连接
                    con.Open();
                    //执行命令并将结果返回
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 执行返回SqlDataReader
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pms"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] pms)
        {            
            SqlConnection con = new SqlConnection(constr);//定义SqlConnection对象，注意这里不能使该对象自动销毁
             //创建执行Sql命令对象
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {                    
                    if (pms != null)//判断是否传递了sql参数
                    {                        
                        cmd.Parameters.AddRange(pms);//将参数添加到Parameters集合中
                    }
                    try
                    {
                        con.Open();//打开连接，执行查询，返回SqlDataReader对象
                    //执行命令，返回一个SqlDataReader对象，连接对象不关闭
                    //只有当返回的对象销毁时才关闭数据库连接对象
                        return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    }
                    catch(Exception)//如果在上面的查询操作中出现异常，在这里捕获
                    {
                        con.Close();    //发生异常时，关闭连接对象
                        con.Dispose();//将连接对象销毁
                        throw;           //然后再将异常抛出
                    }
                }
        }

        /// <summary>
        /// 执行返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pms"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql, params SqlParameter[] pms)
        {
            //先定义一个表格对象
            DataTable dt = new DataTable();
            //定义一个数据适配器对象
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, constr))
            {
                if(pms != null)
                {
                    adapter.SelectCommand.Parameters.AddRange(pms);
                }
                //通过数据适配器对象将查询的对象填充到表格中
                adapter.Fill(dt);
            }
            return dt;
        }
    }
}