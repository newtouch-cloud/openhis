namespace NewtouchHIS.WebAPI.Manage.Models.EMR
{
    public class MRQCScoreRequest
    {
        /// <summary>
        /// 组织机构
        /// </summary>
        public string orgId { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// 病理类型
        /// </summary>
        public string bllxId { get; set; }

    }
}
