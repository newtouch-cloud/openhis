using SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace wqsj_PlatForm_DAS
{
    public class PlatFormService : IPlatFormService
    {
        private WqserverDbHelper wqserver;

        private WqemrDBHelper wqemr;

        public PlatFormService()
        {
            wqserver = new WqserverDbHelper();
            wqemr = new WqemrDBHelper();
        }

        #region 执行sql 语句
        public string GetServerAddress()
        {
            return wqserver.serverAddress;
        }


        /// <summary>
        /// 执行无参sql语句
        /// </summary>
        /// <param name="SQLString">sql语句</param>
        /// <returns></returns>
        public DataSet Query(string SQLString)
        {
            return wqserver.Query(SQLString);
        
        }
        /// <summary>
        /// 执行简单SQL语句
        /// </summary>
        /// <remarks>该方法中不含事务处理，建议执行单条语句时采用</remarks>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SQLString)
        {
            return wqserver.ExecuteSql(SQLString);
        }

       
        /// <summary>
        /// 合并执行多条sql语句
        /// </summary>
        /// <param name="SQLStringList">sql语句集合</param>
        /// <returns>受影响行数</returns>
        public bool Merge(List<string> SQLStringList, out int errorNo)
        {
            return wqserver.ExecuteSqlTran(SQLStringList, out errorNo);
        }


        #endregion

        #region 执行存储过程
        #region wqserver


        /// <summary>
        /// 执行过程仅仅返回DataTable
        /// </summary>
        /// <param name="connectType">过程所在数据库</param>
        /// <param name="procName">过程名</param>
        /// <param name="dic">参数</param>
        /// <returns></returns>
        public DataTable RunProc_DataTable_WqServer(string procName, Dictionary<string, object> dic)
        {
            return wqserver.RunProcedure_Table(procName, dic);
        }

        /// <summary>
        /// 执行过程返回DataTable,cgbz,gcts
        /// </summary>
        /// <param name="procName">过程名</param>
        /// <param name="dic">参数</param>
        /// <param name="cgbz">成功标志</param>
        /// <param name="gcts">过程提示</param>
        /// <returns></returns>
        public DataTable RunProc_DataTable_Wqserver(string procName, Dictionary<string, object> dic, out int cgbz, out string gcts)
        {
            return wqserver.RunProcedure_Table(procName, dic, out cgbz, out gcts);
        }


        /// <summary>
        /// 执行过程返回datatable及output内容
        /// </summary>
        /// <param name="storedProcName">过程名</param>
        /// <param name="dic">参数列表</param>
        /// <param name="outList">返回参数的集合</param>
        /// <param name="outputDic">返回参数字典</param>
        /// <returns></returns>
        public DataTable RunProc_DataTable_Wqserver(string procName, Dictionary<string, object> dic, List<string> outList, out Dictionary<string, object> outputDic)
        {
            return wqserver.RunProcdure_Table(procName, dic, outList, out outputDic);
        }

        /// <summary>
        /// 执行过程返回datatable及output内容
        /// </summary>
        /// <param name="storedProcName">过程名</param>
        /// <param name="dic">参数列表</param>
        /// <param name="outList">返回参数的集合</param>
        /// <param name="outputDic">返回参数字典</param>
        /// <returns></returns>
        public DataTable RunProc_DataTable_Wqserver(string procName, Dictionary<string, object> dic, List<string> outList, List<string> inoutList, out Dictionary<string, object> outputDic)
        {
            return wqserver.RunProcdure_Table(procName, dic, outList, inoutList, out outputDic);
        }

        /// <summary>
        /// 执行过程仅仅返回DataSet
        /// </summary>
        /// <param name="connectType">过程所在数据库</param>
        /// <param name="procName">过程名</param>
        /// <param name="dic">参数</param>
        /// <returns></returns>
        public DataSet RunProc_DataSet_Wqserver(string procName, Dictionary<string, object> dic)
        {
            return wqserver.RunProcedure(procName, wqserver.GetSqlParameter(dic),"ds");
        }

        /// <summary>
        /// 执行过程返回DataSet,cgbz,gcts
        /// </summary>
        /// <param name="procName">过程名</param>
        /// <param name="dic">参数</param>
        /// <param name="cgbz">成功标志</param>
        /// <param name="gcts">过程提示</param>
        /// <returns></returns>
        public DataSet RunProc_DataSet_Wqserver(string procName, Dictionary<string, object> dic, out int cgbz, out string gcts)
        {
            List<string> outStrs = new List<string>();
            outStrs.Add("@cgbz");
            outStrs.Add("@gcts");
            SqlParameter[] sps = wqserver.GetSqlParameter(dic, outStrs);
            DataSet ds= wqserver.RunProcedure(procName, sps,"ds");
            cgbz = Convert.ToInt32(sps[0].Value);
            gcts = Convert.ToString(sps[1].Value);
            return ds;

        }

        /// <summary>
        /// 查询数据（存储过程）
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">参数列表</param>
        /// <param name="tableName">DataSet中填充的DataTable名称</param>
        /// <returns>DataSet</returns>
        public System.Data.DataSet Query(string storedProcName, System.Data.IDataParameter[] parameters, string tableName)
        {
            return wqserver.RunProcedure(storedProcName, parameters, tableName);
        }

        /// <summary>
        /// 执行存储过程（无返回值）
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">参数列表</param>
        public void RunProc_Direct_Wqserver(string storedProcName, IDataParameter[] parameters)
        {
            wqserver.RunProcedure_Direct(storedProcName, parameters);
        }

        #endregion

        #region wqemr
        /// <summary>
        /// 执行过程仅仅返回DataTable
        /// </summary>
        /// <param name="connectType">过程所在数据库</param>
        /// <param name="procName">过程名</param>
        /// <param name="dic">参数</param>
        /// <returns></returns>
        public DataTable RunProc_DataTable_WqEmr(string procName, Dictionary<string, object> dic)
        {
            return wqemr.RunProcedure_Table(procName, dic);
        }

        /// <summary>
        /// 执行过程返回DataTable,cgbz,gcts
        /// </summary>
        /// <param name="procName">过程名</param>
        /// <param name="dic">参数</param>
        /// <param name="cgbz">成功标志</param>
        /// <param name="gcts">过程提示</param>
        /// <returns></returns>
        public DataTable RunProc_DataTable_WqEmr(string procName, Dictionary<string, object> dic, out int cgbz, out string gcts)
        {
            return wqemr.RunProcedure_Table(procName, dic, out cgbz, out gcts);
        }

        /// <summary>
        /// 执行过程返回datatable及output内容
        /// </summary>
        /// <param name="storedProcName">过程名</param>
        /// <param name="dic">参数列表</param>
        /// <param name="outList">返回参数的集合</param>
        /// <param name="outputDic">返回参数字典</param>
        /// <returns></returns>
        public DataTable RunProc_DataTable_WqEmr(string procName, Dictionary<string, object> dic, List<string> outList, out Dictionary<string, object> outputDic)
        {
            return wqemr.RunProcdure_Table(procName, dic, outList,out outputDic);
        }

        /// <summary>
        /// 执行过程仅仅返回DataSet
        /// </summary>
        /// <param name="connectType">过程所在数据库</param>
        /// <param name="procName">过程名</param>
        /// <param name="dic">参数</param>
        /// <returns></returns>
        public DataSet RunProc_DataSet_WqEmr(string procName, Dictionary<string, object> dic)
        {
            return wqemr.RunProcedure(procName, wqserver.GetSqlParameter(dic), "ds");
        }

        /// <summary>
        /// 执行过程返回DataSet,cgbz,gcts
        /// </summary>
        /// <param name="procName">过程名</param>
        /// <param name="dic">参数</param>
        /// <param name="cgbz">成功标志</param>
        /// <param name="gcts">过程提示</param>
        /// <returns></returns>
        public DataSet RunProc_DataSet_WqEmr(string procName, Dictionary<string, object> dic, out int cgbz, out string gcts)
        {
            List<string> outStrs = new List<string>();
            outStrs.Add("@cgbz");
            outStrs.Add("@gcts");
            SqlParameter[] sps = wqserver.GetSqlParameter(dic, outStrs);
            DataSet ds = wqemr.RunProcedure(procName, sps, "ds");
            cgbz = Convert.ToInt32(sps[0].Value);
            gcts = Convert.ToString(sps[1].Value);
            return ds;

        }
        #endregion
        #endregion
    }
}
