using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Common;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPatientNatureRepo : RepositoryBase<SysPatientNatureEntity>, ISysPatientNatureRepo
    {
        public SysPatientNatureRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<SysPatientNatureEntity> GetList(string orgId)
        {
            return this.IQueryable().Where(a => a.OrganizeId == orgId).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="ogrId"></param>
        /// <returns></returns>
        public List<SysPatientNatureEntity> GetbxzcBySearch(string keyword, string ogrId)
        {
            if (string.IsNullOrWhiteSpace(ogrId))
            {
                return null;
            }
            var linq = ExtLinq.True<SysPatientNatureEntity>();
            return this.IQueryable(linq).Where(P => P.zt == "1" && P.OrganizeId == ogrId
            && (string.IsNullOrEmpty(keyword) || P.brxzmc.Contains(keyword) || P.py.Contains(keyword) || (P.brxz.Contains(keyword)))).ToList(); //modify by caishanshan 20161217 加了个brxz的条件
        }

        /// <summary>
        /// 获取报销政策下拉框（自动提示）
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public string GetbxzcSelect(string keyword, string ogrId)
        {
            var data = this.GetbxzcBySearch(keyword, ogrId);
            var treeList = new List<TreeSelectModel>();
            foreach (SysPatientNatureEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.brxzbh.ToString();
                treeModel.text = item.brxzmc;
                treeModel.parentId = "0";
                treeList.Add(treeModel);
            }
            return treeList.TreeSelectJson();
        }

        /// <summary>
        /// 获取病人性质有效列表
        /// </summary>
        /// <returns></returns>
        public string getEffectPatiNatureList(string orgId)
        {
            var data = this.IQueryable().Where(a => a.zt == "1" && a.OrganizeId == orgId).ToList();

            var treeList = new List<TreeSelectModel>();
            foreach (SysPatientNatureEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.text = item.brxzmc;
                treeModel.parentId = "0";
                treeList.Add(treeModel);
            }
            return treeList.TreeSelectJson();

        }

        /// <summary>
        /// 根据brxz编号获取实体
        /// </summary>
        /// <param name="brxzbh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public SysPatientNatureEntity SelectBrxzByBrxzbh(int brxzbh, string orgId)
        {
            return IQueryable().FirstOrDefault(a => a.brxzbh == brxzbh & a.zt == "1" && a.OrganizeId == orgId);
        }

        /// <summary>
        /// 根据brxz获取实体
        /// </summary>
        /// <param name="brxz">病人性质</param>
        /// <param name="orgId">组织机构</param>
        /// <returns></returns>
        public SysPatientNatureEntity SelectBrxzByBrxz(string brxz, string orgId)
        {
            return IQueryable().FirstOrDefault(a => a.brxz == brxz & a.zt == "1" && a.OrganizeId == orgId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysPatientNatureEntity GetForm(string keyValue)
        {
            return this.FindEntity(int.Parse(keyValue));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysPatientNatureEntity entity, string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                var brxzbh = int.Parse(keyValue);
                //Code重复判断
                if (this.IQueryable().Any(p => p.brxz == entity.brxz && p.brxzbh != brxzbh && p.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("编码不可重复");
                }
                entity.Modify(brxzbh);
                this.Update(entity);
            }
            else
            {
                //Code重复判断
                if (this.IQueryable().Any(p => p.brxz == entity.brxz && p.OrganizeId == entity.OrganizeId))
                {
                    throw new FailedException("编码不可重复");
                }
                entity.Create(true, EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("xt_brxz"));
                this.Insert(entity);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brxzbh"></param>
        /// <param name="orgId"></param>
        public void DeleteForm(int brxzbh, string orgId)
        {
            this.Delete(a => a.brxzbh == brxzbh && a.OrganizeId == orgId);
        }
    }
}
