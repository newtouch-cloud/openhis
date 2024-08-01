﻿
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;
using Newtouch.Domain.Entity;

namespace Newtouch.Domain.IRepository
{
    public interface IBlctglRepo : IRepositoryBase<BlctglEntity>
    {

        List<BlctglEntity> GetTreeList(string orgId, int qx, string UserCode, string parentId);

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        void SubmitForm(BlctglEntity entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        void DeleteForm(string keyValue);

        BlctglEntity GetCitiaoByID(string orgId, string ID);

    }
}
