using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.KnowledgeBaseManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.KnowledgeBaseManage;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.DomainServices.KnowledgeBaseManage
{
    public class MedicalRecordDmnService : DmnServiceBase, IMedicalRecordDmnService
    {
        public MedicalRecordDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        /// <summary>
        /// 获取病案首页诊断信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<MedicalRecordDiagnosisVO> GetAllMedicalRecordDiagnosisVOList(string zyh, string orgId)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                return null;
            }
            StringBuilder sqlStr = new StringBuilder();
            IList<SqlParameter> inSqlParameterList = new List<SqlParameter>();
            sqlStr.Append(@"
select id zdId,JBDM zddm,jbmc zdmc from [Newtouch_EMR]..mr_basy_zd
where zyh=@zyh and OrganizeId=@orgId
 order by zdorder 
                        ");
            inSqlParameterList.Add(new SqlParameter("@orgId", orgId.Trim()));
            inSqlParameterList.Add(new SqlParameter("@zyh", (zyh ?? "").Trim()));

            var list = this.FindList<MedicalRecordDiagnosisVO>(sqlStr.ToString(), inSqlParameterList.ToArray()).ToList();
            return list;
        }
        /// <summary>
        /// 获取病案首页手术信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<MedicalRecordOperationVO> GetAllMedicalRecordOperationVOList(string zyh, string orgId)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                return null;
            }
            StringBuilder sqlStr = new StringBuilder();
            IList<SqlParameter> inSqlParameterList = new List<SqlParameter>();
            sqlStr.Append(@"
 select id ssId,SSJCZBM ssdm,SSJCZMC ssmc from  [Newtouch_EMR]..mr_basy_ss
where zyh=@zyh and OrganizeId=@orgId
   order by SSOrder 
                        ");
            inSqlParameterList.Add(new SqlParameter("@orgId", orgId.Trim()));
            inSqlParameterList.Add(new SqlParameter("@zyh", (zyh ?? "").Trim()));

            var list = this.FindList<MedicalRecordOperationVO>(sqlStr.ToString(), inSqlParameterList.ToArray()).ToList();
            return list;
        }
        /// <summary>
        /// 病案首页诊断信息排序
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string UpdatePatDiagnosisOrder(string zdIds, string orgId)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                return null;
            }
            try
            {
                for (int i = 0; i < zdIds.Split(',').Length; i++)
                {
                    string sqlStr = "";
                    IList<SqlParameter> inSqlParameterList = new List<SqlParameter>();
                    sqlStr = "update  [Newtouch_EMR]..mr_basy_zd set ZDOrder=@order where id=@zdIds and OrganizeId=@orgId";
                    inSqlParameterList.Add(new SqlParameter("@order", (i + 1)));
                    inSqlParameterList.Add(new SqlParameter("@orgId", orgId.Trim()));
                    inSqlParameterList.Add(new SqlParameter("@zdIds", zdIds.Split(',')[i]));
                    this.ExecuteSqlCommand(sqlStr, inSqlParameterList.ToArray());
                }

            }
            catch (System.Exception ex)
            {

                return ex.Message;
            }
            return "";
        }
        /// <summary>
        /// 病案首页手术信息排序
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string UpdatePatOperationOrder(string ssIds, string orgId)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                return null;
            }
            try
            {
                for (int i = 0; i < ssIds.Split(',').Length; i++)
                {
                    string sqlStr = "";
                    IList<SqlParameter> inSqlParameterList = new List<SqlParameter>();
                    sqlStr = "update  [Newtouch_EMR]..mr_basy_ss set ssOrder=@order where id=@ssIds and OrganizeId=@orgId";
                    inSqlParameterList.Add(new SqlParameter("@order", (i + 1)));
                    inSqlParameterList.Add(new SqlParameter("@orgId", orgId.Trim()));
                    inSqlParameterList.Add(new SqlParameter("@ssIds", ssIds.Split(',')[i]));
                    this.ExecuteSqlCommand(sqlStr, inSqlParameterList.ToArray());
                }

            }
            catch (System.Exception ex)
            {

                return ex.Message;
            }
            return "";
        }
        /// <summary>
        /// 获取病案首页病人信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public PatInformationVO GetPatInformationList(string zyh, string orgId)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                return null;
            }
            StringBuilder sqlStr = new StringBuilder();
            IList<SqlParameter> inSqlParameterList = new List<SqlParameter>();
            sqlStr.Append(@"
   select bah bah,convert(varchar(100),xb)  xb,
   convert(varchar(100),nl) nl,'' nlt,
   convert(varchar(100),XSECSTZ) cstz,
   CYKB ks,convert(varchar(100),SJZYTS) ts,convert(varchar(100),LYFS) LYFS from  [Newtouch_EMR]..mr_basy where zyh=@zyh and organizeid=@orgId                       ");
            inSqlParameterList.Add(new SqlParameter("@orgId", orgId.Trim()));
            inSqlParameterList.Add(new SqlParameter("@zyh", zyh.Trim()));

            var list = this.FirstOrDefault<PatInformationVO>(sqlStr.ToString(), inSqlParameterList.ToArray());
            return list;
        }
        /// <summary>
        /// 手术浮层
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<MedicalRecordOperationVO> GetAllOpVOList(string ssmc, string orgId)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                return null;
            }
            StringBuilder sqlStr = new StringBuilder();
            IList<SqlParameter> inSqlParameterList = new List<SqlParameter>();
            sqlStr.Append(@"
 select  top 100 ssdm,ssmc from [Newtouch_OR].[dbo].[OR_Operation] a with(nolock) where  organizeid=@orgId and zt='1'
                        ");
            if (ssmc != null && ssmc != "")
            {
                sqlStr.Append(@" and( ssmc like @ssmc or ssdm like @ssmc or zjm like @ssmc) ");
                ssmc = "%" + ssmc + "%";
                inSqlParameterList.Add(new SqlParameter("@ssmc", ssmc.Trim()));
            }
            inSqlParameterList.Add(new SqlParameter("@orgId", orgId.Trim()));

            var list = this.FindList<MedicalRecordOperationVO>(sqlStr.ToString(), inSqlParameterList.ToArray()).ToList();
            return list;
        }
        public List<MedicalRecordPatVO> GetPatMedicalRecordList(string zyh, string kssj, string jssj, string orgId)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                return null;
            }
            StringBuilder sqlStr = new StringBuilder();
            IList<SqlParameter> inSqlParameterList = new List<SqlParameter>();
            sqlStr.Append(@"select basy.bah,convert(varchar(100),basy.xb) xb,
 convert(varchar(100),basy.nl) nl,
 '0' nlt, isnull(convert(varchar(100),XSECSTZ),'0') cstz,
 isnull(convert(varchar(100),brxx.ks),'') cykb,
 convert(varchar(100),SJZYTS) zyts,
 convert(varchar(100),LYFS) lyfs,
 ZFY zfy,
  STUFF((
        SELECT ', ' + JBDM
        FROM [Newtouch_EMR]..mr_basy_zd where BAH=basy.bah and OrganizeId=basy.OrganizeId and zt='1'
        FOR XML PATH(''), TYPE
    ).value('.', 'nvarchar(max)'), 1, 2, '') AS zdlist,
  STUFF((
        SELECT ', ' + SSJCZBM
        FROM [Newtouch_EMR]..mr_basy_ss where BAH=basy.bah and OrganizeId=basy.OrganizeId and zt='1'
        FOR XML PATH(''), TYPE
    ).value('.', 'nvarchar(max)'), 1, 2, '') AS sslist,
	brxz.brxzmc
  from [Newtouch_EMR]..mr_basy basy
  left join [NewtouchHIS_Sett]..zy_brjbxx brxx
  on basy.ZYH=brxx.zyh and basy.OrganizeId=brxx.OrganizeId and brxx.zt='1'
  left join [NewtouchHIS_Sett]..xt_brxz brxz on brxz.brxz=brxx.brxz and brxz.OrganizeId=brxx.OrganizeId and brxz.zt='1'
  where  basy.zt='1'
   and basy.rysj>=@kssj
and basy.rysj<=@jssj
                        ");
            if (zyh != null && zyh != "")
            {
                sqlStr.Append(@"  and basy.zyh=@zyh ");
                inSqlParameterList.Add(new SqlParameter("@zyh", zyh.Trim()));
            }
            inSqlParameterList.Add(new SqlParameter("@orgId", orgId.Trim()));

            inSqlParameterList.Add(new SqlParameter("@kssj", kssj));
            inSqlParameterList.Add(new SqlParameter("@jssj", jssj));
            var list = this.FindList<MedicalRecordPatVO>(sqlStr.ToString(), inSqlParameterList.ToArray()).ToList();
            return list;
        }
    }
}
