/**********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description： 单个模板对应项目列表
// Author：hlf
// CreateDate： 2017/1/10 12:41:31 
//**********************************************************/

using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class OutPatChargeItemDetailVO
    {
        /// <summary>
        /// 模板编号
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 项目列表
        /// </summary>
        public List<TemplateContentVO> tempDetailList { get; set; }
    }
}
