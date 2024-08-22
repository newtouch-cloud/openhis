using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Post.YiBao
{
    public class Post_1162
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
        public string deviceType { get; set; }

        /// <summary>
        /// 获取信息类别 0 表示读卡 1 表示电子凭证读卡
        /// </summary>
        public string mdtrt_cert_type { get; set; }
    }
}
