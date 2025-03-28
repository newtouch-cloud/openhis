using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.EMR.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using System;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2021-03-01 15:24
    /// 描 述：病程记录
    /// </summary>
    public class YBInpCourseDiseaseRepo : RepositoryBase<YBInpCourseDiseaseEntity>, IYBInpCourseDiseaseRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public YBInpCourseDiseaseRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public void Submit(YBInpCourseDiseaseEntity ety)
        {
            if (ety != null)
            {
                try {
                    ety.BKF737 = ety.BKF737== "00010101000000"? null: Convert.ToDateTime(ety.BKF737).ToString("yyyyMMddHHmmss");
                    var ext = FindEntity(p => p.Id == ety.Id && p.OrganizeId == ety.OrganizeId && p.zt == "1");
                    if (ext != null)
                    {
                        ety.Modify(ety.Id);
                        this.Update(ety);
                    }
                    else
                    {
                        ety.Create(true, ety.Id);
                        this.Insert(ety);
                    }
                }
                catch (Exception ex)
                {
                    throw new FailedException("保存失败，" + ex.Message);
                }
            }
        }

    }
}