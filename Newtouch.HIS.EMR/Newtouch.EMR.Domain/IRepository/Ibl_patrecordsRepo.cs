using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;

namespace Newtouch.EMR.Domain.IRepository
{
 
    public interface Ibl_patrecordsRepo : IRepositoryBase<bl_patrecordsEntity>
    {

        bl_patrecordsEntity GetpatrecordsByID(string ID);
        void SubmitForm(bl_patrecordsEntity entity, string keyValue);

    }
}