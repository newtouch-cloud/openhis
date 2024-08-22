using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class SysChargeTemplateInfoVM
    {
        /// <summary>
        /// 模板实体
        /// </summary>
        public SysChargeTemplateEntity TemplateEntity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<SysChargeItemTemplateVO> SysList { get; set; }

    }

}
