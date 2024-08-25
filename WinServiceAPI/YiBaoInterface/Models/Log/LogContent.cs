using System;

namespace NeiMengGuYiBaoApp.Models.Log
{
    public class LogContent
    {
        /// <summary>
        /// 凭证号码
        /// </summary>
        public decimal inNumber { get; set; }

        /// <summary>
        /// 交易编码
        /// </summary>
        public string tradiNumber { get; set; }

        /// <summary>
        /// 	病人ID		varchar	G1123	跟HIS关联的病人关键值（跟HIS唯一关联号）
        /// </summary>
        public string hisId { get; set; }

        /// <summary>
        /// 输入日期
        /// </summary>
        public DateTime beginDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime endDate { get; set; }

        /// <summary>
        /// 交易输入串		varchar		交易输入的第18项 ，json格式保存
        /// </summary>
        public string inHead { get; set; }
        /// <summary>
        /// 7	交易输入串		varchar		交易输入的第18项 ，json格式保存
        /// </summary>
        public string inContent { get; set; }
        /// <summary>
        /// 交易输出控制		varchar		交易输出的1-5项，json格式保存
        /// </summary>
        public string outHead { get; set; }
        /// <summary>
        /// 	交易输出串		varchar		交易输出的第6项，json格式保存
        /// </summary>
        public string outContent { get; set; }
        /// <summary>
        /// 11	交易错误信息		varchar		交易输出第5项，字符串
        /// </summary>
        public string errorMsg { get; set; }
        /// <summary>
        /// 10	交易返回值		int	0	交易输出结果值（ BUSINESS_HANDLE）
        /// </summary>
        public int stateId { get; set; }


    }
}

