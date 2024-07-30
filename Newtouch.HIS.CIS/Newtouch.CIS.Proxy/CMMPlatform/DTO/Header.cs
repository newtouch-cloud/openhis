namespace Newtouch.CIS.Proxy.CMMPlatform.DTO
{
    /// <summary>
    /// 头文件
    /// </summary>
    public class Header
    {
        /// <summary>
        /// 发送应用程序，指定为：PLAT
        /// 必填
        /// </summary>
        public string sender { get; set; }

        /// <summary>
        /// 接收应用程序，指定为：HIS,EMR,AE,HEAL,CBS,RC,RT,K
        /// 必填
        /// </summary>
        public string receiver { get; set; }

        /// <summary>
        /// 发送时间，格式：yyyyMMddHHmmss
        /// 必填
        /// </summary>
        public string sendTime { get; set; }

        /// <summary>
        /// 消息类型：TCM_ PLAT _06
        /// 必填
        /// </summary>
        public string msgType { get; set; }

        /// <summary>
        /// 消息 ID(应用程序编码+发送时间) ，如：PLAT20170606020202
        /// 必填
        /// </summary>
        public string msgID { get; set; }
    }
}