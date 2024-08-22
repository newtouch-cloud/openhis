using System.ComponentModel;

namespace NeiMengGuYiBaoApp.Models.Post.YiBao
{
    public class Post_1101
    {
        /// <summary>
        /// 就诊凭证类型  字符型  3  Y Y 
        /// </summary>

        public string mdtrt_cert_type { get; set; }
        /// <summary>
        /// 就诊凭证编号  字符型  50  Y  就诊凭证 类型为“01”时填 写电子凭 证令牌，为“02”时填 写身份证 号，为“03” 时填写社 会保障卡卡号
        /// </summary>
        [Description("就诊凭证编号")]
        public string mdtrt_cert_no { get; set; }

        /// <summary>
        /// 人员证件类型  字符型  6 Y
        /// </summary>
        [Description("人员证件类型")]
        public string psn_cert_type { get; set; }

        /// <summary>
        /// 就诊凭证    类型为 “03”时必 填
        /// </summary>
        [Description("就诊凭证")]
        public string card_sn { get; set; }

        /// <summary>
        /// 操作员代码
        /// </summary>
        public string operatorId { get; set; }

        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string operatorName { get; set; }
    }
}
