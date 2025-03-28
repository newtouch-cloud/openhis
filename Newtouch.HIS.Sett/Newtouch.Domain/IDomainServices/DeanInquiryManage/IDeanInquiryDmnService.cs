using Newtouch.HIS.Domain.Entity.DeanInquiryManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices.DeanInquiryManage
{
    public interface IDeanInquiryDmnService
    {
        /// <summary>
        /// 当日动态_全院收入概况 
        /// </summary>
        List<DailyUpdatesEntiy> DailyUpdates_GetQysr();
        /// <summary>
        /// 院长查询-今日动态-banner 
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        JRDT_Banner GetJRDTBanner(string orgId);
        /// <summary>
        /// 当日动态_今日动态门诊
        /// </summary>
        List<DailyUpdates_GetJrdtmz> DailyUpdates_GetJrdt(string orgid);
        /// <summary>
        /// 当日动态_门诊处方
        /// </summary>
        List<DailyUpdates_GetMzcf> DailyUpdates_GetMzcf(string orgId);
        /// <summary>
        /// 当日动态_门诊费用
        /// </summary>
        List<DailyUpdates_GetMzfy> DailyUpdates_GetMzfy(string orgId);
        /// <summary>
        /// 当日动态_住院占床率
        /// </summary>
        List<DailyUpdates_GetZyzcl> DailyUpdates_GetZyzcl(string orgId);
        /// <summary>
        /// 当日动态_门诊挂号统计
        /// </summary>
        List<DailyUpdates_GetMzghtj> DailyUpdates_GetMzghtj(string orgId);
        /// <summary>
        /// 当日动态_住院患者统计
        /// </summary>
        List<DailyUpdates_GetZyhztj> DailyUpdates_GetZyhztj(string orgId);
        /// <summary>
        /// 业务排名_门诊医生工作量
        /// </summary>
        List<BusinessRankingEntiy> BusinessRankingEntiy_mzysgzl(string kssj, string jssj,string OrganizeId);
        /// <summary>
        /// 业务排名_门诊科室收入排名
        /// </summary>
        List<BusinessRankingEntiy_mzkssr> BusinessRankingEntiy_mzkssr(string kssj, string jssj, string OrganizeId);
        /// <summary>
        /// 业务排名_住院业务科室排名
        /// </summary>
        List<BusinessRankingEntiy_zyywks>  BusinessRankingEntiy_zyywks(string kssj, string jssj, string orgId);
        /// <summary>
        /// 业务排名_医生业务排名
        /// </summary>
        List<BusinessRankingEntiy_ysyw> BusinessRankingEntiy_ysyw(string kssj, string jssj, string orgId, string bqcode);
        /// <summary>
        /// 门诊综合分析_抬头数据
        /// </summary>
        OutpatientComprehensiveAnalysisEntiy OutpatientComprehensiveAnalysisEntiy_TitleData(string orgId, string ksrq, string jsrq, string rtype);
        /// <summary>
        /// 门诊综合分析_门诊患者分析
        /// </summary>
        OutpatientComprehensiveAnalysisEntiy_Mzhzfx OutpatientComprehensiveAnalysisEntiy_Mzhzfx(string orgId, string ksrq, string jsrq);
        /// <summary>
        /// 门诊工作量_门诊科室工作量
        /// </summary>
        List<OutpatientWorkloadEntiy> OutpatientWorkloadEntiy_Mzksgzl(string ksrq, string jsrq, string OrganizeId);
        /// <summary>
        /// 门诊工作量_门诊科室工作量
        /// </summary>
        List<OutpatientWorkloadEntiy_ysgzl> OutpatientWorkloadEntiy_Ysgzl(string ksrq, string jsrq, string OrganizeId, string ks);
        /// <summary>
        /// 门诊费用分析_门诊费用分类分析
        /// </summary>
        List<OutpatientCostEntiy> OutpatientCostEntiy_Mzfyflfx(string orgId, string ksrq, string jsrq);
        /// <summary>
        /// 门诊费用分类分析_门诊费用分类分析_图表（患者人均费用分析图表也用这个字段都是一样的）
        /// </summary>
        List<OutpatientCostEntiy_Mzfyflfx_tb> OutpatientCostEntiy_Mzfyflfx_tb(string orgId, string ksrq, string jsrq, string rtype);
        /// <summary>
        /// 门诊费用分析_患者人均费用分析
        /// </summary>
        List<OutpatientCostEntiy_Hzrjfyfx> OutpatientCostEntiy_Hzrjfyfx(string orgId, string ksrq, string jsrq);
        /// <summary>
        /// 门诊费用分析_患者人均费用分析_图标
        /// </summary>
        List<OutpatientCostEntiy_Mzfyflfx_tb> OutpatientCostEntiy_Hzrjfyfx_tb(string orgId, string ksrq, string jsrq, string rtype);
        /// <summary>
        /// 门诊效益_门诊效益明细
        /// </summary>
        List<OutpatientBenefitsEntiy> OutpatientCostEntiy_Mzxymx(string orgId, string ksrq, string jsrq);
        /// <summary>
        /// 门诊效益_医生效益
        /// </summary>
        List<OutpatientBenefitsEntiy_Ysxy> OutpatientCostEntiy_Ysxy(string orgId, string ksrq, string jsrq, string code);
        /// <summary>
        /// 
        /// </summary>
        List<KSXYPMBySJSRDTO> OutpatientCostEntiy_Lsqs(string orgId, string ksrq, string jsrq,string rType,string type);
        List<KSXYPMDTO> GetKSXYPM(string orgId, string ksrq, string jsrq);
        List<KSXYPMBySJSRDTO> GetKSXYPMBySJSY(string orgId, string ksrq, string jsrq);
        /// <summary>
        /// 药品top 50 排行
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="yptype"></param>
        /// <returns></returns>
        List<DrugTrackingEntity> DrugTrackingEntity_Lsqs(string kssj, string jssj, string OrganizeId, string yptype);

        /// <summary>
        /// 损益排行
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        List<Profitandlossranking> Profitandlossranking_Lsqs(string kssj, string jssj, string OrganizeId);

        /// <summary>
        /// 医生开单排行
        /// </summary>
        /// <param name="ypcode"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        List<DoctorBillingRanking> DoctorBillingRanking_Lsqs(string kssj, string jssj, string ypcode, string OrganizeId);

        /// <summary>
        /// 耗材销量排名TOP50
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        List<ConsumablestatisticsEntity> Consumablestatistics_Lsqs(string kssj, string jssj, string OrganizeId);

        /// <summary>
        /// 耗材医生排名
        /// </summary>
        /// <param name="sfxmdm"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        List<DoctorBillingRanking> DoctorBillingRankingHC_Lsqs(string kssj, string jssj, string sfxmdm, string OrganizeId);

        /// <summary>
        /// 耗材统计
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        List<Profitandlossranking> ProfitandlossrankingHC_Lsqs(string kssj, string jssj, string OrganizeId);

        List<NumberTrendEntity> ryqs_Lsqs(string time, string OrganizeId);
        List<NumberTrendEntity> cyqs_Lsqs(string time, string OrganizeId);
        List<NumberTrendEntity> zzqs_Lsqs(string time, string OrganizeId);

        List<NumberPersonEntity> Rstj_Lsqs(string kssj,string jssj, string OrganizeId);

        List<DeparNumberEntity> Ksgzl_Lsqs(string kssj, string jssj, string OrganizeId);

        List<StaffNumberEntity> YSgzl_Lsqs(string kssj, string jssj, string bq, string OrganizeId);
        #region 住院费用分析
        /// <summary>
        /// 住院费用分析_抬头数据
        /// </summary>
        ZYFYFXDTO ZYFYFX_TitleData(string orgId, string ksrq, string jsrq, string rtype);
        List<ZYFYFX_KSFYFXDTO> ZYFYFX_KSFYFXDTO(string orgId, string ksrq, string jsrq, string rtype);
        List<ZYFYFX_CYHZFYFXDTO> GetCYHZFYFData(string orgId, string ksrq, string jsrq, string rtype);
        List<CYHZFYFTJTDTO> GetCYHZFYFTJData(string orgId, string ksrq, string jsrq, string rtype);
        #endregion
    }
}
