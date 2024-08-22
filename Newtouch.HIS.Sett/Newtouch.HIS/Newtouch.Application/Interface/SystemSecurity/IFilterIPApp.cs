using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFilterIPApp
    {
        List<FilterIPEntity> GetList(string keyword);

        FilterIPEntity GetForm(string keyValue);

        void DeleteForm(string keyValue);

        void SubmitForm(FilterIPEntity filterIPEntity, string keyValue);

    }
}
