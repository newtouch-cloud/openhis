namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_PLAT_06
{
    /// <summary>
    /// 推送患者信息请求体
    /// </summary>
    public class PullUserRequestEntity : RequestBase
    {
        /// <summary>
        /// 处理类型请求体
        /// </summary>
        public HandleType HandleType { get; set; }

        /// <summary>
        /// 人员信息体
        /// </summary>
        public User User { get; set; }
    }

    /// <summary>
    /// 处理类型请求体
    /// </summary>
    public class HandleType
    {
        /// <summary>
        /// 处理类型：A-新增，U-修改
        /// 必填
        /// </summary>
        public string handleType { get; set; }
    }

    /// <summary>
    /// 人员信息体
    /// </summary>
    public class User
    {
        /// <summary>
        ///人员编号（用户名）
        /// 必填
        /// </summary>
        /// <returns></returns>
        public string id { get; set; }

        /// <summary>
        ///默认值：1
        /// 必填
        /// </summary>
        /// <returns></returns>
        public string typeCode { get; set; }

        /// <summary>
        ///工号
        /// </summary>
        /// <returns></returns>
        public string jobNo { get; set; }

        /// <summary>
        ///姓名
        /// 必填
        /// </summary>
        /// <returns></returns>
        public string name { get; set; }

        /// <summary>
        ///性别编码
        /// 必填
        /// </summary>
        /// <returns></returns>
        public string genderCode { get; set; }

        /// <summary>
        ///性别
        /// 必填
        /// </summary>
        /// <returns></returns>
        public string gender { get; set; }

        /// <summary>
        ///证件类型编码
        /// 必填
        /// </summary>
        /// <returns></returns>
        public string cardTypeCode { get; set; }

        /// <summary>
        ///证件类型
        /// 必填
        /// </summary>
        /// <returns></returns>
        public string cardType { get; set; }

        /// <summary>
        ///证件号码
        /// 必填
        /// </summary>
        /// <returns></returns>
        public string cardNo { get; set; }

        /// <summary>
        ///出生日期-YYYYMMDD
        /// </summary>
        /// <returns></returns>
        public string birthday { get; set; }

        /// <summary>
        ///手机
        /// </summary>
        /// <returns></returns>
        public string telephone { get; set; }

        /// <summary>
        ///邮箱
        /// </summary>
        /// <returns></returns>
        public string email { get; set; }

        /// <summary>
        ///介绍
        /// </summary>
        /// <returns></returns>
        public string introduce { get; set; }

        /// <summary>
        ///职称编码-医师职称编码
        /// </summary>
        /// <returns></returns>
        public string proTitleCode { get; set; }

        /// <summary>
        ///职称
        /// </summary>
        /// <returns></returns>
        public string proTitle { get; set; }

        /// <summary>
        ///所属机构编码
        /// 必填
        /// </summary>
        /// <returns></returns>
        public string orgCode { get; set; }

        /// <summary>
        ///所属科室编码
        /// 必填
        /// </summary>
        /// <returns></returns>
        public string deptCode { get; set; }

        /// <summary>
        ///角色编码 0： 省管理员,1： 中医馆管理员,2： 医生
        /// 必填
        /// </summary>
        /// <returns></returns>
        public string roleCode { get; set; }

        /// <summary>
        ///机构编号（唯一标识）
        /// 必填
        /// </summary>
        /// <returns></returns>
        public string oid { get; set; }
    }
}