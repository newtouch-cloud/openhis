using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2018-06-28 15:37
    /// 描 述：住院医嘱套餐明细
    /// </summary>
    [Table("zy_yztcmx")]
    public class InpatientOrderPackageDetailEntity : IEntity<InpatientOrderPackageDetailEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// MainId
        /// </summary>
        /// <returns></returns>
        public string MainId { get; set; }
        /// <summary>
        /// 本地医嘱分组序号
        /// </summary>
        /// <returns></returns>
        public int? zh { get; set; }
        /// <summary>
        /// 频次代码
        /// </summary>
        /// <returns></returns>
        public string pcCode { get; set; }
        /// <summary>
        /// 执行次数
        /// </summary>
        /// <returns></returns>
        public int? zxcs { get; set; }
        /// <summary>
        /// 执行周期
        /// </summary>
        /// <returns></returns>
        public int? zxzq { get; set; }
        /// <summary>
        /// 执行周期单位 -1:不规则周期，0：天,1：小时,2：分钟.注意：当为-1时，周代码（zdm）为数字（1234567）中的任意几个数字
        /// </summary>
        /// <returns></returns>
        public int? zxzqdw { get; set; }
        /// <summary>
        /// 周代码
        /// </summary>
        /// <returns></returns>
        public string zdm { get; set; }
        /// <summary>
        /// 药品idm
        /// </summary>
        /// <returns></returns>
        public decimal? idm { get; set; }
        /// <summary>
        /// xmdm
        /// </summary>
        /// <returns></returns>
        public string xmdm { get; set; }
        /// <summary>
        /// xmmc
        /// </summary>
        /// <returns></returns>
        public string xmmc { get; set; }
        /// <summary>
        /// 剂量单位
        /// </summary>
        /// <returns></returns>
        public string dw { get; set; }
        /// <summary>
        /// sl
        /// </summary>
        /// <returns></returns>
        public int sl { get; set; }
        /// <summary>
        /// 单位类别 0剂量单位，1药库单位，2门诊单位，3住院单位，4进货单位(指示医嘱单位类别为0)
        /// </summary>
        /// <returns></returns>
        public int? dwlb { get; set; }
        /// <summary>
        /// 本地医嘱类别 0：药品医嘱，1：抗生素医嘱，2：检查医嘱，3：检验医嘱，4：指示医嘱，5：普通医嘱 ， 6 停止医嘱， 7  出院带药， 8 膳食医嘱， 9 手术医嘱   10  中草药  
        /// </summary>
        /// <returns></returns>
        public int yzlx { get; set; }
        /// <summary>
        /// 药品剂量
        /// </summary>
        /// <returns></returns>
        public decimal? ypjl { get; set; }
        /// <summary>
        /// 药品规格
        /// </summary>
        /// <returns></returns>
        public string ypgg { get; set; }
        /// <summary>
        /// 嘱托内容
        /// </summary>
        /// <returns></returns>
        public string ztnr { get; set; }
        /// <summary>
        /// 医嘱内容
        /// </summary>
        /// <returns></returns>
        public string yznr { get; set; }
        /// <summary>
        /// 药品用法
        /// </summary>
        /// <returns></returns>
        public string ypyfdm { get; set; }
        /// <summary>
        /// 0 通用 1 自备
        /// </summary>
        /// <returns></returns>
        public int zbbz { get; set; }
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
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
        public string bw { get; set; }
        public string zxksdm { get; set; }
        public string nlmd { get; set; }
        /// <summary>
        /// 长临标志
        /// </summary>
        public string yzlb { get; set; }
    }
}