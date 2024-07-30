using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Post
{
    public class Post_SE01:Post_Base
    {
        /// <summary>
        /// 电子凭证二维码值
        /// </summary>
        public string ecQrCode { get; set; }
        /// <summary>
        /// 获取二维码渠道 1:支付宝 2：微信 3：随身办
        /// </summary>
        public string ecQrChannel { get; set; }
        /// <summary>
        /// 用码业务类型
        /// </summary>
        public string businessType { get; set; }
        /// <summary>
        /// ip信息
        /// </summary>
        public string termId { get; set; }
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
