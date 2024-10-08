﻿using System.Xml.Serialization;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_07
{
    /// <summary>
    /// 提取电子病历请求体
    /// </summary>
    [XmlRoot("Request")]
    public class ReceiveRequestEntity : RequestBase
    {
        /// <summary>
        /// 接收信息请求体
        /// </summary>
        public Receive Receive { get; set; }
    }


}