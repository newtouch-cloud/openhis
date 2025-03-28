using System;
using System.ComponentModel.DataAnnotations;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.ResourcesOperate
{
    /// <summary>
    /// 门诊
    /// </summary>
    public class OutpatientCommitRequestDTO : RequestBase
    {
        /// <summary>
        /// 结算内码 必填
        /// </summary>
        [Required]
        public long Jsnm { get; set; }

        /// <summary>
        /// 收费时间
        /// </summary>
        [Required]
        public DateTime Sfsj { get; set; }

        /// <summary>
        /// 处方号 必填
        /// </summary>
        [Required]
        public string Cfh { get; set; }

        /// <summary>
        /// 处方内码 必填
        /// </summary>
        [Required]
        public long Cfnm { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string Fph { get; set; }

        /// <summary>
        /// 发药药房代码
        /// </summary>
        public string fyyf { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string CreatorCode { get; set; }
    }
}