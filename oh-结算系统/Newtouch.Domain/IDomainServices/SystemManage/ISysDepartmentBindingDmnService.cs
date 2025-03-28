using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;

namespace Newtouch.HIS.Domain.IDomainServices.SystemManage
{
    public interface ISysDepartmentBindingDmnService
    {
        SysDepartmentBindingVO GetSaffEntity(string keyValue, string OrganizeId);
    }
}
