using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.EMR.Domain.DTO;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using Newtouch.EMR.Domain.BusinessObjects;

namespace Newtouch.EMR.Domain.IDomainServices
{
    public interface IZybrjbxxDmnService
    {

        void Sync_HisPatinfo(DateTime lastUpdate, OperatorModel user);
        void Sync_HisPatinfo(string OrgId, DateTime Bdate, DateTime Edate);
        List<PatientzbqResponseDto> GetzbqPatientList(PatientzbqRequestDto req,string OrgId);
        List<PatientycqResponseDto> GetycqPatientList(PatientycqRequestDto req, string OrgId);
        List<PatientmyResponseDto> GetmyPatientList(PatientmyRequestDto req, string OrgId);
        /// <summary>
        /// 根据关键字获取患者信息
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<PatientmyResponseDto> GetPatInfoBykeyword(string keyword, string orgId);
        /// <summary>
        /// 获取患者病历树
        /// </summary>
        /// <param name="OrgId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<PatMedRecordTreeVO> GetPatMedRecordTree(string OrgId, string zyh, string rygh);
        /// <summary>
        /// 获取病历字典
        /// </summary>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        IList<MedRecordTypeVO> GetSysItemDic(string OrgId, string Code,string bllxId);
        /// <summary>
        /// 患者信息列表(在院/出院)
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="zyh"></param>
        /// <param name="OrgId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        IList<PatListVO> GetPatList(Pagination pagination, string keyword, string zyh, string cyts, string blzt, string ysgh, string OrgId, int type, string bq = null, string appId = null);
        /// <summary>
        /// 医生患者列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="ysgh"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        IList<PatListVO> GetMyPatList(Pagination pagination, string keyword, string ysgh, string OrgId);

        void UpdtPatMedRecord(string blId, string blgxId, string OrgId, OperatorModel user);
        /// <summary>
        /// 获取病案首页患者基本信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        BabasyVO GetPatBasicInfo_basy(string orgId, string zyh);

        string GetCwNameByCode(string cw,string orgId,string bq);

        string selectBJQX(string blId, string blgxId, string OrganizeId, OperatorModel user);
    }

}
