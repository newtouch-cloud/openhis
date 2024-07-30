using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 门诊处方
    /// </summary>
    [Table("mz_cf")]
    public class OutpatientPrescriptionEntity : IEntity<OutpatientPrescriptionEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int cfnm { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ghnm { get; set; }

        /// <summary>
        /// 同本次挂号的病人内码－提高性能之冗余字段
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// 同本次挂号的病人性质－提高性能之冗余字段
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// 1：门诊 2：急诊
        /// </summary>
        public string mjzbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        /// 0 待结 1 已结    (没有已退标志,因为存在部??退药的处方)   --2006-06-17 启用,作为便于识别结算情况的冗余字段   重要：当作废“未结算”处方时，本cfzt会置为1，所以取所有已结算处方（因为认为作废置结算内码为-1，既然变动了结算内码就是一种结算）
        /// </summary>
        public string cfzt { get; set; }

        /// <summary>
        /// 0 收费处 1 医生站
        /// </summary>
        public string cfly { get; set; }

        /// <summary>
        /// 0 待配 1 待发 2 已发   (没有已退标志,因为存在部分退药的处方)       退方与处方一样含义   --2007.03.27注释： 在退方时：直接置2
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// 因为必须从库存中选取药品，故输入时就已经确定发药药房了。
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// 领药窗口放到结算时存入更加合理，但是可能造成大量的数据无法结算（窗口关闭），故放在保存处方时生成，效果没有差异。可以
        /// </summary>
        public string lyck { get; set; }

        /// <summary>
        /// 本来应该放在结算大类表中，中药一个序号，西药成药一个序号，但是考虑到数据的复杂性，特放置于此。   现在每张处方一个领药序号，病人刷卡发药，本人连续的领药号会在发药完成后自动跳过。      －－一期开发暂时不做2006.06.21
        /// </summary>
        public short? lyxh { get; set; }

        /// <summary>
        /// 配药日期
        /// </summary>
        public string pyry { get; set; }

        /// <summary>
        /// 配药人员
        /// </summary>
        public DateTime? pyrq { get; set; }

        /// <summary>
        /// 发药人员
        /// </summary>
        public string fyry { get; set; }

        /// <summary>
        /// 发药日期
        /// </summary>
        public DateTime? fyrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Obsolete]
        public int? jsnm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? jsrq { get; set; }

        /// <summary>
        /// 来自医生工作站处方号，手工处方cms系统自动生成处方号
        /// </summary>
        public string cfh { get; set; }

        ///// <summary>
        ///// 成组号 放在处方明细表
        ///// </summary>
        //public string czh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        public string ksmc { get; set; }
        public string ysmc { get; set; }

        /// <summary>
        /// 处方类型 枚举EnumPrescriptionType
        /// </summary>
        public int? cflx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cflxmc { get; set; }

        /// <summary>
        /// 收费日期 20180329加
        /// </summary>
        public DateTime? sfrq { get; set; }

        /// <summary>
        /// 中药贴数
        /// </summary>
        public int? ts { get; set; }

        /// <summary>
        /// 中药代煎标志 1 需要代煎
        /// </summary>
        public int? djbz { get; set; }

        /// <summary>
        /// 申请所处状态（检验检查 0、已申请 1、已接收 2、已完成）
        /// </summary>
        public int? sqdzt { get; set; }

    }
}
