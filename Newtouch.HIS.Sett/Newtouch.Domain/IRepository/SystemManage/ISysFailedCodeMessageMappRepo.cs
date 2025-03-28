using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-09-30 14:21
    /// 描 述：错误提示配置
    /// </summary>
    public interface ISysFailedCodeMessageMappRepo : IRepositoryBase<SysFailedCodeMessageMappEntity>
    {
        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">筛选关键字</param>
        IList<SysFailedCodeMessageMappEntity> GetPagintionList(Pagination pagination, string keyword);

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        void SubmitForm(SysFailedCodeMessageMappEntity entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        void DeleteForm(string keyValue);

        /// <summary>
        /// 获取组织机构FailMsg配置列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysFailedCodeMessageMappEntity> GetListByOrgId(string orgId = null);

    }
}