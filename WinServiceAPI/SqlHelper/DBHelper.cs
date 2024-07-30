using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SqlHelper
{
    public class DBHelper
    {
        
        IniFile myIni = new IniFile(Application.StartupPath + "\\ybjk.ini");

        public string serverAddress {
            get {
                string mConnStr = DESEncrypt.Decrypt(myIni.ReadString("Connection", "ConnectionString",""));
                return mConnStr.Substring(mConnStr.IndexOf("=")+1, mConnStr.IndexOf(";") - mConnStr.IndexOf("=")-1);
            }
        }
        

        /// <summary>
        /// 获取默认的连接字符串
        /// </summary>
        public virtual string connectionString
        {
            get { return GetConnectionString("ConnectionString"); }
        }

        /// <summary>
        /// 数据库返回运行结果等待时间
        /// </summary>
        public int SqlTimeOutNumber
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["SqlTimeOut"]); }
        }

        /// <summary>
        /// 获取指定连接配置节内容。
        /// </summary>
        /// <value>
        /// 配置文件：App.config
        /// 连接节点：ConnectionString
        /// </value>
        /// <param name="configName">连接配置节名称</param>
        /// <returns>返回一个已解密的连接字符串</returns>
        public string GetConnectionString(string configName)
        {
            string connectionString = string.Empty;
            if (string.IsNullOrEmpty(configName))
            {
                connectionString = myIni.ReadString("Connection", "ConnectionString", "");
            }
            else
            {
                connectionString = myIni.ReadString("Connection", configName, "");
            }
            string mConnStr = DESEncrypt.Decrypt(connectionString);
            connectionString = mFunConvertUserId(mConnStr);
            configName = "";
            return connectionString;


        }

        /// <summary>
        /// 修改连接字符串中UserID
        /// </summary>
        /// <param name="mConnStr">原连接字符串</param>
        /// <param name="mUserId">修改后的User ID，默认为ysgzz</param>
        /// <returns>修改后的连接字符串</returns>
        private string mFunConvertUserId(string mConnStr, string mUserId = "his-yfxt")
        {
            try
            {
                string[] connStrNodes = mConnStr.Split(new char[1] { ';' });

                string mFindStr = connStrNodes.First<string>(mstr => (mstr.Substring(0, mstr.IndexOf("=")).ToLower()) == "user id");

                string passStr = connStrNodes.First<string>(mstr => (mstr.Substring(0, mstr.IndexOf("=")).ToLower()) == "password");
                return mConnStr;
                //return mConnStr.Replace(mFindStr, "User ID=wqsj").Replace(passStr, "Password=" + "wqsj2000");
                //return mConnStr.Replace(mFindStr, "User ID=" + mUserId).Replace(passStr, "Password=" + "xz-wqsj+2019");

            }
            catch
            {
                return mConnStr;
            }
        }


        /// <summary>
        /// 将集合参数转换成sqlparameter参数
        /// </summary>
        /// <param name="dic">集合参数</param>
        /// <returns></returns>
        public SqlParameter[] GetSqlParameter(Dictionary<string, object> dic, List<string> outputList)
        {
            try
            {
                SqlParameter[] param = null;
                if (dic != null)
                {
                    param = new SqlParameter[dic.Count];
                    SqlParameter par = null;
                    int i = 0;
                    foreach (KeyValuePair<string, object> kvp in dic)
                    {
                        par = new SqlParameter(kvp.Key, kvp.Value);
                        if (par.Value == null)
                        {
                            par.Value = DBNull.Value;
                        }
                        param.SetValue(par, i);
                        if (outputList != null)
                        {
                            for (int j = 0; j < outputList.Count; j++)
                            {
                                if (kvp.Key == outputList[j])
                                {
                                    param[i].Direction = ParameterDirection.Output;
                                    param[i].Size = 1000;
                                    break;
                                }
                            }
                        }
                        i++;
                    }
                }
                return param;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        /// <summary>
        /// 将集合参数转换成sqlparameter参数
        /// </summary>
        /// <param name="dic">集合参数</param>
        /// <returns></returns>
        public SqlParameter[] GetSqlParameter(Dictionary<string, object> dic, List<string> outputList,List<string> inoutList)
        {
            try
            {
                SqlParameter[] param = null;
                if (dic != null)
                {
                    param = new SqlParameter[dic.Count];
                    SqlParameter par = null;
                    int i = 0;
                    foreach (KeyValuePair<string, object> kvp in dic)
                    {
                        par = new SqlParameter(kvp.Key, kvp.Value);
                        if (par.Value == null)
                        {
                            par.Value = DBNull.Value;
                        }
                        param.SetValue(par, i);
                        if (outputList != null)
                        {
                            for (int j = 0; j < outputList.Count; j++)
                            {
                                if (kvp.Key == outputList[j])
                                {
                                    if (inoutList.Contains(kvp.Key))
                                    {
                                        param[i].Direction = ParameterDirection.InputOutput;
                                        param[i].Size = 1000;
                                    }
                                    else
                                    {
                                        param[i].Direction = ParameterDirection.Output;
                                        param[i].Size = 1000;
                                    }
                                    break;
                                }
                            }
                        }
                        i++;
                    }
                }
                return param;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlParameter[] GetSqlParameter(Dictionary<string, object> dic)
        {
            SqlParameter[] param = null;
            if (dic != null)
            {
                param = new SqlParameter[dic.Count];
                SqlParameter par = null;
                int i = 0;
                foreach (KeyValuePair<string, object> kvp in dic)
                {
                    par = new SqlParameter(kvp.Key, kvp.Value);
                    if (par.Value == null)
                    {
                        par.Value = DBNull.Value;
                    }
                    param.SetValue(par, i);
                    i++;
                }
            }
            return param;
        }

        #region 公用方法
        /// <summary>
        /// 判断是否存在某表的某个字段
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="columnName">列名称</param>
        /// <returns>是否存在</returns>
        public bool ColumnExists(string tableName, string columnName)
        {
            string sql = "select count(1) from syscolumns where [id]=object_id('" + tableName + "') and [name]='" + columnName + "'";
            object res = GetSingle(sql);
            if (res == null)
            {
                return false;
            }
            return Convert.ToInt32(res) > 0;
        }

        /// <summary>
        /// 获取某表整数列中的最大值
        /// </summary>
        /// <param name="FieldName">列名称</param>
        /// <param name="TableName">表名称</param>
        /// <returns>该整数列中的最大值</returns>
        public int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        /// <summary>
        /// 通过sql语句判断表中是否存在记录
        /// </summary>
        /// <param name="strSql">sql语句,格式必须为"select count(1) from ..."</param>
        /// <returns>是否存在</returns>
        public bool Exists(string strSql)
        {
            object obj = GetSingle(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }

            return (cmdresult != 0);
        }

        /// <summary>
        /// 判断指定表是否存在
        /// </summary>
        /// <param name="TableName">表名称</param>
        /// <returns>是否存在</returns>
        public bool TabExists(string TableName)
        {
            string strsql = "select count(*) from sysobjects where id = object_id(N'[" + TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";

            object obj = GetSingle(strsql);
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 通过带参数的sql语句判断表中是否存在记录
        /// </summary>
        /// <param name="strSql">带参数的sql语句,格式必须为"select count(1) from ..."</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>是否存在 </returns>
        public bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            object obj = GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }

            return (cmdresult != 0);
        }
        #endregion

        #region  执行简单SQL语句

        /// <summary>
        /// 执行简单SQL语句
        /// </summary>
        /// <remarks>该方法中不含事务处理，建议执行单条语句时采用</remarks>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {

                        connection.Open();
                        cmd.CommandTimeout = SqlTimeOutNumber;
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 延时执行Sql语句
        /// </summary>
        /// <param name="SQLString">sql语句</param>
        /// <param name="Times">延时数（毫秒）</param>
        /// <returns>影响的记录数</returns> 
        public int ExecuteSqlByTime(string SQLString, int Times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = Times;
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句集合</param>	
        /// <param name="rollBackStepNo">
        /// 输出回滚处的步号:小于0=异常回滚（‘-’号表示异常的行数）,0=成功提交，无回滚，>1=第几条语句回滚
        /// </param>
        /// <returns>是否执行成功：true=事务成功提交，false=事务回滚</returns>
        public bool ExecuteSqlTran(List<String> SQLStringList, out int rollBackStepNo)
        {
            rollBackStepNo = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = SqlTimeOutNumber;
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;

                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw e;
                }
            }

            return true;
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句集合</param>	
        /// <param name="isZeroRollback">是否影响为0时回滚</param>
        /// <param name="rollBackStepNo">
        /// 输出回滚处的步号:小于0=异常回滚（‘-’号表示异常的行数）,0=成功提交，无回滚，>1=第几条语句回滚
        /// </param>
        /// <returns>是否执行成功：true=事务成功提交，false=事务回滚</returns>
        public bool ExecuteSqlTran(List<String> SQLStringList, bool isZeroRollback, out int rollBackStepNo)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;

                rollBackStepNo = -1;
                int n = 0;
                try
                {
                    for (n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandTimeout = SqlTimeOutNumber;
                            cmd.CommandText = strsql;
                            cmd.CommandType = CommandType.Text;
                            int rowCount = cmd.ExecuteNonQuery();
                            if (isZeroRollback)
                            {
                                //受影响为0，回滚
                                if (rowCount == 0)
                                {
                                    tx.Rollback();
                                    rollBackStepNo = n + 1;
                                    break;
                                }
                            }
                        }
                    }
                    tx.Commit();
                    rollBackStepNo = 0;
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    rollBackStepNo = -(n + 1);
                    throw e;
                }

                return rollBackStepNo == 0;
            }
        }

        /// <summary>
        /// 执行带一个存储过程参数的非查询SQL语句，返回影响行数。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SQLString, string content)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(SQLString, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.CommandTimeout = SqlTimeOutNumber;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行带一个存储过程参数的查询SQL语句，返回查询结果(object)。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>查询结果(object)</returns>
        public object ExecuteSqlGet(string SQLString, string content)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(SQLString, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.CommandTimeout = SqlTimeOutNumber;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 向数据库里插入图像格式的字段
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(strSQL, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@fs", SqlDbType.Image);
                myParameter.Value = fs;
                cmd.CommandTimeout = SqlTimeOutNumber;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        /// <exception cref="SqlException"></exception>
        public object GetSingle(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = SqlTimeOutNumber;
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 延时执行一条查询结果语句，返回查询结果（object）
        /// </summary>
        /// <param name="SQLString">查询结果语句</param>
        /// <param name="Times">查询结果（object）</param>
        /// <returns></returns>
        public object GetSingle(string SQLString, int Times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = Times;
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ExecuteReader(string strSQL)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(strSQL, connection);
            try
            {
                connection.Open();
                cmd.CommandTimeout = SqlTimeOutNumber;
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string SQLString)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlDataAdapter command = new SqlDataAdapter(SQLString, connection))
                    {
                        command.Fill(ds, "ds");
                    } 
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return ds;
        }

        /// <summary>
        /// 延时执行sql语句，返回DataSet数据集
        /// </summary>
        /// <param name="SQLString">sql语句</param>
        /// <param name="Times">延时数（毫秒）</param>
        /// <returns>查询结果数据集</returns>
        public DataSet Query(string SQLString, int Times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.SelectCommand.CommandTimeout = Times;
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }



        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行带多参数的SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.CommandTimeout = SqlTimeOutNumber;
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        cmd.CommandTimeout = SqlTimeOutNumber;
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的泛型集合</param>
        /// <returns>受影响行数合计</returns>
        public int ExecuteSqlTran(System.Collections.Generic.List<CommandInfo> cmdList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandTimeout = SqlTimeOutNumber;
                    try
                    {
                        int count = 0;
                        //循环
                        foreach (CommandInfo myDE in cmdList)
                        {
                            string cmdText = myDE.CommandText;
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Parameters;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);

                            if (myDE.EffentNextType == EffentNextType.WhenHaveContine || myDE.EffentNextType == EffentNextType.WhenNoHaveContine)
                            {
                                if (myDE.CommandText.ToLower().IndexOf("count(") == -1)
                                {
                                    trans.Rollback();
                                    return 0;
                                }

                                object obj = cmd.ExecuteScalar();
                                bool isHave = false;
                                if (obj == null && obj == DBNull.Value)
                                {
                                    isHave = false;
                                }
                                isHave = Convert.ToInt32(obj) > 0;

                                if (myDE.EffentNextType == EffentNextType.WhenHaveContine && !isHave)
                                {
                                    trans.Rollback();
                                    return 0;
                                }
                                if (myDE.EffentNextType == EffentNextType.WhenNoHaveContine && isHave)
                                {
                                    trans.Rollback();
                                    return 0;
                                }
                                continue;
                            }
                            int val = cmd.ExecuteNonQuery();
                            count += val;
                            if (myDE.EffentNextType == EffentNextType.ExcuteEffectRows && val == 0)
                            {
                                trans.Rollback();
                                return 0;
                            }
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                        return count;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的泛型集合</param>
        public void ExecuteSqlTranWithIndentity(System.Collections.Generic.List<CommandInfo> SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        cmd.CommandTimeout = SqlTimeOutNumber;
                        int indentity = 0;
                        //循环
                        foreach (CommandInfo myDE in SQLStringList)
                        {
                            string cmdText = myDE.CommandText;
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Parameters;
                            foreach (SqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.InputOutput)
                                {
                                    q.Value = indentity;
                                }
                            }
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            foreach (SqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.Output)
                                {
                                    indentity = Convert.ToInt32(q.Value);
                                }
                            }
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public void ExecuteSqlTranWithIndentity(Hashtable SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        cmd.CommandTimeout = SqlTimeOutNumber;
                        int indentity = 0;
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            foreach (SqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.InputOutput)
                                {
                                    q.Value = indentity;
                                }
                            }
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            foreach (SqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.Output)
                                {
                                    indentity = Convert.ToInt32(q.Value);
                                }
                            }
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条带参数的计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>查询结果（object）</returns>
        public object GetSingle(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.CommandTimeout = SqlTimeOutNumber;
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandTimeout = SqlTimeOutNumber;
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
            //			finally
            //			{
            //				cmd.Dispose();
            //				connection.Close();
            //			}	

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <remarks>DataSet中填充的DataTable为“ds”</remarks>
        /// <param name="SQLString">查询语句</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = SqlTimeOutNumber;
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }

        /// <summary>
        /// 准备参数
        /// </summary>
        /// <param name="cmd">sql命令执行对象</param>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="trans">事务</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParms">参数列表</param>
        private void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {


                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        #endregion

        #region 存储过程操作
        /// <summary>
        /// 执行存储过程返回DataTable
        /// </summary>
        /// <param name="storedProcName">过程名</param>
        /// <param name="dic">参数</param>
        /// <returns></returns>
        public DataTable RunProcedure_Table(string storedProcName, Dictionary<string, object> dic)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataTable dt = new DataTable();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, GetSqlParameter(dic));
                sqlDA.Fill(dt);
                connection.Close();
                return dt;
            }
        }

        /// <summary>
        /// 执行过程返回datatable，cgbz，gcts
        /// </summary>
        /// <param name="storedProcName">过程名</param>
        /// <param name="dic">参数</param>
        /// <param name="cgbz">成功标志</param>
        /// <param name="gcts">过程提示</param>
        /// <returns></returns>
        public DataTable RunProcedure_Table(string storedProcName, Dictionary<string, object> dic, out int cgbz, out string gcts)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataTable dt = new DataTable();
                connection.Open();
                List<string> strs = new List<string>();
                strs.Add("@cgbz");
                strs.Add("@gcts");
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                SqlParameter[] sps = GetSqlParameter(dic, strs);
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, sps);
                sqlDA.Fill(dt);
                connection.Close();
                cgbz = Convert.ToInt32(sps[0].Value==DBNull.Value?1: sps[0].Value);
                gcts = Convert.ToString(sps[1].Value== DBNull.Value?"": sps[1].Value);
                return dt;
            }
        }

        /// <summary>
        /// 执行过程返回datatable及output内容
        /// </summary>
        /// <param name="storedProcName">过程名</param>
        /// <param name="dic">参数列表</param>
        /// <param name="outList">返回参数的集合</param>
        /// <param name="outputDic">返回参数字典</param>
        /// <returns></returns>
        public DataTable RunProcdure_Table(string storedProcName, Dictionary<string, object> dic, List<string> outList, out Dictionary<string, object> outputDic)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataTable dt = new DataTable();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                SqlParameter[] sps = GetSqlParameter(dic, outList);
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, sps);
                sqlDA.Fill(dt);
                connection.Close();
                outputDic = new Dictionary<string, object>();
                foreach (string str in outList)
                {
                    outputDic.Add(str, sps.FirstOrDefault(y => y.ParameterName == str).Value);
                }
                return dt;
            }
        }

        /// <summary>
        /// 执行过程返回datatable及output内容
        /// </summary>
        /// <param name="storedProcName">过程名</param>
        /// <param name="dic">参数列表</param>
        /// <param name="outList">返回参数的集合</param>
        /// <param name="outputDic">返回参数字典</param>
        /// <returns></returns>
        public DataTable RunProcdure_Table(string storedProcName, Dictionary<string, object> dic, List<string> outList,List<string> inoutList, out Dictionary<string, object> outputDic)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataTable dt = new DataTable();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                SqlParameter[] sps = GetSqlParameter(dic, outList, inoutList);
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, sps);
                sqlDA.Fill(dt);
                connection.Close();
                outputDic = new Dictionary<string, object>();
                foreach (string str in outList)
                {
                    outputDic.Add(str, sps.FirstOrDefault(y => y.ParameterName == str).Value);
                }
                return dt;
            }
        }

        /// <summary>
        /// 执行存储过程返回DataSet
        /// </summary>
        /// <param name="storedProcName">存储过程</param>
        /// <param name="dicParameter">参数字典</param>
        /// <returns></returns>
        public DataSet RunProcedure(string storedProcName, Dictionary<string, object> dicParameter)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                connection.Open();
                using (SqlDataAdapter sqlData = new SqlDataAdapter())
                {
                    sqlData.SelectCommand = BuildQueryCommand(connection, storedProcName, GetSqlParameter(dicParameter));
                    sqlData.Fill(ds);

                }
                connection.Close();
                return ds;
            }
        }

        /// <summary>
        /// 执行存储过程，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataReader returnReader;
            connection.Open();
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return returnReader;
        }

        /// <summary>
        /// 执行存储过程，返回DataSet对象
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }

        /// <summary>
        /// 延时执行存储过程，返回DataSet对象
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <param name="Times">延时数（毫秒）</param>
        /// <returns>DataSet</returns>
        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int Times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.SelectCommand.CommandTimeout = Times;
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }

        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandTimeout = SqlTimeOutNumber;
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }

        /// <summary>
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns>存储过程返回值</returns>
        public bool RunProcedure(string storedProcName, IDataParameter[] parameters, out string returnValue)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int rowsAffected = 0;
                connection.Open();
                SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                returnValue = "执行成功！";//(string)command.Parameters["ReturnValue"].Value;
                return rowsAffected > 0;
            }
        }

        /// <summary>
        /// 执行存储过程	
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        public void RunProcedure_Direct(string storedProcName, IDataParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        /// <summary>
        /// 创建 SqlCommand 对象实例(用来返回一个整数值)	
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        private SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        #endregion


    }

    class IniFile
    {
        private string m_FileName;
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get { return m_FileName; }
            set { m_FileName = value; }
        }

        /// <summary>
        /// 获取键值
        /// </summary>
        /// <param name="lpAppName">节</param>
        /// <param name="lpKeyName">键</param>
        /// <param name="nDefault">默认值</param>
        /// <param name="lpFileName">文件名</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileInt(
            string lpAppName,
            string lpKeyName,
            int nDefault,
            string lpFileName
            );

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="lpAppName">节</param>
        /// <param name="lpKeyName">键</param>
        /// <param name="lpDefault">默认值</param>
        /// <param name="lpReturnedString">返回字符串</param>
        /// <param name="nSize">返回字符串宽度</param>
        /// <param name="lpFileName">问渐渐</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpDefault,
            StringBuilder lpReturnedString,
            int nSize,
            string lpFileName
            );

        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="lpAppName">节</param>
        /// <param name="lpKeyName">键</param>
        /// <param name="lpString">值</param>
        /// <param name="lpFileName">文件</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern int WritePrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpString,
            string lpFileName
            );

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="aFileName">Ini文件路径</param>
        public IniFile(string aFileName)
        {
            this.m_FileName = aFileName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public IniFile()
        { }

        /// <summary>
        /// [扩展]读Int数值
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="name">键</param>
        /// <param name="def">默认值</param>
        /// <returns></returns>
        public int ReadInt(string section, string name, int def)
        {
            return GetPrivateProfileInt(section, name, def, this.m_FileName);
        }

        /// <summary>
        /// [扩展]读取string字符串
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="name">键</param>
        /// <param name="def">默认值</param>
        /// <returns></returns>
        public string ReadString(string section, string name, string def)
        {
            StringBuilder vRetSb = new StringBuilder(2048);
            GetPrivateProfileString(section, name, def, vRetSb, 2048, this.m_FileName);
            return vRetSb.ToString();
        }

        /// <summary>
        /// [扩展]写入Int数值，如果不存在 节-键，则会自动创建
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="name">键</param>
        /// <param name="Ival">写入值</param>
        public void WriteInt(string section, string name, int Ival)
        {

            WritePrivateProfileString(section, name, Ival.ToString(), this.m_FileName);
        }

        /// <summary>
        /// [扩展]写入String字符串，如果不存在 节-键，则会自动创建
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="name">键</param>
        /// <param name="strVal">写入值</param>
        public void WriteString(string section, string name, string strVal)
        {
            WritePrivateProfileString(section, name, strVal, this.m_FileName);
        }

        /// <summary>
        /// 删除指定的 节
        /// </summary>
        /// <param name="section"></param>
        public void DeleteSection(string section)
        {
            WritePrivateProfileString(section, null, null, this.m_FileName);
        }

        /// <summary>
        /// 删除全部 节
        /// </summary>
        public void DeleteAllSection()
        {
            WritePrivateProfileString(null, null, null, this.m_FileName);
        }

        /// <summary>
        /// 读取指定 节-键 的值
        /// </summary>
        /// <param name="section"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string IniReadValue(string section, string name)
        {
            StringBuilder strSb = new StringBuilder(256);
            GetPrivateProfileString(section, name, "", strSb, 256, this.m_FileName);
            return strSb.ToString();
        }

        /// <summary>
        /// 写入指定值，如果不存在 节-键，则会自动创建
        /// </summary>
        /// <param name="section"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void IniWriteValue(string section, string name, string value)
        {
            WritePrivateProfileString(section, name, value, this.m_FileName);
        }
    }
}
