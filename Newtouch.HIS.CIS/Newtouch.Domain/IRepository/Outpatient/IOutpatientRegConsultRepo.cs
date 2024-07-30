using Newtouch.Domain.Entity.Outpatient;
using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IRepository.Outpatient
{
    public interface IOutpatientRegConsultRepo : IRepositoryBase<OutpatientRegConsultEntity>
    {
        int SubmitForm(OutpatientRegConsultEntity entity);
        int UpdateCalledstu(int ghnm, int calledstu);
    }
}
