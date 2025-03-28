using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using Newtouch.HIS.Sett.Request.SelfService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.DomainServices.OutpatientManage
{
	/// <summary>
	/// 自助机（接口）相关
	/// </summary>
	public class SelfServiceDmnService: DmnServiceBase, ISelfServiceDmnService
	{
		public SelfServiceDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
		{
		}

		public zzjCardInfoVO queryAppCardInfo(queryAppCardInfoReqDTO dto)
		{
			var sql = @"SELECT  CONVERT(VARCHAR(50),a.zjh) ID_NO ,
        CONVERT(VARCHAR(50),b.CardNo) CARD_NO ,
        CONVERT(VARCHAR(50),a.patid) PATIENT_ID ,
        a.xm PATIENT_NAME ,
        '1' CARD_TYPE ,
        '医保卡' CARD_TYPENAME ,
        CONVERT(VARCHAR(50),a.jjlldh) PHONE_NUMBER ,
        NULL STATUS ,
        NULL TOTAL_FEE ,
        NULL QUERY_TIME
FROM    [NewtouchHIS_Sett].[dbo].[xt_brjbxx] a
        LEFT JOIN [NewtouchHIS_Sett].[dbo].[xt_card] b ON b.OrganizeId = a.OrganizeId
                                                          AND b.patid = a.patid AND b.zt = '1'
WHERE   b.CardNo = @CardNo AND a.zt='1'
        AND b.OrganizeId = @orgId ";
			return this.FirstOrDefault<zzjCardInfoVO>(sql,
				new[] { new SqlParameter("@orgId", dto.OrganizeId), new SqlParameter("@CardNo", dto.CARD_NO) });
		}
		/// <summary>
		/// 查取门诊费用主表Patinfo
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		public OutpfeeMasterInfoVO queryOutpfeeMasterInfo(queryOutpfeeMasterInfoReqDTO dto)
		{
			OutpfeeMasterInfoVO resInfoVo = new OutpfeeMasterInfoVO();
			var sql = @"SELECT  CONVERT(VARCHAR(50),a.zjh) ID_NO ,
        CONVERT(VARCHAR(50),a.patid) PATIENT_ID ,
        a.xm PATIENT_NAME ,
        CONVERT(VARCHAR(50),a.jjlldh) PHONE_NUMBER ,
        CONVERT(VARCHAR(50),a.xb) GENDER,
		CONVERT(VARCHAR(50),FLOOR(datediff(DY,a.csny,getdate())/365.25)) AGE,
		CONVERT(VARCHAR(10),a.csny,120) BIRTHDAY
FROM    [NewtouchHIS_Sett].[dbo].[xt_brjbxx] a
        INNER JOIN [NewtouchHIS_Sett].[dbo].[xt_card] b ON b.OrganizeId = a.OrganizeId
                                                          AND b.patid = a.patid
                                                           AND b.zt='1'
WHERE   a.zt='1' 
        AND b.CardNo = @CardNo
        AND b.OrganizeId = @orgId ";
			var patinfo= this.FirstOrDefault<OutpfeeMasterPatInfo>(sql,
				new[] { new SqlParameter("@orgId", dto.OrganizeId), new SqlParameter("@CardNo", dto.CARD_NO) });
			resInfoVo.ID_NO = patinfo.ID_NO;
			resInfoVo.PATIENT_ID = patinfo.PATIENT_ID;
			resInfoVo.PATIENT_NAME = patinfo.PATIENT_NAME;
			resInfoVo.PHONE_NUMBER = patinfo.PHONE_NUMBER;
			resInfoVo.GENDER = patinfo.GENDER;
			resInfoVo.AGE = patinfo.AGE;
			resInfoVo.BIRTHDAY = patinfo.BIRTHDAY;
			resInfoVo.ITEMS = queryOutpfeeMasterInfoList(dto);
			return resInfoVo;
		}
		/// <summary>
		/// 查取门诊费用主表List
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		public List<OutpfeeMasterInfoList> queryOutpfeeMasterInfoList(queryOutpfeeMasterInfoReqDTO dto)
		{
			var sql = @"SELECT  d.mzh VISIT_NO ,
        e.Name DEPT_NAME ,
        CONVERT(VARCHAR(50),b.CardNo) CARD_NO ,
        d.mzh OUTPATIENT_NO ,
        CONVERT(VARCHAR(50),a.jsnm) RECEIPT_NO ,
        CONVERT(VARCHAR(50),a.jsnm) BUSSINESS_NO ,
        a.fph INVOICE_NO ,
        '普通门诊' INVOICE_TYPE ,
        '医保' INSUR_TYPE ,
        '0' CARD_TYPE ,
        CONVERT(VARCHAR(50),b.CardNo) INSUR_CARD_NO ,
        CONVERT(VARCHAR(50),a.jsnm) INSUR_RCPT_NO ,
        CONVERT(VARCHAR(50),a.zje) ACCOUNT_SUM ,
        CONVERT(VARCHAR(50),a.zje - a.xjzf) INSUR_PAY ,
        CONVERT(VARCHAR(50),c.prm_yka065) ACCOUNT_PAY ,
        CONVERT(VARCHAR(50),a.xjzf) PERSONAL_PAY ,
        '0.00' DISEASE_FUND ,
        CONVERT(VARCHAR(50),c.prm_yke030) INJURY_FUND ,
        '0.00' FERTILITY_FUND ,
        CONVERT(VARCHAR(50),c.prm_yka056) MYSELF_PAY ,
        CONVERT(VARCHAR(50),c.prm_yka111) CLASS_SELF_PAY ,
        CONVERT(VARCHAR(50),c.prm_akc087) INSUR_ACCOUNT ,
        CONVERT(VARCHAR(10), a.jzsj, 120) SETTLEMENT_DATE ,
        CONVERT(VARCHAR(50),a.jsnm) ORDER_NO ,
        '1' PAY_CHANNEL
FROM    NewtouchHIS_Sett..mz_js a
        INNER JOIN NewtouchHIS_Sett..xt_card b ON b.OrganizeId = a.OrganizeId
                                                  AND b.patid = a.patid
        INNER JOIN NewtouchHIS_Sett..mz_js_gaybjyfy c ON c.jsnm = a.jsnm
                                                         AND c.OrganizeId = a.OrganizeId
                                                         AND c.zt = '1'
        INNER JOIN NewtouchHIS_Sett..mz_gh d ON d.ghnm = a.ghnm
                                                AND d.OrganizeId = a.OrganizeId
                                                AND d.zt = '1'
        LEFT JOIN NewtouchHIS_Base..Sys_Department e ON e.Code = d.ks
                                                        AND e.OrganizeId = d.OrganizeId
                                                        AND e.zt = '1'
WHERE   b.CardNo = @CardNo
        AND b.OrganizeId = @orgId
        AND a.jzsj >= @StartDate
        AND a.jzsj < DATEADD(DAY, 1,CONVERT(DATE,@EndDate,120))
        AND a.zt = '1'; ";
			var pars = new List<SqlParameter>();
			pars.Add(new SqlParameter("@orgId", dto.OrganizeId));
			pars.Add(new SqlParameter("@CardNo", dto.CARD_NO));
			pars.Add(new SqlParameter("@StartDate", dto.START_DATE));
			pars.Add(new SqlParameter("@EndDate", dto.END_DATE));
			return this.FindList<OutpfeeMasterInfoList>(sql, pars.ToArray());
		}

	    /// <summary>
		/// 获取门诊结算明细费用
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		public OutpfeeDetailVO queryOutpfeeDetailInfo(queryOutpfeeDetailInfoReqDTO dto)
		{
			OutpfeeDetailVO resInfoVo = new OutpfeeDetailVO();
			var sql = @"SELECT  a.xm PATIENT_NAME ,
        CONVERT(VARCHAR(50),a.patid) PATIENT_ID ,
        CONVERT(VARCHAR(50),a.zjh) ID_NO ,
        a.jjlldh PHONE_NUMBER ,
        CONVERT(VARCHAR(10),d.ghrq,120) VISIT_DATE ,
        d.mzh VISIT_NO ,
        CONVERT(VARCHAR(10),c.jzsj,120) PAY_DATE
FROM    [NewtouchHIS_Sett].[dbo].[xt_brjbxx] a
        INNER JOIN NewtouchHIS_Sett..mz_js c ON c.patid = a.patid
                                                AND c.OrganizeId = a.OrganizeId
                                                AND c.jszt = 1
                                                AND c.zt = '1'
        INNER JOIN NewtouchHIS_Sett..mz_gh d ON d.ghnm = c.ghnm
                                                AND d.OrganizeId = c.OrganizeId
                                                AND d.zt = '1'
WHERE   a.zt = '1'
        AND c.jsnm = @RECEIPT_NO
        AND c.OrganizeId = @orgId
        AND c.jsnm NOT IN ( SELECT DISTINCT
                                    c.cxjsnm
                            FROM    NewtouchHIS_Sett..mz_js
                            WHERE   zt = '1' AND jszt = 2  );";
			var patinfo = this.FirstOrDefault<OutpfeeDetailPatInfo>(sql,
				new[] { new SqlParameter("@orgId", dto.OrganizeId), new SqlParameter("@RECEIPT_NO", dto.RECEIPT_NO) });
			resInfoVo.ID_NO = patinfo.ID_NO;
			resInfoVo.PATIENT_ID = patinfo.PATIENT_ID;
			resInfoVo.PATIENT_NAME = patinfo.PATIENT_NAME;
			resInfoVo.PHONE_NUMBER = patinfo.PHONE_NUMBER;
			resInfoVo.VISIT_DATE = patinfo.VISIT_DATE;
			resInfoVo.VISIT_NO = patinfo.VISIT_NO;
			resInfoVo.PAY_DATE = patinfo.PAY_DATE;
			resInfoVo.ITEMS = queryOutpfeeDetailInfoList(dto);
			return resInfoVo;
		}

		/// <summary>
		/// 查取门诊费用明细List
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		public IList<OutpfeeDetailList> queryOutpfeeDetailInfoList(queryOutpfeeDetailInfoReqDTO dto)
		{
			var sql = @"SELECT  CONVERT(VARCHAR(50),a.jsmxnm) M_FEIYONGID ,
        CONVERT(VARCHAR(50),a.jsmxnm) ITEM_NO ,
        CONVERT(VARCHAR(50),b.yp) ITEM_CODE ,
        c.ypmc ITEM_NAME ,
        CONVERT(VARCHAR(50),c.ypgg) ITEM_SPEC ,
        c.jldw ITEM_UNITS ,
        NULL ITEM_FORM ,
        CONVERT(VARCHAR(50),a.sl) ITEM_AMOUNT ,
        CONVERT(VARCHAR(50),b.dj) ITEM_PRICE ,
        CONVERT(VARCHAR(50),b.je) ITEM_COSTS ,
        d.dlmc ITEM_CLASS ,
        NULL INSUR_CLASS ,
        NULL MYSELF_SCALE ,
        NULL SELF_PAY ,
        NULL CLASS_SELF_PAY ,
        CONVERT(VARCHAR(50),c.ybdm) MEDICAL_INSURANCE_ITEM_CODE ,
        c.ypmc MEDICAL_INSURANCE_ITEM_NAME ,
        e.Name DOCTOR_NAME ,
        NULL PERFORM_DEPT
FROM    [NewtouchHIS_Sett].[dbo].[mz_jsmx] a
        LEFT JOIN NewtouchHIS_Sett..mz_cfmx b ON b.cfmxId = a.cf_mxnm
                                                 AND b.OrganizeId = a.OrganizeId
                                                 AND b.zt = '1'
        LEFT JOIN NewtouchHIS_Base..V_C_xt_yp c ON c.ypCode = b.yp
                                                   AND c.OrganizeId = b.OrganizeId
                                                   AND c.zt = '1'
        LEFT JOIN NewtouchHIS_Base..xt_sfdl d ON d.dlCode = c.dlCode
                                                 AND d.OrganizeId = c.OrganizeId
                                                 AND d.zt = '1'
        LEFT JOIN NewtouchHIS_Base..V_C_Sys_UserStaff e ON e.Account = b.CreatorCode
                                                           AND e.OrganizeId = b.OrganizeId
                                                           AND e.zt = '1'
WHERE   a.jsnm = @RECEIPT_NO
        AND a.OrganizeId = @orgId
        AND a.cf_mxnm IS NOT NULL
UNION ALL
SELECT  CONVERT(VARCHAR(50),a.jsmxnm) M_FEIYONGID ,
        CONVERT(VARCHAR(50),a.jsmxnm) ITEM_NO ,
        CONVERT(VARCHAR(50),b.sfxm) ITEM_CODE ,
        c.sfxmmc ITEM_NAME ,
        c.gg ITEM_SPEC ,
        c.dw ITEM_UNITS ,
        NULL ITEM_FORM ,
        CONVERT(VARCHAR(50),a.sl) ITEM_AMOUNT ,
        CONVERT(VARCHAR(50),b.dj) ITEM_PRICE ,
        CONVERT(VARCHAR(50),b.je) ITEM_COSTS ,
        d.dlmc ITEM_CLASS ,
        NULL INSUR_CLASS ,
        NULL MYSELF_SCALE ,
        NULL SELF_PAY ,
        NULL CLASS_SELF_PAY ,
        c.ybdm MEDICAL_INSURANCE_ITEM_CODE ,
        c.sfxmmc MEDICAL_INSURANCE_ITEM_NAME ,
        b.ysmc DOCTOR_NAME ,
        b.ksmc PERFORM_DEPT
FROM    [NewtouchHIS_Sett].[dbo].[mz_jsmx] a
        LEFT JOIN NewtouchHIS_Sett..mz_xm b ON b.xmnm = a.mxnm
                                               AND b.zt = '1'
        LEFT JOIN NewtouchHIS_Base..xt_sfxm c ON b.sfxm = c.sfxmCode
                                                 AND c.OrganizeId = b.OrganizeId
                                                 AND c.zt = '1'
        LEFT JOIN NewtouchHIS_Base..xt_sfdl d ON d.dlCode = c.sfdlCode
                                                 AND d.OrganizeId = c.OrganizeId
                                                 AND d.zt = '1'
WHERE   a.jsnm = @RECEIPT_NO
        AND a.OrganizeId = @orgId
        AND a.mxnm IS NOT NULL; ";
			var pars = new List<SqlParameter>();
			pars.Add(new SqlParameter("@orgId", dto.OrganizeId));
			pars.Add(new SqlParameter("@RECEIPT_NO", dto.RECEIPT_NO));
			return this.FindList<OutpfeeDetailList>(sql, pars.ToArray());
		}

		/// <summary>
		/// 获取住院费用一日清
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		public InPatientInfoVO QueryInPatientInfo(queryAppCardInfoReqDTO dto)
		{
			InPatientInfoVO resInfoVo = new InPatientInfoVO();
			var sql = @"SELECT TOP 1  CONVERT(VARCHAR(50),b.CardNo) CARD_NO ,
        CONVERT(VARCHAR(50),a.patid) PATIENT_ID ,
        CONVERT(VARCHAR(50),a.zjh) ID_NO ,
        d.Name DeptName ,
        e.bqmc WardName ,
        CONVERT(VARCHAR(50),c.BedCode) BedNo ,
        CASE WHEN c.zybz = '1' THEN '在院'
             ELSE '已出院'
        END PatStatus ,
        CONVERT(VARCHAR(10),c.ryrq,120) InHospitalDate ,
        CONVERT(VARCHAR(10),c.rqrq,120) InDate ,
        CONVERT(VARCHAR(10),c.cqrq,120) OutDate ,
        CASE WHEN c.sex = '1' THEN '男'
             WHEN c.sex = '2' THEN '女'
             ELSE '未知'
        END SexName ,
        CONVERT(VARCHAR(50),f.zhye) AccBalance,
		NULL TotalMoney,
		c.zyh
FROM    [NewtouchHIS_Sett].[dbo].[xt_brjbxx] a
        INNER JOIN [NewtouchHIS_Sett].[dbo].[xt_card] b ON b.OrganizeId = a.OrganizeId
                                                           AND b.patid = a.patid
        INNER JOIN Newtouch_CIS..zy_brxxk c ON c.blh = a.blh
                                               AND c.OrganizeId = a.OrganizeId
                                               AND c.zt = '1'
        INNER JOIN NewtouchHIS_Base..Sys_Department d ON d.Code = c.DeptCode
                                                         AND d.OrganizeId = c.OrganizeId
                                                         AND d.zt = '1'
        INNER JOIN NewtouchHIS_Base..[V_S_xt_bq] e ON e.bqCode = c.WardCode
                                                      AND e.OrganizeId = c.OrganizeId
                                                      AND e.zt = '1'
        LEFT JOIN [NewtouchHIS_Sett].[dbo].[xt_zh] f ON f.patid = a.patid
                                                        AND f.OrganizeId = a.OrganizeId
                                                        AND f.zt = '1'
WHERE   b.CardNo = @CardNo
        AND a.zt='1' 
		AND c.zybz IN ('1','2')
        AND b.OrganizeId = @orgId 
		ORDER BY c.CreateTime desc ;";
			var patinfo = this.FirstOrDefault<InPatientInfoPatInfo>(sql,
				new[] { new SqlParameter("@orgId", dto.OrganizeId), new SqlParameter("@CardNo", dto.CARD_NO) });
			if (patinfo !=null)
			{
				resInfoVo.ID_NO = patinfo.ID_NO;
				resInfoVo.PATIENT_ID = patinfo.PATIENT_ID;
				resInfoVo.DeptName = patinfo.DeptName;
				resInfoVo.WardName = patinfo.WardName;
				resInfoVo.BedNo = patinfo.BedNo;
				resInfoVo.PatStatus = patinfo.PatStatus;
				resInfoVo.InHospitalDate = patinfo.InHospitalDate;
				resInfoVo.InDate = patinfo.InDate;
				resInfoVo.OutDate = patinfo.OutDate;
				resInfoVo.SexName = patinfo.SexName;
				resInfoVo.AccBalance = patinfo.AccBalance;
				resInfoVo.TotalMoney = patinfo.TotalMoney;
				resInfoVo.ITEMS = QueryInPatientInfoList(dto.OrganizeId, patinfo.zyh);
			}
			
			return resInfoVo;
		}

		/// <summary>
		/// 查取门诊费用明细List
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		public IList<InPatientInfoList> QueryInPatientInfoList(string orgId, string zyh)
		{
			var sql = @"SELECT  b.sfxmmc ITEM_NAME ,
        NULL ITEM_FORM ,
        CONVERT(VARCHAR(50),a.sl) ITEM_AMOUNT ,
        CONVERT(VARCHAR(50),a.dj) ITEM_PRICE ,
        CONVERT(VARCHAR(50),CONVERT(NUMERIC(20,2),a.dj * a.sl)) ITEM_COSTS ,
        c.dlmc ITEM_CLASS ,
        a.jfdw ITEM_UNITS ,
        CONVERT(VARCHAR(10),a.tdrq,120) ITEM_DATE
FROM    NewtouchHIS_Sett..V_C_Sys_HbtfZyXmjfb a
        LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm b ON b.sfxmCode = a.sfxm
                                                     AND b.OrganizeId = a.OrganizeId
                                                     AND b.zt = '1'
        LEFT JOIN NewtouchHIS_Base..xt_sfdl c ON c.dlCode = a.dl
                                                 AND c.OrganizeId = a.OrganizeId
                                                 AND c.zt = '1'
WHERE   a.zyh = @zyh
        AND a.OrganizeId = @orgId
UNION ALL
SELECT  b.ypmc ITEM_NAME ,
        NULL ITEM_FORM ,
        CONVERT(VARCHAR(50),a.sl) ITEM_AMOUNT ,
        CONVERT(VARCHAR(50),a.dj) ITEM_PRICE ,
        CONVERT(VARCHAR(50),CONVERT(NUMERIC(20,2),a.dj * a.sl)) ITEM_COSTS ,
        c.dlmc ITEM_CLASS ,
        a.jfdw ITEM_UNITS ,
        CONVERT(VARCHAR(10),a.tdrq,120) ITEM_DATE
FROM    NewtouchHIS_Sett..V_C_Sys_HbtfZyYpjfb a
        LEFT JOIN NewtouchHIS_Base..V_C_xt_yp b ON b.ypCode = a.yp
                                                   AND b.OrganizeId = a.OrganizeId
                                                   AND b.zt = '1'
        LEFT JOIN NewtouchHIS_Base..xt_sfdl c ON c.dlCode = a.dl
                                                 AND c.OrganizeId = a.OrganizeId
                                                 AND c.zt = '1'
WHERE   a.zyh = @zyh
        AND a.OrganizeId = @orgId;";
			var pars = new List<SqlParameter>();
			pars.Add(new SqlParameter("@orgId", orgId));
			pars.Add(new SqlParameter("@zyh", zyh));
			return this.FindList<InPatientInfoList>(sql, pars.ToArray());
		}
	}
}
