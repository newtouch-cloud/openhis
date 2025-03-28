using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 贵州医保基础信息
    /// </summary>
    public interface IGzybBaseInfoDmnService
    {
        /// <summary>
        /// 获取贵州服务项目目录,最大流水号
        /// </summary>
        /// <returns></returns>
        int GetItemCodeMaxLsh();

        #region 目录相关
        string Header(string tbname);

        IList<G_yb_mluCommon_Info> Get_G_yb_mluCommon_Info(Pagination pagination, string mlbh, string key);
        #endregion
    }
}
