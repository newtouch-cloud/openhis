namespace NewtouchHIS.Lib.Services.SignalR
{
    public class SignalRMessageVO
    {
        /// <summary>
        /// 发起人
        /// </summary>
        public string? send_user { get; set; }
        /// <summary>
        /// 接受人
        /// </summary>

        public string get_user { get; set; }
        /// <summary>
        /// 标题
        /// </summary>

        public string title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>

        public string content { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>

        public DateTime? send_date { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>

        public DateTime? created_date { get; set; }
    }


}
