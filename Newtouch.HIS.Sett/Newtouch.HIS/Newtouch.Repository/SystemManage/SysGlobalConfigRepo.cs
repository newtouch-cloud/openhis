using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Infrastructure;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository.SystemManage
{
    /// <summary>
    /// 
    /// </summary>
    public class SysGloablConfigRepo : RepositoryBase<SysGlobalConfigVEntity>, ISysGloablConfigRepo
    {
        public SysGloablConfigRepo(INewtouchDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 根据Code获取配置Value
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetValueByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return null;
            }
            var sql = @"select Value from [NewtouchHIS_Base]..V_S_Sys_GlobalConfig where Code = @code and zt = '1'";
            return this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@code", code) });
        }

        /// <summary>
        /// 根据Code获取配置Value
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int GetIntValueByCode(string code, int defaultValue = 0)
        {
            var str = GetValueByCode(code);
            int intReturn;
            if (!int.TryParse(str, out intReturn))
            {
                return defaultValue;
            }
            return intReturn;
        }

        /// <summary>
        /// 根据Code获取配置Value
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool? GetBoolValueByCode(string code, bool? defaultValue = null)
        {
            var str = GetValueByCode(code);
            bool boolReturn;
            if (!bool.TryParse(str, out boolReturn))
            {
                return defaultValue;
            }
            return boolReturn;
        }

    }
}
