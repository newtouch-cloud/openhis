using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.MRQC.Domain.IDomainServices.Apply;
using Newtouch.MRQC.Domain.ValueObjects.Apply;
using Newtouch.MRQC.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.DomainServices.Apply
{
    public class MrApplyDmnService : DmnServiceBase, IMrApplyDmnService
    {
        public MrApplyDmnService(IDefaultDatabaseFactory databaseFactory)
           : base(databaseFactory)
        {
        }


        public IList<MedicalApplyVo> GetBlApplyList(Pagination pagination, string OrganizeId, string status,DateTime ksrq,DateTime jsrq)
        {
            string sql = @" select apply.Id,ApplyDept,dept.Name ApplyDeptName,Bed,PatName,ApplyDoctor,staff.Name ApplyDoctorName
,ApplyType,MedicalName,CompletionDate,ApplyDate,ApplyCompletionDate,ApplyReason,ApplyStatus
,Approver,staff1.Name ApproverName,ApproverDept,dept1.Name ApproverDeptName,ApprovalDate
,Ryrq,Cyrq
from [Mr_Apply] apply
left join NewtouchHIS_Base..V_S_Sys_Staff staff 
	on apply.ApplyDoctor=staff.gh and apply.OrganizeId=staff.OrganizeId
left join NewtouchHIS_Base..V_S_Sys_Staff staff1 
    on apply.Approver=staff1.gh and apply.OrganizeId=staff1.OrganizeId
left join NewtouchHIS_Base..V_S_Sys_Department dept 
	on apply.ApplyDept=dept.Code and apply.OrganizeId=dept.OrganizeId
left join NewtouchHIS_Base..V_S_Sys_Department dept1 
	on apply.ApproverDept=dept1.Code and apply.OrganizeId=dept1.OrganizeId
where apply.zt='1' and apply.OrganizeId=@OrgId and ApplyDate>=@ksrq and ApplyDate<=@jsrq 
";
            var para = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(status))
            {
                sql += " and ApplyStatus=@status ";
                para.Add(new SqlParameter("@status", status));
            }
            para.Add(new SqlParameter("@OrgId", OrganizeId));
            para.Add(new SqlParameter("@ksrq", ksrq));
            para.Add(new SqlParameter("@jsrq", jsrq));
            return this.QueryWithPage<MedicalApplyVo>(sql, pagination, para.ToArray()).ToList();
        }
        public void UpdateApplyStatus(string shbhlist,string OrganizeId, string Deptcode, string Gh)
        {
            string sql = @" update Mr_Apply set ApplyStatus=@status ,Approver=@spr,ApproverDept=@dept,ApprovalDate=@sprq
                            where zt='1' and OrganizeId=@OrganizeId and id in(select col from dbo.f_split(@shbhlist,',')) ";
            IList<SqlParameter> inSqlParameterList = new List<SqlParameter>();
            inSqlParameterList.Add(new SqlParameter("@shbhlist", shbhlist));
            inSqlParameterList.Add(new SqlParameter("@OrganizeId", OrganizeId));
            inSqlParameterList.Add(new SqlParameter("@spr", Gh));
            inSqlParameterList.Add(new SqlParameter("@dept", Deptcode));
            inSqlParameterList.Add(new SqlParameter("@sprq", DateTime.Now));
            inSqlParameterList.Add(new SqlParameter("@status",(int)ApplyStatusEnum.ysp));
            int cnt= this.ExecuteSqlCommand(sql, inSqlParameterList.ToArray());
            if (cnt==0)
            {
                throw new FailedException("审批失败,未找到待审批申请!");
            }
        }
    }
}
