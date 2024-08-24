using System;

namespace NeiMengGuYiBaoApp.Models.Log
{
    public class LogMain
    {
        /// <summary>
        /// 凭证号码
        /// </summary>
        public decimal inNumber { get; set; }
        /// <summary>
        /// 输入日期
        /// </summary>
        public DateTime inDate { get; set; }
        /// <summary>
        /// 交易编码
        /// </summary>
        public string tradiNumber { get; set; }

        /// <summary>
        /// 用户电脑ip
        /// </summary>
        public string userIp { get; set; }
        /// <summary>
        /// 操作员id
        /// </summary>
        public string operatorId { get; set; }


    }
}
