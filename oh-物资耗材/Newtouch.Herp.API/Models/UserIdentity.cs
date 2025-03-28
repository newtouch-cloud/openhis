/*******************************************************************************
 * Copyright © 2022 开源医疗授权 版权所有
 * Author: Newtouch
 * Description: 
*********************************************************************************/

using Newtouch.HIS.API.Common.Models;

namespace Newtouch.Herp.API.Models
{
    /// <summary>
    /// 用户身份（根据项目需要 扩展用户身份的字段）
    /// </summary>
    public class UserIdentity : Identity
    {
        /// <summary>
        /// 医疗机构Id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 顶级医疗机构Id
        /// </summary>
        public string TopOrganizeId { get; set; }

    }
}