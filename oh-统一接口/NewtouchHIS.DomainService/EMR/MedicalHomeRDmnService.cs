using Microsoft.Data.SqlClient;
using NewtouchHIS.Base.DomainService;
using NewtouchHIS.Domain.Entity.EMR;
using NewtouchHIS.Domain.IDomainService.EMR;
using NewtouchHIS.Domain.InterfaceObjets; 
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model.DRG;
using System.Data.Common;

namespace NewtouchHIS.DomainService.EMR
{
    /// <summary>
    /// 病案首页查询服务
    /// </summary>
    public class MedicalHomeRDmnService : BaseServices<MedicalHomeVO>, IMedicalHomeRDmnService
    {
        /// <summary>
        /// 查询患者病案首页重点信息（with：诊断、手术）
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        /// <exception cref="FailedException"></exception>
        public async Task<MedicalHomeVO> MedicalHomeQuery(string zyh, string orgId, DBEnum db = DBEnum.EmrDb)
        {
            //var data =await getJoinList<MedicalHomeRecordEntity, MedicalHomeRecordRelEntity>(DBEnum.MrmsDb.ToString(),(a, b) => new JoinQueryInfos(JoinType.Left, a.BAH == b.BAH), (a, b) => new { basy = a.ZYH, basyrel = b.SYId }, true, (a, b) => a.ZYH == zyh && a.OrganizeId == b.OrganizeId);
            var homeData = await baseDal.GetListBySqlQuery<MedicalHomeVO>(db.ToString(), @"select * from mr_basy with(nolock) where zyh=@zyh and organizeid=@orgId and zt='1' ", new List<DbParameter> {
                new SqlParameter("@zyh",zyh),
                new SqlParameter("@orgId",orgId),
            });
            if (homeData.Count == 0)
            {
                throw new FailedException($"未找到该患者病案首页信息【{db.GetDescription()}】");
            }
            MedicalHomeVO home = homeData.FirstOrDefault() ?? new MedicalHomeVO();
            var zdData = await baseDal.GetByWhere<MedicalHomeDiagEntity>(db.ToString(), p => p.ZYH == zyh && p.OrganizeId == orgId && p.zt == "1");
            if (zdData != null && zdData.Count > 0)
            {
                home.DiagList = zdData.OrderBy(p => p.ZDOrder).ToList();
            }
            var ssData = await baseDal.GetByWhere<MedicalHomeOperationEntity>(db.ToString(), p => p.ZYH == zyh && p.OrganizeId == orgId && p.zt == "1");
            if (ssData != null && ssData.Count > 0)
            {
                home.OperationList = ssData;
            }
            return home;
        }

        public async Task<DrgMedicalRecord> MedicalHomeRecordFormtDrg(string zyh, string orgId, DBEnum db = DBEnum.EmrDb)
        {
            var homeData = await baseDal.GetListBySqlQuery<DrgMedicalRecord>(db.ToString(), @"select  BAH  [Index],XB gender,NL age,BZYZSNL ageDay,XSECSTZ [weight],CYKB dept,floor(SJZYTS) inHospitalTime,LYFS leavingType from mr_basy with(nolock) where zyh=@zyh and organizeid=@orgId and zt='1' ", new List<DbParameter> {
                new SqlParameter("@zyh",zyh),
                new SqlParameter("@orgId",orgId),
            });
            if (homeData.Count == 0)
            {
                throw new FailedException($"未找到该患者病案首页信息【{db.GetDescription()}】");
            }
            DrgMedicalRecord drgMedical = homeData.FirstOrDefault() ?? new DrgMedicalRecord();
            var zdData = await baseDal.GetByWhere<MedicalHomeDiagEntity>(db.ToString(), p => p.ZYH == zyh && p.OrganizeId == orgId && p.zt == "1");
            if (zdData != null && zdData.Count > 0)
            {
                drgMedical.zdList = zdData.OrderBy(p => p.ZDOrder).Select(p => p.JBDM).ToArray();
            }
            var ssData = await baseDal.GetByWhere<MedicalHomeOperationEntity>(db.ToString(), p => p.ZYH == zyh && p.OrganizeId == orgId && p.zt == "1");
            if (ssData != null && ssData.Count > 0)
            {
                drgMedical.ssList = ssData.OrderBy(p => p.SSOrder).Select(p => p.SSJCZBM).ToArray();
            }
            return drgMedical;
        }
    }
}
