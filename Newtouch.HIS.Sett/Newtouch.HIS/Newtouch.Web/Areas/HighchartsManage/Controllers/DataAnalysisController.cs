using Newtouch.Common;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.IDomainServices.ReportManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.ReportManage.Controllers
{
    public class DataAnalysisController : ControllerBase
    {
        private readonly IDataAnalysisDmnService _dataAnalysisDmnService;
        public DataAnalysisController(IDataAnalysisDmnService dataAnalysisDmnService)
        {
            _dataAnalysisDmnService = dataAnalysisDmnService;
        }
    }
}