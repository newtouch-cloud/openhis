using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2018-09-11 10:46
    /// 描 述：患者病历文书关系表
    /// </summary>
    public interface IZymeddocsrelationRepo : IRepositoryBase<ZymeddocsrelationEntity>
    {
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        void SubmitForm(ZymeddocsrelationEntity entity, string keyValue);
   
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        void DeleteForm(string keyValue);
        /// <summary>
        /// 新增/修改病人病历关系树
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="zyh"></param>
        /// <param name="BlId"></param>
        /// <param name="BllxId"></param>
        /// <param name="OrgId"></param>
        void SubmitEntity(ZymeddocsrelationEntity entity);

        List<ZymeddocsrelationEntity> GetTreeList(string orgId, string zyh);

    }
}