using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 出入库方式
    /// </summary>
    public interface IXtypcrkfsRepo : IRepositoryBase<XtypcrkfsEntity>
    {
        /// <summary>
        /// get crkfs list 
        /// </summary>
        /// <param name="crkbz"></param>
        /// <returns></returns>
        List<XtypcrkfsEntity> GetCrkfsList(string crkbz);
    }
}