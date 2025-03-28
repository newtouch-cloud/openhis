using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 同步治疗记录
    /// </summary>
    public class SyncTreatmentServiceRecordDmnService : DmnServiceBase, ISyncTreatmentServiceRecordDmnService
    {
        public SyncTreatmentServiceRecordDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取同步治疗记录
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="clzt"></param>
        /// <param name="mzh"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<SyncTreatmentServiceRecordVO> GetList(string orgId, int? clzt = 1, bool? wtjlbz = false, string mzh = "", string zyh = "", string zlsgh = "")
        {
            if (string.IsNullOrWhiteSpace(mzh) && string.IsNullOrWhiteSpace(zyh))
            {
                return null;
            }
            var sql = @"
select tr.*
,CASE tr.disciplineTrack WHEN 'PT' THEN 'RTM_PT' WHEN 'OT' THEN 'RTM_OT' WHEN 'ST' THEN 'RTM_ST' WHEN 'TM' THEN 'RTM_STM' ELSE tr.disciplineTrack END kflb
, staff.DepartmentCode ks  , therapistIdDept.Name ksmc
, sfdl.dlCode sfdlCode  , sfdl.dlmc sfdlmc
,ISNULL(sfxm.dj, 0.0000) dj, sfxm.dw dw
, staff.Id StaffId,staff.Name StaffName
from TB_Sync_TreatmentServiceRecord(nolock) tr
left join [NewtouchHIS_Base]..V_S_xt_sfxm sfxm
on sfxm.sfxmCode = tr.serviceCode and sfxm.OrganizeId = tr.siteId
left join [NewtouchHIS_Base]..V_S_xt_sfdl sfdl
on sfdl.dlCode = sfxm.sfdlCode and sfdl.OrganizeId = sfxm.OrganizeId
left join [NewtouchHIS_Base]..V_S_Sys_Staff staff
on staff.gh = tr.therapistId and staff.OrganizeId = tr.siteId
left join [NewtouchHIS_Base]..V_S_Sys_Department therapistIdDept
on therapistIdDept.Code = staff.DepartmentCode and therapistIdDept.OrganizeId = staff.OrganizeId

where siteId = @orgId and tr.zt = '1' and isDeleted='0'";

            sql += "";

            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            if (clzt.HasValue)
            {
                sql += " and clzt = @clzt";
                pars.Add(new SqlParameter("@clzt", clzt));
            }
            if (!string.IsNullOrWhiteSpace(mzh))
            {
                sql += " and LOWER(patientType) = 'outpatient' and admsNum = @mzh";
                pars.Add(new SqlParameter("@mzh", mzh));
            }
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                sql += " and LOWER(patientType) = 'inpatient' and admsNum = @zyh";
                pars.Add(new SqlParameter("@zyh", zyh));
            }
            if (!string.IsNullOrWhiteSpace(zlsgh))
            {
                sql += " and staff.gh = @zlsgh";
                pars.Add(new SqlParameter("@zlsgh", zlsgh));
            }
            if (wtjlbz.HasValue)
            {
                if (wtjlbz.Value)
                {
                    //true 仅问题记录
                    sql += " and isnull(wtjlbz,0) = 1";
                }
                else
                {
                    //false 排除问题记录
                    sql += " and isnull(wtjlbz,0) = 0";
                }
            }

            //确保一下 没有确认过
            sql += " and jfbId is null";

            sql += " order by isnull(serviceDate,CreatedDate)";

            return FindList<SyncTreatmentServiceRecordVO>(sql, pars.ToArray());
        }

    }
}
