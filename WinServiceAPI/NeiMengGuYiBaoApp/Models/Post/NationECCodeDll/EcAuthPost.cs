using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Post.NationECCodeDll
{
    /// <summary>
    /// 刷脸获取医保用户身份信息-前端调用
    /// </summary>
    public class EcAuthPost 
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
    }
}
