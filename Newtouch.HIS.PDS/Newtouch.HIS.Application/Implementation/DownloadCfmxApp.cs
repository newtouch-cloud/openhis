using System;
using System.Collections.Generic;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.DTO.OutPatientPharmacy;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.PDS.Requset;
using Newtouch.Tools;
using static Newtouch.Common.Web.APIRequestHelper;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 落地处方明细
    /// </summary>
    public class DownloadCfmxApp : ProcessorFun<string>
    {

        private List<MzCfmxEntity> _mzCfmxEntities;
        private List<OutPatientpyCfmxDTO> _cfmx;
        private readonly IMzCfmxRepo _mzCfmxRepo;
        private readonly ISysMedicineReceiptDmnService _sysMedicineReceiptDmnService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        public DownloadCfmxApp(string request) : base(request)
        {
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (string.IsNullOrWhiteSpace(Request) || Request.ToLower().Equals("undefined")) throw new FailedException("处方号不能为空");
            return new ActResult();
        }

        /// <summary>
        /// 预处理
        /// </summary>
        protected override void BeforeAction(ActResult actResult)
        {
            //_mzCfmxEntities = _mzCfmxRepo.GetCfmxByCfh(Request.cfh);
            //if (_mzCfmxEntities == null || _mzCfmxEntities.Count <= 0)
            //{
            //    _cfmx = QueryCfmx();
            //}
           
            _cfmx = QueryCfmx();
        }

        /// <summary>
        /// 从结算系统获取处方明细
        /// </summary>
        /// <returns></returns>
        private List<OutPatientpyCfmxDTO> QueryCfmx()
        {
            var reqObj = new
            {
                cfh = Request,
                TimeStamp = DateTime.Now
            };
            var apiResp = SiteSettAPIHelper.Request<object,DefaultResponse>("api/OutpatientPharmacy/WaitingDispenseMedicineDetailQuery", reqObj);
            if (apiResp.code != ResponseResultCode.SUCCESS && apiResp.data == null)
            {
                throw new FailedException("从结算系统获取处方明细失败");
            }
            var cfmxs = apiResp.data.ToString().ToObject<List<OutPatientpyCfmxDTO>>();
            if (cfmxs != null && cfmxs.Count > 0)
            {
                return cfmxs;
            }
            throw new FailedException("从结算系统获取处方明细失败");
        }

        /// <summary>
        /// 主处理
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            AssembleMzCfmxEntityEntities();
            if (_mzCfmxEntities != null && _mzCfmxEntities.Count > 0 && _cfmx != null && _cfmx.Count > 0)
            {
                if (_mzCfmxRepo.Insert(_mzCfmxEntities) > 0) return;
                actResult.IsSucceed = false;
                actResult.ResultMsg = "保存处方明细失败";
            }
        }

        /// <summary>
        /// 组装处方明细
        /// </summary>
        private void AssembleMzCfmxEntityEntities()
        {
            //if (_mzCfmxEntities != null && _mzCfmxEntities.Count > 0) return;
            _mzCfmxEntities = new List<MzCfmxEntity>();
            _cfmx.ForEach(p =>
            {
                //该处方明细是否存在
                if (IsExist(p))
                {
                    _mzCfmxRepo.DeleteCfmx(Request);
                }
                //同步处方明细的时候，判断数量大于0的落地
                if (p.sl > 0)
                {
                    var zhyz = _sysMedicineReceiptDmnService.GetZhyz(OperatorProvider.GetCurrent(true).OrganizeId, Constants.CurrentYfbm.yfbmCode, p.yp);
                    if (zhyz == 0) throw new FailedException("药库药品【{0}】转换因子为0", p.ypmc);
                    _mzCfmxEntities.Add(new MzCfmxEntity
                    {
                        bz = p.yszt,
                        cfh = p.sffymxxh,
                        czh = p.czh,
                        dj = p.dj,
                        dw = p.dw,
                        gg = p.ypgg,
                        je = p.je,
                        jl = p.jl,
                        jldw = p.jldw,
                        sl = Convert.ToInt32(p.sl) * (int)p.zhyz / zhyz,
                        ycmc = p.ycmc,
                        yfmc = p.yfmc,
                        ypCode = p.yp,
                        ypmc = p.ypmc,
                        CreateTime = DateTime.Now,
                        CreatorCode = OperatorProvider.GetCurrent().UserCode,
                        OrganizeId = p.OrganizeId,
                        zhyz = (int)p.zhyz
                    });
                }               
            });
        }

        /// <summary>
        /// 判断是否存在该处方明细
        /// </summary>
        /// <param name="outPatientpyCfmxDto"></param>
        /// <returns></returns>
        private bool IsExist(OutPatientpyCfmxDTO outPatientpyCfmxDto)
        {
            return _mzCfmxRepo.IsExist(outPatientpyCfmxDto.sffymxxh, outPatientpyCfmxDto.yp, OperatorProvider.GetCurrent().OrganizeId, outPatientpyCfmxDto.ypgg);
        }

        /// <summary>
        /// 后处理
        /// </summary>
        protected override void AfterAction(ActResult actResult)
        {
            actResult.Data = _mzCfmxEntities;
        }
    }
}
