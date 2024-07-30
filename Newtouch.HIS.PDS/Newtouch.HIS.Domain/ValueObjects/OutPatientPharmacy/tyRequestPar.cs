using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy
{
    /// <summary>
    /// 退药操作参数
    /// </summary>
    public class tyRequestPar
    {
        /// <summary>
        /// 处方明细内码
        /// </summary>
        public int cfmxnm { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 频号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 频次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 药品
        /// </summary>
        public string yp { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int sl { get; set; }
    }

    /// <summary>
    /// 退药信息
    /// </summary>
    public class tyInfo
    {
        /// <summary>
        /// 退药部门
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// 退药单单号
        /// </summary>
        public string returnDrugBillNo { get; set; }

        /// <summary>
        /// 退药药品明细
        /// </summary>
        public List<tyParam> tyDrugDetail { get; set; }

        /// <summary>
        /// 退药单单号
        /// </summary>
        public string zytyapplyno { get; set; }
    }

    /// <summary>
    /// 住院退药请求信息
    /// </summary>
    public class HospitalizationReturnDrugParem
    {
        /// <summary>
        /// 退药单单号
        /// </summary>
        public string returnDrugBillNo { get; set; }

        /// <summary>
        /// 退药申请单单号
        /// </summary>
        public string applyNo { get; set; }

        /// <summary>
        /// 药房部么代码
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// 退药明细
        /// </summary>
        public List<TyRpInfo> rpInfo { get; set; }
    }

    /// <summary>
    /// 退药医嘱细腻
    /// </summary>
    public class TyRpInfo
    {
        /// <summary>
        /// 医嘱ID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// 医嘱执行批次
        /// </summary>
        public List<ExecuteBatchDetail> executeBatchDetail { get; set; }
    }

    /// <summary>
    /// 执行批次信息
    /// </summary>
    public class ExecuteBatchDetail
    {

        /// <summary>
        /// 医嘱执行ID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// 退药医嘱明细
        /// </summary>
        public List<RpDetail> tyRpDetail { get; set; }
    }

    /// <summary>
    /// 医嘱明细
    /// </summary>
    public class RpDetail
    {

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 转化因子
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 药品批次信息
        /// </summary>
        public List<DrugBatch> drugBatch { get; set; }
    }

    /// <summary>
    /// 药品批次信息
    /// </summary>
    public class DrugBatch
    {

        /// <summary>
        /// 数量(部门单位数量，zhyz*sl=最小单位数量)
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 频次
        /// </summary>
        public string pc { get; set; }
    }

    /// <summary>
    /// 退药操作参数
    /// </summary>
    public class tyParam
    {
        /// <summary>
        /// 医嘱ID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// 医嘱执行ID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 频次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 数量(部门单位数量，zhyz*sl=最小单位数量)
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 转化因子
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 退药申请单单号
        /// </summary>
        public string applyNo { get; set; }

        /// <summary>
        /// 药房部么代码
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// 成组号
        /// </summary>
        public string czh { get; set; }

        public string zytyapplyno { get; set; }
        public string zyh { get; set; }
    }
    
}
