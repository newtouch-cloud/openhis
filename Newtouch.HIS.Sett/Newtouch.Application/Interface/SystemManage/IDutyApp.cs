using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Tools;
using Newtouch.Common;

namespace Newtouch.HIS.Application
{
    public interface IDutyApp
    {
        //    List<RoleEntity> GetList(string keyword = "");

        //    List<RoleEntity> GetList(Pagination pagination, string keyword = "");

        //    RoleEntity GetForm(string keyValue);

        //    void DeleteForm(string keyValue);

        //    void SubmitForm(RoleEntity roleEntity, string keyValue);

        List<DutyEntity> GetList(string keyword = "");

        List<DutyEntity> GetList(Pagination pagination, string keyword = "");

        DutyEntity GetForm(string keyValue);

        void DeleteForm(string keyValue);

        void SubmitForm(DutyEntity roleEntity, string keyValue);
    }
}
