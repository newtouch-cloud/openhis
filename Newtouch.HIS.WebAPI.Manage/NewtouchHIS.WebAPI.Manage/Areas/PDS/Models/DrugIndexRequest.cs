namespace NewtouchHIS.WebAPI.Manage.Areas.PDS.Models
{
    /// <summary>
    /// 药品查询索引
    /// </summary>
    public class DrugIndexRequest
    {
        /// <summary>
        /// 药品大类
        /// </summary>
        public string? dlcode { get; set; }
        /// <summary>
        /// 药品编码
        /// </summary>
        public string? ypcode { get; set; }
        /// <summary>
        /// 药品名称
        /// </summary>
        public string? ypmc { get; set; }
        /// <summary>
        /// 机构Id
        /// </summary>
        public string orgId { get; set; }
        /// <summary>
        /// 药房部门
        /// </summary>
        public string yfbm { get; set; }

    }
}
