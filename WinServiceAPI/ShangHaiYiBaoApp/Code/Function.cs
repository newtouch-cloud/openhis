using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ShangHaiYiBaoApp.Code
{
    public static class Function
    {
        /// <summary>
        /// 获取本地的IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIp()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            return AddressIP;
        }



        /// <summary>
        /// DataTable 转Json
        /// </summary>
        /// <param name="table">dataTable值</param>
        /// <returns></returns>
        public static string ToJson(DataTable table)
        {
            StringBuilder jsonStr = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                jsonStr.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    jsonStr.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        jsonStr.Append($"\"{table.Columns[j].ColumnName.ToString()}\":\"{Convert.ToString(table.Rows[i][j])}\"");

                        if (j < table.Columns.Count - 1) jsonStr.Append(",");
                    }
                    jsonStr.Append(i == table.Rows.Count - 1 ? "}" : "},");
                }
                jsonStr.Append("]");
            }
            return jsonStr.ToString();
        }

        public static List<T> ToList<T>(this DataTable table)
        {
            try
            {
                List<T> list = new List<T>();
                if (!table.HasData()) return list;

                foreach (DataRow row in table.Rows)
                {
                    T t = Activator.CreateInstance<T>();
                    PropertyInfo[] pis = t.GetType().GetProperties();
                    foreach (PropertyInfo pi in pis)
                    {
                        if (pi.CanWrite == false) continue;
                        if (table.Columns.Contains(pi.Name))
                        {
                            object value = row[pi.Name];
                            string typeName = pi.PropertyType.Name.ToLower();
                            if (typeName.Contains("int"))
                            {
                                pi.SetValue(t, value == DBNull.Value ? 0 : Convert.ToInt32(value), null);
                            }
                            else if (typeName.Contains("string"))
                            {
                                pi.SetValue(t, value == DBNull.Value ? "" : Convert.ToString(value), null);
                            }
                            else if (typeName.Contains("decimal"))
                            {
                                pi.SetValue(t, value == DBNull.Value ? 0 : Convert.ToDecimal(value), null);
                            }
                            else
                            {
                                pi.SetValue(t, value == DBNull.Value ? null : value, null);
                            }
                        }
                    }
                    list.Add(t);
                }

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 是否包含有表
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static bool HasData(this DataTable table)
        {
            return !(table == null || table.Rows.Count == 0);
        }

        /// <summary>
        /// 删除json指定的节点
        /// </summary>
        /// <param name="jsonStr">json字符串</param>
        /// <param name="node">节点</param>
        private static void JsonUpNode(string jsonStr, string node)
        {
            JObject json = JObject.Parse(jsonStr);
            json["err_msg"] = "HIS信息提示：写入就诊登记信息表失败drjk_mzjzxxsc_input-100";
            json["infcode"] = "-100";

        }
        /// <summary>
        /// R代表目标实体   T代表数据源实体
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static R Mapping<R, T>(T model)
        {

            R result = Activator.CreateInstance<R>();
            foreach (PropertyInfo info in typeof(R).GetProperties())
            {
                PropertyInfo pro = typeof(T).GetProperty(info.Name);
                if (pro != null)
                    info.SetValue(result, pro.GetValue(model));
            }
            return result;
        }


        /// <summary>
        ///将table  保存记事本文件  中
        /// </summary>
        /// <param name="dirTXT">文件名称</param>
        /// <param name="dt">table</param>
        public static void WriteTXT(string dirTXT, System.Data.DataTable dt)
        {
            FileStream fs = new FileStream(dirTXT, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
				//开始写入
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					sw.WriteLine(dt.Rows[i][0].ToString() + "	" + dt.Rows[i][1].ToString() + "	" + dt.Rows[i][2].ToString() + "	" + dt.Rows[i][3].ToString() + "	" + dt.Rows[i][4].ToString() + "	" + dt.Rows[i][5].ToString() + "	" + dt.Rows[i][6].ToString());
				}
				//清空缓冲区
				sw.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //关闭流
                sw.Close();
                fs.Close();
            }
        }
        /// <summary>
        ///将table  保存记事本文件  中
        /// </summary>
        /// <param name="dirTXT">文件名称</param>
        /// <param name="dt">table</param>
        public static void WriteTXT_3211(string dirTXT, System.Data.DataTable dt)
        {
            FileStream fs = new FileStream(dirTXT, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                //开始写入
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sw.WriteLine(dt.Rows[i][0].ToString());
                }
                //清空缓冲区
                sw.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //关闭流
                sw.Close();
                fs.Close();
            }
        }
        public static void WriteStringToTXT(string dirTXT, string str)
        {
            FileStream fs = new FileStream(dirTXT, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.Write(str + "\r\n");
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// 文件 转换成byte 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] FileToBytes(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                return new byte[0];
            }
            FileInfo fi = new FileInfo(path);
            byte[] buff = new byte[fi.Length];
            FileStream fs = fi.OpenRead();
            fs.Read(buff, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            return buff;
        }

    }
}
