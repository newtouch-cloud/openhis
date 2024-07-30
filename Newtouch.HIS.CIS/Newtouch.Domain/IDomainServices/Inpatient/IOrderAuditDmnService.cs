using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IDomainServices
{
    public interface IOrderAuditDmnService
    {
        IList<InpWardPatTreeVO> GetPatWardTree(string staffId);
        IList<OrderAuditVO> GetOrderAuditYZList(Pagination pagination, string patList,string orgId, string IsRehabAuthtoNurse = null);
        //IList<OrderAuditVO> GetOrderAuditYZList(Pagination pagination, string patList, int Yzxz);
        IList<InpWardPatTreeVO> GetPatTree(string staffId,string isQX);
        IList<InpWardPatTreeVO> GetPatTree(string staffId);
        IList<InpWardPatTreeVO> GetWardTree(string staffId);
        string OrderAuditSubmit(OperatorModel user, IList<OrderAuditVO> orderList);
        IList<SysDeptWardRelVO> GetTheLowerKsCodeList(string OrganizeId, string keyword);
        List<DrugStockInfoVEntity> GetDrugAndStock(string yfcode,string keyword, string OrganizeId);
        IList<PharmacyInfoVEntity> GetTheLowerYfbmCodeList(string keyword, string OrganizeId);
        /// <summary>
        /// 审核长期/临时/全部 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="patList"></param>
        /// <param name="Yzxz"></param>
        /// <param name="IsRehabAuthtoNurse"></param>
        string OrderAuditSubmitbyPat(OperatorModel user, string patList, int Yzxz, IList<OrderAuditVO> orderList,string IsRehabAuthtoNurse = null, bool Iskf = false, string zxks = null);
        /// <summary>
        /// 康复医嘱
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="patList"></param>
        /// <param name="orgId"></param>
        /// <param name="zxks"></param>
        /// <returns></returns>
        IList<OrderAuditVO> GetOrderAuditYZList_KF(Pagination pagination, string patList, string orgId, string zxks);

        /// <summary>
        /// 皮试录入树
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        IList<SkintestVO> SkintestVO(string orgId,string keyword,string selectkey);

        /// <summary>
        /// 皮试病人信息展示
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="patList"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SkintestqueryVO> Skintestquery(Pagination pagination, string patList, string orgId, string selectkey);

        void Inputskintestresults(OperatorModel user, IList<SkintestqueryVO> orderList);
        /// <summary>
        /// 多医嘱录入
        /// </summary>
        /// <param name="user"></param>
        /// <param name="yzids"></param>
        /// <param name="lrjg"></param>
        string Enteragain(OperatorModel user, string yzids, string lrjg);

        string Enteragain(OperatorModel user, string zyh,string yzid,string lrjg,string yzlb);


        IList<DisplayinformationVO> Displayinformation(string patList,string orgId);

		/// <summary>
		/// 住院医嘱发药查询
		/// </summary>
		/// <param name="xm"></param>
		/// <param name="bqCode"></param>
		/// <param name="ypmc"></param>
		/// <param name="cw"></param>
		/// <param name="kssj"></param>
		/// <param name="jssj"></param>
		/// <returns></returns>
		IList<OrdersDrugsVO> GetOrdersDrugsGridJson(Pagination pagination, string xm, string bqCode, string ypmc, string cw, string zyh, DateTime kssj, DateTime jssj,string OrganizeId);
        string PrepareMedicine(OperatorModel user, string OrganizeId, BYDjInfoDTO Djnr);
        IList<PreparationStockVO> PreparationStockGridJson(string orgId, Pagination pagination, string ypmc);

        IList<PreparationdrugsVO> PrepareMedicineApplyGridJson(string orgId, Pagination pagination, string ksbyzt, DateTime? kssj = null, DateTime? jssj = null);
        List<PreparationdrugsMXVO> QueryPrepareMedicine(string djId,string orgId);
        string PrepareMedicineSubmit(string ById, string OrganizeId, OperatorModel user);
        string PrepareMedicineback(string Djh, string OrganizeId, OperatorModel user);
        string PrepareMedicinedelete(string ById, string OrganizeId, OperatorModel user);
        string BydjQueryKykc(string ypbm,string pc,string ph, string yfbm, string orgId);
        List<PreparationdrugsVO> QueryPrepareMedicinebyId(string djId, string orgId);
        IList<SkintestInfoVO> GetSkinTestInfoGridJson(Pagination pagination, string zyh, string OrganizeId);
    }
}
