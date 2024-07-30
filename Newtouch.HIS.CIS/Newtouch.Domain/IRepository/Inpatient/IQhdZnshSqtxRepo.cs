using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IRepository.Inpatient
{
    public interface IQhdZnshSqtxRepo : IRepositoryBase<QhdZnshSqtxEntity>
    {
        void SubmitForm(QhdZnshSqtxEntity entity,out string rzid, string keyValue=null);
    }
}
