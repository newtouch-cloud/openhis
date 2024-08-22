using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.NationECCodeDll
{
    /// <summary>
    /// 刷脸获取医保用户身份授权接入參
    /// </summary>
    public class EcAuthInput : InputBase 
    {
        /// <summary>
        /// 机构 ID 字符 40 N 医保定点机构代码
        /// </summary>
        public string orgId { get; set; }

        /// <summary>
        /// 定点医药机构本次业务流水号 字符 64 N 不可重复，每次请求都需要唯一
        /// </summary>
        public string outBizNo { get; set; }

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
        /// 扩展参数 字符 N JSON 对象字符
        /// </summary>
        public string extData { get; set; }
    }
}
