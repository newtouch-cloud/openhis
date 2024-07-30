using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.IDomainServices.NonTreatmentItemManage;
using Newtouch.HIS.Domain.ValueObjects.NonTreatmentItemManage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.DomainServices.NonTreatmentItemManage
{
    public class NonTreatmentItemDmnService : DmnServiceBase, INonTreatmentItemDmnService
    {

        public NonTreatmentItemDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        #region 记账
        /// <summary>
        /// 根据病历号查patid和xm
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<PatientInfoVO> SelectPatientInfoByblhOrzyh(string orgId, string blh, string zyh,string xm)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                return null;
            }
            StringBuilder sqlStr = new StringBuilder();
            IList<SqlParameter> inSqlParameterList = new List<SqlParameter>();
            sqlStr.Append(@"
SELECT brjbxx.blh,
        brjbxx.patid,
        brjbxx.xm
");
            if (!string.IsNullOrEmpty(zyh))
            {
                sqlStr.Append(@" ,zybrjbxx.zyh");
                inSqlParameterList.Add(new SqlParameter("@zyh", zyh.Trim()));
            }
                sqlStr.Append(@"
FROM xt_brjbxx brjbxx
");
            if (!string.IsNullOrEmpty(zyh)) {
                sqlStr.Append(@"
right join zy_brjbxx zybrjbxx
on zybrjbxx.blh=brjbxx.blh and zybrjbxx.zt='1' and zybrjbxx.OrganizeId=brjbxx.OrganizeId
                            ");
            }
                sqlStr.Append(@"
WHERE brjbxx.OrganizeId=@orgId
        AND brjbxx.zt='1' 
                        ");
            if (!string.IsNullOrEmpty(blh))
            {
                sqlStr.Append(" and brjbxx.blh like @blh ");
                inSqlParameterList.Add(new SqlParameter("@blh", "%" + blh.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(zyh))
            {
                sqlStr.Append(" and zybrjbxx.zyh = @zyh ");
            }
            if (!string.IsNullOrEmpty(xm))
            {
                sqlStr.Append(" and brjbxx.xm like @xm ");
                inSqlParameterList.Add(new SqlParameter("@xm", "%" + xm.Trim() + "%"));
            }
            inSqlParameterList.Add(new SqlParameter("@orgId", orgId.Trim()));
            var list = this.FindList<PatientInfoVO>(sqlStr.ToString(), inSqlParameterList.ToArray()).ToList();
            return list;
        }
        #endregion

        #region 记账退费
        /// <summary>
        /// 记账退费查询
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="smry"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<NonTreatmentItemBillingInfoVO> SelectRefundItemList(Pagination pagination, string zyh, string blh, string keyword, string smry, string kehu, DateTime? kssj, DateTime? jssj, string orgId)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                return null;
            }
            StringBuilder sqlStr = new StringBuilder();
            var parlist = new List<SqlParameter>();
            sqlStr.Append(@"
SELECT *
FROM 
    (SELECT *
    FROM 
        (SELECT jfbid,
         sum(sl) AS sl,
         sum(sl) AS tsl,
         sum(je) AS je
        FROM ( select
            CASE
            WHEN (cxjfbid is NOT null) THEN
            cxjfbid
            WHEN (cxjfbid is null) THEN
            jfbid
            END jfbid,sl,je
        FROM [xt_fzlxmjfb]
        WHERE OrganizeId=@orgId 
        ");
            if (kssj.HasValue)
            {
                sqlStr.Append(" AND jzrq >= @kssj");
                parlist.Add(new SqlParameter("@kssj", kssj.Value));
            }
            if (jssj.HasValue)
            {
                sqlStr.Append(" AND jzrq < @jssj");
                parlist.Add(new SqlParameter("@jssj", jssj.Value.AddDays(1).Date));
            }
            sqlStr.Append(@"
        ) AS a
        GROUP BY  jfbid )as b
        WHERE sl<>0 ) AS c
    LEFT JOIN 
    (SELECT 
        fzlxmjfb.jfbid AS fzlxmjfbId,
        fzlxmjfb.xm,
        fzlxmjfb.zyh,
        fzlxmjfb.blh,
        sfdl.dlmc AS dlName,
        fzlxmjfb.dlCode,
        sfxm.sfxmmc AS sfxmName,
        fzlxmjfb.sfxmCode,
        fzlxmjfb.dj,
        Staff.Name AS smryName,
        fzlxmjfb.smry,
        Department.Name AS smksName,
        fzlxmjfb.smksCode,
        fzlxmjfb.patId,
        fzlxmjfb.jzrq,
        fzlxmjfb.CreateTime
    FROM xt_fzlxmjfb fzlxmjfb
    LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm sfxm
        ON sfxm.sfxmCode=fzlxmjfb.sfxmCode
            AND sfxm.OrganizeId=@orgId
    LEFT JOIN NewtouchHIS_Base..V_S_xt_sfdl sfdl
        ON sfdl.dlCode=sfxm.sfdlCode
            AND sfdl.OrganizeId=@orgId
    LEFT JOIN NewtouchHIS_Base..V_S_Sys_Department Department
        ON Department.Code=fzlxmjfb.smksCode
            AND Department.OrganizeId=@orgId
    LEFT JOIN NewtouchHIS_Base..V_S_Sys_Staff Staff
        ON Staff.gh=fzlxmjfb.smry
            AND Staff.OrganizeId=@orgId
    WHERE fzlxmjfb.OrganizeId=@orgId ) d
    ON d.fzlxmjfbId=c.jfbId
WHERE 1=1
                        ");
            parlist.Add(new SqlParameter("@orgId", orgId.Trim()));
            if (!string.IsNullOrEmpty(keyword))
            {
                sqlStr.Append(" and (sfxmName like @keyword or sfxmCode like @keyword) ");
                parlist.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(smry))
            {
                sqlStr.Append(" and smry = @smry");
                parlist.Add(new SqlParameter("@smry", smry.Trim()));
            }
            if (!string.IsNullOrEmpty(kehu))
            {
                sqlStr.Append(" and (xm like @kehu or zyh like @kehu) ");
                parlist.Add(new SqlParameter("@kehu", "%" + kehu.Trim() + "%"));
            }
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                sqlStr.Append(" and zyh = @zyh ");
                parlist.Add(new SqlParameter("@zyh", zyh.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(blh))
            {
                sqlStr.Append(" and blh like @blh ");
                parlist.Add(new SqlParameter("@blh", "%" + blh.Trim() + "%"));
            }
            if (kssj.HasValue)
            {
                sqlStr.Append(" and jzrq >= @kssj");
            }
            if (jssj.HasValue)
            {
                sqlStr.Append(" and jzrq < @jssj");
            }
            //sqlStr.Append(" ORDER BY CreateTime DESC ");

            var list = this.QueryWithPage<NonTreatmentItemBillingInfoVO>(sqlStr.ToString(),pagination, parlist.ToArray()).ToList();
            return list;

        }
        #endregion

        #region 记账查询

        /// <summary>
        /// 获取收费项目分类List
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysChargeCategoryVEntity> GetChargeCategoryTreeList(string orgId)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"
select * from [NewtouchHIS_Base]..V_S_xt_sfdl where dllb = 3 and zt = '1' and OrganizeId = @orgId 
--顶级不显示
and ParentId is not null
                        ");
            SqlParameter[] param =
               {
                    new SqlParameter("@orgId", orgId)
                };
            var list = this.FindList<SysChargeCategoryVEntity>(sqlStr.ToString(), param).ToList();
            return list;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sffl"></param>
        /// <param name="keyword"></param>
        /// <param name="smry"></param>
        /// <param name="smks"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<NonTreatmentItemBillingInfoVO> SelectNonTreatmentItemList(Pagination pagination, string sfdl, string keyword,string smry,string smks,DateTime? kssj, DateTime? jssj,string orgId, string kehu, string zyh, string blh)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                return null;
            }
            StringBuilder sqlStr = new StringBuilder();
            var parlist = new List<SqlParameter>();
            sqlStr.Append(@"
SELECT aaaaaaa.rowNum as serialNumber,fzlxmjfb.CreateTime,
        fzlxmjfb.zjfbId jfbId ,
        fzlxmjfb.zyh,
        fzlxmjfb.blh,
        fzlxmjfb.xm ,
        sfdl.dlmc AS dlName,
        sfxm.sfxmmc AS sfxmName,
        fzlxmjfb.sfxmCode ,
        fzlxmjfb.dj,
        fzlxmjfb.sl,
        fzlxmjfb.je,
        fzlxmjfb.smry,
        Department.Name smksName,
        fzlxmjfb.smksCode ,
        fzlxmjfb.jzrq,
    CASE
    WHEN cxjfbId is NULL THEN
    Staff.Name
    ELSE null
    END smryName, --退费人员
    CASE
    WHEN cxjfbId is NULL THEN
    null
    ELSE Staff.Name
    END tfryName, --售卖时间
    CASE
    WHEN cxjfbId is NULL THEN
    fzlxmjfb.jzrq
    ELSE null
    END SaleTime, --退费时间
    CASE
    WHEN cxjfbId is NULL THEN
    null
    ELSE fzlxmjfb.jzrq
    END RefundTime
FROM 
    (
	SELECT row_number()
        OVER (order by jzrq desc, CreateTime desc) AS rowNum,jfbId, OrganizeId
    FROM xt_fzlxmjfb
    WHERE cxjfbId is null   --记账主记录
            AND zt = '1'
            AND OrganizeId = @orgId
                        ");
            if (kssj.HasValue)
            {
                sqlStr.Append(" AND jzrq >= @kssj");
                parlist.Add(new SqlParameter("@kssj", kssj.Value));
            }
            if (jssj.HasValue)
            {
                sqlStr.Append(" AND jzrq < @jssj");
                parlist.Add(new SqlParameter("@jssj", jssj.Value.AddDays(1)));
            }
            sqlStr.Append(@"
	) aaaaaaa
LEFT JOIN 
	( 
	SELECT
		CASE
		WHEN cxjfbId is NULL THEN
			jfbId
		ELSE cxjfbId           --得到主记录的Id
		END zjfbId, *
	FROM xt_fzlxmjfb
	WHERE zt = '1' 
			AND OrganizeId = @orgId
                        ");
            if (kssj.HasValue)
            {
                sqlStr.Append(" AND jzrq >= @kssj");
            }
            if (jssj.HasValue)
            {
                sqlStr.Append(" AND jzrq < @jssj");
            }
            sqlStr.Append(@"
    ) fzlxmjfb
	ON aaaaaaa.jfbId = fzlxmjfb.zjfbId
LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm sfxm
    ON sfxm.sfxmCode=fzlxmjfb.sfxmCode
        AND sfxm.OrganizeId=aaaaaaa.OrganizeId
LEFT JOIN NewtouchHIS_Base..V_S_xt_sfdl sfdl
    ON sfdl.dlCode=sfxm.sfdlCode
        AND sfdl.OrganizeId=aaaaaaa.OrganizeId
LEFT JOIN NewtouchHIS_Base..V_S_Sys_Department Department
    ON Department.Code=fzlxmjfb.smksCode
        AND Department.OrganizeId=aaaaaaa.OrganizeId
LEFT JOIN NewtouchHIS_Base..V_S_Sys_Staff Staff
    ON Staff.gh=fzlxmjfb.smry
        AND Staff.OrganizeId=aaaaaaa.OrganizeId
WHERE  1=1
                        ");
            if (kssj.HasValue)
            {
                sqlStr.Append(" AND jzrq >= @kssj");
            }
            if (jssj.HasValue)
            {
                sqlStr.Append(" AND jzrq < @jssj");
            }
            if (!string.IsNullOrEmpty(smry))
            {
                sqlStr.Append(" AND smry = @smry");
                parlist.Add(new SqlParameter("@smry", smry.Trim()));
            }
            if (!string.IsNullOrEmpty(smks))
            {
                sqlStr.Append(" AND smksCode = @smks");
                parlist.Add(new SqlParameter("@smks", smks.Trim()));
            }
            if (!string.IsNullOrEmpty(kehu))
            {
                sqlStr.Append(" AND xm like @kehu");
                parlist.Add(new SqlParameter("@kehu", "%" + kehu.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(zyh))
            {
                sqlStr.Append(" AND zyh = @zyh ");
                parlist.Add(new SqlParameter("@zyh", zyh.Trim()));
            }
            if (!string.IsNullOrEmpty(blh))
            {
                sqlStr.Append(" AND blh like @blh ");
                parlist.Add(new SqlParameter("@blh", "%" + blh.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(sfdl))
            {
                sqlStr.Append(" and sfdl.dlCode = @sfdl");
                parlist.Add(new SqlParameter("@sfdl", sfdl.Trim()));
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                sqlStr.Append(" and (sfxm.sfxmmc like @keyword or sfxm.sfxmCode like @keyword) ");
                parlist.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            parlist.Add(new SqlParameter("@orgId", orgId.Trim()));

            var list = this.QueryWithPage<NonTreatmentItemBillingInfoVO>(sqlStr.ToString(),pagination, parlist.ToArray()).ToList();
            return list;
        }
        #endregion


    }
}
