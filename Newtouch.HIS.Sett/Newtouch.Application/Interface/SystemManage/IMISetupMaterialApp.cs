using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    public interface IMISetupMaterialApp
    {
        List<MISetMaterialEntity> GetList();

        MISetMaterialEntity GetForm(string keyValue);

        void DeleteForm(int keyValue);

        void SubmitForm(MISetMaterialEntity entity, string keyValue);


        string GetybdmSelect();
    }
}
