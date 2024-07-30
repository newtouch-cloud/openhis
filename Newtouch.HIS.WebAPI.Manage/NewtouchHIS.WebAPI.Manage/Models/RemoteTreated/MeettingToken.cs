namespace NewtouchHIS.WebAPI.Manage.Models
{
    public class MeettingTokenRequest
    {
        /// <summary>
        /// 音视频房间号，不同会议的房间号需唯一，长度不超过 50 位，建议使用自增长主键或 UUID
        /// </summary>
        public string roomid { get; set; }
        /// <summary>
        /// 为进入该房间的用户姓名，用以区分不同用户
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 为音视频使用设备，建议取值为 Mobile（移动端），Browser（电脑浏览器）
        /// </summary>
        public string device { get; set; }
    }

    public class MeettingTokenResponse
    {
        /// <summary>
        /// 为音视频账号以 Secret 为密钥进行 AES 对称加密后的结果，调用音视频服务需要用到，请解密后使用
        /// </summary>
        public string rtcuserid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string roomid { get; set; }
        public string? username { get; set; }

        /// <summary>
        /// 若 status 为 Success 则请求成功，若 status 为 Failure 则请求失败，errorCode 里显示失败原因。
        /// </summary>
        public string status { get; set; }
        public string errorCode { get; set; }
    }

    public class MeettingResponse
    {
        /// <summary>
        /// 为音视频账号以 Secret 为密钥进行 AES 对称加密后的结果，调用音视频服务需要用到，请解密后使用
        /// </summary>
        public string rtcuserid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string roomid { get; set; }
        public string? username { get; set; }
        public string? roompath { get; set; }
        public string? organization { get; set; }
        public string? device { get; set; }
    }
}
          