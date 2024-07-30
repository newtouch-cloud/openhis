using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Input
{
    public class SE01Input:InputBase
    {
        /// <summary>
        /// 医疗机构代码
        /// </summary>
        public string orgId { get; set; }
        /// <summary>
        /// 电子凭证二维码
        /// </summary>
        public string ecQrCode { get; set; }
        /// <summary>
        /// 获取二维码渠道
        /// </summary>
        public string ecQrChannel { get; set; }
        /// <summary>
        /// 用码业务类型
        /// </summary>
        public string businessType { get; set; }
        /// <summary>
        /// 终端 Id
        /// </summary>

        public string termId { get; set; }

        /// <summary>
        /// ip 信息
        /// </summary>
        public string termIp { get; set; }

        /// <summary>
        /// 操作员工号
        /// </summary>
        public string operatorId { get; set; }
        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string operatorName { get; set; }

        /// <summary>
        /// 医保科室编号
        /// </summary>
        public string officeId { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string officeName { get; set; }
    }
}
