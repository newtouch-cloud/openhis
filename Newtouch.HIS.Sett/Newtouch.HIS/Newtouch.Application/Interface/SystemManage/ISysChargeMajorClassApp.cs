/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Tools;
using Newtouch.Common;

namespace Newtouch.HIS.Application
{
    public interface ISysChargeMajorClassApp
    {

        /// <summary>
        /// A页面带一个对象到B页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysChargeCategoryEntity GetForm(int keyValue);

        void DeleteForm(int keyValue);
        void SubmitForm(SysChargeCategoryEntity xt_sfdlEntity, string keyValue);

        /// <summary>
        /// 收费大类下拉框
        /// </summary>
        /// <returns></returns>
        string GetsfdlTreeSelectJson();
    }
}
