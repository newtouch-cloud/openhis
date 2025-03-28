using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using Newtouch.Common.Web;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Sett.Request;
using Newtouch.HIS.Sett.Request.OutPatientPharmacy;
using Newtouch.HIS.Sett.Request.Patient;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.Log;
using Newtouch.PDS.Requset;
using Newtouch.Tools;
using static Newtouch.Common.Web.APIRequestHelper;

namespace Newtouch.HIS.Application.Implementation.Process
{
    /// <summary>
    /// 门诊退药
    /// </summary>
    public class OutpatientReturnDrugsProcess : ProcessorFun<tyInfo>
    {
        private readonly ItyDmnService _tyDmnService;

        public OutpatientReturnDrugsProcess(tyInfo request) : base(request)
        {
        }

        protected override ActResult Validata()
        {
            if (Request == null || Request.tyDrugDetail == null || Request.tyDrugDetail.Count == 0)
            {
                throw new FailedException("请传入需要退药的药品");
            }

            return base.Validata();
        }

        protected override void BeforeAction(ActResult actResult)
        {
            Parallel.ForEach(Request.tyDrugDetail, p =>
            {
                if (p.sl <= 0)
                {
                    Request.tyDrugDetail.Remove(p);
                }
            });
            if (Request.tyDrugDetail.Count != 0)
            {
                return;
            }

            actResult.IsSucceed = false;
            actResult.ResultMsg = "退药数必须大于零";
        }

        protected override void Action(ActResult actResult)
        {
            var returnDrugBillNo = "";
            var tyResult = _tyDmnService.ReturnDrugSingleThread(Request, out returnDrugBillNo);
            if (string.IsNullOrWhiteSpace(tyResult))
            {
                actResult.Data = returnDrugBillNo;
                return;
            }
            actResult.IsSucceed = false;
            actResult.ResultMsg = tyResult;
        }

        protected override void AfterAction(ActResult actResult)
        {
            var cfxx = new MzCfRepo(new DefaultDatabaseFactory()).FindEntity(p =>
                p.cfh == Request.cfh && p.zt == "1" && p.OrganizeId == OrganizeId);
            if (cfxx == null)
            {
                return;
            }
            var errorMsg = new StringBuilder();
            Request.tyDrugDetail.ForEach(p =>
            {
                try
                {
                    var cfmx = new MzCfmxRepo(new DefaultDatabaseFactory()).FindEntity(d => d.cfh == Request.cfh && d.ypCode == p.ypCode && d.zt == "1" && d.OrganizeId == OrganizeId);
                    if (cfmx == null)
                    {
                        return;
                    }
                    var request = new
                    {
                        cfnm = (int)cfxx.cfnm,
                        yp = p.ypCode,
                        sl = (decimal)p.sl * p.zhyz / cfmx.zhyz,
                        czh = p.czh,
                        OrganizeId = Request.organizeId
                    };
                    var apiResp = SiteSettAPIHelper.Request<object, DefaultResponse>("api/OutpatientPharmacy/OutpatientDrugWithdrawalNotify", request);
                    if (apiResp == null || apiResp.data == null)
                    {
                        errorMsg.Append(string.Format("处方【{0}】门诊退药接口异常，请联系管理员；", p.cfh));
                    }
                }
                catch (Exception e)
                {
                    return;
                }
            });

        }
    }
}