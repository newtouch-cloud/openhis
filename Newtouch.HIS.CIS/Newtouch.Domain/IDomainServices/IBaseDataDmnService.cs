using System.Collections.Generic;
using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ViewModels;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// 基础信息
    /// </summary>
    public interface IBaseDataDmnService
    {
        /// <summary>
        /// 药品用法 检索
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysMedicineUsageVEntity> GetMedicineUsageList();
        /// <summary>
        /// 代煎方式
        /// </summary>
        /// <returns></returns>
        IList<SysAuxiliaryDictionaryEntity> GetDjfsUsageList(string OrganizeId);
        /// <summary>
        /// 获取中药代煎费
        /// </summary>
        /// <returns></returns>
        decimal? GetDjFee(string orgId);

        /// <summary>
        /// 部位大类
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<GetSysBodyPartsVO> GetSysBodyBwFl(string orgId);
        /// <summary>
        /// 检查部位大类
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<GetSysBodyPartsVO> GetSysBodyJcBwFl(string orgId);

        /// <summary>
        /// 获取系统部位
        /// </summary>
        /// <returns></returns>
        IList<GetSysBodyPartsVO> GetSysBodyParts(string orgId);


        /// <summary>
        /// 获取系统部位
        /// </summary>
        /// <returns></returns>
        IList<GetYxBodyPartsVO> GetYxBodyParts(string orgId);

        /// <summary>
        /// 获取影像方法
        /// </summary>
        /// <returns></returns>
        IList<string> GetYxBodyMethod(string orgId, string jcbw);

        IList<GetSysJcBodyPartsVo> GetJcListByOrg(string orgId);
        /// <summary>
        /// 获取系统嘱托
        /// </summary>
        /// <returns></returns>
        IList<SysDoctorRemarkVO2> GetSysDoctorRemark(string orgId, string ksCode);

        /// <summary>
        /// 获取医生科室
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ysgh"></param>
        /// <returns></returns>
        string GetDoctorDepartCode(string orgId, string ysgh);

        void UpdatebrxzInfo(string orgId, string mzh, string zyh, string brxzCode, string brxzmc);

        /// <summary>
        /// 获取病人基本信息
        /// </summary>
        /// <param name="blh">病历号</param>
        /// <param name="mzh">门诊号</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SysPatientBasicInfoVO> SelectXtBrjbxx(string blh, string mzh, string organizeId);

        /// <summary>
        /// 获取病人卡信息
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SysPatientCardDetail> SelectCardDetail(string blh, string organizeId);
        /// <summary>
        /// 科室病区关系列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<SysDeptWardRelVO> GetDeptWardRel(string orgId, string ks, string bq);

		/// <summary>
		/// 获取医生开药权限
		/// </summary>
		/// <param name="orgid"></param>
		/// <param name="gh"></param>
		/// <param name="ypzl"></param>
		/// <returns></returns>
		 int GetPermissions(string orgid, string gh, string tsypzl, string dlcode, string kssqxjb);
	}
}
