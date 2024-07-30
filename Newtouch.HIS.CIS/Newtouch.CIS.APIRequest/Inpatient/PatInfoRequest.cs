using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.CIS.APIRequest.Inpatient
{
    public class PatInfoRequest: JSONRequestBase
    {
        /// <summary>
        /// 住院号
        /// </summary>
        [Required]
        public string zyh { get; set; }

        /// <summary>
        /// 在院标志
        /// </summary>
        public string zybz { get; set; }
        /// <summary>
        /// 入院诊断
        /// </summary>
        public string ryzd { get; set; }  
        public string ryzdmc { get; set; }
        //病人性质代码 
        public string brxz { get; set; }
        /// <summary>
        /// 病人性质描述
        /// </summary>
        public string brxzmc { get; set; }
        /// <summary>
        ///  联系人姓名
        /// </summary>
        public string lxr { get; set; }

        /// <summary>
        /// 联系人关系
        /// </summary>
        public string lxrgx { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string lxrdh { get; set; }
    }
}
