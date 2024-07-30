using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects.Outpatient;

namespace Newtouch.Domain.IDomainServices
{
    public interface IAllergyManageDmnService
    {
        /// <summary>
        /// 门诊病人筛选
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="ks"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<OutHosPatientVO> GetOutPatientPaginationList(Pagination pagination, string keyword, string ks, string kssj, string jssj, string orgId, string PsItemCode, string Status);

        /// <summary>
        /// 住院病人筛选
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="ks"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<InHosPatientVO> GetInPatientPaginationList(Pagination pagination, string keyword, string ks, string kssj, string jssj, string orgId, string PsItemCode, string Status);


        /// <summary>
        /// 保存患者过敏信息
        /// </summary>
        /// <param name="entity"></param>
        void SaveAllergyInfo(AllergyEntity entity);

        /// <summary>
        /// 删除执行过敏信息
        /// </summary>
        /// <param name="entity"></param>
        int DeleteAllergyInfo(string gmxxId, string orgId, string UserCode, string UserName);

        /// <summary>
        /// 获取门诊患者开的皮试项目
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="Id"></param>
        /// <param name="orgId"></param>
        /// <param name="mzzybz">门诊住院标志 1门诊 2住院</param>
        /// <returns></returns>
        IList<PsItemVO> GetMzPatientPsItemsList(Pagination pagination, string Id, string orgId, string PsItemCode);

        /// <summary>
        /// 获取住院患者开的皮试项目
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="Id"></param>
        /// <param name="orgId"></param>
        /// <param name="mzzybz">门诊住院标志 1门诊 2住院</param>
        /// <returns></returns>
        IList<PsItemVO> GetZyPatientPsItemsList(Pagination pagination, string zyh, string orgId, string PsItemCode);

        /// <summary>
        /// 获取患者过敏信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<PatientGmxxDetailVO> GetPatientGmxxList(Pagination pagination, string keyword, string orgId);

        /// <summary>
        /// 获取抗生素药品信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<KssYpVO> GetKssYpListData(string orgId);

        /// <summary>
        /// 获取抗生素类别
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<KssYpLbVO> GetKssYpDlData(string ypCode, string orgId);

        string GetPsItemsSetting(string PsItemCode);
    }
}