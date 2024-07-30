namespace NewtouchHIS.Lib.Base.Model
{
    /// <summary>
    /// 用户身份（根据项目需要 扩展用户身份的字段）
    /// </summary>
    public class UserIdentity : Identity
    {
        /// <summary>
        /// 组织机构Id（医院）
        /// </summary>
        public string? OrganizeId { get; set; }

        /// <summary>
        /// 顶级组织机构
        /// </summary>
        public string? TopOrganizeId { get; set; }

    }

}
