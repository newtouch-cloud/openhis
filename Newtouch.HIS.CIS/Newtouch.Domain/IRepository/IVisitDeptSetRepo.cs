using System.Collections.Generic;
using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// 出诊设置
    /// </summary>
    public interface IVisitDeptSetRepo : IRepositoryBase<VisitDeptSetEntity>
    {

        /// <summary>
        /// 获取出诊信息
        /// </summary>
        /// <param name="ysgh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VisitDeptSetEntity> SelectData(string ysgh, string organizeId);

        /// <summary>
        /// 根据ID物理删除记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteItem(string id);
    }
}