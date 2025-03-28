/**********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description：整合收费项目模板 及相应的收费项目列表 
// Author：hlf
// CreateDate： 2017/1/10 12:24:52 
//**********************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class OutPatChargeItemTempVO
    {
        /// <summary>
        /// 模板列表
        /// </summary>
        public List<ChargeItemTemplateVO> tempList { get; set; }

        /// <summary>
        /// 模板详细信息
        /// </summary>
        public List<OutPatChargeItemDetailVO> tempDetailList { get; set; }
    } 
}
