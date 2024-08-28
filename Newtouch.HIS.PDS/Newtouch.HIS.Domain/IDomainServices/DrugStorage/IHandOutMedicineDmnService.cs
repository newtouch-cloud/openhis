using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO.DrugStorage;
using Newtouch.HIS.Domain.Entity.V;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 出库
    /// </summary>
    public interface IHandOutMedicineDmnService
    {
        /// <summary>
        /// 输入码自动提示
        /// </summary>
        /// <returns></returns>
        List<HandOutMedicinesrmVO> GetsrmInfoFlevelList(string yfbmCode, string srm);

        /// <summary>
        ///  获取科室药品库存记录
        /// </summary>
        /// <param name="ypdm"></param>
        /// <returns></returns>
        List<MedicinepcInfo> GetpcList(string ypdm);

        /// <summary>
        /// 发药(直接出库)
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="lybm"></param>
        /// <param name="fydh"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        string HandOutMedicine(List<XT_YP_LS_NBFYMXK> xtYpLsNbfymxk, string lybm, string fydh, string fyfs, int type);

        /// <summary>
        /// 药房向科室发药
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="lybm"></param>
        /// <param name="pdh"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        string DispensingMedicineToKs(List<XT_YP_LS_NBFYMXK> xtYpLsNbfymxk, string lybm, string pdh, string fyfs, int type);

        /// <summary>
        /// 申领出库
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="fydh"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        string HandOutMedicineByRequest(Dictionary<string, List<XT_YP_LS_NBFYMXK>> xtYpLsNbfymxk, string fydh, string fyfs, int type);

        /// <summary>
        /// 退药
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="tydh"></param>
        /// <param name="fyfs"></param>
        /// <param name="tybm"></param>
        /// <param name="type"></param>
        string RequestOfReturnMedicine(List<XT_YP_LS_NBFYMXK> xtYpLsNbfymxk, string tybm, string tydh, string fyfs, int type);

        /// <summary>
        /// 获取退药药品明细
        /// </summary>
        /// <param name="ckbm">出库部门 申请退药的部门</param>
        /// <param name="rkbm">入库部门 药品即将退回到的部门</param>
        /// <param name="srm">输入码 关键字</param>
        /// <returns></returns>
        List<HandOutMedicinesrmVO> GetTyypBySrm(string ckbm, string rkbm, string srm);

        /// <summary>
        /// 获取药品信息  根据批次批号分组
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<HandOutMedicinesrmVO> GetDrugGroupByPc(string ypCode, string yfbmCode, string organizeId);

        /// <summary>
        /// 获取批量出库药品库存信息
        /// </summary>
        /// <param name="yfbmCode">当前部门</param>
        /// <param name="rkbm">入库部门</param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        IList<DrugStockInfoVEntity> GetDirectDeliveryDrugsList(Pagination pagination, string yfbmCode, string rkbm, string organizeid);

        /// <summary>
        /// 获取批量出库药品库存信息
        /// </summary>
        /// <param name="yfbmCode">当前部门</param>
        /// <param name="rkbm">入库部门</param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        IList<DrugStockInfoVEntity> GetDirectDeliveryDrugsList(string yfbmCode, string rkbm, string organizeid);

        /// <summary>
        /// 前天提交批量直接出库
        /// </summary>
        /// <param name="paraDto"></param>
        /// <returns></returns>
        string DirectDeliveryBatchSubmit(DirectDeliveryBatchDTO paraDto);
    }
}
