using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.V;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 内部发药申请
    /// </summary>
    public interface IApplyDmnService
    {
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="lastModifierCode"></param>
        /// <returns></returns>
        int Cancel(string crkId, string lastModifierCode);

        /// <summary>
        /// 提交内部发药申请
        /// </summary>
        /// <param name="sysMedicineRequisitionEntity"></param>
        /// <param name="sysMedicineRequisitionDetailEntities"></param>
        /// <returns></returns>
        string SubmitApply(SysMedicineRequisitionEntity sysMedicineRequisitionEntity, List<SysMedicineRequisitionDetailEntity> sysMedicineRequisitionDetailEntities);

        /// <summary>
        /// 获取申领单主信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="ffzt"></param>
        /// <param name="djh"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeid"></param>
        /// <param name="slbm">申领部门</param>
        /// <returns></returns>
        IList<ApplyMainVEntity> GetApplyMainInfo(Pagination pagination, int ffzt, string djh, DateTime startTime, DateTime endTime, string yfbmCode, string organizeid, string slbm = "");

        /// <summary>
        /// 获取申领单明细
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldId"></param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        IList<ApplyDetailVEntity> GetApplyDetails(Pagination pagination, string sldId, string organizeid);

        /// <summary>
        /// 获取申领出库明细
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldIds"></param>
        /// <param name="yfbmCode">当前部门</param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        IList<ApplyOutStockVEntity> GetApplyOutStockDetail(Pagination pagination, string sldIds, string yfbmCode, string organizeid);

        /// <summary>
        /// 修改申领单发放状态
        /// </summary>
        /// <param name="sldId"></param>
        /// <param name="ffzt">目标状态</param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        string UpgradeStatus(string sldId, int ffzt, string organizeid);

        /// <summary>
        /// 提交申领发药 以申领单为原子，结果只有两种 1.全部成功 2.全部失败   没有成功一半的情况 
        /// </summary>
        /// <param name="group">同一申领单发药信息</param>
        /// <param name="source">全部发药信息</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string SubmitAppOutStock(List<ApplyOutStockVEntity> group, List<ApplyOutStockVEntity> source, string organizeId, string userCode);

        /// <summary>
        /// 根据单据ID获取出入库单据明细
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx"></param>
        /// <returns></returns>
        List<ReceiptQueryDetailVO> SelectOutOrInStorageBillDetail(string crkId, int djlx);
        List<DrupreparationMXVO> SelectDrupreparationInfoMX(string byid);
    }
}