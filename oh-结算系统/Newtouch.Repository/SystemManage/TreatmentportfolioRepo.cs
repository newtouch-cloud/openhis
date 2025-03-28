using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository.SystemManage;
using Newtouch.HIS.Domain.ValueObjects.TherapeutistCompleteManage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.SystemManage
{
    public class TreatmentportfolioRepo : RepositoryBase<TreatmentportfolioEntity>, ITreatmentportfolioRepo
    {
        public TreatmentportfolioRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public void ADDInsert(TreatmentportfolioEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.Update(entity);
            }
            else
            {
                entity.Create(true);
                this.Insert(entity);
            }

        }
        /// <summary>
        /// 保存收费项目组合
        /// </summary>
        /// <param name="entity"></param>
        public void ADDrowdata(TreatmentportfolioEntity entity)
        {
            entity.Create(true);
            this.Insert(entity);
        }
        /// <summary>
        /// 删除收费项目组合
        /// </summary>
        /// <param name="keyValue"></param>
        public void deleteid(string keyValue, string orgId)
        {
            this.Delete(p => p.zhcode == keyValue && p.OrganizeId == orgId && p.zt == "1");
        }

        /// <summary>
        /// 删除收费组合中明细
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="zhcodemc"></param>
        public void deletemc(string keyValue, string zhcodemc, string orgId)
        {
            this.Delete(p => p.zlxmmc == keyValue && p.zhcode == zhcodemc && p.OrganizeId == orgId && p.zt == "1");
        }

        /// <summary>
        /// 是否已存在收费项目组合
        /// </summary>
        /// <param name="zhcode"></param>
        /// <param name="sfxmmc"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<TreatmentportfolioEntity> TJchaxun(string zhcode, string sfxmmc, string orgId)
        {
            var sql = "select * from mz_gh_zlxmzh where zhcode=@zhcode  and zlxmmc=@sfxmmc  and  OrganizeId=@orgId and zt='1'";
            SqlParameter[] par = {
                new SqlParameter("@zhcode",zhcode),
                new SqlParameter("@sfxmmc",sfxmmc),
                 new SqlParameter("@orgId",orgId)
                };
            return this.FindList<TreatmentportfolioEntity>(sql, par);
        }

        /// <summary>
        /// 获取修改收费项目组合
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zhcode"></param>
        /// <returns></returns>
        public IList<TreatmentportfolioEntity> Codecx(string orgId, string zhcode)
        {
            var sql = "select * from mz_gh_zlxmzh where zhcode=@zhcode  and  OrganizeId=@orgId and zt='1' ";
            SqlParameter[] par = {
                new SqlParameter("@zhcode",zhcode),
                new SqlParameter("@orgId",orgId)

                };
            return this.FindList<TreatmentportfolioEntity>(sql, par);
        }
        /// <summary>
        /// 查询收费项目组合
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<TreamentviewVO> Keyword(Pagination pagination, string keyword, string orgId)
        {
            IList<SqlParameter> inSqlParameterList = new List<SqlParameter>();

            string sql = @"select a.*,  b.dlmc,a.zhmc+'-'+a.zhcode as zhmccode from [NewtouchHIS_Sett].[dbo].[mz_gh_zlxmzh] a,[NewtouchHIS_Base].[dbo].[xt_sfdl] b
                where a.sfdl = b.dlCode and a.OrganizeId=b.OrganizeId and a.zt='1' and a.OrganizeId=@orgId ";

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql = sql + " and (a.zhmc like  @keyword or a.zhcode like @keyword )";
                inSqlParameterList.Add(new SqlParameter("@keyword", '%' + keyword + '%'));
            }

            inSqlParameterList.Add(new SqlParameter("@orgId", orgId));
            return this.QueryWithPage<TreamentviewVO>(sql, pagination, inSqlParameterList.ToArray());
        }



    }
}
