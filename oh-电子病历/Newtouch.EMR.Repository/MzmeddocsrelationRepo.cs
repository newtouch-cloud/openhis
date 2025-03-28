using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using System.Collections.Generic;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common.Exceptions;
using System;
using System.Linq;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2018-09-11 10:46
    /// 描 述：患者病历文书关系表
    /// </summary>
    public class MzmeddocsrelationRepo : RepositoryBase<MzmeddocsrelationEntity>, IMzmeddocsrelationRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public MzmeddocsrelationRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        public List<MzmeddocsrelationEntity> GetTreeList(string orgId, string mzh)
        {
            return this.IQueryable().Where(p => p.OrganizeId == orgId && p.zt == "1"&&p.mzh== mzh).OrderBy(p => p.CreateTime).ToList();
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(MzmeddocsrelationEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                dbEntity.LastModifierCode = entity.LastModifierCode;
                dbEntity.LastModifyTime = DateTime.Now;
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
            }
            else
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.CreateTime = DateTime.Now;
                entity.zt = "1";
                entity.Create(true);
                this.Insert(entity);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }

        /// <summary>
        /// 更新患者病历关系
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="zyh"></param>
        /// <param name="BlId"></param>
        /// <param name="BllxId"></param>
        /// <param name="OrgId"></param>
        public void SubmitEntity(MzmeddocsrelationEntity entity)
        {
            //查找唯一病历
            var ety = FindEntity(p => p.blId == entity.blId && p.OrganizeId == entity.OrganizeId);

            if (ety != null)
            {
                //校验病历信息
                if (ety.mzh == entity.mzh && ety.mbId == entity.mbId)
                {
                    ety.blzt = entity.blzt;
                    ety.blrq = entity.blrq;
                    ety.blmc = entity.blmc;
                    ety.blbt = entity.blbt;
                    ety.Memo = entity.Memo;
                    ety.ysgh = entity.ysgh;
                    ety.ysxm = entity.ysxm;
                    ety.zt = entity.zt;

                    var dbEntity = this.FindEntity(ety.Id);
                    dbEntity.Modify(ety.Id);
                    this.Update(dbEntity);
                }
                else
                {
                    throw new FailedException("病历信息异常");
                }
            }
            else
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.CreateTime = DateTime.Now;
                entity.zt = "1";
                entity.Create(true);
                this.Insert(entity);
            }
        }

    }
}