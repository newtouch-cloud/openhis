using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application.Interface.SystemManage
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysDepartmentApp
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysDepartmentEntity GetForm(string keyValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SysDepartmentEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysDepartmentEntity SysDepartmentEntity, string keyValue);

    }
}
