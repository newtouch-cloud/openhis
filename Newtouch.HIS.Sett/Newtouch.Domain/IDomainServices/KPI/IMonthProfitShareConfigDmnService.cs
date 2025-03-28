using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.KPI;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 月利润分成
    /// </summary>
    public interface IMonthProfitShareConfigDmnService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<TherapeutistProfitShareConfigVO> GetTherapeutistPSConfigList(string orgId, string keyword);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        TherapeutistProfitShareConfigVO GetTherapeutistPSConfig(string keyValue);

        /// <summary>
        /// 提交治疗师提成配置
        /// </summary>
        /// <param name="entity"></param>
        void SubmitTherapeutistPSConfig(TherapeutistMonthProfitShareConfigEntity entity, string keyValue);

        /// <summary>
        /// 治疗师利润提成 固定报表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="curUserCode"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="isReGene"></param>
        void DoGenerateTherapeutistPS(string orgId, string curUserCode, int year, int month, bool? isReGene);

        /// <summary>
        /// Check报表是否已经生成过
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        bool TherapeutistPSCheckIsGenerated(string orgId, int year, int month);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<MedicalOrgProfitShareConfigVO> GetMedicalOrgPSConfigList(string orgId, string keyword);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        MedicalOrgProfitShareConfigVO GetMedicalOrgPSConfig(string keyValue);

        /// <summary>
        /// 提交医疗机构提成配置
        /// </summary>
        /// <param name="entity"></param>
        void SubmitMedicalOrgPSConfig(MedicalOrgMonthProfitShareConfigEntity entity, string keyValue);

        /// <summary>
        /// 医疗机构利润分成 固定报表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="curUserCode"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="isReGene"></param>
        void DoGenerateMedicalOrgPS(string orgId, string curUserCode, int year, int month, bool? isReGene);

        /// <summary>
        /// Check报表是否已经生成过
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        bool MedicalOrgPSCheckIsGenerated(string orgId, int year, int month);

    }
}
