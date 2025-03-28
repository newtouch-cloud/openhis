using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    public interface IUserLogOnApp
    {
        UserLogOnEntity GetForm(string keyValue);

        void UpdateForm(UserLogOnEntity userLogOnEntity);

        void RevisePassword(string userPassword, string keyValue);


    }
}
