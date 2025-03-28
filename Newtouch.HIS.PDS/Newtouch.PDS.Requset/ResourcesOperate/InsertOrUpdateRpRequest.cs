using Newtouch.HIS.API.Common;
using System.Collections.Generic;

namespace Newtouch.PDS.Requset.ResourcesOperate
{
    /// <summary>
    /// 新增或修改处方请求报文
    /// </summary>
    public class InsertOrUpdateRpRequest : RequestBase
    {
        /// <summary>
        /// 新增或修改的处方集合
        /// </summary>
        public List<RpInfo> Rps { get; set; }
    }

    /// <summary>
    /// 处方信息
    /// </summary>
    public class RpInfo
    {
        /// <summary>
        /// 处方号
        /// </summary>
        public string Cfh { get; set; }

        /// <summary>
        /// 药房部门代码
        /// </summary>
        public string Yfbm { get; set; }

        /// <summary>
        /// 处方内码
        /// </summary>
        public int Cfnm { get; set; }

        /// <summary>
        /// 组织结构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 修改后的处方明细
        /// </summary>
        public List<Cfmx> Cfmx { get; set; }

        /// <summary>
        /// 操作员代码
        /// </summary>
        public string CreatorCode { get; set; }
    }

    /// <summary>
    /// 插入或更新失败的处方
    /// </summary>
    public class FailRp : RpInfo
    {
        /// <summary>
        /// 失败原因
        /// </summary>
        public string FailMsg { get; set; }
    }
}
