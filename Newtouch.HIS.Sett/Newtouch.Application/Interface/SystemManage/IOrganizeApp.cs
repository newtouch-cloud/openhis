using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    public interface IOrganizeApp
    {
        List<OrganizeEntity> GetList();
        OrganizeEntity GetForm(string keyValue);

        void DeleteForm(string keyValue);

        void SubmitForm(OrganizeEntity organizeEntity, string keyValue);


    }
}
