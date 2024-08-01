using System.Collections.Generic;
using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace FrameworkBase.MultiOrg.Domain.IRepository
{
    /// <summary>
    /// 系统诊断
    /// </summary>
    public interface ISysDiagnosisRepo : IRepositoryBase<SysDiagnosisVEntity>
    {
        /// <summary>
        /// 根据关键字模糊查找（有效的）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword">null或Empty返回所有</param>
        /// <param name="zdlx">诊断类型（字典DiagnosisType）</param>
        /// <param name="ybnhlx">诊断类型（字典DiagnosisType）</param>
        /// <returns></returns>
        IList<SysDiagnosisVEntity> GetList(string orgId, string keyword, string zdlx = null, string ybnhlx = null);

        /// <summary>
        /// 根据编码查找实体（可能无效）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        SysDiagnosisVEntity GetEntityByCode(string orgId, string code);

        /// <summary>
        /// 根据编码查找实体
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        SysDiagnosisVEntity SelectData(string orgId, string code);

        /// <summary>
        /// 根据ICD10编码查找实体
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="icd10"></param>
        /// <returns></returns>
         SysDiagnosisVEntity SelectDataByICD10(string orgId, string icd10);
    }
}
