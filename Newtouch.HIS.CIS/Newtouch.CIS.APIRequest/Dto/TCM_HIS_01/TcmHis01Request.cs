using System;
using Newtouch.HIS.API.Common;

namespace Newtouch.CIS.APIRequest.Dto.TCM_HIS_01
{
    /// <summary>
    /// TcmHis01 Request
    /// </summary>
    [Serializable]
    public class TcmHis01Request : RequestBase
    {
        /// <summary>
        /// 就诊ID
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// 组织机构id
        /// </summary>
        public string organizeId { get; set; }
    }
}