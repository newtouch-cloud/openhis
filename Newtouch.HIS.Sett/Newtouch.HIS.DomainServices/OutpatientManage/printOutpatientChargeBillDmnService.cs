using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.IDomainServices.OutpatientManage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.DomainServices.OutpatientManage
{
    public class printOutpatientChargeBillDmnService : DmnServiceBase, IprintOutpatientChargeBillDmnService
    {
        private readonly IOutpatientRegistRepo _outpatientRegistRepo;
      

        public printOutpatientChargeBillDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public string GetmjzpjJson(string jsnm,string orgId) {
            OutpatientChargeBillDto dto = new OutpatientChargeBillDto();
            try
            {
                var par = new List<SqlParameter>();
                par.Add(new SqlParameter("@jsnm", jsnm));
                par.Add(new SqlParameter("@orgId", orgId));
                dto.mzsfpj_dlInfo = this.FindList<mzsfpj_dlInfo>("EXEC dbo.rpt_RegisterChargePrint_cs_fastreport @jsnm=@jsnm,@orgId=@orgId ", par.ToArray());


                string sql = @"DECLARE @brxz VARCHAR(10);
SELECT  @brxz = brxz
FROM    [NewtouchHIS_Sett].[dbo].mz_js(NOLOCK)  
WHERE   jsnm = @jsnm

IF @brxz='08' OR @brxz='8'
BEGIN
	DECLARE @qtfy NUMERIC(16,2);
	DECLARE @zhzf NUMERIC(16,2);
	DECLARE @compensateCost NUMERIC(16,2);
	DECLARE @salvaJSCost NUMERIC(16,2);
	DECLARE @civilCost NUMERIC(16,2);
	DECLARE @bottomRedeem NUMERIC(16,2);
	SELECT @qtfy=CONVERT(NUMERIC(16,4),sr.civilCost)+CONVERT(NUMERIC(16,4),sr.compensateCost)+CONVERT(NUMERIC(16,4),sr.insureCost)+CONVERT(NUMERIC(16,4),sr.salvaJSCost)
					  +CONVERT(NUMERIC(16,4),sr.bottomRedeem)+CONVERT(NUMERIC(16,4),sr.bottomSecondRedeem)
					  +CONVERT(NUMERIC(16,4),sr.medicineCost)+CONVERT(NUMERIC(16,4),sr.salvaYFCost)
					  +CONVERT(NUMERIC(16,4),sr.salvaCLCost)+CONVERT(NUMERIC(16,4),sr.salvaFPCost)
					  +CONVERT(NUMERIC(16,4),sr.salvaJKCost)+CONVERT(NUMERIC(16,4),sr.continentInsuranceCost),
		   @zhzf=compensateCost, @compensateCost=@compensateCost, @salvaJSCost=salvaJSCost, @civilCost=civilCost, @bottomRedeem=CONVERT(NUMERIC(16,4),sr.bottomRedeem)+CONVERT(NUMERIC(16,4),sr.bottomSecondRedeem)
	FROM dbo.mz_xnh_settResult(NOLOCK) sr WHERE jsnm=@jsnm AND jszt=1 AND OrganizeId=@orgId AND zt='1'
	
	SELECT  gh.xm ,
			gh.xb ,
			xz.brxzmc ,
			gh.mzh ,
			js.fph ,
			js.zje ,
			ISNULL(js.zkhzje, js.zje) AS zkhzje ,
			dbo.fn_getformatmoney(ISNULL(js.zkhzje, js.zje))  AS zkhdxje ,
			ISNULL(js.zl, 0.00) AS zl ,
			ISNULL(js.ysk, 0.00) AS ysk ,
			staff.Name AS sky ,
			js.CreateTime ,
			js.xjzffs ,
			ISNULL(js.xjzf, 0.00) XJZF ,--现金支付
			ISNULL(@qtfy, 0.00) TCZF ,--基本医疗统筹
			0.00 GWYBZ ,--公务员补助
			0.00 DBZF ,--大额支付
			ISNULL(@civilCost, 0.00) MZBC ,--民政补偿
			CAST(@salvaJSCost as varchar(50)) JSBC ,--计生补偿
		    CAST(@bottomRedeem as varchar(50))	 DDBC,--兜底补偿
			ISNULL(@zhzf, 0.00) AS ZHZF --账户支付
	FROM    dbo.mz_gh(NOLOCK) gh 
			LEFT JOIN dbo.mz_js(NOLOCK) js ON gh.ghnm = js.ghnm AND js.OrganizeId = gh.OrganizeId
			LEFT JOIN dbo.xt_brxz(NOLOCK) xz ON xz.brxz = gh.brxz AND xz.OrganizeId = gh.OrganizeId
			LEFT JOIN NewtouchHIS_Base.dbo.V_C_Sys_UserStaff staff ON staff.Account = js.CreatorCode AND staff.OrganizeId = js.OrganizeId
			LEFT JOIN dbo.xt_xjzffs(NOLOCK) zffs ON zffs.xjzffs = js.xjzffs
	WHERE   gh.OrganizeId = @orgId
			AND js.OrganizeId = gh.OrganizeId
			AND (js.jsnm = @jsnm OR @jsnm='')
			AND gh.zt = 1
			AND js.zt = 1 
END
ELSE
BEGIN
	SELECT  gh.xm ,
			gh.xb ,
			xz.brxzmc ,
			gh.mzh ,
			js.fph ,
			js.zje ,
			ISNULL(js.zkhzje, js.zje) AS zkhzje ,
			dbo.fn_getformatmoney(ISNULL(js.zkhzje, js.zje)) AS zkhdxje ,
			ISNULL(js.zl, 0.00) AS zl ,
			ISNULL(js.ysk, 0.00) AS ysk ,
			staff.Name AS sky ,
			js.CreateTime ,
			js.xjzffs ,
			ISNULL(js.xjzf, 0.00) XJZF ,
			ISNULL(b.prm_yka248, 0.00) TCZF ,
			ISNULL(b.prm_yke030, 0.00) GWYBZ ,
			ISNULL(b.prm_yka062, 0.00) DBZF ,
			ISNULL(b.prm_ake181, 0.00) MZBC ,
			'0.00' JSBC ,'0.00' DDBC,
			ISNULL(b.prm_yka065, 0.00) AS ZHZF
	FROM    dbo.mz_gh gh WITH ( NOLOCK )
			LEFT JOIN dbo.mz_js js ON gh.ghnm = js.ghnm
									  AND js.OrganizeId = gh.OrganizeId
			LEFT JOIN [mz_js_gaybjyfy] b WITH ( NOLOCK ) ON js.jsnm = b.jsnm
															AND js.organizeid = b.organizeid
															AND LEN(ISNULL(b.prm_akc190,
																  '')) > 0
			LEFT JOIN dbo.xt_brxz xz ON xz.brxz = gh.brxz
										AND xz.OrganizeId = gh.OrganizeId
			LEFT JOIN NewtouchHIS_Base..V_C_Sys_UserStaff staff ON staff.Account = js.CreatorCode
																  AND staff.OrganizeId = js.OrganizeId
			LEFT JOIN dbo.xt_xjzffs zffs ON zffs.xjzffs = js.xjzffs
	WHERE   gh.OrganizeId = @orgId
			AND js.OrganizeId = gh.OrganizeId
			AND ( js.jsnm = @jsnm OR @jsnm='')
			AND gh.zt = 1
			AND js.zt = 1 
END";
                var par2 = new List<SqlParameter>();
                par2.Add(new SqlParameter("@jsnm", jsnm));
                par2.Add(new SqlParameter("@orgId", orgId));
                dto.mzsfpj_xmInfo = this.FindList<mzsfpj_xmInfo>(sql, par2.ToArray());

                string sql3 = @"select Name from [NewtouchHIS_Base]..Sys_Organize where Id = @orgId and zt = '1'";
                var par3 = new List<SqlParameter>();
                par3.Add(new SqlParameter("@orgId", orgId));
                dto.orgInfo = this.FirstOrDefault<orgInfo>(sql3, par3.ToArray());
            }
            catch (System.Exception e)
            {

                throw;
            }
           
            return dto.ToJson();
        }

        public string GetCqmjzpjJson(string jsnm, string orgId)
        {
            CqOutpatientChargeBillDto dto = new CqOutpatientChargeBillDto();
            try {
                #region
                string sql = @"WITH    jsmx
AS (
SELECT   dept.name ksName,mzgh.mjzbz,mzjs.CreatorCode,mzgh.kh,mzjs.jsnm ,mzjs.patid,mzjs.fph,mzgh.mzh,mzjs.xm,case mzjs.xb when '1' then '男' else '女' end xb,mzgh.nlshow nl,
        mzjs.csny,
        mzjs.zje ,
        mzjsmx.jsmxnm ,
        mzjsmx.mxnm AS xmjfId ,
        mzjsmx.cf_mxnm AS ypjfId ,
        mzjsmx.sl ,
        mzjsmx.jyje ,
        ISNULL(mzjs.jzsj, mzjs.CreateTime) AS sfrq ,
        mzjs.CreateTime
FROM     mz_js AS mzjs
        LEFT OUTER JOIN mz_jsmx AS mzjsmx ON mzjsmx.jsnm = mzjs.jsnm
                                        AND mzjsmx.OrganizeId = mzjs.OrganizeId
		Left join mz_gh as mzgh on mzgh.ghnm=mzjs.ghnm
		LEFT JOIN NewtouchHIS_Base..V_S_Sys_Department dept on  dept.Code = mzgh.ks AND dept.OrganizeId = mzgh.OrganizeId AND dept.zt = 1
WHERE    ( mzjs.OrganizeId =@orgId )
        AND ( mzjs.zt = '1' )
        AND ( mzjsmx.zt = '1' )
        AND ( mzjs.jsnm = @jsnm
                OR @jsnm = ''
            )
        --AND ( ISNULL(mzjs.tbz, 0) = 0 )
        AND ( mzjsmx.jyje IS NOT NULL )
),
cqmzfpdy AS(

	 
				SELECT 
            --xmjsmx.jsmxnm ,
			ksName,xmjsmx.CreatorCode,kh,xmjsmx.patid,xmjsmx.fph,xmjsmx.mzh,xmjsmx.xm,xmjsmx.xb,xmjsmx.nl,xmjsmx.csny,xmjsmx.sfrq,
            xmjsmx.sl ,
            xmjsmx.jyje ,
            convert(decimal(18,2),mzxm.dj)dj ,
            2 AS feeType ,
            mzxm.dw ,
            --cf.cfh ,
            sfxm.sfxmCode ,
            sfxm.sfxmmc AS mc ,
            sfdl.dlmc,
			'' ypgg
    FROM      ( SELECT    jsnm ,kh,CreatorCode,ksName,
						patid,fph,mzh,xm,xb,nl,csny,
                        zje ,
                        jsmxnm ,
                        xmjfId ,
                        ypjfId ,
                        sl ,
                        jyje ,
                        sfrq ,
                        CreateTime
                FROM      jsmx AS jsmx_2
                WHERE     ( ISNULL(xmjfId, 0) <> 0 )
            ) AS xmjsmx
            LEFT OUTER JOIN mz_xm AS mzxm ON mzxm.xmnm = xmjsmx.xmjfId
            LEFT OUTER JOIN mz_cf AS cf ON mzxm.cfnm = cf.cfnm
                                            AND cf.OrganizeId = mzxm.OrganizeId
            inner JOIN NewtouchHIS_Base.dbo.V_S_xt_sfxm
            AS sfxm ON sfxm.sfxmCode = mzxm.sfxm
                        AND sfxm.OrganizeId = mzxm.OrganizeId
            LEFT OUTER JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl
            AS sfdl ON sfdl.dlCode = sfxm.sfdlCode
                        AND sfdl.OrganizeId = sfxm.OrganizeId
    WHERE     ( 1 = 1 )
            AND ( mzxm.xmnm IS NOT NULL )
    UNION ALL
    SELECT 
            --ypjsmx.jsmxnm ,
			ksName,ypjsmx.CreatorCode,kh,ypjsmx.patid,ypjsmx.fph,ypjsmx.mzh,ypjsmx.xm,ypjsmx.xb,ypjsmx.nl,ypjsmx.csny,ypjsmx.sfrq,
            ypjsmx.sl ,
            ypjsmx.jyje AS jsmxje ,
            convert(decimal(18,2),ypmx.dj) dj,
            1 AS feeType ,
            ypmx.dw ,
            --cf.cfh ,
            yp.ypCode sfxmCode ,
            yp.ypmc AS mc ,
            sfdl.dlmc,
			cyp.ypgg
    FROM      ( SELECT    jsnm ,kh,CreatorCode,ksName,
						patid,fph,mzh,xm,xb,nl,csny,
                        zje ,
                        jsmxnm ,
                        xmjfId ,
                        ypjfId ,
                        sl ,
                        jyje ,
                        sfrq ,
                        CreateTime
                FROM      jsmx AS jsmx_1
                WHERE     ( ISNULL(ypjfId, 0) <> 0 )
            ) AS ypjsmx
            LEFT OUTER JOIN mz_cfmx AS ypmx ON ypmx.cfmxId = ypjsmx.ypjfId
            LEFT OUTER JOIN mz_cf AS cf ON ypmx.cfnm = cf.cfnm
                                            AND cf.OrganizeId = ypmx.OrganizeId
            inner JOIN NewtouchHIS_Base.dbo.V_S_xt_yp
            AS yp ON yp.ypCode = ypmx.yp
                        AND yp.OrganizeId = ypmx.OrganizeId
						LEFT OUTER JOIN NewtouchHIS_Base.dbo.V_C_xt_yp
            AS cyp ON cyp.ypCode = ypmx.yp
                        AND cyp.OrganizeId = ypmx.OrganizeId
            LEFT OUTER JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl
            AS sfdl ON sfdl.dlCode = yp.dlCode
                        AND sfdl.OrganizeId = yp.OrganizeId
    WHERE     ( 1 = 1 )
            AND ( ypmx.cfmxId IS NOT NULL )

	
),
mzzfzje AS
(
	SELECT dbo.fn_getformatmoney(sum(cast((cqmzfpdy.sl*dj) as decimal(18,2)))) zje FROM cqmzfpdy 
),
ybjs as
(
select 1 a,cqtczf,zhzf,gwybz,delpje,jsjl.maf_pay mzjzje,dbzddyljgdz,isnull(jsjl.hifes_pay,0) lsqfxgwyff,jsjl.hifmi_pay  zhdy,jsjl.oth_pay qtzc,0.00 sytc,js.xjzf cqxjzf,zhye  from cqyb_OutPut05 ybjs
left join drjk_mzjs_output jsjl on jsjl.setl_id=ybjs.jylsh 
left join mz_js js on ybjs.jsnm=js.jsnm and js.OrganizeId=ybjs.OrganizeId
where ybjs.jsnm=@jsnm and ybjs.OrganizeId =@orgId
)
select cast(t.a as varchar) a,t.jyje ,t.kh,t.creatorCode,t.ksName,t.dlmc,cast(t.patid as varchar) patid,
t.mzh,t.fph,t.xm,t.xb,t.nl,CONVERT(varchar(10), t.csny,121) csny,t.sfrq,cast(t.feeType as varchar) feeType,t.zje,
cast(isnull(ybjs.cqtczf,0.00) as varchar) cqtczf,cast(isnull(ybjs.cqxjzf,0.00) as varchar) cqxjzf,cast(ISNULL(ybjs.delpje,0.00) as varchar) delpje,
cast(isnull(ybjs.gwybz,0.0) as varchar) gwybz,cast(isnull(ybjs.lsqfxgwyff,0.0) as varchar) lsqfxgwyff,cast(isnull(ybjs.sytc,0.0) as varchar) sytc,
cast(isnull(ybjs.zhdy,'0.00') as varchar) zhdy,cast(isnull(ybjs.zhye,0.00) as varchar) zhye,cast(isnull(ybjs.zhzf,0.00) as varchar) zhzf,
cast(isnull(ybjs.mzjzje,'0.00') as varchar) mzjzje,cast(isnull(ybjs.dbzddyljgdz,'0.00') as varchar) dbzddyljgdz ,cast(isnull(ybjs.qtzc,'0.00') as varchar) qtzc  from (
select mzjymx.*,mzzfzje.* from(
SELECT 1 a, sum(jyje) jyje,kh,CreatorCode,ksName,
	 dlmc,patid,mzh,fph,xm,xb,nl,csny,convert(varchar(10), sfrq,23) sfrq,feeType
      FROM     cqmzfpdy
			where sl > 0
	group by patid,mzh,fph,xm,xb,nl,cqmzfpdy.dlmc,cqmzfpdy.feeType,csny,sfrq,kh,CreatorCode,ksName
	) as mzjymx,mzzfzje) t
	left join ybjs on ybjs.a=t.a";
                #endregion
                var par2 = new List<SqlParameter>();
                par2.Add(new SqlParameter("@jsnm", jsnm));
                par2.Add(new SqlParameter("@orgId", orgId));
                dto.mzsfpj_dlInfo = this.FindList<Cqmzsfpj_dlInfo>(sql, par2.ToArray());

                string sql2 = @"select js.CreatorCode cjr,ss.Name xm from mz_js js 
left join [NewtouchHIS_Base]..Sys_Staff ss on ss.gh=js.CreatorCode and ss.OrganizeId=js.OrganizeId and ss.zt=1
where jsnm=@jsnm  and js.OrganizeId=@orgId";
                var par3 = new List<SqlParameter>();
                par3.Add(new SqlParameter("@orgId", orgId));
                par3.Add(new SqlParameter("@jsnm", jsnm));
                dto.patInfo = this.FirstOrDefault<patInfo>(sql2, par3.ToArray());
            }
            catch (System.Exception e)
            {

                throw;
            }
            return dto.ToJson();
        }
    }
}
