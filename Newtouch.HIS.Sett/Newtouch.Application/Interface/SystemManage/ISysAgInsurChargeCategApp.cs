/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    public interface ISysAgInsurChargeCategApp
    {
        List<SysAgriInsuranceChargeCategoryEntity> GetEffectiveList(int keyValue);
        SysAgriInsuranceChargeCategoryEntity GetForm(int keyValue);
        void DeleteForm(int keyValue);
        void SubmitForm(SysAgriInsuranceChargeCategoryEntity sysAgInsurChargeCategEntity, int keyValue);

        /// <summary>
        /// 获取所有农保收费大类下拉框
        /// </summary>
        /// <returns></returns>
        object GetnbdlSelect();
    }
}
