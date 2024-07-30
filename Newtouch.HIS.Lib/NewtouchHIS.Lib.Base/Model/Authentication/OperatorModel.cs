namespace NewtouchHIS.Lib.Base.Model
{

    /// <summary>
    /// 登录用户 身份信息
    /// </summary>
    public class OperatorModel
    {
        /// <summary>
        /// 系统用户 Id
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// 系统人员 Id
        /// </summary>
        public string? StaffId { get; set; }

        /// <summary>
        /// 系统用户 Code（登录账号）
        /// </summary>
        public string? UserCode { get; set; }

        /// <summary>
        /// 系统人员 工号
        /// </summary>
        public string? rygh { get; set; }

        /// <summary>
        /// 系统人员 姓名
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 系统人员 科室Code
        /// </summary>
        public string? DepartmentCode { get; set; }

        /// <summary>
        /// 想当前登录用户 开放的系统药房部门Code List
        /// </summary>
        public List<string>? yfbmCodeList { get; set; }

        /// <summary>
        /// 系统用户 角色List
        /// </summary>
        public List<string>? RoleIdList { get; set; }

        /// <summary>
        /// 系统用户 角色List
        /// </summary>
        public List<string>? RoleCodeList { get; set; }

        /// <summary>
        /// 系统用户是否是某某医疗机构的管理员（具体的单一医院、诊所）
        /// 也要求关联单一系统人员
        /// </summary>
        public bool IsHospAdministrator { get; set; }

        /// <summary>
        /// 系统用户 是否是系统管理员
        /// 与IsHospAdministrator的区别：不服务于具体的医疗机构（医院、诊所）
        /// </summary>
        public bool IsAdministrator { get; set; }

        /// <summary>
        /// 系统用户 是否是root账户
        /// 与Administrator的区别：甚至更高级别的权限，一般不向客户开放
        /// </summary>
        public bool IsRoot { get; set; }

        /// <summary>
        /// 登录IP
        /// </summary>
        public string? LoginIPAddress { get; set; }

        /// <summary>
        /// 登录IP 所定位到的地址
        /// </summary>
        public string? LoginIPAddressName { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime? LoginTime { get; set; }

        /// <summary>
        /// 组织机构Id（已经定位到了具体医院）（系统供多家组织机构使用，且统一部署时可借助于此字段）
        /// </summary>
        public string? OrganizeId { get; set; }

        /// <summary>
        /// 顶级组织机构
        /// </summary>
        public string? TopOrganizeId { get; set; }

        /// <summary>
        /// 应用Id（在config配置 AppId）
        /// </summary>
        public string? AppId { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// 身份令牌
        /// </summary>
        public string? token { get; set; }
        /// <summary>
        /// SSO 通用token
        /// </summary>
        public string? access_token { get; set; }
        /// <summary>
        /// 是否需要选择机构
        /// </summary>
        public bool NeedChooseOrgFlag { get; set; } = false;
    }

}
