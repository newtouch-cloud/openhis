using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysDepartmentBindingRepo : RepositoryBase<SysDepartmentBindingEntity>, ISysDepartmentBindingRepo
    {
        public SysDepartmentBindingRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IList<SysDepartmentBindingEntity> GetSysDepartmentBindingList(string keyValue, string OrganizeId)
        {
            IList<SysDepartmentBindingEntity> list = null;
            if (!string.IsNullOrEmpty(keyValue))
            {
                list = this.IQueryable().Where(a => a.xm.Contains(keyValue) && a.OrganizeId == OrganizeId).ToList();
            }
            else
            {
                list = this.IQueryable().Where(a => a.OrganizeId == OrganizeId).ToList();
            }
            return list;
        }

        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="bddm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public SysDepartmentBindingEntity GetSysDepartmentBindingEntity(string bddm, string orgId)
        {
            var entity = this.IQueryable().Where(a => a.bddm == bddm && a.OrganizeId == orgId).FirstOrDefault();
            return entity;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sysDepartmentBindinEntity"></param>
        /// <param name="bddm"></param>
        public void SubmitForm(SysDepartmentBindingEntity sysDepartmentBindingEntity, string bddm)
        {
            if (!string.IsNullOrEmpty(bddm))
            {
                sysDepartmentBindingEntity.Modify(bddm);
                this.Update(sysDepartmentBindingEntity);
            }
            else
            {
                sysDepartmentBindingEntity.Create();
                sysDepartmentBindingEntity.bddm = Guid.NewGuid().ToString();
                this.Insert(sysDepartmentBindingEntity);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="bddm"></param>
        /// <param name="orgId"></param>
        public void DeleteForm(string bddm, string orgId)
        {
            this.Delete(a => a.bddm == bddm && a.OrganizeId == orgId);
        }
    }
}


