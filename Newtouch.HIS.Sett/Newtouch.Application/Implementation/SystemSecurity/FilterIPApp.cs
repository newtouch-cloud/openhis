using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using FrameworkBase.MultiOrg.Application;

namespace Newtouch.HIS.Application.SystemSecurity
{
    /// <summary>
    /// 
    /// </summary>
    public class FilterIPApp: AppBase, IFilterIPApp
    {
        private readonly IFilterIPRepository _filterIPRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<FilterIPEntity> GetList(string keyword)
        {
            var expression = ExtLinq.True<FilterIPEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_StartIP.Contains(keyword));
            }
            return _filterIPRepository.IQueryable(expression).OrderByDescending(t => t.CreateTime).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public FilterIPEntity GetForm(string keyValue)
        {
            return _filterIPRepository.FindEntity(keyValue);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            _filterIPRepository.Delete(t => t.Id == keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterIPEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(FilterIPEntity filterIPEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                filterIPEntity.Modify(keyValue);
                _filterIPRepository.Update(filterIPEntity);
            }
            else
            {
                filterIPEntity.Create(true);
                _filterIPRepository.Insert(filterIPEntity);
            }
        }
    }
}
