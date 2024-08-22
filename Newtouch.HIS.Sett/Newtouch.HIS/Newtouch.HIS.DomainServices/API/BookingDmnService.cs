using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.API;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.API;
using Newtouch.HIS.Sett.Request;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.DomainServices.API
{
    public class BookingDmnService: DmnServiceBase, IBookingDmnService
    {
        public BookingDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        private readonly IMzghbookRepo _MzghbookRepo;
        private readonly IOutPatientDmnService _OutPatientDmnService;
        private readonly IOutPatientSettleDmnService _OutPatientSettleDmnService;
        private readonly IOutpatientRegistRepo _outpatientRegRepo;
        private readonly IOutpatientSettlementRepo _outpatientSettlementRepo;
        private readonly IOutpatientSettlementDetailRepo _outpatientSettlementDetailRepo;
        private readonly IOutPatientUniversalDmnService _outPatientUniversalDmnService;
        private readonly IRefundDmnService _refundService;
        private readonly ISysCashPaymentModelRepo _sysForCashPayRepository;
        private readonly IOutPatientChargeQueryDmnService _outPatienChargeQueryDmnService;
        /// <summary>
        /// 获取科室信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ks"></param>
        /// <param name="ksmc"></param>
        /// <returns></returns>
        public IList<SysDepartmentInfo> GetDepartmentInfo(string orgId,string ks,string ksmc)
        {

            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));

            string sql = @"select OrganizeId,Name as ksmc,Code as ks,mzzybz from [NewtouchHIS_Base].[dbo].[V_S_Sys_Department] where 1=1 and zt='1' and organizeId=@orgId";


            if (!string.IsNullOrEmpty(ks))
            {
                sql += " and code=@code ";
                para.Add(new SqlParameter("@code", ks));
            }
            if (!string.IsNullOrEmpty(ksmc))
            {
                sql += " and name=@name ";
                para.Add(new SqlParameter("@name", ksmc));
            }return this.FindList<SysDepartmentInfo>(sql, para.ToArray());

        }

        /// <summary>
        /// 获取医生人员信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ks"></param>
        /// <param name="ksmc"></param>
        /// <returns></returns>
        public IList<SysStaffVO> GetStaffInfo(string orgId, string gh, string staffName)
        {
                List<SqlParameter> para = new List<SqlParameter>();
                para.Add(new SqlParameter("@orgId", orgId));

                string sql = @"  select staff.organizeId,departmentCode,department.Name as departmentName,staff.Name as staffName,Gender,gh,DutyId ,duty.Name as dutyName
  from [NewtouchHIS_Base].[dbo].[Sys_Staff] staff
  left join [NewtouchHIS_Base].[dbo].[Sys_Department] department on
  department.code=staff.departmentCode
  left join [NewtouchHIS_Base].[dbo].[Sys_StaffDuty] staffduty on
  staff.id=staffduty.staffId
  left join [NewtouchHIS_Base].[dbo].[Sys_Duty] duty on
  duty.id=staffduty.dutyId 
  where 1=1 and staff.zt='1' and staff.organizeId=@orgId";
                
                if (!string.IsNullOrEmpty(gh))
                {
                    sql += " and gh=@gh ";
                    para.Add(new SqlParameter("@gh", gh));
                }
                if (!string.IsNullOrEmpty(staffName))
                {
                    sql += " and staff.Name=@staffName ";
                    para.Add(new SqlParameter("@staffName", staffName));
                }
                return this.FindList<SysStaffVO>(sql, para.ToArray());
        }

        /// <summary>
        /// 获取基本病人信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="kh"></param>
        /// <param name="zjh"></param>
        /// <param name="xm"></param>
        /// <returns></returns>
        public IList<SysPatInfoVO> GetPatInfo(string orgId,string kh, string zjh, string xm)
        {
            
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));

            string sql = @"select a.organizeId,a.patid, c.brxzmc brxz, blh,a.xm,xb,csny, zjh,dh,dz,mz,zy,b.cardno kh,b.cardtypename klx
from xt_brjbxx a with(nolock)
left join xt_card b with(nolock) on a.organizeID=b.organizeid and a.patid=b.patid and b.zt='1'
left join dbo.xt_brxz c with(nolock) on a.organizeID=c.organizeid and a.brxz=c.brxz and c.zt='1'
where a.zt='1' and a.organizeId=@orgId
";

            if(!string.IsNullOrEmpty(kh))
            {
                sql += " and b.cardno=@kh ";
                para.Add(new SqlParameter("@kh", kh));
            }
            if (!string.IsNullOrEmpty(zjh))
            {
                sql += " and a.zjh=@zjh ";
                para.Add(new SqlParameter("@zjh", zjh));
            }
            if (!string.IsNullOrEmpty(xm))
            {
                sql += " and a.xm=@xm ";
                para.Add(new SqlParameter("@xm", xm));
            }
            sql += " group by a.organizeId,a.patid, c.brxzmc, blh,xm,xb,csny, zjh,dh,dz,mz,zy,b.cardno,b.cardtypename ";
            return this.FindList<SysPatInfoVO>(sql, para.ToArray());

        }

        public IList<SysItemDetailVO> SysItemDetail(string orgId, string cateCode)
        {
            string sql = @"select CateCode,Code,Name,OrganizeId
from [NewtouchHIS_Base].[dbo].[V_C_Sys_ItemsDetail] with(nolock)
where zt='1'
";
            if (!string.IsNullOrWhiteSpace(cateCode))
            {
                sql += " and catecode=@code  ";
            }

            return FindList<SysItemDetailVO>(sql, new SqlParameter[] {new SqlParameter("@code", cateCode ?? "")});
        }


        /// <summary>
        /// 医院药品信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sfxmId"></param>
        /// <param name="sfxmCode"></param>
        /// <param name="sfxmmc"></param>
        /// <returns></returns>
        public IList<SysDrugVO> GetDrugInfo(string orgId, int ypId, string ypmc)
        {

            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));

            string sql = @" select yp.ypId,ypmc,spm,ycmc,ypsx.ypgg,jldw,jl,jx as jxCode,jxmc as jx,bzdw,bzs,isKss,lsj,zfxz,yp.YpCode 
  from [NewtouchHIS_Base].[dbo].[V_S_xt_yp] yp
  left join [NewtouchHIS_Base].[dbo].[V_S_xt_ypsx] ypsx
  on yp.ypId=ypsx.ypId 
  left join [NewtouchHIS_Base].[dbo].[V_S_xt_ypjx] ypjx
  on ypjx.jxCode=yp.jx
  where 1=1 and yp.zt='1' and yp.organizeId=@orgId";

            if (ypId != 0)
            {
                sql += " and yp.ypId=@ypId ";
                para.Add(new SqlParameter("@ypId", ypId));
            }
            if (!string.IsNullOrEmpty(ypmc))
            {
                sql += " and ypmc=@ypmc ";
                para.Add(new SqlParameter("@ypmc", ypmc));
            }
            return this.FindList<SysDrugVO>(sql, para.ToArray());

        }


        /// <summary>
        /// 获取医疗项目信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sfxmId"></param>
        /// <param name="sfxmCode"></param>
        /// <param name="sfxmmc"></param>
        /// <returns></returns>
        public IList<SysMedicalVO> GetMedicalInfo(string orgId, int sfxmId, string sfxmCode,string sfxmmc)
        {

            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));

            string sql = @"  select sfxmId,sfxmCode,sfxmmc,dj,sfdlCode,dlmc,ybdm,zfxz from [NewtouchHIS_Base].[dbo].[V_S_xt_sfxm] sfxm
  left join [NewtouchHIS_Base].[dbo].[V_S_xt_sfdl] sfdl 
  on sfxm.sfdlCode=sfdl.dlCode and sfdl.organizeId=@orgId 
  where 1=1 and sfxm.zt='1' and sfxm.organizeId=@orgId";
            
            if (sfxmId != 0) {

                sql += " and sfxmId=@sfxmId ";
                para.Add(new SqlParameter("@sfxmId", sfxmId));
            }
            if (!string.IsNullOrEmpty(sfxmCode))
            {
                sql += " and sfxmCode=@sfxmCode ";
                para.Add(new SqlParameter("@sfxmCode", sfxmCode));
            }
            if (!string.IsNullOrEmpty(sfxmmc))
            {
                sql += " and sfxmmc=@sfxmmc ";
                para.Add(new SqlParameter("@sfxmmc", sfxmmc));
            }
            return this.FindList<SysMedicalVO>(sql, para.ToArray());

        }


        /// <summary>
        /// 获取门诊疾病信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sfxmId"></param>
        /// <param name="sfxmCode"></param>
        /// <param name="sfxmmc"></param>
        /// <returns></returns>
        public IList<SysDiseaseVO> GetDiseaseInfo(string orgId, string zdCode, string zdmc,string icd10)
        {

            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));

            string sql = @"  select zdCode,zdmc,icd10 from [NewtouchHIS_Base].[dbo].[V_S_xt_zd] where 1=1 ";
            
            if (!string.IsNullOrEmpty(zdCode))
            {
                sql += " and zdCode=@zdCode ";
                para.Add(new SqlParameter("@zdCode", zdCode));
            }
            if (!string.IsNullOrEmpty(zdmc))
            {
                sql += " and zdmc=@zdmc ";
                para.Add(new SqlParameter("@zdmc", zdmc));
            }
            if (!string.IsNullOrEmpty(icd10))
            {
                sql += " and icd10=@icd10 ";
                para.Add(new SqlParameter("@icd10", icd10));
            }
            return this.FindList<SysDiseaseVO>(sql, para.ToArray());

        }


        /// <summary>
        /// 门诊排班
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="OutDate"></param>
        /// <param name="czks"></param>
        /// <param name="ysgh"></param>
        /// <param name="RegType"></param>
        /// <param name="ghpbid"></param>
        /// <param name="ScheduId"></param>
        /// <returns></returns>
        public IList<MzpbScheduleVO> GetMzpbSchedule(string orgId,string OutDate,string czks, string ysgh,string RegType,string ghpbid,string ScheduId,string FromOutDate = null,int pblx=1)
        {
            string sql = @"select convert(varchar(20),ScheduId) ScheduId,a.[OrganizeId],convert(varchar(10),[OutDate],120)[OutDate],[ysgh],
[ysxm],[czks],[czksmc],[RegType],[Title],[Period],[PeriodDesc],[TotalNum]
,[PeriodStart],[PeriodEnd],[RegFee],[IsCancel],[CancelReason],[CancelTime],
YYNum,ghpbid,IsBook,[ghlx],[zlxm],weekdd,d.sfxmmc ghxmmc,e.sfxmmc zlxmmc
from mz_ghpb_schedule a with(nolock)
left join [NewtouchHIS_Base].dbo.[V_S_xt_sfxm] d with(nolock) on a.organizeid =d.organizeid and d.zt='1' and a.ghlx=d.sfxmcode
left join [NewtouchHIS_Base].dbo.[V_S_xt_sfxm] e with(nolock) on a.organizeid =e.organizeid and d.zt='1' and a.zlxm=d.sfxmcode
where a.[OrganizeId]=@orgId and a.zt='1' and OutDate>=@limitdate and IsBook='1' and IsCancel='0' ";
            List<SqlParameter> para=new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId",orgId));
            para.Add(new SqlParameter("@limitdate", DateTime.Now.ToString("yyyy-MM-dd")));

            if (pblx == (int) EnumMzpblx.yy)
            {
                sql += " and IsBook=1 ";
            }
            if (pblx == (int)EnumMzpblx.fyy)
            {
                sql += " and IsBook=0 ";
            }
            if (!string.IsNullOrWhiteSpace(ScheduId))
            {
                sql += " and ScheduId=@Id";
                para.Add(new SqlParameter("@Id", Convert.ToDecimal(ScheduId)));
            }

            if (!string.IsNullOrWhiteSpace(OutDate))
            {
                sql += " and OutDate=@date";
                para.Add(new SqlParameter("@date", Convert.ToDateTime(OutDate)));
            }
            if (!string.IsNullOrWhiteSpace(FromOutDate))
            {
                sql += " and OutDate>=@datefrom";
                para.Add(new SqlParameter("@datefrom", Convert.ToDateTime(FromOutDate)));
            }
            if (!string.IsNullOrWhiteSpace(czks))
            {
                sql += " and czks=@czks";
                para.Add(new SqlParameter("@czks", czks));
            }
            if (!string.IsNullOrWhiteSpace(ysgh))
            {
                sql += " and ysgh=@ysgh";
                para.Add(new SqlParameter("@ysgh", ysgh));
            }
            if (!string.IsNullOrWhiteSpace(RegType))
            {
                sql += " and RegType=@RegType";
                para.Add(new SqlParameter("@RegType", RegType));
            }
            if (!string.IsNullOrWhiteSpace(ghpbid))
            {
                sql += " and ghpbid=@ghpbid";
                para.Add(new SqlParameter("@ghpbid", ghpbid));
            }

            return FindList<MzpbScheduleVO>(sql, para.ToArray());
        }


        /// <summary>
        /// 门诊处方信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cfnm"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IList<SysRecipeVO> GetRecipeInfo(string orgId, string cfnm, string mzh)
        {
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));

            string sql = @"select cfnm,e.mzh,cfh,e.kh,cfzt,cflx,cflxmc,a.ks,ksmc,dutyId,d.name as dutyName,a.ys as gh,b.name as ysxm,a.createTime,zje,a.mjzbz from  [NewtouchHIS_Sett].[dbo].[mz_cf] a
  left join [NewtouchHIS_Base].[dbo].[Sys_Staff] b
  on a.ys=b.gh 
  left join [NewtouchHIS_Base].[dbo].[Sys_StaffDuty] c 
  on b.Id=c.StaffId 
  left join [NewtouchHIS_Base].[dbo].[Sys_Duty] d
  on c.dutyId=d.id
  left join [NewtouchHIS_Sett].[dbo].[mz_gh] e
  on a.ghnm=e.ghnm
  where 1=1 and a.zt='1' and a.organizeId=@orgId";

            if (!string.IsNullOrEmpty(cfnm))
            {
                sql += " and cfnm=@cfnm ";
                para.Add(new SqlParameter("@cfnm", cfnm));
            }
            if (!string.IsNullOrEmpty(mzh))
            {
                sql += " and e.mzh=@mzh ";
                para.Add(new SqlParameter("@mzh", mzh));
            }
            return this.FindList<SysRecipeVO>(sql, para.ToArray());
        }
        /// <summary>
        /// 门诊处方明细
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cfmxId"></param>
        /// <param name="cfnm"></param>
        /// <returns></returns>

        public IList<SysRecipeDrugVO> GetRecipeDrugInfo(string orgId,string cfmxId,string cfnm) {

            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));

            string sql = @"select cfmxId,cfnm,czh,yp,ypmc,spm,bzs,bzdw,ycmc,c.jxCode,jxmc as jx,d.ypgg,dj,sl,je,tybz,a.zfxz 
from [dbo].[mz_cfmx] a
left join [NewtouchHIS_Base].[dbo].[xt_yp] b
on a.yp=b.ypCode
left join [NewtouchHIS_Base].[dbo].[V_S_xt_ypjx] c
  on c.jxCode=b.jx  left join [NewtouchHIS_Base].[dbo].[V_S_xt_ypsx] d
  on b.ypId=d.ypId
  where 1=1 and a.zt='1' and a.organizeId=@orgId";

            if (!string.IsNullOrEmpty(cfmxId))
            {
                sql += " and cfmxId=@cfmxId ";
                para.Add(new SqlParameter("@cfmxId", cfmxId));
            }
            if (!string.IsNullOrEmpty(cfnm))
            {
                sql += " and a.cfnm=@cfnm ";
                para.Add(new SqlParameter("@cfnm", cfnm));
            }
            return this.FindList<SysRecipeDrugVO>(sql, para.ToArray());
        }

        /// <summary>
        /// 预约记录查询
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="kh"></param>
        /// <param name="lxdh"></param>
        /// <param name="OutDate"></param>
        /// <param name="BookId"></param>
        /// <param name="yyzt"></param>
        /// <returns></returns>
        public IList<MzghBookRecordVO> OutbookRecord(string orgId,string kh,string xm,string lxdh,string BookId,int? yyzt,string ksrq,string jsrq,string ks,string ysgh,string appId)
        {
//            string sql = @"SELECT [OrganizeId],[BookId],[yyzt],[ScheduId],[ghxz],[patid],[xm],[xb],[lxdh],[kh],[ks],[ys],[OutDate]
//,[RegType],[Period],[PeriodStart],[PeriodEnd],[QueueNo],[RegFee],[PayFee],[PayLsh],[PayTime]
//,[CancelReason],[CancelTime],[mzh],[ghrq],[AppId],yynr
//from [mz_gh_book] a with(nolock)
//where a.zt='1' and a.OrganizeId=@orgId ";

            string sql = @"SELECT a.[OrganizeId],[BookId],
(case [yyzt] when 1 then (case when datediff(dd,getdate(),a.outdate)>=0 then '已预约' else '已作废' end) 
when 2 then '已挂号' when 3 then '预约取消' when 4 then '已作废' end)yyzt,
[ScheduId],b.brxzmc [ghxz],[patid],[xm],(case when [xb]=1 then '男' else '女' end)[xb],[lxdh],[kh],
c.name [ks],d.name [ys],[OutDate]
,e.name [RegType],[Period],[PeriodStart],[PeriodEnd],[QueueNo],[RegFee],[PayFee],[PayLsh],[PayTime]
,[CancelReason],[CancelTime],[mzh],[ghrq],[AppId],yynr
from [mz_gh_book] a with(nolock)
left join xt_brxz b with(nolock) on a.organizeid=b.organizeid and a.ghxz=b.brxz
left join [NewtouchHIS_Base].[dbo].[V_S_Sys_Department] c on a.organizeid=c.organizeid and a.ks=c.code and c.zt='1'
left join [NewtouchHIS_Base].[dbo].[V_S_Sys_Staff] d on a.ys=d.gh and a.organizeid=d.organizeid and d.zt='1'
left join [NewtouchHIS_Base].[dbo].[V_C_Sys_ItemsDetail] e with(nolock) on a.[RegType]=e.code and e.CateCode='RegType' and e.zt='1'
where a.zt='1'
";

            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));

            sql += " and a.OutDate>=@ksrq and a.OutDate<@jsrq ";
            if (!string.IsNullOrWhiteSpace(ksrq)&& !string.IsNullOrWhiteSpace(jsrq))
            {
                var jstime = Convert.ToDateTime(jsrq).AddDays(1).ToString("yyyy-MM-dd");
                
                para.Add(new SqlParameter("@ksrq", ksrq));
                para.Add(new SqlParameter("@jsrq", jstime));
            }
            else
            {
                para.Add(new SqlParameter("@ksrq", DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd")));
                para.Add(new SqlParameter("@jsrq", DateTime.Now.AddDays(15).ToString("yyyy-MM-dd")));
            }
            if (!string.IsNullOrWhiteSpace(appId))
            {
                sql += " and a.AppId=@appId ";
                para.Add(new SqlParameter("@appId", appId));
            }
            if (!string.IsNullOrWhiteSpace(kh))
            {
                sql += " and a.kh=@kh ";
                para.Add(new SqlParameter("@kh", kh));
            }
            if (!string.IsNullOrWhiteSpace(lxdh))
            {
                sql += " and a.lxdh=@lxdh ";
                para.Add(new SqlParameter("@lxdh", lxdh));
            }
            if (!string.IsNullOrWhiteSpace(BookId))
            {
                sql += " and a.BookId=@BookId ";
                para.Add(new SqlParameter("@BookId", BookId));
            }
            if (yyzt!=null)
            {
                sql += " and a.yyzt=@yyzt ";
                para.Add(new SqlParameter("@yyzt", yyzt));
            }
            if (!string.IsNullOrWhiteSpace(ks))
            {
                sql += " and a.ks=@ks ";
                para.Add(new SqlParameter("@ks", ks));
            }
            if (!string.IsNullOrWhiteSpace(ysgh))
            {
                sql += " and a.ys=@ys ";
                para.Add(new SqlParameter("@ks", ysgh));
            }

            sql += @"
order by a.outdate desc " ;
            return FindList<MzghBookRecordVO>(sql, para.ToArray());

        }
        /// <summary>
        /// 获取排班号源详情
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ScheduId"></param>
        /// <returns></returns>
        public IList<MzpbScheduleVO> GetScheduleDetail(string orgId, string ScheduId)
        {
            string sql = @"select convert(varchar(20),a.ScheduId)ScheduId,a.ghpbId,convert(varchar(10),a.OutDate,120)OutDate,a.ysgh,a.ysxm,a.czks,a.czksmc,
a.RegType,a.Title,a.Period,a.PeriodDesc,a.TotalNum,a.PeriodStart,a.PeriodEnd,
a.RegFee,a.IsBook,a.ghlx,a.zlxm,a.weekdd,a.IsCancel,a.TotalNum-count(b.bookid) BookNum
from [mz_ghpb_schedule] a with(nolock)
left join mz_gh_book b with(nolock) on a.organizeid=b.organizeid and a.scheduid=b.scheduid and b.zt=1 and b.yyzt<>@yyzt
where  a.zt=1 and a.organizeid=@orgId 
";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));
            para.Add(new SqlParameter("@yyzt", (int)EnumMzyyzt.cancel));
            if (!string.IsNullOrWhiteSpace(ScheduId))
            {
                sql += " and a.scheduId=@scheduId";
                para.Add(new SqlParameter("@scheduId", ScheduId));
            }

            sql += @" 
group by a.ScheduId,a.ghpbId,a.OutDate,a.ysgh,a.ysxm,a.czks,a.czksmc,
a.RegType,a.Title,a.Period,a.PeriodDesc,a.TotalNum,a.PeriodStart,a.PeriodEnd,
a.RegFee,a.IsBook,a.ghlx,a.zlxm,a.weekdd,a.IsCancel ";

            return FindList<MzpbScheduleVO>(sql, para.ToArray());
        }
        /// <summary>
        /// 预约申请
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="scheduId">排班日程id</param>
        /// <param name="patid"></param>
        /// <param name="kh">患者卡号</param>
        public decimal OutbookingApply(BookingRequestDto req, SysPatInfoVO patvo, MzpbScheduleVO pbvo)
        {
            //获取剩余号源
            var syhy = GetScheduleDetail(req.OrgId, pbvo.ScheduId).FirstOrDefault();
            if (syhy!=null)
            {
                if (syhy.BookNum == 0)
                {
                    return -100;
                    //throw new FailedException("该排班已无可约号源");
                }
            }
            else
            {
                return -200;
                //throw new FailedException("无法获取号源信息，请重试");
            }
            //申请预约
            if (req != null && patvo != null && pbvo != null)
            {
                var queno= _OutPatientDmnService.GetJzxh(req.scheduId, pbvo.czks, pbvo.ysgh, "", req.AppId, req.OrgId);
                var bookId = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_gh_book");
                MzghbookEntity yyety = new MzghbookEntity();
                yyety.BookId = bookId;
                yyety.ScheduId = Convert.ToDecimal(req.scheduId);
                yyety.kh = req.kh;
                yyety.AppId = req.AppId;
                yyety.OutDate = Convert.ToDateTime(pbvo.OutDate);
                yyety.patid = patvo.patid;
                yyety.Period = Convert.ToInt32(pbvo.Period);
                yyety.PeriodStart = pbvo.PeriodStart;
                yyety.PeriodEnd = pbvo.PeriodEnd;
                yyety.QueueNo = queno;
                yyety.ghxz = req.ghxz;
                yyety.RegType = pbvo.RegType;
                yyety.RegFee = pbvo.RegFee == null ? 0 : Convert.ToDecimal(pbvo.RegFee);
                yyety.ks = pbvo.czks;
                yyety.lxdh = req.dh ?? patvo.dh;
                yyety.ys = pbvo.ysgh;
                yyety.xm = patvo.xm;
                yyety.xb = patvo.xb;
                yyety.yynr = pbvo.Title;
                
                yyety.OrganizeId = req.OrgId;
                yyety.yyzt = ((int) EnumMzyyzt.book).ToString();


                yyety.Create();
                _MzghbookRepo.Insert(yyety);

                return bookId;

            }
            else
            {
                return -300;
                //throw new FailedException("关键信息不可为空");
            }

        }

        /// <summary>
        /// 预约挂号
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="kh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public APIOutputDto OutpatRegApply(string bookId, string kh,string orgId, string payFee, string payLsh, string user)
        {
            APIOutputDto res =new APIOutputDto();
            SysPatInfoVO patvo=new SysPatInfoVO();
            MzghbookEntity bookRecord=new MzghbookEntity();

            int patid = -1;
            if (!string.IsNullOrWhiteSpace(bookId))
            {
                string yyzt = ((int) EnumMzyyzt.book).ToString();
                DateTime datevalid=DateTime.Now.Date;
                bookRecord = _MzghbookRepo.FindEntity(p =>
                    p.BookId.ToString() == bookId && p.yyzt == yyzt && p.zt == "1" && p.OrganizeId == orgId &&
                    p.OutDate >= datevalid);
                if (bookRecord != null)
                {
                    patid = Convert.ToInt32(bookRecord.patid);
                    if (kh != bookRecord.kh)
                    {
                        res.code = 0;
                        res.msg = "卡信息异常，请确认预约信息。";
                    }

                    if (bookRecord.RegFee != Convert.ToDecimal(payFee))
                    {
                        res.code = 0;
                        res.msg = "挂号金额有误。";
                    }
                }
                else
                {
                    res.code = 0;
                    res.msg = "请确认预约状态及日期是否有效。";
                }
            }

            if (!string.IsNullOrWhiteSpace(kh))
            {
                patvo = GetPatInfo(orgId, kh, null, null).FirstOrDefault();
                if (patvo!=null)
                {
                    if (patid == -1)
                    {
                        patid = patvo.patid;
                    }
                    else if(patid!=patvo.patid)
                    {
                        res.code = 0;
                        res.msg = "预约信息与卡信息不符，请重试。";
                    }

                }
                else 
                {
                    res.code = 0;
                    res.msg = "卡信息异常，请联系管理员。";
                }
            }

            if (string.IsNullOrWhiteSpace(res.msg) && patvo!=null && bookRecord!=null)
            {
                //获取门诊号
                string mzh = _OutPatientSettleDmnService.GetRegMzh(patid, bookRecord.ScheduId.ToString(), bookRecord.ks, orgId, null,
                    bookRecord.QueueNo, bookRecord.OutDate.ToString("yyyy-MM-dd"));
                var scheduety = GetMzpbSchedule(orgId, null, null, null, null, null, bookRecord.ScheduId.ToString()).FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(mzh) && scheduety!=null)
                {
                    try
                    {
                        object regobj;
                        OutpatientSettFeeRelatedDTO feeRelated = new OutpatientSettFeeRelatedDTO();
                        feeRelated.zje = bookRecord.RegFee;
                        feeRelated.orglxjzfys = bookRecord.RegFee;
                        feeRelated.ssk = bookRecord.RegFee;
                        feeRelated.zhaoling = 0;
                        feeRelated.xjzfys = bookRecord.RegFee;
                        var scheduId = Convert.ToInt32(scheduety.ScheduId);
                        string ghly = "1";
                        SysCashPaymentModelEntity zffsEty=new SysCashPaymentModelEntity();
                        if (bookRecord.AppId == EnumMzghly.WeChat.ToString())
                        {
                            zffsEty = _sysForCashPayRepository.FindEntity(p =>
                                p.xjzffsmc.Contains("微信") && p.zt == "1");
                            ghly = ((int) EnumMzghly.WeChat).ToString();
                        }
                        else if (bookRecord.AppId == EnumMzghly.Alipay.ToString())
                        {
                            zffsEty = _sysForCashPayRepository.FindEntity(p =>
                                p.xjzffsmc.Contains("支付宝") && p.zt == "1");
                            ghly = ((int) EnumMzghly.Alipay).ToString();
                        }
                        feeRelated.zffs1 = zffsEty == null ? "" : zffsEty.xjzffs;
                        feeRelated.zfje1 = zffsEty == null ? null : bookRecord.RegFee;

                        _OutPatientSettleDmnService.Save(orgId, patid, kh, ghly, scheduety.RegType,
                            scheduety.czks, "", scheduety.czksmc, "", scheduety.ghlx, scheduety.zlxm, null, null, false,
                            false, bookRecord.QueueNo, scheduId, feeRelated, bookRecord.ghxz, null, mzh, "0", null,null,null,null,null,
                            out regobj);

                        var regEty =
                            _outpatientRegRepo.FindEntity(p => p.mzh == mzh && p.zt == "1" && p.OrganizeId == orgId);
                        if (regEty != null)
                        { 
                            regEty.ghrq = bookRecord.OutDate;
                            _outpatientRegRepo.Update(regEty);

                            bookRecord.PayFee = Convert.ToDecimal(payFee);
                            bookRecord.PayLsh = payLsh;
                            bookRecord.LastModifyTime = DateTime.Now;
                            bookRecord.LastModifierCode = user;
                            bookRecord.yyzt= ((int)EnumMzyyzt.reg).ToString();
                            bookRecord.ghrq= bookRecord.OutDate;
                            bookRecord.mzh = mzh;
                            _MzghbookRepo.Update(bookRecord);

                            res.data = regobj;
                            res.code = 1;
                            //res.msg = regobj.ToString();

                        }
                        else
                        {
                            res.code = 0;
                            res.msg = "挂号失败，请重试";
                        }

                    }
                    catch (FailedException ex)
                    {
                        res.code = 0;
                        res.msg = ex.Msg;
                    }


                }
                else
                {
                    res.code = 0;
                    res.msg = "生成门诊号失败";
                }

            }
            

            return res;
        }

        public APIOutputDto OutPatRegCancel(string orgId, string user, string mzh,string kh,string appId)
        {
            APIOutputDto res = new APIOutputDto();
            object newJszbInfo;
            string outTradeNo;
            decimal refundAmount;
            if (!string.IsNullOrWhiteSpace(orgId) && !string.IsNullOrWhiteSpace(mzh) &&
                !string.IsNullOrWhiteSpace(kh) && !string.IsNullOrWhiteSpace(user) && !string.IsNullOrWhiteSpace(appId))
            {

                var regEty = _outpatientRegRepo.FindEntity(p =>
                    p.mzh == mzh && p.zt == "1" && p.OrganizeId == orgId && p.CreatorCode == user &&
                    p.ghly != ((int) EnumMzghly.His).ToString());
                if (regEty != null)
                {
                    var isTody = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                    //待就诊 且已结
                    if (regEty.jzbz == ((int) EnumOutpatientJzbz.Djz).ToString() && regEty.ghzt == "1" &&
                        regEty.ghrq == isTody)
                    {
                        var jsEty = _outpatientSettlementRepo.FindEntity(p =>
                            p.OrganizeId == orgId && p.ghnm == regEty.ghnm &&
                            p.jslx == ((int) EnumJslx.GH).ToString() && p.jszt == (int) Constants.jsztEnum.YJ &&
                            p.tbz != 1);
                        if (jsEty != null)
                        {
                            var detail = _refundService.RefundableQuery(orgId, jsEty.jsnm);
                            Dictionary<int, decimal> dic = new Dictionary<int, decimal>();
                            if (detail.Count > 0)
                            {
                                decimal expectedTmxZje = 0;
                                foreach (var item in detail)
                                {
                                    decimal sl = 0;
                                    string jsmx = "";

                                    if (item.sl >= 0)
                                    {
                                        sl = Convert.ToDecimal(item.sl);
                                        dic.Add(item.jsmxnm, sl);
                                        expectedTmxZje += item.dj * sl;
                                    }
                                }

                                //预约挂号只能当前平台退号
                                var yyzt = ((int) EnumMzyyzt.reg).ToString();
                                var bookRecord = _MzghbookRepo.FindEntity(p =>
                                    p.OrganizeId == orgId && p.mzh == mzh && p.zt == "1" && p.yyzt == yyzt);
                                if (bookRecord != null)
                                {
                                    if (bookRecord.AppId == appId)
                                    {
                                        _outPatientUniversalDmnService.RefundSettlement(orgId, jsEty.jsnm, dic,
                                            expectedTmxZje,
                                            null, out newJszbInfo, out outTradeNo, out refundAmount, null, null,
                                            null, null, null, null);
                                        var newjsEty = _outpatientSettlementRepo.FindEntity(p =>
                                            p.OrganizeId == orgId && p.ghnm == regEty.ghnm &&
                                            p.jslx == ((int) EnumJslx.GH).ToString() &&
                                            p.jszt == (int) Constants.jsztEnum.YT &&
                                            p.tbz == 1);
                                        if (newjsEty != null)
                                        {
                                            bookRecord.yyzt = ((int) EnumMzyyzt.regcancel).ToString();
                                            bookRecord.LastModifyTime = DateTime.Now;
                                            bookRecord.LastModifierCode = user;
                                            _MzghbookRepo.Update(bookRecord);

                                            res.code = 1;
                                            res.msg = bookRecord.yynr + "退号成功";
                                            res.data = new
                                            {
                                                kh = regEty.kh,
                                                mzh = mzh,
                                                BookId = bookRecord.BookId
                                            };
                                        }
                                        else
                                        {
                                            res.code = 0;
                                            res.sub_code = 44001;
                                            res.msg = "退号失败,请重试。";
                                            res.data = new
                                            {
                                                kh = regEty.kh,
                                                mzh = mzh
                                            };
                                        }
                                    }
                                    else
                                    {
                                        res.code = 0;
                                        res.sub_code = 44008;
                                        res.msg = "挂号来源校验失败，不可退号。";
                                    }
                                }
                                else
                                {
                                    res.code = 0;
                                    res.sub_code = 44002;
                                    res.msg = "预约挂号状态异常，当前挂号不可退。";
                                }
                            }
                            else
                            {
                                res.code = 0;
                                res.sub_code = 44003;
                                res.msg = "结算明细数据异常，请至窗口查询。";
                            }

                        }
                        else
                        {
                            res.code = 0;
                            res.sub_code = 44004;
                            res.msg = "结算数据异常，请至窗口查询。";
                        }
                    }
                    else if (regEty.ghzt == "2")
                    {
                        res.code = 0;
                        res.sub_code = 44005;
                        res.msg = "当前挂号已退，请勿重复操作。";
                    }
                    else
                    {
                        res.code = 0;
                        res.sub_code = 44006;
                        res.msg = "当前挂号无法退号，请至窗口查询。（退号仅限当天未就诊已结算的挂号）";
                    }
                }
                else
                {
                    res.code = 0;
                    res.sub_code = 44007;
                    res.msg = "未找到有效可退挂号。（退号仅限当前挂号平台当天未就诊已结算的挂号）";
                }
            }
            else
            {
                res.code = 0;
                res.msg = "关键参数不可为空。";
            }

            return res;
        }

        public APIOutputDto OutpatReg(string kh, string orgId, string ghrq, string ghxz, string ks,string user,string AppId, string ghlybz)
        {
            APIOutputDto res = new APIOutputDto();
            SysPatInfoVO patvo = new SysPatInfoVO();

            int patid = -1;

            if (!string.IsNullOrWhiteSpace(kh))
            {
                patvo = GetPatInfo(orgId, kh, null, null).FirstOrDefault();
                if (patvo != null)
                {
                    patid = patvo.patid;
                }
                else
                {
                    res.code = 0;
                    res.msg = "卡信息异常，请联系管理员。";
                }
            }

            string dtnow = DateTime.Now.ToString("yyyy-MM-dd");

            var con = ConfigurationHelper.GetAppConfigValue("RegLimit");
            if (con=="1")
            {
                var reginfo = RegPermitCheck(patvo.blh, orgId, ks, dtnow);
                if (reginfo == false)
                {
                    res.code = 0;
                    res.msg = "同一卡号相同科室不允许重复挂号。";
                }
            }

            if (string.IsNullOrWhiteSpace(res.msg) && patvo != null && !string.IsNullOrWhiteSpace(ghrq) && !string.IsNullOrWhiteSpace(ks))
            {
                string mzh = "";
                int jzxh = 0;
                
                if(string.IsNullOrWhiteSpace(ghxz))
                {
                    ghxz = ((int)EnumGhxz.PTGH).ToString();
                }

                //获取排班
                var scheduety = GetMzpbSchedule(orgId, ghrq, ks, null, "1", null, null).FirstOrDefault();
                if (scheduety != null)
                {
                    //获取门诊号 就诊序号
                    var mjz = ((int)EnumOutPatientType.generalOutpat).ToString();
                    var ghready = _OutPatientSettleDmnService.GetCQMzhJzxh(patvo.patid,scheduety.ScheduId,ks,null,orgId, user, mjz,null,ghrq);
                    if(ghready!=null)
                    {
                        jzxh = ghready.Item1;
                        mzh = ghready.Item2;

                        try
                        {
                            object regobj;
                            OutpatientSettFeeRelatedDTO feeRelated = new OutpatientSettFeeRelatedDTO();
                            var scheduId = Convert.ToInt32(scheduety.ScheduId);
                            string ghly = "1";
                            SysCashPaymentModelEntity zffsEty = new SysCashPaymentModelEntity();
                            if (AppId == EnumMzghly.WeChat.ToString())
                            {
                                zffsEty = _sysForCashPayRepository.FindEntity(p => p.xjzffsmc.Contains("微信") && p.zt == "1");
                                ghly = ((int)EnumMzghly.WeChat).ToString();
                            }
                            else if (AppId == EnumMzghly.Alipay.ToString())
                            {
                                zffsEty = _sysForCashPayRepository.FindEntity(p => p.xjzffsmc.Contains("支付宝") && p.zt == "1");
                                ghly = ((int)EnumMzghly.Alipay).ToString();
                            }
                            else if (AppId == EnumMzghly.Tj.ToString())
                            {
                                zffsEty = _sysForCashPayRepository.FindEntity(p => p.xjzffsmc.Contains("体检") && p.zt == "1");
                                ghly = ((int)EnumMzghly.Tj).ToString();
                            }

                            if (ghly == ((int)EnumMzghly.Tj).ToString())
                            {
                                feeRelated.zje = 0;
                                feeRelated.orglxjzfys = 0;
                                feeRelated.ssk = 0;
                                feeRelated.zhaoling = 0;
                                feeRelated.xjzfys = 0;
                            }
                            else
                            {
                                feeRelated.zje = scheduety.RegFee;
                                feeRelated.orglxjzfys = scheduety.RegFee;
                                feeRelated.ssk = scheduety.RegFee;
                                feeRelated.zhaoling = 0;
                                feeRelated.xjzfys = scheduety.RegFee;
                            }

                            feeRelated.zffs1 = zffsEty == null ? "" : zffsEty.xjzffs;
                            feeRelated.zfje1 = zffsEty == null ? null : scheduety.RegFee;

                            _OutPatientSettleDmnService.Save(orgId, patid, kh, ghly, scheduety.RegType,
                                scheduety.czks, "", scheduety.czksmc, "", null, null, null, null, false,
                                false, jzxh, scheduId, feeRelated, ghxz, null, mzh, "0",null, null,null,null,null,
                                out regobj);

                            var regEty =
                                _outpatientRegRepo.FindEntity(p => p.mzh == mzh && p.zt == "1" && p.OrganizeId == orgId);
                            if (regEty != null)
                            {
                                if (!string.IsNullOrWhiteSpace(ghlybz) && ghlybz == "1")
                                {
                                    regEty.ghlybz = ghly;
                                    _outpatientRegRepo.Update(regEty);
                                }
                                
                                res.data = regobj;
                                res.code = 1;
                                res.msg = "挂号成功。";

                            }
                            else
                            {
                                res.code = 0;
                                res.msg = "挂号失败，请重试";
                            }

                        }
                        catch (FailedException ex)
                        {
                            res.code = 0;
                            res.msg = ex.Msg;
                        }

                    }
                    else
                    {
                        res.code = 0;
                        res.msg = "生成门诊号失败。";
                    }
                }
                else
                {
                    res.code = 0;
                    res.msg = "该科室当日无相关排班。";
                }
             

            }


            return res;

        }

        public bool RegPermitCheck(string blh,string orgId,string ks,string ghrq)
        {
            string sql = @"
            select mzh 
            from mz_gh with(nolock)
            where organizeid=@orgId and blh=@blh and ks=@ks and convert(date,ghrq)=convert(date,@ghrq) and ghzt=0 ";

            var list= this.FindList<OutPatientRegInfoVO>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@blh",blh),
                new SqlParameter("@ks",ks),
                new SqlParameter("@ghrq",ghrq),
            });

            if(list!=null && list.Count>0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public IList<OutPatientChargeInfoDto> OutpatChargeInfo(string orgId, string kh, string mzh, string AppId)
        {
            IList<OutPatientChargeInfoDto> dtoList = new List<OutPatientChargeInfoDto>();

            string sql = @"SELECT js.blh, js.jsnm,js.jslx,
       CASE WHEN len(gh.kh) >= 28 THEN substring(gh.kh,1,10) ELSE gh.kh END AS kh,
       js.xm,
	   gh.mzh,xx.sbbh,
       isnull(js.fph, '') AS fph,
       js.zje jszje,js.xjzf jsxjzf,
       ybjyfy.ybjsh, ybjyfy.JBZF,ybjyfy.GBZF,ybjyfy.JBYE,ybjyfy.GBYE,
       js.CreatorCode,
       js.CreateTime,
       ks.name AS ghksmc,
       ISNULL(CASE WHEN fpdy.oldFPH IS NOT NULL OR LEN(LTRIM(RTRIM(fpdy.oldFPH)))>0 THEN fpdy.oldFPH ELSE js.fph END, '') oldfph,
       CASE WHEN fpdy.xfph IS NOT NULL OR fpdy.dyfs=2 THEN '1' ELSE '0' END AS isxfph,
       ISNULL(js.LastModifierCode, '') tCreatorCode,
        sfyuserstaff.Name CreatorName,ghysStaff.Name ghysmc,
        isnull(js.jzsj, js.CreateTime) sfrq,
		brxz.brxzmc brxzmc
FROM dbo.mz_js(NOLOCK) js
left join mz_js_ybjyfy(nolock) ybjyfy on ybjyfy.jsnm = js.jsnm
LEFT JOIN dbo.mz_gh(NOLOCK) gh ON gh.ghnm=js.ghnm AND gh.OrganizeId=js.OrganizeId
LEFT JOIN NewtouchHIS_Base..V_S_Sys_Department ks ON ks.Code = gh.ks AND ks.zt='1' AND ks.OrganizeId=js.OrganizeId
LEFT JOIN mz_curr_fp(nolock) fpdy ON js.jsnm = fpdy.jsnm AND fpdy.zt='1' AND fpdy.OrganizeId=js.OrganizeId
LEFT JOIN NewtouchHIS_Base..V_C_Sys_UserStaff sfyuserstaff 
ON sfyuserstaff.Account = js.CreatorCode AND sfyuserstaff.zt='1' AND sfyuserstaff.OrganizeId=js.OrganizeId
LEFT JOIN NewtouchHIS_Base..V_S_Sys_Staff ghysStaff 
ON ghysStaff.gh = gh.ys AND ghysStaff.zt='1' AND ghysStaff.OrganizeId=js.OrganizeId
left join xt_brxz brxz
on brxz.brxz = gh.brxz and brxz.zt = '1' and brxz.OrganizeId = gh.OrganizeId
 LEFT JOIN dbo.xt_brjbxx xx ON xx.blh = gh.blh
                                      AND xx.OrganizeId = js.OrganizeId
                                      AND xx.zt = '1'
WHERE js.OrganizeId=@orgId and js.zt= '1'
--未退
and ISNULL(js.tbz, 0)=0  ";

            if(!string.IsNullOrWhiteSpace(kh))
            {
                sql += " and gh.kh=@kh ";
            }
            if (!string.IsNullOrWhiteSpace(mzh))
            {
                sql += " and gh.mzh=@mzh ";
            }

            var list= this.FindList<OutPatientChargeMainDto>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@kh",kh),
                new SqlParameter("@mzh",mzh),
            });

            if(list!=null && list.Count>0)
            {
                foreach (var item in list)
                {
                    OutPatientChargeInfoDto dto = new OutPatientChargeInfoDto();
                    dto.MainRecords = new List<OutPatientChargeMainDto>();
                    dto.DetailRecords = new List<OutPatientRegChargeDetailsVO>();
                    dto.MainRecords.Add(item);
                    var delist= OutpatChargeDetail(orgId,item.jsnm.ToString());
                    if(delist!=null && delist.Count>0)
                    {
                        foreach(var s in delist)
                        {
                            dto.DetailRecords.Add(s);
                        }                       
                    }

                    dtoList.Add(dto);
                }

            }

            return dtoList;

        }

        public IList<OutPatientRegChargeDetailsVO> OutpatChargeDetail(string orgId,string jsnm)
        {
            string sql = @"exec spSelectRecordsDetailByJsnm @jsnm=@jsnm,@OrganizeId=@OrganizeId ";
            return this.FindList<OutPatientRegChargeDetailsVO>(sql, new SqlParameter[] {
                new SqlParameter("@jsnm",jsnm),
                new SqlParameter("@OrganizeId",orgId)
            });
        }
    }
}
