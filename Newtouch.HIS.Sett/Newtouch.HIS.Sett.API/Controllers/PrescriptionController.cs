using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.API.Common.Attributes;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Sett.Request.Prescription;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using Newtouch.HIS.Domain.Entity;
using System.Linq;
using Newtouch.HIS.API.Common.Exceptions;
using Newtouch.HIS.API.Common.Filter;
using Newtouch.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Application;

namespace Newtouch.HIS.Sett.API.Controllers
{
    /// <summary>
    /// 处方相关
    /// </summary>
    [RoutePrefix("api/Prescription")]
    [DefaultAuthorize]
    public class PrescriptionController : ApiControllerBase<PrescriptionController>
    {
        private readonly IOutPatientUniversalDmnService _outPatientUniversalDmnService;
        private readonly IHospPatientBasicInfoRepo _hospPatientBasicInfoRepo;
        private readonly IHospItemFeeApp _HospItemFeeApp;
        public PrescriptionController(IComponentContext com)
            : base(com)
        {
        }

        /// <summary>
        /// 处方更新请求（新处方、或新增明细、或删除明细）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddOrUpdate")]
        public HttpResponseMessage AddOrUpdate(PrescriptionAddOrUpdateRequest par)
        {
            Action<PrescriptionAddOrUpdateRequest, DefaultResponse> ac = (req, resp) =>
            {
                IList<string> updateSkipSettledCfhList;
                add(par.PrescriptionList, par.mzh, par.ys, out updateSkipSettledCfhList);

                resp.data = new
                {
                    settledCfhList = updateSkipSettledCfhList,
                };
                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);

            return base.CreateResponse(response);
        }

        /// <summary>
        /// 门诊号下的处方信息全部重写请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateAllForOutNo")]
        //[IgnoreTokenDecrypt]
        public HttpResponseMessage UpdateAllForOutNo(PrescriptionUpdateAllForOutNoRequest par)
        {
            Action<PrescriptionUpdateAllForOutNoRequest, DefaultResponse> ac = (req, resp) =>
            {
                if (string.IsNullOrWhiteSpace(par.AheadCancelCf))
                {
                    //作废所有的未收费处方
                    var cfList = _outPatientUniversalDmnService.GetValidCfListByMzh(this.UserIdentity.OrganizeId, par.mzh, cfzt: "0", cfly: "1");   //未收费 来自门诊医生站

                    if (cfList != null && cfList.Count > 0)
                    {
                        _outPatientUniversalDmnService.CancelPrescription(this.UserIdentity.OrganizeId
                            , cfhList: cfList.Select(p => p.cfh).ToList());
                    }
                }
                else
                {
                    //作废部分指定的处方
                    var cfList = _outPatientUniversalDmnService.GetValidCfListByMzh(this.UserIdentity.OrganizeId, par.mzh, cfzt: "0", cfly: "1", cfhs: par.AheadCancelCf);   //未收费 来自门诊医生站

                    if (cfList != null && cfList.Count > 0)
                    {
                        _outPatientUniversalDmnService.CancelPrescription(this.UserIdentity.OrganizeId
                            , cfhList: cfList.Select(p => p.cfh).Distinct().ToList());
                    }
                }

                //再新增
                IList<string> updateSkipSettledCfhList;
                add(par.PrescriptionList, par.mzh, par.ys, out updateSkipSettledCfhList);

                resp.data = new
                {
                    settledCfhList = updateSkipSettledCfhList,
                };
                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);

            return base.CreateResponse(response);
        }

        /// <summary>
        /// 处方作废
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Cancel")]
        public HttpResponseMessage Cancel(PrescriptionCancelRequest par)
        {
            Action<PrescriptionCancelRequest, DefaultResponse> ac = (req, resp) =>
            {
                if (par.cfList != null && par.cfList.Count > 0)
                {
                    _outPatientUniversalDmnService.CancelPrescription(this.UserIdentity.OrganizeId
                 , cfhList: par.cfList.Select(p => p.cfh).Distinct().ToList());
                }

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);

            return base.CreateResponse(response);
        }

        /// <summary>
        /// 处方作废 根据门诊号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CancelByOutNo")]
        public HttpResponseMessage CancelByOutNo(PrescriptionCancelByOutNoRequest par)
        {
            Action<PrescriptionCancelByOutNoRequest, DefaultResponse> ac = (req, resp) =>
            {
                //作废所有的未收费处方
                var cfList = _outPatientUniversalDmnService.GetValidCfListByMzh(this.UserIdentity.OrganizeId, par.mzh, cfzt: "0", cfly: "1");   //未收费 来自门诊医生站

                if (cfList != null && cfList.Count > 0)
                {
                    _outPatientUniversalDmnService.CancelPrescription(this.UserIdentity.OrganizeId
                        , cfhList: cfList.Select(p => p.cfh).Distinct().ToList());
                }

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);

            return base.CreateResponse(response);
        }

        /// <summary>
        /// 处方状态查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("StatusQuery")]
        public HttpResponseMessage StatusQuery(PrescriptionStatusQueryRequest par)
        {
            Action<PrescriptionStatusQueryRequest, DefaultResponse> ac = (req, resp) =>
            {
                var data = new PrescriptionStatusDTO()
                {
                    cfzt = "1",
                    cfztValue = "正常",
                    jszt = "1",
                    jsztValue = "已结",
                };

                resp.data = data;

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);

            return base.CreateResponse(response);
        }
        /// <summary>
        /// 代煎费项目生成
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DjxmInsert")]
        public HttpResponseMessage DjxmInsert(DjxmInsertRequest djxm)
        {
            Action<DjxmInsertRequest, DefaultResponse> ac = (req, resp) =>
            {
                var zyxx = _hospPatientBasicInfoRepo.FindEntity(p => p.zyh == djxm.zyh && p.OrganizeId == djxm.OrganizeId);
                if (zyxx.zybz == ((int)EnumZYBZ.Wry).ToString() || zyxx.zybz == ((int)EnumZYBZ.Ycy).ToString())
                {
                }
                HospItemBillingEntity zyXmjfb = new HospItemBillingEntity();
                zyXmjfb.zyh = djxm.zyh;
                zyXmjfb.tdrq = DateTime.Now;
                zyXmjfb.sfxm = djxm.sfxm;
                zyXmjfb.dl = djxm.dl;
                zyXmjfb.ys = djxm.ys;
                zyXmjfb.ks = djxm.ks;
                zyXmjfb.ysmc = djxm.ysmc;
                zyXmjfb.ksmc = djxm.ksmc;
                zyXmjfb.bq = zyxx.bq;
                zyXmjfb.cw = zyxx.cw;
                zyXmjfb.dj = djxm.dj;
                zyXmjfb.sl = djxm.sl;
                zyXmjfb.jfdw = djxm.dw;
                zyXmjfb.zxks = djxm.zxks;
                zyXmjfb.yzxz = ((int)EnumYZXZ.LSYZ).ToString();
                zyXmjfb.yzzt = ((int)EnumYZZT.WCX).ToString();
                zyXmjfb.zfxz = djxm.zfxz;
                zyXmjfb.zfbl = djxm.zfbl;
                zyXmjfb.cxzyjfbbh = 0;
                zyXmjfb.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
                _HospItemFeeApp.SubmitForm(zyXmjfb, null);
                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, djxm);

            return base.CreateResponse(response);
        }
        #region private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtoList"></param>
        /// <returns></returns>
        private IList<OutpatientPrescriptionDetailEntity> convert(IList<PrescriptionMedicineItemDTO> dtoList)
        {
            if (dtoList == null)
            {
                return null;
            }
            return dtoList.Select(p =>
            {
                var newEntity = new OutpatientPrescriptionDetailEntity()
                {
                    yp = p.yp,
                    sl = p.sl,
                    dw = p.dw,
                    jl = p.jl,
                    jldw = p.jldw,
                    czh = p.zh,
                    dj = -1,    //CIS医生没有定价的权限 -1后面会更新为字典价格
                    yfCode = p.yfCode,
                    zzfbz = p.zzfbz,
                    pcCode = p.pcCode
                };
                if (string.IsNullOrWhiteSpace(newEntity.yp))
                {
                    throw new ArgumentsRequiredException("yp");
                }
                if (newEntity.sl <= 0)
                {
                    throw new ArgumentsRequiredException("sl");
                }
                return newEntity;
            }).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtoList"></param>
        /// <returns></returns>
        private IList<OutpatientItemEntity> convert(IList<PrescriptionTreamentItemDTO> dtoList, string cflxmc)
        {
            if (dtoList == null)
            {
                return null;
            }
            return dtoList.Select(p =>
            {
                var newEntity = new OutpatientItemEntity()
                {
                    sfxm = p.sfxm,
                    dczll = p.dczll,
                    zxcs = p.zxcs,
                    sl = p.sl,
                    zzll = p.zll,
                    dw = p.dw,
                    dj = -1,    //CIS医生没有定价的权限 -1后面会更新为字典价格
                    zzfbz = p.zzfbz,
                    ztId = p.ztId,
                    ztmc = p.ztmc,
                    ztsl = p.ztsl
                };
                if (string.IsNullOrWhiteSpace(newEntity.sfxm))
                {
                    throw new ArgumentsRequiredException("sfxm");
                }
                newEntity.sl = newEntity.sl < 0 ? 0 : newEntity.sl; //后面 根据‘单位计量数’计算
                if (newEntity.sl <= 0 && cflxmc.Contains("康复"))
                {
                    //throw new ArgumentsRequiredException("sl");

                    if ((newEntity.dczll ?? 0) <= 0)
                    {
                        throw new ArgumentsRequiredException("dczll");
                    }
                    if ((newEntity.zxcs ?? 0) <= 0)
                    {
                        throw new ArgumentsRequiredException("zxcs");
                    }
                    if ((newEntity.zzll ?? 0) <= 0)
                    {
                        if ((newEntity.dczll ?? 0) > 0 && (newEntity.zxcs ?? 0) > 0)
                        {
                            newEntity.zzll = newEntity.dczll.Value * newEntity.zxcs.Value;
                        }
                        else
                        {
                            throw new ArgumentsRequiredException("zzll");
                        }
                    }
                }
                return newEntity;
            }).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prs"></param>
        /// <param name="mzh"></param>
        /// <param name="ys"></param>
        private void add(IList<PrescriptionUpdateDTO> prs, string mzh, string ys
            , out IList<string> updateSkipSettledCfhList)
        {
            var cfs = new List<FeeItemsGroupedBO>();
            foreach (var pr in prs)
            {
                var cf = new FeeItemsGroupedBO();
                cf.xmWithCf = true;
                cf.cflx = pr.cflx;
                cf.cflxxf = pr.cflxxf;
                cf.cflxmc = pr.cflxmc;
                cf.cfh = pr.cfh;
                cf.lyyf = pr.lyyf;
                cf.ts = pr.ts;
                cf.djbz = pr.djbz == true ? 1 : 0;
                cf.ypList = convert(pr.AddMedicineItems);
                cf.ztInvalidYpList = convert(pr.DeleteMedicineItems);
                cf.xmList = convert(pr.AddTreamentItems, pr.cflxmc);
                cf.ztInvalidXmList = convert(pr.DeleteTreamentItems, pr.cflxmc);
                cfs.Add(cf);
            }

            IList<int> cfnmList;
            IList<int> xmnmList;
            _outPatientUniversalDmnService.SubmitItems(this.UserIdentity.OrganizeId
                , out cfnmList, out xmnmList
                , out updateSkipSettledCfhList
                , ItemsGroupBOList: cfs
                , mzh: mzh, ys: ys
                , settledUpdateSkip: true
                , cfly: "1");
        }

        #endregion

    }
}
