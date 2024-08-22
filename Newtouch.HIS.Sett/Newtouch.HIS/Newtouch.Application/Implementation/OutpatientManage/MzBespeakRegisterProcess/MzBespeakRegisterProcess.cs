using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Strategy;
using Newtouch.HIS.Application.Strategy.MzBespeakRegister;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using Newtouch.Infrastructure;
using System;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application.Implementation.OutpatientManage.MzBespeakRegisterProcess
{
    /// <summary>
    /// 预约挂号处理
    /// </summary>
    public class MzBespeakRegisterProcess : ProcessorFun<BespeakRegisterParamDTO>
    {
        private readonly IOutpatientRegistRepo _outpatientRegistRepo;

        /// <summary>
        /// 已预约挂号
        /// </summary>
        private List<MzBespeakRegisterQueryResponseDTO> _mzyygh;

        /// <summary>
        /// 预约挂号执行策略
        /// </summary>
        private List<ICommitStrategy> _strategy;

        /// <summary>
        /// 门诊挂号
        /// </summary>
        private readonly MzBespeakRegisterVO _mzBespeakRegisterVo;

        public MzBespeakRegisterProcess(BespeakRegisterParamDTO request) : base(request)
        {
            _mzBespeakRegisterVo = new MzBespeakRegisterVO
            {
                registerInfo = Request,
                userCode = UserIdentity.UserCode,
            };
            _strategy = new List<ICommitStrategy>();
        }

        /// <summary>
        /// 效验request
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (string.IsNullOrWhiteSpace(Request.zjh)) throw new FailedException("证件号不能为空");
            if (string.IsNullOrWhiteSpace(Request.blh)) throw new FailedException("病历号不能为空");
            if (Request.mzlx <= 0) throw new FailedException("门诊类型不能为空");
            return new ActResult();
        }

        /// <summary>
        /// 预处理
        /// </summary>
        /// <param name="actResult"></param>
        protected override void BeforeAction(ActResult actResult)
        {
            var mzBespeakRegisterQueryResponse = SiteCISAPIHelper.MzBespeakRegisterQuery(Request.zjlx, Request.zjh, Request.blh, Request.kh);
            if (mzBespeakRegisterQueryResponse != null && mzBespeakRegisterQueryResponse.data != null)
            {
                _mzBespeakRegisterVo.bespeakRegDetail = mzBespeakRegisterQueryResponse.data.ToString().ToObject<List<MzBespeakRegisterQueryResponseDTO>>();
            }
            var mzyyghpb = SiteCISAPIHelper.MzBespeakRegisterSchedulingQuery(Request.ksCode, Request.ysgh, Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")), "");
            if (mzyyghpb != null && mzyyghpb.data != null)
            {
                _mzBespeakRegisterVo.bespeakRegSchedulingDetail = mzyyghpb.data.ToString().ToObject<List<MzBespeakRegisterSchedulingQueryResponseDTO>>();
                if (_mzBespeakRegisterVo.bespeakRegSchedulingDetail != null && _mzBespeakRegisterVo.bespeakRegSchedulingDetail.Count > 0)
                {
                    _mzBespeakRegisterVo.allBespeakMaxCount = _mzBespeakRegisterVo.bespeakRegSchedulingDetail.Sum(p => p.bespeakMaxCount);
                }
            }
            var yyymzghsl = SiteCISAPIHelper.MzAlreadyBespeakRegisterCountQuery(Request.ksCode, Request.ysgh, Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")), "");
            if (yyymzghsl != null && yyymzghsl.data != null)
            {
                var data = yyymzghsl.data.ToString().ToObject<MzAlreadyBespeakRegisterCountQueryResponseDTO>();
                if (data != null)
                {
                    _mzBespeakRegisterVo.alreadyBespeakRegisterCount = data.alreadyBespeakRegisterCount;
                }
            }
            var todayDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            var tomorrowDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            switch (Request.mzlx)
            {
                case (int)EnumOutPatientType.expertOutpat:
                    _strategy.Add(ExpertBespeakStrategy.Instance());
                    var zjghDetail = _outpatientRegistRepo.IQueryable(p => p.OrganizeId == OrganizeId && p.CreateTime >= todayDate && p.CreateTime < tomorrowDate && p.zt == "1" && p.blh == Request.blh && p.ks == Request.ksCode && p.ys == Request.ysgh);
                    _mzBespeakRegisterVo.alreadyRegisterCount = zjghDetail.Count();
                    break;
                case (int)EnumOutPatientType.generalOutpat:
                    _strategy.Add(GeneralBespeakStrategy.Instance());
                    var ptghDetail = _outpatientRegistRepo.IQueryable(p => p.OrganizeId == OrganizeId && p.CreateTime >= todayDate && p.CreateTime < tomorrowDate && p.zt == "1" && p.blh == Request.blh && p.ks == Request.ksCode && (p.ys == null || p.ys == ""));
                    _mzBespeakRegisterVo.alreadyRegisterCount = ptghDetail.Count();
                    break;
                case (int)EnumOutPatientType.emerDiagnosis:
                default:
                    break;
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="actResult"></param>
        protected override void Action(ActResult actResult)
        {
            _strategy.ForEach(p =>
            {
                p.Process(new CancellationTokenSource(), _mzBespeakRegisterVo, actResult);
            });
        }

        /// <summary>
        /// 后处理
        /// </summary>
        /// <param name="actResult"></param>
        protected override void AfterAction(ActResult actResult)
        {
            _strategy.ForEach(p =>
            {
                p.After(new CancellationTokenSource(), _mzBespeakRegisterVo, actResult);
            });
        }
    }
}
