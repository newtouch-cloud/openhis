/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using System;
using System.Data;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using FrameworkBase.MultiOrg.Application;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMSItemComparedApp :AppBase, ISysMSItemComparedApp
    {

        private readonly ISysMedicalTechItemMappRepo _sysMedicalTechItemMappRepo;

        /// <summary>
        /// A页面带一个对象到B页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysMedicalTechItemMappEntity GetForm(Guid keyValue)
        {
            return _sysMedicalTechItemMappRepo.FindEntity(keyValue);
        }
        public void DeleteForm(int keyValue)
        {
            _sysMedicalTechItemMappRepo.Delete(t => t.yjxmdzbh == keyValue);
        }
        public void SubmitForm(SysMedicalTechItemMappEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                _sysMedicalTechItemMappRepo.Update(entity);
            }
            else
            {
                entity.Create();
                _sysMedicalTechItemMappRepo.Insert(entity);
            }
        }

        /// <summary>
        /// Grid筛选查询显示
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public DataTable GetListBySearch(string keyword)
        {
            //xt_yjxmdz 根据xm 找到 对于 执行科室
            return null;
        }
    }
}
