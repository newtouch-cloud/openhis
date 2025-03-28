using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysDepartmentRepository : IRepositoryBase<SysDepartmentEntity>
    {
        /// <summary>
        /// 获取科室列表
        /// </summary>
        /// <returns></returns>
        List<SysDepartmentEntity> GetListByTopOrg(string topOrganizeId);

        /// <summary>
        /// 获取科室列表
        /// </summary>
        /// <returns></returns>
        List<SysDepartmentEntity> GetListByOrg(string organizeId);

        /// <summary>
        /// 获取当前组织下的所有有效科室
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysDepartmentEntity> GetValidListByOrg(string organizeId);

        /// <summary>
        /// 获取组织 的 有效科室
        /// </summary>
        /// <returns></returns>
        IList<SysDepartmentEntity> GetValidListByTopOrg(string topOrganizeId);

        /// <summary>
        /// 根据Code获取名称
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetNameByCode(string code, string orgId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SysDepartmentEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysDepartmentEntity SysDepartmentEntity, string keyValue);
        /// <summary>
        /// 更新科室医保上传状态
        /// </summary>
        /// <param name="uploadYB"></param>
        /// <param name="id"></param>
        void UpdateYbUpload(int uploadYB, string id);
        /// <summary>
        /// 根据查询条件获取有效科室列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysDepartmentEntity> GetDeptListByKeyValue(string orgId, string keyValue);

    }
}
