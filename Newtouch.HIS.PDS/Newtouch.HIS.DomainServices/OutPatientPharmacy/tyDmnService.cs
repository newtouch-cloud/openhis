using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy;
using Newtouch.HIS.Domain.VO;
using Newtouch.HIS.Repository;
using Newtouch.Infrastructure.TSQL;
using Newtouch.PDS.Requset.ResourcesOperate;

namespace Newtouch.HIS.DomainServices.OutPatientPharmacy
{
    /// <summary>
    /// 退药
    /// </summary>
    public class tyDmnService : DmnServiceBase, ItyDmnService
    {
        private readonly IOutpatientPrescriptionDetailBatchNumberRepo _mxph;

        public tyDmnService(IDefaultDatabaseFactory databaseFactory, bool needIoc = true) : base(databaseFactory, needIoc)
        {
        }

        /// <summary>
        /// 查询可退药品信息
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<RefundedDrugVo> SelectRefundedDrugs(string cfh, string yfbmCode, string organizeId)
        {
            const string sql = @"
SELECT @cfh cfh, b.ypCode,b.ypmc,b.gg,b.ph,b.pc,CONVERT(NUMERIC(11,4),b.zxdwdj*b.zhyz) dj, CONVERT(NUMERIC(11,2), b.zxdwdj*b.zxdwsl) je, dbo.f_getyfbmDw(@yfbmCode, b.ypCode, @OrganizeId) dw, b.ycmc,b.czh,
CONVERT(INT,b.zhyz) zhyz, CONVERT(INT, b.zxdwsl/b.zhyz) ktsl,dbo.f_getComplexYpSlandDw(b.zxdwsl, b.zhyz,b.dw, yp.zxdw) slstr 
FROM (
	SELECT a.ypCode,a.ypmc,SUM(a.zxdwsl) zxdwsl,dbo.f_getyfbmDw(@yfbmCode, a.ypCode, @OrganizeId) dw,a.gg,a.ph,a.pc,a.zxdwdj,dbo.f_getyfbmZhyz(@yfbmCode, a.ypCode, @OrganizeId) zhyz,a.ycmc,a.czh 
	FROM (
		SELECT mxph.yp ypCode, cfmx.ypmc, mxph.pc, mxph.ph, cfmx.gg, cfmx.dj zxdwdj,
		mxph.sl zxdwsl, cfmx.ycmc, cfmx.czh
		FROM dbo.mz_cfmxph(NOLOCK) mxph
		INNER JOIN dbo.mz_cfmx(NOLOCK) cfmx ON cfmx.cfh=mxph.cfh AND cfmx.ypCode=mxph.yp AND cfmx.OrganizeId=mxph.OrganizeId AND cfmx.zt='1'
		INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm yfbm ON yfbm.yfbmCode=mxph.fyyf AND yfbm.OrganizeId = mxph.OrganizeId AND yfbm.zt = '1'
		WHERE mxph.OrganizeId=@OrganizeId
		AND mxph.cfh=@cfh
		AND mxph.zt='1'
		AND mxph.gjzt='0'
		AND mxph.fyyf=@yfbmCode
		UNION ALL
		SELECT tymx.ypCode, cfmx2.ypmc, tymx.pc, tymx.ph, cfmx2.gg, cfmx2.dj zxdwdj,
		-1*tymx.sl zxdwsl, cfmx2.ycmc, cfmx2.czh
		FROM dbo.mz_tfmx(NOLOCK) tymx
		INNER JOIN dbo.mz_cfmx(NOLOCK) cfmx2 ON cfmx2.cfh=tymx.cfh AND cfmx2.ypCode=tymx.ypcode AND cfmx2.OrganizeId = tymx.OrganizeId AND cfmx2.zt='1'
		WHERE tymx.cfh=@cfh 
		AND tymx.OrganizeId=@OrganizeId
		AND tymx.zt='1'
	) a
	GROUP BY a.ypCode,a.ypmc,a.gg,a.ph,a.pc,a.zxdwdj,a.ycmc,a.czh 
)b
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=b.ypCode AND yp.OrganizeId=@OrganizeId 
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=@OrganizeId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@OrganizeId",organizeId),
                new SqlParameter("@cfh",cfh),
                new SqlParameter("@yfbmCode",yfbmCode)
            };
            return FindList<RefundedDrugVo>(sql, param);
        }

        /// <summary>
        /// 查询全退可退药品信息
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<RefundedDrugVo> SelectCompleteRefundedDrugs(string cfh, string yfbmCode, string organizeId)
        {
            const string sql = @"
SELECT @cfh cfh, b.ypCode,b.ypmc,b.gg,b.ph,b.pc,CONVERT(NUMERIC(11,4),b.zxdwdj*b.zhyz) dj, CONVERT(NUMERIC(11,2), b.zxdwdj*b.zxdwsl) je, dbo.f_getyfbmDw(@yfbmCode, b.ypCode, @OrganizeId) dw, b.ycmc,b.czh,
CONVERT(INT,b.zhyz) zhyz, CONVERT(INT, b.zxdwsl/b.zhyz) sl, CONVERT(INT, b.zxdwsl/b.zhyz) ktsl,dbo.f_getComplexYpSlandDw(b.zxdwsl, b.zhyz,b.dw, yp.zxdw) slstr 
FROM (
	SELECT a.ypCode,a.ypmc,SUM(a.zxdwsl) zxdwsl,dbo.f_getyfbmDw(@yfbmCode, a.ypCode, @OrganizeId) dw,a.gg,a.ph,a.pc,a.zxdwdj,dbo.f_getyfbmZhyz(@yfbmCode, a.ypCode, @OrganizeId) zhyz,a.ycmc,a.czh 
	FROM (
		SELECT mxph.yp ypCode, cfmx.ypmc, mxph.pc, mxph.ph, cfmx.gg, cfmx.dj zxdwdj,
		mxph.sl zxdwsl, cfmx.ycmc, cfmx.czh
		FROM dbo.mz_cfmxph(NOLOCK) mxph
		INNER JOIN dbo.mz_cfmx(NOLOCK) cfmx ON cfmx.cfh=mxph.cfh AND cfmx.ypCode=mxph.yp AND cfmx.OrganizeId=mxph.OrganizeId AND cfmx.zt='1'
		INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm yfbm ON yfbm.yfbmCode=mxph.fyyf AND yfbm.OrganizeId = mxph.OrganizeId AND yfbm.zt = '1'
		WHERE mxph.OrganizeId=@OrganizeId
		AND mxph.cfh=@cfh
		AND mxph.zt='1'
		AND mxph.gjzt='0'
		AND mxph.fyyf=@yfbmCode
        AND ISNULL(mxph.czh,'')=ISNULL(cfmx.czh,'')
        AND mxph.cfmxId=cfmx.Id
		UNION ALL
		SELECT tymx.ypCode, cfmx2.ypmc, tymx.pc, tymx.ph, cfmx2.gg, cfmx2.dj zxdwdj,
		-1*tymx.sl zxdwsl, cfmx2.ycmc, cfmx2.czh
		FROM dbo.mz_tfmx(NOLOCK) tymx
		INNER JOIN dbo.mz_cfmx(NOLOCK) cfmx2 ON cfmx2.cfh=tymx.cfh AND cfmx2.ypCode=tymx.ypcode AND cfmx2.OrganizeId = tymx.OrganizeId AND cfmx2.zt='1'
		WHERE tymx.cfh=@cfh 
		AND tymx.OrganizeId=@OrganizeId
		AND tymx.zt='1'
	) a
	GROUP BY a.ypCode,a.ypmc,a.gg,a.ph,a.pc,a.zxdwdj,a.ycmc,a.czh 
)b
INNER JOIN NewtouchHIS_Base.dbo.V_C_xt_yp yp ON yp.ypCode=b.ypCode AND yp.OrganizeId=@OrganizeId AND yp.zt='1'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@OrganizeId",organizeId),
                new SqlParameter("@cfh",cfh),
                new SqlParameter("@yfbmCode",yfbmCode)
            };
            return FindList<RefundedDrugVo>(sql, param);
        }

        /// <summary>
        /// 查询药品可退数量
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns>最小单位可退数量</returns>
        public int SelectRefundedSl(string ypCode, string pc, string ph, string yfbmCode, string organizeId)
        {
            const string sql = @"
SELECT CONVERT(INT,ISNULL(SUM(a.sl),0)) zxdwsl FROM (
	SELECT mxph.sl
	FROM dbo.mz_cfmx(NOLOCK) cfmx
	INNER JOIN dbo.mz_cfmxph(NOLOCK) mxph ON mxph.yp=cfmx.ypCode AND mxph.cfh = cfmx.cfh AND mxph.OrganizeId = cfmx.OrganizeId AND mxph.zt='1' AND mxph.gjzt='0'
	WHERE cfmx.ypCode=@ypCode AND cfmx.zt='1' AND cfmx.OrganizeId=@OrganizeId
	AND mxph.fyyf=@yfbmCode AND mxph.ph=@ph AND mxph.pc=@pc
	UNION ALL
	SELECT -1*tymx.sl sl
	FROM dbo.mz_tfmx(NOLOCK) tymx
	WHERE tymx.ph=@ph AND tymx.pc=@pc
	AND tymx.OrganizeId=@OrganizeId AND tymx.ypCode=@ypCode
	AND tymx.cfh=@cfh AND tymx.zt='1'
) a
";
            var param = new DbParameter[]
            {
                new SqlParameter("@ypCode", ypCode),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@yfbmCode", yfbmCode),
                new SqlParameter("@pc", pc),
                new SqlParameter("@ph", ph)
            };
            return FirstOrDefaultNoLog<int>(sql, param);
        }

        /// <summary>
        /// 线程锁
        /// </summary>
        public static object ThreadLocker = new object();

        /// <summary>
        /// 门诊退药
        /// </summary>
        /// <param name="tyInfo"></param>
        /// <param name="returnDrugBillNo"></param>
        /// <returns></returns>
        public string ReturnDrug(tyInfo tyInfo, out string returnDrugBillNo)
        {
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                returnDrugBillNo = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(1, 100);
                var trdbn = returnDrugBillNo;
                var errorMsg = new StringBuilder();
                var cts = new CancellationTokenSource();
                var parent = new Task(() =>
                {
                    var taskFactory = new TaskFactory<bool>(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
                    tyInfo.tyDrugDetail.ForEach(p =>
                    {
                        taskFactory.StartNew(() =>
                        {
                            try
                            {
                                if (cts.Token.IsCancellationRequested)
                                {
                                    return default(bool);
                                }

                                var excResult = new DispensingDmnService(new DefaultDatabaseFactory(), false)
                                    .OutpatientReturnDrugAddStock(p.ypCode, p.ph, p.pc, p.sl * p.zhyz, tyInfo.yfbmCode,
                                        tyInfo.cfh, tyInfo.organizeId, tyInfo.userCode, trdbn);
                                if (string.IsNullOrWhiteSpace(excResult)) return true;
                                lock (ThreadLocker)
                                {
                                    errorMsg.Append(errorMsg + ";");
                                }
                                throw new Exception(excResult);
                            }
                            catch (Exception e)
                            {
                                cts.Cancel();
                                lock (ThreadLocker)
                                {
                                    errorMsg.Append(e.Message + ";");
                                }
                                return false;
                            }
                        }, cts.Token);
                    });
                }, cts.Token);

                parent.Start();
                parent.Wait();
                if (cts.IsCancellationRequested || errorMsg.ToString().Length > 0)
                {
                    return errorMsg.ToString();
                }
                db.Commit();
                return "";
            }
        }

        /// <summary>
        /// 门诊退药
        /// </summary>
        /// <param name="tyInfo"></param>
        /// <param name="returnDrugBillNo"></param>
        /// <returns></returns>
        public string ReturnDrugSingleThread(tyInfo tyInfo, out string returnDrugBillNo)
        {
            returnDrugBillNo = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(1, 100);
            var trdbn = returnDrugBillNo;
            try
            {
                using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
                {
                    foreach (var excResult in tyInfo.tyDrugDetail.Select(p => new DbParameter[]
                    {
                        new SqlParameter("@ypCode", p.ypCode),
                        new SqlParameter("@yfbmCode", tyInfo.yfbmCode),
                        new SqlParameter("@OrganizeId", tyInfo.organizeId),
                        new SqlParameter("@ph", p.ph),
                        new SqlParameter("@pc", p.pc),
                        new SqlParameter("@cfh", tyInfo.cfh),
                        new SqlParameter("@tysl", p.sl * p.zhyz), //最小单位数量
                        new SqlParameter("@userCode", tyInfo.userCode),
                        new SqlParameter("@returnDrugBillNo", trdbn)
                    }).Select(param => db.FirstOrDefault<string>(TSqlOutpatient.mz_rp_reture_drug, param)).Where(excResult => !string.IsNullOrWhiteSpace(excResult)))
                    {
                        throw new Exception(excResult);
                    }
                    db.Commit();
                    return "";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 根据处方号获取有效的处方
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<MzCfEntity> SelectRpList(string cfh, string organizeId)
        {
            const string sql = "SELECT * FROM dbo.mz_cf(NOLOCK) WHERE cfh=@cfh AND OrganizeId=@OrganizeId AND zt='1'";
            var param = new DbParameter[]
            {
                new SqlParameter("@cfh",cfh ),
                new SqlParameter("@OrganizeId",organizeId )
            };
            return FindListNoLog<MzCfEntity>(sql, param);
        }

        /// <summary>
        /// 门诊部分退药
        /// </summary>
        /// <param name="item"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public string OutpatientPartReturn(ReturnItemData item, string organizeId)
        {
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                if (item == null) return "退药内容不能为空";
                var mxphDmnService = new OutpatientPrescriptionDetailBatchNumberDmnService(new DefaultDatabaseFactory());
                var mxphList = mxphDmnService.GetList(item.cfh, organizeId, item.ypdm);
                if (mxphList == null || mxphList.Count == 0) return "";
                var tysl = (decimal)item.sl * item.zhyz;
                if (tysl > mxphList.Sum(p => p.sl)) return "退药数了不能大于冻结数";
                var sysl = tysl;
                while (sysl > 0 && mxphList.Count > 0)
                {
                    var p = mxphList[0];
                    if (p.sl > sysl)
                    {
                        p.sl -= sysl;
                        sysl = 0;
                    }
                    else
                    {
                        sysl -= p.sl;
                        p.sl = 0;
                        p.gjzt = "1";
                    }
                    new OutpatientPrescriptionDetailBatchNumberRepo(new DefaultDatabaseFactory()).Update(p);
                    mxphList.Remove(p);
                }
                db.Commit();
                return "";
            }
        }
    }
}