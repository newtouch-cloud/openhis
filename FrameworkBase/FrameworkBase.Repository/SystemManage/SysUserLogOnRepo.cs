using System.Linq;
using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.IRepository;
using FrameworkBase.Infrastructure;
using Newtouch.Core.Common.Security;
using Newtouch.Tools;

namespace FrameworkBase.Repository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:15
    /// 描 述：用户登录信息表
    /// </summary>
    public sealed class SysUserLogOnRepo : RepositoryBase<SysUserLogOnEntity>, ISysUserLogOnRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public SysUserLogOnRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 更新 可 登录 状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="locked"></param>
        public void UpdateLockedStatus(string userId, bool? locked)
        {
            var entity = this.IQueryable().Where(p => p.UserId == userId).FirstOrDefault();
            if (entity != null)
            {
                entity.Locked = locked;
                entity.Modify();
                this.Update(entity);
            }
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userPassword"></param>
        /// <param name="keyValue"></param>
        public void RevisePassword(string userPassword, string keyValue)
        {
            var userLogOnEntity = this.FindEntity(p => p.UserId == keyValue);    //是UserId

            if (userLogOnEntity != null)
            {
                userLogOnEntity.UserSecretkey = Md5.md5(Comm.CreateNo(), 16).ToLower();
                userLogOnEntity.UserPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userPassword, 32).ToLower(), userLogOnEntity.UserSecretkey).ToLower(), 32).ToLower();

                userLogOnEntity.Modify();
                this.Update(userLogOnEntity, dstnFieldNameList: new[] { "UserPassword", "UserSecretkey" });
            }
        }

    }
}