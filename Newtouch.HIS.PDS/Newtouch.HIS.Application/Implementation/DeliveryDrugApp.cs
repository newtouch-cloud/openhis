using System;
using System.Threading;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Repository;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.Log;
using Newtouch.PDS.Requset;
using Newtouch.Tools;
using static Newtouch.Common.Web.APIRequestHelper;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 发药
    /// </summary>
    public class DeliveryDrugApp : ProcessorFun<string>
    {
        private readonly IfyDmnService _fyDmnService;
        private readonly IMzCfRepo _mzCfRepo;

        public DeliveryDrugApp(string request) : base(request)
        {
            Tags.Add(Constants.Cfh, request);
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (string.IsNullOrWhiteSpace(Request)) throw new FailedException("处方号不能为空");
            return new ActResult();
        }

        /// <summary>
        /// 主处理
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            var result = _fyDmnService.ExecMzHandoutMedicine(Request, Constants.CurrentYfbm.yfbmCode, OperatorProvider.GetCurrent().UserCode, OrganizeId);
            if (string.IsNullOrWhiteSpace(result)) return;
            actResult.IsSucceed = false;
            actResult.ResultMsg = result;
        }

        protected override void AfterAction(ActResult actResult)
        {
            var userCode = OperatorProvider.GetCurrent().UserCode;
            Task.Run(() =>
            {
                Thread.CurrentThread.IsBackground = false;
                var cfEntity = new MzCfRepo(new DefaultDatabaseFactory()).FindEntity(p => p.cfh == Request);
                Tags.Add(Constants.Cfnm, cfEntity.cfnm.ToString());
                var ztObj = new
                {
                    cfnm = cfEntity.cfnm,
                    user_code = userCode,
                    TimeStamp = DateTime.Now
                };
                var apiResp = SiteSettAPIHelper.Request<object, DefaultResponse>("api/OutpatientPharmacy/UpdatefyztByFY", ztObj);
                LogCore.Info("UpdatefyztByFY", string.Format("门诊发药同步发药状态; \n request:{0} \n response:{1} ; ", ztObj.ToJson(), apiResp.ToJson()));
            });
        }
    }
}
