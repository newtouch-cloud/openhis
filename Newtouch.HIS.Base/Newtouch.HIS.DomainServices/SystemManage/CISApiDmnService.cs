using Newtouch.Core.Common;
using Newtouch.HIS.Base.HOSP.Request;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 字典、字典项 DmnService
    /// </summary>
    public class CISApiDmnService : DmnServiceBase, ICISApiDmnService
    {
        private readonly ISysItemsDetailRepository _sysItemsDetailRepository;

        public CISApiDmnService(IBaseDatabaseFactory databaseFactory
            , ISysItemsDetailRepository sysItemsDetailRepository) : base(databaseFactory)
        {
            this._sysItemsDetailRepository = sysItemsDetailRepository;
        }


        public IList<MedicineInfoVO2> GETYNMidecine(MedicineQueryRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.pagination.sidx))
            {
                throw new Exception("pagination.sidx is required");
            }
            Pagination pagination =request.pagination;
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@orgId", request.orgId),
                new SqlParameter("@xmbm", request.xmbm.Trim()),
                new SqlParameter("@xmmc", "%" + request.xmmc.Trim() + "%"),
                new SqlParameter("@lkc", request.ck_kc == "0" ? 0 : 1),
            };
            var sql = @"SELECT convert(varchar(50),yp.ypId)  ypId,
                    yp.ypCode ,
                    yp.ypmc ,
                    yp.ycmc ,
                    sx.ypgg ,
                    SUM(ISNULL(kc.kcsl, 0)) kcsl ,
                    bm.Zcxh ,
                    CASE bm.zt WHEN 1 THEN '正常' WHEN 0 THEN '控制' END syzt ,
            		NewtouchHIS_PDS.dbo.f_getYfYpComplexYpSlandDw(SUM(ISNULL(kc.kcsl, 0)), fm.mzzybz, bm.Ypdm, bm.OrganizeId) klsl ,
                    SUM(CONVERT(NUMERIC(11, 2),ISNULL(kc.kcsl, 0)/ yp.bzs)) YkKcsl ,
                    yp.bzdw YkDw ,
                   NewtouchHIS_PDS.dbo.f_getyfbmDw(bm.yfbmCode, bm.Ypdm, bm.OrganizeId) deptdw ,
                   CONVERT(DECIMAL(11, 2),yp.lsj/yp.bzs*(CASE fm.mzzybz WHEN '0' THEN yp.bzs WHEN '1' THEN yp.mzcls WHEN '2' THEN yp.zycls WHEN '3' THEN yp.mzcls END)) lsj ,
                    CONVERT(DECIMAL(11, 4),yp.pfj/yp.bzs*(CASE fm.mzzybz WHEN '0' THEN yp.bzs WHEN '1' THEN yp.mzcls WHEN '2' THEN yp.zycls WHEN '3' THEN yp.mzcls END)) pfj ,
                    jx.jxmc ,
                    bm.Ypkw ,
                    bm.Pxfs1 ,
                    bm.Pxfs2 ,
                    bm.Kcsx ,
                    bm.Kcxx ,
                    bm.Jhd ,
                    bm.Jhl ,
                    RTRIM(LTRIM(dl.dlmc)) AS yplb ,
                    CASE yp.zt WHEN '1' THEN '正常' WHEN '0' THEN '停用' END ypzt ,
                    bm.Sysx,fm.yfbmCode,fm.yfbmmc
            FROM    NewtouchHIS_PDS.dbo.XT_YP_BMYPXX(nolock) bm
                    INNER JOIN [NewtouchHIS_Base].dbo.V_S_xt_yp yp ON yp.ypCode = bm.Ypdm and yp.OrganizeId=bm.OrganizeId and yp.mzzybz='1' and yp.isYnss='1' 
                    INNER JOIN [NewtouchHIS_Base].dbo.V_S_xt_ypsx sx ON sx.ypId = yp.ypId and sx.OrganizeId=bm.OrganizeId
                    INNER JOIN [NewtouchHIS_Base]..V_S_xt_yfbm fm ON fm.yfbmCode = bm.yfbmCode and fm.OrganizeId=bm.OrganizeId and fm.zt='1'
                    LEFT  JOIN NewtouchHIS_PDS.dbo.XT_YP_KCXX(nolock) kc ON kc.yfbmCode = bm.yfbmCode AND kc.Ypdm = bm.Ypdm AND kc.OrganizeId=bm.OrganizeId
                    LEFT JOIN [NewtouchHIS_Base]..V_S_xt_sfdl dl ON dl.dlcode = yp.dlcode and dl.OrganizeId=bm.OrganizeId and dl.zt='1'
                    LEFT  JOIN [NewtouchHIS_Base].dbo.V_S_xt_ypjx jx ON jx.jxCode = yp.jx 
		             WHERE bm.OrganizeId=@orgId and yp.dlCode in ('01','02','03')  and (@xmbm= '' or yp.ypCode=@xmbm) 
		             and (@xmmc='%%' or yp.ypmc like @xmmc)
                     GROUP BY yp.ypId ,sx.ypgg ,bm.Zcxh ,bm.zt,bm.yfbmCode,bm.OrganizeId,bm.Ypdm,bm.Ypkw ,bm.Pxfs1 ,
		                      bm.Pxfs2 ,bm.Kcsx ,bm.Kcxx ,bm.Jhd ,bm.Jhl ,bm.Sysx,fm.mzzybz,yp.bzs,yp.mzcls,yp.zycls,
                     yp.bzdw,yp.lsj,yp.pfj,yp.ypCode ,yp.ypmc ,yp.ycmc,yp.zt,jx.jxmc,dl.dlmc,fm.yfbmmc,fm.yfbmCode,fm.yfbmCode,fm.yfbmmc
                      having SUM(ISNULL(@lkc, 0))>0";
             if (string.IsNullOrEmpty(sql))
            {
                return new List<MedicineInfoVO2>();
            }
            
            
             return this.QueryWithPage<MedicineInfoVO2>(sql, pagination, parameters.ToArray());
        }

        public IList<MedicineInfoVO2> GETYNzlxm(MedicineQueryRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.pagination.sidx))
            {
                throw new Exception("pagination.sidx is required");
            }
            Pagination pagination = request.pagination;
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@orgId", request.orgId),
                new SqlParameter("@xmbm", request.xmbm.Trim()),
                new SqlParameter("@xmmc", "%" + request.xmmc.Trim() + "%")
            };
            var sql = @"SELECT convert(varchar(50),a.sfxmId) ypId,  
 sfxmCode ypCode, sfxmmc ypmc,'' ycmc,
 gg ypgg,
 0 kcsl,
 '' Zcxh,
 '' syzt ,
'' klsl,
0.0 YkKcsl,
 a.dw YkDw,
 '' deptdw,
 a.dj lsj,
 a.dj pfj,
 '' jxmc,
 '' Ypkw,
 '' Pxfs1,
 '' Pxfs2,
 0 Kcsx,
 0 Kcxx,
 0 Jhd,
0 Jhl,
 RTRIM(LTRIM(dl.dlmc)) AS yplb ,
 CASE a.zt WHEN '1' THEN '正常' WHEN '0' THEN '停用' END ypzt ,
 0 Sysx,
 '' yfbmCode,
'' yfbmmc
 FROM [NewtouchHIS_Base]..V_S_xt_sfxm(NOLOCK) a  
 INNER JOIN [NewtouchHIS_Base]..V_S_xt_sfdl dl ON dl.dlcode = a.sfdlCode and dl.OrganizeId=a.OrganizeId and dl.zt='1'
 and a.sfdlCode not in ('01','02','03') and a.isYnss='1' and a.OrganizeId=@orgId  and (@xmbm='' or sfxmCode=@xmbm) and (@xmmc='%%' or sfxmmc like @xmmc)";
            if (string.IsNullOrEmpty(sql))
            {
                return new List<MedicineInfoVO2>();
            }


            return this.QueryWithPage<MedicineInfoVO2>(sql, pagination, parameters.ToArray());
        }
    }
}
