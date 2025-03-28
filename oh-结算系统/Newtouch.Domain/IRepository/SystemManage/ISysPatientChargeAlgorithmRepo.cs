/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 

*********************************************************************************/
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface ISysPatientChargeAlgorithmRepo : IRepositoryBase<SysPatientChargeAlgorithmEntity>
    {
        /// <summary>
        /// 获取所有有效算法
        /// </summary>
        /// <returns></returns>
        List<SysPatientChargeAlgorithmEntity> getAllMzActive();
        void SubmitForm(SysPatientChargeAlgorithmEntity entity, string keyValue);

        List<ChargeItemDetailVO> GetSFXMItemInfoByDlCode(string keyword, string dlCode, string orgId);
    }
}
