using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.PatientManage;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 病人病历DmnService
    /// </summary>
    public interface ISysPatientMedicalRecordDmnService
    {
        /// <summary>
        /// 获取病历
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="blId"></param>
        /// <returns></returns>
        IList<SysPatientMedicalRecordDTO> GetMedicalRecordList(string orgId, string blh, string blId = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        SysPatientMedicalRecordEntity GetMedicalRecordById(string Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        IList<SysPatientMedicalRecordDetailEntity> GetMedicalRecordDetailListByMainId(string Id);

        void SubmitForm(string keyValue, string blh
            , DateTime rq
            , string zt, string bz, IList<string> delDetailIdList, List<SysPatientMedicalRecordDetailEntity> addDetailEntityList,
            List<SysPatientMedicalRecordDetailEntity> updateDetailEntityList
            , string orgId);

        /// <summary>
        /// 卫健数据上传日志
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="tabname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<HealthyUploadVo> GetHealthUpload(Pagination pagination, DateTime ksrq, DateTime jsrq, string tabname, string orgId);
    }
}
