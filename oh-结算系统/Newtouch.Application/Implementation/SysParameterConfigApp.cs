using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ViewModels;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using FrameworkBase.MultiOrg.Application;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 系统参数设置
    /// </summary>
    public class SysParameterConfigApp : AppBase, ISysParameterConfigApp
    {
        private readonly ISysChargeTemplateRepo _sysChargeTemplateEntityRepository;
        private readonly ISysFeeDmnService _sysFeeDmnService;

        #region 收费模板

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysChargeTemplateInfoVM GetSysChargeTemplateInfo(string keyValue)
        {
            //收费项目列表
            var templateEntity = _sysChargeTemplateEntityRepository.IQueryable()
                    .Where(p => p.sfmbbh == keyValue).FirstOrDefault();
            if (templateEntity == null || string.IsNullOrWhiteSpace(keyValue) || templateEntity.OrganizeId == null)
            {
                return null;
            }
            return new SysChargeTemplateInfoVM()
            {
                TemplateEntity = templateEntity,
                SysList = _sysFeeDmnService.GetChargeTemplateChargeItemList(keyValue, templateEntity.OrganizeId)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="xmListStr"></param>
        public void ChargeTemplate_SubmitForm(SysChargeTemplateEntity entity, string xmListStr)
        {
            var itemList = Json.ToObject<IList<SysChargeItemTemplateVO>>(xmListStr);
            _sysFeeDmnService.SaveChargeTemplate(entity, itemList);
        }

        #endregion

    }
}
