using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request
{
    public class InPatientCenterRequest: RequestBase
    {
        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string zjh { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// 关键字：姓名、拼音
        /// </summary>
        public string keyword { get; set; }
        /// <summary>
        /// 业务类型 ：mz、zy
        /// </summary>
        public string ywlx { get; set; }
    }
}
