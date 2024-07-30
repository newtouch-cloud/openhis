using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Post
{
    public class Post_1161
    {
        /// <summary>
        ///  机构 ID 字符型 40 Y 医保定点机构代码
        /// </summary>
        public string orgId { get; set; }

        /// <summary>
        ///  用码业务类型 字符型 5 Y Y 代码为对应：code_biz_type
        /// </summary>
        public string businessType { get; set; }

        /// <summary>
        /// 收款员编号 字符型 20 Y
        /// </summary>
        public string operatorId { get; set; }

        /// <summary>
        ///  收款员姓名 字符型 30 Y
        /// </summary>
        public string operatorName { get; set; }

        /// <summary>
        ///  医保科室编号 字符型 20 Y
        /// </summary>
        public string officeId { get; set; }

        /// <summary>
        ///  科室名称 字符型 30 Y
        /// </summary>
        public string officeName { get; set; }

        /// <summary>
        ///  deviceType 设备类型 字符型 30 Y 自助机该字段设为SelfService，其它情况不用设置
        /// </summary>
        //public string deviceType { get; set; }

        /// <summary>
        /// 获取信息类别 0 表示读卡 1 表示电子凭证读卡
        /// </summary>
        //public string mdtrt_cert_type { get; set; }
        /// <summary>
        /// 电子凭证展码
        /// </summary>
        public string mdtrt_cert_no { get; set; }
        /// <summary>
        /// 就诊凭证    类型为 “03”时必 填
        /// </summary>
        [Description("就诊凭证")]
        public string card_sn { get; set; }
        public string mdtrt_cert_type { get; set; }
    }
}
