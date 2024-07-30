using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    /// <summary>
    /// 更新发药标志 请求阐述
    /// </summary>
    public class UpdatefyztRequest : RequestBase
    {
        /// <summary>
        /// 处方内码
        /// </summary>
        public string cfnm { get; set; }

        /// <summary>
        /// 配药人员
        /// </summary>
        public string user_code { get; set; }

        /// <summary>
        /// 领药窗口
        /// </summary>
        public string lyck { get; set; }
    }
}
