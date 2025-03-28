using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.OR.ManageSystem.Infrastructure
{
    /// <summary>
    /// 常用到的（放Repo、DmnService都不合适的，但又要请求数据库的）
    /// 比如获取唯一编号（自增，但有不能重复）
    /// </summary>
    public class EFDBBaseFuncHelper
    {
        private EFDBBaseFuncHelper(IDatabaseFactory databaseFactory)
        {
            this._databaseFactory = databaseFactory;
        }

        private IDatabaseFactory _databaseFactory;

        /// <summary>
        /// 默认DB上下文
        /// </summary>
        public static EFDBBaseFuncHelper Instance
        {
            get
            {
                IDatabaseFactory databaseFactory
                    = DependencyDefaultInstanceResolver.GetInstance<IDefaultDatabaseFactory>();
                return new EFDBBaseFuncHelper(databaseFactory);
            }
        }

        /// <summary>
        /// 指定DB上下文
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static EFDBBaseFuncHelper GetInstance(EnumDBFactoryType? dbType = null)
        {
            if (!dbType.HasValue)
            {
                dbType = EnumDBFactoryType.DefaultDB;  //默认
            }
            IDatabaseFactory databaseFactory = null;
            switch (dbType)
            {
                case EnumDBFactoryType.DefaultDB:
                    databaseFactory = DependencyDefaultInstanceResolver.GetInstance<IDefaultDatabaseFactory>();
                    break;
            }
            return new EFDBBaseFuncHelper(databaseFactory);
        }

        #region 方法

        //-------------------------------------示例------------------------------------------------
        ///// <summary>
        ///// 获取主键字段唯一Id
        ///// </summary>
        ///// <param name="tableName"></param>
        ///// <returns></returns>
        //public string GetNewPrimaryKeyString(string tableName)
        //{
        //    var sqlParList = new List<SqlParameter>();
        //    sqlParList.Add(new SqlParameter("@tableName", tableName));

        //    var outParameter = new SqlParameter("@NumStr", System.Data.SqlDbType.VarChar, 8);
        //    outParameter.Direction = System.Data.ParameterDirection.Output;
        //    sqlParList.Add(outParameter);

        //    _databaseFactory.Get().Database.ExecuteSqlCommand("exec spGetNewID @tableName, @NumStr out", sqlParList.ToArray());

        //    return outParameter.Value.ToString();
        //}

        /// <summary>
        /// 获取 字段/业务 唯一值
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="orgId"></param>
        /// <param name="initFormat"></param>
        /// <param name="dateFlag"></param>
        /// <returns>string类型</returns>
        public string GetNewFieldUniqueValue(string fieldName, string orgId, string initFormat = "", bool dateFlag = false)
        {
            var tpl = GetNewFieldUniqueObjectValue(fieldName, orgId, initFormat, dateFlag);

            if (tpl.Item2 == null || string.IsNullOrWhiteSpace(tpl.Item2.ToString()))
            {
                return tpl.Item1.ToString();
            }
            else
            {
                return string.Format(tpl.Item2.ToString(), Convert.ToInt32(tpl.Item1));
            }
        }


        /// <summary>
        /// 获取 字段/业务 唯一值 返回Tuple
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="orgId"></param>
        /// <param name="initFormat"></param>
        /// <param name="dateFlag"></param>
        /// <returns></returns>
        private Tuple<object, object> GetNewFieldUniqueObjectValue(string fieldName, string orgId, string initFormat = "", bool dateFlag = false)
        {
            var sqlParList = new List<SqlParameter>();

            sqlParList.Add(new SqlParameter("@OrganizeId", orgId));
            sqlParList.Add(new SqlParameter("@TopOrganizeId", Constants.TopOrganizeId));
            sqlParList.Add(new SqlParameter("@FieldName", fieldName));
            sqlParList.Add(new SqlParameter("@DateFlag", dateFlag));
            sqlParList.Add(new SqlParameter("@currDate", DateTime.Now.ToString("yyyyMMdd")));
            sqlParList.Add(new SqlParameter("@InitFormat", initFormat));
            var outParameter1 = new SqlParameter("@NumStr", System.Data.SqlDbType.VarChar, 50);
            outParameter1.Direction = System.Data.ParameterDirection.Output;
            sqlParList.Add(outParameter1);
            var outParameter2 = new SqlParameter("@Format", System.Data.SqlDbType.VarChar, 50);
            outParameter2.Direction = System.Data.ParameterDirection.Output;
            sqlParList.Add(outParameter2);
            _databaseFactory.Get().Database.ExecuteSqlCommand("exec spFieldGetNewID  @OrganizeId, @TopOrganizeId, @FieldName, @DateFlag, @currDate, @InitFormat, @NumStr out, @Format out", sqlParList.ToArray());

            var val1 = outParameter1.Value;
            var val2 = outParameter2.Value;
            return Tuple.Create(val1, val2);
        }


        public string GetNewFieldUniqueValueDB(string db, string fieldName, string orgId, string initFormat = "", bool dateFlag = false)
        {
            var tpl = GetNewFieldUniqueObjectValueDB(db,fieldName, orgId, initFormat, dateFlag);

            if (tpl.Item2 == null || string.IsNullOrWhiteSpace(tpl.Item2.ToString()))
            {
                return tpl.Item1.ToString();
            }
            else
            {
                return string.Format(tpl.Item2.ToString(), Convert.ToInt32(tpl.Item1));
            }
        }
        private Tuple<object, object> GetNewFieldUniqueObjectValueDB(string db,string fieldName, string orgId, string initFormat = "", bool dateFlag = false)
        {
            var sqlParList = new List<SqlParameter>();

            sqlParList.Add(new SqlParameter("@OrganizeId", orgId));
            sqlParList.Add(new SqlParameter("@TopOrganizeId", Constants.TopOrganizeId));
            sqlParList.Add(new SqlParameter("@FieldName", fieldName));
            sqlParList.Add(new SqlParameter("@DateFlag", dateFlag));
            sqlParList.Add(new SqlParameter("@currDate", DateTime.Now.ToString("yyyyMMdd")));
            sqlParList.Add(new SqlParameter("@InitFormat", initFormat));
            var outParameter1 = new SqlParameter("@NumStr", System.Data.SqlDbType.VarChar, 50);
            outParameter1.Direction = System.Data.ParameterDirection.Output;
            sqlParList.Add(outParameter1);
            var outParameter2 = new SqlParameter("@Format", System.Data.SqlDbType.VarChar, 50);
            outParameter2.Direction = System.Data.ParameterDirection.Output;
            sqlParList.Add(outParameter2);
            _databaseFactory.Get().Database
                .ExecuteSqlCommand(
                    "exec " + db +
                    " spFieldGetNewID  @OrganizeId, @TopOrganizeId, @FieldName, @DateFlag, @currDate, @InitFormat, @NumStr out, @Format out",
                    sqlParList.ToArray());

            var val1 = outParameter1.Value;
            var val2 = outParameter2.Value;
            return Tuple.Create(val1, val2);
        }
        #endregion

        #region 应用
        /// <summary>
        /// 获取手术申请号
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static string GetOPApplyNo(string orgId)
        {
            var thisDate = DateTime.Now.ToString("yyyyMMdd");
            var No = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("OR_ApplyInfo.Applyno", orgId, "{0:D6}", true);
            return "S" + thisDate + "A" + No;
        }
        public static string GetOPSsxh(string orgId)
        {
            var thisDate = DateTime.Now.ToString("yyyyMMdd");
            var No = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("OR_Registration.ssxh", orgId, "{0:D6}", true);
            return "S" + thisDate + "A" + No;
        }
        #endregion

    }

    /// <summary>
    /// 上下文 类型
    /// </summary>
    public enum EnumDBFactoryType
    {
        DefaultDB,

    }

}