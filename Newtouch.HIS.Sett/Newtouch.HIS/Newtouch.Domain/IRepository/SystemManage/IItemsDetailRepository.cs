/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 
*********************************************************************************/

using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface IItemsDetailRepository : IRepositoryBase<ItemsDetailEntity>
    {
        List<ItemsDetailEntity> GetItemList(string enCode);
    }
}
