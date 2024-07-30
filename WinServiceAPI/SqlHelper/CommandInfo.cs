using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SqlHelper
{
    #region 事务影响枚举
    /// <summary>
    /// 事务影响枚举
    /// 枚举影响事务执行的各种异常情况
    /// </summary>
    public enum EffentNextType
    {
        /// <summary>
        /// 对其他语句无任何影响 
        /// </summary>
        /// <remarks>对其他语句无任何影响</remarks>
        None,
        /// <summary>
        /// 当前语句必须为"select count(1) from .."格式，如果存在则继续执行，不存在回滚事务
        /// </summary>
        /// <remarks>当前语句必须为"select count(1) from .."格式，如果存在则继续执行，不存在回滚事务</remarks>
        WhenHaveContine,
        /// <summary>
        /// 当前语句必须为"select count(1) from .."格式，如果不存在则继续执行，存在回滚事务
        /// </summary>
        /// <remarks>当前语句必须为"select count(1) from .."格式，如果不存在则继续执行，存在回滚事务</remarks>
        WhenNoHaveContine,
        /// <summary>
        /// 当前语句影响到的行数必须大于0，否则回滚事务
        /// </summary>
        /// <remarks>当前语句影响到的行数必须大于0，否则回滚事务</remarks>
        ExcuteEffectRows,
        /// <summary>
        /// 引发事件-当前语句必须为"select count(1) from .."格式，如果不存在则继续执行，存在回滚事务
        /// </summary>
        /// <remarks>引发事件-当前语句必须为"select count(1) from .."格式，如果不存在则继续执行，存在回滚事务</remarks>
        SolicitationEvent
    }
    #endregion

    /// <summary>
    /// 连接类型
    /// </summary>
    public enum ConnectType
    {
        wqserver,
        wqemr,
        wqyz,
        ybjkdata
    }

    #region 数据库访问命令信息辅助类
    /// <summary>
    /// 数据库访问命令信息辅助类
    /// </summary>
    public class CommandInfo
    {
        public object ShareObject = null;
        public object OriginalData = null;

        #region 事件委托
        event EventHandler _solicitationEvent;
        public event EventHandler SolicitationEvent
        {
            add
            {
                _solicitationEvent += value;
            }
            remove
            {
                _solicitationEvent -= value;
            }
        }
        public void OnSolicitationEvent()
        {
            if (_solicitationEvent != null)
            {
                _solicitationEvent(this, new EventArgs());
            }
        }
        #endregion

        public string CommandText;
        public System.Data.Common.DbParameter[] Parameters;
        public EffentNextType EffentNextType = EffentNextType.None;

        #region 构造函数
        public CommandInfo()
        {

        }

        public CommandInfo(string sqlText, SqlParameter[] para)
        {
            this.CommandText = sqlText;
            this.Parameters = para;
        }

        public CommandInfo(string sqlText, SqlParameter[] para, EffentNextType type)
        {
            this.CommandText = sqlText;
            this.Parameters = para;
            this.EffentNextType = type;
        }
        #endregion
    }
    #endregion
}
