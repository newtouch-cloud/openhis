namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    /// <summary>
    /// 站点收支统计表 收入信息
    /// </summary>
    public class jgssEarningVO
    {
        public string Id { get; set; }
        public string siteId { get; set; }
        public string zdmc { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        public decimal jgss { get; set; }
        public decimal grsss { get; set; }
        public string shzt { get; set; }
        public decimal zsr { get; set; }
        public decimal mzsfzje { get; set; }
        public decimal zysfzje { get; set; }
        public string shr { get; set; }
        public string cjr { get; set; }
        public string jgssfdbl { get; set; }
    }

    public class jgssAttachmentVO {
        public string Id { get; set; }
        public string fjmc { get; set; }
        public string ContentType { get; set; }
        public string fjPath { get; set; }
    }
}
