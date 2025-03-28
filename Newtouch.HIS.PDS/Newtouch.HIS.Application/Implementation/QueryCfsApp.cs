using System;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.PDS.Requset;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 获取待排药的处方
    /// </summary>
    public class QueryCfsApp : ProcessorFun<string>
    {
        private readonly IMzCfRepo mzCfRepo;
        private readonly ISysConfigRepo _sysConfigRepo;

        public QueryCfsApp(string request) : base(request)
        {
        }

        /// <summary>
        /// 主处理
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            var ago = -7;
            var td = ConfigurationHelper.GetAppConfigValue("pyCfBeforDaysAgo");
            int.TryParse(td, out ago);
            var cfList = mzCfRepo.GetNoArrangedCfList(DateTime.Now.AddDays(ago), DateTime.Now);
            if (cfList != null && cfList.Count > 0)
            {
                actResult.Data = cfList;
            }
            else
            {
                actResult.IsSucceed = false;
                actResult.ResultMsg = "未能获取到未排班的处方，请同步处方！";
            }
        }
    }
}
