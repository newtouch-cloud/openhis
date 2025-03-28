using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.IDomainServices.OutpatientManage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Sett.Request.OutPatientPharmacy;

namespace Newtouch.HIS.DomainServices.OutpatientManage
{
    public class OutpatientPharmacyAPIDmnService : DmnServiceBase, IOutpatientPharmacyAPIDmnService
    {

        private readonly IOutpatientSettlementDetailRepo _outpatientSettlementDetailRepo;

        public OutpatientPharmacyAPIDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 加载排药合计信息
        /// </summary>
        /// <param name="ksdm"></param>
        /// <param name="fysjs"></param>
        /// <param name="fybz"></param>
        /// <returns></returns>
        public OutPatientpyInfoDTO GetFyhjInfo(string ksdm, DateTime fysjs, int fybz)
        {
            var strSql = new StringBuilder();
            strSql.Append(@" EXEC [dbo].[GetFyhjInfo] @ksdm = @ksdm, @Fysjs = @fysjs, @Fybz = @fybz");
            var paraList = new DbParameter[]
            {
                new SqlParameter("@ksdm", ksdm),
                new SqlParameter("@fysjs", fysjs),
                new SqlParameter("@fybz", fybz)
            };
            return FirstOrDefault<OutPatientpyInfoDTO>(strSql.ToString(), paraList);
        }

        /// <summary>
        /// 查询排药信息
        /// </summary>
        /// <param name="ksdm"></param>
        /// <param name="yxq"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<OutPatientpyListDTO> GetFyList(string ksdm, int yxq, string orgId)
        {
            var paraList = new DbParameter[]
            {
                new SqlParameter("@ksdm", ksdm),
                new SqlParameter("@yxq", yxq),
                new SqlParameter("@orgId", orgId)
            };
            return FindList<OutPatientpyListDTO>(@"EXEC dbo.YP_XT_CXFYXX @Ksdm = @ksdm, @Fyyxq = @yxq,@orgId=@orgId", paraList);
        }

        /// <summary>
        /// 补打发药单 查询发药信息
        /// </summary>
        /// <param name="ksdm"></param>
        /// <param name="fph"></param>
        /// <param name="xm"></param>
        /// <param name="kh"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<OutPatientpyListDTO> GetFyListOnPrint(string ksdm, string fph, string xm, string kh, DateTime kssj, DateTime jssj, string organizeId)
        {
            var paraList = new DbParameter[]
            {
                new SqlParameter("@ksdm", ksdm),
                new SqlParameter("@fph", fph),
                new SqlParameter("@xm", xm),
                new SqlParameter("@kh", kh),
                new SqlParameter("@kssj", kssj),
                new SqlParameter("@jssj", jssj),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return FindList<OutPatientpyListDTO>(@"EXEC [dbo].[YP_XT_CXFYXX_BD] @OrganizeId, @Ksdm = @Ksdm, @Fph = @fph, @Xm = @xm, @Kh = @kh, @Kssj =@kssj, @Jssj =@jssj", paraList);
        }

        /// <summary>
        /// 查询排药详细药品信息
        /// </summary>
        /// <param name="cfh">处方号</param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<OutPatientpyDetailListDTO> GetFyDetailList(string cfh, string orgId)
        {
            var paraList = new DbParameter[]
            {
                new SqlParameter("@cfh", cfh), new SqlParameter("@orgId", orgId)
            };
            return FindList<OutPatientpyDetailListDTO>(@"EXEC dbo.YP_XT_CXFYXXMX @cfh = @cfh,@orgId=@orgId", paraList);
        }

        /// <summary>
        /// 根据处方内码获取详细的处方药品信息
        /// </summary>
        /// <param name="cfnm"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<YP_XT_OP_PYListDTO> GetpyParList(string cfnm, string organizeId)
        {
            var paraList = new DbParameter[]
            {
                new SqlParameter("@cfnm", cfnm),
                new SqlParameter("OrganizeId", organizeId)
            };
            return FindList<YP_XT_OP_PYListDTO>(@"SELECT cfmx.cfmxId ,cfmx.yp yp , jsmx.sl sl ,cfmx.tybz ,1 AS cls, cf.cfh
FROM mz_cfmx(NOLOCK) cfmx
INNER JOIN dbo.mz_jsmx(NOLOCK) jsmx ON jsmx.cf_mxnm=cfmx.cfmxId AND jsmx.OrganizeId=cfmx.OrganizeId
INNER JOIN dbo.mz_js(NOLOCK) js ON js.jsnm=jsmx.jsnm AND js.cxjsnm=0 AND ISNULL(js.tbz, 0) = 0 AND js.jszt=1 AND js.OrganizeId=cfmx.OrganizeId
INNER JOIN dbo.mz_cf(NOLOCK) cf ON cf.cfnm=cfmx.cfnm AND cf.OrganizeId=cfmx.OrganizeId
WHERE cfmx.cfnm = @cfnm 
AND cfmx.OrganizeId=@OrganizeId", paraList);
        }

        /// <summary>
        /// 获取处方金额，领药药房信息
        /// </summary>
        /// <param name="cfnm"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<GetcfInfoDTO> GetcfInfo(string cfnm, string organizeId)
        {
            var paraList = new DbParameter[]
            {
                new SqlParameter("@cfnm", cfnm),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return FindList<GetcfInfoDTO>(@"SELECT zje, lyyf FROM mz_cf(NOLOCK) WHERE zt = 1 AND OrganizeId=@OrganizeId AND cfnm = @cfnm AND ISNULL(fybz, '0')=0 ", paraList);
        }

        /// <summary>
        /// 更新发药标志
        /// </summary>
        /// <param name="par"></param>
        public bool Updatecfzt(UpdatefyztRequest par)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"    UPDATE  mz_cf
                                SET     fybz = @zt ,
                                        lyck = @lyck ,
                                        pyry = @user_code,
                                        pyrq = GETDATE()
                                WHERE   cfnm = @cfnm ");
            var paraList = new DbParameter[]
            {
                new SqlParameter("@zt", "1"),
                new SqlParameter("@lyck", par.lyck),
                new SqlParameter("@user_code", par.user_code),
                new SqlParameter("@cfnm", par.cfnm)
            };
            _dataContext.Database.ExecuteSqlCommand(strSql.ToString(), paraList.ToArray());
            return true;
        }

        /// <summary>
        /// 门诊发药显示主表信息
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<fyMainListRequest> GetfyMainInfoList(string keyword, string orgId)
        {
            var strSql = new StringBuilder(@"
SELECT DISTINCT js.fph fph, br.xm xm,
CAST(FLOOR(DATEDIFF(DY, br.csny, GETDATE()) / 365.25) AS INT) nl ,
c.CardNo kh,cf.cfh cfh,
( CASE cf.brxz WHEN '00' THEN '自费' ELSE '医保' END ) brlx ,
js.CreateTime sfsj, cf.zje je,
( CASE mx.dl WHEN '10' THEN 2 ELSE 1 END ) cflx ,
c.CardNo ybh, dbo.fn_getGender(br.xb) xb, ks.Name ks, ry.Name ys, ry2.Name pyy
FROM mz_cf(NOLOCK) cf
INNER JOIN mz_cfmx(NOLOCK) mx ON mx.cfnm = cf.cfnm AND mx.OrganizeId=cf.OrganizeId
INNER JOIN mz_jsmx(NOLOCK)jsmx ON jsmx.cf_mxnm = mx.cfmxId AND jsmx.OrganizeId=cf.OrganizeId
INNER JOIN mz_js(NOLOCK) js ON js.jsnm = jsmx.jsnm AND js.cxjsnm=0 AND ISNULL(js.tbz, 0) = 0 AND js.jszt=1 AND js.OrganizeId=cf.OrganizeId
LEFT JOIN xt_brjbxx(NOLOCK) br ON br.patid = cf.patid AND br.zt = '1' AND br.OrganizeId=cf.OrganizeId
LEFT JOIN dbo.xt_card(NOLOCK) c ON c.patid = br.patid AND c.OrganizeId=cf.OrganizeId
LEFT JOIN xt_brxz(NOLOCK) xz ON xz.brxz = cf.brxz AND br.zt = '1' AND xz.OrganizeId=cf.OrganizeId
LEFT JOIN [NewtouchHIS_Base]..V_S_Sys_Department ks ON ks.Code = cf.ks AND ks.OrganizeId=cf.OrganizeId
LEFT JOIN [NewtouchHIS_Base]..V_S_Sys_Staff ry ON ry.gh=cf.ys AND ry.OrganizeId=cf.OrganizeId
LEFT JOIN [NewtouchHIS_Base]..V_S_Sys_Staff ry2 ON ry2.gh = cf.pyry AND ry2.OrganizeId=cf.OrganizeId
WHERE cf.OrganizeId=@orgId
AND cf.cfzt = '1'
AND cf.fybz = '1' 
AND ( br.xm LIKE @keyword OR c.CardNo LIKE @keyword OR js.fph LIKE @keyword)
");
            var paraList = new DbParameter[]
            {
                new SqlParameter("@keyword", "%" + keyword + "%"),
                new SqlParameter("@orgId", orgId)
            };
            return FindList<fyMainListRequest>(strSql.ToString(), paraList);
        }

        /// <summary>
        /// 门诊发药显示处方详细信息
        /// </summary>
        /// <param name="cfh">处方号</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<fyDetailListRequest> GetfyDetailInfoList(string cfh, string organizeId)
        {
            var strSql = new StringBuilder(@"
SELECT DISTINCT cf.cfh, 
cfmxId, 
y.ypmc ypmc, 
y.ypCode ypCode, 
x.ypgg gg, 
y.mzcldw dw, 
a.dj dj, 
a.je je, 
a.cfnm cfnm, 
y.ycmc sccj, 
ISNULL(y.jl, 0) jl, 
y.jldw jldw, 
b.yfmc yfmc, 
ISNULL(a.yl, 0) yl, 
a.yldw yldw, 
NULL yszt
FROM mz_cfmx(NOLOCK) a
INNER JOIN mz_cf (NOLOCK) cf ON cf.cfnm = a.cfnm AND cf.OrganizeId=a.OrganizeId
INNER JOIN dbo.mz_jsmx(NOLOCK) jsmx ON jsmx.cf_mxnm=a.cfmxId AND jsmx.OrganizeId=a.OrganizeId
INNER JOIN dbo.mz_js(NOLOCK) js ON js.jsnm=jsmx.jsnm AND js.OrganizeId=a.OrganizeId AND js.cxjsnm=0 AND ISNULL(js.tbz, 0) = 0 AND js.jszt IN ( '1','3')
INNER JOIN NewtouchHIS_Base..V_S_xt_yp y ON y.ypCode = a.yp AND y.zt = '1' AND y.mzzybz = '1' AND y.OrganizeId=a.OrganizeId --药品字典
INNER JOIN NewtouchHIS_Base..V_S_xt_ypsx x ON x.ypId = y.ypId AND x.zt='1' AND x.OrganizeId=a.OrganizeId --药品属性
LEFT JOIN NewtouchHIS_Base..V_S_xt_ypyf b ON b.yfCode = y.yfCode --药品用法
WHERE cf.cfh = @cfh
AND a.OrganizeId=@OrganizeId
");
            var paraList = new DbParameter[]
            {
                new SqlParameter("@cfh", cfh),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return FindList<fyDetailListRequest>(strSql.ToString(), paraList);
        }

        #region GetfyDetailCFInfo old code
        //        /// <summary>
        //        /// 门诊发药，发药时获取处方信息
        //        /// </summary>
        //        /// <param name="cfh"></param>
        //        /// <param name="lyyf"></param>
        //        /// <param name="organizeId"></param>
        //        /// <returns></returns>
        //        public IList<fyCfYpInfo> GetfyDetailCFInfo(string cfh, string lyyf, string organizeId)
        //        {
        //            var cntSql = new StringBuilder(@"    
        //SELECT DISTINCT cf.cfh
        //FROM dbo.mz_cf(NOLOCK) cf
        //INNER JOIN dbo.mz_cfmx(NOLOCK) cfmx ON cfmx.cfnm=cf.cfnm AND cf.OrganizeId=cfmx.OrganizeId
        //INNER JOIN dbo.mz_jsmx(NOLOCK) jsmx ON jsmx.cf_mxnm=cfmx.cfmxId AND cf.OrganizeId=jsmx.OrganizeId
        //INNER JOIN dbo.mz_js(NOLOCK) js ON js.jsnm=jsmx.jsnm AND js.tbz=0 AND js.OrganizeId=cf.OrganizeId
        //WHERE cf.cfh=@cfh
        //AND cf.OrganizeId=@OrganizeId
        //AND ( js.jszt = '1'OR js.jszt = '3')
        //");
        //            var par = new DbParameter[] {
        //                new SqlParameter("@cfh",cfh),
        //                new SqlParameter("@OrganizeId", organizeId)
        //            };

        //            // int res = this._dataContext.Database.ExecuteSqlCommand(cntSql.ToString(), paraList2.ToArray());
        //            var res = this.FindList<string>(cntSql.ToString(), par).Count;
        //            if (res <= 0)
        //            {
        //                return null;
        //            }
        //            var strSql = new StringBuilder(@"  
        //SELECT cf.cfnm , mx.cfmxId ,mx.yp
        //FROM dbo.mz_cf(NOLOCK) cf
        //INNER JOIN dbo.mz_cfmx(NOLOCK) mx ON cf.cfnm = mx.cfnm AND mx.OrganizeId=cf.OrganizeId
        //WHERE cf.OrganizeId=@OrganizeId
        //AND cf.cfh = @cfh 
        //AND cf.lyyf = @lyyf 
        //AND cf.cfzt = '1' 
        //AND cf.fybz = '1'
        //");
        //            var paraList = new DbParameter[]
        //            {
        //                new SqlParameter("@cfh", cfh),
        //                new SqlParameter("@lyyf", lyyf),
        //                new SqlParameter("@OrganizeId", organizeId)
        //            };
        //            return FindList<fyCfYpInfo>(strSql.ToString(), paraList);
        //        }

        #endregion
        /// <summary>
        /// 门诊发药，发药时获取处方信息
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="lyyf"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<fyCfYpInfo> GetfyDetailCFInfo(string cfh, string lyyf, string organizeId)
        {
            const string cntSql = @"    
SELECT cf.cfnm, cf.cfh, cfmx.cfmxId, cfmx.yp
FROM dbo.mz_cf(NOLOCK) cf
INNER JOIN dbo.mz_cfmx(NOLOCK) cfmx ON cfmx.cfnm=cf.cfnm AND cfmx.OrganizeId=cf.OrganizeId
INNER JOIN dbo.mz_jsmx(NOLOCK) jsmx ON jsmx.cf_mxnm=cfmx.cfmxId AND jsmx.OrganizeId=cf.OrganizeId
INNER JOIN dbo.mz_js(NOLOCK) js ON js.jsnm=jsmx.jsnm AND js.cxjsnm=0 AND ISNULL(js.tbz, 0) = 0 AND ( js.jszt = '1'OR js.jszt = '3')
WHERE cf.cfh=@cfh
AND cf.OrganizeId=@OrganizeId
AND cf.lyyf = @lyyf 
AND cf.cfzt = '1' 
AND ISNULL(cf.fybz,'1') = '1'
";
            var par = new DbParameter[] {
                new SqlParameter("@cfh",cfh),
                new SqlParameter("@lyyf", lyyf),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return FindList<fyCfYpInfo>(cntSql, par);
        }

        /// <summary>
        /// 发药完成后更新处方表的发药标志
        /// </summary>
        /// <param name="cfnm"></param>
        /// <param name="userCode"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public bool UpdatecfztByFY(string cfnm, string userCode, string zt)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"    UPDATE    mz_cf
                      SET       fybz = @zt ,
                                fyry = @user_code ,
                                fyrq = GETDATE()
                      WHERE     cfnm = @cfnm
                              --  AND isnull(fybz,'') <> '2'; ");
            var paraList = new object[]
            {
                new SqlParameter("@zt", zt),
                new SqlParameter("@user_code", userCode),
                new SqlParameter("@cfnm", cfnm)
            };
            var count = _dataContext.Database.ExecuteSqlCommand(strSql.ToString(), paraList);
            return count == 1;
        }

        /// <summary>
        /// 门诊退药显示主表信息
        /// </summary>
        /// <param name="req"></param>
        /// <param name="orgId"></param>
        /// <returns></returns> 
        public IList<tyCFMainInfo> GetTyMainInfoList(tyCFYpInfoRequest req, string orgId)
        {
            var strSql = new StringBuilder(@"
SELECT DISTINCT mx.cfmxId, mx.yp, js.fph fph , br.xm xm ,
CAST(FLOOR(DATEDIFF(DY, br.csny, GETDATE()) / 365.25) AS INT) nl ,
c.CardNo kh , cf.cfh cfh , cf.brlx brlx , js.CreateTime sfsj , cf.zje zje ,
CASE mx.dl WHEN '10' THEN 2 ELSE 1 END cflx ,
c.cardno ybh , dbo.fn_getGender(br.xb) xb, cf.ks ks , cf.ys ys , cf.pyy pyy
FROM ( 
	SELECT aa.cfnm , aa.cfh , aa.zje ,aa.fyrq ,	aa.cfzt ,aa.jsnm ,aa.patid ,aa.brxz ,k.Name ks ,ry1.Name ys ,ry2.Name pyy, (CASE aa.brxz WHEN '00' THEN '自费' ELSE '医保' END) brlx 	
	FROM mz_cf(NOLOCK) aa
	LEFT JOIN [NewtouchHIS_Base]..V_S_Sys_Department k ON k.Code = aa.ks and k.OrganizeId = aa.OrganizeId
	LEFT JOIN [NewtouchHIS_Base]..V_S_Sys_Staff ry1 ON ry1.gh = aa.ys AND ry1.OrganizeId = aa.OrganizeId
	LEFT JOIN [NewtouchHIS_Base]..V_S_Sys_Staff ry2 ON ry2.gh = aa.pyry AND ry2.OrganizeId = aa.OrganizeId
	WHERE aa.fybz = '2'
	AND aa.OrganizeId=@orgId
	AND aa.cfzt = '1'
	AND aa.fyrq >= CONVERT(VARCHAR(100), DATEADD(DAY, -10, GETDATE()), 23)
	AND aa.fyrq < CONVERT(VARCHAR(100), DATEADD(DAY, 1, GETDATE()), 23)
	AND aa.lyyf = @lyyf
) cf
INNER JOIN mz_cfmx(NOLOCK) mx ON mx.cfnm = cf.cfnm
INNER JOIN mz_jsmx(NOLOCK) jsmx ON jsmx.cf_mxnm = mx.cfmxId
INNER JOIN mz_js(NOLOCK) js ON js.jsnm = jsmx.jsnm AND js.cxjsnm=0 AND ISNULL(js.tbz, 0) = 0 AND js.jszt=1
LEFT JOIN xt_brjbxx(NOLOCK) br ON br.patid = cf.patid AND br.zt = '1'
INNER JOIN dbo.xt_card(NOLOCK) c ON c.patid = br.patid
LEFT JOIN xt_brxz (NOLOCK)xz ON xz.brxz = cf.brxz
WHERE (c.CardNo LIKE @keyword or br.xm LIKE @keyword or js.fph LIKE @keyword)
GROUP BY js.fph , br.xm , br.csny, c.CardNo , cf.cfh , xz.brxzmc , js.CreateTime ,cf.zje ,cf.cfnm ,mx.dl ,c.CardNo ,cf.ks ,cf.ys,cf.pyy ,cf.brlx,mx.cfmxId,br.xb, mx.yp
");
            var paraList = new DbParameter[]
            {
                new SqlParameter("@lyyf", req.lyyf),
                new SqlParameter("@keyword", "%" + req.keyword + "%"),
                new SqlParameter("@orgId", orgId)
            };
            return FindList<tyCFMainInfo>(strSql.ToString(), paraList);
        }

        /// <summary>
        /// 门诊退药显示处方详细信息
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<tyDetailListRequest> GettyDetailInfoList(string cfh, string orgId)
        {
            var strSql = new StringBuilder(@"
SELECT DISTINCT a.cfmxId,
cf.cfh,
y.ypCode ,
y.ypmc ypmc ,
x.ypgg gg ,
y.mzcldw dw ,
a.dj dj ,
a.je je ,
y.ycmc sccj ,
ISNULL(y.jl, 0) jl ,
ISNULL(y.jldw, '') jldw ,
ISNULL(b.yfmc, '') yf ,
ISNULL(a.yl, 0) yl ,
ISNULL(a.yldw, '') yldw ,
'' yszt ,
a.cfnm cfnm,
jsmx.sl sl
FROM mz_cfmx(NOLOCK) a
INNER JOIN mz_cf(NOLOCK) cf ON cf.cfnm = a.cfnm AND cf.OrganizeId=a.OrganizeId
INNER JOIN dbo.mz_jsmx(NOLOCK) jsmx ON jsmx.cf_mxnm=a.cfmxId AND jsmx.OrganizeId=cf.OrganizeId
INNER JOIN dbo.mz_js(NOLOCK) js ON js.jsnm=jsmx.jsnm AND js.cxjsnm=0 AND ISNULL(js.tbz, 0) = 0 AND js.jszt=1 AND js.OrganizeId=a.OrganizeId
INNER JOIN NewtouchHIS_Base..V_S_xt_yp y ON y.ypCode = a.yp AND y.zt = '1' AND y.mzzybz = '1' AND y.OrganizeId=a.OrganizeId
INNER JOIN  NewtouchHIS_Base..V_S_xt_ypsx x ON x.ypCode = y.ypCode AND x.OrganizeId=a.OrganizeId
LEFT JOIN NewtouchHIS_Base..V_S_xt_ypyf b ON b.yfCode = a.yfdm  --药品用法
WHERE cf.cfh = @cfh
AND a.OrganizeId=@OrganizeId
ORDER BY a.cfmxId
");
            var paraList = new DbParameter[]
            {
                new SqlParameter("@cfh", cfh),
                new SqlParameter("@OrganizeId", orgId)
            };
            return FindList<tyDetailListRequest>(strSql.ToString(), paraList);
        }

        /// <summary>
        /// 门诊发药查询页面 查询主表信息
        /// </summary>
        /// <param name="req"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<fyQueryMainInfo> GetfyQueryInfoList(searchFyInfoReqVO req, string orgId)
        {
            var strSql = new StringBuilder(@"
SELECT DISTINCT mx.cfmxId ,mx.yp ,cf.lyyf ,cf.cfnm ,js.fph ,cf.cfh ,br.xm ,c.CardNo kh ,
CAST(FLOOR(DATEDIFF(DY, br.csny, GETDATE()) / 365.25) AS INT) nl ,
xz.brxzmc ,cf.fyrq AS fyrq ,cf.fyry AS fyry ,ry.Name AS fyrymc ,cf.lyck AS fyck ,cf.jsrq AS sfrq ,ks.Name ksmc
FROM dbo.mz_cf(NOLOCK) cf
INNER JOIN mz_cfmx(NOLOCK) mx ON mx.cfnm = cf.cfnm AND mx.OrganizeId=cf.OrganizeId
INNER JOIN mz_jsmx(NOLOCK) jsmx ON jsmx.cf_mxnm = mx.cfmxId AND jsmx.OrganizeId=cf.OrganizeId
INNER JOIN mz_js(NOLOCK) js ON js.jsnm = jsmx.jsnm AND js.OrganizeId=cf.OrganizeId AND js.cxjsnm=0 AND ISNULL(js.tbz, 0) = 0 AND js.jszt=1
LEFT JOIN xt_brjbxx(NOLOCK) br ON br.patid = cf.patid AND br.OrganizeId=cf.OrganizeId
INNER JOIN dbo.xt_card(NOLOCK) c ON c.patid = br.patid AND cf.OrganizeId=cf.OrganizeId
LEFT JOIN xt_brxz(NOLOCK) xz ON xz.brxz = cf.brxz AND br.zt = '1' AND xz.OrganizeId=cf.OrganizeId
LEFT JOIN [NewtouchHIS_Base]..V_S_Sys_Department ks ON ks.Code = cf.ks and ks.OrganizeId = cf.OrganizeId
LEFT JOIN [NewtouchHIS_Base]..V_S_Sys_Staff ry ON ry.gh = cf.fyry AND ry.OrganizeId=cf.OrganizeId
WHERE cf.lyyf = @lyyf
AND cf.OrganizeId=@OrganizeId
AND cf.fyrq >= @starTime
AND cf.fyrq < @endTime
AND ISNULL(c.CardNo,'') LIKE @kh
AND ISNULL(br.xm, '') LIKE @xm
AND ISNULL(js.fph, '') LIKE @fph
");
            var paraList = new DbParameter[]
            {
                new SqlParameter("@lyyf", req.yfbmcode),
                new SqlParameter("@starTime", req.begindate),
                new SqlParameter("@endTime", req.enddate),
                new SqlParameter("@kh", "%" + req.kh + "%"),
                new SqlParameter("@xm", "%" + req.xm + "%"),
                new SqlParameter("@fph", "%" + req.fph + "%"),
                new SqlParameter("@OrganizeId", orgId)
            };
            return FindList<fyQueryMainInfo>(strSql.ToString(), paraList);
        }

        /// <summary>
        /// 门诊发药查询显示药品详细信息
        /// </summary>
        /// <param name="cfnm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<fyDetailListRequest> GetfyQueryDetailInfoList(string cfnm, string orgId)
        {
            var strSql = new StringBuilder(@"
SELECT DISTINCT cf.cfh, 
a.cfnm,
a.cfmxId ,
a.yp ypCode,
ypmc ,
c.ypgg gg,
b.ycmc ,
b.mzcldw dw,
a.dj ,
b.pfj je,
b.jl ,
b.jldw ,
ISNULL(yf.yfmc, '') yfmc,
ISNULL(a.yl, 0) yl,
ISNULL(a.yldw, '') yldw,
'' yszt,
ISNULL(jsmx.sl, 0) sl
FROM dbo.mz_cfmx(NOLOCK) a
INNER JOIN dbo.mz_cf(NOLOCK) cf ON cf.cfnm=a.cfnm AND cf.OrganizeId=a.OrganizeId
INNER JOIN dbo.mz_jsmx(NOLOCK) jsmx ON jsmx.cf_mxnm=a.cfmxId AND jsmx.OrganizeId=a.OrganizeId
INNER JOIN dbo.mz_js(NOLOCK) js ON js.jsnm=jsmx.jsnm AND js.cxjsnm=0 AND ISNULL(js.tbz, 0) = 0 AND js.jszt=1 AND js.OrganizeId=a.OrganizeId
INNER JOIN NewtouchHIS_Base..V_S_xt_yp b ON a.yp = b.ypCode AND b.OrganizeId=a.OrganizeId AND b.zt='1'
INNER JOIN NewtouchHIS_Base..V_S_xt_ypsx c ON c.ypId = b.ypId AND c.OrganizeId=a.OrganizeId AND c.zt='1'
LEFT JOIN NewtouchHIS_Base..V_S_xt_ypyf yf ON yf.yfCode = a.yfdm AND yf.zt='1'
WHERE a.cfnm = @cfnm
AND a.OrganizeId=@OrganizeId
");
            var paraList = new DbParameter[]
            {
                new SqlParameter("@cfnm", cfnm),
                new SqlParameter("@OrganizeId", orgId)
            };
            return FindList<fyDetailListRequest>(strSql.ToString(), paraList);
        }

        /// <summary>
        /// 已结算处方明细查询
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fph"></param>
        /// <param name="kh"></param>
        /// <param name="yfCode">用法代码</param>
        /// <returns></returns>
        public List<OutpatientSettledRpQueryResponseDTO> OutpatientSettledRpQuery(string organizeId, DateTime kssj, DateTime jssj, string fph, string kh, string yfCode,string mzh)
        {
            const string sql = @"
SELECT cf.cfnm, cfmx.cfmxId, js.jsnm, jsmx.jsmxnm, gh.xm, gh.kh,gh.mzh, js.CreateTime sfsj,cf.cfh, yp.ypCode, yp.ypmc, ypsx.ypgg,
 cfmx.yfCode,cfmx.pcCode,pc.zxcs,js.fph
,cfmx.yl,cfmx.yldw,CONCAT(cfmx.yl,'',cfmx.yldw) ylstr
,cfmx.sl,cfmx.dw,CONCAT(cfmx.sl,'',cfmx.dw) slstr
,cfmx.jl,cfmx.jldw,CONCAT(cfmx.jl,'',cfmx.jldw) jlstr
FROM dbo.mz_cfmx(NOLOCK) cfmx 
INNER JOIN dbo.mz_cf(NOLOCK) cf ON cf.cfnm=cfmx.cfnm AND cf.OrganizeId=cfmx.OrganizeId AND cf.zt='1'
INNER JOIN dbo.mz_jsmx(NOLOCK) jsmx ON jsmx.cf_mxnm=cfmx.cfmxId AND jsmx.OrganizeId=cfmx.OrganizeId AND jsmx.zt='1' AND jsmx.sl>0
INNER JOIN dbo.mz_js(NOLOCK) js ON js.jsnm=jsmx.jsnm AND js.OrganizeId=cfmx.OrganizeId AND js.zt='1' AND js.jszt=1 AND ISNULL(js.tbz, 0) = 0 AND js.cxjsnm=0
INNER JOIN dbo.mz_gh(NOLOCK) gh ON gh.ghnm=js.ghnm AND gh.OrganizeId=cfmx.OrganizeId AND gh.zt='1' 
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=cfmx.yp AND yp.OrganizeId=cfmx.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=cfmx.OrganizeId
Left join NewtouchHIS_Base.dbo.xt_yzpc pc on pc.yzpccode=cfmx.pcCode and pc.zt=1
WHERE cf.OrganizeId=@OrganizeId
AND js.CreateTime BETWEEN @kssj AND @jssj
AND (js.fph=@fph OR ''=@fph)
AND (gh.kh=@kh OR ''=@kh)
AND (gh.mzh=@mzh OR ''=@mzh)
AND cfmx.yfCode in (select col from f_split(@yfCode,',') where col>'') ";
            var param = new DbParameter[] {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@kssj", kssj),
                new SqlParameter("@jssj", jssj),
                new SqlParameter("@fph", fph??""),
                new SqlParameter("@kh", kh??""),
                new SqlParameter("@mzh", mzh??""),
                new SqlParameter("@yfCode", yfCode??"")
            };
            return FindList<OutpatientSettledRpQueryResponseDTO>(sql, param);
        }

        /// <summary>
        /// 获取已结算药品
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="cfnm"></param>
        /// <param name="yp"></param>
        /// <param name="sl"></param>
        /// <param name="czh">成组号</param>
        public void OutpatientDrugWithdrawalNotify(string organizeId, int cfnm, string yp, decimal sl, string czh)
        {
            const string sql = @"
select jsmx.jsmxnm 
FROM mz_cf(NOLOCK) cf
inner join mz_cfmx(NOLOCK) cfmx ON cfmx.cfnm = cf.cfnm and cfmx.OrganizeId = cf.OrganizeId
inner join mz_jsmx(NOLOCK) jsmx ON jsmx.cf_mxnm = cfmx.cfmxId and jsmx.OrganizeId = cf.OrganizeId
inner join mz_js(NOLOCK) js ON js.jsnm = jsmx.jsnm and js.OrganizeId = jsmx.OrganizeId
where cf.zt = '1' and cfmx.zt = '1' and jsmx.zt = '1' and js.zt = '1' 
AND isnull(js.tbz, 0) = 0 AND cfmx.yp = @yp and cf.cfnm = @cfnm 
AND cf.OrganizeId = @orgId AND ISNULL(cfmx.czh,'')=@czh
";
            var param = new DbParameter[] {
                new SqlParameter("@orgId", organizeId),
                new SqlParameter("@cfnm", cfnm),
                new SqlParameter("@yp", yp),
                new SqlParameter("@czh", czh??"")
            };
            var jsmxnmList = FindList<int>(sql, param);
            if (jsmxnmList.Count > 1)
            {
                throw new FailedException("", "结算明细不明确");
            }

            if (jsmxnmList.Count == 0)
            {
                throw new FailedException("", string.Format("根据药品编码【{0}】、处方内码【{1}】未找到有效数据", yp, cfnm));
            }
            var jsmxnm = jsmxnmList[0];

            var jsmxEntity = _outpatientSettlementDetailRepo.IQueryable(p => p.jsmxnm == jsmxnm).First();
            jsmxEntity.ktsl = (jsmxEntity.ktsl ?? 0) + sl;
            if (jsmxEntity.ktsl > jsmxEntity.sl)
            {
                throw new FailedException("", string.Format("可退数量异常，数量：{0}，原可退数量：{1}，请求新退数量:{2}", jsmxEntity.sl, (jsmxEntity.ktsl - sl), sl));
            }
            _outpatientSettlementDetailRepo.Update(jsmxEntity);
        }

    }
}
