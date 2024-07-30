using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai
{
    public class SHOutputReturn
    {
        /// <summary>
        /// 交易时间
        /// </summary>
        public string jysj { get; set; }
        /// <summary>
        /// 消息类型码
        /// </summary>
        public string xxlxm { get; set; }

        /// <summary>
        /// 交易返回码
        /// </summary>
        public string xxfhm { get; set; }

        /// <summary>
        /// 交易返回信息
        /// </summary>
        public string fhxx { get; set; }

        /// <summary>
        /// 交易版本号
        /// </summary>
        public string bbh { get; set; }

        /// <summary>
        /// 报文 ID
        /// </summary>
        public string msgid { get; set; }

        /// <summary>
        /// 发卡地行政区划代码
        /// </summary>
        public string xzqhdm { get; set; }

        /// <summary>
        /// 医疗机构代码
        /// </summary>
        public string jgdm { get; set; }
        /// <summary>
        /// 操作员编码
        /// </summary>

        public string czybm { get; set; }

        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string czyxm { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public object xxnr { get; set; }

        /// <summary>
        /// 交易渠道
        /// </summary>
        public string jyqd { get; set; }
        /// <summary>
        /// 交易验证码
        /// </summary>
        public string jyyzm { get; set; }
        /// <summary>
        /// 终端设备识别码
        /// </summary>
        public string zdjbhs { get; set; }
    }
}
