using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Clinic
{
    public class MeetingResponseVO
    {
        /// <summary>
        /// 为音视频账号以 Secret 为密钥进行 AES 对称加密后的结果，调用音视频服务需要用到，请解密后使用
        /// </summary>
        public string rtcuserid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string roomid { get; set; }
        public string username { get; set; }
        public string roompath { get; set; }
        public string organization { get; set; }
        public string device { get; set; }
    }
}
