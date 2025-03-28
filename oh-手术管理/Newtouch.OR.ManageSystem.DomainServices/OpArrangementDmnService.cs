using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.OR.ManageSystem.Domain.DTO;
using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IDomainServices;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using Newtouch.OR.ManageSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.DomainServices
{
    public class OpArrangementDmnService : DmnServiceBase, IOpArrangementDmnService
    {
        public OpArrangementDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        //public IList<ArrangeListVO> GetArrangeList(Pagination pagination, QueryArrangeDto dto) 
        public IList<ArrangeListVO> GetArrangeList(QueryArrangeDto dto)
        {
            #region 注释SQL
            //            string sql = @"SELECT [OrganizeId]
            //      ,[Id]
            //      ,[ApplyId]
            //      ,[Applyno]
            //      ,[ssxh]
            //      ,[zyh]
            //      ,[xm]
            //      ,[ks]
            //      ,[bq]
            //      ,[ch]
            //      ,[zd]
            //      ,[sqzt]
            //      ,[ssmc]
            //      ,[ssdm]
            //      ,[sssj]
            //      ,[ysgh]
            //      ,[ysxm]
            //      ,[AnesCode]
            //      ,[oproom]
            //      ,[oporder]
            //      ,[zlys1]
            //      ,[zlys2]
            //      ,[zlys3]
            //      ,[zlys4]
            //      ,[xhhs]
            //      ,[xshs]
            //      ,[ssbw]
            //      ,[zt]
            //      ,[CreateTime]
            //      ,[CreatorCode]
            //      ,[LastModifyTime]
            //      ,[LastModifierCode]
            //  FROM[dbo].[OR_Arrangement] a with(nolock)
            //  where OrganizeId=@orgId and zt='1'
            //  and exists (select 1 from [OR_ApplyInfo] b with(nolock)
            //		where a.organizeid=b.organizeid and a.zyh=b.zyh and a.[Applyno]=b.[Applyno] and b.zt='1')
            //";
            #endregion
            string sql = @"select a.OrganizeId,a.Id,ApplyId,a.Applyno,ssxh,a.zyh,a.xm,a.xb,a.ks,d.bqmc bq,
e.cwmc ch,a.zd,b.sqzt,c.ssmc,c.ssdm,a.sssj,isnull(b.ysgh,a.ysgh) ysgh,isnull(b.ysxm,a.ysxm) ysxm,a.AnesCode,oproom,oporder,
zlys1,zlys2,zlys3,zlys4,xhhs,xshs,a.ssbw,a.zt,a.CreateTime,a.CreatorCode,a.LastModifyTime,a.LastModifierCode,
(case when datediff(yy,a.csrq,getdate())>=1 then convert(varchar(3),datediff(yy,a.csrq,getdate()))+'岁' 
else convert(varchar(2),datediff(mm,a.csrq,getdate()))+'个月' end  )nl 
from [dbo].OR_ApplyInfo a left join [dbo].OR_Arrangement b on a.Id=b.ApplyId 
left join [OR_ApplyInfo_Expand] c on a.organizeId=c.organizeId and a.applyno=c.applyno and px=1  
left join NewtouchHIS_Base.dbo.V_S_xt_cw e with(nolock) on a.organizeid=e.organizeid and a.ch=e.cwcode and e.zt=1 
left join NewtouchHIS_Base..V_S_xt_bq  d with(nolock) on a.organizeid=d.organizeid and a.bq=d.bqcode and d.zt=1
where a.zt='1' and a.sqzt<> " + (int)EnumSqzt.yqx;
            var para = new List<SqlParameter>();

            if (dto != null)
            {
                if (dto.orgId != null)
                {
                    sql += " and a.OrganizeId=@orgId";
                    para.Add(new SqlParameter("@orgId", dto.orgId));
                }
                if (dto.ksrq != null && dto.jsrq != null)
                {
                    sql += " and a.sssj between @ksrq and @jsrq ";
                    para.Add(new SqlParameter("@ksrq", Convert.ToDateTime(dto.ksrq).ToString("yyyy-MM-dd HH:mm:ss")));
                    para.Add(new SqlParameter("@jsrq", Convert.ToDateTime(dto.jsrq).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss")));
                }
                if (!string.IsNullOrWhiteSpace(dto.keyword))
                {
                    sql += " and (a.zyh=@keyword or a.xm like '%" + dto.keyword + "%')";
                    para.Add(new SqlParameter("@keyword", dto.keyword));
                }
                if (!string.IsNullOrWhiteSpace(dto.sszt))
                {
                    sql += " and isnull(b.sqzt," + (int)EnumSqzt.dsh + ") = @sszt";
                    para.Add(new SqlParameter("@sszt", dto.sszt));
                }
                if (!string.IsNullOrWhiteSpace(dto.bq))
                {
                    sql += " and a.bq=@bq";
                    para.Add(new SqlParameter("@bq", dto.bq));
                }
                if (!(string.IsNullOrWhiteSpace(dto.ysgh) || dto.ysgh == "undefined"))
                {
                    sql += " and b.ysgh=@ysgh";
                    para.Add(new SqlParameter("@ysgh", dto.ysgh));
                }
                if (!string.IsNullOrWhiteSpace(dto.room))
                {
                    sql += " and oproom=@room";
                    para.Add(new SqlParameter("@room", dto.room));
                }
            }
            return this.FindList<ArrangeListVO>(sql.ToString(), para == null ? null : para.ToArray());
            //return this.FindList<ORArrangementEntity>(sql, para.ToArray());
        }

        public string getArrangeIdByApplyId(string ApplyId, string OrganizeId)
        {
            string sql = "select b.Id from [dbo].[OR_ApplyInfo] a left join [dbo].[OR_Arrangement] b on a.Id=b.ApplyId where 1=1 and b.zt='1'";
            var para = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(OrganizeId))
            {
                sql += " and a.OrganizeId=@OrganizeId and b.OrganizeId=@OrganizeId ";
                para.Add(new SqlParameter("@OrganizeId", OrganizeId));
            }
            if (!string.IsNullOrWhiteSpace(ApplyId))
            {
                sql += " and a.Id=@ApplyId";
                para.Add(new SqlParameter("@ApplyId", ApplyId));
            }
            return this.FindList<string>(sql, para.ToArray()).FirstOrDefault();
        }

        public ArrangeListVO GetArrangeObjByApplyId(string ApplyId, string OrganizeId)
        {
            string sql = @"select Top 1 a.OrganizeId,b.Id,a.id ApplyId,a.Applyno,ssxh,a.zyh,a.xm,a.xb,a.ks,a.bq,a.ch,a.zd,b.sqzt,ssmcn as ssmc,a.ssdm,a.sssj,
case when b.sqzt='1' or b.sqzt is null then a.ysgh else b.ysgh end ysgh,
case when b.sqzt='1' or b.sqzt is null then a.ysxm else b.ysxm end ysxm,
b.AnesCode,oproom,oporder,zlys1,zlys2,zlys3,zlys4,zlys1name=(select top 1 ryxm from [dbo].[OR_OpStaffRecord] where rygh=(select top 1 zlys1 from [dbo].OR_Arrangement where applyId=@ApplyId)),
zlys2name=(select top 1 ryxm from [dbo].[OR_OpStaffRecord] where rygh=(select top 1 zlys2 from [dbo].OR_Arrangement where applyId=@ApplyId)),
xhhsname=(select top 1 ryxm from [dbo].[OR_OpStaffRecord] where rygh=(select top 1 xhhs from [dbo].OR_Arrangement where applyId=@ApplyId)),
xshsname=(select top 1 ryxm from [dbo].[OR_OpStaffRecord] where rygh=(select top 1 xshs from [dbo].OR_Arrangement where applyId=@ApplyId)),
xhhs,xshs,case when b.sqzt='1' or b.sqzt is null then a.ssbw else b.ssbw end ssbw,a.zt,a.CreateTime,a.CreatorCode,a.LastModifyTime,a.LastModifierCode,(case when datediff(yy,csrq,getdate())>=1 then convert(varchar(3),datediff(yy,csrq,getdate()))+'岁' else convert(varchar(2),datediff(mm,csrq,getdate()))+'个月' end  )nl  
from [dbo].OR_ApplyInfo a 
left join [dbo].OR_Arrangement b on a.Id=b.ApplyId and a.OrganizeId=b.OrganizeId  and a.zt=b.zt 
where a.zt='1'";

            var para = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(OrganizeId))
            {
                sql += " and a.OrganizeId=@OrganizeId";
                para.Add(new SqlParameter("@OrganizeId", OrganizeId));
            }
            if (!string.IsNullOrWhiteSpace(ApplyId))
            {
                sql += " and a.Id=@ApplyId";
                para.Add(new SqlParameter("@ApplyId", ApplyId));
            }
            return this.FindList<ArrangeListVO>(sql, para.ToArray()).FirstOrDefault();

        }

        public ORApplyInfoEntity GetApplyInfoByKey(string keyValue, string OrganizeId)
        {
            string sql = "";
            var para = new List<SqlParameter>();
            sql = @"SELECT a.[OrganizeId],[Id] ,[Applyno],[zyh],[xm] ,[xb] ,[csrq] ,[sfz] ,[ks],bqmc bq,[ch],[ryrq],[zd] ,[sqzt] ,[ssmcn],[ssdm],[sssj],[ysgh] ,[ysxm] ,mzys,[AnesCode] ,[ssbw] ,[isgl] ,a.[zt],a.[CreateTime] ,a.[CreatorCode],a.[LastModifyTime],a.[LastModifierCode],(case when datediff(yy,csrq,getdate())>=1 then convert(varchar(3),datediff(yy,csrq,getdate()))+'岁' else convert(varchar(2),datediff(mm,csrq,getdate()))+'个月' end  )nl FROM [dbo].[OR_ApplyInfo] a left join [NewtouchHIS_Base].dbo.xt_bq b on a.bq=b.bqCode  and a.OrganizeId=b.OrganizeId  and a.zt=b.zt  where a.zt!=0 ";
            if (!string.IsNullOrWhiteSpace(OrganizeId))
            {
                sql += " and a.OrganizeId=@OrganizeId ";
                para.Add(new SqlParameter("@OrganizeId", OrganizeId));
            }
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                sql += " and Id=@keyValue ";
                para.Add(new SqlParameter("@keyValue", keyValue));
            }
            return this.FindList<ORApplyInfoEntity>(sql, para.ToArray()).FirstOrDefault();
        }
    }
}
