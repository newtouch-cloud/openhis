using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.IDomainServices.ReportManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Newtouch.HIS.DomainServices.ReportManage
{
    public class DataAnalysisDmnService : DmnServiceBase, IDataAnalysisDmnService
    {
        public DataAnalysisDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

    }
}
