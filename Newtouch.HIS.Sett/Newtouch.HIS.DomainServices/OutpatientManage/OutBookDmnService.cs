using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.OutpatientManage;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.API;
using Newtouch.HIS.Domain.IDomainServices.OutpatientManage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using Newtouch.HIS.DomainServices.API;
using Newtouch.HIS.Sett.Request.Booking.Request;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.DomainServices.OutpatientManage
{
    public class OutBookDmnService : DmnServiceBase, IOutBookDmnService
    {
        private readonly IMzghbookRepo _MzghbookRepo;
        private readonly IOutPatientDmnService _OutPatientDmnService;
        private readonly IBookingRegisterDmnService _bookingReginsterDmnService;
        public OutBookDmnService(IDefaultDatabaseFactory databaseFactory)
           : base(databaseFactory)
        {
        }

        //分页获取挂号排班列表
        public IList<OutBookVO> GetPagintionList(Pagination pagination, string orgId, string keyword)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                return null;
            }
            StringBuilder sqlStr = new StringBuilder();
            var parlist = new List<SqlParameter>();
            sqlStr.Append(@"
select ghpb.[ghpbId] ,ghpb.[OrganizeId],ghpb.[ks] ,ghpb.[ys],ghpb.[zt] ,ghpb.[zhl] ,ghpb.[ghzb],
ghpb.[CreatorCode] ,ghpb.[CreateTime] ,ghpb.[LastModifyTime],ghpb.[LastModifierCode] ,ghpb.[px],
ghpb.[ghlx] ,ghpb.[zlxm] ,ghpb.[mjzbz],sfxm.sfxmmc,zlsfxm.zhmc zlxmmc,department.name as ksmz,
staff.name as ysxm,ghpb.pbdesc
from mz_ghpb_config ghpb
left join NewtouchHIS_Base.[dbo].[V_S_xt_sfxm] sfxm 
on  sfxm.sfxmCode=ghpb.ghlx and sfxm.OrganizeId = ghpb.OrganizeId
left join NewtouchHIS_Base.[dbo].[V_S_Sys_Department] department 
on ghpb.ks =department.Code and department.OrganizeId=ghpb.OrganizeId and department.zt='1'
left join (select zhcode,zhmc,OrganizeId from mz_gh_zlxmzh where OrganizeId= @OrganizeId group by zhcode,zhmc,OrganizeId) zlsfxm 
on zlsfxm.zhcode=ghpb.zlxm and zlsfxm.OrganizeId = ghpb.OrganizeId
left join NewtouchHIS_Base.[dbo].[V_S_Sys_Staff] staff 
on ghpb.ys=staff.gh  
            where 1=1 ");
            if (!string.IsNullOrEmpty(orgId))
            {
                sqlStr.Append(" AND ghpb.OrganizeId = @OrganizeId");
                parlist.Add(new SqlParameter("@OrganizeId", orgId));
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                sqlStr.Append(" AND (department.name like @ksmz)");
                parlist.Add(new SqlParameter("@ksmz", "%" + keyword.Trim() + "%"));
            }
            var list = this.QueryWithPage<OutBookVO>(sqlStr.ToString(), pagination, parlist.ToArray()).ToList();
            return list;
        }

        //获取医生工号姓名对应关系
        public IList<OutPatientStaffEntity> GetStaffList(string orgId) {
            if (string.IsNullOrEmpty(orgId))
            {
                return null;
            }
            string sql = @"select gh,Name from  NewtouchHIS_Base.[dbo].[V_S_Sys_Staff] where 1=1 and zt='1'";
            if (!string.IsNullOrWhiteSpace(orgId))
            {
                sql += " and organizeId=@orgId ";
            }

            return this.FindList<OutPatientStaffEntity>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId)
            });
        }

        //根据医生工号获取医生姓名
        public string getStaffName(string gh) {
            string sql = "select Name from  NewtouchHIS_Base.[dbo].[V_S_Sys_Staff] where 1=1 and zt='1'";
            var para = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(gh))
            {
                sql += " and gh=@gh ";
                para.Add(new SqlParameter("@gh", gh));
            }
            return this.FindList<string>(sql, para.ToArray()).FirstOrDefault();
        }

        //排班页获取排班信息
        public OutBookVO getArrangeInfo(int ghpbId,string orgId) {
            if (ghpbId==0)
            {
                return null;
            }
//            string sql = @"select ghpb.[ghpbId] ,ghpb.[OrganizeId],ghpb.[ks] ,ghpb.[ys],ghpb.[zt] ,ghpb.[zhl] ,ghpb.[ghzb],ghpb.[CreatorCode] ,ghpb.[CreateTime] ,ghpb.[LastModifyTime],ghpb.[LastModifierCode] ,ghpb.[px],ghpb.[ghlx] ,ghpb.[zlxm] ,ghpb.[mjzbz],sfxm.sfxmmc,zlsfxm.sfxmmc zlxmmc,department.name as ksmz,staff.name as ysxm from mz_ghpb_config ghpb
//left join NewtouchHIS_Base.[dbo].[V_S_xt_sfxm] sfxm on  sfxm.sfxmCode=ghpb.ghlx and sfxm.OrganizeId = ghpb.OrganizeId
//left join NewtouchHIS_Base.[dbo].[V_S_Sys_Department] department on ghpb.ks =department.Code
//left join NewtouchHIS_Base.[dbo].[V_S_xt_sfxm] zlsfxm on zlsfxm.sfxmCode=ghpb.zlxm and zlsfxm.OrganizeId = ghpb.OrganizeId
//left join NewtouchHIS_Base.[dbo].[V_S_Sys_Staff] staff on ghpb.ys=staff.gh 
//where 1=1 ";
            //诊疗项目打包为收费组合
            string sql = @"select ghpb.[ghpbId] ,ghpb.[OrganizeId],ghpb.[ks] ,ghpb.[ys],ghpb.[zt] ,ghpb.pbdesc,
ghpb.[zhl] ,ghpb.[ghzb],ghpb.[CreatorCode] ,ghpb.[CreateTime] ,ghpb.[LastModifyTime],ghpb.[LastModifierCode] ,
ghpb.[px],ghpb.[ghlx] ,ghpb.[zlxm] ,ghpb.[mjzbz],sfxm.sfxmmc,zlsfxm.zhmc zlxmmc,department.name as ksmz,staff.name as ysxm 
from mz_ghpb_config ghpb
left join NewtouchHIS_Base.[dbo].[V_S_xt_sfxm] sfxm on  sfxm.sfxmCode=ghpb.ghlx and sfxm.OrganizeId = ghpb.OrganizeId
left join NewtouchHIS_Base.[dbo].[V_S_Sys_Department] department on ghpb.ks =department.Code  and ghpb.OrganizeId= department.OrganizeId and department.zt='1'
left join (select zhcode,zhmc,OrganizeId from mz_gh_zlxmzh where OrganizeId=@orgId group by zhcode,zhmc,OrganizeId) zlsfxm on zlsfxm.zhcode=ghpb.zlxm and zlsfxm.OrganizeId = ghpb.OrganizeId
left join NewtouchHIS_Base.[dbo].[V_S_Sys_Staff] staff on ghpb.ys=staff.gh and ghpb.OrganizeId=staff.OrganizeId
where 1=1 ";
            if (ghpbId!=0)
            {
                sql += " and ghpbId=@ghpbId ";
            }
            return this.FirstOrDefault<OutBookVO>(sql, new SqlParameter[] {
                new SqlParameter("@ghpbId",ghpbId),
                new SqlParameter("@orgId",orgId)
            });
        }
        public IList<OutBookDateVO> getDateTimeInfo(string organizeId, int ghpbId,string timeslot)
        {

            if (string.IsNullOrEmpty(organizeId))
            {
                return null;
            }
                string sql = @"select top 1 organizeId,ghpbId, 
			zyi=(select period from [dbo].[mz_ghpb_date] where 1=1  and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=1 and period=2 and timeslot!='' and timeslot=@timeslot),
			zer=(select period from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=2 and period=2 and timeslot!='' and timeslot=@timeslot),
			zsan=(select period from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=3 and period=2 and timeslot!='' and timeslot=@timeslot),
			zsi=(select period from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=4 and period=2 and timeslot!='' and timeslot=@timeslot),
			zwu=(select period from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=5 and period=2 and timeslot!='' and timeslot=@timeslot),
			zliu=(select period from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=6 and period=2 and timeslot!='' and timeslot=@timeslot),
			zri=(select period from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=7 and period=2 and timeslot!='' and timeslot=@timeslot),
			hy1=(select totalNum from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=1 and period=2 and timeslot!='' and timeslot=@timeslot),
			hy2=(select totalNum from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=2 and period=2 and timeslot!='' and timeslot=@timeslot),
			hy3=(select totalNum from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=3 and period=2 and timeslot!='' and timeslot=@timeslot),
			hy4=(select totalNum from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=4 and period=2 and timeslot!='' and timeslot=@timeslot),
			hy5=(select totalNum from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=5 and period=2 and timeslot!='' and timeslot=@timeslot),
			hy6=(select totalNum from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=6 and period=2 and timeslot!='' and timeslot=@timeslot),
			hy7=(select totalNum from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=7 and period=2 and timeslot!='' and timeslot=@timeslot),
            sjd=(select timeslot from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=1 and period=2 and timeslot!='' and timeslot=@timeslot),
			isBook=(select Top 1 isBook from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId  and period=2  and timeslot!=''  and timeslot=@timeslot)
			from [dbo].[mz_ghpb_date] where OrganizeId=@organizeId and ghpbId=@ghpbId and timeslot!=''  and timeslot=@timeslot";
                return this.FindList<OutBookDateVO>(sql, new SqlParameter[] {
                new SqlParameter("@organizeId",organizeId),
                new SqlParameter("@timeslot",timeslot),
                new SqlParameter("@ghpbId",ghpbId)
            });
        }
        //排班页获取排班时间
        public OutBookDateVO getDateInfo(string organizeId,int ghpbId) {
             
                if (string.IsNullOrEmpty(organizeId))
            {
                return null;
            }
            string sql = @"select Top 1 organizeId,ghpbId, 
			zyi=(select period from [dbo].[mz_ghpb_date] where 1=1  and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=1 and period=0),
			zer=(select period from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=2 and period=0),
			zsan=(select period from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=3 and period=0),
			zsi=(select period from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=4 and period=0),
			zwu=(select period from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=5 and period=0),
			zliu=(select period from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=6 and period=0),
			zri=(select period from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=7 and period=0),
			hy1=(select totalNum from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=1 and period=0),
			hy2=(select totalNum from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=2 and period=0),
			hy3=(select totalNum from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=3 and period=0),
			hy4=(select totalNum from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=4 and period=0),
			hy5=(select totalNum from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=5 and period=0),
			hy6=(select totalNum from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=6 and period=0),
			hy7=(select totalNum from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=7 and period=0),
            hy21=(select totalNum from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=1 and period=1),
			hy22=(select totalNum from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=2 and period=1),
			hy23=(select totalNum from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=3 and period=1),
			hy24=(select totalNum from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=4 and period=1),
			hy25=(select totalNum from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=5 and period=1),
			hy26=(select totalNum from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=6 and period=1),
			hy27=(select totalNum from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=7 and period=1),
            sjd1=(select timeslot from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=1 and period=0),
			sjd2=(select timeslot from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=2 and period=0),
			sjd3=(select timeslot from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=3 and period=0),
			sjd4=(select timeslot from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=4 and period=0),
			sjd5=(select timeslot from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=5 and period=0),
			sjd6=(select timeslot from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=6 and period=0),
			sjd7=(select timeslot from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId and Weekdd=7 and period=0),
			isBook=(select Top 1 isBook from [dbo].[mz_ghpb_date] where 1=1 and OrganizeId=@organizeId and ghpbId=@ghpbId  and period=0)
			from [dbo].[mz_ghpb_date] where OrganizeId=@organizeId and ghpbId=@ghpbId ";
            return this.FirstOrDefault<OutBookDateVO>(sql, new SqlParameter[] {
                new SqlParameter("@organizeId",organizeId),
                new SqlParameter("@ghpbId",ghpbId)
            });
        }

        //更新排班信息
        public int UpdateArrange(OutBookArrangeVO entity,int ghpbId,string orgId,string User,DateTime Time) {
            string sql = @"update [mz_ghpb_config] 
set organizeId = @organizeId,mjzbz = @mjzbz,ghlx = @ghlx,zlxm = @zlxm,ks = @ks, zt = @zt,
LastModifyTime=@Time,LastModifierCode=@User ,ys=@ys,pbdesc=@pbdesc
where ghpbId = @ghpbId and organizeId=@orgId";
            SqlParameter[] para ={
                new SqlParameter("@organizeId",entity.OrganizeId ?? ""),
                new SqlParameter("@mjzbz",entity.mjzbz ?? ""),
                new SqlParameter("@ghlx",entity.ghlx ?? ""),
                new SqlParameter("@zlxm",entity.zlxm ?? ""),
                new SqlParameter("@ks",entity.ks ?? ""),
                new SqlParameter("@ys",entity.ys ?? ""),
                new SqlParameter("@zt",entity.zt ?? ""),
                new SqlParameter("@User",User ?? ""),
                new SqlParameter("@Time",Convert.ToDateTime(Time)),
                new SqlParameter("@ghpbId",ghpbId),
                new SqlParameter("@orgId",orgId ?? ""),
                new SqlParameter("@pbdesc",entity.pbdesc ?? "")
                };
            return this.ExecuteSqlCommand(sql, para);
        }

        //新增排班信息
        public int InsertArrange(OutBookArrangeVO entity, string orgId, string User, DateTime Time,int ghpbIdNew) {
            string sql = @"insert into [dbo].[mz_ghpb_config] (ghpbId,OrganizeId, ks, mjzbz, ghlx,zlxm, zt, CreatorCode, CreateTime,pbdesc,ys) 
values(@ghpbId,@OrganizeId, @ks, @mjzbz, @ghlx,@zlxm, '1', @CreatorCode, @CreateTime,@pbdesc,@ys)";
            SqlParameter[] para ={
                new SqlParameter("@ghpbId",ghpbIdNew),
                new SqlParameter("@organizeId",entity.OrganizeId ?? ""),
                new SqlParameter("@mjzbz",entity.mjzbz ?? ""),
                new SqlParameter("@ghlx",entity.ghlx ?? ""),
                new SqlParameter("@zlxm",entity.zlxm ?? ""),
                new SqlParameter("@ks",entity.ks ?? ""),
                new SqlParameter("@CreatorCode",User ?? ""),
                new SqlParameter("@CreateTime",Convert.ToDateTime(Time)),
                new SqlParameter("@pbdesc",entity.pbdesc ?? ""),
                new SqlParameter("@ys",entity.ys ?? ""),
                };
            return this.ExecuteSqlCommand(sql, para);
        }
        //新增排班信息
        public int DeleteArrange(int ghpbId,string orgId)
        {
            string deletesql = @"delete from mz_ghpb_date where ghpbid=@ghpbId and OrganizeId=@organizeId";
            //                    string sql = @"update [mz_ghpb_date] 
            //set  period = @period,totalNum = @totalNum,isBook = @isBook,zt = @zt, 
            //LastModifierCode = @User,LastModifyTime = @Time,timeslot=@timeslot
            //where 1=1 and organizeId = @organizeId and ghpbId = @ghpbId and weekdd=@weekdd";
            SqlParameter[] para ={
                new SqlParameter("@ghpbId",ghpbId),
                new SqlParameter("@organizeId",orgId)
                };
           return this.ExecuteSqlCommand(deletesql, para);
        }
        //新增排班时间
        public int InsertghpbTime(string begintime, string endtime, string orgId, string User, DateTime Time,int id)
        {

            if (id==1)
            {
                string deletesql = @"delete from mz_ghpb_Time";
                SqlParameter[] para1 = { };
                this.ExecuteSqlCommand(deletesql, para1);
            }
            string sql = @"INSERT INTO [dbo].[mz_ghpb_Time]
           ([Id]
           ,[OrganizeId]
           ,[BeginTime]
           ,[EndTime]
           ,[CreateTime]
           ,[CreatorCode]
           ,[zt])
     VALUES
           (@Id
           ,@OrganizeId
           ,@BeginTime
           ,@EndTime
           ,@CreateTime
           ,@CreatorCode
           ,'1')";
            SqlParameter[] para ={
                new SqlParameter("@Id",id),
                new SqlParameter("@OrganizeId",orgId ?? ""),
                new SqlParameter("@BeginTime",begintime ?? ""),
                new SqlParameter("@EndTime",endtime ?? ""),
                new SqlParameter("@CreateTime",Time),
                new SqlParameter("@CreatorCode",User ?? ""),
                };
            return this.ExecuteSqlCommand(sql, para);
        }
        public int getghpbId()
        {
            string sql = "select isnull(Max(ghpbId),0)+1 ghpbId from [dbo].[mz_ghpb_config]  ";
            return this.FirstOrDefault<int>(sql, new SqlParameter[] {
            });
        }

        //根据科室获取医生列表
        public IList<string> getStaffListByKs(string ks,string orgId)
        {
            string sql = "select name from [dbo].[mz_ghpb_rel_doc] ghpb left join NewtouchHIS_Base.[dbo].[V_S_Sys_Staff] staff on ghpb.ys = staff.gh where 1=1  and ghpb.zt='1'";
            var para = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(ks))
            {
                sql += " and ks=@ks ";
            }
            if (!string.IsNullOrWhiteSpace(orgId))
            {
                sql += " and ghpb.organizeId=@organizeId ";
            }

            return this.FindList<string>(sql, new SqlParameter[] {
                new SqlParameter("@ks",ks),
                new SqlParameter("@organizeId",orgId)
            });
        }
        public IList<string> getDateInfosjd(string orgId)
        {
            string sql = "select begintime+'-'+EndTime from mz_ghpb_Time where organizeid=@organizeId";
            return this.FindList<string>(sql, new SqlParameter[] {
                new SqlParameter("@organizeId",orgId)
            });
        }

        /// <summary>
        /// 修改排班号源
        /// </summary>
        /// <param name="pbList"></param>
        /// <param name="orgId"></param>
        public void SaveDatapb(List<OutBookScheduleEntity> pbList, string orgId)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                if (pbList != null && pbList.Count > 0)
                {
                    foreach (var item in pbList)
                    {
                            var uptsentity = db.IQueryable<OutBookScheduleEntity>().FirstOrDefault(p => p.ScheduId == item.ScheduId && p.OrganizeId == orgId);
                            if (uptsentity != null)
                            {
                                uptsentity.TotalNum = item.TotalNum;
                                uptsentity.Modify();
                                db.Update(uptsentity);
                            }
                            else
                            {
                                throw new FailedException("数据异常，未查询到该治疗建议信息");
                            }

                    }
                }
                db.Commit();
            }
        }
        /// <summary>
        /// 修改排班号源
        /// </summary>
        /// <param name="pbList"></param>
        /// <param name="orgId"></param>
        public void SaveDatatzcz(Decimal ScheduId,string czzt, string orgId,string tzyy)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                        var uptsentity = db.IQueryable<OutBookScheduleEntity>().FirstOrDefault(p => p.ScheduId ==ScheduId && p.OrganizeId == orgId);
                        if (uptsentity != null)
                        {
                            uptsentity.istz = czzt;
                            uptsentity.tzyy = tzyy;
                            uptsentity.tzsj = DateTime.Now.ToString();
                            uptsentity.Modify();
                            db.Update(uptsentity);
                        }
                        else
                        {
                            throw new FailedException("数据异常，未查询到该治疗建议信息");
                        }
                db.Commit();
            }
        }
        public IList<string> getDateInfosjdcount(int ghpbId, string orgId)
        {
            string sql = "select timeslot from [mz_ghpb_date]  where  ghpbId=@ghpbId and Period=2  and timeslot!='' and OrganizeId=@organizeId";
            return this.FindList<string>(sql, new SqlParameter[] {
                new SqlParameter("@organizeId",orgId),
                 new SqlParameter("@ghpbId",ghpbId)
            });
        }

        /// <summary>
        /// 获取排班门诊诊疗项目组合
        /// </summary>
        /// <param name="zhcode"></param>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<MzpbZlxmzh> GetMzpbZlxmzh(string zhcode,string keyword, string orgId)
        {
            string sql = @"select [zhmc],[zhcode],sum(price) zhje,sfdl
from mz_gh_zlxmzh with(nolock) 
where zt='1' and OrganizeId=@orgId
";
            if (!string.IsNullOrWhiteSpace(zhcode))
            {
                sql += " and zhcode=@code ";
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and( zhmc like @keyword or zlxmmc like @keyword or zlxmpy like @keyword)";
            }
            sql += " group by [zhmc],[zhcode],sfdl ";
            return this.FindList<MzpbZlxmzh>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@code",zhcode ??""),
                new SqlParameter("@keyword","%"+keyword+"%")
            });
        }
        public IList<MzpbZlxmzh> GetMzpbZlxmzhDetail(string zhcode, string keyword, string orgId)
        {
            string sql = @"select [OrganizeId],[Id],[zhmc],[zhcode],[ord],[zlxm],[zlxmmc],[zt]
,[price],zlxmpy,[CreatorCode],[CreateTime],[LastModifyTime],[LastModifierCode]
from mz_gh_zlxmzh with(nolock) 
where zt='1' and OrganizeId=@orgId
";
            if (!string.IsNullOrWhiteSpace(zhcode))
            {
                sql += " and zhcode=@code ";
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and( zhmc like '@keyword' or zlxmmc like '@keyword' or zlxmpy like '@keyword')";
            }

            return this.FindList<MzpbZlxmzh>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@code",zhcode ??""),
                new SqlParameter("@keyword","%"+keyword+"%")
            });
        }


        #region HIS预约/取消预约
        public string PatBookGh(string cardNo, int ScheduId, string brxz, string Doctor, DateTime OutDate, string orgId)
        {
            MzAppointmentReq req = new MzAppointmentReq();
            req.AppID = EnumMzghly.His.ToString() ;
            req.ScheduId = ScheduId;
            req.OutDate = OutDate;
            req.HospitalID = "HIS";
            req.CardNo = cardNo;
            req.ghxz = brxz;
            var BookId = _bookingReginsterDmnService.OutAppointment(req);
            return BookId.BookID;
        }
        public int CancalBook(string BookId)
        {
            MzAppointmentRecordReq req = new MzAppointmentRecordReq();
            req.HospitalID = "HIS";
            req.BookId = BookId;
            req.AppID = EnumMzghly.His.ToString();
            req.Lxdh = "";
            var cnt = _bookingReginsterDmnService.CancelOutApp(req);
            return cnt;
        }
        #endregion
    }
}
