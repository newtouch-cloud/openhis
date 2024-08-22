using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    public class SysPatientAccountRevenueAndExpenseRepo : RepositoryBase<SysPatientAccountRevenueAndExpenseEntity>, ISysPatientAccountRevenueAndExpenseRepo
    {
        
        public SysPatientAccountRevenueAndExpenseRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 根据住院号 获取 收支记录 列表
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<SysPatientAccountRevenueAndExpenseEntity> GetListByZyhAndYjjzh(string zyh, int zh, string orgId)
        {
            return this._dataContext.Set<SysPatientAccountRevenueAndExpenseEntity>().Where(p => p.zyh == zyh && p.zh == zh && p.OrganizeId == orgId).ToList();
        }


        /// <summary>
        /// 根据账号 获取账户余额
        /// </summary>
        /// <param name="zh"></param>
        /// <returns></returns>
        public SysPatientAccountRevenueAndExpenseEntity GetLastJL(int zh, string orgId)
        {
            var payList = this.IQueryable().Where(p => p.zh == zh && p.OrganizeId == orgId && p.zt == "1")
                .OrderByDescending(p => p.zhszjlbh).ToList();
            if (payList!=null && payList.Count > 0)
            {
                return payList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 添加收支记录
        /// 预交款充值
        /// </summary>
        public void AddPayInfo(SysPatientAccountRevenueAndExpenseEntity sysPatPayInfoEntity, string orgId, string curUserCode, string curUserName)
        {
                sysPatPayInfoEntity.CreatorCode = curUserCode;
                sysPatPayInfoEntity.CreatorUserName = curUserName;
                sysPatPayInfoEntity.CreateTime = DateTime.Now;
                sysPatPayInfoEntity.OrganizeId = orgId;
                sysPatPayInfoEntity.zhszjlbh = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("zhszjlbh");
                this.Insert(sysPatPayInfoEntity);
             
           
        }
         
    }
}
