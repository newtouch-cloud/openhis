using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.ResourcesOperate
{
    /// <summary>
    /// 预定报文
    /// </summary>
    public class OutpatientBookRequestDTO : RequestBase
    {
        /// <summary>
        /// 药品代码
        /// </summary>
        [Required]
        public List<ItemDetail> Items { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        [Required]
        public string OrganizeId { get; set; }

        /// <summary>
        /// 操作员代码
        /// </summary>
        public string CreatorCode { get; set; }
    }

    /// <summary>
    /// 资源详情
    /// </summary>
    public class ItemDetail
    {

        /// <summary>
        /// 药房部门代码
        /// </summary>
        [Required]
        public string Yfbm { get; set; }

        /// <summary>
        /// 处方号 必填
        /// </summary>
        [Required]
        public string Cfh { get; set; }

        /// <summary>
        /// 项目/药品代码
        /// </summary>
        [Required]
        public string ItemCode { get; set; }

        /// <summary>
        /// 项目/药品数量 （药房部门单位）
        /// </summary>
        [Required]
        public decimal ItemCount { get; set; }

        /// <summary>
        /// 转化因子 与ItemCount配合使用 ItemCount*Zhyz=最小单位数量
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// 项目/药品名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 药品规格
        /// </summary>
        public string ItemSpecifications { get; set; }

        /// <summary>
        /// 项目/药品单价 （药房部门单位单价与ItemUnit匹配）
        /// </summary>
        public decimal ItemUnitPrice { get; set; }

        /// <summary>
        /// 项目/药品单位名称 （药房部门单位，与ItemCount匹配）
        /// </summary>
        public string ItemUnitName { get; set; }

        /// <summary>
        /// 项目/药品生产商名称
        /// </summary>
        public string ItemManufacturer { get; set; }

        /// <summary>
        /// 收费大类
        /// </summary>
        public string DlCode { get; set; }

        /// <summary>
        /// 剂量
        /// </summary>
        public decimal? Dosage { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        public string DosageUnit { get; set; }

        /// <summary>
        /// 用法名称
        /// </summary>
        public string UsageName { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int nl { get; set; }

        /// <summary>
        /// 病人性质名称
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 成组号
        /// </summary>
        public string GroupNum { get; set; }
    }

    /// <summary>
    /// 预定失败项
    /// </summary>
    public class FailItemDetail : ItemDetail
    {
        /// <summary>
        /// 失败原因
        /// </summary>
        public string FailMsg { get; set; }

        /// <summary>
        /// 失败代码
        /// </summary>
        public string FailCode { get; set; }
    }
}
