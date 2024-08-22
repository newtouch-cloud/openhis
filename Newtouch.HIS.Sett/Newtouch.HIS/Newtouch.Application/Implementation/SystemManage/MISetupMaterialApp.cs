/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application
{
    public class MISetupMaterialApp : IMISetupMaterialApp
    {

        private readonly IMISetMaterialRepo _mISetMaterialRepo;

        public MISetupMaterialApp(IMISetMaterialRepo mISetMaterialRepo)
        {
            this._mISetMaterialRepo = mISetMaterialRepo;
        }

        public List<MISetMaterialEntity> GetList()
        {
            return _mISetMaterialRepo.IQueryable().ToList();
        }
        public MISetMaterialEntity GetForm(string keyValue)
        {
            return _mISetMaterialRepo.FindEntity(keyValue);
        }
        public void DeleteForm(int keyValue)
        {
            _mISetMaterialRepo.Delete(t => t.cl_id == keyValue);
        }
        public void SubmitForm(MISetMaterialEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                _mISetMaterialRepo.Update(entity);
            }
            else
            {
                entity.Create();
                _mISetMaterialRepo.Insert(entity);
            }
        }
        /// <summary>
        /// 获取医保-设置-材料的物价代码
        /// </summary>
        /// <returns></returns>
        public string GetybdmSelect()
        {
            //var data = this.GetList();
            //var treeList = new List<TreeSelectModel>();
            //foreach (var item in data)
            //{
            //    TreeSelectModel treeModel = new TreeSelectModel();
            //    treeModel.id = item.ybdm;
            //    treeModel.text = item.;
            //    treeModel.parentId = "0";
            //    treeList.Add(treeModel);
            //}
            //return treeList.TreeSelectJson();
            return "";
        }
    }
}
