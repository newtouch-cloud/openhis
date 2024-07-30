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
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.HIS.Domain.IDomainServices.SystemManage;
using Newtouch.Common;
using Newtouch.Common.Operator;
using System;

namespace Newtouch.HIS.Application
{
    public class SysChargeItemApp : ISysChargeItemApp
    {
        private readonly ISysChargeItemRepo _xt_sfxmRepository;
        private readonly ISysChargeItemDmnService _ISysChargeItemDmnService;

        public SysChargeItemApp(ISysChargeItemRepo xt_sfxmRepository, ISysChargeItemDmnService ISysChargeItemDmnService)
        {
            this._xt_sfxmRepository = xt_sfxmRepository;
            this._ISysChargeItemDmnService = ISysChargeItemDmnService;
        }

        public SysChargeItemVO GetForm(int keyValue)
        {
            return _ISysChargeItemDmnService.GetsfxmFormJson(keyValue);
        }
        public void DeleteForm(int keyValue)
        {
            //_xt_sfxmRepository.Delete(t => t.sfxmbh == keyValue);
        }

        public void SubmitForm(SysChargeItemEntity xt_sfxmEntity, string keyValue)
        {
            //_xt_sfxmRepository.SubmitForm(xt_sfxmEntity, keyValue);
        }

        /// <summary>
        /// 查询条件，筛选出收费项目grid
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>

        public List<SysChargeItemVO> GetsfxmBySearch(Pagination Pagination, string keyword)
        {
            return _ISysChargeItemDmnService.GetsfxmBySearch(Pagination, keyword);
        }
    }
}
