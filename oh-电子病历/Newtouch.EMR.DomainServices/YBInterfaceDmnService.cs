using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using Newtouch.EMR.Domain.DTO;
using Newtouch.EMR.Domain.DTO.OutputDto.MRUpload;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.EMR.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.EMR.DomainServices
{
    public class YBInterfaceDmnService : DmnServiceBase, IYBInterfaceDmnService
    {

        private readonly IYBInpPatRegInfoDiagRepo _yBInpPatRegInfoDiagRepo;
        private readonly IYBInpOutHosSummariesDiagRepo _yBInpOutHosSummariesDiagRepo;
        public YBInterfaceDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public YB_7100 GetRyblInfo(string orgId,string dzblId)
        {
            string sql = @"select *
  FROM [dbo].[YB_Inp_PatRegInfo]
  where zt='1' and OrganizeId=@orgId and Id=@dzblId
--for xml path('INPUT')";
            return this.FirstOrDefault<YB_7100>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@dzblId",dzblId)
            });
        }

        public IList<YB_7110> GetRyblZdInfo(string orgId, string dzblId)
        {
            string sql = @"select *
  FROM [dbo].[YB_Inp_PatRegInfo_Diag]
  where zt='1' and OrganizeId=@orgId and RegId=@dzblId
--for xml path('INPUT')";
            return this.FindList<YB_7110>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@dzblId",dzblId)
            });
        }

        public YB_7200 GetBcjlInfo(string orgId, string dzblId)
        {
            string sql = @"select *
  FROM [dbo].[YB_Inp_CourseDisease]
  where zt='1' and OrganizeId=@orgId and Id=@dzblId
--for xml path('INPUT')";
            return this.FirstOrDefault<YB_7200>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@dzblId",dzblId)
            });
        }
        public YB_7500 GetCyxjInfo(string orgId, string dzblId)
        {
            string sql = @"select *
  FROM [dbo].[YB_Inp_OutHosSummaries]
  where zt='1' and OrganizeId=@orgId and Id=@dzblId
--for xml path('INPUT')";
            return this.FirstOrDefault<YB_7500>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@dzblId",dzblId)
            });
        }

        public IList<YB_7510> GetCyxjZdInfo(string orgId, string dzblId)
        {
            string sql = @"select *
  FROM [dbo].[YB_Inp_OutHosSummaries_Diag]
  where zt='1' and OrganizeId=@orgId and OutId=@dzblId
--for xml path('INPUT')";
            return this.FindList<YB_7510>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@dzblId",dzblId)
            });
        }

        public IList<YB_7610> GetBasyZdInfo(string orgId, string zyh)
        {
            string sql = @"select zyh AKC190, bah BKF303,zyh BKC191,(case when ZDOrder=1 then '1' else '2' end)BKF718,
convert(varchar(10),ZDOrder) BKF512,JBDM BKF857 ,JBMC BKF856,convert(varchar(10),RYBQ) BKF262
from mr_basy_zd with(nolock)
where OrganizeId=@orgId and ZYH=@zyh and zt='1'
";
            return this.FindList<YB_7610>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@zyh",zyh)
            });
        }
        /// <summary>
        /// 入院诊断保存
        /// </summary>
        /// <param name="zdlist"></param>
        /// <param name="orgId"></param>
        /// <param name="dzblId"></param>
        /// <param name="zyh"></param>
        public string YBInhosDiagInfoSave(List<DiagnoseDocDTO> zdlist,string orgId,string dzblId,string zyh)
        {
            string errmsg = "";
            if (zdlist != null && zdlist.Count > 0 && !string.IsNullOrWhiteSpace(orgId) && !string.IsNullOrWhiteSpace(dzblId))
            {
                var check = zdlist.Find(p => string.IsNullOrWhiteSpace(p.zdfl) || string.IsNullOrWhiteSpace(p.zdlx));
                if (check != null)
                {
                    errmsg = "诊断关键信息（诊断分类、诊断标识）不全，保存失败。";
                }

                var exist = _yBInpPatRegInfoDiagRepo.FindEntity(p => p.RegId == dzblId && p.zt=="1" && p.OrganizeId==orgId);
                if (exist != null)
                {
                    string sql = @"update a set a.zt='0',a.LastModifyTime=getdate(),a.LastModifierCode='admin'
from YB_Inp_PatRegInfo_Diag a where a.RegId=@Id and a.OrganizeId=@orgId and a.zt='1'";
                    this.ExecuteSqlCommand(sql, new SqlParameter[] {
                        new SqlParameter("@orgId",orgId),
                        new SqlParameter("@Id",dzblId)
                    });
                }
                foreach (DiagnoseDocDTO zd in zdlist)
                {
                    YBInpPatRegInfoDiagEntity ety = new YBInpPatRegInfoDiagEntity();
                    ety.AKC190 = zyh;
                    ety.BKC191 = zyh;
                    ety.BKF260 = zd.zdfl;
                    ety.BKF718 = zd.zdlx;
                    ety.BKF512 = zd.px.ToString() ;
                    ety.BKF857 = zd.zdCode;
                    ety.BKF856 = zd.zdName;
                    ety.BKF510 = DateTime.Now.ToString("yyyyMMddHHmmss");
                    ety.OrganizeId = orgId;
                    ety.RegId = dzblId;
                    _yBInpPatRegInfoDiagRepo.Submit(ety);
                }
            }

            return errmsg;
        }
        /// <summary>
        /// 出院诊断保存
        /// </summary>
        /// <param name="zdlist"></param>
        /// <param name="orgId"></param>
        /// <param name="dzblId"></param>
        /// <param name="zyh"></param>
        public string YBOuthosDiagInfoSave(List<DiagnoseDocDTO> zdlist, string orgId, string dzblId, string zyh)
        {
            string errmsg = "";
            if (zdlist != null && zdlist.Count > 0 && !string.IsNullOrWhiteSpace(orgId) && !string.IsNullOrWhiteSpace(dzblId))
            {
                var check = zdlist.Find(p => string.IsNullOrWhiteSpace(p.zdfl) || string.IsNullOrWhiteSpace(p.zdlx));
                if (check != null)
                {
                    errmsg = "诊断关键信息（诊断分类、诊断标识）不全，保存失败。";
                }

                var exist = _yBInpOutHosSummariesDiagRepo.FindEntity(p=>p.OutId==dzblId && p.OrganizeId==orgId && p.zt=="1");
                if (exist != null)
                {
                    string sql = @"update a set a.zt='0',a.LastModifyTime=getdate(),a.LastModifierCode='admin'
from YB_Inp_OutHosSummaries_Diag a where a.OutId=@Id and a.OrganizeId=@orgId and a.zt='1'";
                    this.ExecuteSqlCommand(sql, new SqlParameter[] {
                        new SqlParameter("@orgId",orgId),
                        new SqlParameter("@Id",dzblId)
                    });
                }
                foreach (DiagnoseDocDTO zd in zdlist)
                {
                    YBInpOutHosSummariesDiagEntity ety = new YBInpOutHosSummariesDiagEntity();
                    ety.AKC190 = zyh;
                    ety.BKC191 = zyh;
                    ety.BKF417 = zd.zdfl;
                    ety.BKF718 = zd.zdlx;
                    ety.BKF512 = zd.px.ToString();
                    ety.BKF857 = zd.zdCode;
                    ety.BKF856 = zd.zdName;
                    ety.OrganizeId = orgId;
                    ety.OutId = dzblId;
                    _yBInpOutHosSummariesDiagRepo.Submit(ety);
                }
            }
            return errmsg;

        }
    }
}
