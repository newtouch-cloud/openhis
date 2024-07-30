namespace NewtouchHIS.Base.Domain.ValueObjects
{
    public class MsgNoticeVO
    {
        public string? Id { get; set; }
        public string? OrganizeId { get; set; }
        public int? msgtypecode { get; set; }
        public string? msgtype { get; set; }
        public int? ywlx { get; set; }
        public int? px { get; set; }
        public string? CreatorCode { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string? LastModifierCode { get; set; }
        public string zt { get; set; }
    }
}
