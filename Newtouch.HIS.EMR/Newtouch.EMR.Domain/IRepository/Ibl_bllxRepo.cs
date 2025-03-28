using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;

namespace Newtouch.EMR.Domain.IRepository
{
 
    public interface Ibl_bllxRepo : IRepositoryBase<bl_bllxEntity>
    {
        void SubmitForm(bl_bllxEntity entity, string keyValue);
        void DeleteForm(string keyValue, string orgId, string user);
    }
}