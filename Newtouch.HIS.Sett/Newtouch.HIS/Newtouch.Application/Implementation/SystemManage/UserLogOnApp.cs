using Newtouch.Tools;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.Entity;
using System;
using System.Linq;
using Newtouch.Core.Common.Security;

namespace Newtouch.HIS.Application.SystemManage
{
    /// <summary>
    /// 
    /// </summary>
    public class UserLogOnApp : IUserLogOnApp
    {
        private readonly IUserLogOnRepository _userLogOnRepository;
        private readonly IUserRepository _userRepository;

        public UserLogOnApp(IUserLogOnRepository userLogOnRepository, IUserRepository userRepository)
        {
            this._userLogOnRepository = userLogOnRepository;
            this._userRepository = userRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public UserLogOnEntity GetForm(string keyValue)
        {
            return _userLogOnRepository.FindEntity(keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLogOnEntity"></param>
        public void UpdateForm(UserLogOnEntity userLogOnEntity)
        {
            _userLogOnRepository.Update(userLogOnEntity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userPassword"></param>
        /// <param name="keyValue"></param>
        public void RevisePassword(string userPassword, string keyValue)
        {
            var userLogOnEntity = _userLogOnRepository.FindEntity(p => p.F_UserId == keyValue);    //是UserId

            if (userLogOnEntity == null)
            {
                throw new Exception();
            }

            userLogOnEntity.F_UserSecretkey = Md5.md5(Comm.CreateNo(), 16).ToLower();
            userLogOnEntity.F_UserPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userPassword, 32).ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();

            _userLogOnRepository.Update(userLogOnEntity, dstnFieldNameList: new[] { "F_UserPassword", "F_UserSecretkey" });
        }

    }
}
