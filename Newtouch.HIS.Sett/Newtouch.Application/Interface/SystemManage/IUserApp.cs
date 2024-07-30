using Newtouch.Tools;
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Common;

namespace Newtouch.HIS.Application
{
    public interface IUserApp
    {
        List<UserEntity> GetList(Pagination pagination, string keyword);

        UserEntity GetForm(string keyValue);

        void DeleteForm(string keyValue);

        void SubmitForm(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string[] roleList, string[] dutyList, string keyValue);


        void UpdateForm(UserEntity userEntity);

        UserEntity CheckLogin(string username, string password);


    }
}
