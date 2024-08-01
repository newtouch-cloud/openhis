using FrameworkBase.MultiOrg.Domain.DTO;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.ValueObjects;
using System.Collections.Generic;

namespace FrameworkBase.MultiOrg.Domain.IDomainServices
{
    /// <summary>
    /// 基础数据
    /// </summary>
    public interface IBaseDataDmnService
    {
        /// <summary>
        /// 检索药品项目
        /// </summary>
        /// <param name="reqDto"></param>
        /// <returns></returns>
        IList<SfxmYpSelectResultVO> SelectSfxmYp(SelectSfxmYpFilterDTO reqDto);

        /// <summary>
        /// 根据人员工号检索权限内病区
        /// </summary>
        /// <param name="gh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysWardVO> GetWardListByStaffGh(string gh, string orgId);

        /// <summary>
        /// 获取药品用法列表
        /// </summary>
        /// <returns></returns>
        IList<SysMedicineUsageVEntity> GetMediUsageList();

        /// <summary>
        /// 获取药品剂型用法对照关系
        /// </summary>
        /// <returns></returns>
        IList<SysMedicineFormulationUsageVEntity> GetMediFormlUsageList();

        /// <summary>
        /// 医嘱频次 检索
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysMedicalOrderFrequencyVEntity> GetOrderFrequencyList(string orgId);

        /// <summary>
        /// 获取收费项目的执行科室列表xt_sfxm_zxks（未绑定时返回所有科室）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sfxmCode"></param>
        /// <returns></returns>
        IList<SysDepartmentVEntity> GetSfxmZxksList(string orgId, string sfxmCode);

        /// <summary>
        /// 查询药品详情
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        SysMedicineComplexVEntity GetYpDetails(string orgId, string ypCode);

        /// <summary>
        /// 获取有效民族List
        /// </summary>
        /// <returns></returns>
        IList<SysNationVEntity> GetmzList();

        /// <summary>
        /// 获取所有收费大类
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysChargeCategoryVEntity> SelectSfdl(string organizeId);

        /// <summary>
        /// 获取所有收费项目
        /// </summary>
        /// <param name="sfdlCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysChargeItemVEntity> SelectSfxm(string sfdlCode, string organizeId);
    }
}
