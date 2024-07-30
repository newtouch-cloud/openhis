namespace NewtouchHIS.Base.Domain.ValueObjects
{
    public class SysUserOrgandDmtVO 
    {
        public string Id { get; set; }
        public string zt { get; set; }
        public string Account { get; set; }
        public bool? Locked { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeName { get; set; }
    }
}
