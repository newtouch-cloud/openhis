using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using System;
using System.Collections.Generic;

namespace Newtouch.Repository.Outpatient
{
    /// <summary>
    /// 输液执行信息
    /// </summary>
    public class MzsyzxxxRepo : RepositoryBase<MzsyzxxxEntity>, IMzsyzxxxRepo
    {
        public MzsyzxxxRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 提交执行
        /// </summary>
        /// <param name="syIds"></param>
        /// <param name="zt"></param>
        /// <param name="dispenser"></param>
        /// <param name="executor"></param>
        /// <param name="remark"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public string Exec(List<long> syIds,string seatNum, string zt, string dispenser, string executor, DateTime? sykssj, DateTime? syjssj, string remark, string organizeId)
        {
            if (syIds == null || syIds.Count <= 0) return "输液信息主键不能为空";
            var successCount = 0;
            syIds.ForEach(p =>
            {
                //var entity = FindEntity(n => n.syypxxId == p);
                //if (entity != null)
                //{
                //    entity.zt = zt;
                //    entity.dispenser = dispenser;
                //    entity.executor = executor;
                //    entity.remark = remark;
                //    entity.Modify();
                //    successCount += Update(entity);
                //}
                //else
                //{
                    var entity = new MzsyzxxxEntity
                    {
                        syypxxId = p,
                        dispenser = dispenser,
                        executor = executor,
                        remark = remark,
                        sykssj=sykssj,
                        syjssj=syjssj,
                        OrganizeId= organizeId,
                        seatNum= seatNum
                    };
                    entity.Create(true);
                    successCount += Insert(entity);
               // }
            });
            return successCount != syIds.Count ? "部分执行失败" : "";
        }

        public string CanCelExec(List<string> syIds, string OrganizeId)
        {
            if (syIds == null || syIds.Count <= 0) return "输液信息主键不能为空";
            var successCount = 0;
            syIds.ForEach(p =>
            {
                var entity = FindEntity(n => n.Id == p &&n.OrganizeId==OrganizeId && n.zt=="1");
                if (entity != null)
                {
                    entity.zt = "0";
                    entity.Modify();
                    successCount += Update(entity);
                }
            });
            return successCount != syIds.Count ? "部分取消执行失败" : "";
        }

    }
}
