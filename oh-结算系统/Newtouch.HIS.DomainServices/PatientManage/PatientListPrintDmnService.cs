using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Newtouch.HIS.Domain.DTO.PrintDto;
using Newtouch.HIS.Domain.IDomainServices.PatientManage;
using Newtouch.HIS.Domain.ReportTemplateVO;
using Newtouch.Infrastructure;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.HIS.DomainServices.PatientManage
{
    public class PatientListPrintDmnService : DmnServiceBase, IPatientListPrintDmnService
    {
        public PatientListPrintDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取患者列表
        /// </summary>
        /// <returns></returns>
        public List<PatientListOutPutVO> PatientList(PatientListInputVO patientListInputVO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"EXEC sp_xt_zycyhzlb @begingdate=@begingdate,@enddate=@enddate,@flag=@flag,@sort=sort,@adsc=@adsc");
            SqlParameter[] par = {
                       new SqlParameter("@begingdate",patientListInputVO.BeginDate),
                        new SqlParameter("@enddate",patientListInputVO.EndDate),
                         new SqlParameter("@flag",patientListInputVO.flag),
                          new SqlParameter("@sort",patientListInputVO.sort),
                          new SqlParameter("@adsc",patientListInputVO.adsc)
            };
            return this.FindList<PatientListOutPutVO>(strSql.ToString(), par);
        }
    }
}
