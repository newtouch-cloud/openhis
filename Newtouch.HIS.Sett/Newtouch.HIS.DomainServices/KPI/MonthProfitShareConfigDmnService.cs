using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects.KPI;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.HIS.DomainServices.KPI
{
    /// <summary>
    /// 月利润分成
    /// </summary>
    public class MonthProfitShareConfigDmnService : DmnServiceBase, IMonthProfitShareConfigDmnService
    {
        private readonly ITherapeutistMonthProfitShareConfigRepo _therapeutistMonthProfitShareConfigRepo;

        private readonly IMedicalOrgMonthProfitShareConfigRepo _medicalOrgMonthProfitShareConfigRepo;

        public MonthProfitShareConfigDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        #region 治疗师

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<TherapeutistProfitShareConfigVO> GetTherapeutistPSConfigList(string orgId, string keyword)
        {
            var sb = new StringBuilder();
            sb.Append(@"select a.*, b.dlmc sfdlmc, d.sfxmmc sfxmmc, c.Name zlsxm, c.Id zlsId
from zlsps_m_config a
left join [NewtouchHIS_Base]..V_S_xt_sfdl b
on a.dlCode = b.dlCode and b.zt = '1' and b.OrganizeId = a.OrganizeId
left join [NewtouchHIS_Base]..V_S_Sys_Staff c
on a.gh = c.gh and b.zt = '1' and c.OrganizeId = a.OrganizeId
left join[NewtouchHIS_Base]..V_S_xt_sfxm d
on a.sfxmCode = d.sfxmCode and d.zt = '1' and d.OrganizeId = a.OrganizeId

where a.OrganizeId = @orgId
");
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sb.Append(" and (1 = 2");
                sb.Append(" or a.dlCode like @keyword");
                sb.Append(" or b.dlmc like @keyword");
                sb.Append(" or a.gh like @keyword");
                sb.Append(" or c.Name like @keyword");
                sb.Append(")");
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            sb.Append(" order by a.dlCode, a.gh, a.zt desc, a.CreateTime");

            return this.FindList<TherapeutistProfitShareConfigVO>(sb.ToString(), pars.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public TherapeutistProfitShareConfigVO GetTherapeutistPSConfig(string keyValue)
        {
            var sb = new StringBuilder();
            sb.Append(@"select a.*, b.dlmc sfdlmc, d.sfxmmc sfxmmc, c.Name zlsxm, c.Id zlsId
from zlsps_m_config a
left join [NewtouchHIS_Base]..V_S_xt_sfdl b
on a.dlCode = b.dlCode and b.zt = '1' and b.OrganizeId = a.OrganizeId
left join [NewtouchHIS_Base]..V_S_Sys_Staff c
on a.gh = c.gh and b.zt = '1' and c.OrganizeId = a.OrganizeId
left join[NewtouchHIS_Base]..V_S_xt_sfxm d
on a.sfxmCode = d.sfxmCode and d.zt = '1' and d.OrganizeId = a.OrganizeId

where a.Id = @Id
");
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@Id", keyValue ?? ""));

            return this.FirstOrDefault<TherapeutistProfitShareConfigVO>(sb.ToString(), pars.ToArray());
        }

        /// <summary>
        /// 提交治疗师提成配置
        /// </summary>
        /// <param name="entity"></param>
        public void SubmitTherapeutistPSConfig(TherapeutistMonthProfitShareConfigEntity entity, string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                if (_therapeutistMonthProfitShareConfigRepo.IQueryable(p => p.OrganizeId == entity.OrganizeId && p.dlCode == entity.dlCode && p.gh == entity.gh
                    && p.Id != keyValue).Any())
                {
                    throw new FailedException("重复配置");
                }
                entity.Modify(keyValue);
                _therapeutistMonthProfitShareConfigRepo.Update(entity);
            }
            else
            {
                if (_therapeutistMonthProfitShareConfigRepo.IQueryable(p => p.OrganizeId == entity.OrganizeId && p.dlCode == entity.dlCode && p.gh == entity.gh).Any())
                {
                    throw new FailedException("重复配置");
                }
                entity.Create(true);
                _therapeutistMonthProfitShareConfigRepo.Insert(entity);
            }
        }

        /// <summary>
        /// 治疗师利润提成 固定报表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="curUserCode"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="isReGene"></param>
        public void DoGenerateTherapeutistPS(string orgId, string curUserCode, int year, int month, bool? isReGene)
        {
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            pars.Add(new SqlParameter("@year", year));
            pars.Add(new SqlParameter("@month", month));
            pars.Add(new SqlParameter("@curUserCode", curUserCode));
            pars.Add(new SqlParameter("@isReGene", isReGene ?? false));
            var flagPar = new SqlParameter("@flag", System.Data.SqlDbType.Bit);
            flagPar.Direction = System.Data.ParameterDirection.Output;
            pars.Add(flagPar);
            var msgPar = new SqlParameter("@msg", System.Data.SqlDbType.VarChar, 128);
            msgPar.Direction = System.Data.ParameterDirection.Output;
            pars.Add(msgPar);

            this.ExecuteSqlCommand("exec usp_kpi_zls_mainlist_dogenerate @orgId, @year, @month, @curUserCode, @isReGene, @flag out, @msg out", pars.ToArray());

            var flag = flagPar.Value as bool?;
            if (!(flag ?? false))
            {
                var msg = msgPar.Value as string;
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    throw new FailedException(msg);
                }
                else
                {
                    throw new FailedException("执行存储过程失败，内部错误");
                }
            }
        }

        /// <summary>
        /// Check报表是否已经生成过
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public bool TherapeutistPSCheckIsGenerated(string orgId, int year, int month)
        {
            var sql = "select 1 from zlsps_m_main where zt = '1' and [year] = @year and [month] = @month and OrganizeId = @orgId";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            pars.Add(new SqlParameter("@year", year));
            pars.Add(new SqlParameter("@month", month));
            return this.FirstOrDefault<int?>(sql, pars.ToArray()) == 1;
        }

        #endregion

        #region 医疗机构

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<MedicalOrgProfitShareConfigVO> GetMedicalOrgPSConfigList(string orgId, string keyword)
        {
            var sb = new StringBuilder();
            sb.Append(@"select a.*, b.dlmc sfdlmc, c.sfxmmc sfxmmc
from medicalorgps_m_config a
left
join [NewtouchHIS_Base]..V_S_xt_sfdl b
on a.dlCode = b.dlCode and b.zt = '1' and b.OrganizeId = a.OrganizeId
left join[NewtouchHIS_Base]..V_S_xt_sfxm c
on a.sfxmCode = c.sfxmCode and c.zt = '1' and c.OrganizeId = a.OrganizeId

where a.OrganizeId = @orgId
");
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sb.Append(" and (1 = 2");
                sb.Append(" or a.dlCode like @keyword");
                sb.Append(" or b.dlmc like @keyword");
                sb.Append(" or a.sfxmCode like @keyword");
                sb.Append(" or c.sfxmmc like @keyword");
                sb.Append(")");
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            sb.Append(" order by a.dlCode, a.sfxmCode, a.zt desc, a.CreateTime");

            return this.FindList<MedicalOrgProfitShareConfigVO>(sb.ToString(), pars.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public MedicalOrgProfitShareConfigVO GetMedicalOrgPSConfig(string keyValue)
        {
            var sb = new StringBuilder();
            sb.Append(@"select a.*, b.dlmc sfdlmc, c.sfxmmc sfxmmc
from medicalorgps_m_config a
left
join [NewtouchHIS_Base]..V_S_xt_sfdl b
on a.dlCode = b.dlCode and b.zt = '1' and b.OrganizeId = a.OrganizeId
left join[NewtouchHIS_Base]..V_S_xt_sfxm c
on a.sfxmCode = c.sfxmCode and c.zt = '1' and c.OrganizeId = a.OrganizeId

where a.Id = @Id
");
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@Id", keyValue ?? ""));

            return this.FirstOrDefault<MedicalOrgProfitShareConfigVO>(sb.ToString(), pars.ToArray());
        }

        /// <summary>
        /// 提交医疗机构提成配置
        /// </summary>
        /// <param name="entity"></param>
        public void SubmitMedicalOrgPSConfig(MedicalOrgMonthProfitShareConfigEntity entity, string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                if (_medicalOrgMonthProfitShareConfigRepo.IQueryable(p => p.OrganizeId == entity.OrganizeId && p.dlCode == entity.dlCode && p.sfxmCode == entity.sfxmCode
    && p.Id != keyValue).Any())
                {
                    throw new FailedException("重复配置");
                }
                entity.Modify(keyValue);
                _medicalOrgMonthProfitShareConfigRepo.Update(entity);
            }
            else
            {
                if (_medicalOrgMonthProfitShareConfigRepo.IQueryable(p => p.OrganizeId == entity.OrganizeId && p.dlCode == entity.dlCode && p.sfxmCode == entity.sfxmCode).Any())
                {
                    throw new FailedException("重复配置");
                }
                entity.Create(true);
                _medicalOrgMonthProfitShareConfigRepo.Insert(entity);
            }
        }

        /// <summary>
        /// 医疗机构利润分成 固定报表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="curUserCode"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="isReGene"></param>
        public void DoGenerateMedicalOrgPS(string orgId, string curUserCode, int year, int month, bool? isReGene)
        {
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            pars.Add(new SqlParameter("@year", year));
            pars.Add(new SqlParameter("@month", month));
            pars.Add(new SqlParameter("@curUserCode", curUserCode));
            pars.Add(new SqlParameter("@isReGene", isReGene ?? false));
            var flagPar = new SqlParameter("@flag", System.Data.SqlDbType.Bit);
            flagPar.Direction = System.Data.ParameterDirection.Output;
            pars.Add(flagPar);
            var msgPar = new SqlParameter("@msg", System.Data.SqlDbType.VarChar, 128);
            msgPar.Direction = System.Data.ParameterDirection.Output;
            pars.Add(msgPar);

            this.ExecuteSqlCommand("exec usp_ps_medicalorg_mainlist_dogenerate @orgId, @year, @month, @curUserCode, @isReGene, @flag out, @msg out", pars.ToArray());

            var flag = flagPar.Value as bool?;
            if (!(flag ?? false))
            {
                var msg = msgPar.Value as string;
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    throw new FailedException(msg);
                }
                else
                {
                    throw new FailedException("执行存储过程失败，内部错误");
                }
            }
        }

        /// <summary>
        /// Check报表是否已经生成过
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public bool MedicalOrgPSCheckIsGenerated(string orgId, int year, int month)
        {
            var sql = "select 1 from medicalorgps_m_main where zt = '1' and [year] = @year and [month] = @month and OrganizeId = @orgId";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            pars.Add(new SqlParameter("@year", year));
            pars.Add(new SqlParameter("@month", month));
            return this.FirstOrDefault<int?>(sql, pars.ToArray()) == 1;
        }

        #endregion

    }
}
