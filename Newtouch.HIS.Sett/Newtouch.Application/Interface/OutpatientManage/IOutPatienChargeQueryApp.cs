using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using System;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOutPatienChargeQueryApp
    {
        /// <summary>
        /// 获取门诊挂号收费详情
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        OutPatientRegChargeDetailDto GetRecordsByJsnm(string jsnm);

        /// <summary>
        /// 获取门诊挂号收费详情
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        OutPatientRegChargeDetailDto ChargeRecordsQuery(string jsnm);

        IList<OutPatientReprintOrSuppPrintSettleVO> LoadMzjsRecords(Pagination pagination, string jsnm, DateTime? startDate, DateTime? endDate, string kh, string yfph);

        OutPatientReprintOrSuppPrintSettleDetailDto LoadMzjsMXRecords(string jsnmStr);

        void PrintMxData(string jsnmStr);

        void CheckPintInfo(string jsnm);

        void PrintInvoice(string jsnm, bool isGH);

        void RePrint(string jsnm, string pageFph, bool isGH);

        void SupplementPrint(string jsnm, bool isGH);


    }
}
