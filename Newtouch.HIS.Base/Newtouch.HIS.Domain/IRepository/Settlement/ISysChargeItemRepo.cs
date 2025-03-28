using Newtouch.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 收费项目
    /// </summary>
    public interface ISysChargeItemRepo : IRepositoryBase<SysChargeItemEntity>
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysChargeItemEntity> GetList(string keyValue, string orgId);
        
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysChargeItemEntity GetForm(int keyValue);

        /// <summary>
        /// 提交保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysChargeItemEntity entity, int? keyValue);

        /// <summary>
        /// 医保同步修改最后上传时间
        /// </summary>
        /// <param name="ypId"></param>
        /// <returns></returns>
        bool YibaoUpload(int sfxmId, out string error);
    }
}
