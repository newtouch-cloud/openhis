using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using Newtouch.MR.ManageSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.DomainServices
{
    public class EMRDmnService: DmnServiceBase, IEMRDmnService
    {
        public EMRDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        #region EMR 相关

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

                string sql = "exec [Newtouch_EMR].dbo.usp_Pat_MedRecordTree @OrgId=@OrgId,@zyh=@zyh,@rygh=@rygh ";
                return this.FindList<PatMedRecordTreeVO>(sql, para);

            }
            catch (Exception ex)
            {
                throw new FailedException("获取病历列表异常，" + ex.InnerException);
            }
        }

        /// <summary>
        /// 获取无病案病人列表
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="OrgId"></param>
        /// <param name="type"></param>
        /// <param name="brzt"></param>
        /// <returns></returns>
        public IList<PatListVO> GetPatList(Pagination pagination, string keyword, string zyh,
            string cyts, string blzt, string ysgh, string OrgId, int type)
        {
            string sql = "";
            var para = new List<SqlParameter>();
            para.Add(new SqlParameter("@OrgId", OrgId));
            para.Add(new SqlParameter("@ysgh", ysgh));

            int wtj = (int)EnumRecordStu.wtj;
            
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                sql = @"select a.[Id],a.[OrganizeId],a.[zyh],a.[blh],a.[xm],a.[py],[wb],a.[sfzh],[sex],[birth],[zybz],[sfqj],[DeptCode]
,[WardCode],[ysgh],[BedCode],[ryrq],[rqrq],CONVERT(datetime, cqrq, 23) cqrq,[wzjb],[hljb],[ryfs],[cyfs],[gdxmzxrq],[brxzdm],[brxzmc]
,[cardno],[cardtype],[lxr],[lxrgx],a.[lxrdh],[zddm],[zdmc],[cyzddm],[cyzdmc],[Memo],(case when a.zyh = @zyh then 1 else 0 end ) isCheck,
cwmc,bqmc,isnull(RecordStu," + wtj.ToString() + @") RecordStu,CommitTime,Commitor,cyts,c.BAH
from V_EMR_InpPatInfo a
left join [Newtouch_EMR].dbo.mr_basy c
on a.zyh=c.ZYH and a.OrganizeId=c.OrganizeId and c.zt='1'
where a.[OrganizeId]=@OrgId
and exists(select 1 from NewtouchHIS_Base.dbo.V_C_Sys_StaffWard b with(nolock) where a.organizeid=b.organizeid and b.staffgh=@ysgh and b.bqCode=a.WardCode  and b.zt=1) 
and not exists(select 1 from mr_basy m with(nolock) where a.organizeid=m.organizeid and  m.zt='1' and a.zyh=m.zyh) ";

                para.Add(new SqlParameter("@zyh", zyh));
            }
            else
            {
                sql = @"select a.[Id],a.[OrganizeId],a.[zyh],a.[blh],a.[xm],a.[py],[wb],a.[sfzh],[sex],[birth],[zybz],[sfqj],[DeptCode]
,[WardCode],[ysgh],[BedCode],[ryrq],[rqrq],CONVERT(datetime, cqrq, 23)  cqrq,[wzjb],[hljb],[ryfs],[cyfs],[gdxmzxrq],[brxzdm],[brxzmc]
,[cardno],[cardtype],[lxr],[lxrgx],a.[lxrdh],[zddm],[zdmc],[cyzddm],[cyzdmc],[Memo],0 isCheck,
cwmc,bqmc,isnull(RecordStu," + wtj.ToString() + @") RecordStu,CommitTime,Commitor,cyts,c.BAH
from V_EMR_InpPatInfo a
left join [Newtouch_EMR].dbo.mr_basy c
on a.zyh=c.ZYH and a.OrganizeId=c.OrganizeId and c.zt='1'
where a.[OrganizeId]=@OrgId
and exists(select 1 from NewtouchHIS_Base.dbo.V_C_Sys_StaffWard b with(nolock) where a.organizeid=b.organizeid and b.staffgh=@ysgh and b.bqCode=a.WardCode  and b.zt=1) 
and not exists(select 1 from mr_basy m with(nolock) where a.organizeid=m.organizeid and  m.zt='1' and a.zyh=m.zyh)";

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    sql += " and (a.zyh like '%" + @keyword + "%' or charindex(@keyword,a.xm)>0) ";
                    para.Add(new SqlParameter("@keyword", keyword));
                }
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

           

            sql += " and isnull(RecordStu,0)=@blzt ";
            para.Add(new SqlParameter("@blzt", (int)EnumRecordStu.yqs));

            if (!string.IsNullOrWhiteSpace(cyts))
            {
                int ts = Convert.ToInt16(cyts);
                if (ts > 0)
                {
                    string rq = DateTime.Now.AddDays(-ts + 1).ToString("yyyy-MM-dd");
                    sql += " and cqrq<='" + rq + "' ";
                }

            }

            //return this.QueryWithPage<PatListVO>(sql, pagination, para.ToArray()).ToList();
            var result= this.QueryWithPage<PatListVO>(sql, pagination, para.ToArray()).ToList();
            return result;
        }

		#endregion


		public IList<PatListVO> GetPagintionList(Pagination pagination, string keyword, string zyh,
			string cyts, string blzt, string ysgh, string OrgId, int type, string kssj, string jssj)
		{
			string sql = "";
			var para = new List<SqlParameter>();
			para.Add(new SqlParameter("@OrgId", OrgId));
			para.Add(new SqlParameter("@ysgh", ysgh));

			int wtj = (int)EnumRecordStu.wtj;

			if (!string.IsNullOrWhiteSpace(zyh))
			{
				sql = @"SELECT a.[Id],a.[OrganizeId],[zyh],[blh],[xm],a.[py],[wb],[sfzh],[sex],[birth],[zybz],[sfqj],[DeptCode],e.[name] DeptName
                ,[WardCode],[ysgh],[BedCode],[ryrq],[rqrq],[cqrq],[wzjb],[hljb],[ryfs],[cyfs],[gdxmzxrq],[brxzdm],[brxzmc]
                ,[cardno],[cardtype],[lxr],[lxrgx],[lxrdh],[zddm],[zdmc],[cyzddm],[cyzdmc],[Memo],a.[CreateTime]
                ,a.[CreatorCode],a.[LastModifyTime],a.[LastModifierCode],a.[zt],(case when zyh=@zyh then 1 else 0 end ) isCheck,
                c.cwmc,d.bqmc,isnull(RecordStu," + wtj.ToString() + @") RecordStu,CommitTime,Commitor
                ,(case when cqrq is not null then datediff(dd,cqrq,getdate()) else null end) cyts
                FROM [Newtouch_EMR].[dbo].[zy_brjbxx] a  with(nolock)
                left join NewtouchHIS_Base.dbo.V_S_xt_cw c with(nolock) on a.organizeid=c.organizeid and a.bedcode=c.cwcode and c.zt=1 
                left join NewtouchHIS_Base..V_S_xt_bq  d with(nolock) on a.organizeid=d.organizeid and a.wardcode=d.bqcode and d.zt=1
				left join NewtouchHIS_Base.dbo.Sys_Department e on a.deptcode=e.code and a.organizeid=d.organizeid and d.zt=1                 
                where a.zt=1 and a.[OrganizeId]=@OrgId
                and exists(select 1 from [NewtouchHIS_Sett].dbo.zy_brjbxx e where a.zyh=e.zyh and a.organizeid=e.organizeid and e.zt='1')
                and exists(select 1 from NewtouchHIS_Base..V_C_Sys_StaffWard b with(nolock) 
		                where a.organizeid=b.organizeid and b.staffgh=@ysgh and b.bqCode=a.WardCode and b.zt=1) ";

				para.Add(new SqlParameter("@zyh", zyh));
			}
			else
			{
				sql = @"SELECT a.[Id],a.[OrganizeId],[zyh],[blh],[xm],a.[py],[wb],[sfzh],[sex],[birth],[zybz],[sfqj],[DeptCode],e.[name] DeptName
                ,[WardCode],[ysgh],[BedCode],[ryrq],[rqrq],[cqrq],[wzjb],[hljb],[ryfs],[cyfs],[gdxmzxrq],[brxzdm],[brxzmc]
                ,[cardno],[cardtype],[lxr],[lxrgx],[lxrdh],[zddm],[zdmc],[cyzddm],[cyzdmc],[Memo],a.[CreateTime]
                ,a.[CreatorCode],a.[LastModifyTime],a.[LastModifierCode],a.[zt],0 isCheck,
                c.cwmc,d.bqmc,isnull(RecordStu," + wtj.ToString() + @") RecordStu,CommitTime,Commitor
                ,(case when cqrq is not null then datediff(dd,cqrq,getdate()) else null end) cyts                        
                FROM [Newtouch_EMR].[dbo].[zy_brjbxx] a  with(nolock)
                left join NewtouchHIS_Base.dbo.V_S_xt_cw c with(nolock) on a.organizeid=c.organizeid and a.bedcode=c.cwcode and c.zt=1 
                left join NewtouchHIS_Base..V_S_xt_bq  d with(nolock) on a.organizeid=d.organizeid and a.wardcode=d.bqcode and d.zt=1
				left join NewtouchHIS_Base.dbo.Sys_Department e on a.deptcode=e.code and a.organizeid=d.organizeid and d.zt=1                 
				where a.zt=1 and a.[OrganizeId]=@OrgId
                and exists(select 1 from [NewtouchHIS_Sett].dbo.zy_brjbxx e where a.zyh=e.zyh and a.organizeid=e.organizeid and e.zt='1')
                and exists(select 1 from NewtouchHIS_Base..V_C_Sys_StaffWard b with(nolock) 
		            where a.organizeid=b.organizeid and b.staffgh=@ysgh and b.bqCode=a.WardCode  and b.zt=1) ";

				if (!string.IsNullOrWhiteSpace(keyword))
				{
					sql += " and (zyh like '%" + @keyword + "%' or charindex(@keyword,xm)>0) ";
					para.Add(new SqlParameter("@keyword", keyword));
				}
			}

            if (!string.IsNullOrWhiteSpace(kssj) && !string.IsNullOrWhiteSpace(jssj))
            {
                sql += " and cqrq between '" + kssj + "' and '" + jssj + "'+' 23:59:59'  ";
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

			if (blzt != ((int)EnumRecordStu.wtj).ToString()) //尚未提交
			{
				sql += " and isnull(RecordStu,0)=@blzt ";
				para.Add(new SqlParameter("@blzt", blzt));
			}
			else{
				sql += " and isnull(RecordStu,0)<>"+(int)EnumRecordStu.ytj+" and isnull(RecordStu,0)<>"+(int)EnumRecordStu.yqs;
				//sql += " and CONVERT (nvarchar(12),GETDATE(),112) - a.cqrq >3";
			}

			//if (!string.IsNullOrWhiteSpace(cyts))
			//{
			//	int ts = Convert.ToInt16(cyts);
			//	if (ts > 0)
			//	{
			//		string rq = DateTime.Now.AddDays(-ts + 1).ToString("yyyy-MM-dd");
			//		sql += " and cqrq<='" + rq + "' ";
			//	}

			//}

            

            //return this.QueryWithPage<PatListVO>(sql, pagination, para.ToArray()).ToList();
            var result = this.QueryWithPage<PatListVO>(sql, pagination, para.ToArray()).ToList();
			return result;
		}

        public ZybrjbxxVO GetZyPatInfobyzyh(string orgId,string zyh) {
            string sql = @"select a.[Id],a.[OrganizeId],a.[zyh],a.[blh],a.[xm],a.[py],a.[wb],a.[sfzh],
(case when a.sex=2 then '女' when a.sex=1 then '男' else '未说明' end)[sex],a.[birth],convert(varchar(10),a.[zybz])zybz,a.[sfqj],a.[DeptCode]
,a.[WardCode],[ysgh],a.[BedCode],a.[ryrq],a.[rqrq],a.[cqrq],a.[wzjb],a.[hljb],convert(varchar(10),a.[ryfs])ryfs,convert(varchar(10),[cyfs])cyfs,a.[gdxmzxrq],a.[brxzdm],a.[brxzmc]
,a.[cardno],a.[cardtype],a.[lxr],a.[lxrgx],a.[lxrdh],a.[zddm],a.[zdmc],a.[cyzddm],a.[cyzdmc],a.[Memo],--(case when a.zyh = @zyh then 1 else 0 end ) isCheck,
cwmc BedName,a.bqmc,convert(varchar(10),cyts)cyts
,b.[Name] ysxm,c.bqmc WardName,d.[Name] DeptName,convert(varchar(10),e.nl)nl
from V_EMR_InpPatInfo a
left join NewtouchHIS_Base.dbo.V_S_Sys_Staff b on a.ysgh=b.gh and a.OrganizeId=b.OrganizeId and b.zt='1'
left join NewtouchHIS_Base.dbo.V_S_xt_bq c on a.WardCode=c.bqCode and a.OrganizeId=c.OrganizeId and c.zt='1'
left join NewtouchHIS_Base.dbo.Sys_Department d on a.DeptCode=d.code and a.OrganizeId=d.OrganizeId and d.zt='1'
left join [NewtouchHIS_Sett].dbo.zy_brjbxx e on a.zyh=e.zyh and a.organizeid=e.organizeid and e.zt='1'
where a.[OrganizeId]=@OrgId and a.zyh=@zyh   
";
            return FindList<ZybrjbxxVO>(sql, new SqlParameter[] {
                new SqlParameter("OrgId",orgId),
                new SqlParameter("zyh",zyh)
            }).FirstOrDefault();
        }
	}
}
