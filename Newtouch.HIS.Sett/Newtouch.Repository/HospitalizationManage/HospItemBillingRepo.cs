using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class HospItemBillingRepo : RepositoryBase<HospItemBillingEntity>, IHospItemBillingRepo
    {
        public HospItemBillingRepo(IDefaultDatabaseFactory databaseFactory, IHospSettlementRepo SettOfTheHosRepository)
            : base(databaseFactory)
        {
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="hospItemFeeEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(HospItemBillingEntity hospItemFeeEntity, int? keyValue)
        {
            if (keyValue > 0)
            {
                var entity = this.FindEntity(hospItemFeeEntity.jfbbh);
                entity.Modify(keyValue);
                this.Update(entity);
                hospItemFeeEntity.Create(true, EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("zy_xmjfb"));
                this.Insert(hospItemFeeEntity);
            }
            else
            {
                hospItemFeeEntity.Create(true, EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("zy_xmjfb"));
                this.Insert(hospItemFeeEntity);
            }
        }

        /// <summary>
        /// 查询 时间段内的 项目计费EntityList
        /// 已考虑退费
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="sourceQuery"></param>
        /// <returns></returns>
        public IList<HospItemBillingEntity> GetItemFeeEntityListByTime(string zyh, string orgId, DateTime startTime, DateTime endTime
            , IQueryable<HospItemBillingEntity> sourceQuery = null)
        {
            if (sourceQuery == null)
            {
                sourceQuery = this.IQueryable();
            }
            //没有加入结束日期的过滤 是为了 退费
            var query1 = sourceQuery.Where(p => p.zyh == zyh
              && p.OrganizeId == orgId && p.zt == "1"
              && p.CreateTime > startTime).ToList();
            //
            var query2 = query1.GroupBy(p => p.cxzyjfbbh != 0 ? p.cxzyjfbbh : p.jfbbh)
                .Select(p => new HospItemBillingEntity()
                {
                    jfbbh = p.Key,
                    sl = p.Sum(i => i.sl)
                }).ToList();
            if (query2.Count == query1.Count)
            {
                //没有退过费
                return query1.Where(p => p.CreateTime <= endTime).ToList();
            }
            query1 = query1.Where(p => p.CreateTime <= endTime).ToList();
            //
            var query = from q2 in query2
                        join q1 in query1
                        on q2.jfbbh equals q1.jfbbh
                        //一定是q2joinq1，然后过滤q1的时间
                        //退完不显示
                        where q1.CreateTime <= endTime && q2.sl > 0
                        select new HospItemBillingEntity
                        {
                            jfbbh = q1.jfbbh,
                            OrganizeId = q1.OrganizeId,
                            zyh = q1.zyh,
                            tdrq = q1.tdrq,
                            sfxm = q1.sfxm,
                            dl = q1.dl,
                            ys = q1.ys,
                            ysmc = q1.ysmc,
                            ks = q1.ks,
                            ksmc = q1.ksmc,
                            cw = q1.cw,
                            dj = q1.dj,
                            sl = q2.sl, //q2.sl
                            jfdw = q1.jfdw,
                            zfbl = q1.zfbl,
                            zfxz = q1.zfxz,
                            ssbz = q1.ssbz,
                            ssry = q1.ssry,
                            ssrq = q1.ssrq,
                            yzxz = q1.yzxz,
                            yzzt = q1.yzzt,
                            CreatorCode = q1.CreatorCode,
                            CreateTime = q1.CreateTime,
                            LastModifyTime = q1.LastModifyTime,
                            LastModifierCode = q1.LastModifierCode,
                            px = q1.px,
                            bq = q1.bq,
                            zt = q1.zt,
                            jzjhmxId = q1.jzjhmxId,
                            kflb = q1.kflb,
                            ttbz = q1.ttbz,
                            duration = q1.duration,
                            zzll = q1.zzll,
                        };

            return query.ToList();
        }

    }
}


