using Newtouch.Core.Common;
using Newtouch.Domain.ValueObjects.Outpatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IDomainServices.Outpatient
{
    public interface IElectronicPrescriptionDmnService
    {
        IList<ElectronicPrescriptionVO> GetGridJson(Pagination pagination, string organizeId, DateTime kssj, DateTime jssj, string xm);
    }
}
