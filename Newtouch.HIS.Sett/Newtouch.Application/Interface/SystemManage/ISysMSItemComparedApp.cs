using System;
using System.Data;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMSItemComparedApp
    {
        /// <summary>
        /// A页面带一个对象到B页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysMedicalTechItemMappEntity GetForm(Guid keyValue);
        void DeleteForm(int keyValue);
        void SubmitForm(SysMedicalTechItemMappEntity entity, string keyValue);

        /// <summary>
        /// Grid筛选查询显示
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        DataTable GetListBySearch(string keyword);
    }
}
