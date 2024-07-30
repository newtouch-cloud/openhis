namespace Newtouch.HIS.Proxy.guian.DTO.S29
{
    /// <summary>
    /// 根据时间戳下载疾病字典信息 返回报文
    /// </summary>
    public class item
    {
        /// <summary>
        /// 疾病中文名
        /// </summary>
        public string name { get; set; }
        
        /// <summary>
        /// 疾病编码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string date { get; set; }
    }
}