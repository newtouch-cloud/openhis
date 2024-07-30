using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.InputDto
{
    public class InPatientQueryDto
    {
        public string zyh { get; set; }
        public string xm { get; set; }
        public string nl { get; set; }
        public string nlshow { get; set; }
        public string brxz { get; set; }
        public string brxzmc { get; set; }
        public string blh { get; set; }
        public string zzdCode { get; set; }
        public string zzdicd10 { get; set; }
        public string zzdmc { get; set; }
        public DateTime? csny { get; set; }
        public DateTime ryrq { get; set; }
        public DateTime? cyrq { get; set; }
        public string bqCode { get; set; }
        public string bqmc { get; set; }
        public int zybz { get; set; }
        public string zybzmc { get; set; }
        public string UpdateTime { get; set; }
        public string sexValue { get; set; }
        public string ys { get; set; }
        public string ysxm { get; set; }
    }

    public class InPatientDetailQueryDto: InPatientQueryDto
    {
        public string OrganizeId { get; set; }
        public string py { get; set; }
        public string wb { get; set; }
        public string zjlx { get; set; }
        public string zjlxValue { get; set; }
        public string zjh { get; set; }
        public string idCardNo { get; set; }
        public string sex { get; set; }
        public string ks { get; set; }
        public string ksmc { get; set; }
        
        public string kh { get; set; }
        public string CardType { get; set; }
        public string CardTypeName { get; set; }
        public string contPerName { get; set; }
        public string contPerPhoneNum { get; set; }
        public string contPerRel { get; set; }
        public string contPerRelValue { get; set; }
        public DateTime rqrq { get; set; }
        public string wzjb { get; set; }
        public string BedCode { get; set; }
        public string cwmc { get; set; }
        public string ryfs { get; set; }
        public string grbh { get; set; }
    }
    public class InAreapatientInfo:InPatientDetailQueryDto
    {
        public string cyzdmc { get; set; }    }
    public class OutAreapatientInfo : InPatientDetailQueryDto
    {
        public DateTime? cqrq { get; set; }
    }

    public class RequestMsgDto
    {
        public string code { get; set; }
        public string msg { get; set; }
        public string sub_code { get; set; }
        public string sub_msg { get; set; }
        public string data { get; set; }
    }

    public class RequestOrderExecMsgDto
    {
        /// <summary>
        /// 是否成功 true:全部成功/部分成功  false:全部失败/验证失败
        /// </summary>
        public bool IsSucceed { get; set; }

        /// <summary>
        /// 状态码 参照枚举ResultCode
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        public string ResultMsg { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public dynamic Data { get; set; }
    }

    /// <summary>
    /// 医嘱明细   Data返回内容,以集合呈现
    /// </summary>
    //public class YzDetail
    //{
    //    /// <summary>
    //    /// 医嘱ID
    //    /// </summary>
    //    public string yzId { get; set; }

    //    /// <summary>
    //    /// 领药序号
    //    /// </summary>
    //    public long lyxh { get; set; }

    //    /// <summary>
    //    /// 患者姓名
    //    /// </summary>
    //    public string patientName { get; set; }

    //    /// <summary>
    //    /// 住院号
    //    /// </summary>
    //    public string zyh { get; set; }

    //    /// <summary>
    //    /// 药品代码
    //    /// </summary>
    //    public string ypCode { get; set; }

    //    /// <summary>
    //    /// 大类
    //    /// </summary>
    //    public string dl { get; set; }

    //    /// <summary>
    //    /// 数量
    //    /// </summary>
    //    public decimal sl { get; set; }

    //    /// <summary>
    //    /// 口服、静滴...
    //    /// </summary>
    //    public string zlff { get; set; }

    //    /// <summary>
    //    /// 执行时间：04,06,08...
    //    /// </summary>
    //    public string sjap { get; set; }

    //    /// <summary>
    //    /// 与执行数量对应
    //    /// </summary>
    //    public string pcmc { get; set; }

    //    /// <summary>
    //    /// 用量
    //    /// </summary>
    //    public decimal yl { get; set; }

    //    /// <summary>
    //    /// 用量单位
    //    /// </summary>
    //    public string yldw { get; set; }

    //    /// <summary>
    //    /// 医生工号
    //    /// </summary>
    //    public string ysgh { get; set; }

    //    /// <summary>
    //    /// 开始日期
    //    /// </summary>
    //    public DateTime ksrq { get; set; }

    //    /// <summary>
    //    /// 结束日期
    //    /// </summary>
    //    public DateTime jsrq { get; set; }

    //    /// <summary>
    //    /// 发药药房
    //    /// </summary>
    //    public string fyyf { get; set; }

    //    /// <summary>
    //    /// 1：临时；2：长期
    //    /// </summary>
    //    public string yzxz { get; set; }

    //    /// <summary>
    //    /// 嘱托
    //    /// </summary>
    //    public string yzbz { get; set; }

    //    /// <summary>
    //    /// 频次
    //    /// </summary>
    //    public int zxsl { get; set; }

    //    /// <summary>
    //    /// 单价
    //    /// </summary>
    //    public decimal dj { get; set; }

    //    /// <summary>
    //    /// 金额
    //    /// </summary>
    //    public decimal je { get; set; }

    //    /// <summary>
    //    /// 自负比例
    //    /// </summary>
    //    public decimal? zfbl { get; set; }

    //    /// <summary>
    //    /// 自负性质
    //    /// </summary>
    //    public char zfxz { get; set; }

    //    /// <summary>
    //    /// 科室名称
    //    /// </summary>
    //    public string ksmc { get; set; }

    //    /// <summary>
    //    /// 病区名称
    //    /// </summary>
    //    public string bqmc { get; set; }

    //    /// <summary>
    //    /// 床位
    //    /// </summary>
    //    public string cw { get; set; }

    //    /// <summary>
    //    /// 发药标志  0：未发；1：已排；2：已发；3：已退
    //    /// </summary>
    //    public char fybz { get; set; }

    //    /// <summary>
    //    /// 医嘱执行申请人
    //    /// </summary>
    //    public string yzzxsqr { get; set; }

    //    /// <summary>
    //    /// 执行日期
    //    /// </summary>
    //    public DateTime zxrq { get; set; }
    //}
}
