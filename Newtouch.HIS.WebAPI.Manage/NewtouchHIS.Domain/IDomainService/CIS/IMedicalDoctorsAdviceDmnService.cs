using Microsoft.Data.SqlClient;
using NewtouchHIS.Domain.InterfaceObjets.CIS;
using NewtouchHIS.Domain.InterfaceObjets.EMR;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.EnumExtend;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.IDomainService.CIS
{
    public interface IMedicalDoctorsAdviceDmnService : IScopedDependency
    {
        /// <summary>
        /// 住院医嘱
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="yzlx"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<List<MedicalDoctorsAdviceVo>> MedicalDoctorsAdviceRecord(string zyh, string orgId, string yzlx, DBEnum db = DBEnum.CisDb);
        /// <summary>
        /// 门诊住院检验检查申请单
        /// </summary>
        /// <param name="jzh"></param>
        /// <param name="orgId"></param>
        /// <param name="reportType"></param>
        /// <param name="mzzybz"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<List<MedicalInspectionExaminationVo>> MedicalJyjcRecord(string jzh, string orgId, string reportType, string mzzybz, DateTime ksrq, DateTime jsrq, DBEnum db = DBEnum.CisDb);
    }
}
