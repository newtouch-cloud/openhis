using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.OR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-10-31 10:04
    /// 描 述：手术申请记录
    /// </summary>
    public interface IORApplyInfoRepo : IRepositoryBase<ORApplyInfoEntity>
    {
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        int SubmitForm(ORApplyInfoEntity entity, string keyValue);

        

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">申请单号</param>
        void DeleteForm(string keyValue, string orgId,string ssmcn);
        int UpdateSqzt(string keyValue, string sqzt);

    }
}