using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGzybCodeContrastRepo : IRepositoryBase<GzybCodeContrastEntity>
    {
        /// <summary>
        /// 判断是否存在该数据
        /// </summary>
        /// <param name="aaa100">代码类别</param>
        /// <param name="aaa102">代码值</param>
        /// <returns></returns>
        bool Exist(string aaa100, string aaa102);

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="entity"></param>
        bool InsertInfo(GzybCodeContrastEntity entity);
    }
}
