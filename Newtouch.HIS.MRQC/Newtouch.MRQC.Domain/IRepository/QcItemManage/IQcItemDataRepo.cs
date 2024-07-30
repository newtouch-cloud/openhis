using Newtouch.Infrastructure.EF;
using Newtouch.MRQC.Domain.Entity.QcItemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.Domain.IRepository.QcItemManage
{
    public interface IQcItemDataRepo : IRepositoryBase<QcItemDataEntity>
    {
        void DeleteForm(int Id, string orgId);
    }
}
