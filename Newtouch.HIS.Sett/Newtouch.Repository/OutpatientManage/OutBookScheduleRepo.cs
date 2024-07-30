using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity.OutpatientManage;
using Newtouch.HIS.Domain.IRepository.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.OutpatientManage
{
    public class OutBookScheduleRepo : RepositoryBase<OutBookScheduleEntity>, IOutBookScheduleRepo
    {

        public OutBookScheduleRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        
        //分页获取Schedule列表
        public IList<OutBookScheduleVO> GetPagintionList(Pagination pagination, string orgId, DateTime time)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                return null;
            }
            StringBuilder sqlStr = new StringBuilder();
            var parlist = new List<SqlParameter>();
            sqlStr.Append(@"select a.*,b.sfxmmc,c.sfxmmc as zlxmmc from  mz_ghpb_schedule  a
			left join NewtouchHIS_Base.[dbo].[V_S_xt_sfxm] b
			on b.sfxmCode=a.ghlx and b.OrganizeId=a.OrganizeId
			left join NewtouchHIS_Base.[dbo].[V_S_xt_sfxm] c 
			on c.sfxmCode=a.zlxm and c.OrganizeId=a.OrganizeId 
            where 1=1 and a.zt='1' ");

            sqlStr.Append(" and a.createTime >= @time ");
            parlist.Add(new SqlParameter("@time", Convert.ToDateTime(time).ToString("yyyy-MM-dd HH:mm:ss")));
            if (!string.IsNullOrEmpty(orgId))
            {
                sqlStr.Append(" AND a.OrganizeId = @OrganizeId");
                parlist.Add(new SqlParameter("@OrganizeId", orgId));
            }
            var list = this.QueryWithPage<OutBookScheduleVO>(sqlStr.ToString(), pagination, parlist.ToArray()).ToList();
            return list;
        }
        //获取Schedule列表
        public IList<OutBookScheduleVO> GetPagintionListTime(string orgId, string kssj,string jssj,string ys,string czztcx, string ScheduId, string ks, string lx, string isbook)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                return null;
            }
            StringBuilder sqlStr = new StringBuilder();
            var parlist = new List<SqlParameter>();
            sqlStr.Append(@"select a.*,b.sfxmmc,c.sfxmmc as zlxmmc from  mz_ghpb_schedule  a
			left join NewtouchHIS_Base.[dbo].[V_S_xt_sfxm] b
			on b.sfxmCode=a.ghlx and b.OrganizeId=a.OrganizeId
			left join NewtouchHIS_Base.[dbo].[V_S_xt_sfxm] c 
			on c.sfxmCode=a.zlxm and c.OrganizeId=a.OrganizeId 
            where 1=1 and a.zt='1' ");
            if (czztcx != null && czztcx != "")
            {
                if (czztcx == "1")
                {
                    sqlStr.Append(" and a.istz='0'");
                }
                else if (czztcx == "2")
                {
                    sqlStr.Append(" and a.istz='1'");
                }
            }
            
            if (ys != null && ys != "")
            {
                sqlStr.Append(" and a.ysgh=@ys ");
            }
            if (ScheduId != null && ScheduId != "")
            {
                sqlStr.Append(" and a.ScheduId=@ScheduId ");
                parlist.Add(new SqlParameter("@ScheduId", ScheduId));
            }
            else {
                sqlStr.Append(" and a.OutDate >= @kssj and a.OutDate <= @jssj ");
                parlist.Add(new SqlParameter("@kssj", kssj));
                parlist.Add(new SqlParameter("@jssj", jssj));
            }
            if (ks != null && ks != "")
            {
                sqlStr.Append(" and a.czks=@ks ");
                parlist.Add(new SqlParameter("@ks", ks));
            }
            if (lx != null && lx != "")
            {
                sqlStr.Append(" and a.RegType=@lx ");
                parlist.Add(new SqlParameter("@lx", lx));
            }
            if (ys!=null&& ys!="")
            {
                parlist.Add(new SqlParameter("@ys", ys));
            }
            if (!string.IsNullOrEmpty(orgId))
            {
                sqlStr.Append(" AND a.OrganizeId = @OrganizeId");
                parlist.Add(new SqlParameter("@OrganizeId", orgId));
            }
            if(!string.IsNullOrWhiteSpace(isbook))
            {
                sqlStr.Append(" AND a.isbook = @isbook");
                parlist.Add(new SqlParameter("@isbook", isbook));
            }
            sqlStr.Append(" order by a.OutDate");
            var list = this.FindList<OutBookScheduleVO>(sqlStr.ToString(), parlist.ToArray()).ToList();
            return list;
        }
        //获取Schedule列表
        public IList<OutBookScheduleVO> GetPagintionListTime(Pagination pagination ,string orgId, string kssj, string jssj, string ys, string czztcx, string ScheduId, string ks, string lx,string isbook, out Pagination paging )
        {
            if (string.IsNullOrEmpty(orgId))
            {
                paging = new Pagination();
                return null;
            }
            StringBuilder sqlStr = new StringBuilder();
            var parlist = new List<SqlParameter>();
            sqlStr.Append(@"select a.*,b.sfxmmc,c.sfxmmc as zlxmmc from  mz_ghpb_schedule  a
			left join NewtouchHIS_Base.[dbo].[V_S_xt_sfxm] b
			on b.sfxmCode=a.ghlx and b.OrganizeId=a.OrganizeId
			left join NewtouchHIS_Base.[dbo].[V_S_xt_sfxm] c 
			on c.sfxmCode=a.zlxm and c.OrganizeId=a.OrganizeId 
            where 1=1 and a.zt='1' ");

            if (!string.IsNullOrEmpty(orgId))
            {
                sqlStr.Append(" AND a.OrganizeId = @OrganizeId");
                parlist.Add(new SqlParameter("@OrganizeId", orgId));
            }
            
            if (czztcx != null && czztcx != "")
            {
                if (czztcx == "1")
                {
                    sqlStr.Append(" and a.istz='0'");
                }
                else if (czztcx == "2")
                {
                    sqlStr.Append(" and a.istz='1'");
                }
            }

            if (ys != null && ys != "")
            {
                sqlStr.Append(" and a.ysgh=@ys ");
            }
            if (ScheduId != null && ScheduId != "")
            {
                sqlStr.Append(" and a.ScheduId=@ScheduId ");
                parlist.Add(new SqlParameter("@ScheduId", ScheduId));
            }
            else
            {
                sqlStr.Append(" and a.OutDate >= @kssj and a.OutDate <= @jssj ");
                parlist.Add(new SqlParameter("@kssj", kssj));
                parlist.Add(new SqlParameter("@jssj", jssj));
            }
            if (ks != null && ks != "")
            {
                sqlStr.Append(" and a.czks=@ks ");
                parlist.Add(new SqlParameter("@ks", ks));
            }
            if (lx != null && lx != "")
            {
                sqlStr.Append(" and a.RegType=@lx ");
                parlist.Add(new SqlParameter("@lx", lx));
            }
            if (ys != null && ys != "")
            {
                parlist.Add(new SqlParameter("@ys", ys));
            }
            if (!string.IsNullOrWhiteSpace(isbook))
            {
                sqlStr.Append(" AND a.isbook = @isbook");
                parlist.Add(new SqlParameter("@isbook", isbook));
            }
            var list = this.QueryWithPage<OutBookScheduleVO>(sqlStr.ToString(),pagination, parlist.ToArray()).ToList();
            paging = pagination;
            return list;
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <returns></returns>
        public int ExecSchedule()
        {
            string sql = "exec sp_mz_ghpb_schedule_create";
            SqlParameter[] para ={
                };
            return this.ExecuteSqlCommand(sql, para);
        }        
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <returns></returns>
        public int ExecSchedulebyGroup()
        {
            string sql = "exec sp_mz_ghpb_schedulebyzh_create ";
            SqlParameter[] para ={
                };
            return this.ExecuteSqlCommand(sql, para);
        }

    }
}
