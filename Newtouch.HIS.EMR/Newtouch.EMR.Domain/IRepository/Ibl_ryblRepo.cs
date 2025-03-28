using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;

namespace Newtouch.EMR.Domain.IRepository
{
 
    public interface Ibl_ryblRepo : IRepositoryBase<bl_ryblEntity>
    {

        bl_ryblEntity bl_ryblGetByID(string ID);
        void SubmitForm(bl_ryblEntity entity, string keyValue);
    }
}