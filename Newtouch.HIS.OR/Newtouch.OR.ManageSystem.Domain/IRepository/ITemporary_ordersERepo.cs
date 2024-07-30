using Newtouch.Infrastructure.EF;
using Newtouch.OR.ManageSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.IRepository
{
    public interface ITemporary_ordersERepo : IRepositoryBase<Temporary_ordersEntity>
    {
        int Submitlsyz(Temporary_ordersEntity entity, string keyVaue);
    }
}
