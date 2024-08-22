using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Proxy.guian.DO
{
    /// <summary>
    /// 结算完整流程所需参数
    /// </summary>
    public class SettlementOverallProcessDo
    {
        /// <summary>
        /// 组织机构代码
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 门诊补偿序号
        /// </summary>
        public string outpId { get; set; }
    }

    /// <summary>
    /// 虚拟结算完整流程所需参数
    /// </summary>
    public class SimulationSettlementOverallProcessDo
    {
        /// <summary>
        /// 组织机构代码
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 门诊补偿序号
        /// </summary>
        public string outpId { get; set; }

        /// <summary>
        /// 查询当此门诊已上传费用明细 请求参数
        /// </summary>
        public S22Param S22Param { get; set; }

        /// <summary>
        /// S21接口上传明细
        /// </summary>
        public List<S21Detail> S21Details { get; set; }
    }

    /// <summary>
    /// 查询当此门诊已上传费用明细 请求参数
    /// </summary>
    public class S22Param
    {
        /// <summary>
        /// 开始时间(yyyy-mm-dd)
        /// </summary>
        public string startDate { get; set; }

        /// <summary>
        /// 截止时间(yyyy-mm-dd)
        /// </summary>
        public string endDate { get; set; }
    }

    /// <summary>
    /// S18接口所需参数
    /// </summary>
    public class S18Param
    {
        /// <summary>
        /// 个人编码
        /// </summary>
        public string memberId { get; set; }

        /// <summary>
        /// 就诊时间(yyyy-mm-dd)
        /// </summary>
        public DateTime jzsj{ get; set; }

        /// <summary>
        /// 就诊科室，我们系统所用科室
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// 经治医生
        /// </summary>
        public string doctorName { get; set; }

        /// <summary>
        /// 疾病代码(接口S29下载获取)
        /// </summary>
        public string diseaseCode { get; set; }
    }

    /// <summary>
    /// S21上传明细所需参数
    /// </summary>
    public class S21Detail
    {
        /// <summary>
        /// 明细中文名
        /// </summary>
        public string detailName { get; set; }

        /// <summary>
        /// 农合编码
        /// </summary>
        public string detailCode { get; set; }

        /// <summary>
        /// His明细序号（唯一编号）
        /// </summary>
        public string hisDetailCode { get; set; }
        
        /// <summary>
        /// 本地编码PDA可以上传农合编码
        /// </summary>
        public string detailHosCode { get; set; }
        
        /// <summary>
        /// 本地类别代码
        /// </summary>
        public string typeCode { get; set; }
        
        /// <summary>
        /// 数量（四位小数精度）
        /// </summary>
        public decimal num { get; set; }
        
        /// <summary>
        ///  单价（四位小数精度）
        /// </summary>
        public decimal price { get; set; }
       
        /// <summary>
        /// 总价（四位小数精度）
        /// </summary>
        public decimal totalCost { get; set; }
        
        /// <summary>
        /// 用药日期(yyyy-mm-dd)
        /// </summary>
        public string date { get; set; }
        
        /// <summary>
        /// 单位
        /// </summary>
        public string unit { get; set; }
        
        /// <summary>
        /// 规格
        /// </summary>
        public string standard { get; set; }
        
        /// <summary>
        /// 剂型
        /// </summary>
        public string formulations { get; set; }
    }
}