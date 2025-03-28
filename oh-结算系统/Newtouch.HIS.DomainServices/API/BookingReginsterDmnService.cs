using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.API;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.IRepository.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.API;
using Newtouch.HIS.Proxy.guian.DTO.S25;
using Newtouch.HIS.Sett.Request;
using Newtouch.HIS.Sett.Request.Booking.Request;
using Newtouch.HIS.Sett.Request.Booking.Response;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.DomainServices.API
{
    public class BookingReginsterDmnService : DmnServiceBase, IBookingRegisterDmnService
    {
        public BookingReginsterDmnService(IDefaultDatabaseFactory databaseFactory)
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
        private readonly ISysPatientBasicInfoRepo _SysPatientBasicInfoRepo;
        private readonly ISysCardRepo _SysCardRepo;
        private readonly IPatientBasicInfoDmnService _PatientBasicInfoDmnService;
        private readonly IOutBookScheduleRepo _OutBookScheduleRepo;
        private readonly IOutpatientRegistRepo _ioutpatientRegistRepo;
        //private readonly IOutPatChargeApp _outPatChargeApp;
        /// <summary>
        /// 获取卡信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public IList<CardInfoResp> GetCardInfo(CardInfoReq req)
        {
            string sql = @"select a.patid PatientNumber,blh,a.xm PatientName,xb Gender,csny Brithday,b.CardNo ,b.CardType,
b.CardTypeName,b.brxz, b.brxz PatType,c.brxzmc PatTypeName, Zjh
from xt_brjbxx a with(nolock)
left join xt_card b with(nolock) on a.organizeID=b.organizeid and a.patid=b.patid and b.zt='1'
left join dbo.xt_brxz c with(nolock) on b.organizeID=c.organizeid and b.brxz=c.brxz and c.zt='1'
where a.zt='1' and a.organizeId=@orgId 
";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", ConfigurationManager.AppSettings[req.HospitalID]));
            if (!string.IsNullOrEmpty(req.CardNo))
            {
                sql += " and b.cardno=@CardNo ";
                para.Add(new SqlParameter("@CardNo", req.CardNo));
            }
            if (!string.IsNullOrEmpty(req.IDCard))
            {
                sql += " and a.zjh=@zjh ";
                para.Add(new SqlParameter("@zjh", req.IDCard));
            }
            if (!string.IsNullOrEmpty(req.Name))
            {
                sql += " and a.xm=@xm ";
                para.Add(new SqlParameter("@xm", req.Name));
            }
            if (!string.IsNullOrEmpty(req.PatType))
            {
                sql += " and b.brxz=@brxz ";
                para.Add(new SqlParameter("@brxz", req.PatType));
            }
            return this.FindList<CardInfoResp>(sql, para.ToArray());
        }
        /// <summary>
        /// 获取预约出诊科室信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ks"></param>
        /// <param name="ksmc"></param>
        /// <returns></returns>
        public IList<DepartmentResp> GetDepartmentInfo(DepartmentDTO dto)
        {

            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", ConfigurationManager.AppSettings[dto.HospitalID]));

            string sql = @"select Name as DeptName,Code as Dept from [NewtouchHIS_Base].[dbo].[V_S_Sys_Department] where 1=1 and zt='1' and iscz='1' and organizeId=@orgId";


            if (!string.IsNullOrEmpty(dto.Dept))
            {
                sql += " and code=@code ";
                para.Add(new SqlParameter("@code", dto.Dept));
            }
            if (!string.IsNullOrEmpty(dto.DeptName))
            {
                sql += " and name=@name ";
                para.Add(new SqlParameter("@name", dto.DeptName));
            }return this.FindList<DepartmentResp>(sql, para.ToArray());

        }
        /// <summary>
        /// 一段时间内门诊排班及号源等信息
        /// </summary>
        /// <returns></returns>
        public IList<MzpbScheduleResponse> GetMzpbSchedule(DepartmentSchedulingDTO dto)
        {
            string sql = @"select cast(a.ScheduId as int) ScheduId,a.[OutDate],
[czks] OutDept,[czksmc] OutDeptName,a.[RegType],[Title],[ysgh] Doctor,[ysxm] DoctorName,Weekdd,a.[Period],[PeriodDesc],a.[PeriodStart],a.[PeriodEnd]
,convert(decimal(10,2),a.[RegFee]) [RegFee],isnull(d.dj,0.00) GhFee,sum(isnull(e.price,0.00)) ZLFee,
[ghlx],a.[zlxm],d.sfxmmc ghxmName,'诊疗费' zlxmName,[TotalNum],a.TotalNum-count(b.bookid) BookNum
from mz_ghpb_schedule a with(nolock)
left join mz_gh_book b with(nolock) on a.organizeid=b.organizeid and a.scheduid=b.scheduid and b.zt=1 and b.yyzt<>'3'
left join [NewtouchHIS_Base].dbo.[V_S_xt_sfxm] d with(nolock) on a.organizeid =d.organizeid and d.zt='1' and a.ghlx=d.sfxmcode
left join [dbo].[mz_gh_zlxmzh] e with(nolock) on a.organizeid =e.organizeid and e.zt='1' and a.zlxm=e.zhcode
where a.[OrganizeId]=@orgId and a.zt='1' and a.OutDate>=convert(varchar(10),GETDATE(),121) and 
a.OutDate<=convert(varchar(10), DATEADD(DAY,@pbday,GETDATE()),121) and IsBook='1' and IsCancel='0' ";
            List<SqlParameter> para=new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", ConfigurationManager.AppSettings[dto.HospitalID]));

            if (string.IsNullOrWhiteSpace(dto.SchedulingDay.ToString()))
            {
                dto.SchedulingDay =Convert.ToInt32(ConfigurationHelper.GetAppConfigValue("mzpbday"));
            }
            para.Add(new SqlParameter("@pbday", dto.SchedulingDay));

            if (!string.IsNullOrWhiteSpace(dto.Dept))
            {
                sql += " and czks=@czks";
                para.Add(new SqlParameter("@czks",dto.Dept));
            }
            if (!string.IsNullOrWhiteSpace(dto.RegType))
            {
                sql += " and a.RegType=@RegType";
                para.Add(new SqlParameter("@RegType", dto.RegType));
            }
            if (!string.IsNullOrWhiteSpace(dto.DeptName))
            {
                sql += " and czksmc=@czksmc";
                para.Add(new SqlParameter("@czksmc", dto.DeptName));
            }
            if (!string.IsNullOrWhiteSpace(dto.Doctor))
            {
                sql += " and ysgh=@Doctor";
                para.Add(new SqlParameter("@Doctor", dto.Doctor));
            }
            if (!string.IsNullOrWhiteSpace(dto.DoctorName))
            {
                sql += " and ysxm=@DoctorName";
                para.Add(new SqlParameter("@DoctorName", dto.DoctorName));
            }
            sql += @"  group by a.ScheduId,a.OutDate,
[czks],[czksmc],a.[RegType],[Title],[ysgh],[ysxm],weekdd,a.[Period],[PeriodDesc],a.[PeriodStart],a.[PeriodEnd],a.[RegFee],
[ghlx],a.[zlxm],d.sfxmmc,TotalNum,d.dj,e.price
order by a.OutDate  ";
            return FindList<MzpbScheduleResponse>(sql, para.ToArray());
        }
        /// <summary>
        /// 出诊日排班及号源信息
        /// </summary>
        /// <returns></returns>
        public IList<MzpbResponse> GetMzpb(MzKsPbDto dto)
        {
            string sql = @"select a.[OutDate],
[czks] OutDept,[czksmc] OutDeptName,a.[RegType],[Title],sum(a.TotalNum) TotalNum,sum(a.TotalNum)-count(b.bookid) BookNum
from mz_ghpb_schedule a with(nolock)
left join mz_gh_book b with(nolock) on a.organizeid=b.organizeid and a.scheduid=b.scheduid and b.zt=1 and b.yyzt<>'3'
where a.[OrganizeId]=@orgId and a.zt='1' and IsBook='1' and IsCancel='0' ";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", ConfigurationManager.AppSettings[dto.HospitalID]));

            if (!string.IsNullOrWhiteSpace(dto.RegType))
            {
                sql += " and a.OutDate=convert(varchar(10),@OutDate,121) ";
                para.Add(new SqlParameter("@OutDate", dto.OutDate));
            }
            if (!string.IsNullOrWhiteSpace(dto.RegType))
            {
                sql += " and a.RegType=@RegType";
                para.Add(new SqlParameter("@RegType", dto.RegType));
            }
            if (!string.IsNullOrWhiteSpace(dto.Dept))
            {
                sql += " and czks=@czks";
                para.Add(new SqlParameter("@czks", dto.Dept));
            }
            if (!string.IsNullOrWhiteSpace(dto.DeptName))
            {
                sql += " and czksmc=@czksmc";
                para.Add(new SqlParameter("@czksmc", dto.DeptName));
            }
            if (!string.IsNullOrWhiteSpace(dto.Doctor))
            {
                sql += " and ysgh=@Doctor";
                para.Add(new SqlParameter("@Doctor", dto.Doctor));
            }
            if (!string.IsNullOrWhiteSpace(dto.DoctorName))
            {
                sql += " and ysxm=@DoctorName";
                para.Add(new SqlParameter("@DoctorName", dto.DoctorName));
            }
            sql += @"  group by a.[OutDate],
[czks],[czksmc],a.[RegType],[Title]
order by a.OutDate ";
            return FindList<MzpbResponse>(sql, para.ToArray());
        }
        /// <summary>
        /// 出诊日排班时间段明细
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public IList<MzpbScheduleResponse> GetMzpbDetail(MzKsPbDto dto)
        {

            string sql = @"select cast(a.ScheduId as int) ScheduId,a.[OutDate],
[czks] OutDept,[czksmc] OutDeptName,a.[RegType],[Title],[ysgh] Doctor,[ysxm] DoctorName,Weekdd,a.[Period],[PeriodDesc],a.[PeriodStart],a.[PeriodEnd]
,convert(decimal(10,2),a.[RegFee]) [RegFee],isnull(d.dj,0.00) GhFee,sum(isnull(e.price,0.00)) ZLFee,
[ghlx],a.[zlxm],d.sfxmmc ghxmName,'诊疗费' zlxmName,[TotalNum],a.TotalNum-count(b.bookid) BookNum
from mz_ghpb_schedule a with(nolock)
left join mz_gh_book b with(nolock) on a.organizeid=b.organizeid and a.scheduid=b.scheduid and b.zt=1 and b.yyzt<>'3'
left join [NewtouchHIS_Base].dbo.[V_S_xt_sfxm] d with(nolock) on a.organizeid =d.organizeid and d.zt='1' and a.ghlx=d.sfxmcode
left join [dbo].[mz_gh_zlxmzh] e with(nolock) on a.organizeid =e.organizeid and e.zt='1' and a.zlxm=e.zhcode
where a.[OrganizeId]=@orgId and a.zt='1' and IsBook='1' and IsCancel='0' ";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", ConfigurationManager.AppSettings[dto.HospitalID]));

            if (!string.IsNullOrWhiteSpace(dto.RegType))
            {
                sql += " and a.OutDate=convert(varchar(10),@OutDate,121) ";
                para.Add(new SqlParameter("@OutDate", dto.OutDate));
            }
            if (!string.IsNullOrWhiteSpace(dto.Dept))
            {
                sql += " and czks=@czks";
                para.Add(new SqlParameter("@czks", dto.Dept));
            }
            if (!string.IsNullOrWhiteSpace(dto.DeptName))
            {
                sql += " and czksmc=@czksmc";
                para.Add(new SqlParameter("@czksmc", dto.DeptName));
            }
            if (!string.IsNullOrWhiteSpace(dto.Doctor))
            {
                sql += " and ysgh=@Doctor";
                para.Add(new SqlParameter("@Doctor", dto.Doctor));
            }
            if (!string.IsNullOrWhiteSpace(dto.DoctorName))
            {
                sql += " and ysxm=@DoctorName";
                para.Add(new SqlParameter("@DoctorName", dto.DoctorName));
            }
            if (!string.IsNullOrWhiteSpace(dto.RegType))
            {
                sql += " and a.RegType=@RegType";
                para.Add(new SqlParameter("@RegType", dto.RegType));
            }
            if (!string.IsNullOrWhiteSpace(dto.ScheduId.ToString()))
            {
                sql += " and a.ScheduId=@ScheduId";
                para.Add(new SqlParameter("@ScheduId", dto.ScheduId.ToString()));
            }
            sql += @"  group by a.ScheduId,a.OutDate,
[czks],[czksmc],a.[RegType],[Title],[ysgh],[ysxm],weekdd,a.[Period],[PeriodDesc],a.[PeriodStart],a.[PeriodEnd],a.[RegFee],
[ghlx],a.[zlxm],d.sfxmmc,TotalNum,d.dj,e.price
order by a.OutDate  ";
            return FindList<MzpbScheduleResponse>(sql, para.ToArray());
        }

        /// <summary>
        /// 获取卡信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="kh"></param>
        /// <param name="zjh"></param>
        /// <param name="xm"></param>
        /// <returns></returns>
        public IList<SysPatInfoResp> GetPatInfo(string orgId, string kh, string zjh, string xm)
        {

            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", ConfigurationManager.AppSettings[orgId]));

            string sql = @"select a.organizeId,a.patid, c.brxzmc brxz, blh,a.xm,xb,csny, zjh,dh,dwdz,mz,zy,b.cardno kh,b.cardtypename klx
from xt_brjbxx a with(nolock)
left join xt_card b with(nolock) on a.organizeID=b.organizeid and a.patid=b.patid and b.zt='1'
left join dbo.xt_brxz c with(nolock) on b.organizeID=c.organizeid and b.brxz=c.brxz and c.zt='1'
where a.zt='1' and a.organizeId=@orgId
";
            if (!string.IsNullOrEmpty(kh))
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
            return this.FindList<SysPatInfoResp>(sql, para.ToArray());

        }
        /// <summary>
        /// 预约挂号/非预约当日挂号
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public MzAppointmentResp OutAppointment(MzAppointmentReq dto)
        {
            string message = "";
            if (dto.IsBooking == "N" && Convert.ToDateTime(dto.OutDate).Date != DateTime.Now.Date)
            {
                throw new FailedException("非预约挂号只能选择当天！");
            }
            if (Convert.ToDateTime(dto.OutDate).Date < DateTime.Now.Date)
            {
                throw new FailedException("预约日期需要为今天之后的日期！");
            }
            MzKsPbDto pbxx = new MzKsPbDto();
            pbxx.ScheduId = dto.ScheduId;
            pbxx.HospitalID = dto.HospitalID;
            var pbEntity = GetMzpbDetail(pbxx).FirstOrDefault();
            if (pbEntity == null)
            {
                message = dto.IsBooking == "N" ? "该排班不支持当日挂号！" : "该排班不可预约！";
                throw new FailedException(message);
            }
            if (pbEntity.BookNum==0)
            {
                throw new FailedException("该排班已无号源！");
            }
            var brxxEntiey = GetPatInfo(dto.HospitalID, dto.CardNo, null, null).FirstOrDefault();
            if (brxxEntiey == null)
            {
                throw new FailedException("该卡号在His中未登记！");
            }
            DateTime pbOutdate = Convert.ToDateTime(dto.OutDate);
            dto.HospitalID = ConfigurationManager.AppSettings[dto.HospitalID];
            var yyEntity = _MzghbookRepo.FindEntity(p => p.OrganizeId == dto.HospitalID && p.kh == dto.CardNo && 
                                                    p.yyzt != ((int)EnumMzyyzt.cancel).ToString() &&
                                                    p.yyzt != ((int)EnumMzyyzt.bookreg).ToString() &&
                                                    p.ScheduId == dto.ScheduId && p.zt == "1" && p.OutDate == pbOutdate);

            if (yyEntity != null)
            {
                message = dto.IsBooking == "N" ? "该卡号已预约过该排班,请取消后在进行当日挂号或用预约号挂号！" : "该卡号已预约过该排班，请勿重复预约！";
                throw new FailedException(message);
            }
            var kepbEntity = _MzghbookRepo.FindEntity(p =>
                p.OrganizeId == dto.HospitalID && p.yyzt != ((int)EnumMzyyzt.cancel).ToString() &&
                p.yyzt != ((int)EnumMzyyzt.bookreg).ToString() &&
                p.zt == "1" && p.kh == dto.CardNo && p.ks == pbEntity.OutDept && p.OutDate == pbOutdate);
            if (kepbEntity != null)
            {
                message = dto.IsBooking == "N" ? "该卡号已预约过该科室,请取消后在进行当日挂号或用预约号挂号！" : "同一科室当天不可重复预约！";
                throw new FailedException(message);
            }
            var queno = _OutPatientDmnService.GetJzxh(dto.ScheduId.ToString(), pbEntity.OutDept, pbEntity.Doctor, "", dto.AppID, dto.HospitalID);
            var bookId = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_gh_book");
            MzghbookEntity yyety = new MzghbookEntity();
            yyety.BookId = bookId;
            yyety.ScheduId = Convert.ToDecimal(dto.ScheduId);
            yyety.kh = dto.CardNo;
            yyety.AppId = dto.AppID;
            yyety.OutDate = Convert.ToDateTime(dto.OutDate);
            yyety.patid = brxxEntiey.patid;
            yyety.Period = Convert.ToInt32(pbEntity.Period);
            yyety.PeriodStart = pbEntity.PeriodStart;
            yyety.PeriodEnd = pbEntity.PeriodEnd;
            yyety.QueueNo = queno;
            yyety.ghxz = dto.ghxz;
            yyety.RegType = pbEntity.RegType;
            yyety.RegFee = pbEntity.RegFee == null ? 0 : Convert.ToDecimal(pbEntity.RegFee);
            yyety.ks = pbEntity.OutDept;
            yyety.lxdh = brxxEntiey.dh ?? brxxEntiey.dh;
            yyety.ys = pbEntity.Doctor;
            yyety.xm = brxxEntiey.xm;
            yyety.xb = brxxEntiey.xb;
            yyety.yynr = pbEntity.Title;

            yyety.OrganizeId = dto.HospitalID;
            yyety.CreatorCode = dto.AppID;
            yyety.yyzt =dto.IsBooking== "N" ? ((int)EnumMzyyzt.bookreg).ToString(): ((int)EnumMzyyzt.book).ToString();


            yyety.Create();
            _MzghbookRepo.Insert(yyety);
            var regEty = _OutBookScheduleRepo.FindEntity(p => p.ScheduId == dto.ScheduId && p.zt == "1");
            if (regEty != null)
            {
                regEty.YYNum += 1;
                _OutBookScheduleRepo.Update(regEty);
            }
            else {
                message = dto.IsBooking == "N" ? "未找到当日挂号排班信息！" : "未找到当前的预约数据！";
                throw new FailedException(message);
            }
            return new MzAppointmentResp()
            {
                BookID = bookId.ToString(),
                QueueNo=queno
            };
        }
        /// <summary>
        /// (挂号结算)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public RegSettResp OutpatRegSett(RegSettReq req)
        {
            MzghbookEntity bookRecord = new MzghbookEntity();
            string message = "";
            int patid = -1;
            int qzjzxh = -1;
            string orgId = ConfigurationManager.AppSettings[req.HospitalID];
            if (!string.IsNullOrWhiteSpace(req.BookID.ToString()))
            {
                string yyzt = req.IsBooking=="N" ? ((int)EnumMzyyzt.bookreg).ToString():((int)EnumMzyyzt.book).ToString();
                DateTime datevalid = DateTime.Now.Date;
                bookRecord = _MzghbookRepo.FindEntity(p =>
                    p.BookId == req.BookID && p.yyzt == yyzt && p.zt == "1" && p.OrganizeId == orgId &&
                    p.OutDate >= datevalid);
                if (bookRecord != null)
                {
                    patid = Convert.ToInt32(bookRecord.patid);
                    qzjzxh = bookRecord.QueueNo;
                    if (req.CardNo != bookRecord.kh)
                    {
                        message = req.IsBooking == "N" ? "卡信息异常,请重新挂号！" : "卡信息异常，请确认预约信息与患者是否一致";
                        throw new FailedException(message);
                    }
                }
                else
                {
                    message = req.IsBooking == "N" ? "挂号失败，请重试" : "未找到预约号【" + req.BookID + "】,请确认预约状态及日期是否有效";
                    throw new FailedException(message);
                }
                var brxxEntiey = GetPatInfo(req.HospitalID, req.CardNo, null, null).FirstOrDefault();
                if (brxxEntiey != null)
                {
                    if (patid != brxxEntiey.patid)
                    {
                        throw new FailedException("预约信息与卡信息不符");
                    }
                }
                else
                {
                    throw new FailedException("当前卡号【" + req.CardNo + "】在HIS中不存在！");
                }
                
            }
            //获取门诊号
            string mzh = _OutPatientSettleDmnService.GetRegMzh(patid, bookRecord.ScheduId.ToString(), bookRecord.ks, orgId, null,
                bookRecord.QueueNo, bookRecord.OutDate.ToString("yyyy-MM-dd"));
            MzKsPbDto pbxx = new MzKsPbDto();
            var scheduId = Convert.ToInt32(bookRecord.ScheduId);
            pbxx.HospitalID = req.HospitalID;
            pbxx.ScheduId = scheduId;
            var pbEntity = GetMzpbDetail(pbxx).FirstOrDefault();
            if (pbEntity == null)
                throw new FailedException("该排班无效，请重试！");

            object regobj;
            OutpatientSettFeeRelatedDTO feeRelated = new OutpatientSettFeeRelatedDTO();
            feeRelated.zje = bookRecord.RegFee;
            feeRelated.orglxjzfys = bookRecord.RegFee;
            feeRelated.ssk = bookRecord.RegFee;
            feeRelated.zhaoling = 0;
            feeRelated.xjzfys = bookRecord.RegFee;
            feeRelated.yjjzfje = 0;
            feeRelated.djjess = req.PayFee;


            string ghly = "1";
            SysCashPaymentModelEntity zffsEty = new SysCashPaymentModelEntity();
            if (bookRecord.AppId == EnumMzghly.WeChat.ToString())
            {
                zffsEty = _sysForCashPayRepository.FindEntity(p =>
                    p.xjzffsmc.Contains("微信") && p.zt == "1");
                ghly = ((int)EnumMzghly.WeChat).ToString();
            }
            else if (bookRecord.AppId == EnumMzghly.Alipay.ToString())
            {
                zffsEty = _sysForCashPayRepository.FindEntity(p =>
                    p.xjzffsmc.Contains("支付宝") && p.zt == "1");
                ghly = ((int)EnumMzghly.Alipay).ToString();
            }
            feeRelated.zffs1 = zffsEty == null ? "" : zffsEty.xjzffs;
            feeRelated.zfje1 = zffsEty == null ? null : req.PayFee;
            feeRelated.djjesszffs = zffsEty == null ? "" : zffsEty.xjzffs;

            _OutPatientSettleDmnService.Save(orgId, patid, req.CardNo, ghly, pbEntity.RegType,
                           pbEntity.OutDept, "", pbEntity.OutDeptName, "", pbEntity.Ghlx, pbEntity.Zlxm, null, null, false,
                           false, bookRecord.QueueNo, scheduId, feeRelated, bookRecord.ghxz, null, mzh, "0", null, null, null, null, null, null,
                           out regobj);
            var regEty = _outpatientRegRepo.FindEntity(p => p.mzh == mzh && p.zt == "1" && p.OrganizeId == orgId);
            if (regEty != null)
            {
                regEty.ghrq = bookRecord.OutDate;
                _outpatientRegRepo.Update(regEty);

                bookRecord.PayFee = req.PayFee;
                bookRecord.PayLsh = req.PayLsh;
                bookRecord.LastModifyTime = DateTime.Now;
                bookRecord.LastModifierCode = req.AppID;
                bookRecord.yyzt = req.IsBooking == "N" ? ((int)EnumMzyyzt.bookreg).ToString():((int)EnumMzyyzt.reg).ToString();
                bookRecord.drghstatu =req.IsBooking=="N"?"Y":"";
                bookRecord.ghrq = bookRecord.OutDate;
                bookRecord.mzh = mzh;
                _MzghbookRepo.Update(bookRecord);

            }
            return new RegSettResp()
            {
                RegId = ((dynamic)regobj).jsnm.ToString(),
                QueueNo= bookRecord.QueueNo,
                Mzh=mzh
                //RegId = regobj.ToString()
            };
        }

        /// <summary>
        /// 预约号查询预约信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public MzAppointmentRecordResp QueryAppRecord(MzAppointmentRecordReq req)
        {
            string sql = @"SELECT cast(BookId as int) BookId,yyzt BookStatus,
cast(ScheduId as int) ScheduId,Ghxz,Xm PatName,xb Gnder,Lxdh,kh CardNo,
c.name Dept,d.name Doctor,OutDate
,RegType,e.name RegName,Period,PeriodStart,PeriodEnd,QueueNo,RegFee,PayFee,PayLsh,PayTime
,CancelReason,CancelTime,mzh,ghrq,yynr
from [mz_gh_book] a with(nolock)
left join xt_brxz b with(nolock) on a.organizeid=b.organizeid and a.ghxz=b.brxz
left join [NewtouchHIS_Base].[dbo].[V_S_Sys_Department] c on a.organizeid=c.organizeid and a.ks=c.code and c.zt='1'
left join [NewtouchHIS_Base].[dbo].[V_S_Sys_Staff] d on a.ys=d.gh and a.organizeid=d.organizeid and d.zt='1'
left join [NewtouchHIS_Base].[dbo].[V_C_Sys_ItemsDetail] e with(nolock) on a.[RegType]=e.code and e.CateCode='RegType' and e.zt='1'
where a.zt='1' and a.OrganizeId=@orgId
";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", ConfigurationManager.AppSettings[req.HospitalID]));
            if (!string.IsNullOrWhiteSpace(req.BookId))
            {
                sql += " and a.BookId=@BookId ";
                para.Add(new SqlParameter("@BookId", req.BookId));
            }
            return FindList<MzAppointmentRecordResp>(sql, para.ToArray()).FirstOrDefault();
        }

        /// <summary>
        /// 预约list
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public IList<MzAppointmentRecordListResp> QueryAppRecordList(MzAppointmentRecordListReq req)
        {
            string sql = @"SELECT RegType,cast(BookId as int) BookId,yyzt BookStatus,a.Xm PatName,a.xb Gender,
c.name Dept,OutDate,QueueNo,RegFee,a.CreateTime
from [mz_gh_book] a with(nolock)
left join xt_brjbxx xtxx with(nolock) on xtxx.patid=a.patid and xtxx.OrganizeId=a.OrganizeId and xtxx.zt=1
left join xt_brxz b with(nolock) on a.organizeid=b.organizeid and a.ghxz=b.brxz
left join [NewtouchHIS_Base].[dbo].[V_S_Sys_Department] c on a.organizeid=c.organizeid and a.ks=c.code and c.zt='1'
where a.zt='1'
";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", ConfigurationManager.AppSettings[req.HospitalID]));
            if (!string.IsNullOrWhiteSpace(req.CardNo))
            {
                sql += " and a.kh=@kh ";
                para.Add(new SqlParameter("@kh", req.CardNo));
            }
            if (!string.IsNullOrWhiteSpace(req.IDCard))
            {
                sql += " and xtxx.zjh=@zjh ";
                para.Add(new SqlParameter("@zjh", req.IDCard));
            }
            if (!string.IsNullOrWhiteSpace(req.RegType))
            {
                sql += " and a.RegType=@RegType ";
                para.Add(new SqlParameter("@RegType", req.RegType));
            }
            if (!string.IsNullOrWhiteSpace(req.Dept))
            {
                sql += " and a.ks=@Dept ";
                para.Add(new SqlParameter("@Dept", req.Dept));
            }
            if (!string.IsNullOrWhiteSpace(req.DeptName))
            {
                sql += " and c.name=@DeptName ";
                para.Add(new SqlParameter("@DeptName", req.DeptName));
            }
            if (req.OutDate!=null)
            {
                sql += " and a.OutDate=convert(varchar(10),@OutDate,121) ";
                para.Add(new SqlParameter("@OutDate", req.OutDate));
            }
            return FindList<MzAppointmentRecordListResp>(sql, para.ToArray());
        }
        /// <summary>
        /// 取消预约(限未结算)
        /// </summary>
        /// <param name="dto"></param>
        public int CancelOutApp(MzAppointmentRecordReq req)
        {
            int bookid = Convert.ToInt32(req.BookId);
            req.HospitalID = ConfigurationManager.AppSettings[req.HospitalID];
            var yyEntity = _MzghbookRepo.FindEntity(p => p.BookId == bookid && p.OrganizeId == req.HospitalID && p.zt=="1");
            if (yyEntity == null)
            {
                throw new FailedException("无效的预约号");
            }
            if (yyEntity.yyzt == ((int)EnumMzyyzt.bookreg).ToString())
            {
                throw new FailedException("当日挂号无需取消预约");
            }
            if (yyEntity.yyzt != ((int)EnumMzyyzt.book).ToString())
            {
                throw new FailedException("取消预约失败，当前预约状态为【"+ ((EnumMzyyzt)Convert.ToInt32(yyEntity.yyzt)).GetDescription() + "】");
            }
            yyEntity.yyzt = ((int)EnumMzyyzt.cancel).ToString();
            yyEntity.CancelReason = req.AppID + ";" + yyEntity.lxdh ?? req.Lxdh;
            yyEntity.CancelTime = DateTime.Now;
            yyEntity.LastModifierCode = req.AppID;
            yyEntity.LastModifyTime = DateTime.Now;
            var refCnt= _MzghbookRepo.Update(yyEntity);
            var regEty = _OutBookScheduleRepo.FindEntity(p => p.ScheduId == yyEntity.ScheduId && p.zt == "1");
            if (regEty != null)
            {
                regEty.YYNum= regEty.YYNum>0 ? regEty.YYNum - 1: 0;
                _OutBookScheduleRepo.Update(regEty);
            }
            //else
            //{
            //    throw new FailedException("取消预约成功,号源退还失败,原因:未找到预约排班！");
            //}
            return refCnt;
        }
        /// <summary>
        /// 获取医生信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public IList<DeptDoctorResp> GetStaffInfo(DeptDoctorReq req)
        {
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", ConfigurationManager.AppSettings[req.HospitalID]));

            string sql = @"  select departmentCode Dept,department.Name as DeptName,gh Doctor,staff.Name as DoctorName,cast( Gender as varchar) Gender ,duty.Name as dutyName
  from [NewtouchHIS_Base].[dbo].[Sys_Staff] staff
  left join [NewtouchHIS_Base].[dbo].[Sys_Department] department on
  department.code=staff.departmentCode and department.zt=1
  left join [NewtouchHIS_Base].[dbo].[Sys_StaffDuty] staffduty on
  staff.id=staffduty.staffId and staffduty.zt=1
  left join [NewtouchHIS_Base].[dbo].[Sys_Duty] duty on
  duty.id=staffduty.dutyId  and duty.zt=1
  where  staff.zt='1' and iscz='1'  and duty.code='Doctor'  and staff.organizeId=@orgId and departmentcode is not null ";

            if (!string.IsNullOrEmpty(req.Doctor))
            {
                sql += " and gh=@gh ";
                para.Add(new SqlParameter("@gh", req.Doctor));
            }
            if (!string.IsNullOrEmpty(req.Dept))
            {
                sql += " and departmentCode=@Dept ";
                para.Add(new SqlParameter("@Dept", req.Dept));
            }
            return this.FindList<DeptDoctorResp>(sql, para.ToArray());

        }
        /// <summary>
        /// 建卡
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public CardInfoResp SysPatInfoSet(RegisterReq req)
        {
            string OrgId= ConfigurationManager.AppSettings[req.HospitalID];
            if (!CommmHelper.CheckIDCard(req.IDCard))
            {
                throw new FailedException("证件号格式不正确");
            }
            if (req.PatType != ((int)EnumBrxz.zf).ToString())
            {
                var khvisit = _SysCardRepo.FindEntity(p=>p.CardNo==req.CardNo &&p.OrganizeId==OrgId &&p.zt=="1");
                if(khvisit!=null)
                    throw new FailedException("卡号已经存在,不可多次绑定");
                string sql = @"  select a.xm
                             from xt_brjbxx a
                             left join xt_card b on  a.patid=b.patid and a.organizeid=b.organizeid and b.zt=1
                             where a.zt=1 and a.OrganizeId=@OrgId 
                                and a.zjh=@zjh  and b.brxz=@brxz ";
                var validatepat = this.FindList<string>(sql,
                    new[] {new SqlParameter("OrgId", OrgId) ,
                    new SqlParameter("zjh",req.IDCard),
                    new SqlParameter("brxz",req.PatType)
                    });
                if (validatepat.Count() > 0)
                {
                    throw new FailedException("证件信息已经存在,非自费性质只能绑定一次");
                }
            }
            var FirstVisit = ConfigurationManager.AppSettings["FirstVisit"];
            if (FirstVisit == "N")
            {
                var visit = _SysPatientBasicInfoRepo.FindEntity(p=>p.zjh==req.IDCard &&p.OrganizeId==OrgId &&p.zt=="1");
                if(visit==null)
                    throw new FailedException("初次就诊需要先线下医院就诊");
            }
            SysHosBasicInfoVO vo = new SysHosBasicInfoVO();
            if (req.PatType != ((int)EnumBrxz.zf).ToString())
            {
                if (string.IsNullOrWhiteSpace(req.CardNo))
                {
                    throw new FailedException("非自费性质,卡号不能为空");
                }
                vo.kh = req.CardNo;
                vo.cardtype = ((int)EnumCardType.YBJYK).ToString();
            }
            else {
                vo.kh = _SysCardRepo.GetCardSerialNo(OrgId);
            }
            vo.blh = _SysPatientBasicInfoRepo.Getblh(OrgId);
            vo.xm = req.Name;
            vo.csny = req.BithDay.ToDateString();
            vo.zjlx = ((int)EnumZJLX.sfz).ToString();
            vo.zjh = req.IDCard;
            vo.dh = req.Phone;
            vo.xb = req.Gender;
            vo.brxz = req.PatType;
            vo.py = CommmHelper.StrConvertToPinyin(req.Name, "1");
            vo.isdsfly = "Y"; //第三方健卡 不更改一卡通基本信息

            _PatientBasicInfoDmnService.SavePatBasicCardInfo(vo, OrgId, "");
            CardInfoReq kxx = new CardInfoReq();
            kxx.IDCard = req.IDCard;
            kxx.CardNo = vo.kh;
            kxx.Name = req.Name;
            kxx.PatType = req.PatType;
            kxx.HospitalID = req.HospitalID;
            var list = GetCardInfo(kxx).FirstOrDefault();
            return list;
        }
        /// <summary>
        /// 取消结算
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public CancalSettResp CancalSett(CancalSettReq req)
        {
            object newJszbInfo;
            string outTradeNo;
            decimal refundAmount;
            string mzh = "";
            OrderVo ordervo = new OrderVo();
            var orgId = ConfigurationManager.AppSettings[req.HospitalID];
            var ghlybz = (int)Enum.Parse(typeof(EnumMzghly), req.AppID);

            var settList = _refundService.RefundableQuery(orgId, Convert.ToInt32(req.RegId));
            if (settList.Count == 0)
            {
                throw new FailedException("未查询结算记录,请确认交易流水号是否正确");
            }
            int ghnm = settList.FirstOrDefault().ghnm;

            if (settList.FirstOrDefault().jslx == "0")//退挂号
            {
                var regEty = _outpatientRegRepo.FindEntity(p =>
                    p.ghnm == ghnm && p.zt == "1" && p.OrganizeId == orgId &&
                    p.ghly != ((int)EnumMzghly.His).ToString() &&
                    p.ghly == ghlybz.ToString());
                if (regEty == null)
                {
                    throw new FailedException("无效的就诊记录,不支持" + req.AppID + "端结算退费");
                }
                string sql = @"  select jsnm from mz_js nolock 
                                 where ghnm=@ghnm and  jszt=1 and jslx<>0 and zt=1 and OrganizeId=@orgId and zje>0 and 
                                       jsnm not in (select cxjsnm from mz_js nolock where jszt=2 and zt=1 ) ";
                var feeValid = this.FindList<string>(sql,
                    new[] {new SqlParameter("@orgId", orgId) ,
                    new SqlParameter("@ghnm",ghnm)
                    }).FirstOrDefault();
                if (feeValid != null)
                {
                    throw new FailedException("存在已结的处方,请先退完处方再退挂号费");
                }
                mzh = regEty.mzh;
            }
            else {
                string vilidsql = @" select mzzyh,cardno,outtradeno,tradeno,totalamount,Realitytotalamount,jsnm from [dbo].Xt_Order with(nolock) 
                                    where jsnm=@jsnm and OrderStatus=@status and OrganizeId=@orgId and zt=1";
                ordervo = this.FindList<OrderVo>(vilidsql,
                    new[] {new SqlParameter("@orgId", orgId) ,
                    new SqlParameter("@status", (int)EnumOrderStatus.yzf) ,
                    new SqlParameter("@jsnm",req.RegId)
                    }).FirstOrDefault();
                if (vilidsql==null)
                {
                    throw new FailedException("无效的结算记录!");
                }
            }
            Dictionary<int, decimal> dic = new Dictionary<int, decimal>();
            decimal expectedTmxZje = 0;
            foreach (var item in settList)
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
            if (expectedTmxZje<=0)
            {
                throw new FailedException("可退金额为零,请确认是否HIS端已退");
            }
            _outPatientUniversalDmnService.RefundSettlement(orgId, Convert.ToInt32(req.RegId), dic,
                                           expectedTmxZje,
                                           null, out newJszbInfo, out outTradeNo, out refundAmount, null, null,
                                           null, null, null, null);
            if (newJszbInfo==null)
            {
                throw new FailedException("退费失败,请重试");
            }
            if (settList.FirstOrDefault().jslx == "0")
            {
                var yyEntity = _MzghbookRepo.FindEntity(p => p.mzh == mzh && p.OrganizeId == orgId && p.zt == "1");
                // && p.yyzt == ((int)EnumMzyyzt.bookreg).ToString()
                if (yyEntity != null)
                {
                    yyEntity.yyzt = ((int)EnumMzyyzt.regcancel).ToString();
                    yyEntity.CancelReason = req.AppID + ";" + yyEntity.lxdh ?? "";
                    yyEntity.CancelTime = DateTime.Now;
                    yyEntity.LastModifierCode = req.AppID;
                    yyEntity.LastModifyTime = DateTime.Now;
                    var refCnt = _MzghbookRepo.Update(yyEntity);
                }
            }
            else
            {
                string tkinsert = @"insert into Xt_OrderRefund
(Id,OrderId,OrganizeId,RefundAmount,TradeNo,RefundFee,RefunDate,tkjsnm,Createtime,CreatorCode,zt)
values(NEWID(),@OrderId,@OrganizeId,@RefundAmount,@TradeNo,@RefundFee,getdate(),@tkjsnm,getdate(),@CreatorCode,'1')

update Xt_Order set OrderStatus=2 where jsnm=@jsnm and zt=1
";
                var pars = new List<SqlParameter>();
                pars.Add(new SqlParameter("@OrderId", ordervo.outtradeno));
                pars.Add(new SqlParameter("@OrganizeId", orgId));
                pars.Add(new SqlParameter("@RefundAmount", ordervo.totalamount));
                pars.Add(new SqlParameter("@TradeNo", ordervo.tradeno));
                pars.Add(new SqlParameter("@RefundFee", ordervo.Realitytotalamount));
                pars.Add(new SqlParameter("@tkjsnm", ((dynamic)newJszbInfo).jsnm.ToString()));
                pars.Add(new SqlParameter("@CreatorCode", req.AppID));
                pars.Add(new SqlParameter("@jsnm", ordervo.jsnm));
                var i = this.ExecuteSqlCommand(tkinsert, pars.ToArray());
            }
            return new CancalSettResp()
            {
                RegId = ((dynamic)newJszbInfo).jsnm.ToString()
            };
        }
        /// <summary>
        /// 待缴费订单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public IList<CostOrderResp> QueryCostOrder(CostOrderReq req)
        {
            var orgId= ConfigurationManager.AppSettings[req.HospitalID];
            var ghEntity = _ioutpatientRegistRepo.IQueryable(p => p.kh == req.CardNo && p.OrganizeId == orgId && p.zt == "1").OrderByDescending(k => k.CreateTime).FirstOrDefault();
            if (ghEntity == null)
            {
                throw new FailedException("该卡号无未就诊的挂号信息");
            }
            var feelistsql = new StringBuilder();
            feelistsql.Append(@"select 
             OutTradeNo OrderNo,b.name Dept,c.name Doctor,mzzyh Mzh,Caption CostName,ghrq DiagDay,TotalAmount 
        from [dbo].Xt_Order a with(nolock) 
		left join [NewtouchHIS_Base].[dbo].[V_S_Sys_Department] b on a.ks=b.code and a.organizeid=b.organizeid
		left join [NewtouchHIS_Base].[dbo].[V_S_Sys_Staff] c on a.ys=c.gh and a.organizeid=c.organizeid
                                    where cardno=@CardNo and a.zt=1 and a.OrganizeId=@orgId ");
            if (req.CostType == "1")
            {
                feelistsql.Append(" and OrderStatus=1 ");
            }
            else {
//                //暂时先采用删除在生成未缴费订单
//                string sql1 = @" delete Xt_OrderDetail from  Xt_OrderDetail a join Xt_Order b on a.OrderId=b.Id and (b.OrderStatus=0 or b.OrderStatus=3) where  b.CardNo=@CardNo and b.OrganizeId=@orgId
//                            delete  from Xt_Order where CardNo=@CardNo and OrganizeId=@orgId  and (OrderStatus=0 or OrderStatus=3)
//";
//                var pars = new List<SqlParameter>();
//                pars.Add(new SqlParameter("@orgId", orgId));
//                pars.Add(new SqlParameter("@CardNo", req.CardNo));
//                var i = this.ExecuteSqlCommand(sql1, pars.ToArray());

                // 发生变化并且订单待支付的处方作废并生成新处方订单
                string sql = @" exec Cf_OrderGenerate @orgId, @cardNo ";
                var result = this.FindList<string>(sql,
                        new[] {new SqlParameter("orgId", orgId) ,
                    new SqlParameter("cardNo",req.CardNo)
                        }).FirstOrDefault();

                feelistsql.Append(" and OrderStatus=0 ");
            }
            
            List<SqlParameter> para = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(req.DiagDay.ToString()))
            {
                feelistsql.Append(" and ghrq<=GETDATE() and ghrq>=DATEADD(DD,-@DiagDay,GETDATE()) ");
                para.Add(new SqlParameter("@DiagDay", req.DiagDay));
            }
            if (req.DiagDate!=null)
            {
                feelistsql.Append(" and ghrq=convert(varchar(10),@DiagDate,121) ");
                para.Add(new SqlParameter("@DiagDate", req.DiagDate));
            }
            if (string.IsNullOrWhiteSpace(req.DiagDay.ToString()) && req.DiagDate == null)
            {
                feelistsql.Append(" and ghrq<=GETDATE() and ghrq>=DATEADD(DD,-3,GETDATE())");
            }
            para.Add(new SqlParameter("@CardNo", req.CardNo));
            para.Add(new SqlParameter("@orgId", orgId));
            return this.FindList<CostOrderResp>(feelistsql.ToString(), para.ToArray());
        }
        /// <summary>
        /// 待缴费明细
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public CostOrderDetailResp QueryCostOrderDetail(CostOrderDetailReq req)
        {
            var orgId = ConfigurationManager.AppSettings[req.HospitalID];
            var ghEntity = _ioutpatientRegistRepo.FindEntity(p => p.mzh == req.Mzh && p.OrganizeId == orgId && p.zt == "1");
            if (ghEntity == null)
            {
                throw new FailedException("该门诊号未查询到挂号信息");
            }
            string sql = @"  SELECT 
            a.kh CardNo,c.OutTradeNo OrderNo,
            a.mzh Mzh,
            b.xm PatientName,
            case a.xb when '1' then '男' when '2' then '女' else '' end Gender,
            a.nlshow ,
			ISNULL(b.jzysmc, '医生') ClinicDoctor ,
			CONVERT(VARCHAR(20), b.zlkssj, 120) ClinicDateTime ,
            isnull(c.TotalAmount,0.00) TotalAmount,c.fph Fph
            FROM  NewtouchHIS_Sett..mz_gh a with(nolock)
            LEFT JOIN Newtouch_CIS..xt_jz b with(nolock) ON b.mzh = a.mzh
                      AND b.OrganizeId = a.OrganizeId AND b.zt = '1'
            LEFT JOIN NewtouchHIS_Sett..Xt_Order c with(nolock) on c.mzzyh = a.mzh AND c.OrganizeId = a.OrganizeId  
                      AND c.orderstatus = @status AND c.zt = 1
            WHERE a.zt = 1 and a.ghnm = @ghnm  AND a.OrganizeId = @orgId
";
            var baseVO = this.FirstOrDefault<OrderInfo>(sql,
                new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@ghnm", ghEntity.ghnm),
                    new SqlParameter("@status", req.CostType) });
            string feesql = @" SELECT
            cfh,Doctor,Dept,Price,Num,Amount,case CfType when '1' then '药品' else '项目' end  CfType,XmCode,Xmmc,dw,gg Gg,dlmc Flmc
            ,'' ReMark
            FROM Xt_Order  a with(nolock)
            JOIN Xt_OrderDetail b with(nolock) on b.orderId=a.Id and a.organizeId=b.organizeId
            WHERE a.zt=1 and orderstatus=@status and a.mzzyh=@Mzh  and a.OrganizeId=@orgId";
            var costList = this.FindList<CostOrderDetail>(feesql,
                new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@Mzh", ghEntity.mzh),
                 new SqlParameter("@status", req.CostType)});

            string feedlxsql = @" SELECT
            cfh,cftypename,sum(amount)  amount
            FROM Xt_Order  a with(nolock)
            JOIN Xt_OrderDetail b with(nolock) on b.orderId=a.Id and a.organizeId=b.organizeId
where a.zt=1 and a.mzzyh=@mzh and orderstatus=@status and a.OrganizeId=@orgId
group by cfh,cftypename";
            var costTypeList = this.FindList<OutPatientCost>(feedlxsql,
               new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@Mzh", ghEntity.mzh),
                   new SqlParameter("@status", req.CostType) });
            return new CostOrderDetailResp()
            {
                CardNo = baseVO.CardNo,
                OrderNo=baseVO.OrderNo,
                Mzh=baseVO.Mzh,
                PatientName = baseVO.PatientName,
                Gender = baseVO.Gender,
                nlshow = baseVO.nlshow,
                ClinicDoctor = baseVO.ClinicDoctor,
                ClinicDateTime = baseVO.ClinicDateTime,
                TotalAmount = baseVO.TotalAmount,
                Fph=baseVO.Fph,
                OrderDetailData = costList,
                CostTypeData=costTypeList
            };
        }
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public int CancalOrde(CancalOrderReq req)
        {
            var orgId = ConfigurationManager.AppSettings[req.HospitalID];
            string isexsit = @"select orderstatus from Xt_Order where outtradeno=@orderId and  mzzyh=@mzh and cardno=@cardno and
 organizeid=@orgId   and zt=1  -- and (orderstatus=@status1 or  orderstatus=@status2)
";
            var orderList = this.FirstOrDefault<string>(isexsit,
               new[] { new SqlParameter("@orgId", orgId),
               new SqlParameter("@mzh", req.Mzh),
               new SqlParameter("@cardno", req.CardNo),
               new SqlParameter("@orderId", req.OrderNo)
                   //new SqlParameter("@status1",((int)EnumOrderStatus.dzf).ToString()),
                   //new SqlParameter("@status2", ((int)EnumOrderStatus.zfz).ToString())
               });
            if(orderList==null)
                throw new FailedException("订单号:"+req.OrderNo+"无效");

            if (orderList==((int)EnumOrderStatus.yzf).ToString()|| orderList == ((int)EnumOrderStatus.ytk).ToString())
                throw new FailedException("订单号:" + req.OrderNo + "状态为"+ ((EnumOrderStatus)Convert.ToInt32(orderList)).GetDescription()+"不可取消");

            //string sql = @"update  Xt_OrderDetail  set zt=0 from Xt_Order a 
            //               where xt_OrderDetail.OrderId=a.Id and xt_OrderDetail.organizeId=a.organizeId and a.zt=1
            //               and  OuttradeNo=@orderNo and a.organizeId=@orgId
            //               update Xt_Order set zt=0 where  OuttradeNo=@orderNo and organizeId=@orgId and zt=1";
            //string sql = @" update Xt_Order set orderstatus=@status where  OuttradeNo=@orderNo and organizeId=@orgId and zt=1";
            //var pars = new List<SqlParameter>();
            //pars.Add(new SqlParameter("@orgId", orgId));
            //pars.Add(new SqlParameter("@orderNo", req.OrderNo));
            //pars.Add(new SqlParameter("@status", ((int)EnumOrderStatus.dzf).ToString()));
            //var i = this.ExecuteSqlCommand(sql, pars.ToArray());
            var i = ErrOrderCancal(req);
            return i;
        }
        /// <summary>
        /// 订单状态重置
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public int ErrOrderCancal(CancalOrderReq req)
        {
            var orgId = ConfigurationManager.AppSettings[req.HospitalID];
            string sql = @" update Xt_Order set orderstatus=@status where  OuttradeNo=@orderNo and organizeId=@orgId and zt=1";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            pars.Add(new SqlParameter("@orderNo", req.OrderNo));
            pars.Add(new SqlParameter("@status", ((int)EnumOrderStatus.dzf).ToString()));
            var i = this.ExecuteSqlCommand(sql, pars.ToArray());
            return i;
        }
        /// <summary>
        /// 预结算
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public PreSettResp OutPreSett(PreSettReq req)
        {
            var orgId = ConfigurationManager.AppSettings[req.HospitalID];
            if (req.FeeType == "0")
            {
                if (string.IsNullOrWhiteSpace(req.BookID.ToString())|| string.IsNullOrWhiteSpace(req.IsBooking))
                {
                    throw new FailedException("入参:BookID和IsBooking不能为空");
                }
                var validPre = ValidPreSett(req);
                if (!string.IsNullOrWhiteSpace(validPre))
                {
                    if (!string.IsNullOrWhiteSpace(req.PatType))
                    {
                        if (req.PatType != ((int)EnumBrxz.zf).ToString() && validPre == ((int)EnumBrxz.zf).ToString())
                        {
                            throw new FailedException("入参:PatType 值错误,该人员性质为自费");
                        }
                    }
                    if (validPre != ((int)EnumBrxz.zf).ToString() && req.PatType != ((int)EnumBrxz.zf).ToString())
                    {
                        //走医保
                        throw new FailedException("医保收费未开通");
                    }
                    else
                    {
                        return new PreSettResp()
                        {
                            ybzf = 0.00.ToDecimal(),
                            grzf=req.TotalAmount,
                            xjzf= req.TotalAmount
                        };
                    }
                }
                else {
                    throw new FailedException("获取病人性质异常，请重试");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(req.Mzh) || string.IsNullOrWhiteSpace(req.OrderNo))
                {
                    throw new FailedException("入参:Mzh和OrderNo不能为空");
                }
                var ghEntity = _ioutpatientRegistRepo.FindEntity(p => p.mzh == req.Mzh && p.OrganizeId == orgId && p.zt == "1");

                if (ghEntity == null)
                    throw new FailedException("不存在该门诊号的挂号记录");

                if (!string.IsNullOrWhiteSpace(req.PatType))
                {
                    if (req.PatType != ((int)EnumBrxz.zf).ToString() && ghEntity.brxz == ((int)EnumBrxz.zf).ToString())
                    {
                        throw new FailedException("入参:PatType 值错误,该人员性质为自费");
                    }
                }
                var amount = GetOrderAmount(req.Mzh,orgId);
                if (amount.Count==0)
                    throw new FailedException("订单中心无未结算订单");
                if (amount.Count > 1)
                    throw new FailedException("订单中心存在多条未结订单,请取消订单或重新查询代缴费订单");
                var jsje = amount.FirstOrDefault().TotalAmount;
                if (jsje!=req.TotalAmount)
                    throw new FailedException("收费金额异常，HIS为：【" + jsje + "】传入金额为：【" + req.TotalAmount + "】");

                if (amount.FirstOrDefault().Orderstatus==((int)EnumOrderStatus.zfz).ToString())
                {
                    throw new FailedException("订单号:"+req.OrderNo+"正在支付中,请勿重复支付");
                }
                var validCf = ValidFee(ghEntity.mzh, orgId);
                if (!string.IsNullOrWhiteSpace(validCf))
                    throw new FailedException("门诊号:" + req.Mzh + "的处方订单已发生改变，请重新查询费用订单");
                //费用无变化 锁定账单
                string orderlock = @" update Xt_Order set orderstatus=@status where  OuttradeNo=@orderNo and organizeId=@orgId and zt=1";
                var pars = new List<SqlParameter>();
                pars.Add(new SqlParameter("@orgId", orgId));
                pars.Add(new SqlParameter("@orderNo", req.OrderNo));
                pars.Add(new SqlParameter("@status", ((int)EnumOrderStatus.zfz).ToString()));
                var i = this.ExecuteSqlCommand(orderlock, pars.ToArray());
                if (ghEntity.brxz != ((int)EnumBrxz.zf).ToString() && req.PatType != ((int)EnumBrxz.zf).ToString())
                {
                    //走医保
                    throw new FailedException("医保收费未开通");
                    //医保返回错误需重置
                    CancalOrderReq cancalreq = new CancalOrderReq();
                    cancalreq.AppID = req.AppID;
                    cancalreq.HospitalID = req.HospitalID;
                    cancalreq.Mzh = req.Mzh;
                    cancalreq.CardNo = req.Mzh;
                    cancalreq.OrderNo = req.OrderNo;
                    var cz = ErrOrderCancal(cancalreq);
                }
                else
                {
                    return new PreSettResp()
                    {
                        ybzf = 0.00.ToDecimal(),
                        grzf = jsje,
                        xjzf = jsje
                    };
                }
            }
        }
        /// <summary>
        /// 处方结算
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public void OutSett(SettReq req,int jsnm,string orgId)
        {
            #region 
            //var orgId = ConfigurationManager.AppSettings[req.HospitalID];
            //var ghEntity = _ioutpatientRegistRepo.FindEntity(p => p.mzh == req.Mzh && p.OrganizeId == orgId && p.zt == "1");

            //if (ghEntity == null)
            //    throw new FailedException("不存在该门诊号的挂号记录");
            //if (!string.IsNullOrWhiteSpace(req.PatType))
            //{
            //    if (req.PatType != ((int)EnumBrxz.zf).ToString() && ghEntity.brxz == ((int)EnumBrxz.zf).ToString())
            //    {
            //        throw new FailedException("入参:PatType 值错误,该人员性质为自费");
            //    }
            //}
            //var validCf = ValidFee(ghEntity.mzh, orgId);
            //if (string.IsNullOrWhiteSpace(validCf))
            //    throw new FailedException("门诊号:" + req.Mzh + "的处方订单已发生改变，请重新拉取费用订单");

            //var amount = GetOrderAmount(req.Mzh, orgId);
            //if (amount.Count == 0)
            //    throw new FailedException("订单中心无未结算订单");
            //if (amount.Count > 1)
            //    throw new FailedException("订单中心存在多条未结订单,请取消订单或重新查询代缴费订单");
            //var jsje = amount.FirstOrDefault();
            //if (jsje != req.TotalAmount)
            //    throw new FailedException("收费金额异常，HIS为：【" + jsje + "】传入金额为：【" + req.TotalAmount + "】");

            //if (ghEntity.brxz != ((int)EnumBrxz.zf).ToString() && req.PatType != ((int)EnumBrxz.zf).ToString())
            //{
            //    //走医保
            //    throw new FailedException("医保收费未开通");
            //}
            //OutpatientSettFeeRelatedDTO feeRelated = new OutpatientSettFeeRelatedDTO()
            //{
            //    zje = Convert.ToDecimal(req.TotalAmount),
            //    ssk = Convert.ToDecimal(req.PayFee),
            //    zffs1 = (req.AppID == "Alipay" ? "10" : "11"),//11微信,10支付宝
            //    zfje1 = Convert.ToDecimal(req.PayFee),
            //    zhaoling = 0
            //};
            //BasicInfoDto2018 patInfo =getPatInfo(ghEntity.mzh, orgId);
            //var cfnmlist = getCfnmList(ghEntity.mzh,orgId);
            //if (!(cfnmlist != null || cfnmlist.Count > 0))
            //{
            //    throw new FailedException("该门诊号无可收费数据");
            //}
            //patInfo.fph = _OutPatientDmnService.GetInvoiceListByEmpNo(req.AppID, orgId);
            //string outTradeNo = "";
            //IList<int> jsnmList;
            //OutpatientSettGAYbFeeRelatedDTO ybfeeRelated = new OutpatientSettGAYbFeeRelatedDTO();
            //S25ResponseDTO xnhybfeeRelated = new S25ResponseDTO();
            // var resultnewjs = _outPatChargeApp.submitOutpatCharge(patInfo, feeRelated, ybfeeRelated, xnhybfeeRelated, null, req.HospitalID, cfnmList, out jsnmList, extxmnmList, outTradeNo);

            #endregion
            string sql = @" update Xt_Order set OrderStatus=@status,PayType=@paytype,TradeNo=@tradeno,PaymentDateTime=@paydate
                           ,RealityTotalAmount=@payamount,jsnm=@jsnm,LastModifyTime=GETDATE(),LastModifierCode=@modifyCode
                           where OutTradeNo=@OutTradeNo ";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            pars.Add(new SqlParameter("@status", ((int)EnumOrderStatus.yzf).ToString()));
            pars.Add(new SqlParameter("@paytype", req.AppID == "Alipay" ? 10 : 11));
            pars.Add(new SqlParameter("@tradeno", req.PayLsh));
            pars.Add(new SqlParameter("@paydate", req.PayTime));
            pars.Add(new SqlParameter("@payamount", req.PayFee));
            pars.Add(new SqlParameter("@jsnm", jsnm));
            pars.Add(new SqlParameter("@modifyCode", req.AppID));
            pars.Add(new SqlParameter("@OutTradeNo", req.OrderNo));
            var i = this.ExecuteSqlCommand(sql, pars.ToArray());

        }
        /// <summary>
        /// 对账信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public List<ContrastBill> ContrastBill(ContrastBillReq req)
        {
            var orgId = ConfigurationManager.AppSettings[req.HospitalID];
            Pagination pt = new Pagination();
            string sql = @" select IDCard,PatientName,PayTime,TradeStatus,PayLsh,OrderNo,Amount,PayAmount from( 
select mzjs.CreateTime,
mzjs.zjh IDCard ,mzjs.xm PatientName,zf.PaymentDateTime  PayTime,
 '1' TradeStatus,zf.TradeNo PayLsh,zf.OutTradeNo OrderNo, zf.totalAmount Amount,zf.RealityTotalAmount PayAmount
 from [dbo].[mz_js] mzjs 
join [dbo].Xt_Order zf on  mzjs.jsnm=zf.jsnm and zf.OrganizeId=mzjs.OrganizeId and zf.zt=1
left join [dbo].[xt_brjbxx] brjbxx on brjbxx.patid=mzjs.patid and brjbxx.OrganizeId=mzjs.OrganizeId and brjbxx.zt=1
where mzjs.zt=1 and jszt=1 and mzjs.OrganizeId=@orgId ";

            string tksql = @" select mzjs.CreateTime,
 mzjs.zjh IDCard ,mzjs.xm PatientName,RefunDate PayTime,
 '2' TradeStatus,tk.TradeNo PayLsh,zf.OutTradeNo OutTradeNo,RefundAmount Amount,RefundFee PayAmount
 from [dbo].[mz_js] mzjs
join  [dbo].Xt_OrderRefund tk on tk.tkjsnm=mzjs.jsnm
left join [dbo].[xt_brjbxx] brxx on brxx.patid=mzjs.patid 
left join  [dbo].[Xt_Order] zf on zf.ID=tk.orderId
where mzjs.zt=1 and jszt=1 and mzjs.OrganizeId=@orgId ";

            string ghsql = @" select ghyy.CreateTime,jbxx.zjh,jbxx.xm,ghyy.PayTime,case yyzt when '2' then '1' else '2' end TradeStatus ,PayLsh,'' OutTradeNo,RegFee,PayFee
 from mz_gh_book ghyy
join mz_gh gh on gh.mzh=ghyy.mzh and gh.OrganizeId=ghyy.OrganizeId and gh.zt=1
join xt_brjbxx jbxx on jbxx.patid=gh.patid and jbxx.OrganizeId=gh.OrganizeId  and jbxx.zt=1
where ghyy.zt=1 and ghyy.yyzt in ('2','5') and ghyy.OrganizeId=@orgId and ghyy.AppId=@appId ";
            //pt.page = req.PageNo==0 ? 1 : req.PageNo;
            //pt.rows = req.PageSize==0 ? 100 : req.PageSize;
            //pt.sidx = " PayTime ";
            //pt.sord = " desc";
            var pars = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(req.Begindate.ToString()))
            {
                sql += "  and mzjs.createtime>= @createtime ";
                tksql += "  and mzjs.createtime>= @createtime ";
                ghsql += "  and ghyy.createtime>= @createtime ";
                pars.Add(new SqlParameter("@createtime", req.Begindate));

            }
            if (!string.IsNullOrWhiteSpace(req.EndDate.ToString()))
            {
                sql += "  and mzjs.createtime<=@EndDate ";
                tksql += "  and mzjs.createtime<=@EndDate ";
                ghsql += "  and ghyy.createtime<= @EndDate ";
                pars.Add(new SqlParameter("@EndDate", req.EndDate));
            }
            if (!string.IsNullOrWhiteSpace(req.Mzh))
            {
                sql += "  and zf.mzzyh=@mzzyh";
                tksql += "  and zf.mzzyh=@mzzyh";
                ghsql += "  and ghyy.mzh=@mzzyh";
                pars.Add(new SqlParameter("@mzzyh", req.Mzh));
            }
            if (!string.IsNullOrWhiteSpace(req.CardNo))
            {
                sql += "  and zf.CardNo= @CardNo";
                tksql += "  and zf.CardNo= @CardNo";
                ghsql += "  and gh.kh= @CardNo";
                pars.Add(new SqlParameter("@CardNo", req.CardNo));
            }
            if (!string.IsNullOrWhiteSpace(req.PayLsh))
            {
                sql += "  and zf.TradeNo= @PayLsh";
                tksql += "  and zf.TradeNo= @PayLsh";
                ghsql += "  and ghyy.PayLsh=@PayLsh";
                pars.Add(new SqlParameter("@PayLsh", req.PayLsh));
            }
            if (!string.IsNullOrWhiteSpace(req.OrderNo))
            {
                sql += "  and zf.OutTradeNo= @OrderNo ";
                tksql += "  and zf.OutTradeNo= @OrderNo";
                ghsql += "  and 1=2";
                pars.Add(new SqlParameter("@OrderNo", req.OrderNo));
            }
            sql += " union all " + tksql+" union all "+ghsql;

            sql += "  ) d ";
            pars.Add(new SqlParameter("@orgId", orgId));
            pars.Add(new SqlParameter("@appId", req.AppID));
            //var hospitalTradeList = this.QueryWithPage<ContrastBill>(sql, pt, pars.ToArray()).ToList();
            var hospitalTradeList = this.FindList<ContrastBill>(sql,pars.ToArray());
            return hospitalTradeList;
            //return new ContrastBillResp
            //{
            //    contrastBill = hospitalTradeList,
            //    PageNo=pt.page,
            //    PageSize=pt.rows,
            //    Record=pt.records
            //};
        }
        /// <summary>
        /// 挂号预结算验证
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string ValidPreSett(PreSettReq req)
        {
            MzghbookEntity bookRecord = new MzghbookEntity();
            string message = "";
            int patid = -1;
            int qzjzxh = -1;
            string orgId = ConfigurationManager.AppSettings[req.HospitalID];
            if (!string.IsNullOrWhiteSpace(req.BookID.ToString()))
            {
                string yyzt = req.IsBooking == "N" ? ((int)EnumMzyyzt.bookreg).ToString() : ((int)EnumMzyyzt.book).ToString();
                DateTime datevalid = DateTime.Now.Date;
                bookRecord = _MzghbookRepo.FindEntity(p =>
                    p.BookId == req.BookID && p.yyzt == yyzt && p.zt == "1" && p.OrganizeId == orgId &&
                    p.OutDate >= datevalid);
                if (bookRecord != null)
                {
                    patid = Convert.ToInt32(bookRecord.patid);
                    qzjzxh = bookRecord.QueueNo;
                    if (req.CardNo != bookRecord.kh)
                    {
                        message = req.IsBooking == "N" ? "卡信息异常,请重新挂号！" : "卡信息异常，请确认预约信息与患者是否一致";
                        throw new FailedException(message);
                    }
                }
                else
                {
                    message = req.IsBooking == "N" ? "挂号失败，请重试" : "未找到预约号【" + req.BookID + "】,请确认预约状态及日期是否有效";
                    throw new FailedException(message);
                }
                var brxxEntiey = GetPatInfo(req.HospitalID, req.CardNo, null, null).FirstOrDefault();
                if (brxxEntiey != null)
                {
                    if (patid != brxxEntiey.patid)
                    {
                        throw new FailedException("预约信息与卡信息不符");
                    }
                }
                else
                {
                    throw new FailedException("当前卡号【" + req.CardNo + "】在HIS中不存在！");
                }

            }
            //获取门诊号
            MzKsPbDto pbxx = new MzKsPbDto();
            var scheduId = Convert.ToInt32(bookRecord.ScheduId);
            pbxx.HospitalID = req.HospitalID;
            pbxx.ScheduId = scheduId;
            var pbEntity = GetMzpbDetail(pbxx).FirstOrDefault();
            if (pbEntity == null)
                throw new FailedException("该排班无效，请重新获取排班！");
            return bookRecord.ghxz;
        }
        /// <summary>
        /// 处方费用是否发生变化
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string ValidFee(string mzh,string orgId)
        {
            //考虑医保上传唯一流水号使用明细Id 用处方或项目明细Id判断处方是否发生变化
            string sql = @"
SELECT cast(a.cfmxId as varchar) cfmxId from(
SELECT  b.yp SubItemCode ,
        c.ypmc SubItemName ,a.cfh,
        CONVERT(VARCHAR(10), b.dj) Price ,
        CONVERT(VARCHAR(10), b.sl) Number ,
        CONVERT(VARCHAR(10),b.je) Amount ,
        CONVERT(VARCHAR(10),b.cfnm) ItemCode ,
        a.cflxmc ItemType,cfmxId ,a.organizeId
FROM    NewtouchHIS_Sett..mz_cf a with(nolock)
        RIGHT JOIN NewtouchHIS_Sett..mz_cfmx b with(nolock) ON b.cfnm = a.cfnm
                                                    AND b.OrganizeId = a.OrganizeId
                                                    AND b.zt = '1'
        LEFT JOIN NewtouchHIS_Base..V_C_xt_yp c ON c.OrganizeId = a.OrganizeId
                                                    AND b.yp = c.ypCode
        LEFT JOIN NewtouchHIS_Sett..mz_gh gh with(nolock) on gh.ghnm=a.ghnm and gh.OrganizeId=a.OrganizeId
WHERE   a.cfzt = '0' AND A.ZT=1  
        AND gh.mzh = @mzh 
        AND a.OrganizeId=@orgId
UNION ALL
SELECT b.sfxm SubItemCode ,
        c.sfxmmc SubItemName ,cf.cfh,
        CONVERT(VARCHAR(10), b.dj) Price ,
        CONVERT(VARCHAR(10), b.sl) Number ,
        CONVERT(VARCHAR(10), b.je) Amount ,
        CONVERT(VARCHAR(10), b.cfnm) ItemCode ,
        '项目处方' ItemType,b.xmnm,b.organizeId
 FROM   NewtouchHIS_Sett..mz_xm b with(nolock)
        LEFT JOIN NewtouchHIS_Sett..mz_cf cf with(nolock) on cf.cfnm=b.cfnm and cf.OrganizeId=b.OrganizeId and cf.zt=1
        LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm c ON c.OrganizeId = b.OrganizeId
                                                     AND b.sfxm = c.sfxmCode
		left join NewtouchHIS_Sett..mz_gh gh with(nolock) on gh.ghnm=b.ghnm and gh.OrganizeId=b.OrganizeId
 WHERE  b.xmzt = '0' AND b.zt=1
        AND gh.mzh = @mzh
        AND b.je != 0
        AND b.OrganizeId = @orgId
		) a 
Full  join (
SELECT  XmCode,Xmmc,cfh,Price,Num,Amount,cfmxid ,a.organizeId
From    Xt_Order  a with(nolock)
		join Xt_OrderDetail b with(nolock) on b.orderId=a.Id and a.organizeId=b.organizeId
where   a.zt=1 and a.mzzyh=@mzh and (orderstatus='0' or orderstatus='3') and a.OrganizeId=@orgId) b
		on a.cfmxId=b.cfmxId and a.organizeId=b.organizeId
where   a.cfmxId is null or b.cfmxId is null";
            return this.FirstOrDefault<string>(sql,
                new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@Mzh", mzh) });

        }
        /// <summary>
        /// 待缴订单总金额
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<CfOrderVo> GetOrderAmount(string mzh,string orgId)
        {
            string sql = @" select TotalAmount,orderstatus from [dbo].Xt_Order a with(nolock) where  a.zt=1 and a.mzzyh=@mzh 
and (orderstatus=@status1 or orderstatus=@status2)
and a.OrganizeId=@orgId  ";
            return this.FindList<CfOrderVo>(sql,
                new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@mzh", mzh)
                , new SqlParameter("@status1", ((int)EnumOrderStatus.dzf).ToString())
                , new SqlParameter("@status2", ((int)EnumOrderStatus.zfz).ToString())});
        }
        /// <summary>
        /// 获取待缴处方内码
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<int> getCfnmList(string mzh, string orgId)
        {
            string sql = @" 
select distinct cfnm
 from Xt_Order  a with(nolock)
join Xt_OrderDetail b with(nolock) on b.orderId=a.Id and a.organizeId=b.organizeId
where a.zt=1 and (orderstatus='0' or orderstatus='3') and a.mzzyh=@mzh  and a.OrganizeId=@orgId ";

            return this.FindList<int>(sql,
               new[] { new SqlParameter("@mzh", mzh), new SqlParameter("@orgId", orgId) });
        }
        /// <summary>
        /// 获取病人基本信息
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public BasicInfoDto2018 getPatInfo(string mzh, string orgId)
        {
            var sql = @"       SELECT   b.xm ,
                                        b.xb ,
                                        b.csny ,
                                        b.zjh ,
                                        b.zjlx ,
                                        b.blh ,
                                        b.patid ,
                                        a.brxz ,
                                        a.mzh ,
                                        a.ghnm ,
                                        a.ys ,
                                        NULL sfrq ,
                                        NULL fph ,
                                        CONVERT(BIT,0) isQfyj
                               FROM     NewtouchHIS_Sett..mz_gh a
                                        LEFT JOIN NewtouchHIS_Sett..xt_brjbxx b ON b.OrganizeId = a.OrganizeId
                                                                                   AND b.patid = a.patid
                                                                                   AND b.zt = '1'
                               WHERE    a.mzh = @mzh
                                        AND a.OrganizeId = @orgId ";
            return this.FirstOrDefault<BasicInfoDto2018>(sql,
                new[] { new SqlParameter("@mzh", mzh), new SqlParameter("@orgId", orgId) });
        }
    }
}
