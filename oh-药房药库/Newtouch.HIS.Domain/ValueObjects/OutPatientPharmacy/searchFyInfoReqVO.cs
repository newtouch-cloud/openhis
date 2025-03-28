using System;

namespace Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy
{
    /// <summary>
    /// 门诊发药查询界面查询条件
    /// </summary>
    public class searchFyInfoReqVO
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? begindate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? enddate { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string keyWord { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 药房部门代码
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }
    }
}
