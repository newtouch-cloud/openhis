using Newtouch.EMR.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.IRepository
{
    public interface IMzmeddocsrelationRepo : IRepositoryBase<MzmeddocsrelationEntity>
    {
        List<MzmeddocsrelationEntity> GetTreeList(string orgId, string mzh);
        void SubmitForm(MzmeddocsrelationEntity entity, string keyValue);
        void DeleteForm(string keyValue);
        void SubmitEntity(MzmeddocsrelationEntity entity);
    }
}
