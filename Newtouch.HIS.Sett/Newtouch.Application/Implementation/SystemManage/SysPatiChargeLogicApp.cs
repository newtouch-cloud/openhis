/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.Core.Common;
using FrameworkBase.MultiOrg.Application;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPatiChargeLogicApp : AppBase, ISysPatiChargeLogicApp
    {

        private readonly ISysPatientChargeAlgorithmRepo _xt_brsfsfRepository;
        private readonly ICLogicJoinGridDmnService _chargeLogicJoinInGridSer;

        public List<PatiChargeLogicVO> GetList(Pagination pagination, string keyword, string orgId)
        {
            return _chargeLogicJoinInGridSer.GetPatiChargeLogicBySearch(pagination, keyword,orgId);
        }

        public PatiChargeLogicVO GetForm(string keyValue)
        {
            var data = _chargeLogicJoinInGridSer.GetPatiChargeLogicFirst(keyValue)[0];
            return data;
        }
        public void DeleteForm(int keyValue)
        {
            _xt_brsfsfRepository.Delete(t => t.brsfsfbh == keyValue);
        }

        public void SubmitForm(PatiChargeLogicVO vo, string keyValue, string orgId)
        {
            var entity = new SysPatientChargeAlgorithmEntity
            {
                brxz = vo.brxz,
                dl = vo.dl,
                sfxm = vo.sfxm,
                sfjb = vo.sfjb,
                fyfw = vo.fyfw,
                zfbl = vo.zfbl,
                zfxz = vo.zfxz,
                fysx = vo.fysx,
                mzzybz = vo.mzzybz,
                zt = vo.zt,
                OrganizeId = orgId
            };
            _xt_brsfsfRepository.SubmitForm(entity, keyValue);

        }

        public List<ChargeItemDetailVO> GetSFXMItemInfoByDlCode(string keyword,string dlCode,string orgId)
        {
           return  _xt_brsfsfRepository.GetSFXMItemInfoByDlCode(keyword,dlCode,orgId);
        }
    }
}
