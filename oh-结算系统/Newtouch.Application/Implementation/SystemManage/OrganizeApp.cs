using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Common.Operator;
using Newtouch.Common.Exceptions;
using Newtouch.Tools;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class OrganizeApp : IOrganizeApp
    {
        private readonly IOrganizeRepository _organizeRepository;

        public OrganizeApp(IOrganizeRepository organizeRepository)
        {
            this._organizeRepository = organizeRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<OrganizeEntity> GetList()
        {
            return _organizeRepository.IQueryable().OrderBy(t => t.CreateTime).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public OrganizeEntity GetForm(string keyValue)
        {
            return _organizeRepository.FindEntity(keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            if (_organizeRepository.IQueryable().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            {
                throw new FailedException("该机构包含下级机构，请先删除下级机构");
            }
            else
            {
                _organizeRepository.Delete(t => t.Id == keyValue);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizeEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(OrganizeEntity organizeEntity, string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(organizeEntity.F_MobilePhone) 
                && organizeEntity.F_MobilePhone != "&nbsp;"
                && !Validate.IsValidMobile(organizeEntity.F_MobilePhone))
            {
                throw new FailedException("手机号格式错误");
            }
            if (!string.IsNullOrWhiteSpace(organizeEntity.F_TelePhone)
                && organizeEntity.F_TelePhone != "&nbsp;"
                && !Validate.IsValidPhoneAndMobile(organizeEntity.F_TelePhone))
            {
                throw new FailedException("电话号码格式错误");
            }
            if (!string.IsNullOrEmpty(keyValue))
            {
                organizeEntity.Modify(keyValue);
                organizeEntity.LastModifyTime = DateTime.Now;
                organizeEntity.LastModifierCode = OperatorProvider.GetCurrent().UserCode;
                _organizeRepository.Update(organizeEntity, ignoreFieldNameList: new string[] { "px" });
            }
            else
            {
                organizeEntity.Create(true);
                _organizeRepository.Insert(organizeEntity);
            }
        }

    }
}
