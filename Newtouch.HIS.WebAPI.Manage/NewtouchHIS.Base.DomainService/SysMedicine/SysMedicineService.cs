using Mapster;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.Dictionary;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;

namespace NewtouchHIS.Base.DomainService
{
    public class SysMedicineService : BaseDmnService<DrugUsageEntity>, ISysMedicineService
    {
        /// <summary>
        /// 系统药品字典
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<List<SysMedicineVVO>> GetMedicineList(QueryParamsBase query)
        {
            List<SysMedicineVEntity> list = null;
            var whereExp = Expressionable.Create<SysMedicineVEntity>();
            if (!string.IsNullOrWhiteSpace(query.keyword))
            {
                whereExp.And(p => p.OrganizeId == query.orgId && p.zt == "1" && (p.ypCode == query.keyword || p.ypmc.Contains(query.keyword)));
            }
            else
            {
                whereExp.And(p => p.OrganizeId == query.orgId && p.zt == "1");
            }
            if (!string.IsNullOrWhiteSpace(query.code))
            {
                whereExp.And(p => p.dlCode == query.code);
            }
            if (!string.IsNullOrWhiteSpace(query.ypdm))
            {
                whereExp.And(p => p.ypCode == query.ypdm);
            }
            list = await GetByWhereWithAttr<SysMedicineVEntity>(whereExp.ToExpression());
            return list.Adapt<List<SysMedicineVVO>>();
        }
        /// <summary>
        /// 系统药品字典(分页)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PageResponseRow<List<SysMedicineVVO>>> GetMedicinePage(OLPagination<QueryParamsBase> query)
        {
            PageResponseRow<List<SysMedicineVEntity>> list = null;
            var whereExp = Expressionable.Create<SysMedicineVEntity>();
            if (!string.IsNullOrWhiteSpace(query.queryParams?.keyword))
            {
                whereExp.And(p => p.OrganizeId == query.queryParams.orgId && p.zt == "1" && (p.ypCode == query.queryParams.keyword || p.ypmc.Contains(query.queryParams.keyword)));
            }
            else
            {
                whereExp.And(p => p.OrganizeId == query.queryParams.orgId && p.zt == "1");
            }
            if (!string.IsNullOrWhiteSpace(query.queryParams.code))
            {
                whereExp.And(p => p.dlCode == query.queryParams.code);
            }
            if (!string.IsNullOrWhiteSpace(query.queryParams.ypdm))
            {
                whereExp.And(p => p.ypCode == query.queryParams.ypdm);
            }
            list = await GetPageList<SysMedicineVEntity>(query.offset, query.limit, true, whereExp.ToExpression());
            return list.Adapt<PageResponseRow<List<SysMedicineVVO>>>();
        }
        /// <summary>
        /// 药品用法
        /// </summary>
        /// <param name="code"></param>
        /// <param name="yplx"></param>
        /// <returns></returns>
        public async Task<List<DrugUsageEntity>> DrugUsageDic(string? code = null, int? yplx = null)
        {
            var data = await GetByWhereWithAttr<DrugUsageEntity>(p => p.zt == "1");
            if (data != null && !string.IsNullOrWhiteSpace(code))
            {
                data = data.Where(p => p.yfCode == code).WhereIF(yplx > 0, p => p.yplx == yplx.ToString()).ToList();
            }
            return data;
        }
        /// <summary>
        /// 频次
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<List<SysFrequencyEntity>> SysFrequencyDic(string? code = null, string? orgId = null)
        {
            var data = await GetByWhereWithAttr<SysFrequencyEntity>(p => p.zt == "1" && p.OrganizeId == orgId);
            if (data != null && !string.IsNullOrWhiteSpace(code))
            {
                data = data.Where(p => p.yzpcCode == code).ToList();
            }
            return data;
        }
        /// <summary>
        /// 药品单位
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<List<DrugUnitEntity>> DrugUnitDic(string? code = null, string? name = null)
        {
            var data = await GetByWhereWithAttr<DrugUnitEntity>(p => p.zt == "1");
            if (data != null && (!string.IsNullOrWhiteSpace(code) || !string.IsNullOrWhiteSpace(name)))
            {
                data = data.Where(p => p.ypdwCode == code || p.ypdwmc == name).ToList();
            }
            return data;
        }
        /// <summary>
        /// 药品剂型用法
        /// </summary>
        /// <returns></returns>
        public async Task<List<DrugFormulationUsageVO>> GetDrugFormulationUsageList()
        {
            var data = await GetByWhereWithAttr<DrugFormulationUsageEntity>(p => p.zt == "1");
            return data.Adapt<List<DrugFormulationUsageVO>>();
        }
    }
}
