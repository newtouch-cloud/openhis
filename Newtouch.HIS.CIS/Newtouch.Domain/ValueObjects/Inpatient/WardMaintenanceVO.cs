using System;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    public class WardMaintenanceVO
    {
    }
    public class BedItemsVO
    {
        public string ChargeId { get; set; }
        public string ChargeCode { get; set; }
        public string ChargeName { get; set; }
        public decimal ChargePrice { get; set; }
        public string ChargeUtity { get; set; }
        public int ChargeNum { get; set; }
        public string ChargeItem { get; set; }
        public string djbd { get; set; }//床位等级绑定标志
    }
    public class Dispensing
    {
        public string bysj { get; set; }
        public string hldy { get; set; }
        public string tjz { get; set; }
        public int yzsm { get; set; }

    }
    public class DispensingMX
    {
        public string cw { get; set; }
        public string xm { get; set; }
        public string zyh { get; set; }
        public string yznr { get; set; }
        public string jl { get; set; }
        public string pc { get; set; }
        public string zxsj { get; set; }
        public string ypmc { get; set; }
        public string ypgg { get; set; }
        public decimal sl { get; set; }
        public string dw { get; set; }
        public string ycmc { get; set; }
        public decimal ys { get; set; }
        public decimal ss { get; set; }
        public string kssj { get; set; }
        public string tzsj { get; set; }
        public string jytj { get; set; }
        public string fb { get; set; }
        public int? zh { get; set; }

    }
    public class BedDocsVO
    {
        public string Id { get; set; }
        public string ysgh { get; set; }
        public string ysmc { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }
    }

    public class PatDiagnosisVO
    {
        public string Id { get; set; }
        public string zdlb { get; set; }
        public string zdlx { get; set; }
        public string zddm { get; set; }
        public string zdmc { get; set; }
        public string zdyzdmc { get; set; }
        public string cyqk { get; set; }
    }

    /// <summary>
    /// 出去诊断
    /// </summary>
    public class PatOutAreaVO
    {
        public string zyh { get; set; }
        public string cyfs { get; set; }
        public DateTime cqsj { get; set; }
        public DateTime zdrq { get; set; }
        public string zyzd { get; set; }
        public string zyzddm { get; set; }
        public string fzzd1 { get; set; }
        public string fzzd1dm { get; set; }
        public string fzzd2 { get; set; }
        public string fzzd2dm { get; set; }
        public string fzzd3 { get; set; }
        public string fzzd3dm { get; set; }
        public string zdyzd { get; set; }
        /// <summary>
        /// 出院情况 EnumCYQK
        /// </summary>
        public string cyqk { get; set; }
        public string cyqk1 { get; set; }
        public string cyqk2 { get; set; }
        public string cyqk3 { get; set; }
        public string cyqkzdy { get; set; }
    }

    public class PatOutAreaInfoVO
    {
        public string zyh { get; set; }
        public string cyfs { get; set; }
        public DateTime cqsj { get; set; }
        public string zyzd { get; set; }
        public string zyzddm { get; set; }

    }

    /// <summary>
    /// 入区诊断
    /// </summary>
    public class PatInAreaVO
    {
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 入区诊断1名称
        /// </summary>
        public string ryzdmc1 { get; set; }

        /// <summary>
        /// 入区诊断1代码
        /// </summary>
        public string ryzddm1 { get; set; }

        /// <summary>
        /// 入区诊断1ICD10
        /// </summary>
        public string ryzdICD101 { get; set; }

        /// <summary>
        /// 入区诊断2名称
        /// </summary>
        public string ryzdmc2 { get; set; }

        /// <summary>
        /// 入区诊断2代码
        /// </summary>
        public string ryzddm2 { get; set; }

        /// <summary>
        /// 入区诊断2ICD10
        /// </summary>
        public string ryzdICD102 { get; set; }

        /// <summary>
        /// 入区诊断3名称
        /// </summary>
        public string ryzdmc3 { get; set; }

        /// <summary>
        /// 入区诊断3代码
        /// </summary>
        public string ryzddm3 { get; set; }

        /// <summary>
        /// 入区诊断3ICD10
        /// </summary>
        public string ryzdICD103 { get; set; }
    }

}
