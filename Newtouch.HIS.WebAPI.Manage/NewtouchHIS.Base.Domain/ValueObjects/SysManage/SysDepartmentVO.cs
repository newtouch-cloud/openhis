namespace NewtouchHIS.Base.Domain.ValueObjects
{
    public class SysDepartmentVO
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 医技标志（是否是医技部门，如放射科）
        /// </summary>
        public bool yjbz { get; set; }

        /// <summary>
        /// 门诊住院标志
        /// </summary>
        public byte mzzybz { get; set; }

        /// <summary>
        /// 首拼
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// 诊疗科室
        /// </summary>
        public string zlks { get; set; }
    }

    public class HisDeptVO
    {
        public string? OrganizeId { get; set; }
        public string? ks { get; set; }
        public string? ksmc { get; set; }
        /// <summary>
        /// 医技标志（是否是医技部门，如放射科）
        /// </summary>
        public bool? yjbz { get; set; }

        /// <summary>
        /// 门诊住院标志
        /// </summary>
        public byte? mzzybz { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        public string? zt { get; set; }

    }
}
