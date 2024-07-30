using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtouch.HIS.API.Common;


namespace Newtouch.PDS.Requset.ResourcesOperate
{
    /// <summary>
    /// 处方修改
    /// </summary>
    public class UpdateRpRequset : RequestBase
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
    /// 处方明细
    /// </summary>
    public class Cfmx
    {

        /// <summary>
        /// 处方明细内码(非必填)
        /// </summary>
        public int Cfmxnm { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        [Required]
        public string YpCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string YpName { get; set; }

        /// <summary>
        /// 药品数量
        /// </summary>
        [Required]
        public int YpCount { get; set; }

        /// <summary>
        /// 收费大类
        /// </summary>
        public string DlCode { get; set; }
    }
}
