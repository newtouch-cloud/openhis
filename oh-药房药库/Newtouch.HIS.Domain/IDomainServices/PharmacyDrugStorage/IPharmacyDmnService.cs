using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage
{
    public interface IPharmacyDmnService
    {
        IList<PharmacyWindowVO> GetPagintionList(Pagination pagination, string organizeId, string keyword = null);
        IList<PharmacyDepartmentOpenMedicineRepoVO> SelectDepartmentMedicine(string dlCode, string organizeId);
    }
}
