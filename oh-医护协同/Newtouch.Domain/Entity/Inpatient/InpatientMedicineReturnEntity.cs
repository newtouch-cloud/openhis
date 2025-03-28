using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2018-06-28 15:37
    /// 描 述：住院退药请求库
    /// </summary>
    [Table("zy_tyjl")]
    public class InpatientMedicineReturnEntity : IEntity<InpatientMedicineReturnEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 对应发药记录Id
        /// </summary>
        public string fyId { get; set; }
        /// <summary>
        /// 组织机构ID
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 退药序号 （标记一次退药）
        /// </summary>
        /// <returns></returns>
        public decimal tyxh { get; set; }
        /// <summary>
        /// 医嘱序号
        /// </summary>
        /// <returns></returns>
        public string yzxh { get; set; }
        /// <summary>
        /// 分组序号
        /// </summary>
        public int? fzxh { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        /// <returns></returns>
        public string hzxm { get; set; }
        /// <summary>
        /// 病区代码
        /// </summary>
        /// <returns></returns>
        public string WardCode { get; set; }
        /// <summary>
        /// 发药请求序号
        /// </summary>
        /// <returns></returns>
        public decimal? fyqqxh { get; set; }
        /// <summary>
        /// 药品代码
        /// </summary>
        /// <returns></returns>
        public string ypdm { get; set; }
        /// <summary>
        /// 退药数量
        /// </summary>
        /// <returns></returns>
        public int tysl { get; set; }
        /// <summary>
        /// 可退药品数量
        /// </summary>
        /// <returns></returns>
        public int ktypsl { get; set; }
        /// <summary>
        /// 药品规格
        /// </summary>
        /// <returns></returns>
        public string ypgg { get; set; }
        /// <summary>
        /// 药品单位
        /// </summary>
        /// <returns></returns>
        public string ypdw { get; set; }
        /// <summary>
        /// 单位系数
        /// </summary>
        /// <returns></returns>
        public decimal? dwxs { get; set; }
        /// <summary>
        /// 药库系数
        /// </summary>
        /// <returns></returns>
        public decimal? ykxs { get; set; }
        /// <summary>
        /// 药品单价
        /// </summary>
        /// <returns></returns>
        public decimal? ypdj { get; set; }
        /// <summary>
        /// （0 申请退药 1 退药成功）
        /// </summary>
        /// <returns></returns>
        public int? tyqrbz { get; set; }
        /// <summary>
        /// 退药确认序号 代替原：tyqqxh
        /// </summary>
        /// <returns></returns>
        public decimal? tyqrxh { get; set; }
        /// <summary>
        /// 退药操作员代码
        /// </summary>
        /// <returns></returns>
        public string tyczydm { get; set; }
        /// <summary>
        /// 药房退药操作员代码
        /// </summary>
        /// <returns></returns>
        public string yftyczydm { get; set; }
        /// <summary>
        /// 药房退药日期
        /// </summary>
        /// <returns></returns>
        public DateTime? yftyrq { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// 0无效1有效
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
        /// <summary>
        /// 退药单号
        /// </summary>
        /// <returns></returns>
        public string tydh { get; set; }
    }
}