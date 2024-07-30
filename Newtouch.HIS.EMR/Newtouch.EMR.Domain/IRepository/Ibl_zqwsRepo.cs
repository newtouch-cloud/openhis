using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;

namespace Newtouch.EMR.Domain.IRepository
{
 
    public interface Ibl_zqwsRepo : IRepositoryBase<bl_zqwsEntity>
    {
    
        bl_zqwsEntity bl_zqwsGetByID(string ID);
        void SubmitForm(bl_zqwsEntity entity, string keyValue);
    }
}