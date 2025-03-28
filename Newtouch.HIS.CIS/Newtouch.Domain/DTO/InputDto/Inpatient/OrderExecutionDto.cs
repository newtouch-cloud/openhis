using Newtouch.Domain.DTO.InputDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.InputDto
{
    /// <summary>
    /// 医嘱明细
    /// </summary>
    public class OrderExecutionDto
    {
        /// <summary>
        /// 医嘱ID
        /// </summary>
        public string yzId { get; set; }
        /// <summary>
        /// 领药序号
        /// </summary>
        public int? lyxh { get; set; }
        ///// <summary>
        ///// 医嘱类型
        ///// </summary>
        //public int? yzlx { get; set; }
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
        public int? sl { get; set; }
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
        public int? pcmc { get; set; }
        /// <summary>
        /// 用量
        /// </summary>
        public string yl { get; set; }
        /// <summary>
        /// 用量单位
        /// </summary>
        public string yldw { get; set; }
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
        public int? yzxz { get; set; }
        /// <summary>
        /// 嘱托
        /// </summary>
        public string yzbz { get; set; }
        /// <summary>
        /// 频次
        /// </summary>
        public int? zxsl { get; set; }
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
        public char fybz { get; set; }
        /// <summary>
        /// 医嘱执行申请人
        /// </summary>
        public string yzzxsqr { get; set; }

    }
    /// <summary>
    /// 执行结果
    /// </summary>
    public class ActResult
    {
        /// <summary>
        /// 执行结果
        /// </summary>
        public ActResult()

        {

            IsSucceed = true;

        }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSucceed { get; set; }
        /// <summary>
        /// 错误编码
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// 错误描述
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public dynamic Data { get; set; }
    }
}

/// <summary>
/// 预定失败项
/// </summary>
public class FailYzDetail : OrderExecutionDto
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




