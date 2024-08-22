using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.InsuranceManage;
using System.Collections.Generic;


namespace Newtouch.HIS.Domain.IRepository.InsuranceManage
{
    public interface ICommercialInsuranceRepo : IRepositoryBase<CommercialInsuranceEntity>
    {
        CInsuranceInfoInGrid GetForm(string keyValue, string orgId);
        List<CInsuranceInfoInGrid> GetListJson(string orgId, string code, string engName);
        void DeleteForm(string keyValue);
        List<CommercialInsuranceEntity> GetList(string orgId);
    }
}
