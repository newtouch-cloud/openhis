using SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace wqsj_PlatForm_DAS
{
    public interface IPlatFormService
    {
        #region 执行sql语句
        /// <summary>
        /// 执行无参sql语句
        /// </summary>
        /// <param name="SQLString"></param>
        /// <returns></returns>
        DataSet Query(string SQLString);

        #endregion

        #region 执行存储过程
        #region wqserver
        /// <summary>
        /// 执行过程仅仅返回DataTable
        /// </summary>
        /// <param name="procName">过程名</param>
        /// <param name="dic">参数</param>
        /// <returns></returns>
        DataTable RunProc_DataTable_WqServer(string procName, Dictionary<string, object> dic);

        /// <summary>
        /// 执行过程返回DataTable,cgbz,gcts
        /// </summary>
        /// <param name="procName">过程名</param>
        /// <param name="dic">参数</param>
        /// <param name="cgbz">成功标志</param>
        /// <param name="gcts">过程提示</param>
        /// <returns></returns>
        DataTable RunProc_DataTable_Wqserver(string procName, Dictionary<string, object> dic, out int cgbz, out string gcts);

        /// <summary>
        /// 执行过程返回datatable及output内容
        /// </summary>
        /// <param name="storedProcName">过程名</param>
        /// <param name="dic">参数列表</param>
        /// <param name="outList">返回参数的集合</param>
        /// <param name="outputDic">返回参数字典</param>
        /// <returns></returns>
        DataTable RunProc_DataTable_Wqserver(string procName, Dictionary<string, object> dic,List<string> outList, out Dictionary<string, object> outputDic);
        /// <summary>
        /// 执行过程仅仅返回DataSet
        /// </summary>
        /// <param name="connectType">过程所在数据库</param>
        /// <param name="procName">过程名</param>
        /// <param name="dic">参数</param>
        /// <returns></returns>
        DataSet RunProc_DataSet_Wqserver(string procName, Dictionary<string, object> dic);

        /// <summary>
        /// 执行过程返回DataSet,cgbz,gcts
        /// </summary>
        /// <param name="procName">过程名</param>
        /// <param name="dic">参数</param>
        /// <param name="cgbz">成功标志</param>
        /// <param name="gcts">过程提示</param>
        /// <returns></returns>
        DataSet RunProc_DataSet_Wqserver(string procName, Dictionary<string, object> dic, out int cgbz, out string gcts);
        #endregion

        #region wqemr
        /// <summary>
        /// 执行过程仅仅返回DataTable
        /// </summary>
        /// <param name="procName">过程名</param>
        /// <param name="dic">参数</param>
        /// <returns></returns>
        DataTable RunProc_DataTable_WqEmr(string procName, Dictionary<string, object> dic);

        /// <summary>
        /// 执行过程返回DataTable,cgbz,gcts
        /// </summary>
        /// <param name="procName">过程名</param>
        /// <param name="dic">参数</param>
        /// <param name="cgbz">成功标志</param>
        /// <param name="gcts">过程提示</param>
        /// <returns></returns>
        DataTable RunProc_DataTable_WqEmr(string procName, Dictionary<string, object> dic, out int cgbz, out string gcts);

        /// <summary>
        /// 执行过程返回datatable及output内容
        /// </summary>
        /// <param name="storedProcName">过程名</param>
        /// <param name="dic">参数列表</param>
        /// <param name="outList">返回参数的集合</param>
        /// <param name="outputDic">返回参数字典</param>
        /// <returns></returns>
        DataTable RunProc_DataTable_WqEmr(string procName, Dictionary<string, object> dic, List<string> outList, out Dictionary<string, object> outputDic);

        /// <summary>
        /// 执行过程仅仅返回DataSet
        /// </summary>
        /// <param name="connectType">过程所在数据库</param>
        /// <param name="procName">过程名</param>
        /// <param name="dic">参数</param>
        /// <returns></returns>
        DataSet RunProc_DataSet_WqEmr(string procName, Dictionary<string, object> dic);

        /// <summary>
        /// 执行过程返回DataSet,cgbz,gcts
        /// </summary>
        /// <param name="procName">过程名</param>
        /// <param name="dic">参数</param>
        /// <param name="cgbz">成功标志</param>
        /// <param name="gcts">过程提示</param>
        /// <returns></returns>
        DataSet RunProc_DataSet_WqEmr(string procName, Dictionary<string, object> dic, out int cgbz, out string gcts);
        #endregion
        #endregion

        #region 操作服务器文件
        #endregion

    }
}
