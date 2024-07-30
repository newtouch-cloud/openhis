using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    public interface IAreaApp
    {
        List<AreaEntity> GetList();

        AreaEntity GetForm(string keyValue);

        void DeleteForm(string keyValue);

        void SubmitForm(AreaEntity areaEntity, string keyValue);


    }
}
