using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.VO;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 排药
    /// </summary>
    public interface IPyDmnService
    {

        /// <summary>
        /// 取消排药
        /// </summary>
        /// <param name="mzCfmxEntity"></param>
        /// <returns></returns>
        string CancelArrangedDrug(MzCfmxEntity mzCfmxEntity);

        /// <summary>
        /// 获取待处理事件总数
        /// </summary>
        /// <returns></returns>
        NeedDealCountVO GetNeedDealCount();

        /// <summary>
        /// 获取药库待处理事件总数
        /// </summary>
        /// <returns></returns>
        NeedDealCountVO GetNeedDealCountByYk();

        /// <summary>
        /// 获取药房待处理事件总数
        /// </summary>
        /// <returns></returns>
        NeedDealCountVO GetNeedDealCountByYf();

        /// <summary>
        /// 获取门诊月处方发药次
        /// </summary>
        /// <returns></returns>
        List<FyCountVoByYfbm> GetMzFyCountVoByYfbm();

        /// <summary>
        /// 获取住院月医嘱发药次
        /// </summary>
        /// <returns></returns>
        List<FyCountVoByYfbm> GetZyFyCountVoByYfbm();

        /// <summary>
        /// 获取医嘱次|处方药品次
        /// </summary>
        /// <returns></returns>
        List<FyCountBydlVO> GetFyCountBydl();

        /// <summary>
        /// 插入门诊处方信息
        /// </summary>
        /// <param name="mzCfEntities"></param>
        /// <param name="mzCfmxEntities"></param>
        /// <returns></returns>
        string InsertOutpatientRpInfo(List<MzCfEntity> mzCfEntities, List<MzCfmxEntity> mzCfmxEntities);


        /// <summary>
        /// 物理删除处方主表和明细表   慎用
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        int DeleteRpInfo(string cfh, string organizeId, string yfbmCode);

        /// <summary>
        /// 查询处方信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="xm"></param>
        /// <param name="cardNo"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="ksmc">科室名称</param>
        /// <param name="kssj">排药时间</param>
        /// <param name="jssj">排药时间</param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="jszt">结算状态 0-未结算 1-已结算</param>
        /// <param name="settExpired">结算已过期 1-是 0-否</param>
        /// <returns></returns>
        IList<CfxxVO> SelectRpList(Pagination pagination, string xm, string cardNo, string invoiceNo, string ksmc
            , DateTime kssj, DateTime jssj, string organizeId, string yfbmCode, int jszt, string settExpired);

        /// <summary>
        /// 查询处方信息
        /// </summary>
        /// <param name="xm"></param>
        /// <param name="cardNo"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="ksmc">科室名称</param>
        /// <param name="kssj">排药时间</param>
        /// <param name="jssj">排药时间</param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="jszt">结算状态 0-未结算 1-已结算</param>
        /// <returns></returns>
        List<CfxxVO> SelectRpList(string xm, string cardNo, string invoiceNo, string ksmc, DateTime kssj, DateTime jssj, string organizeId, string yfbmCode, int jszt);

        /// <summary>
        /// 查询处方明细
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        List<CfmxVO> SelectRpDetail(string cardNo, string cfh, string organizeId, string yfbmCode);

        /// <summary>
        /// 查询处方排药明细
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        List<CfmxVO> SelectRpArrangementDetail(string cardNo, string cfh, string organizeId, string yfbmCode);

        /// <summary>
        /// 取消排药
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string CancelArrangedDrug(string cardNo, string cfh, string yfbmCode, string organizeId, string userCode);

        /// <summary>
        /// 门诊排药
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        string DrugArrangement(patientInfoVO patientInfo, string yfbmCode, string organizeId,
           string creatorCode);

        /// <summary>
        /// 获取已排未发未结算的排药信息
        /// </summary>
        /// <param name="expirationDate"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<OutpatientPrescriptionDetailBatchNumberEntity> SelectMxph(DateTime expirationDate, string yfbmCode, string organizeId);

        /// <summary>
        /// 获取要取消的处方
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="processesMaxNum"></param>
        /// <param name="expirationDate"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<RpCancelVo> SelectCancelRps(string cfh, int processesMaxNum, DateTime expirationDate, string yfbmCode, string organizeId);

        /// <summary>
        /// 取消排药
        /// </summary>
        /// <param name="mxphs"></param>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string OutPatientCancelArrangementDrug(List<OutpatientPrescriptionDetailBatchNumberEntity> mxphs, string yfbmCode, string userCode, string organizeId);

        /// <summary>
        /// 门诊取消排药
        /// </summary>
        /// <param name="cfInfo"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string OutPatientCancelArrangement(RpCancelVo cfInfo, string userCode);

        /// <summary>
        /// 排药归架，解冻库存
        /// </summary>
        /// <param name="mxphs"></param>
        /// <param name="userCode"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        string OutPatientSubtractForzenKcAndGj(List<OutpatientPrescriptionDetailBatchNumberEntity> mxphs, string userCode, Infrastructure.EF.EFDbTransaction db = null);

    }
}
