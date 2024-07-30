using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using FrameworkBase.MultiOrg.Infrastructure;
using System;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2018-08-30 11:31
    /// 描 述：住院患者信息
    /// </summary>
    public class ZybrjbxxRepo : RepositoryBase<ZybrjbxxEntity>, IZybrjbxxRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public ZybrjbxxRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }
        public ZybrjbxxEntity GetZybrjbxx(string zyh, string OrganizeId)
        {

            return this.FindEntity(p => p.zyh==zyh&&p.OrganizeId== OrganizeId);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(ZybrjbxxEntity entity, string keyValue)
        {
            ZybrjbxxEntity dbEntity = null;
            if (entity != null && string.IsNullOrEmpty(keyValue))
            {
                dbEntity = this.FindEntity(p => p.zyh == entity.zyh && p.OrganizeId == entity.OrganizeId);
            }
            else if (entity != null && !string.IsNullOrEmpty(keyValue))
            {
                dbEntity = this.FindEntity(keyValue);
            }

            if (dbEntity != null)
            {
                //properties
                dbEntity.OrganizeId = entity.OrganizeId;
                dbEntity.zyh = entity.zyh;
                dbEntity.blh = entity.blh;
                dbEntity.xm = entity.xm;
                dbEntity.py = entity.py;
                dbEntity.wb = entity.wb;
                dbEntity.sfzh = entity.sfzh;
                dbEntity.sex = entity.sex;
                dbEntity.birth = entity.birth;
                dbEntity.zybz = entity.zybz;
                dbEntity.sfqj = entity.sfqj;
                dbEntity.DeptCode = entity.DeptCode;
                dbEntity.WardCode = entity.WardCode;
                dbEntity.ysgh = entity.ysgh;
                dbEntity.BedCode = entity.BedCode;
                dbEntity.ryrq = entity.ryrq;
                dbEntity.rqrq = entity.rqrq;
                dbEntity.cqrq = entity.cqrq;
                dbEntity.wzjb = entity.wzjb;
                dbEntity.hljb = entity.hljb;
                dbEntity.ryfs = entity.ryfs;
                dbEntity.cyfs = entity.cyfs;
                dbEntity.gdxmzxrq = entity.gdxmzxrq;
                dbEntity.brxzdm = entity.brxzdm;
                dbEntity.brxzmc = entity.brxzmc;
                dbEntity.cardno = entity.cardno;
                dbEntity.cardtype = entity.cardtype;
                dbEntity.lxr = entity.lxr;
                dbEntity.lxrgx = entity.lxrgx;
                dbEntity.lxrdh = entity.lxrdh;
                dbEntity.zddm = entity.zddm;
                dbEntity.zdmc = entity.zdmc;
                dbEntity.cyzddm = entity.cyzddm;
                dbEntity.cyzdmc = entity.cyzdmc;
                //dbEntity.Memo = entity.Memo;
                //dbEntity.CreateTime = entity.CreateTime;
                //dbEntity.CreatorCode = entity.CreatorCode;
                //dbEntity.LastModifyTime = entity.LastModifyTime;
                //dbEntity.LastModifierCode = entity.LastModifierCode;
                //dbEntity.zt = entity.zt;
                
                dbEntity.Modify(dbEntity.Id);
                this.Update(dbEntity);
            }
            else
            {
                entity.Create(true);
                this.Insert(entity);
            }
        }

        /// <summary>
        /// 提交病案
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="OrgId"></param>
        /// <param name="user"></param>
        public void CommitRecord(string zyh,string OrgId,string user)
        {
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                var ety = this.FindEntity(p => p.zyh == zyh && p.OrganizeId == OrgId && p.zt == "1");
                if (ety != null)
                {
                    ety.RecordStu = ((int)EnumRecordStu.ytj).ToString();
                    ety.Commitor = user;
                    ety.CommitTime = DateTime.Now;
                    this.Update(ety);
                }
            }
            else
                throw new FailedException("患者信息不可为空");
        }

        /// <summary>
        /// 退回病案
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="OrgId"></param>
        /// <param name="user"></param>
        public void BackRecord(string zyh, string OrgId, string user)
        {
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                var ety = this.FindEntity(p => p.zyh == zyh && p.OrganizeId == OrgId && p.zt == "1");
                if (ety != null)
                {
                    ety.RecordStu = ((int)EnumRecordStu.th).ToString();
                    ety.Commitor = user;
                    ety.CommitTime = DateTime.Now;
                    this.Update(ety);
                }
            }
            else
                throw new FailedException("患者信息不可为空");
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }

    }
}