using System;
using System.Collections.Generic;
using Microsoft.Build.Framework;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.ResourcesOperate
{
    /// <summary>
    /// 住院退药
    /// </summary>
    public class HospitalizatiionReturnDispensingMedicineRequestDTO : RequestBase
    {
        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 退药药品明细
        /// </summary>
        public List<ReturnDispensingDrug> ReturnDrugDetail { get; set; }

        /// <summary>
        /// 接口调用流水号
        /// </summary>
        public string ClientNo { get; set; }
    }

    /// <summary>
    /// 药品退药
    /// </summary>
    public class ReturnDispensingDrug
    {

        /// <summary>
        /// 医嘱ID
        /// </summary>
        [Required]
        public string yzId { get; set; }

        /// <summary>
        /// 药品code
        /// </summary>
        [Required]
        public string ypCode { get; set; }

        /// <summary>
        /// 退药数量 部门单位
        /// </summary>
        [Required]
        public int tysl { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 转换因子（tysl*zhyz=最小单位数量）  默认住院拆零数
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// 退药申请人代码
        /// </summary>
        [Required]
        public string tysqr { get; set; }

        /// <summary>
        /// 组号
        /// </summary>
        public string zh { get; set; }

        /// <summary>
        /// 执行日期
        /// </summary>
        public DateTime? zxrq { get; set; }

        /// <summary>
        /// 病区代码
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// 发药药房
        /// </summary>
        public string fyyf { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string ksCode { get; set; }
        /// <summary>
        /// 领药序号
        /// </summary>
        public long lyxh { get; set; }
    }
}