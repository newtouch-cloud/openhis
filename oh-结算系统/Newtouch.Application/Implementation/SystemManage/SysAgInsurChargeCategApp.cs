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
    public class SysAgInsurChargeCategApp : ISysAgInsurChargeCategApp
    {
        private readonly ISysAgriInsuranceChargeCategoryRepo _sysAgInsurChargeCategRepo;

        public SysAgInsurChargeCategApp(ISysAgriInsuranceChargeCategoryRepo sysAgInsurChargeCategRepo)
        {
            this._sysAgInsurChargeCategRepo = sysAgInsurChargeCategRepo;
        }
        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <returns></returns>
        public List<SysAgriInsuranceChargeCategoryEntity> GetEffectiveList(int keyValue)
        {
            return _sysAgInsurChargeCategRepo.GetEffectiveList(keyValue);
        }
        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysAgriInsuranceChargeCategoryEntity GetForm(int keyValue)
        {
            return _sysAgInsurChargeCategRepo.GetForm(keyValue);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(int keyValue)
        {
            _sysAgInsurChargeCategRepo.DeleteForm(keyValue);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sysAgInsurChargeCategEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysAgriInsuranceChargeCategoryEntity sysAgInsurChargeCategEntity, int keyValue)
        {
            _sysAgInsurChargeCategRepo.SubmitForm(sysAgInsurChargeCategEntity, keyValue);
        }
        /// <summary>
        /// 获取所有农保收费大类下拉框
        /// </summary>
        /// <returns></returns>
        public object GetnbdlSelect()
        {
            var orgId = OperatorProvider.GetCurrent().OrganizeId;
            return _sysAgInsurChargeCategRepo.GetnbdlSelect(orgId);
        }
    }
}
