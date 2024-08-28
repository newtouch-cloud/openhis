using System;


namespace Newtouch.HIS.Domain.ValueObjects
{
    public class YPFYPatientInfoVO
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 医嘱ID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// 医嘱执行ID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// 领药序号
        /// </summary>
        public long lyxh { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 大类
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 数量+单位
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// 口服、静滴...
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// 执行时间：04,06,08...
        /// </summary>
        public string sjap { get; set; }

        /// <summary>
        /// 与执行数量对应
        /// </summary>
        public string pcmc { get; set; }

        /// <summary>
        /// 用量
        /// </summary>
        public decimal yl { get; set; }

        /// <summary>
        /// 用量单位
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// 实发单位
        /// </summary>
        public string zycldw { get; set; }

        /// <summary>
        /// 医生工号
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime ksrq { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime jsrq { get; set; }

        /// <summary>
        /// 发药药房
        /// </summary>
        public string fyyf { get; set; }

        /// <summary>
        /// 1：临时；2：长期
        /// </summary>
        public string yzxzmc { get; set; }

        /// <summary>
        /// 嘱托
        /// </summary>
        public string yzbz { get; set; }

        /// <summary>
        /// 频次
        /// </summary>
        public int zxsl { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 自负比例
        /// </summary>
        public decimal? zfbl { get; set; }

        /// <summary>
        /// 自负性质
        /// </summary>
        public char zfxz { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 病区代码
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// 病区名称
        /// </summary>
        public string bqmc { get; set; }

        /// <summary>
        /// 床位
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// 发药标志  0：未发；1：已排；2：已发；3：已退
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// 操作类型名称  1-发药 2-已退
        /// </summary>
        public string czlxbz { get; set; }

        /// <summary>
        /// 医嘱执行申请人
        /// </summary>
        public string yzzxsqr { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 修改用户
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string ypgg { get; set; }
        /// <summary>
        /// 频次
        /// </summary>
        public string ycmc { get; set; }
        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }
        /// <summary>
        /// 排药人员
        /// </summary>
        public string pyry { get; set; }
        /// <summary>
        /// 排药日期
        /// </summary>
        public DateTime? pyrq { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
        public string jxmc { get; set; }
        /// <summary>
        /// 申请退药标志
        /// </summary>
        public string sqtyzt { get; set; }
        /// <summary>
        /// 组号
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// 单价+单位
        /// </summary>
        public string djStr { get; set; }

        /// <summary>
        /// 贴数
        /// </summary>
        public decimal ts { get; set; }
        /// <summary>
        /// 国家医保代码
        /// </summary>
        public string gjybdm { get; set; }
    }

    /// <summary>
    /// 患者信息
    /// </summary>
    public class HospitalizationPatientInfo
    {

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// 床位
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }
    }

    /// <summary>
    /// 住院医嘱信息
    /// </summary>
    public class HospitalizationRpDetail : HospitalizationPatientInfo
    {
        /// <summary>
        /// 医嘱ID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// 医嘱执行ID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 药品规格
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// 组号
        /// </summary>
        public string zh { get; set; }

        /// <summary>
        /// 医嘱性质名称
        /// </summary>
        public string yzxzmc { get; set; }

        /// <summary>
        /// 药厂名称
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// 治理方法
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 剂型名称
        /// </summary>
        public string jxmc { get; set; }

        /// <summary>
        /// 频次名称
        /// </summary>
        public string pcmc { get; set; }

        /// <summary>
        /// 用量
        /// </summary>
        public decimal? yl { get; set; }

        /// <summary>
        /// 用量单位
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// 发药标志  0：未发；1：已排；2：已发；3：已退
        /// </summary>
        public string fybz { get; set; }
    }

    /// <summary>
    /// 住院发药明细
    /// </summary>
    public class HospitalizationDispenseDetail : HospitalizationRpDetail
    {
        public Int64 Id { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 数量+单位
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// 操作类型   退药/发药
        /// </summary>
        public string czlx { get; set; }

        /// <summary>
        /// 用量单位
        /// </summary>
        public string ylStr { get; set; }

        /// <summary>
        /// 单价+单位
        /// </summary>
        public string djStr { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string CreatorCode { get; set; }

        public string zytyapplyno { get; set; }

    }

    /// <summary>
    /// 住院发药明细
    /// </summary>
    public class HospitalizationDispenseDetailV2 : HospitalizationRpDetail
    {
        public Int64 Id { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 数量+单位
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// 操作类型   退药/发药
        /// </summary>
        public string czlx { get; set; }

        /// <summary>
        /// 用量单位
        /// </summary>
        public string ylStr { get; set; }

        /// <summary>
        /// 单价+单位
        /// </summary>
        public string djStr { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string CreatorCode { get; set; }

        public Int64? lyxh { get; set; }

    }

    public class FybdComboboxList
    {
        public string operatetime { get; set; }
    }

    /// <summary>
    /// 住院退药
    /// </summary>
    public class HospitalizationReturnDispenseDetail : HospitalizationRpDetail
    {

        /// <summary>
        /// 申请退药单号
        /// </summary>
        public string applyNo { get; set; }

        /// <summary>
        /// 申请退药明细ID
        /// </summary>
        public string rdbdId { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 退药数量  默认0
        /// </summary>
        public int tysl { get; set; }

        /// <summary>
        /// 申请退药数（带单位）  
        /// </summary>
        public string tyslStr { get; set; }

        /// <summary>
        /// 最大可退数量 部门单位数量
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 最大可退数量 最小单位数量
        /// </summary>
        public int zxdwsl { get; set; }

        /// <summary>
        /// 部门单位
        /// </summary>
        public string bmdw { get; set; }

        /// <summary>
        /// 数量+单位
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// 用量单位
        /// </summary>
        public string ylStr { get; set; }

        /// <summary>
        /// 单价+单位
        /// </summary>
        public string djStr { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 发药时间
        /// </summary>
        public DateTime? fysj { get; set; }
        
        public string zytyapplyno { get; set; }
    }

    public class KSBYSQDInfoVO
    {
        public string djh { get; set; }
        public string bqbm { get; set; }
        public string bqmc { get; set; }
        public string thyy { get; set; }
    }
}
