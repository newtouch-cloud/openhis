/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 

*********************************************************************************/

using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    public class SysChargeItemSpecialMarkRepo : RepositoryBase<SysChargeItemSpecialMarkEntity>, ISysChargeItemSpecialMarkRepo
    {
        public SysChargeItemSpecialMarkRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public void submitform(SysChargeItemSpecialMarkEntity xt_sfxmtsbzEntity, string keyValue)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {

                    xt_sfxmtsbzEntity.Modify(int.Parse(keyValue));
                    db.Update(xt_sfxmtsbzEntity);
                }
                else
                {
                    xt_sfxmtsbzEntity.Create(true, EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("xt_sfxmtsbz"));
                    db.Insert(xt_sfxmtsbzEntity);
                }
                db.Commit();
            }
        }
    }
}
