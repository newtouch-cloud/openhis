/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using Newtouch.Tools;
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Linq;
using Newtouch.Common.Operator;

namespace Newtouch.HIS.Application
{
    public class SysMRChargeClassApp : ISysMRChargeClassApp
    {
        private readonly ISysMedicalRecordChargeCategoryRepo _sysMRChargeClassRepo;

        public SysMRChargeClassApp(ISysMedicalRecordChargeCategoryRepo sysMRChargeClassRepo)
        {
            this._sysMRChargeClassRepo = sysMRChargeClassRepo;
        }
        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <returns></returns>
        public List<SysMedicalRecordChargeCategoryEntity> GetEffectiveList(string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                return _sysMRChargeClassRepo.IQueryable().Where(a => a.dl.Contains(keyValue) || a.dlmc.Contains(keyValue) || a.py.Contains(keyValue)).ToList();
            }
            else
            {
                return _sysMRChargeClassRepo.IQueryable().ToList();
            }
            
        }
        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysMedicalRecordChargeCategoryEntity GetForm(int keyValue)
        {
            return _sysMRChargeClassRepo.FindEntity(keyValue);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(SysMedicalRecordChargeCategoryEntity sysMRChargeClassEntity)
        {
            _sysMRChargeClassRepo.DeleteForm(sysMRChargeClassEntity);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sysMRChargeClassEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysMedicalRecordChargeCategoryEntity sysMRChargeClassEntity, int? keyValue)
        {
            _sysMRChargeClassRepo.SubmitForm(sysMRChargeClassEntity, keyValue);
        }
        /// <summary>
        /// 获取所有病案收费大类下拉框
        /// </summary>
        /// <returns></returns>
        public object GetdlSelect()
        {
            var orgId = OperatorProvider.GetCurrent().OrganizeId;
            return _sysMRChargeClassRepo.GetdlSelect(orgId);
        }
    }
}
