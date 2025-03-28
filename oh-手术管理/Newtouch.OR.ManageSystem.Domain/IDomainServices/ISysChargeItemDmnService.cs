using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.Core.Common;
using Newtouch.OR.ManageSystem.Domain.DTO.InputDto;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;

namespace Newtouch.OR.ManageSystem.Domain.IDomainServices
{
    public interface ISysChargeItemDmnService
    {
        /// <summary>
        /// 收费模板页面index list
        /// </summary>
        /// <returns></returns>
        IList<SysChargeTemplateGridVO> Search(Pagination pagination, string keyword, string ks, string organizeId);

        void ChargeTemplate_SubmitForm(SysChargeTemplateDto entity, string xmListStr,string user);
        /// <summary>
        /// 收费项目
        /// </summary>
        /// <param name="cnt"></param>
        /// <param name="orgId"></param>
        /// <param name="dl">1药品 2项目 3非治疗项目	多个 逗号分割</param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysChargeItemTemplateVO> GetDicChargeItems(int cnt, string orgId, string dl, string keyword);

        IList<SysChargeItemTemplateVO> GetChargeTemplateChargeItemList(string sfmbbh, string orgId);
        SysChargeTemplateInfoVM GetSysChargeTemplateInfo(string keyValue, string orgId);
    }
}
