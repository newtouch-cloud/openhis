using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;

namespace Newtouch.EMR.Domain.IRepository
{
 
    public interface Ibl_hljlRepo : IRepositoryBase<bl_hljlEntity>
    {

        bl_hljlEntity bl_hljlGetByID(string ID);
        void SubmitForm(bl_hljlEntity entity, string keyValue);
    }
}