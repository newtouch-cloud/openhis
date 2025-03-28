using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Infrastructure;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    public interface IInpatientOrderPackageDmnService
    {
        string saveAsTemplate(InpatientOrderPackageEntity mbObj, List<InpatientOrderPackageVO> mxList, string orgId);
        /// <summary>
        /// 根据模板ID找到模板内容
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<DoctorServiceUIRequestDto> GetMBDetailByMainId(string Id, string orgId);

        List<HistoricalOrdersRequestDto> GetHistoricalOrders(string zyh,string type,string kssj,string jssj,string cqorls, string orgId);
        /// <summary>
        /// 根据模板ID找到模板详情
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<DoctorServiceUIRequestDto> GetMBDetailByDetailId(string IdList, string orgId);
        List<HistoricalOrdersRequestDto> GetHistoricalOrdersById(string yzlistId, string orgId);
        List<PatientDto> GetPatientQuery(string zyh, string orgId);

        string GetHistoricalRegist(string zyh,string curzyh, string yzlistId, string orgId, string creatorcode);
        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="mbId"></param>
        void DeleteTemplate(string mbId, string orgId);
    }
}