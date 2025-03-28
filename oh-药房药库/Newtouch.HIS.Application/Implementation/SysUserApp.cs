using Newtouch.Common.Exceptions;
using Newtouch.Core.Common.Security;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class SysUserApp: ISysUserApp
    {
        private readonly ISysUserRepo _sysUserRepo;
        private readonly ISysUserLogOnRepo _sysUserLogOnRepository;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly ISysRoleRepo _sysRoleRepository;

        public SysUserApp(ISysUserRepo sysUserRepo, ISysUserLogOnRepo sysUserLogOnRepository
            , ISysUserDmnService sysUserDmnService, ISysRoleRepo sysRoleRepository)
        {
            this._sysUserRepo = sysUserRepo;
            this._sysUserLogOnRepository = sysUserLogOnRepository;
            this._sysUserDmnService = sysUserDmnService;
            this._sysRoleRepository = sysRoleRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public SysUserEntity CheckLogin(string topOrganizeId, string username, string password)
        {
            var userEntity = _sysUserRepo.FindEntity(t => t.Account == username
                && t.TopOrganizeId == topOrganizeId);
            if (userEntity != null)
            {
                if (userEntity.zt == "1")
                {
                    var userLogOnEntity = _sysUserLogOnRepository.FindEntity(p => p.UserId == userEntity.Id);
                    string dbPassword = Md5.md5(DESEncrypt.Encrypt(password.ToLower(), userLogOnEntity.UserSecretkey).ToLower(), 32).ToLower();
                    if (dbPassword == userLogOnEntity.UserPassword)
                    {
                        var lastVisitTime = DateTime.Now;
                        int LogOnCount = (userLogOnEntity.LogOnCount).ToInt() + 1;
                        if (userLogOnEntity.LastVisitTime != null)
                        {
                            userLogOnEntity.PreviousVisitTime = userLogOnEntity.LastVisitTime.ToDate();
                        }
                        userLogOnEntity.LastVisitTime = lastVisitTime;
                        userLogOnEntity.LogOnCount = LogOnCount;
                        _sysUserLogOnRepository.Update(userLogOnEntity);
                        return userEntity;
                    }
                    else
                    {
                        throw new FailedException("密码不正确，请重新输入");
                    }
                }
                else
                {
                    throw new FailedException("账户被系统锁定,请联系管理员");
                }
            }
            else
            {
                throw new FailedException("账户不存在，请重新输入");
            }
        }
    }
}
