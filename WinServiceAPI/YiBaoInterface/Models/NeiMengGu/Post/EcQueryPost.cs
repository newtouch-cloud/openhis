using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.NeiMengGu.Input
{
    /// <summary>
    /// 刷脸获取医保用户身份信息-前端调用
    /// </summary>
    public class EcQueryPost 
    {
        /// <summary>
        /// hisId唯一his的唯一标识
        /// </summary> 
        public string hisId { get; set; }

        /// <summary>
        /// 用码业务类型 字符 5 N 详见附录 A.1
        /// </summary>
        public string businessType { get; set; }

        /// <summary>
        /// 收款员编号 字符 20 N 收款员编号
        /// </summary>
        public string operatorId { get; set; }

        /// <summary>
        /// 收款员姓名 字符 30 N 收款员姓名
        /// </summary>
        public string operatorName { get; set; }

        /// <summary>
        /// 医保科室编号 字符 20 N 医保科室编号
        /// </summary>
        public string officeId { get; set; }

        /// <summary>
        /// officeName 科室名称 字符 30 N 科室名称
        /// </summary>
        public string officeName { get; set; }
        /// <summary>
        /// 设备类型 字符 30 Y 自助机该字段设为 SelfService, 其它情况不用设置
        /// </summary>
        public string deviceType { get; set; }

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
    }
}
