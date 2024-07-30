using Newtouch.EMR.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.IRepository
{
    public interface Ibl_ElementDomain_DetailRepo : IRepositoryBase<bl_ElementDomain_DetailEntity>
    {

        void SubmitForm(bl_ElementDomain_DetailEntity entity, string keyValue);
        void DeleteForm(string keyValue);
    }
}
