using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Tools;
using Newtouch.Common;

namespace Newtouch.HIS.Application
{
    public interface ISysChargeClassifyApp
    {

        /// <summary>
        /// 获取所有收费分类下拉框
        /// </summary>
        /// <returns></returns>
        object GetsfflSelect();

        /// <summary>
        /// A页面带一个对象到B页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysChargeClassificationEntity GetForm(int keyValue);
        void DeleteForm(int keyValue);
        void SubmitForm(SysChargeClassificationEntity xt_sfflEntity, string keyValue);

        /// <summary>
        /// Grid筛选查询显示
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<SysChargeClassificationEntity> GetListBySearch(Pagination Pagination, string keyword);
    }
}
