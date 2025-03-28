using Newtouch.Core.Common;
using Newtouch.Domain.ValueObjects.API;
using System.Collections.Generic;

namespace Newtouch.Domain.ValueObjects
{
    public class RegisteredInfoResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public Pagination pagination { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<RegisteredInfoRespVO> list { get; set; }
    }
}
