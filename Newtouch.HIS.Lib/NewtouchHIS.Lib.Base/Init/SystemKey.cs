using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Lib.Base
{
    /// <summary>
    /// 通用系统变量
    /// </summary>
    public sealed class SystemKey
    {
        public static string Cache_UserTokenKey = "HisToken:User_";
        public static string Cache_OrgListKey = "TopOrg_";


        /// <summary>
        /// 组装用户Token缓存key
        /// </summary>
        /// <param name="account"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static string AssemblyUserTokenKey(string account,string orgId)
        {
            return $"{Cache_UserTokenKey}{account}_{orgId?.Replace("-", "")}";
        }
        /// <summary>
        /// 组装机构列表
        /// </summary>
        /// <param name="topOrgId"></param>
        /// <returns></returns>
        public static string AssemblyOrgListKey(string topOrgId)
        {
            return $"{Cache_OrgListKey}{topOrgId.Replace("-", "")}";
        }

    }
}
