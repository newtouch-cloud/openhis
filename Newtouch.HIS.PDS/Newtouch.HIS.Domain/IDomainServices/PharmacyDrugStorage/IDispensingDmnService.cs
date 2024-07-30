using System.Collections.Generic;
using Newtouch.HIS.Domain.DO;
using Newtouch.HIS.Domain.Entity;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.Stock;
using Newtouch.PDS.Requset.Zyypyz;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 发药管理
    /// </summary>
    public interface IDispensingDmnService
    {

        /// <summary>
        /// 门诊处方预定（冻结库存）
        /// </summary>
        /// <param name="bookItemDo"></param>
        /// <returns></returns>
        string OutPatientBook(BookItemDo bookItemDo);

        /// <summary>
        /// 门诊处方预定（冻结库存） V2
        /// </summary>
        /// <param name="bookItemDo"></param>
        /// <returns></returns>
        string OutPatientBookV2(BookItemDo bookItemDo);

        /// <summary>
        /// 取消预定
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="sl">最小单位数量</param>
        /// <param name="yfbmCode"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        string OutPatientBookCancel(string ypCode, int sl, string yfbmCode, string cfh, string organizeId, string creatorCode);

        /// <summary>
        /// 取消预定
        /// </summary>
        /// <param name="mxphlist"></param>
        /// <param name="organizeId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        string OutPatientBookCancel(List<OutpatientPrescriptionDetailBatchNumberEntity> mxphlist, string organizeId, string creatorCode);

        /// <summary>
        /// outpatient commit
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        string OutpatientCommit(string ypCode, string yfbmCode, string cfh, string organizeId, string creatorCode);

        /// <summary>
        /// 门诊扣库存(发药)
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string OutpatientDispensingReduceStock(string ypCode, string yfbmCode, string cfh, string organizeId, string userCode);

        /// <summary>
        /// 门诊发药(扣库存、修改处方发药标志、生成发药记录)
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="sl">最小单位数量</param>
        /// <param name="yfbmCode"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="cfmxId">mz_cfmx.id</param>
        /// <returns></returns>
        string OutpatientDispensingDrug(string ypCode, long cfmxId, int sl, string yfbmCode, string cfh, string organizeId, string userCode);

        /// <summary>
        /// 门诊退药还库存
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="zxdwsl"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="ph"></param>
        /// <param name="pc"></param>
        /// <param name="returnDrugBillNo">退药单号</param>
        /// <returns></returns>
        string OutpatientReturnDrugAddStock(string ypCode, string ph,
           string pc, int zxdwsl,
           string yfbmCode, string cfh,
           string organizeId, string userCode, string returnDrugBillNo);

        /// <summary>
        /// 住院Book冻结库存 并保存zy_ypyzzxph
        /// </summary>
        /// <param name="bookItemDo"></param>
        /// <returns></returns>
        string HospitalizationBook(BookItemDo bookItemDo);

        /// <summary>
        /// 住院Book冻结库存 并保存zy_ypyzzxph
        /// </summary>
        /// <param name="yzId">医嘱ID</param>
        /// <param name="zxId">执行ID</param>
        /// <param name="organizeId">组织机构ID</param>
        /// <param name="yfbmCode">药房部门编码</param>
        /// <param name="creatorCode">创建人</param>
        /// <param name="patientName">医嘱所属患者</param>
        /// <returns></returns>
        string HospitalizationBook(string yzId, string zxId, string organizeId, string yfbmCode, string creatorCode,
            string patientName);

        /// <summary>
        /// 取消冻结，并物理删除医嘱信息 慎用
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="zxId"></param>
        /// <returns></returns>
        string CancelForzenAndDeleteYz(string yzId, string zxId);

        /// <summary>
        /// 住院Book冻结库存 并保存zy_ypyzzxph
        /// </summary>
        /// <param name="zxId">执行ID</param>
        /// <param name="yzDetails"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string HospitalizationBook(string zxId, List<YzDetail> yzDetails, string organizeId);

        /// <summary>
        /// 住院Book冻结库存 并保存zy_ypyzzxph
        /// </summary>
        /// <param name="zxId"></param>
        /// <param name="bookItemDo"></param>
        /// <returns></returns>
        string HospitalizationCancelBook(string zxId, BookItemDo bookItemDo);

        /// <summary>
        /// 取消排药
        /// </summary>
        /// <param name="cancelArrangement"></param>
        /// <returns></returns>
        string HospitalizationCancelArrangement(CancelArrangement cancelArrangement);

        /// <summary>
        /// 取消排药 事务
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        string HospitalizationCancelArrangementWithTrans(CancelArrangement param);

        /// <summary>
        /// 住院发药
        /// </summary>
        /// <param name="zyphLs"></param>
        /// <param name="yzLs"></param>
        /// <param name="userCode"></param>
        /// <param name="fyid"></param>
        /// <returns></returns>
        string HospitalizationDispensing(List<ZyYpyzzxphEntity> zyphLs, List<ZyYpyzxxEntity> yzLs, string userCode,out string fyid,string organizeId);

        /// <summary>
        /// 获取药品大类
        /// </summary>
        /// <param name="ypcode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string getYpDl(string ypcode, string organizeId);
        string PrepareMedicine(BYDjInfoDTO yplist, string organizeId,string yhgh);
        string PrepareMedicineReturn(BythDjInfoDTO yplist, string organizeId, string yhgh);
        string WithdrawalPreparationReturn(string Djh, string organizeId, string yhgh, string thzt);
        void Updatezy_brxxexpand(string OrganizeId, string zyh);
        void SyncPatFee(string orgId, string zyh, int zxtype);
    }
}