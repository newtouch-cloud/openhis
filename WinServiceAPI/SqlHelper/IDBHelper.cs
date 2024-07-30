using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SqlHelper
{
    public interface IDBHelper
    {
        #region 公用方法
        /// <summary>
        /// 判断是否存在某表的某个字段
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="columnName">列名称</param>
        /// <returns>是否存在</returns>
        bool ColumnExists(string tableName, string columnName);

        /// <summary>
        /// 获取某表整数列中的最大值
        /// </summary>
        /// <param name="FieldName">列名称</param>
        /// <param name="TableName">表名称</param>
        /// <returns>该整数列中的最大值</returns>
        int GetMaxID(string FieldName, string TableName);

        /// <summary>
        /// 通过sql语句判断表中是否存在记录
        /// </summary>
        /// <param name="strSql">sql语句,格式必须为"select count(1) from ..."</param>
        /// <returns>是否存在</returns>
        bool Exists(string strSql);

        /// <summary>
        /// 判断指定表是否存在
        /// </summary>
        /// <param name="TableName">表名称</param>
        /// <returns>是否存在</returns>
        bool TabExists(string TableName);

        /// <summary>
        /// 通过带参数的sql语句判断表中是否存在记录
        /// </summary>
        /// <param name="strSql">带参数的sql语句,格式必须为"select count(1) from ..."</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>是否存在 </returns>
        bool Exists(string strSql, params SqlParameter[] cmdParms);
        #endregion

        #region 执行简单SQL语句
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        int ExecuteSql(string SQLString);

        /// <summary>
        /// 延时执行Sql语句
        /// </summary>
        /// <param name="SQLString">sql语句</param>
        /// <param name="Times">延时数（毫秒）</param>
        /// <returns>影响的记录数</returns>
        int ExecuteSqlByTime(string SQLString, int Times);

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>	
        /// <returns>受影响行数合计</returns>
        bool ExecuteSqlTran(List<String> SQLStringList, out int rollBackStepNo);

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>	
        /// <param name="isZeroRollback">影响为0时是否回滚</param>
        /// <returns>受影响行数合计</returns>
        bool ExecuteSqlTran(List<String> SQLStringList, bool isZeroRollback, out int rollBackStepNo);

        /// <summary>
        /// 执行带一个存储过程参数的非查询SQL语句，返回影响行数。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        int ExecuteSql(string SQLString, string content);

        /// <summary>
        /// 执行带一个存储过程参数的查询SQL语句，返回查询结果(object)。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>查询结果(object)</returns>
        object ExecuteSqlGet(string SQLString, string content);

        /// <summary>
        /// 向数据库里插入图像格式的字段
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        int ExecuteSqlInsertImg(string strSQL, byte[] fs);

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        /// <exception cref="SqlException"></exception>
        object GetSingle(string SQLString);

        /// <summary>
        /// 延时执行一条查询结果语句，返回查询结果（object）
        /// </summary>
        /// <param name="SQLString">查询结果语句</param>
        /// <param name="Times">查询结果（object）</param>
        /// <returns></returns>
        object GetSingle(string SQLString, int Times);

        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        SqlDataReader ExecuteReader(string strSQL);

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        DataSet Query(string SQLString);

        /// <summary>
        /// 延时执行sql语句，返回DataSet数据集
        /// </summary>
        /// <param name="SQLString">sql语句</param>
        /// <param name="Times">延时数（毫秒）</param>
        /// <returns>查询结果数据集</returns>
        DataSet Query(string SQLString, int Times);
        #endregion

        #region 执行带参数的SQL语句
        /// <summary>
        /// 执行带多参数的SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>影响的记录数</returns>
        int ExecuteSql(string SQLString, params SqlParameter[] cmdParms);

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        void ExecuteSqlTran(Hashtable SQLStringList);

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的泛型集合</param>
        /// <returns>受影响行数合计</returns>
        int ExecuteSqlTran(System.Collections.Generic.List<CommandInfo> cmdList);

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的泛型集合</param>
        void ExecuteSqlTranWithIndentity(System.Collections.Generic.List<CommandInfo> SQLStringList);

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        void ExecuteSqlTranWithIndentity(Hashtable SQLStringList);

        /// <summary>
        /// 执行一条带参数的计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>查询结果（object）</returns>
        object GetSingle(string SQLString, params SqlParameter[] cmdParms);

        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>SqlDataReader</returns>
        SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms);

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <remarks>DataSet中填充的DataTable为“ds”</remarks>
        /// <param name="SQLString">查询语句</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>DataSet</returns>
        DataSet Query(string SQLString, params SqlParameter[] cmdParms);


        #endregion

        #region 存储过程操作
        /// <summary>
        /// 执行存储过程，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters);

        /// <summary>
        /// 执行存储过程，返回DataSet对象
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName);

        /// <summary>
        /// 延时执行存储过程，返回DataSet对象
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <param name="Times">延时数（毫秒）</param>
        /// <returns>DataSet</returns>
        DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int Times);

        /// <summary>
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns>存储过程返回值</returns>
        bool RunProcedure(string storedProcName, IDataParameter[] parameters, out string returnValue);


        #endregion
    }
}
