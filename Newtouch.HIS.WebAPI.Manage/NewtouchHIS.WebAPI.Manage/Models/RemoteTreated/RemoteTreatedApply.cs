
namespace NewtouchHIS.WebAPI.Manage.Models
{
    /// <summary>
    /// 申请远程诊疗
    /// </summary>
    public class TreatedApplyInfoRequest
    {
        /// <summary>
        /// 申请流水号 
        /// 由第三方生成 唯一号
        /// </summary>
        public string? sqlsh { get; set; }
        /// <summary>
        /// HIS 诊疗申请Id
        /// </summary>
        public string? ApplyId { get; set; }
        
    }

    public class TreatedApplyReponse
    {

        /// <summary>
        /// HIS 诊疗申请Id
        /// </summary>
        public string? ApplyId { get; set; }
        public string? sqr { get; set; }
        /// <summary>
        /// 申请流水号 
        /// 由第三方生成 唯一号
        /// </summary>
        public string? sqlsh { get; set; }
        /// <summary>
        /// 申请机构
        /// </summary>
        public string? ApplyOrg { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string? ApplyOrgName { get; set; }
        /// <summary>
        /// 申请人工号
        /// </summary>
        public string? sqrgh { get; set; }

    }
}
