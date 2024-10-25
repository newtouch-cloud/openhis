using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage
{
    public interface ISysMedicineElectronicPrescriptionDmnService
    {
        IList<SysMedicineElectronicPrescriptionVO> GetPaginationList(Pagination pagination, string genname, string medListCodg);
    }
}
