using System;
using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Proxy.guian;
using Newtouch.HIS.Proxy.guian.DO;
using Newtouch.HIS.Proxy.guian.DTO.S18;
using Newtouch.HIS.Proxy.guian.DTO.S21;
using Newtouch.HIS.Proxy.guian.DTO.S22;
using Newtouch.HIS.Proxy.guian.DTO.S23;
using Newtouch.HIS.Proxy.guian.DTO.S24;

namespace Newtouch.HIS.Application.Implementation.OutpatientManage
{
    /// <summary>
    /// 新农合门诊模拟结算
    /// </summary>
    public class SimulationSettlementOverallProcess : ProcessorFun<SimulationSettlementOverallProcessDo>
    {
        private readonly ITTCataloguesComparisonDmnService _ttCataloguesComparisonDmnService;

        private List<TTCataloguesComparisonDetailEntity> allTTmllb;
        private S18RequestDTO _s18RequestDto;
        private S21RequestDTO _s21RequestDto;
        private S22RequestDTO _s22RequestDto;
        private S24RequestDTO _s24RequestDto;

        public SimulationSettlementOverallProcess(SimulationSettlementOverallProcessDo request) : base(request)
        {
        }

        protected override ActResult Validata()
        {
            if (Request == null) throw new FailedException("新农合虚拟结算内容不能为空");
            if (string.IsNullOrWhiteSpace(Request.OrganizeId)) throw new FailedException("组织结构不能为空");
            if (Request.S21Details == null) throw new FailedException("门诊费用明细上传不能为空");
            if (string.IsNullOrWhiteSpace(Request.outpId)) throw new FailedException("门诊补偿序号不能为空");
            if (Request.S22Param == null || string.IsNullOrWhiteSpace(Request.S22Param.startDate) || string.IsNullOrWhiteSpace(Request.S22Param.endDate)) throw new FailedException("查询门诊上传明细参数不能为空");
            allTTmllb = _ttCataloguesComparisonDmnService.GetTTItem(Request.OrganizeId, "mllb", "guianxinnonghe");
            if (allTTmllb == null || allTTmllb.Count <= 0) throw new FailedException("缺少类别对照");
            return base.Validata();
        }

        protected override void BeforeAction(ActResult actResult)
        {

            _s21RequestDto = new S21RequestDTO
            {
                outpId = Request.outpId,
                list = new List<detail>()
            };
            _s22RequestDto = new S22RequestDTO
            {
                startDate = Request.S22Param.startDate,
                endDate = Request.S22Param.endDate,
                outpId = Request.outpId
            };
            Request.S21Details.ForEach(p =>
            {
                var tmpTTmllb = allTTmllb.FirstOrDefault(i => i.Code == p.typeCode);
                if (tmpTTmllb == null) throw new FailedException(string.Format("未找到{0}的项目类别对照", p.typeCode));
                _s21RequestDto.list.Add(new detail
                {
                    detailCode = p.detailCode,
                    detailName = p.detailName,
                    detailHosCode = p.detailCode,
                    typeCode = tmpTTmllb.TTCode,
                    num = p.num,
                    price = p.price,
                    totalCost = p.totalCost,
                    date = p.date
                });
            });
            _s24RequestDto = new S24RequestDTO
            {
                outpId = Request.outpId
            };
        }

        protected override void Action(ActResult actResult)
        {
            #region 回退先前上传的明细

            var s22Response = OutpatientProxy.GetInstance(Request.OrganizeId).S22(_s22RequestDto);
            if (s22Response.state)
            {
                if (s22Response.data != null && s22Response.data.Count > 0)
                {
                    var s23Request = new S23RequestDTO
                    {
                        outpId = Request.outpId,
                        list = new List<string>()
                    };
                    s22Response.data.ForEach(p => { s23Request.list.Add(p.detailNo); });
                    var s23Response = OutpatientProxy.GetInstance(Request.OrganizeId).S23(s23Request);
                    if (!s23Response.state)
                    {
                        actResult.IsSucceed = false;
                        actResult.ResultMsg = string.Format("新农合医保门诊费用明细退单接口返回异常！异常提示：{0}  ", s23Response.message);
                        return;
                    }
                }
            }
            else
            {
                actResult.IsSucceed = false;
                actResult.ResultMsg = string.Format("新农合医保上传明细查询接口返回异常！异常提示：{0}  ", s22Response.message);
                return;
            }
            #endregion

            #region 上传明细

            var s21Response = OutpatientProxy.GetInstance(Request.OrganizeId).S21(_s21RequestDto);
            if (!s21Response.state)
            {
                actResult.IsSucceed = false;
                actResult.ResultMsg = string.Format("新农合医保门诊费用上传接口返回异常！异常提示：{0}  ", s21Response.message);
                return;
            }
            #endregion

            #region 模拟交易

            var s24Response = OutpatientProxy.GetInstance(Request.OrganizeId).S24(_s24RequestDto);
            if (!s24Response.state)
            {
                actResult.IsSucceed = false;
                actResult.ResultMsg = string.Format("新农合医保门诊模拟结算接口返回异常！异常提示：{0}  ", s24Response.message);
                return;
            }

            actResult.Data = s24Response;

            #endregion
        }
    }
}