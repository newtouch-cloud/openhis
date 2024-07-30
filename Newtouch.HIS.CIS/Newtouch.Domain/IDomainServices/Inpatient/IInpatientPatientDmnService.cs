using Newtouch.Core.Common;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    public interface IInpatientPatientDmnService
    {
        /// <summary>
        /// 获取在病区的病人对象集合
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<PatientzbqResponseDto> GetzbqPatientList(PatientzbqRequestDto req, string OrganizeId);
        /// <summary>
        /// 获取已出区的病人对象集合
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<PatientycqResponseDto> GetycqPatientList(PatientycqRequestDto req, string OrganizeId);

        /// <summary>
        /// 住院病人筛选
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="bq"></param>
        /// <param name="zyh"></param>
        /// <param name="xm"></param>
        /// <param name="zybz">在院标志，多个用逗号分割</param>
        /// <returns></returns>
        IList<InPatientPatientSearchVO> GetInPatSearchPaginationList(Pagination pagination, string orgId, string bq, string zyh, string xm, string bqCode,string zybz = null);

        /// <summary>
        /// 住院号、姓名、卡号获取病人基本信息 姓名，性别，年龄，病人性质、入院日期
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<patientInfoDto> GetInfoBykeyword(string keyword, string orgId);

        /// <summary>
        /// 住院号获取病人基本信息 姓名，性别，年龄，病人性质
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        patientInfoDto GetInfoByZyh(string zyh, string orgId);

        /// <summary>
        /// 住院病人筛选
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="bq"></param>
        /// <param name="zyh"></param>
        /// <param name="xm"></param>
        /// <param name="zybz">在院标志，多个用逗号分割</param>
        /// <returns></returns>
        IList<InPatientNursingInputVO> GetInPatSearchList(string orgId, string bq, DateTime rq, string sj, string zyh, string zybz = null);
        IList<InpWardPatTreeVO> GetPatTree(string orgId, string zyzt, string keyword);
        /// <summary>
        /// 住院多病人护理录入
        /// </summary>
        /// <param name="entity"></param>
        void submitmutiple(List<InpatientVitalSignsEntity> entity, string orgId, int? sjd, DateTime rq,string flag);
        InpatientBasicInfoVO GetInpatientBasicInfo(string zyh, string orgId, string zhxz);
		List<String> GetWardCodeByStaffId(string StaffId, string orgId);
        InpWardPatTreeVO GetPatList(string orgId, string zyh);

        /// <summary>
        /// 医嘱管理中的费用按钮查询
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        PatMedInsurSettVO PreSettbyId(string zyh, string orgId);
        List<PatQfWarnVo> PatYjjWarn(string patzyh, string orgId);

		RefSuccess inserghjz(jzdjbrxx brxx);

	}
}