using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_2201 : InputBase
    {
        public data2201 data { get; set; }
    }

    public class data2201
    {
        /// <summary>
        ///  人员编号 字符型 30 Y
        /// </summary>
        public string psn_no { get; set; }

        /// <summary>
        /// 险种类型 字符型 6 Y Y
        /// </summary>
        public string insutype { get; set; }

        /// <summary>
        ///  开始时间 日期时间型 Y 挂号时间yyyy-MM-ddHH:mm:ss
        /// </summary>
        public DateTime begntime { get; set; }

        /// <summary>
        ///  就诊凭证类型 字符型 3 Y Y
        /// </summary>
        public string mdtrt_cert_type { get; set; }

        /// <summary>
        /// mdtrt_cert_no 就诊凭证编号 字符型 50 Y 就诊凭证类型为“01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号
        /// </summary>
        public string mdtrt_cert_no { get; set; }

        /// <summary>
        ///   住院/门诊号 字符型 30 Y 院内唯一流水
        /// </summary>
        public string ipt_otp_no { get; set; }

        /// <summary>
        ///  医师编码 字符型 30 Y
        /// </summary>
        public string atddr_no { get; set; }

        /// <summary>
        ///  医师姓名 字符型 50 Y
        /// </summary>
        public string dr_name { get; set; }

        /// <summary>
        /// 科室编码 字符型 30 Y
        /// </summary>
        public string dept_code { get; set; }

        /// <summary>
        /// 科室名称 字符型 100 Y
        /// </summary>
        public string dept_name { get; set; }

        /// <summary>
        ///   科别 字符型 6 Y Y
        /// </summary>
        public string caty { get; set; }
    }
}
