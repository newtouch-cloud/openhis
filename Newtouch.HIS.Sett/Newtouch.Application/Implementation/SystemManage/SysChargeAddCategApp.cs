using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Common;
using FrameworkBase.MultiOrg.Application;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class SysChargeAddCategApp: AppBase, ISysChargeAddCategApp
    {
        private readonly ISysChargeAdditionalCategoryRepo _sysChargeAddCategRepo;

        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <returns></returns>
        public List<SysChargeAdditionalCategoryEntity> GetEffectiveList(int keyValue)
        {
            return _sysChargeAddCategRepo.GetEffectiveList(keyValue, this.OrganizeId);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(int keyValue)
        {
            _sysChargeAddCategRepo.DeleteForm(keyValue, this.OrganizeId);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sysChargeAddCategEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysChargeAdditionalCategoryEntity sysChargeAddCategEntity, int keyValue)
        {
            _sysChargeAddCategRepo.SubmitForm(sysChargeAddCategEntity, keyValue, this.OrganizeId);
        }
        /// <summary>
        /// 获取所有收费大类下拉框
        /// </summary>
        /// <returns></returns>
        public string GetSysChargeAddCategSelect()
        {
            var data = this.GetEffectiveList(0);
            var treeList = new List<TreeSelectModel>();
            foreach (SysChargeAdditionalCategoryEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.dl;
                treeModel.text = item.dlmc;
                treeModel.parentId = "0";
                treeList.Add(treeModel);
            }
            return treeList.TreeSelectJson();
        }


    }
}
