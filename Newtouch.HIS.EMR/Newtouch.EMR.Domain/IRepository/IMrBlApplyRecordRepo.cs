using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.ValueObjects.API;
using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.IRepository
{
    public interface IMrBlApplyRecordRepo : IRepositoryBase<MrBlApplyRecordEntity>
    {
        void SubmitForm(MrApplyWritingVo entity, ApplyWritingResp resp, string czr, string orgId);
    }
}
