/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.HIS.Domain.IDomainServices.SystemManage;
using Newtouch.Core.Common;
using FrameworkBase.MultiOrg.Application;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class SysCISpecialMarkApp : AppBase, ISysCISpecialMarkApp
    {
        private readonly ISysChargeItemSpecialMarkRepo _xt_sfxmtsbzRepository;
        private readonly ISysCISpecialMarkDmnService _SysCISpecialMarkDmnService;

        /// <summary>
        /// A页面带一个对象到B页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysCISpecialMarkVO GetForm(int keyValue)
        {
            return _SysCISpecialMarkDmnService.GetFormJson(keyValue);
        }
        public void DeleteForm(int keyValue)
        {
            _xt_sfxmtsbzRepository.Delete(t => t.sfxmtsbzbh == keyValue);
        }
        public void SubmitForm(SysChargeItemSpecialMarkEntity xt_sfxmtsbzEntity, string keyValue)
        {
            _xt_sfxmtsbzRepository.submitform(xt_sfxmtsbzEntity, keyValue);
        }

        /// <summary>
        /// Grid筛选查询显示
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<SysCISpecialMarkVO> GetListBySearch(Pagination pagination, string keyword)
        {

            return _SysCISpecialMarkDmnService.GetGridBySearch(pagination, keyword);
        }
    }
}
