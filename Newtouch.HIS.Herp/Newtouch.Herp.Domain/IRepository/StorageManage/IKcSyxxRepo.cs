using System.Collections.Generic;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// 库存损益
    /// </summary>
    public interface IKcSyxxRepo : IRepositoryBase<KcSyxxEntity>
    {
    }
}