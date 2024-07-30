using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Base.Domain.IDomainService
{
    /// <summary>
    /// 系统药品
    /// </summary>
    public interface ISysMedicineService : ISingletonDependency
    {
        /// <summary>
        /// 系统药品字典
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<SysMedicineVVO>> GetMedicineList(QueryParamsBase query);
        /// <summary>
        /// 系统药品字典(分页)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<PageResponseRow<List<SysMedicineVVO>>> GetMedicinePage(OLPagination<QueryParamsBase> query);
        /// <summary>
        /// 药品用法
        /// </summary>
        /// <param name="code"></param>
        /// <param name="yplx"></param>
        /// <returns></returns>
        Task<List<DrugUsageEntity>> DrugUsageDic(string? code = null, int? yplx = null);
        /// <summary>
        /// 频次字典
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysFrequencyEntity>> SysFrequencyDic(string? code = null, string? orgId = null);
        /// <summary>
        /// 药品单位
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<List<DrugUnitEntity>> DrugUnitDic(string? code = null, string? name = null);
        /// <summary>
        /// 药品剂型用法
        /// </summary>
        /// <returns></returns>
        Task<List<DrugFormulationUsageVO>> GetDrugFormulationUsageList();
    }
}
