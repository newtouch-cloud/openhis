using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    public class SysChargeItemPriceAdjustRepo : RepositoryBase<SysChargeItemPriceAdjustEntity>, ISysChargeItemPriceAdjustRepo
    {
        public SysChargeItemPriceAdjustRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <returns></returns>
        public List<SysChargeItemPriceAdjustEntity> GetEffectiveList(string keyValue, string orgId)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                return this.IQueryable().Where(a => a.OrganizeId == orgId && a.sfxmmc.Contains(keyValue)).ToList();
            }
            return this.IQueryable().ToList();
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysChargeItemPriceAdjustEntity GetForm(int? keyValue, string orgId)
        {
            return this.IQueryable().Where(a => a.OrganizeId == orgId).FirstOrDefault();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(SysChargeItemPriceAdjustEntity sysChargeProjPriceAdjEntity, string orgId)
        {
            var entity = this.IQueryable().Where(a => a.OrganizeId == orgId && a.tjbh== sysChargeProjPriceAdjEntity.tjbh).FirstOrDefault();
            entity.zt = "0";
            this.Update(entity);

        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sysChargeProjPriceAdjEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysChargeItemPriceAdjustEntity sysChargeProjPriceAdjEntity, int? keyValue, string orgId)
        {
            sysChargeProjPriceAdjEntity.OrganizeId = orgId;
            sysChargeProjPriceAdjEntity.Create(true, EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("xt_sfxmtj"));
            this.Insert(sysChargeProjPriceAdjEntity);
        }

    }
}


