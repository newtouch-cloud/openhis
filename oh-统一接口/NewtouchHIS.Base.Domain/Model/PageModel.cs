namespace NewtouchHIS.Base.Domain.Model
{
    /// <summary>
    /// 分页响应 Model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageRespModel<T>
    {
        public int total { get; set; }
        public T? rows { get; set; }
    }

    public class QueryParamsBase
    {
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string? keyword { get; set; }
        /// <summary>
        /// 机构Id
        /// </summary>
        public string? orgId { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string? code { get;set; }
        public List<string>? stringList { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        public string? kscode { get; set; }
        /// <summary>
        /// 发药开始时间
        /// </summary>
        public DateTime? fykssj { get; set; }
        /// <summary>
        /// 发药结束时间
        /// </summary>
        public DateTime? fyjssj { get; set; }
        /// <summary>
        /// 药品编码
        /// </summary>
        public string? ypdm { get; set; }
        /// <summary>
        /// 出入库明细ID
        /// </summary>
        public string? crkmxId { get; set; }
        /// <summary>
        /// 1 已同步 其余未同步
        /// </summary>
        public string? zstbzt { get; set; }
        public string? isyfy { get; set; }
    }


}
