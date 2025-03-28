using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 包含 门诊记账页面部分功能
    /// </summary>
    public interface IOutPatientDmnService
    {
        /// <summary>
        /// 挂号排班List
        /// </summary>
        /// <param name="keyword">科室/医生</param>
        /// <param name="mzlx">门诊类型 普通门诊/急症/专家门诊</param>
        /// <returns></returns>
        List<RegScheduleVO> GetRegScheduleList(string keyword, string mzlx, string orgId);

        /// <summary>
        /// 获取新的排班List
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="mzlx"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<RegNewScheduleVO> GetNewRegScheduleList(string keyword, string mzlx, string orgId);
        /// <summary>
        /// 获取新挂号排班（治疗组合）
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="mzlx"></param>
        /// <param name="pbks"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<RegNewScheduleVO> GetRegScheduleListbyGroup(string keyword, string mzlx, string pbks, string orgId, DateTime? OutDate, string ys);
        /// <summary>
        /// 已预约挂号list
        /// </summary>
        /// <param name="mzxl"></param>
        /// <param name="patid"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<MzghbookScheduleVO> GetIsMzghBookSchedule(Pagination pagination,string mzxl, string patid, string orgId,string isfeegroup, string yyzt, string ghly, string keyValue,
           string ks, DateTime? yykssj = null, DateTime? yyjssj = null);

        string GetInvoiceListByEmpNo(string userCode, string orgId);

        /// <summary>
        /// check发票号是否被使用过， 已被使用过返回true
        /// </summary>
        /// <param name="invoiceNo">发票号No</param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool ExistsForInvoiceNo(string invoiceNo, string orgId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        OutPatientBasicInfoVO GetBasicInfoByCardNo(string keyValue);

        Int16 GetJzxh(string ghlx, string ks, string ys, string ghzb, string userCode, string orgId);
        List<SysPatiNatureEntity2VO> GetValidBrxzList(string text, string ghlx = "", string ybtsdy = "", string syybbf = "", string fw = "", List<string> brxzList = null);

        /// <summary>
        /// 获取挂号排班列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <param name="ghpbId"></param>
        /// <returns></returns>
        List<OutpatientRegistScheduleVO> GetOutpatientRegistScheduleList(string orgId, string keyword = null, int? ghpbId = null);
		/// <summary>
		/// 自助机获取挂号排班
		/// </summary>
		/// <param name="mzlx"></param>
		/// <param name="pbrq"></param>
		/// <param name="orgId"></param>
		/// <returns></returns>
		List<zzjRegScheduleVO> GetZzjRegScheduleList(DateTime pbrq, string orgId);
		#region 门诊记账

		/// <summary>
		/// 门诊记账保存操作
		/// </summary>
		/// <param name="bacDto"></param>
		/// <param name="accDto"></param>
		/// <param name="orgId"></param>
		/// <param name="isOptimaApi">是否Optima接口对应的记账，默认为空，1表示是(记账计划不做数据处理)，null和0表示否</param>
		/// <param name="isClinic">是否诊所记账，默认为空，1表示是（提醒商保次数），null和0表示否（提醒医保次数）</param>
		void SaveOutpatientAccountInfo(OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> accDto, string orgId, string isOptimaApi = null, string isClinic = null);


        #endregion

        /// <summary>
        /// 门诊记账 保存结算实体赋值
        /// </summary>
        /// <param name="brxz"></param>
        /// <param name="gridDto"></param>
        /// <param name="jsEntity"></param>
        /// <param name="jzjhmxEntity"></param>
        /// <param name="jzjhEntity"></param>
        /// <param name="orgId"></param>
        /// <param name="patid"></param>
        /// <param name="bacDto"></param>
        /// <param name="mzjsList"></param>
        /// <param name="jzsjList"></param>
        /// <returns></returns>
        OutPatChargeSettInAccDataVo GetOutPatChargeAccuDbvo(int patid, string brxz,
           IList<OutpatAccGridInfoDto> gridDto, out SettlementEntityVO jsEntity,
           out List<OutpatientAccountDetailEntity> jzjhmxEntity, OutpatientAccountEntity jzjhEntity, string orgId,
           OutpatAccBasicInfoDto bacDto, out List<OutpatientSettlementEntity> mzjsList, out List<DateTime> jzsjList, out Dictionary<string, string> optimaId, string isOptimaApi = null, string isClinic = null);

        #region optima记账 包括门诊记账和住院记账，不包含结算表和记账计划 Vision-1.2 门诊记账的第二个版本
        void CommitAccounting(string patientType, OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> accDto, string orgId);
        #endregion
        #region 门诊收费 Vision-1.3 门诊记账的第三个版本
        /// <summary>
        /// 保存门诊记账
        /// </summary>
        void SavepatientAccountInfo(OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> accDto, string orgId);
        #endregion

        #region Vision-1.4 门诊记账第四个版本 增加临时和长期，单次治疗量
        /// <summary>
        /// 保存门诊记账
        /// </summary>
        void SaveoutpatientAccountInfo(OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> accDto, string orgId, string userCode);
        /// <summary>
        /// 门诊停止计划
        /// </summary>
        /// <param name="db"></param>
        /// <param name="jzjhmxId"></param>
        /// <param name="orgId"></param>
        void overAccountingPlan(string jzjhmxId, string orgId, string UserCode);

        /// <summary>
        /// 门诊停止记账计划（mz_jzjhmx）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="jzjhmxId"></param>
        /// <param name="orgId"></param>
        void overjzjhmx(string jzjhmxId, string orgId);

        /// <summary>
        /// 未执行计划时，修改记账计划
        /// </summary>
        /// <param name="jzjhmxId"></param>
        /// <param name="orgId"></param>
        /// <param name="type">1表示 门诊 2表示住院</param>
        void delAccountingPlan(Infrastructure.EF.EFDbTransaction db, string jzjhmxId, string orgId, string type);

        /// <summary>
        /// 记账计划查询
        /// </summary>
        List<AccountingPlanVO> SelectAccountingPlanList(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj
            , int? zxzt, string orgId, int? zsftx, int? sycstx
            , string sfzt);


        /// <summary>
        /// 保存记账计划和记账计划明细到数据库
        /// </summary>
        /// <param name="jzjh"></param>
        /// <param name="jzjhmxList"></param>
        void OutpatientAccountDBInCharge(OutpatientAccountEntity jzjh, List<OutpatientAccountDetailEntity> jzjhmxList, List<OutpatientItemEntity> mz_xmList);
        /// <summary>
        /// 保险剩余次数
        /// </summary>
        /// <param name="jzsjList"></param>
        /// <param name="orgId"></param>
        /// <param name="patid"></param>
        void Execsycs(List<DateTime> jzsjList, string orgId, string patid);
        #endregion



        #region 根据门诊号查询关联处方（但不包括zt=0处方）

        /// <summary>
        /// 根据门诊号查询关联药品处方（未发药 退费）（但不包括zt=0处方）
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <param name="refundDict"></param>
        /// <returns></returns>
        IList<OutpatientMedicineRefundVO> GetRelatedYpDetailListByMzh(string mzh, string orgId, Dictionary<int, decimal> refundDict);

		#endregion

		#region  chongqing医保

	    List<RefundPrescriptionsInPut> GetCqyb10Data(string zymzh, string orgId, string type);
        List<Input_2205> GetCqybMxCxData(string zymzh, string orgId, string type, string ybver);

        #endregion

    }
}
