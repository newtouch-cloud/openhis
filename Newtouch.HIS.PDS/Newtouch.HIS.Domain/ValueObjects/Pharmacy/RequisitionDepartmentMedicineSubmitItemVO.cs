namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 申领单 提交申请
    /// </summary>
    public class RequisitionDepartmentMedicineSubmitItemVO
    {
        /// <summary>
        /// 药品Code
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 申领数量
        /// </summary>
        public int slsl { get; set; }

        /// <summary>
        /// 本部门转换因子（申领部门）
        /// </summary>
        public int bbmzhyz { get; set; }
    }
}
