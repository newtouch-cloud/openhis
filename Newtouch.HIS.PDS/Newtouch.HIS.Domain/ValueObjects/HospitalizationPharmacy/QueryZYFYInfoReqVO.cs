using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 查询住院费发药信息
    /// </summary>
    public class QueryZYFYInfoReqVO
    {
        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// 住院药房
        /// </summary>
        public string zyyf { get; set; }

        /// <summary>
        /// 病区编码
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// 药房部门代码
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string Zyh { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime Kssj { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime Jssj { get; set; }

        /// <summary>
        /// 发药状态 0：未发；1：已排；2：已发；3：已退
        /// </summary>
        public string Fyzt { get; set; }

        /// <summary>
        /// 操作类型 1：发药  2：退药
        /// </summary>
        public string operateType { get; set; }

        /// <summary>
        /// 申请退药标志 0：否  1：是
        /// </summary>
        public string sqtybz { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// 医嘱性质
        /// </summary>
        public string yzxz { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 床位号
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime TimeStamp { get; set; }
    }

    public class QueryZYFYInfoReqVOV2 : QueryZYFYInfoReqVO
    {
        public string operatetime { get; set; }
    }

    /// <summary>
    /// 住院发药药
    /// </summary>
    public class HospitalizationDispenseDrugParam : HospitalizationReturnDrugParam
    {

        /// <summary>
        /// 医嘱执行ID
        /// </summary>
        public string yzId { get; set; }
        /// <summary>
        /// 医嘱性质
        /// </summary>
        public string yzxz { get; set; }
    }

    /// <summary>
    /// 住院退药
    /// </summary>
    public class HospitalizationReturnDrugParam
    {
        /// <summary>
        /// 住院号
        /// </summary>
        public string Zyh { get; set; }

        /// <summary>
        /// 申请退药开始时间
        /// </summary>
        public DateTime Kssj { get; set; }

        /// <summary>
        /// 申请退药结束时间
        /// </summary>
        public DateTime Jssj { get; set; }

        /// <summary>
        /// 床位
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 药房部门代码
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 组织结构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 发药标志  0：未发；1：已排；2：已发；3：已退
        /// </summary>
        public string fybz { get; set; }

        public string zytyapplyno { get; set; }
    }

    public class ZyTyApplyNoVO
    {
        public string zytyapplyno { get; set; }

        public string zyh { get; set; }
    }
}
