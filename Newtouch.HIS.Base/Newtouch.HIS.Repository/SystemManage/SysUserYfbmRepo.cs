using System;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysUserYfbmRepo : RepositoryBase<SysUserYfbmEntity>, ISysUserYfbmRepo
    {
        public SysUserYfbmRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="yfbmCode"></param>
        public void submitUserYfbm(string userId, string yfbmCode)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                //先 del
                db.Delete<SysUserYfbmEntity>(p => p.UserId == userId);
                //再添加
                var yfbmCodeArr = (yfbmCode ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var YfbmCode in yfbmCodeArr)
                {
                    string[] str = YfbmCode.Split(new string[] { "a88123jkjfwe13120j" }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
                    if (str.Length != 2)
                    {
                        continue;
                    }
                    string organizeId = str[0];
                    string yfbmcode = str[1];
                    var userYfbmentity = new SysUserYfbmEntity();
                    userYfbmentity.Id = Guid.NewGuid().ToString();
                    userYfbmentity.UserId = userId;
                    userYfbmentity.yfbmCode = yfbmcode;
                    userYfbmentity.OrganizeId = organizeId;
                    userYfbmentity.TopOrganizeId = Constants.TopOrganizeId;
                    userYfbmentity.Create();
                    db.Insert(userYfbmentity);
                }
                db.Commit();
            }
        }
    }
}
