using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Common.Web;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.EMR.Domain.DTO;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Domain.ValueObjects;
using Newtouch.EMR.Infrastructure;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.EMR.Domain.BusinessObjects;

namespace Newtouch.EMR.DomainServices
{
    public class ZybrjbxxDmnService : DmnServiceBase, IZybrjbxxDmnService
    {
        private readonly IZybrjbxxRepo _ZybrjbxxRepo;
        private readonly IZymeddocsrelationRepo _ZymeddocsrelationRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly Ibl_ryblRepo _ryblRepo;
        private readonly Ibl_hljlRepo _hljlRepo;
        private readonly Ibl_bcjlRepo _bcjlRepo;
        private readonly Ibl_zqwsRepo _zqwsRepo;
        private readonly ICommonDmnService _CommonDmnService;
        private readonly Ibl_bllxRepo _BllxRepo;

        private readonly IHljlDataRepo _hljlDataRepo;
        private readonly IBlmblbRepo _blmblbRepo;

        public ZybrjbxxDmnService(IDefaultDatabaseFactory databaseFactory, IZybrjbxxRepo ZybrjbxxRepo, IZymeddocsrelationRepo ZymeddocsrelationRepo,
            ISysConfigRepo SysConfigRepo, Ibl_ryblRepo ryblRepo, Ibl_hljlRepo hljlRepo, Ibl_bcjlRepo bcjlRepo, Ibl_zqwsRepo zqwsRepo, ICommonDmnService CommonDmnService)
            : base(databaseFactory)
        {
            this._ZybrjbxxRepo = ZybrjbxxRepo;
            this._ZymeddocsrelationRepo = ZymeddocsrelationRepo;
            this._sysConfigRepo = SysConfigRepo;
            this._ryblRepo = ryblRepo;
            this._bcjlRepo = bcjlRepo;
            this._hljlRepo = hljlRepo;
            this._zqwsRepo = zqwsRepo;
            this._CommonDmnService = CommonDmnService;
        }

        /// <summary>
        ///  同步his病人信息接口（API）
        /// </summary>
        /// <param name="lastUpdate"></param>
        /// <param name="user"></param>
        public void Sync_HisPatinfo(DateTime lastUpdate, OperatorModel user)
        {
            var reqObj = new
            {
                TimeStamp = DateTime.Now.ToString(),
                Token = SiteHispatinfoAPIHelper.GetToken(),
                lastUpdateTime = "2018-01-01 00:00:00.000",
                pagination = new
                {
                    page = 1,
                    rows = 2000,
                    sidx = "UpdateTime"

                }
            };

            var apiResp = SiteHispatinfoAPIHelper.Request<APIRequestHelper.DefaultResponse<HisPatinfoVO>>(
                "/api/patient/InPatientQuery", reqObj.ToJson());

            if (apiResp.code == APIRequestHelper.ResponseResultCode.SUCCESS && apiResp.data != null)
            {
                ZybrjbxxEntity ety = new ZybrjbxxEntity();
                foreach (var item in apiResp.data.List)
                {
                    ety.BedCode = "";
                    ety.birth = item.csny;
                    ety.blh = item.blh;
                    ety.brxzdm = item.brxz;
                    ety.brxzmc = item.brxzmc;
                    ety.cardno = item.kh;
                    ety.cardtype = item.CardType;
                    ety.CreateTime = DateTime.Now;
                    //ety.CreatorCode = "";
                    //ety.cyfs = "";
                    //ety.cyzddm = "";
                    //ety.cyzdmc = "";
                    ety.DeptCode = item.ks;
                    //ety.gdxmzxrq = null;
                    ety.hljb = null;
                    //ety.Id = null;
                    //ety.LastModifierCode = null;
                    //ety.LastModifyTime = null;
                    ety.lxr = null;
                    ety.lxrdh = null;
                    ety.lxrgx = null;
                    ety.Memo = null;
                    ety.OrganizeId = user.OrganizeId;
                    ety.py = item.py;
                    ety.rqrq = item.ryrq;
                    ety.ryrq = item.ryrq;
                    ety.sex = item.sex;
                    ety.sfqj = null;
                    ety.sfzh = item.idCardNo;
                    ety.WardCode = item.bqCode;
                    ety.wb = item.wb;
                    ety.wzjb = null;
                    ety.xm = item.xm;
                    ety.ysgh = item.ys;
                    //ety.zddm=item.
                    ety.zddm = item.zzdCode;
                    ety.zdmc = item.zzdmc;
                    ety.zt = "1";
                    ety.zybz = item.zybz;
                    ety.zyh = item.zyh;

                    _ZybrjbxxRepo.SubmitForm(ety, "");
                }

            }
        }
        /// <summary>
        /// 同步his患者信息（数据库）
        /// </summary>
        /// <param name="OrgId"></param>
        /// <param name="Bdate"></param>
        /// <param name="Edate"></param>
        public void Sync_HisPatinfo(string OrgId, DateTime Bdate, DateTime Edate)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[] {
                            new SqlParameter("@BeginDate", Bdate),
                            new SqlParameter("@EndDate", Edate),
                            new SqlParameter("@OrgId", OrgId)
                };

                string sql = "exec  usp_Sync_PatinfoCPOE @BeginDate=@BeginDate,@EndDate=@EndDate,@OrgId=@OrgId ";
                DataTable dt = SqlQueryForDataTatable(sql, para);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Flag"].ToString() == "0")
                    {
                        throw new FailedException("同步患者信息失败，" + dt.Rows[0]["Msg"].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                throw new FailedException("同步患者信息失败，" + ex.InnerException);
            }
        }

        public List<PatientzbqResponseDto> GetzbqPatientList(PatientzbqRequestDto req, string OrgId)
        {
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            int wtj = (int)EnumRecordStu.wtj;

            sqlstr = sqlstr.Append(@"SELECT  zyxx.id ,
                        cw.cwmc ,
                        zyxx.xm ,
                        zyxx.sex ,
                        (case when zyxx.birth is null then 0 else (CAST(FLOOR(DATEDIFF(DY, zyxx.birth, GETDATE()) / 365.25) AS INT ) )end) age ,
                        zyxx.zyh ,
                        zyxx.ryrq ,
                        zyxx.hljb ,
                        zyxx.wzjb ,
                        ISNULL(CAST(zyxx.sfqj AS VARCHAR(50)) , '') brzt,
                        ISNULL(staff.Name,'') ysmc,
		                zyxx.brxzmc,
		                zyxx.zdmc,
                        isnull(RecordStu," + wtj.ToString() + @") RecordStu,CommitTime,Commitor,xx.gms
                FROM    dbo.zy_brjbxx zyxx
                        INNER JOIN NewtouchHIS_Base..V_S_xt_bq bq ON bq.bqCode = zyxx.WardCode
                                                                        AND bq.OrganizeId = zyxx.OrganizeId
                        left join NewtouchHIS_Sett..zy_brjbxx xx on zyxx.zyh=xx.zyh 
                                                                    and zyxx.OrganizeId =xx.OrganizeId 
                                                                    and xx.zt=1
                        LEFT JOIN NewtouchHIS_Base..V_S_xt_cw cw ON cw.bqCode = zyxx.WardCode
                                                                    AND cw.cwCode = zyxx.BedCode
                                                                    AND cw.OrganizeId = zyxx.OrganizeId and cw.sfzy=1
                        LEFT JOIN NewtouchHIS_Base..V_S_Sys_Staff staff ON staff.gh = zyxx.ysgh
                                                                            AND staff.OrganizeId = zyxx.OrganizeId
                WHERE   zyxx.OrganizeId=@OrgId ");
            par.Add(new SqlParameter("@OrgId", OrgId));
            if (req != null)
            {
                if (!string.IsNullOrWhiteSpace(req.bqcode))
                {
                    sqlstr.Append("  AND zyxx.WardCode=@wardcode");
                    par.Add(new SqlParameter("@wardcode", req.bqcode));
                    if (!string.IsNullOrWhiteSpace(req.cw))
                    {
                        sqlstr.Append("  AND (cw.cwmc like '%'+@cw+'%' OR ISNULL(@cw, '') = ''  or zyxx.xm like '%'+@cw+'%' or zyxx.zyh =@cw)");
                        par.Add(new SqlParameter("@cw", req.cw));
                    }
                    sqlstr.Append(@"        AND ( (staff.zt = '1')
                                        OR ISNULL(@ysgh, '') = '')");
                    par.Add(new SqlParameter("@ysgh", req.ysgh));
                }
            }
            sqlstr.Append(" and zyxx.zt='1' and cw.zt='1'");
            sqlstr.Append(" and zyxx.zybz=@zybz");
            par.Add(new SqlParameter("@zybz", (int)EnumZYBZ.Bqz));
            return this.QueryWithPage<PatientzbqResponseDto>(sqlstr.ToString(), req.pagination, par.ToArray()).ToList();
        }

        /// <summary>
        /// 获取已出区的病人对象集合
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public List<PatientycqResponseDto> GetycqPatientList(PatientycqRequestDto req, string OrgId)
        {
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            int wtj = (int)EnumRecordStu.wtj;

            sqlstr = sqlstr.Append(@"SELECT  zyxx.id ,
        zyxx.xm ,
        zyxx.zyh ,
        zyxx.cardno ,
        cw.cwmc ,
        zyxx.sex ,
        (case when zyxx.birth is null then 0 else (CAST(FLOOR(DATEDIFF(DY, zyxx.birth, GETDATE()) / 365.25) AS INT ) )end) age ,
        zyxx.ryrq ,
        zyxx.cqrq ,
        ISNULL(staff.Name,'') ysmc,
		zyxx.zdmc,
	    b.nlshow,
        isnull(RecordStu," + wtj.ToString() + @") RecordStu,CommitTime,Commitor
FROM    dbo.zy_brjbxx zyxx with(nolock)
left join NewtouchHIS_Sett..zy_brjbxx b on zyxx.OrganizeId=b.OrganizeId and zyxx.zyh=b.zyh 
        INNER JOIN NewtouchHIS_Base..V_S_xt_bq bq ON bq.bqCode = zyxx.WardCode
                                                     AND bq.OrganizeId = zyxx.OrganizeId
        LEFT JOIN NewtouchHIS_Base..V_S_xt_cw cw ON cw.bqCode = zyxx.WardCode
                                                    AND cw.cwCode = zyxx.BedCode
                                                    AND cw.OrganizeId = zyxx.OrganizeId
        LEFT JOIN NewtouchHIS_Base..V_S_Sys_Staff staff ON staff.gh = zyxx.ysgh
                                                           AND staff.OrganizeId = zyxx.OrganizeId
        WHERE   zyxx.OrganizeId=@OrgId ");
            par.Add(new SqlParameter("@OrgId", OrgId));
            if (req != null)
            {
                if (!string.IsNullOrWhiteSpace(req.bqcode))
                {
                    sqlstr.Append("  AND zyxx.WardCode=@wardcode");
                    par.Add(new SqlParameter("@wardcode", req.bqcode));
                    if (!string.IsNullOrWhiteSpace(req.cw))
                    {
                        sqlstr.Append("  AND (cw.cwmc like '%'+@cw+'%' OR ISNULL(@cw, '') = ''  or zyxx.xm like '%'+@cw+'%' or zyxx.zyh =@cw)");
                        par.Add(new SqlParameter("@cw", req.cw));
                    }

                    if (req.cqksrq.HasValue && req.cqksrq != DateTime.MinValue)
                    {
                        sqlstr.Append(" AND zyxx.cqrq >= @kssj");
                        par.Add(new SqlParameter("@kssj", req.cqksrq.Value));
                    }
                    if (req.cqjsrq.HasValue && req.cqjsrq != DateTime.MinValue)
                    {
                        sqlstr.Append(" AND zyxx.cqrq < @jssj");
                        par.Add(new SqlParameter("@jssj", req.cqjsrq.Value.AddDays(1).Date));
                    }
                    if (!string.IsNullOrWhiteSpace(req.zyh))
                    {
                        sqlstr.Append(" AND zyxx.zyh=@zyh");
                        par.Add(new SqlParameter("@zyh", req.zyh));
                    }
                    sqlstr.Append(@"        AND ( (  staff.zt = '1')
                                        OR ISNULL(@ysgh, '') = '')");
                    par.Add(new SqlParameter("@ysgh", req.ysgh));
                }
            }
            sqlstr.Append(" and zyxx.zt='1' and cw.zt='1'");
            sqlstr.Append(" and zyxx.zybz in(" + (int)EnumZYBZ.Djz + "," + (int)EnumZYBZ.Ycy + ")");
            //par.Add(new SqlParameter("@zybz", (int)EnumZYBZ.Djz+","+ (int)EnumZYBZ.Ycy));
            return this.QueryWithPage<PatientycqResponseDto>(sqlstr.ToString(), req.pagination, par.ToArray()).ToList();
        }

        public List<PatientmyResponseDto> GetmyPatientList(PatientmyRequestDto req, string OrgId)
        {
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            int wtj = (int)EnumRecordStu.wtj;

            sqlstr = sqlstr.Append(@"SELECT  zyxx.id ,
        zyxx.xm ,
        zyxx.zyh ,
        zyxx.cardno ,
        cw.cwmc ,
        zyxx.sex ,
        (case when zyxx.birth is null then 0 else (CAST(FLOOR(DATEDIFF(DY, zyxx.birth, GETDATE()) / 365.25) AS INT ) )end) age ,
        zyxx.ryrq ,
        zyxx.cqrq ,
        ISNULL(staff.Name,'') ysmc,
		zyxx.zdmc,
        zyxx.zybz,
        isnull(RecordStu," + wtj.ToString() + @") RecordStu,CommitTime,Commitor
FROM    dbo.zy_brjbxx zyxx  with(nolock)
        INNER JOIN NewtouchHIS_Base..V_S_xt_bq bq ON bq.bqCode = zyxx.WardCode
                                                     AND bq.OrganizeId = zyxx.OrganizeId
        LEFT JOIN NewtouchHIS_Base..V_S_xt_cw cw ON cw.bqCode = zyxx.WardCode
                                                    AND cw.cwCode = zyxx.BedCode
                                                    AND cw.OrganizeId = zyxx.OrganizeId and cw.sfzy=1
        LEFT JOIN NewtouchHIS_Base..V_S_Sys_Staff staff ON staff.gh = zyxx.ysgh
                                                           AND staff.OrganizeId = zyxx.OrganizeId
        WHERE  zyxx.OrganizeId=@OrgId ");
            par.Add(new SqlParameter("@OrgId", OrgId));
            if (req != null)
            {
                if (!string.IsNullOrWhiteSpace(req.bqcode))
                {
                    sqlstr.Append("  AND zyxx.WardCode=@wardcode");
                    par.Add(new SqlParameter("@wardcode", req.bqcode));
                    if (!string.IsNullOrWhiteSpace(req.cw))
                    {
                        sqlstr.Append("  AND (cw.cwmc like @cw OR ISNULL(@cw, '') = '')");
                        par.Add(new SqlParameter("@cw", "%" + req.cw + "%"));
                    }

                    //if (req.cqksrq.HasValue && req.cqksrq != DateTime.MinValue)
                    //{
                    //    sqlstr.Append(" AND zyxx.cqrq >= @kssj");
                    //    par.Add(new SqlParameter("@kssj", req.cqksrq.Value));
                    //}
                    //if (req.cqjsrq.HasValue && req.cqjsrq != DateTime.MinValue)
                    //{
                    //    sqlstr.Append(" AND zyxx.cqrq < @jssj");
                    //    par.Add(new SqlParameter("@jssj", req.cqjsrq.Value.AddDays(1).Date));
                    //}
                    if (!string.IsNullOrWhiteSpace(req.zyh))
                    {
                        sqlstr.Append(" AND zyxx.zyh=@zyh");
                        par.Add(new SqlParameter("@zyh", req.zyh));
                    }
                    sqlstr.Append(@"        AND ( ( zyxx.ysgh = @ysgh
                                        AND staff.zt = '1')
                                        OR ISNULL(@ysgh, '') = '')");
                    par.Add(new SqlParameter("@ysgh", req.ysgh));
                }
            }
            sqlstr.Append(" and zyxx.zt='1' and cw.zt='1'");
            //sqlstr.Append(" and zyxx.zybz=@zybz");
            //par.Add(new SqlParameter("@zybz", (int)EnumZYBZ.Djz));
            return this.QueryWithPage<PatientmyResponseDto>(sqlstr.ToString(), req.pagination, par.ToArray()).ToList();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public List<PatientmyResponseDto> GetPatInfoBykeyword(string keyword, string orgId)
        {
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            int wtj = (int)EnumRecordStu.wtj;

            sqlstr = sqlstr.Append(@"SELECT  id Id ,
            zyxx.zyh ,
            zyxx. xm ,
            sex ,
            (case when birth is null then 0 else (CAST(FLOOR(DATEDIFF(DY, birth, GETDATE()) / 365.25) AS INT ) )end) age ,                        brxzmc ,
	        b.nlshow,
            zyxx.ryrq,
            isnull(zyxx.blh,'')blh,
            isnull(RecordStu," + wtj.ToString() + @") RecordStu,CommitTime,Commitor
            FROM    zy_brjbxx zyxx with(nolock)  left join NewtouchHIS_Sett..zy_brjbxx b on zyxx.OrganizeId=b.OrganizeId and zyxx.zyh=b.zyh 
            WHERE   zyxx.OrganizeId = @orgId
                    AND zyxx.zt = '1'
                    AND ( zyxx.zyh LIKE @keyword
                            OR zyxx.xm LIKE @keyword
                            OR zyxx.cardno LIKE @keyword)");
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@keyword", "%" + keyword + "%"));
            return this.FindList<PatientmyResponseDto>(sqlstr.ToString(), par.ToArray());
        }

        /// <summary>
        /// 获取患者病历树
        /// </summary>
        /// <param name="OrgId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<PatMedRecordTreeVO> GetPatMedRecordTree(string OrgId, string zyh, string rygh)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[] {
                            new SqlParameter("@zyh", zyh),
                            new SqlParameter("@OrgId", OrgId),
                            new SqlParameter("@rygh", rygh)
                };

                string sql = "exec  usp_Pat_MedRecordTree @OrgId=@OrgId,@zyh=@zyh,@rygh=@rygh ";
                return this.FindList<PatMedRecordTreeVO>(sql, para);

            }
            catch (Exception ex)
            {
                throw new FailedException("获取病历列表异常，" + ex.InnerException);
            }
        }

        public IList<MedRecordTypeVO> GetSysItemDic(string OrgId, string Code, string bllxId)
        {
            string sql = @"select Id,Name,Code,px
                        from [NewtouchHIS_Base].[dbo].[Sys_ItemsDetail] a with(nolock)
                        where exists(select 1 from [NewtouchHIS_Base].[dbo].[Sys_Items] b with(nolock)
			                        where a.itemid=b.id and b.code=@code and b.zt=1)
                        and a.zt=1";

            if (!string.IsNullOrWhiteSpace(bllxId))
            {
                sql += " and Id=@Id ";
            }

            return this.FindList<MedRecordTypeVO>(sql, new SqlParameter[] {
                            new SqlParameter("@OrgId", OrgId),
                            new SqlParameter("@code", Code),
                            new SqlParameter("@Id", bllxId==null?"":bllxId)
                });
        }

        /// <summary>
        /// 获取病人列表
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="OrgId"></param>
        /// <param name="type"></param>
        /// <param name="brzt"></param>
        /// <returns></returns>
        public IList<PatListVO> GetPatList(Pagination pagination, string keyword, string zyh,
            string cyts, string blzt, string ysgh, string OrgId, int type, string bq = null, string appId = null)
        {
            string sql = "";
            var para = new List<SqlParameter>();
            para.Add(new SqlParameter("@OrgId", OrgId));
            para.Add(new SqlParameter("@ysgh", ysgh));

            int wtj = (int)EnumRecordStu.wtj;

            if (!string.IsNullOrWhiteSpace(zyh))
            {
                sql = @"SELECT [Id],a.[OrganizeId],[zyh],[blh],[xm],a.[py],[wb],[sfzh],[sex],[birth],[zybz],[sfqj],[DeptCode]
                ,[WardCode],[ysgh],[BedCode],[ryrq],[rqrq],[cqrq],[wzjb],[hljb],[ryfs],[cyfs],[gdxmzxrq],[brxzdm],[brxzmc]
                ,[cardno],[cardtype],[lxr],[lxrgx],[lxrdh],[zddm],[zdmc],[cyzddm],[cyzdmc],[Memo],[CreateTime]
                ,[CreatorCode],[LastModifyTime],[LastModifierCode],a.[zt],(case when zyh=@zyh then 1 else 0 end ) isCheck,
                c.cwmc,d.bqmc,isnull(RecordStu," + wtj.ToString() + @") RecordStu,CommitTime,Commitor
                ,(case when cqrq is not null then datediff(dd,cqrq,getdate()) else null end) cyts
                ,isnull(RecordStu," + wtj.ToString() + @") RecordStusm
                FROM [dbo].[zy_brjbxx] a  with(nolock)
                left join NewtouchHIS_Base.dbo.V_S_xt_cw c with(nolock) on a.organizeid=c.organizeid and a.bedcode=c.cwcode and c.zt=1  
                left join NewtouchHIS_Base..V_S_xt_bq  d with(nolock) on a.organizeid=d.organizeid and a.wardcode=d.bqcode and d.zt=1
                where a.zt=1 and a.[OrganizeId]=@OrgId
                and exists(select 1 from NewtouchHIS_Base..V_C_Sys_StaffWard b with(nolock) 
		                where a.organizeid=b.organizeid and b.staffgh=@ysgh and b.bqCode=a.WardCode and b.zt=1) ";

                para.Add(new SqlParameter("@zyh", zyh));
            }
            else
            {
                sql = @"SELECT [Id],a.[OrganizeId],[zyh],[blh],[xm],a.[py],[wb],[sfzh],[sex],[birth],[zybz],[sfqj],[DeptCode]
                ,[WardCode],[ysgh],[BedCode],[ryrq],[rqrq],[cqrq],[wzjb],[hljb],[ryfs],[cyfs],[gdxmzxrq],[brxzdm],[brxzmc]
                ,[cardno],[cardtype],[lxr],[lxrgx],[lxrdh],[zddm],[zdmc],[cyzddm],[cyzdmc],[Memo],[CreateTime]
                ,[CreatorCode],[LastModifyTime],[LastModifierCode],a.[zt],0 isCheck,
                c.cwmc,d.bqmc,isnull(RecordStu," + wtj.ToString() + @") RecordStu,CommitTime,Commitor
                ,(case when cqrq is not null then datediff(dd,cqrq,getdate()) else null end) cyts   
                ,isnull(RecordStu," + wtj.ToString() + @") RecordStusm
                FROM [dbo].[zy_brjbxx] a  with(nolock)
                left join NewtouchHIS_Base.dbo.V_S_xt_cw c with(nolock) on a.organizeid=c.organizeid and a.bedcode=c.cwcode and c.zt=1  
                left join NewtouchHIS_Base..V_S_xt_bq  d with(nolock) on a.organizeid=d.organizeid and a.wardcode=d.bqcode and d.zt=1
                where a.zt=1 and a.[OrganizeId]=@OrgId 
                and exists(select 1 from NewtouchHIS_Base..V_C_Sys_StaffWard b with(nolock) 
		            where a.organizeid=b.organizeid and b.staffgh=@ysgh and b.bqCode=a.WardCode  and b.zt=1) ";

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    sql += " and (zyh=@keyword or charindex(@keyword,xm)>0) ";
                    para.Add(new SqlParameter("@keyword", keyword));
                }
            }
            if (!string.IsNullOrWhiteSpace(bq))
            {
                sql += " and d.bqcode=@bq ";
                para.Add(new SqlParameter("@bq", bq));
            }

            //在院
            if (type == Convert.ToInt32(EnumZYBZ.Bqz))
            {
                sql += " and zybz in(" + Convert.ToInt32(EnumZYBZ.Bqz) + "," + Convert.ToInt32(EnumZYBZ.Zq) + ")";

            }
            //出院（已结账、待结账）
            else if (type == Convert.ToInt32(EnumZYBZ.Ycy) || type == Convert.ToInt32(EnumZYBZ.Djz))
            {
                sql += " and zybz in(" + Convert.ToInt32(EnumZYBZ.Ycy) + "," + Convert.ToInt32(EnumZYBZ.Djz) + ") ";
            }
            else if (type == 0)
            {
                if (!string.IsNullOrWhiteSpace(ysgh))
                {
                    sql += " and a.ysgh=@ysgh ";
                }

            }
            //是否提交
            if (!string.IsNullOrWhiteSpace(appId) && appId == "MRMS")
            {
                sql += " and isnull(RecordStu,0)=@blzt ";
                para.Add(new SqlParameter("@blzt", (int)EnumRecordStu.yqs));
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(blzt))
                {
                    sql += " and isnull(RecordStu," + wtj.ToString() + @")=@blzt ";
                    para.Add(new SqlParameter("@blzt", blzt));
                }
            }

            if (!string.IsNullOrWhiteSpace(cyts))
            {
                int ts = Convert.ToInt16(cyts);
                if (ts > 0)
                {
                    string rq = DateTime.Now.AddDays(-ts + 1).ToString("yyyy-MM-dd");
                    sql += " and cqrq<='" + rq + "' ";
                }

            }

            return this.QueryWithPage<PatListVO>(sql, pagination, para.ToArray()).ToList();
        }

        public IList<PatListVO> GetMyPatList(Pagination pagination, string keyword, string ysgh, string OrgId)
        {
            string sql = "";
            var para = new List<SqlParameter>();
            para.Add(new SqlParameter("@OrgId", OrgId));

            int wtj = (int)EnumRecordStu.wtj;
            sql = @"SELECT [Id],[OrganizeId],[zyh],[blh],[xm],[py],[wb],[sfzh],[sex],[birth],[zybz],[sfqj],[DeptCode]
                            ,[WardCode],[ysgh],[BedCode],[ryrq],[rqrq],[cqrq],[wzjb],[hljb],[ryfs],[cyfs],[gdxmzxrq],[brxzdm],[brxzmc]
                            ,[cardno],[cardtype],[lxr],[lxrgx],[lxrdh],[zddm],[zdmc],[cyzddm],[cyzdmc],[Memo],[CreateTime]
                            ,[CreatorCode],[LastModifyTime],[LastModifierCode],[zt],0 isCheck,
                            isnull(RecordStu," + wtj.ToString() + @") RecordStu,CommitTime,Commitor
                            FROM [dbo].[zy_brjbxx] with(nolock)
                            where zt=1 and [OrganizeId]=@OrgId  ";

            if (!string.IsNullOrWhiteSpace(ysgh))
            {
                sql += " and ysgh=@ysgh ";
                para.Add(new SqlParameter("@ysgh", ysgh));
            }

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (zyh=@keyword or charindex(@keyword,xm)>0) ";
                para.Add(new SqlParameter("@keyword", keyword));
            }

            return this.QueryWithPage<PatListVO>(sql, pagination, para.ToArray()).ToList();
        }

        #region 病历操作


        /// <summary>
        /// 删除病历（逻辑删除）
        /// </summary>
        /// <param name="blId"></param>
        /// <param name="blgxId">病历关系Id</param>
        /// <param name="OrgId"></param>
        /// <param name="user"></param>
        public void UpdtPatMedRecord(string blId, string blgxId, string OrgId, OperatorModel user)
        {
            var gxEty = _ZymeddocsrelationRepo.FindEntity(p => p.Id == blgxId);
            if (gxEty.OrganizeId == OrgId && gxEty.blId == blId)
            {
                gxEty.zt = "0";

                try
                {
                    var bllxmc = _BllxRepo.FindEntity(p => p.bllx.Substring(0, 1) == gxEty.bllx.Substring(0, 1) && p.OrganizeId == OrgId && p.zt == "1");
                    string sql = @"update a
                        set a.zt = 0,a.LastModifierCode = '" + user.rygh + @"',a.LastModifyTime = getdate()
                        from " + bllxmc.relTB + @" a
                        where a.id = @id and a.OrganizeId = @OrgId ";
                    _ZymeddocsrelationRepo.SubmitEntity(gxEty);

                    var para = new List<SqlParameter>();
                    para.Add(new SqlParameter("@OrgId", OrgId));
                    para.Add(new SqlParameter("@Id", blId));
                    this.ExecuteSqlCommand(sql, para.ToArray());

                    if (gxEty.bllx.Substring(0, 1) == ((int)EnumBllx.hljl).ToString())
                    {
                        var hldata = _hljlDataRepo.FindEntity(p => p.blId == blId && p.zt == "1");
                        if (hldata != null)
                        {
                            hldata.zt = "0";
                            hldata.Modify();
                            _hljlDataRepo.Update(hldata);
                        }
                    }
                }
                catch (FailedException ex)
                {
                    throw new FailedException("删除失败(" + ex.InnerException + ")");
                }
            }
            else
            {
                throw new FailedException("病历信息异常，请重新获取继续操作");
            }
        }
        #endregion
        /// <summary>
        /// 获取病案首页病人基本信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public BabasyVO GetPatBasicInfo_basy(string orgId, string zyh)
        {
            string sql = @"select convert(varchar(10),[syxh]) bah,[OrganizeId],[zyh],[patid],[brxz] fylb,[zybz],[ks] ryks,[bq] rybf,[ryrq],
            [rytj],[rqry],[rqrq],[zy],[mz],[gj],[cs_sheng] csd_s,[cs_shi] csd_qs,[cs_xian] csd_x,[hu_sheng]+[hu_shi] hkdz_s,
            [hu_xian] hkdx,[hu_dz] hkdz,[xian_sheng] xzds,[xian_shi] xzd_qs,[xian_xian] xzdx,[xian_dz] xzdz,convert(varchar(5),[hy]) hyzk
            ,[bje],[lxr],[lxrgx] gx,[lxrdh] lxdh,[lxrdz] lxdz,[cyjdry],[cyjdrq],[cyrq],convert(varchar(2),[cyzd]) cyzd,[cybq],
            [lxrjtdh],[lxrWebchat],[lxrEmail],[lxr2],[lxrgx2],[lxryddh2],[lxrjtdh2],[lxrWebchat2],[lxrEmail2],[lxrdz2],[gms],
            [ys],[doctor] ryys,[kh] jkkh,[CardType],[CardTypeName],[jkjl],[cw],[rybq],ryzd,[xm] brxm,[xb],[blh]
            ,convert(varchar(10),csny,120)[csny],[zjh] sfzhm,[zjlx],convert(varchar(2),[nl]) nl,[brly],[nlshow],dh xzz_lxdh,dwmc,ryzd_jbbm,cyzdmc,
            b.code yljgdm,b.name yljgmc,sjzyts
            from [dbo].[V_ZY_PatList]  a  with(nolock)
            left join  [NewtouchHIS_Base].[dbo].[Sys_Organize] b with(nolock) on a.OrganizeId=b.ID
            where a.OrganizeId=@orgId and zyh=@zyh and a.zt=1 ";

            BabasyVO ety = this.FindList<BabasyVO>(sql, new SqlParameter[] {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@zyh", zyh)
            }).FirstOrDefault();

            sql = @"select name zy,name gx from  [NewtouchHIS_Base].[dbo].[Sys_ItemsDetail]  m with(nolock)
                 where exists(select 1 from [NewtouchHIS_Base].[dbo].[Sys_Items] n with(nolock) 
                    where n.code=@code and m.itemid=n.id and n.zt=1) and m.code=@detcode";

            if (ety != null && !string.IsNullOrWhiteSpace(ety.zy))
            {
                BabasyVO itemzy = this.FindList<BabasyVO>(sql, new SqlParameter[] {
                    new SqlParameter("@code", "Profession"),
                    new SqlParameter("@detcode", ety.zy)
                }).FirstOrDefault();

                if (itemzy != null)
                {
                    ety.zy = itemzy.zy;
                }
            }

            if (ety != null && !string.IsNullOrWhiteSpace(ety.gx))
            {
                BabasyVO itemgx = this.FindList<BabasyVO>(sql, new SqlParameter[] {
                    new SqlParameter("@code", "RelativeType"),
                    new SqlParameter("@detcode", ety.gx)
                }).FirstOrDefault();

                if (itemgx != null)
                {
                    ety.gx = itemgx.gx;
                }
            }

            sql = null;
            return ety;
        }

        public string GetCwNameByCode(string cw, string orgId, string bq)
        {
            string sql = @"select cwmc BedName  from [NewtouchHIS_Base].dbo.xt_cw with(nolock)
where cwcode = @cw and zt = '1' and OrganizeId = @orgId and bqcode=@bq  ";
            var list = FindList<ZybrjbxxVO>(sql, new SqlParameter[] {
                new SqlParameter("cw",cw??""),
                new SqlParameter("orgId",orgId??""),
                new SqlParameter("bq",bq??"")
            });
            if (list != null && list.Count > 0)
            {
                return list.FirstOrDefault().BedName;
            }

            return "";
        }

        /*查询病历编辑权限*/
        public string selectBJQX(string blId, string blgxId, string OrganizeId, OperatorModel user)
        {
            string sql = @"exec usp_selectBjqx @blId=@blId,@OrganizeId=@OrganizeId,@user=@user";
            var bjqx = this.FindList<selectBjqx>(sql, new SqlParameter[] {
                            new SqlParameter("@blId", blId),
                            new SqlParameter("@user", user.rygh),
                            new SqlParameter("@OrganizeId", OrganizeId)
                });
            return bjqx.First().bjqx;
        }
    }
}
