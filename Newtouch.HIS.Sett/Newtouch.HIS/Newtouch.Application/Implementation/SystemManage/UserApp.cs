using Newtouch.Tools;
using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Common;
using Newtouch.Infrastructure;
using Newtouch.Core.Common.Security;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class UserApp : IUserApp
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserLogOnRepository _userLogOnRepository;
      

        public UserApp(IUserRepository userRepository, IUserLogOnRepository userLogOnRepository)
        {
            this._userLogOnRepository = userLogOnRepository;
            this._userRepository = userRepository;
          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<UserEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<UserEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Account.Contains(keyword));
                expression = expression.Or(t => t.F_RealName.Contains(keyword));
                expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
            }
            expression = expression.And(t => t.F_Account != "admin");
            return _userRepository.FindList(expression, pagination);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public UserEntity GetForm(string keyValue)
        {
            return _userRepository.FindEntity(keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            _userRepository.DeleteForm(keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string[] roleList, string[] dutyList, string keyValue)
        {
            UserEntity oldEntity = null;   //变更前Entity
            if (!string.IsNullOrEmpty(keyValue))
            {
                oldEntity = _userRepository.FindEntity(t => t.Id == keyValue);
                _userRepository.DetacheEntity(oldEntity);

                userEntity.Modify(keyValue);
            }
            else
            {
                userEntity.Create(true);
            }
         
            //角色list
            List<UserRoleEntity> roleLists = new List<UserRoleEntity>();
            foreach (var item in roleList)
            {
                UserRoleEntity entity = new UserRoleEntity();
                entity.Create(true);
                entity.RoleId = item;
                entity.UserId = userEntity.Id;
                entity.zt = ((int)EnumZT.Valid).ToString();
                roleLists.Add(entity);
            }
            //岗位list
            List<UserDutyEntity> dutyLists = new List<UserDutyEntity>();
            foreach (var item in dutyList)
            {
                UserDutyEntity entity = new UserDutyEntity();
                entity.Create(true);
                entity.DutyId = item;
                entity.UserId = userEntity.Id;
                entity.zt = ((int)EnumZT.Valid).ToString();
                dutyLists.Add(entity);
            }
            _userRepository.SubmitForm(userEntity, userLogOnEntity, roleLists, dutyLists, keyValue);

            if (oldEntity != null)
            {
                AppLogger.WriteEntityChangeRecordLog(oldEntity, userEntity, UserEntity.GetTableName(), oldEntity.Id, ignoreFieldNameList: new string[] { "px", "F_NickName" });
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userEntity"></param>
        public void UpdateForm(UserEntity userEntity)
        {
            _userRepository.Update(userEntity, dstnFieldNameList: new[] { "zt" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserEntity CheckLogin(string username, string password)
        {
            UserEntity userEntity = _userRepository.FindEntity(t => t.F_Account == username);
            if (userEntity != null)
            {
                if (userEntity.zt == ((int)EnumZT.Valid).ToString())
                {
                    UserLogOnEntity userLogOnEntity = _userLogOnRepository.FindEntity(userEntity.Id);
                    string dbPassword = Md5.md5(DESEncrypt.Encrypt(password.ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
                    if (dbPassword == userLogOnEntity.F_UserPassword)
                    {
                        DateTime lastVisitTime = DateTime.Now;
                        int LogOnCount = (userLogOnEntity.F_LogOnCount).ToInt() + 1;
                        if (userLogOnEntity.F_LastVisitTime != null)
                        {
                            userLogOnEntity.F_PreviousVisitTime = userLogOnEntity.F_LastVisitTime.ToDate();
                        }
                        userLogOnEntity.F_LastVisitTime = lastVisitTime;
                        userLogOnEntity.F_LogOnCount = LogOnCount;
                        _userLogOnRepository.Update(userLogOnEntity);
                        return userEntity;
                    }
                    else
                    {
                        throw new Exception("密码不正确，请重新输入");
                    }
                }
                else
                {
                    throw new Exception("账户被系统锁定,请联系管理员");
                }
            }
            else
            {
                throw new Exception("账户不存在，请重新输入");
            }
        }

    }
}
