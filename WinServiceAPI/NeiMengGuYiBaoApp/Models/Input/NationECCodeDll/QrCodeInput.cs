using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.NationECCodeDll
{
    /// <summary>
    /// 终端医保电子凭证码解码接口入參
    /// </summary>
    public class QrCodeInput : InputBase 
    {
        /// <summary>
        /// 机构 ID 字符 40 N 医保定点机构代码
        /// </summary>
        public string orgId { get; set; }

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
    }
}
