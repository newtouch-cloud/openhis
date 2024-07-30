using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Domain.IDomainServices.Queue;
using Newtouch.Domain.ValueObjects.Queue;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.DomainServices.Queue
{
    public class QueueDmnService: DmnServiceBase, IQueueDmnService
    {
        public QueueDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
 
        }

        public IList<QueueInfo> GetQueue(string orgId,DateTime qdkssj,DateTime qdjssj,string ks,string ys,string ywbz,string ywlx,int? calledstu,string keyword)
        {
            string sql = @"SELECT [Id],a.[OrganizeId],[ScheduId],[pbId],[queno],[calledstu],[iscalling],[ispassed],[calledtimes],[memo]
,[patid],b.brxzmc [brxz],[brly],[ywbz],[ks],[ys],[fee],[kh],[ywlsh],[xm],[xb],[nlshow]
,[qdsj],[pbdesc],[ywlx],[czks],[czksmc],[czys],[czysxm],[Period],[PeriodDesc]
,a.[CreateTime],a.[CreatorCode],a.[LastModifierCode],a.[LastModifyTime],a.zt 
  FROM [NewtouchHIS_Sett].[dbo].[queue_schedule] a with(nolock)
left join [NewtouchHIS_Sett].[dbo].xt_brxz b with(nolock) on a.brxz= b.brxz and a.OrganizeId=b.OrganizeId and b.zt='1'
where a.[OrganizeId] = @orgId
and qdsj>= @qdkssj and qdsj<= @qdjssj
and a.zt = '1'";
            if (!string.IsNullOrWhiteSpace(ks))
            {
                sql += " and czks=@ks ";
            }
            if (!string.IsNullOrWhiteSpace(ys))
            {
                sql += " and czys=@ys ";
            }
            if (!string.IsNullOrWhiteSpace(ywlx))
            {
                sql += " and ywlx=@ywlx ";
            }
            if (!string.IsNullOrWhiteSpace(ywbz))
            {
                sql += " and ywbz=@ywbz ";
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and ywbz=@ywbz ";
            }

            return FindList<QueueInfo>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@qdkssj",qdkssj),
                new SqlParameter("@qdjssj",qdjssj),
                new SqlParameter("@ks",ks??""),
                new SqlParameter("@ys",ys??""),
                new SqlParameter("@ywbz",ywbz??""),
                new SqlParameter("@ywlx",ywlx??""),
                new SqlParameter("@keyword",keyword??""),
                new SqlParameter("@calledstu",calledstu==null?0:calledstu)
            });
        }
        public IList<QueueInfo> GetQueue(Pagination pagination, string orgId,DateTime qdkssj,DateTime qdjssj,string ks,string ys,string ywbz,string ywlx,int? calledstu,string keyword)
        {
            string sql = @"SELECT [Id],a.[OrganizeId],[ScheduId],[pbId],[queno],[calledstu],[iscalling],[ispassed],[calledtimes],[memo]
,[patid],b.brxzmc [brxz],[brly],[ywbz],[ks],[ys],[fee],[kh],[ywlsh],[xm],[xb],[nlshow]
,[qdsj],[pbdesc],[ywlx],[czks],[czksmc],[czys],[czysxm],[Period],[PeriodDesc]
,a.[CreateTime],a.[CreatorCode],a.[LastModifierCode],a.[LastModifyTime]
  FROM [NewtouchHIS_Sett].[dbo].[queue_schedule] a with(nolock)
left join [NewtouchHIS_Sett].[dbo].xt_brxz b with(nolock) on a.brxz= b.brxz and a.OrganizeId=b.OrganizeId and b.zt='1'
where a.[OrganizeId] = @orgId
and qdsj>= @qdkssj and qdsj<= @qdjssj
and a.zt = '1'";
            if (!string.IsNullOrWhiteSpace(ks))
            {
                sql += " and czks=@ks ";
            }
            if (!string.IsNullOrWhiteSpace(ys))
            {
                sql += " and czys=@ys ";
            }
            if (!string.IsNullOrWhiteSpace(ywlx))
            {
                sql += " and ywlx=@ywlx ";
            }
            if (!string.IsNullOrWhiteSpace(ywbz))
            {
                sql += " and ywbz=@ywbz ";
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (xm like @keyword or ywlsh like @keyword) ";
            }
            if (calledstu!=null)
            {
                sql += " and calledstu=@calledstu ";
            }
            return QueryWithPage<QueueInfo>(sql,pagination, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@qdkssj",qdkssj),
                new SqlParameter("@qdjssj",qdjssj),
                new SqlParameter("@ks",ks??""),
                new SqlParameter("@ys",ys??""),
                new SqlParameter("@ywbz",ywbz??""),
                new SqlParameter("@ywlx",ywlx??""),
                new SqlParameter("@calledstu",calledstu??(int)EmunQueueCalledStu.sign),
                new SqlParameter("@keyword","%"+keyword +"%"??"")
            });
        }

        public QueueInfo GetQueueByMzh(string mzh,string orgId)
        {
            string sql = @"SELECT [Id],a.[OrganizeId],[ScheduId],[pbId],[queno],[calledstu],[iscalling],[ispassed],[calledtimes],[memo]
,[patid],b.brxzmc [brxz],[brly],[ywbz],[ks],[ys],[fee],[kh],[ywlsh],[xm],[xb],[nlshow]
,[qdsj],[pbdesc],[ywlx],[czks],[czksmc],[czys],[czysxm],[Period],[PeriodDesc]
,a.[CreateTime],a.[CreatorCode],a.[LastModifierCode],a.[LastModifyTime]
  FROM [NewtouchHIS_Sett].[dbo].[queue_schedule] a with(nolock)
left join [NewtouchHIS_Sett].[dbo].xt_brxz b with(nolock) on a.brxz= b.brxz and a.OrganizeId=b.OrganizeId and b.zt='1'
where a.[OrganizeId] = @orgId
and a.zt = '1'";
            if (!string.IsNullOrWhiteSpace(mzh))
            {
                sql += " and ywlsh=@mzh ";
            }
            else {
                return new QueueInfo();
            }
       
            return FirstOrDefault<QueueInfo>(sql,  new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@mzh",mzh)
            });
        }
    }
}
