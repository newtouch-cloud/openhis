using Microsoft.Data.SqlClient;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Base.DomainService;
using NewtouchHIS.Domain.IDomainService.EMR;
using NewtouchHIS.Domain.InterfaceObjets.EMR;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Exceptions;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NewtouchHIS.Domain.Enum.MrqcEnum;

namespace NewtouchHIS.DomainService.EMR
{
    public class MedicalApplyHandleDmnService: BaseServices<MedicalApplyApproveVo>, IMedicalApplyHandleDmnService
    {
        public async Task<bool> ApplyApprove(MedicalApplyApproveVo vo,string orgId, DBEnum db = DBEnum.EmrDb)
        {
            string errorMsg = string.Empty;
            if (!ValidRequired(vo, out errorMsg))
            {
                throw new FailedException(errorMsg);
            }
            string sqlstr = @" update Mr_BlApplyRecord set  applyStatus=@status ,ApprovePerson=@spr,ApproveDept=@dept,ApproveDate=@sprq
                            where zt='1' and OrganizeId=@OrganizeId and applyReturnNo in(select col from dbo.f_split(@ApplyNos,',')) ";
            IList<SugarParameter> parameters = new List<SugarParameter>();
            parameters.Add(new SugarParameter("@ApplyNos", vo.ApplyNos));
            parameters.Add(new SugarParameter("@OrganizeId", orgId));
            parameters.Add(new SugarParameter("@spr", vo.ApprovePerson));
            parameters.Add(new SugarParameter("@dept", vo.ApproveDept));
            parameters.Add(new SugarParameter("@sprq", DateTime.Now));
            parameters.Add(new SugarParameter("@status", vo.ApproveStatus));
            return await baseDal.ExecuteCommandSql(db.ToString(), sqlstr, parameters.ToArray());
        }
    }
}
