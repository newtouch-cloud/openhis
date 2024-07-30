using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.Entity;
using System;
using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class DutyApp : IDutyApp
    {
        private readonly IDutyRepository _dutyRepository;

        public DutyApp(IDutyRepository dutyRepository)
        {
            this._dutyRepository = dutyRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<DutyEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<DutyEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }
            expression = expression.And(t => t.zt == ((int)EnumZT.Valid).ToString());
            return _dutyRepository.IQueryable(expression).OrderBy(t => t.px).ToList();
        }

        public List<DutyEntity> GetList(Pagination pagination, string keyword = "")
        {
            var expression = ExtLinq.True<DutyEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }
            return _dutyRepository.FindList(expression, pagination).OrderBy(t => t.px).ToList(); ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public DutyEntity GetForm(string keyValue)
        {
            return _dutyRepository.FindEntity(keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            _dutyRepository.Delete(t => t.Id == keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DutyEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(DutyEntity dutyEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                dutyEntity.Modify(keyValue);
                dutyEntity.LastModifyTime = DateTime.Now;
                dutyEntity.LastModifierCode = OperatorProvider.GetCurrent().UserCode;
                _dutyRepository.Update(dutyEntity);
            }
            else
            {
                dutyEntity.Create(true);
                _dutyRepository.Insert(dutyEntity);
            }
        }

    }
}
