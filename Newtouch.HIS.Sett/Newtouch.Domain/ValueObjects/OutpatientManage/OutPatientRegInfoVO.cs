using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 门诊挂号信息
    /// </summary>
    [NotMapped]
    public class OutPatientRegInfoVO : OutpatientRegistEntity
    {
        public string ksmc { get; set; }
        public string ysmc { get; set; }
        public string brxzmc { get; set; }
        public string py { get; set; }

    }

    /// <summary>
    /// 门诊挂号信息
    /// </summary>
    public class OutPatientRegVO
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// 是否欠费预结
        /// </summary>
        public bool iqQfyj { get; set; }

        /// <summary>
        /// 患者主索引
        /// </summary>
        public int? patid { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 新农合个人编码
        /// </summary>
        public string xnhgrbm { get; set; }
    }

    public class OutPatientRegBaseVO
    {

        /// <summary>
        /// 门诊号
        /// </summary>
        public string Mzh { get; set; }
        public string CardNo { get; set; }

        /// <summary>
        /// 0 -窗口  1-预约
        /// </summary>
        public string Ghly { get; set; }

        /// <summary>
        /// 1：门诊 2：急诊 专家 mjzbz
        /// </summary>
        public string RegType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Dept { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Doctor { get; set; }
        public string DeptName { get; set; }
        public string DoctorName { get; set; }
        public string Ghxzmc { get; set; }
        public string Ghxz { get; set; }
        /// <summary>
        /// 按科室、挂号类型产生的排队号 from mz_jzxh
        /// </summary>
        public Int16? QueueNo { get; set; }
        /// <summary>
        /// 0 待结 1 已结 2 已退   --2006-06-17 启用,作为便于识别结算情况的冗余字段      --2006.06.13 ??时不用此字??   好像可以通过结算内码＝0判断未结，>0判断已结，撤销结算内码>0判断已退？暂时不用此字段
        /// </summary>
        public string RegStatus { get; set; }
        /// <summary>
        /// 就诊标志（就诊状态） 枚举EnumOutpatientJzbz
        /// </summary>
        public string Jzbz { get; set; }

        /// <summary>
        /// 挂号时间
        /// </summary>
        public DateTime RegDate { get; set; }
    }
}
