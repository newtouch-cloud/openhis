using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysPatientNatureRepo : IRepositoryBase<SysPatientNatureEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<SysPatientNatureEntity> GetList(string orgId);

        /// <summary>
        /// 根据病人性质名称获取病人性质信息
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="ogrId"></param>
        /// <returns></returns>
        List<SysPatientNatureEntity> GetbxzcBySearch(string keyword, string ogrId);

        /// <summary>
        /// 获取报销政策下拉框（自动提示）
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        string GetbxzcSelect(string keyword, string orgId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string getEffectPatiNatureList(string orgId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brxzbh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SysPatientNatureEntity SelectBrxzByBrxzbh(int brxzbh, string orgId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brxz"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SysPatientNatureEntity SelectBrxzByBrxz(string brxz, string orgId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysPatientNatureEntity GetForm(string keyValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysPatientNatureEntity entity, string keyValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brxzbh"></param>
        /// <param name="orgId"></param>
        void DeleteForm(int brxzbh, string orgId);
    }
}
