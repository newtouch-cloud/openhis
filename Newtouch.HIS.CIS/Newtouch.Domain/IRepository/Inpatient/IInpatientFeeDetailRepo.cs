using Newtouch.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2018-06-28 13:27
    /// 描 述：住院费用明细库
    /// </summary>
    public interface IInpatientFeeDetailRepo : IRepositoryBase<InpatientFeeDetailEntity>
    {
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        void SubmitForm(InpatientFeeDetailEntity entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        void DeleteForm(string keyValue);

        /// <summary>
        /// 通过住院号,医嘱性质获取收费信息
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="zyh"></param>
        /// <param name="yzxz"></param>
        /// <returns></returns>
        List<InpatientFeeDetailEntity> GetListByZyhYzxz(string OrganizeId, string zyh, string yzxz);

    }
}