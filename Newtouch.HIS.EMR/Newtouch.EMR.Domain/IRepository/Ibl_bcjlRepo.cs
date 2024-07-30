using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;

namespace Newtouch.EMR.Domain.IRepository
{
 
    public interface Ibl_bcjlRepo : IRepositoryBase<bl_bcjlEntity>
    {
    
        bl_bcjlEntity bl_bcjlGetByID(string ID);
        void SubmitForm(bl_bcjlEntity entity, string keyValue);

    }
}