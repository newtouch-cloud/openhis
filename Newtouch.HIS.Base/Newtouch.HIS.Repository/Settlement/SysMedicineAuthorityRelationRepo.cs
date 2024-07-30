

using Newtouch.HIS.Domain.Entity.Settlement;
using Newtouch.HIS.Domain.IRepository.Settlement;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.Repository.Settlement
{
    public class SysMedicineAuthorityRelationRepo : RepositoryBase<SysMedicineAuthorityRelationEntity>, ISysMedicineAuthorityRelationRepo
    {
        public SysMedicineAuthorityRelationRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysMedicineAuthorityRelationEntity GetForm(string keyValue)
        {
            return this.FindEntity(keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IList<SysMedicineAuthorityRelationEntity> GetList(string orgId, string keyword = null)
        {
            var sql = @"select * from xt_qxkz_rel(nolock) where OrganizeId = @orgId
and (qxId like @searchKeyword or gh like @searchKeyword)
order by CreateTime desc";

            return this.FindList<SysMedicineAuthorityRelationEntity>(sql, new[] {
                new SqlParameter("@orgId", orgId)
                ,new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%") });
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysMedicineAuthorityRelationEntity entity, string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                entity.Modify();
                entity.id = keyValue;
                this.Update(entity);
            }
            else
            {
                entity.Create(true);
                this.Insert(entity);
            }
        }



        public void UpdateAuthority(string gh, string organizeId, string[] AuthorityList)
        {
            if (string.IsNullOrWhiteSpace(gh) || AuthorityList == null || AuthorityList.Count() == 0)
            {
                return;
            }
            //权限list
            var authorityLists = new List<SysMedicineAuthorityRelationEntity>();
            foreach (var item in AuthorityList.Where(p => !string.IsNullOrWhiteSpace(p)).Distinct())
            {
                var entity = new SysMedicineAuthorityRelationEntity();
                entity.Create(true);
                entity.qxId = item;
                entity.gh = gh;
                entity.OrganizeId = organizeId;
                entity.zt = "1";
                authorityLists.Add(entity);
            }

            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var oldAuthorityList = db.IQueryable<SysMedicineAuthorityRelationEntity>().Where(p => p.gh == gh).ToList();
                ////var oldAuthorityList = GetListBygh(gh, organizeId);
                for (int i = 0; i < authorityLists.Count; i++)
                {
                    if (oldAuthorityList.Any(p => p.qxId == authorityLists[i].qxId))
                    {
                        oldAuthorityList.Remove(oldAuthorityList.Where(p => p.qxId == authorityLists[i].qxId).First());
                        continue;
                    }
                    db.Insert(authorityLists[i]);
                }
                foreach (var item in oldAuthorityList)
                {
                    db.Delete(item);
                }
                db.Commit();
            }

        }


    }
}
