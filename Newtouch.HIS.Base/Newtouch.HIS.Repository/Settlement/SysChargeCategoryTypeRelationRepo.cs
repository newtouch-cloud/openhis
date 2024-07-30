using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.Settlement;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysChargeCategoryTypeRelationRepo : RepositoryBase<SysChargeCategoryTypeRelationEntity>, ISysChargeCategoryTypeRelationRepo
    {
        public SysChargeCategoryTypeRelationRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IList<GetSfDlMc> GetList(string orgId, string keyword = null)
        {
            var sql = @"select lx.id,lx.Type,lx.dlCode,lx.CreatorCode,lx.CreateTime,lx.LastModifierCode,lx.LastModifyTime,dl.dlmc,lx.OrganizeId,lx.zt,lx.px
from  xt_sfdl_lx lx
left join xt_sfdl dl on lx.dlCode=dl.dlCode 
and dl.zt='1'
and lx.OrganizeId=dl.OrganizeId
where lx.OrganizeId = @orgId
and (lx.dlCode like @searchKeyword or Type like @searchKeyword)
order by CreateTime desc";

            return this.FindList<GetSfDlMc>(sql, new[] {
                new SqlParameter("@orgId", orgId)
                ,new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%") });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysChargeCategoryTypeRelationEntity entity, string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                entity.Modify();
                entity.Id = keyValue;
                this.Update(entity);
            }
            else
            {
                entity.Create(true);
                this.Insert(entity);
            }
        }


    }
}
