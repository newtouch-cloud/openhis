using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Outpatient;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.DomainServices.Outpatient
{
    public class AllergyManageDmnService : DmnServiceBase, IAllergyManageDmnService
    {
        private readonly IAllergyRepo _allergyRepo;
        public AllergyManageDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        { }

        /// <summary>
        /// 保存患者过敏信息
        /// </summary>
        /// <param name="entity"></param>
        public void SavePatAllergyInfo(AllergyEntity entity)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                
                db.Insert(entity);
                db.Commit();
            }
        }


        #region old

        /// <summary>
        /// 门诊病人筛选
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="ks"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="orgId"></param>
        /// <param name="PsItemCode"></param>
        /// <param name="Status">执行状态 1已执行 0未执行</param>
        /// <returns></returns>
        public IList<OutHosPatientVO> GetOutPatientPaginationList(Pagination pagination, string keyword, string ks, string kssj, string jssj, string orgId, string PsItemCode, string Status)
        {
            string PsItemCodeWhere = GetPsItemsSetting(PsItemCode);
            var sql = @"select distinct a.jzId,a.OrganizeId,a.py,a.mzh,a.mjzbz,a.blh,a.xm,a.xb,a.brxzmc,a.zjlx,a.zjh,a.ghksmc,a.ghys,a.ghsj,a.CreateTime,a.CreatorCode,a.LastModifyTime,a.LastModifierCode,a.csny
from [Newtouch_CIS].[dbo].[xt_jz] a
INNER JOIN [Newtouch_CIS].[dbo].[xt_cf] b on a.jzId = b.jzId and a.OrganizeId = b.OrganizeId and b.zt = '1'
INNER JOIN [Newtouch_CIS].[dbo].[xt_cfmx] c on b.cfId = c.cfId and b.OrganizeId = c.OrganizeId and c.zt = '1'
 where a.zt='1' and a.OrganizeId=@orgId
 and a.jzzt=@jzzt";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            pars.Add(new SqlParameter("@jzzt", "2"));
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (a.blh like @keyValue or a.xm like @keyValue or a.py like @keyValue)";
                pars.Add(new SqlParameter("@keyValue", "%" + keyword + "%"));
            }
            if (!string.IsNullOrWhiteSpace(ks))
            {
                sql += " and a.ghksmc = @ks";
                pars.Add(new SqlParameter("@ks", ks));
            }
            if (!string.IsNullOrWhiteSpace(kssj))
            {
                sql += " and a.CreateTime > @kssj";
                pars.Add(new SqlParameter("@kssj", kssj));
            }
            if (!string.IsNullOrWhiteSpace(jssj))
            {
                sql += " and a.CreateTime <= @jssj";
                pars.Add(new SqlParameter("@jssj", jssj));
            }
            if (!string.IsNullOrWhiteSpace(PsItemCodeWhere))
            {
                sql += string.Format(" and c.xmCode IN ({0})", PsItemCodeWhere);
            }
            if ("1".Equals(Status))
            {
                sql += "and a.blh in(select d.blh from [Newtouch_CIS].[dbo].[xt_gmxx] d where d.OrganizeId=a.OrganizeId and d.cfmxId=c.cfmxId and a.blh = d.blh and d.zt = '1' and d.mzzybz='1' and ISNULL(d.Result,'') != '')";
            }
            else if ("0".Equals(Status))
            {
                sql += "and a.blh in(select d.blh from [Newtouch_CIS].[dbo].[xt_gmxx] d where d.OrganizeId=a.OrganizeId and d.cfmxId=c.cfmxId and a.blh = d.blh and d.zt = '1' and d.mzzybz='1' and ISNULL(d.Result,'') = '')";
            }

            return this.QueryWithPage<OutHosPatientVO>(sql, pagination, pars.ToArray());
        }

        /// <summary>
        /// 住院病人筛选
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="ks"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="orgId"></param>
        /// <param name="PsItemCode"></param>
        /// <param name="Status">执行状态 1已执行 0未执行</param>
        /// <returns></returns>
        public IList<InHosPatientVO> GetInPatientPaginationList(Pagination pagination, string keyword, string ks, string kssj, string jssj, string orgId, string PsItemCode, string Status)
        {
            string PsItemCodeWhere = GetPsItemsSetting(PsItemCode);
            var sql = @"SELECT a.[id]
      ,a.[OrganizeId]
      ,a.[zyh]
      ,a.[blh]
      ,a.[xm]
      ,a.[py]
      ,a.[sex]
      ,a.[birth]
      ,a.[BedCode]
      ,a.[ryrq]
      ,a.[rqrq]
      ,a.[cqrq]
      ,a.[cardno]
      ,b.[CreateTime]
      ,b.[CreatorCode]
  FROM [Newtouch_CIS].[dbo].[zy_brxxk] a 
 INNER JOIN [Newtouch_CIS].[dbo].[zy_lsyz] b ON a.zyh = b.zyh and a.OrganizeId = b.OrganizeId and b.zt = '1' and b.yzlx=@yzlx
 WHERE a.zt = '1' and a.OrganizeId = @orgId";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            pars.Add(new SqlParameter("@yzlx", (int)EnumYzlx.sfxm));
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (a.zyh like @keyValue or a.xm like @keyValue or a.py like @keyValue)";
                pars.Add(new SqlParameter("@keyValue", "%" + keyword + "%"));
            }
            if (!string.IsNullOrWhiteSpace(ks))
            {
                sql += " and a.DeptCode = @DeptCode";
                pars.Add(new SqlParameter("@DeptCode", ks));
            }
            if (!string.IsNullOrWhiteSpace(kssj))
            {
                sql += " and b.CreateTime > @kssj";
                pars.Add(new SqlParameter("@kssj", kssj));
            }
            if (!string.IsNullOrWhiteSpace(jssj))
            {
                sql += " and b.CreateTime <= @jssj";
                pars.Add(new SqlParameter("@jssj", jssj));
            }
            if (!string.IsNullOrWhiteSpace(PsItemCodeWhere))
            {
                sql += string.Format(" and b.xmdm IN ({0})", PsItemCodeWhere);
            }
            if ("1".Equals(Status))
            {
                sql += "and a.blh in(select d.blh from [Newtouch_CIS].[dbo].[xt_gmxx] d where d.OrganizeId=a.OrganizeId and b.Id=d.yzId and d.zt = '1' and d.mzzybz='2' and ISNULL(d.Result,'') != '')";
            }
            else if ("0".Equals(Status))
            {
                sql += "and a.blh in(select d.blh from [Newtouch_CIS].[dbo].[xt_gmxx] d where d.OrganizeId=a.OrganizeId and b.Id=d.yzId and d.zt = '1' and d.mzzybz='2' and ISNULL(d.Result,'') = '')";
            }

            return this.QueryWithPage<InHosPatientVO>(sql, pagination, pars.ToArray());
        }

        /// <summary>
        /// 获取门诊患者开的皮试项目
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="Id"></param>
        /// <param name="orgId"></param>
        /// <param name="mzzybz">1 门诊 2住院</param>
        /// <returns></returns>
        public IList<PsItemVO> GetMzPatientPsItemsList(Pagination pagination, string Id, string orgId, string PsItemCode)
        {
            string PsItemCodeWhere = GetPsItemsSetting(PsItemCode);

            var sql = @"select c.blh,c.Id as gmxxId,b.cfmxId,b.xmCode,b.xmmc,b.sl,b.dj,b.je,b.dw,c.CreateTime,c.CreatorName,c.Result,c.Remark from [Newtouch_CIS].[dbo].[xt_cf] a
 INNER JOIN [Newtouch_CIS].[dbo].[xt_cfmx] b on a.cfId = b.cfId and a.OrganizeId = b.OrganizeId and b.zt = '1' and ISNULL(b.xmCode,'') != ''
 LEFT JOIN [Newtouch_CIS].[dbo].[xt_gmxx] c on b.OrganizeId = c.OrganizeId and b.cfmxId = c.cfmxId and c.zt = '1' and c.mzzybz=@mzzybz
  where a.OrganizeId = @orgId and a.zt = '1' and a.jzId = @jzId";
            
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            pars.Add(new SqlParameter("@jzId", Id));
            pars.Add(new SqlParameter("@mzzybz", "1"));
            if (!string.IsNullOrWhiteSpace(PsItemCodeWhere))
            {
                sql += string.Format(" and b.xmCode IN ({0})", PsItemCodeWhere);
            }
            return this.QueryWithPage<PsItemVO>(sql, pagination, pars.ToArray());
        }

        /// <summary>
        /// 获取住院患者开的皮试项目
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="Id"></param>
        /// <param name="orgId"></param>
        /// <param name="mzzybz">1 门诊 2住院</param>
        /// <returns></returns>
        public IList<PsItemVO> GetZyPatientPsItemsList(Pagination pagination, string zyh, string orgId, string PsItemCode)
        {
            string PsItemCodeWhere = GetPsItemsSetting(PsItemCode);

            var sql = @"SELECT b.Id as gmxxId,a.Id as yzId,a.[zyh]
      ,a.[xmdm] as xmCode
      ,a.[xmmc]
      ,a.[dw]
      ,a.[sl]
      ,a.[ypjl]
      ,a.[ypgg]
      ,a.[yznr]
      ,b.blh
      ,b.CreateTime
      ,b.CreatorName
      ,b.Result
      ,b.Remark
  FROM [Newtouch_CIS].[dbo].[zy_lsyz] a
LEFT JOIN [Newtouch_CIS].[dbo].[xt_gmxx] b on a.OrganizeId = b.OrganizeId and a.Id = b.yzid and b.zt = '1' and b.mzzybz=@mzzybz
where a.OrganizeId = @orgId and a.zt = '1' and a.zyh=@zyh and a.yzlx=@yzlx";
            
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            pars.Add(new SqlParameter("@zyh", zyh));
            pars.Add(new SqlParameter("@mzzybz", "2"));
            pars.Add(new SqlParameter("@yzlx", (int)EnumYzlx.sfxm));
            if (!string.IsNullOrWhiteSpace(PsItemCodeWhere))
            {
                sql += string.Format(" and a.xmdm IN ({0})", PsItemCodeWhere);
            }
            return this.QueryWithPage<PsItemVO>(sql, pagination, pars.ToArray());
        }

        /// <summary>
        /// 过敏信息查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="Id"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<PatientGmxxDetailVO> GetPatientGmxxList(Pagination pagination, string keyword, string orgId)
        {
            var sql = @"select a.*,case when a.mzzybz = '1' then b.csny else f.birth end csrq,c.ypmc as gmyp,h.typeName as gmlxmc from
  [Newtouch_CIS].[dbo].[xt_gmxx] a
 LEFT JOIN [Newtouch_CIS].[dbo].[xt_jz] b on a.blh = b.blh and b.zt = '1' and a.OrganizeId = b.OrganizeId and a.mzzybz = '1'
 LEFT JOIN [Newtouch_CIS].[dbo].[zy_lsyz] d on d.Id = a.yzid and a.OrganizeId = d.OrganizeId and d.zt = '1' and a.mzzybz = '2'
 LEFT JOIN [Newtouch_CIS].[dbo].[zy_brxxk] f on d.zyh = f.zyh and f.OrganizeId = d.OrganizeId and f.zt = '1'
 LEFT JOIN [NewtouchHIS_Base].[dbo].[xt_yp] c on a.OrganizeId = c.OrganizeId and c.ypCode = a.ypCode and c.zt = '1'
 LEFT JOIN [NewtouchHIS_Base].[dbo].[xt_kssType] h on a.OrganizeId = h.OrganizeId and a.gmlx = h.Id and h.zt = '1' 
  where a.OrganizeId=@orgId and a.zt = '1' and a.Result = '阳性'";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " and (a.xm like @keyValue or a.blh like @keyValue)";
                pars.Add(new SqlParameter("@keyValue", "%" + keyword + "%"));
            }
            return this.QueryWithPage<PatientGmxxDetailVO>(sql, pagination, pars.ToArray());
        }

        /// <summary>
        /// 获取抗生素药品信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<KssYpVO> GetKssYpListData(string orgId)
        {
            var Sql = @"SELECT a.[ypId],a.[ypCode],a.[ypmc]
            FROM [NewtouchHIS_Base].[dbo].[xt_yp] a
            INNER JOIN [NewtouchHIS_Base].[dbo].[xt_ypKss] b on a.kssId = b.Id and a.OrganizeId = b.OrganizeId and b.zt = '1'
            where a.zt = '1' and a.OrganizeId = @orgId";
            return this.FindList<KssYpVO>(Sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId ?? "")
            });
        }

        /// <summary>
        /// 获取抗生素药品分类
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<KssYpLbVO> GetKssYpDlData(string ypCode, string orgId)
        {
            var Sql = @"SELECT c.[Id], c.[typeName]
  FROM [NewtouchHIS_Base].[dbo].[xt_yp] a
  INNER JOIN [NewtouchHIS_Base].[dbo].[xt_ypKss] b on a.kssId = b.Id and a.OrganizeId = b.OrganizeId and b.zt = '1'
  INNER JOIN [NewtouchHIS_Base].[dbo].[xt_kssType] c on b.OrganizeId = c.OrganizeId and b.kssLevel1TypeId = c.Id and c.zt = '1' 
  where a.zt = '1' and a.OrganizeId = @orgId and a.ypCode = @ypCode";
            return this.FindList<KssYpLbVO>(Sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@ypCode",ypCode)
            });
        }

        /// <summary>
        /// 保存患者过敏信息
        /// </summary>
        /// <param name="entity"></param>
        public void SaveAllergyInfo(AllergyEntity entity)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                db.Insert(entity);
                db.Commit();
            }
        }

        /// <summary>
        /// 删除执行的患者过敏信息
        /// </summary>
        /// <param name="xmCode">过敏项目code</param>
        /// <param name="blh"></param>
        /// <param name="mzzybz"></param>
        /// <param name="orgId"></param>
        public int DeleteAllergyInfo(string gmxxId, string orgId, string UserCode, string UserName)
        {
            var param = new List<DbParameter>
            {
                new SqlParameter("@Id",gmxxId),
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@zt","0"),
                new SqlParameter("@LastModifierCode",UserCode),
                new SqlParameter("@LastModifierName",UserName),
                new SqlParameter("@LastModifyTime",DateTime.Now)
            };
            return (string.IsNullOrWhiteSpace(gmxxId))
                ? 0
                : ExecuteSqlCommand(@"update [Newtouch_CIS].[dbo].[xt_gmxx] set zt=@zt,LastModifierCode=@LastModifierCode,LastModifierName=@LastModifierName,LastModifyTime=@LastModifyTime
                    where Id=@Id and OrganizeId=@orgId;",
                    param.ToArray());
        }

        /// <summary>
        /// 获取系统配置皮试项目
        /// </summary>
        /// <param name="PsItemCode"></param>
        /// <returns></returns>
        public string GetPsItemsSetting(string PsItemCode)
        {
            string Where = string.Empty;
            if (!string.IsNullOrWhiteSpace(PsItemCode))
            {
                var PsItemList = Tools.Json.ToList<PsItemVO>(PsItemCode);
                foreach (var item in PsItemList)
                {
                    Where += string.Format("'{0}',", item.xmCode);
                }

                Where = Where.Length > 1 ? Where.Substring(0, Where.Length - 1) : "";
            }

            if (string.IsNullOrWhiteSpace(Where)) throw new Exception("请联系管理员配置过敏项目配置有误,参数编码：AllergyItemSetting");
            else {
                return Where;
            } 
        }

        #endregion
    }
}
