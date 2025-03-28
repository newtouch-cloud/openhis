/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    public interface ISysMRChargeClassApp
    {
        List<SysMedicalRecordChargeCategoryEntity> GetEffectiveList(string keyValue);
        SysMedicalRecordChargeCategoryEntity GetForm(int keyValue);
        void DeleteForm(SysMedicalRecordChargeCategoryEntity sysMRChargeClassEntity);
        void SubmitForm(SysMedicalRecordChargeCategoryEntity sysMRChargeClassEntity, int? keyValue);
        object GetdlSelect();
    }
}
