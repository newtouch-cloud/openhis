using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using FrameworkBase.MultiOrg.Repository;
using FrameworkBase.MultiOrg.Infrastructure;
using System;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2021-03-01 15:24
    /// 描 述：出院小结
    /// </summary>
    public class YBInpOutHosSummariesRepo : RepositoryBase<YBInpOutHosSummariesEntity>, IYBInpOutHosSummariesRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public YBInpOutHosSummariesRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }
        public void Submit(YBInpOutHosSummariesEntity ety)
        {
            if (ety != null)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(ety.BKC192))
                    {
                        ety.BKC192 = ety.BKC192 == "00010101000000" ? null : Convert.ToDateTime(ety.BKC192).ToString("yyyyMMddHHmmss");
                    }
                    if (!string.IsNullOrWhiteSpace(ety.AKC194))
                    {
                        ety.AKC194 = ety.AKC194 == "00010101000000" ? null : Convert.ToDateTime(ety.AKC194).ToString("yyyyMMddHHmmss");
                    }
                    var ext = this.FindEntity(p => p.Id == ety.Id && p.OrganizeId == ety.OrganizeId && p.zt == "1");
                    if (ext != null && !string.IsNullOrWhiteSpace(ety.Id))
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