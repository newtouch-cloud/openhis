/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Tools;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.Common;

namespace Newtouch.HIS.Application
{
    public interface ISysCIRemindContentApp
    {
        /// <summary>
        /// 收费大类所有数据
        /// </summary>
        /// <returns></returns>
        List<SysChargeItemWarningContentEntity> GetList();

        /// <summary>
        /// A页面带一个对象到B页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysCIRemindContentVO GetForm(int keyValue);
        void DeleteForm(int keyValue);
        void SubmitForm(SysChargeItemWarningContentEntity xt_sfxmjsnrEntity, string keyValue);

        /// <summary>
        /// 获取所有收费大类下拉框
        /// </summary>
        /// <returns></returns>
        string GetdlSelect();

        /// <summary>
        /// Grid筛选查询显示
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<SysCIRemindContentVO> GetListBySearch(Pagination pagination, string keyword);
    }
}
