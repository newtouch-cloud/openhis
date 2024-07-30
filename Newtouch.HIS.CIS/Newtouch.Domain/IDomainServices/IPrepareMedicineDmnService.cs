using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Domain.Entity;
using Newtouch.Domain.Entity.Inpatient;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IDomainServices
{
    public interface IPrepareMedicineDmnService
    {
        #region 报损报益
        /// <summary>
        /// 查询损益药品list
        /// </summary>
        /// <param name="inputCode">关键字</param>
        /// <returns></returns>
        List<ReportLossAndProfitMedicineInfoVO> SelectLossAndProfitMedicineList(string inputCode);

        /// <summary>
        /// 查询药品的kcsl和jj
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="ph"></param>
        /// <param name="pc"></param>
        /// <param name="yxq"></param>
        /// <param name="kcsl"></param>
        /// <param name="jj"></param>
        //void SelectKcslAndJj(string ypCode, string ph, string pc, DateTime? yxq, out int kcsl, out decimal jj);

        ///// <summary>
        ///// 表更库存数量
        ///// </summary>
        ///// <param name="ypCode"></param>
        ///// <param name="ph"></param>
        ///// <param name="pc"></param>
        ///// <param name="yxq"></param>
        //void UpdateStockQuantity(string ypCode, string pc, int sysl);



        /// <summary>
        /// 提交报损报益
        /// </summary>
        /// <param name="syxx"></param>
        /// <returns></returns>
        string SubmitReportLossAndProfit(SysMedicineProfitLossEntity syxx);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="kcsl"></param>
        /// <param name="jj"></param>
        //void SaveReportLossAndProfit(SysMedicineProfitLossEntity entity, int kcsl, decimal jj);

        /// <summary>
        /// 报损报溢查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="inputCode"></param>
        /// <param name="syyy"></param>
        /// <param name="syqk"></param>
        /// <returns></returns>
        //IList<LossAndProditInfoVO> SelectLossAndProditInfoList(Pagination pagination, string startTime, string endTime, string inputCode, string syyy, int syqk);

        /// <summary>
        /// 批发价总金额、零售价总金额查询
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="syyy">损益原因</param>
        /// <param name="inputCode">关键字</param>
        /// <param name="syqk">损益情况</param>
        /// <returns></returns>
        //LossAndProditInfoJeVo ComputePjzeAndLjze(string startTime, string endTime, string syyy, string inputCode, int syqk);

        /// <summary>
        /// 报损报溢 保存
        /// </summary>
        /// <param name="profitLossEntityList"></param>
        //string SaveReportLossAndProfit(List<YpSyxxVo> profitLossEntityList);


        /// <summary>
        /// 报损报溢查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="inputCode"></param>
        /// <param name="syyy"></param>
        /// <param name="syqk"></param>
        /// <returns></returns>
        IList<LossAndProditInfoVO> SelectLossAndProditInfoList(Pagination pagination, string startTime, string endTime, string inputCode, string syyy, int syqk);

        LossAndProditInfoJeVo ComputePjzeAndLjze(string startTime, string endTime, string syyy, string inputCode, int syqk);
        
        #endregion

        #region 科室备药退回
        string PrepareMedicineReturnSubmit(OperatorModel user, string orgId, BythDjInfoDTO Djnr);
        List<DrugStockInfoVEntity> GetDrugAndStock(string yfcode, string keyWord, string organizeid);
        List<DrugStockInfoVEntity> GetApplyDrugAndStock(string ypcodestr,string yfbmstr, string organizeid);
        IList<PrepareMedicineReturnVO> GetPrepareMedicineReturnGridJson(Pagination pagination, string thzt, DateTime kssj, DateTime jssj, string OrganizeId);

        List<PrepareMedicineReturnMXEntity> QueryPrepareMedicine(string djId, string orgId);
        string UpdatePrepareMedicineReturn(PrapareMedicineReturnEntity entity);
        List<PrepareMedicineReturnVO> QueryPrepareMedicineReturnbyId(string djId, string orgId);
        List<PrepareMedicineReturnMXEntity> QueryPrepareMedicineReturnMXbyId(string djId, string orgId);
        string BydjQueryKykc(string ypbm, string pc, string ph, string yfbm, string orgId);
        string PrepareMedicineReturnback(string Djh, string orgId, OperatorModel user);
        string PrepareMedicineReturndelete(string ById, string OrganizeId, OperatorModel user);
        #endregion

        #region 药品使用查询
        IList<MedicineInfoVO> GetMedicineInfoListV2(Pagination pagination, MedicineInfoParam param, string orgId);
        #endregion
    }
}
