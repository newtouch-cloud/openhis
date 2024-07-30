namespace Newtouch.HIS.Proxy.guian.DTO.S18
{
    /// <summary>
    /// 根据获取的家庭参合列表进行门诊上传 跨省登记修改也是这个接口，使用uploadType 返回报文
    /// </summary>
    public class S18ResponseDTO
    {
        /// <summary>
        /// 门诊补偿序号
        /// </summary>
        public string outpId { get; set; }
    }
}