using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System;

namespace Newtouch.MR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-20 11:05
    /// 描 述：病案首页
    /// </summary>
    public interface IMrbasyRepo : IRepositoryBase<MrbasyEntity>
    {
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        int SubmitForm(MrbasyEntity entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        int DeleteForm(string keyValue);

		/// <summary>
		/// 获取病人住院信息
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="organizeId"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		IList<MrbasyEntity> GetPagintionHospitalList(Pagination pagination, string organizeId, string jkkh, string keyword);


        /// <summary>
        /// 修改归档状态
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="ZYH"></param>
        /// <returns></returns>
        int Updatebazt(string dataId, string ZYH,string XM);

        int CXUpdatebazt(string dataId, string ZYH, string XM, string organizeId, string LastModifierCode, DateTime LastModifyTime);
        int CXGDZT(string dataId, string ZYH, string XM, string organizeId, string LastModifierCode, DateTime LastModifyTime);

    }
}