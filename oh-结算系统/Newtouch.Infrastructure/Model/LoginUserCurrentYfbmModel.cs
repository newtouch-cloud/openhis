using System.Collections.Generic;

namespace Newtouch.Infrastructure.Model
{
    /// <summary>
    /// 登录用户，当前药房部门信息
    /// </summary>
    public class LoginUserCurrentYfbmModel
    {
        /// <summary>
        /// 药房部门Code
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 药房部门级别（1级 药库，2级药房）
        /// </summary>
        public string yfbmjb { get; set; }

        /// <summary>
        /// 当前登录身份 的 药房部门：0药库；1门诊药房；2住院药房   药库直接通过yjbmjb判断就可以了
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 上级 药库 Code List（药房可向目标药库申领）
        /// </summary>
        public IList<string> TheUpperYfbmCodeList { get; set; }

        /// <summary>
        /// 下级 药房 Code List（药库可向目标药房发药）
        /// </summary>
        public IList<string> TheLowerYfbmCodeList { get; set; }


    }
}
