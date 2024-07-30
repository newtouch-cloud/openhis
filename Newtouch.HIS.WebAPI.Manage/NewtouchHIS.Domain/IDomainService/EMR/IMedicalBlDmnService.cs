using NewtouchHIS.Domain.Entity.EMR;
using NewtouchHIS.Domain.InterfaceObjets;
using NewtouchHIS.Domain.InterfaceObjets.EMR;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Model.DRG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.IDomainService.EMR
{
    public interface  IMedicalBlDmnService : IScopedDependency
    {
        /// <summary>
        /// 病历类型
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<MedicalBllxItemVo> MedicalbllxRecord(string orgId, DBEnum db = DBEnum.EmrDb);
        /// <summary>
        /// 病历模板
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<MedicalBlMbItemVo> MedicalblmbRecord(string orgId, DBEnum db = DBEnum.EmrDb);
        /// <summary>
        /// 病历类型树
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<MedicalBllxMbTreeVo> MedicalbllxTreeRecord(string orgId, DBEnum db = DBEnum.EmrDb);
        /// <summary>
        /// 住院病人列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<List<MedicalPatientVo>> MedicalCenterPatInfo(string brbz,DateTime ksrq,DateTime jsrq, string srz, string orgId, DBEnum db = DBEnum.EmrDb);
        /// <summary>
        /// 诊断列表
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<List<MedDiagListVo>> MedicalCenterDiaglist(string zyh, string orgId, DBEnum db = DBEnum.EmrDb);
        /// <summary>
        /// 病历文书tree
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<List<MedRecordTree>> MedicalCenterBlwsTree(string zyh, string orgId, DBEnum db = DBEnum.EmrDb);
    }
}
