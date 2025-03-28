using System;

namespace Newtouch.HIS.Sett.Request.Booking.Request
{
    public class PrescriptionInfoReq
    {

    }

    public class PrescriptionInfoResp
    {
        public string PresId { get; set; }
        /// <summary>
        /// 处方号
        /// </summary>
        public string PresNo { get; set; }
        /// <summary>
        /// 处方金额
        /// </summary>
        public decimal PresAmt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DeptName { get; set; }
        public string DoctorName { get; set; }
        /// <summary>
        /// 处方类型
        /// </summary>
        //public string PresType { get; set; }
        /// <summary>
        /// 处方类型说明
        /// </summary>
        public string PresTypeName { get; set; }
        /// <summary>
        /// 开立时间
        /// </summary>
        public string PresTime { get; set; }
    }

    public class PrescriptionDetailResp
    {
        public string PresId { get; set; }
        /// <summary>
        /// 处方号
        /// </summary>
        public string PresNo { get; set; }
        public string PresItemId { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string PresItemName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amt { get; set; }
        /// <summary>
        /// 项目组合名称
        /// </summary>
        public string ItemGroupName { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        //public string Spec { get; set; }

        //public int? PresTypeName { get; set; }

    }
}
