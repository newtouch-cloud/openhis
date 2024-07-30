using System;
using System.ComponentModel;

namespace Newtouch.PDS.Requset
{
    /// <summary>
    /// 退药执行结果
    /// </summary>
    public class DrugReturnResult
    {
        /// <summary>
        /// 执行结果
        /// </summary>
        public DrugReturnResult()
        {
            IsSucceed = true;
            ErrorCode = 0;
            ErrorMsg = string.Empty;
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
        /// 返回医嘱ID
        /// </summary>
        public string yzId { get; set; }
        /// <summary>
        /// 药品CODE
        /// </summary>
        public string ypCode { get; set; }
        /// <summary>
        /// 组号
        /// </summary>
        public string zh { get; set; }
        /// <summary>
        /// 执行日期
        /// </summary>
        public DateTime? zxrq { get; set; }
    }

    public enum ErrorCode
    {
        /// <summary>
        /// 执行成功
        /// </summary>
        [Description("执行成功")]
        Success = 0,

        /// <summary>
        /// 医嘱号不能为空
        /// </summary>
        [Description("医嘱号不能为空")]
        Error1 = 1,

        /// <summary>
        /// 药品编码不能为空
        /// </summary>
        [Description("药品编码不能为空")]
        Error2 = 2,

        /// <summary>
        /// 无法查询该医嘱信息
        /// </summary>
        [Description("无法查询该医嘱信息")]
        Error3 = 3,

        /// <summary>
        /// 未发药,禁止退药
        /// </summary>
        [Description("未发药,禁止退药")]
        Error4 = 4,

        /// <summary>
        /// 药品已经退过了
        /// </summary>
        [Description("药品已经退过了")]
        Error5 = 5,

        /// <summary>
        /// 已经申请退药禁,止重复操作
        /// </summary>
        [Description("已经申请退药禁,止重复操作")]
        Error6 = 6,

        /// <summary>
        /// 退药申请人不能为空
        /// </summary>
        [Description("退药申请人不能为空")]
        Error7 = 7,

        /// <summary>
        /// 执行日期不能为空
        /// </summary>
        [Description("执行日期不能为空")]
        Error8 = 8,
    }
}
