using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IRepository
{
    public interface IAdmissionNoticeRepo : IRepositoryBase<AdmissionNoticeEntity>
    {
        void SubmitForm(AdmissionNoticeEntity entity);
    }
}
