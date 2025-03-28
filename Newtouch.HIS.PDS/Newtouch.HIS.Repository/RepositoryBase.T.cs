using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 仓储实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class RepositoryBase<TEntity> : Infrastructure.EF.RepositoryBase<TEntity> where TEntity : class, new()
    {

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="databaseFactory"></param>
        public RepositoryBase(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        { }

        /// <summary>
        /// 更新部分字段
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dstnFieldNameList">目标列</param>
        /// <param name="ignoreFieldNameList">排除列</param>
        /// <param name="attach">是否需要将Entity附加上上下文（如果这个Entity是从dbcontext查出来的，false）</param>
        /// <returns></returns>
        public override int Update(TEntity entity, IEnumerable<string> dstnFieldNameList = null, IEnumerable<string> ignoreFieldNameList = null, bool attach = true)
        {
            if (attach)
            {
                //唯一一种发生异常的情况：尝试附加相同主键的不同实体（注意这是引用类型，即同一实体重复Attach是不会有问题的）
                _dataContext.Set<TEntity>().Attach(entity);
            }
            var props = entity.GetType().GetProperties();
            foreach (var prop in props)
            {
                if (prop.Name == "CreatorCode" || prop.Name == "CreateTime" || prop.Name == "CreatorName")
                {
                    //_dataContext.Entry(entity).Property(prop.Name).IsModified = false;
                    continue;
                }
                if (prop.Name != "LastModifyTime" && prop.Name != "LastModifierCode")
                {
                    if (dstnFieldNameList != null && dstnFieldNameList.Count() > 0)
                    {
                        if (!dstnFieldNameList.Contains(prop.Name))
                        {
                            //_dataContext.Entry(entity).Property(prop.Name).IsModified = false;
                            continue;
                        }
                    }
                    else if (ignoreFieldNameList != null && ignoreFieldNameList.Count() > 0)
                    {
                        if (ignoreFieldNameList.Contains(prop.Name))
                        {
                            //_dataContext.Entry(entity).Property(prop.Name).IsModified = false;
                            continue;
                        }
                    }
                }
                _dataContext.Entry(entity).Property(prop.Name).IsModified = true;
            }
            return _dataContext.SaveChanges();
        }

    }
}
