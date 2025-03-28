using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.ResourcesOperate
{
    /// <summary>
    /// 门诊取消排药请求报文
    /// </summary>
    public class OutpatientCancelArrangementRequestDTO : RequestBase
    {
        /// <summary>
        /// 目标处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 最大处理处方个数 0：全部处理
        /// </summary>
        public int processesMaxNum { get; set; }

        /// <summary>
        /// 发药药房代码
        /// </summary>
        public string fyyf { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string CreatorCode { get; set; }
    }
}