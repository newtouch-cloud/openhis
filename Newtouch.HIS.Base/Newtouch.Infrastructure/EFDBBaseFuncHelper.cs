using Newtouch.Core.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class EFDBBaseFuncHelper
    {
        private EFDBBaseFuncHelper(IDatabaseFactory databaseFactory)
        {
            this._databaseFactory = databaseFactory;
        }

        private IDatabaseFactory _databaseFactory;

        /// <summary>
        /// 
        /// </summary>
        public static EFDBBaseFuncHelper Instance
        {
            get
            {
                IDatabaseFactory databaseFactory
                    = DependencyDefaultInstanceResolver.GetInstance<IBaseDatabaseFactory>();
                return new EFDBBaseFuncHelper(databaseFactory);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static EFDBBaseFuncHelper GetInstance(EnumDBFactoryType? dbType = null)
        {
            if (!dbType.HasValue)
            {
                dbType = EnumDBFactoryType.NewtouchDBBase;  //默认
            }
            IDatabaseFactory databaseFactory = null;
            switch (dbType)
            {
                case EnumDBFactoryType.NewtouchDBBase:
                    databaseFactory = DependencyDefaultInstanceResolver.GetInstance<IBaseDatabaseFactory>();
                    break;
            }
            return new EFDBBaseFuncHelper(databaseFactory);
        }

        #region
        /// <summary>
        /// 获取 字段/业务 唯一值
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public string GetNewFieldUniqueValue(string fieldName, string orgId, string topOrgId = null, string initFormat = "", bool dateFlag = false)
        {
            if (string.IsNullOrWhiteSpace(topOrgId))
            {
                topOrgId = Constants.TopOrganizeId;
            }

            var sqlParList = new List<SqlParameter>();

            sqlParList.Add(new SqlParameter("@OrganizeId", orgId));
            sqlParList.Add(new SqlParameter("@TopOrganizeId", topOrgId));
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

            if (outParameter2.Value == null || string.IsNullOrWhiteSpace(outParameter2.Value.ToString()))
            {
                return outParameter1.Value.ToString();
            }
            else
            {
                return string.Format(outParameter2.Value.ToString(), Convert.ToInt32(outParameter1.Value));
            }
        }
        #endregion

    }

    /// <summary>
    /// 上下文 类型
    /// </summary>
    public enum EnumDBFactoryType
    {
        NewtouchDBBase,

    }
}
