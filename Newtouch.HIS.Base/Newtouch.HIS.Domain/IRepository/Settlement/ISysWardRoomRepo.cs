using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface ISysWardRoomRepo : IRepositoryBase<SysWardRoomEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysWardRoomEntity> GetPagintionList(Pagination pagination, string organizeId, string keyword);

        /// <summary>
        /// 更新病区信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void submitForm(SysWardRoomEntity entity, int? keyValue);

        ///// <summary>
        ///// 获取所有病区列表
        ///// </summary>
        //List<SysWardEntity> SelectWardList(string orgId);
    }
}
