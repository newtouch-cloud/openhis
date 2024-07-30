namespace NewtouchHIS.Base.Domain.ValueObjects
{
    /// <summary>
    /// 组织机构视图
    /// </summary>
    public class SysOrgVo
    {
        public string? TopOrganizeId { get; set; }
        public string? OrganizeId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? ParentId { get; set; }
        public string? gjjgdm { get; set; }
        public string? zt { get; set; }
    }

    public class SysOrgIndexVo
    {
        public string? OrganizeId { get; set; }
        public string? Name { get; set; }
    }
}
