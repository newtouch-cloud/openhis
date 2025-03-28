using Mapster;
using Microsoft.Data.SqlClient;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.Dictionary;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;
using System.Data.Common;

namespace NewtouchHIS.Base.DomainService.BaseData
{
    public class ChargeItemService : BaseDmnService<SysChargeItemEntity>, IChargeItemService
    {
        public async Task<List<SysChargeCategoryVO>> GetChargeCategory(QueryParamsBase query)
        {
            List<SysChargeCategoryEntity> list = null;
            if (!string.IsNullOrWhiteSpace(query.keyword))
            {
                list = await GetByWhere<SysChargeCategoryEntity>(p => p.OrganizeId == query.orgId && p.zt == "1" && (p.dlCode == query.keyword || p.dlmc.Contains(query.keyword)));
            }
            else
            {
                list = await GetByWhere<SysChargeCategoryEntity>(p => p.OrganizeId == query.orgId && p.zt == "1");
            }
            if (!string.IsNullOrWhiteSpace(query.code))
            {
                list = list.Where(p => p.dlCode == query.code).ToList();
            }
            return list.Adapt<List<SysChargeCategoryVO>>();
        }
        /// <summary>
        /// 获取收费项目
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<List<SysChargeItemVO>> GetChargeItemList(QueryParamsBase query)
        {
            var homeData = await GetListBySqlQuery<SysChargeItemVO>(DBEnum.BaseDb.ToString(),
                @"select sfxm.*,lxmc.Name dllx from [NewtouchHIS_Base]..V_S_xt_sfxm sfxm
left join [NewtouchHIS_Base]..V_S_xt_sfdl_lx dllx on sfxm.sfdlCode=dllx.dlCode  and dllx.zt='1'
left join (select * from [NewtouchHIS_Base].[dbo].V_C_Sys_ItemsDetail where catecode='ChargeCateType'
) lxmc on lxmc.Code=dllx.Type
where sfxm.sfdlCode!='18'
and lxmc.code!='TCM'
and lxmc.code!='WM'
and lxmc.code!='cl'
and sfxm.zt='1'
and sfxm.OrganizeId=@orgId
and sfxm.sfxmmc like '%'+@keyword+'%' ", new List<DbParameter> {
                new SqlParameter("@orgId",query.orgId),
                new SqlParameter("@keyword",query.keyword),
            });
            return homeData;
            //List<SysChargeItemEntity> list = null;
            //if (!string.IsNullOrWhiteSpace(query.keyword))
            //{
            //    list = await GetByWhere(p => p.OrganizeId == query.orgId && p.zt == "1" && (p.sfxmCode == query.keyword || p.sfxmmc.Contains(query.keyword)));
            //}
            //else
            //{
            //    list = await GetByWhere(p => p.OrganizeId == query.orgId && p.zt == "1");
            //}
            //if (!string.IsNullOrWhiteSpace(query.code))
            //{
            //    list = list.Where(p =>p.sfdlCode == query.code || p.sfxmCode == query.code).ToList();
            //}
            //list = list.Where(p => p.sfdlCode !="18").ToList();
            //return list.Adapt<List<SysChargeItemVO>>();
        }
        public async Task<PageResponseRow<List<SysChargeItemVO>>> GetChargeItemPage(OLPagination<QueryParamsBase> query)
        {
            PageResponseRow<List<SysChargeItemEntity>> list = null;
            var whereExp = Expressionable.Create<SysChargeItemEntity, SysCategoryTypesEntity, SysItemsDetailEntity>()
                .And((a, b,c) => a.sfdlCode == b.dlCode && a.zt == "1" && b.zt == "1" && b.Type==c.Code &&c.CateCode== "ChargeCateType");
            if (!string.IsNullOrWhiteSpace(query.queryParams?.keyword))
            {
                whereExp.And((a, b,c) => a.OrganizeId == query.queryParams.orgId && a.zt == "1" && (a.sfxmCode == query.queryParams.keyword || a.sfxmmc.Contains(query.queryParams.keyword)));
                //whereExp.And(p => p.OrganizeId == query.queryParams.orgId && p.zt == "1" && (p.sfxmCode == query.queryParams.keyword || p.sfxmmc.Contains(query.queryParams.keyword)));
            }
            else
            {
                whereExp.And((a, b, c) => a.OrganizeId == query.queryParams.orgId && a.zt == "1");

                //whereExp.And(p => p.OrganizeId == query.queryParams.orgId && p.zt == "1");
            }
            if (!string.IsNullOrWhiteSpace(query.queryParams.code))
            {
                whereExp.And((a, b, c) => a.sfdlCode == query.queryParams.code || a.sfxmCode == query.queryParams.code);
                //whereExp.And(p => p.sfdlCode == query.queryParams.code || p.sfxmCode == query.queryParams.code);
            }
            whereExp.And((a, b, c) => a.sfdlCode !="18"&&c.Code != "TCM" && c.Code != "WM" && c.Code != "cl");

            //whereExp.And(p => p.sfdlCode != "18");
            var pageData = await GetJoinPageList(query.offset, query.limit,
               (a, b, c) => new JoinQueryInfos(JoinType.Left, a.OrganizeId == b.OrganizeId && a.sfdlCode == b.dlCode && b.zt == "1",
                                               JoinType.Left, b.Type == c.Code && c.CateCode == "ChargeCateType"),
               (a, b, c) => new SysChargeItemVO
               {
                   OrganizeId = a.OrganizeId,
                   sfxmId = a.sfxmId,
                   sfxmCode = a.sfxmCode,
                   sfxmmc = a.sfxmmc,
                   sfdlCode = a.sfdlCode,
                   badlCode = a.badlCode,
                   nbdlCode = a.nbdlCode,
                   duration = a.duration,
                   py = a.py,
                   dw = a.dw,
                   dj = a.dj,
                   flCode = a.flCode,
                   zfbl = a.zfbl,
                   zfxz = a.zfxz,
                   mzzybz = a.mzzybz,
                   ssbz = a.ssbz,
                   tsbz = a.tsbz,
                   sfbz = a.sfbz,
                   ybdm = a.ybdm,
                   wjdm = a.wjdm,
                   bz = a.bz,
                   px = a.px,
                   dwjls = a.dwjls,
                   jjcl = a.jjcl,
                   zxks = a.zxks,
                   gg = a.gg,
                   LastYBUploadTime = a.LastYBUploadTime,
                   ssdj = a.ssdj,
                   sqlx = a.sqlx,
                   ybbz = a.ybbz,
                   xnhybdm = a.xnhybdm,
                   gjybdm = a.gjybdm,
                   cxjje = a.cxjje,
                   pzwh = a.pzwh,
                   sccj = a.sccj,
                   gjybmc = a.gjybmc,
                   wz_Code = a.wz_Code,
                   wz_ConsumablesID = a.wz_ConsumablesID,
                   wz_SerialNumber = a.wz_SerialNumber,
                   wz_Packing = a.wz_Packing,
                   wz_IsHight = a.wz_IsHight,
                   wz_MaterialCode = a.wz_MaterialCode,
                   //isYnss = a.isYnss,
                   dllx = c.Name
               }, true, whereExp.ToExpression(), true, (a, b, c) => new { a.CreateTime }, OrderByType.Desc);
            //list = await GetJoinPageList<SysChargeItemEntity, SysCategoryTypesEntity, SysItemsDetailEntity>(query.offset, query.limit, true, whereExp.ToExpression());
            return pageData.Adapt<PageResponseRow<List<SysChargeItemVO>>>();
        }
        /// <summary>
        /// 获取材料项目
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<List<SysChargeItemVO>> GetMaterialItemList(QueryParamsBase query)
        {
            var homeData = await GetListBySqlQuery<SysChargeItemVO>(DBEnum.BaseDb.ToString(),
                @"select sfxm.*,lxmc.Name dllx from [NewtouchHIS_Base]..V_S_xt_sfxm sfxm
left join [NewtouchHIS_Base]..V_S_xt_sfdl_lx dllx on sfxm.sfdlCode=dllx.dlCode and dllx.zt='1'
left join (select * from [NewtouchHIS_Base].[dbo].V_C_Sys_ItemsDetail where catecode='ChargeCateType'
) lxmc on lxmc.Code=dllx.Type
where sfxm.sfdlCode='18'
and sfxm.zt='1'
and sfxm.OrganizeId=@orgId
and sfxm.sfxmmc like '%'+@keyword+'%'", new List<DbParameter> {
                new SqlParameter("@orgId",query.orgId),
                new SqlParameter("@keyword",query.keyword),
            });
            return homeData;
            //List<SysChargeItemEntity> list = null;
            //if (!string.IsNullOrWhiteSpace(query.keyword))
            //{
            //    list = await GetByWhere(p => p.OrganizeId == query.orgId && p.zt == "1" && (p.sfxmCode == query.keyword || p.sfxmmc.Contains(query.keyword)));
            //}
            //else
            //{
            //    list = await GetByWhere(p => p.OrganizeId == query.orgId && p.zt == "1");
            //}
            //if (!string.IsNullOrWhiteSpace(query.code))
            //{
            //    list = list.Where(p => p.sfdlCode == query.code || p.sfxmCode == query.code).ToList();
            //}
            //list = list.Where(p => p.sfdlCode == "18").ToList();
            //return list.Adapt<List<SysChargeItemVO>>();
        }
        public async Task<PageResponseRow<List<SysChargeItemVO>>> GetMaterialItemPage(OLPagination<QueryParamsBase> query)
        {
            PageResponseRow<List<SysChargeItemEntity>> list = null;
            var whereExp = Expressionable.Create<SysChargeItemEntity, SysCategoryTypesEntity, SysItemsDetailEntity>()
                .And((a, b, c) => a.sfdlCode == b.dlCode && a.zt == "1" && b.zt == "1" && b.Type == c.Code && c.CateCode == "ChargeCateType");
            if (!string.IsNullOrWhiteSpace(query.queryParams?.keyword))
            {
                whereExp.And((a, b, c) => a.OrganizeId == query.queryParams.orgId && a.zt == "1" && (a.sfxmCode == query.queryParams.keyword || a.sfxmmc.Contains(query.queryParams.keyword)));
                //whereExp.And(p => p.OrganizeId == query.queryParams.orgId && p.zt == "1" && (p.sfxmCode == query.queryParams.keyword || p.sfxmmc.Contains(query.queryParams.keyword)));
            }
            else
            {
                whereExp.And((a, b, c) => a.OrganizeId == query.queryParams.orgId && a.zt == "1");

                //whereExp.And(p => p.OrganizeId == query.queryParams.orgId && p.zt == "1");
            }
            if (!string.IsNullOrWhiteSpace(query.queryParams.code))
            {
                whereExp.And((a, b, c) => a.sfdlCode == query.queryParams.code || a.sfxmCode == query.queryParams.code);
                //whereExp.And(p => p.sfdlCode == query.queryParams.code || p.sfxmCode == query.queryParams.code);
            }
            whereExp.And((a, b, c) => a.sfdlCode == "18");

            //whereExp.And(p => p.sfdlCode != "18");
            var pageData = await GetJoinPageList(query.offset, query.limit,
               (a, b, c) => new JoinQueryInfos(JoinType.Left, a.OrganizeId == b.OrganizeId && a.sfdlCode == b.dlCode && b.zt == "1",
                                               JoinType.Left, b.Type == c.Code && c.CateCode == "ChargeCateType"),
               (a, b, c) => new SysChargeItemVO
               {
                   OrganizeId = a.OrganizeId,
                   sfxmId = a.sfxmId,
                   sfxmCode = a.sfxmCode,
                   sfxmmc = a.sfxmmc,
                   sfdlCode = a.sfdlCode,
                   badlCode = a.badlCode,
                   nbdlCode = a.nbdlCode,
                   duration = a.duration,
                   py = a.py,
                   dw = a.dw,
                   dj = a.dj,
                   flCode = a.flCode,
                   zfbl = a.zfbl,
                   zfxz = a.zfxz,
                   mzzybz = a.mzzybz,
                   ssbz = a.ssbz,
                   tsbz = a.tsbz,
                   sfbz = a.sfbz,
                   ybdm = a.ybdm,
                   wjdm = a.wjdm,
                   bz = a.bz,
                   px = a.px,
                   dwjls = a.dwjls,
                   jjcl = a.jjcl,
                   zxks = a.zxks,
                   gg = a.gg,
                   LastYBUploadTime = a.LastYBUploadTime,
                   ssdj = a.ssdj,
                   sqlx = a.sqlx,
                   ybbz = a.ybbz,
                   xnhybdm = a.xnhybdm,
                   gjybdm = a.gjybdm,
                   cxjje = a.cxjje,
                   pzwh = a.pzwh,
                   sccj = a.sccj,
                   gjybmc = a.gjybmc,
                   wz_Code = a.wz_Code,
                   wz_ConsumablesID = a.wz_ConsumablesID,
                   wz_SerialNumber = a.wz_SerialNumber,
                   wz_Packing = a.wz_Packing,
                   wz_IsHight = a.wz_IsHight,
                   wz_MaterialCode = a.wz_MaterialCode,
                   //isYnss = a.isYnss,
                   dllx = c.Name
               }, true, whereExp.ToExpression(), true, (a, b, c) => new { a.CreateTime }, OrderByType.Desc);
            //list = await GetJoinPageList<SysChargeItemEntity, SysCategoryTypesEntity, SysItemsDetailEntity>(query.offset, query.limit, true, whereExp.ToExpression());
            return pageData.Adapt<PageResponseRow<List<SysChargeItemVO>>>();
            //PageResponseRow<List<SysChargeItemEntity>> list = null;
            //var whereExp = Expressionable.Create<SysChargeItemEntity>();
            //if (!string.IsNullOrWhiteSpace(query.queryParams?.keyword))
            //{
            //    whereExp.And(p => p.OrganizeId == query.queryParams.orgId && p.zt == "1" && (p.sfxmCode == query.queryParams.keyword || p.sfxmmc.Contains(query.queryParams.keyword)));
            //}
            //else
            //{
            //    whereExp.And(p => p.OrganizeId == query.queryParams.orgId && p.zt == "1");
            //}
            //if (!string.IsNullOrWhiteSpace(query.queryParams.code))
            //{
            //    whereExp.And(p => p.sfdlCode == query.queryParams.code || p.sfxmCode == query.queryParams.code);
            //}
            //whereExp.And(p => p.sfdlCode == "18");
            //list = await GetPageList<SysChargeItemEntity>(query.offset, query.limit, true, whereExp.ToExpression());
            //return list.Adapt<PageResponseRow<List<SysChargeItemVO>>>();
        }
    }
}
