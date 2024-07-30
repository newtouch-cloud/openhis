using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage
{
    public interface ISysMedicineBaseDmnService
    {
        IList<SysMedicineClassificationVO> GetValidList(string keyword = null);
        SysMedicineAntibioticInfoVO GetKssInfo(string Id, string OrganizeId);
        IList<SysItemsDetailVO> GetValidListByItemCode(string code, string keyword, string orgId = null);
        List<SysMedicalOrderFrequencyVO> GetOrderFrequencyList(string orgId, string keyword = null);
        IList<SysChargeCategoryVO> GetsfdlValidList(string orgId);
        IList<SysMedicineFormulationVO> GetypjxValidList(string keyword = null);
        IList<SysMedicineUnitVO> GetypdwValidList(string keyword = null);
        int Insertyfbmyp(SysPharmacyDepartmentOpenMedicineVO entity);
        IList<PharmacyDepartmentOpenMedicineRepoVO> SelectDepartmentMedicine(string dlCode, string organizeId);
        string SubmitForm(SysMedicineAntibioticInfoVO entity);
        IList<ShybBrxzblVo> Getybbxbldata(string keyword, string orgId);
        void SaveYbblValue(List<Sh_YbfyxzblVO> entity, string xmbm, string xmmc, string orgId, string CreatorCode);
    }
}
