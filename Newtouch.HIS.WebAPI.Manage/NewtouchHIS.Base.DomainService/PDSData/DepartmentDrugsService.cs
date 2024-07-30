using Mapster;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.Dictionary;
using NewtouchHIS.Base.Domain.Entity.PDSData;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.IDomainService.PDSData;
using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;

namespace NewtouchHIS.Base.DomainService.PDSData
{
    public class DepartmentDrugsService : BaseDmnService<DrugsDocumentEntity>, IDepartmentDrugsService
    {
        /// <summary>
        /// 获取收费项目
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<List<DepartmentDrugsVO>> GetDepartmentDrugs(QueryParamsBase query)
        {
            
            //创建表达式
            var whereExp = Expressionable.Create<DrugsDocumentMXEntity,DrugsDocumentEntity , SysMedicineVPDSEntity>()
                .And((a, b,c) => a.crkId == b.crkId  && a.zt == "1" && b.zt == "1" && b.OrganizeId == query.orgId && a.Ypdm == c.ypCode);
            if (!string.IsNullOrWhiteSpace(query.keyword))
            {
                whereExp.And((a, b, c) => a.Ypdm.Contains(query.keyword)&& b.OrganizeId == query.orgId);
            }
            else {
                whereExp.And((a, b, c) => b.OrganizeId == query.orgId);
            }
            if (!string.IsNullOrWhiteSpace(query.code))
            {
                whereExp.And((a, b, c) => a.Ypdm == query.code);
            }
            if (!string.IsNullOrWhiteSpace(query.kscode))
            {
                whereExp.And((a, b, c) => b.Rkbm == query.kscode);
            }
            if (query.fykssj!=null&& query.fyjssj != null)
            {
                whereExp.And((a, b,c) => a.CreateTime>= query.fykssj && a.CreateTime <= query.fyjssj);
            }
            if (!string.IsNullOrWhiteSpace(query.zstbzt)&& query.zstbzt=="1")
            {
                whereExp.And((a, b,c) => a.isyfy == "1");
            }
            if (!string.IsNullOrWhiteSpace(query.zstbzt) && query.zstbzt == "2")
            {
                whereExp.And((a, b,c) => a.isyfy == null);
            }
            whereExp.And((a, b,c) => b.djlx == 6);
            var list = await GetJoinList(
               (a, b,c) => new JoinQueryInfos(JoinType.Left,  a.crkId == b.crkId, JoinType.Left, a.Ypdm == c.ypCode),
               (a, b,c) => new DepartmentDrugsVO
               {
                   crkId = b.crkId,
                   sldmxId = a.sldmxId,
                   Ypdm = a.Ypdm,
                   Ypmc = c.ypmc,
                   Fph = a.Fph,
                   Kprq = a.Kprq,
                   Dprq = a.Dprq,
                   Ph = a.Ph,
                   Yxq = a.Yxq,
                   Pfj = a.Pfj,
                   Lsj = a.Lsj,
                   Ykpfj = a.Ykpfj,
                   Yklsj = a.Yklsj,
                   Zje = a.Zje,
                   Sl = a.Sl,
                   rkdw = a.rkdw,
                   ckdw = a.ckdw,
                   Rkzhyz = a.Rkzhyz,
                   Rkbmkc = a.Rkbmkc,
                   Ckzhyz = a.Ckzhyz,
                   Ckbmkc = a.Ckbmkc,
                   Wg = a.Wg,
                   zbbz = a.zbbz,
                   jkzcz = a.jkzcz,
                   hgzm = a.hgzm,
                   ysjg = a.ysjg,
                   Thyy = a.Thyy,
                   Cljg = a.Cljg,
                   scrq = a.scrq,
                   kl = a.kl,
                   jj = a.jj,
                   cd = a.cd,
                   pc = a.pc,
                   OrganizeId = b.OrganizeId,
                   djlx = b.djlx,
                   Pdh = b.Pdh,
                   Rkbm = b.Rkbm,
                   Ckbm = b.Ckbm,
                   Rksj = b.Rksj,
                   Cksj = b.Cksj,
                   Rkczy = b.Rkczy,
                   Ckczy = b.Ckczy,
                   Crkfsdm = b.Crkfsdm,
                   Czsj = b.Czsj,
                   Sqsj = b.Sqsj,
                   Shczy = b.Shczy,
                   shzt = b.shzt,
                   crkmxId = a.crkmxId,
                   zstbzt = a.isyfy == "1"?"诊所已同步":"诊所未同步"
               }, true, whereExp.ToExpression());
            return list.Adapt<List<DepartmentDrugsVO>>();
        }
        public async Task<PageResponseRow<List<DepartmentDrugsVO>>> GetDepartmentDrugsPage(OLPagination<QueryParamsBase> query)
        {
            //PageResponseRow<List<DepartmentDrugsVO>> list = null;
            //创建表达式
            var whereExp = Expressionable.Create<DrugsDocumentMXEntity,DrugsDocumentEntity, SysMedicineVPDSEntity>()
                .And((a, b,c) => a.crkId == b.crkId && a.zt == "1" && b.zt == "1" && b.OrganizeId == query.queryParams.orgId && a.Ypdm == c.ypCode);
            if (!string.IsNullOrWhiteSpace(query.queryParams.keyword))
            {
                whereExp.And((a, b,c) => a.Ypdm.Contains(query.queryParams.keyword) && b.OrganizeId == query.queryParams.orgId);
            }
            else
            {
                whereExp.And((a, b,c) => b.OrganizeId == query.queryParams.orgId);
            }
            if (!string.IsNullOrWhiteSpace(query.queryParams.code))
            {
                whereExp.And((a, b,c) => a.Ypdm == query.queryParams.code);
            }
            if (!string.IsNullOrWhiteSpace(query.queryParams.kscode))
            {
                whereExp.And((a, b,c) => b.Rkbm == query.queryParams.kscode);
            }
            if (query.queryParams.fykssj != null && query.queryParams.fyjssj != null)
            {
                whereExp.And((a, b,c) => a.CreateTime >= query.queryParams.fykssj && a.CreateTime <= query.queryParams.fyjssj);
            }
            if (!string.IsNullOrWhiteSpace(query.queryParams.zstbzt) && query.queryParams.zstbzt == "1")
            {
                whereExp.And((a, b,c) => a.isyfy == "1");
            }
            if (!string.IsNullOrWhiteSpace(query.queryParams.zstbzt) && query.queryParams.zstbzt == "2")
            {
                whereExp.And((a, b,c) => a.isyfy == null);
            }
            whereExp.And((a, b,c) => b.djlx == 6);
            var pageData = await GetJoinPageList(query.offset, query.limit,
               (a, b,c) => new JoinQueryInfos(JoinType.Left, a.crkId == b.crkId,JoinType.Left, a.Ypdm == c.ypCode),
               (a, b,c) => new DepartmentDrugsVO
               {
                   crkId = b.crkId,
                   sldmxId = a.sldmxId,
                   Ypdm = a.Ypdm,
                   Ypmc = c.ypmc,
                   Fph = a.Fph,
                   Kprq = a.Kprq,
                   Dprq = a.Dprq,
                   Ph = a.Ph,
                   Yxq = a.Yxq,
                   Pfj = a.Pfj,
                   Lsj = a.Lsj,
                   Ykpfj = a.Ykpfj,
                   Yklsj = a.Yklsj,
                   Zje = a.Zje,
                   Sl = a.Sl,
                   rkdw = a.rkdw,
                   ckdw = a.ckdw,
                   Rkzhyz = a.Rkzhyz,
                   Rkbmkc = a.Rkbmkc,
                   Ckzhyz = a.Ckzhyz,
                   Ckbmkc = a.Ckbmkc,
                   Wg = a.Wg,
                   zbbz = a.zbbz,
                   jkzcz = a.jkzcz,
                   hgzm = a.hgzm,
                   ysjg = a.ysjg,
                   Thyy = a.Thyy,
                   Cljg = a.Cljg,
                   scrq = a.scrq,
                   kl = a.kl,
                   jj = a.jj,
                   cd = a.cd,
                   pc = a.pc,
                   OrganizeId = b.OrganizeId,
                   djlx = b.djlx,
                   Pdh = b.Pdh,
                   Rkbm = b.Rkbm,
                   Ckbm = b.Ckbm,
                   Rksj = b.Rksj,
                   Cksj = b.Cksj,
                   Rkczy = b.Rkczy,
                   Ckczy = b.Ckczy,
                   Crkfsdm = b.Crkfsdm,
                   Czsj = b.Czsj,
                   Sqsj = b.Sqsj,
                   Shczy = b.Shczy,
                   shzt = b.shzt,
                   crkmxId = a.crkmxId,
                   zstbzt = a.isyfy == "1" ? "诊所已同步" : "诊所未同步"
               }, true, whereExp.ToExpression());
            return pageData.Adapt<PageResponseRow<List<DepartmentDrugsVO>>>();
        }
        /// <summary>
        /// 修改发药状态
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<bool> UpdateMedicationStatus(QueryParamsBase query)
        {
            //创建表达式
            //var whereExp = Expressionable.Create<DrugsDocumentMXEntity>();
            //if (!string.IsNullOrWhiteSpace(query.crkmxId))
            //{
            //    whereExp.And(a => a.crkmxId== query.crkmxId);
            //}
           // var entity = await FindKeyWithAttr<DrugsDocumentMXEntity>(query.crkmxId);
            var entity = await GetFirstOrDefaultWithAttr<DrugsDocumentMXEntity>(p =>  p.zt == "1" && p.crkmxId == query.crkmxId);
            if (query.isyfy != null && query.isyfy != "")
            {
                entity.isyfy = query.isyfy;
            }
            else {
                entity.isyfy = "1";
            }
            return await UpdateWithAttr(entity);
        }
    }
}
