using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Sett.Request.Patient;
using Newtouch.Tools;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 门诊挂号应用
    /// </summary>
    public class OutpatientRegApp : AppBase, IOutpatientRegApp
    {
        private readonly IOutPatientDmnService _outPatientDmnService;
        private readonly IOutPatientSettleDmnService _outPatientSettleDmnService;
        private readonly IFinancialInvoiceRepo _financialInvoiceEntityRepository;
        private readonly ISysConfigRepo _sysConfigRepo;
        private List<MzBespeakRegisterQueryResponseDTO> mzbrDetail;

        /// <summary>
        /// 挂号排班List
        /// </summary>
        /// <param name="keyword">科室/医生</param>
        /// <param name="mzlx">门诊类型 普通门诊/急症/专家门诊</param>
        /// <returns></returns>
        public List<RegScheduleVO> GetRegScheduleList(string keyword, string mzlx)
        {
            return _outPatientDmnService.GetRegScheduleList(keyword, mzlx, this.OrganizeId);
        }
        /// <summary>
        /// 根据工号获取发票号 
        /// by caishanshan 
        /// </summary>
        /// <returns></returns>
        public string GetInvoice()
        {
            return _outPatientDmnService.GetInvoiceListByEmpNo(UserIdentity.UserCode, OrganizeId);
        }
        /// <summary>
        /// 验证输入发票号是否可用
        /// by caishanshan
        /// </summary>
        /// <param name="inputFph"></param>
        public void CheckInvoice(string inputFph)
        {
            FinancialInvoiceEntity fpUpdateEntity, fpInsertEntity;
            _financialInvoiceEntityRepository.UpdateCurrentGetEntitys(inputFph, UserIdentity.UserCode, out fpUpdateEntity, out fpInsertEntity, OrganizeId);
        }

        /// <summary>
        /// 门诊挂号保存
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="kh"></param>
        /// <param name="ghly"></param>
        /// <param name="mjzbz"></param>
        /// <param name="ks"></param>
        /// <param name="ys"></param>
        /// <param name="ksmc"></param>
        /// <param name="ysmc"></param>
        /// <param name="ghxm"></param>
        /// <param name="zlxm"></param>
        /// <param name="fph"></param>
        /// <param name="sfrq"></param>
        /// <param name="isCkf"></param>
        /// <param name="isGbf"></param>
        /// <param name="ghpbId"></param>
        /// <param name="feeRelated"></param>
        /// <param name="brxz">病人性质由页面传过来，因为医保患者可以挂自费</param>
        /// <param name="notSett">true暂时不结（门诊收费时一起结），否则结</param>
        /// <param name="ybjsh">医保端结算号</param>
        /// <param name="qzjzxh">前置就诊序号</param>
        /// <param name="qzmzh">前置门诊号</param>
        /// <param name="mzyyghId">门诊预约挂号ID 对应Newtouch_CIS.dbo.mz_yygh.Id</param>
        /// <returns></returns>
        public void Save(int patid, string kh, string ghly, string mjzbz,
            string ks, string ys, string ksmc, string ysmc, string ghxm, string zlxm
            , string fph, DateTime? sfrq, bool isCkf, bool isGbf, int ghpbId
            , OutpatientSettFeeRelatedDTO feeRelated, string brxz
            , string ybjsh, string mzyyghId, ref short? qzjzxh, ref string qzmzh
            , string jzyy,string jzid, string jzlx, string bzbm, string bzmc, out object newJszbInfo)
        {
            if ((qzjzxh ?? 0) <= 0 || string.IsNullOrWhiteSpace(qzmzh))
            {
                var mzhjzxh = _outPatientSettleDmnService.GetMzhJzxh(patid, ghpbId.ToString(), ks, ys,this.OrganizeId, this.UserIdentity.UserCode);
                qzjzxh = mzhjzxh.Item1;
                qzmzh = mzhjzxh.Item2;
            }
            //保存
            _outPatientSettleDmnService.Save(this.OrganizeId, patid, kh, ghly, mjzbz,
            ks, ys, ksmc, ysmc, ghxm, zlxm, fph, sfrq, isCkf, isGbf, (int)qzjzxh.Value, ghpbId, feeRelated, brxz, ybjsh, qzmzh, jzyy,jzid,jzlx,bzbm,bzmc, mzyyghId, out newJszbInfo);
        }


        public void UnSettSave(int patid, string kh, string ghly, string mjzbz,
            string ks, string ys, string ksmc, string ysmc, string ghxm, string zlxm, bool isCkf, bool isGbf, int ghpbId, string brxz
            , string ybjsh, ref short? qzjzxh, ref string qzmzh
            , string jzyy, string jzid, string jzlx, string bzbm, string bzmc, out object newJszbInfo)
        {
            if (( qzjzxh ?? 0) <= 0 || string.IsNullOrWhiteSpace( qzmzh))
            {
                var mzhjzxh = _outPatientSettleDmnService.GetMzhJzxh( patid,  ghpbId.ToString(),  ks,  ys, this.OrganizeId, this.UserIdentity.UserCode);
                 qzjzxh = mzhjzxh.Item1;
                 qzmzh = mzhjzxh.Item2;
            }
            //保存
            _outPatientSettleDmnService.UnSettSave(this.OrganizeId,  patid,  kh,  ghly,  mjzbz,
             ks,  ys,  ksmc,  ysmc,  ghxm,  zlxm,  isCkf,  isGbf, (int) qzjzxh.Value,  ghpbId,  brxz,  ybjsh,  qzmzh,  jzyy,  jzid,  jzlx,  bzbm,  bzmc, null, out newJszbInfo);
        }

        /// <summary>
        /// 预定
        /// </summary>
        /// <param name="gh"></param>
        /// <param name="ssk"></param>
        /// <param name="fph"></param>
        /// <param name="isCkf"></param>
        /// <param name="isGbf"></param>
        /// <param name="updateBrxz"></param>
        public long Book(OutpatientRegistEntity gh, decimal ssk, string fph, bool isCkf, bool isGbf, string updateBrxz)
        {
            //获取就诊序号
            var jzxh = _outPatientDmnService.GetJzxh(gh.ghlx, gh.ks, gh.ys, gh.ghzb, this.UserIdentity.UserCode, this.OrganizeId);
            if (string.IsNullOrEmpty(jzxh.ToString()))
            {
                throw new FailedCodeException("OUTPAT_FAILED_TO_GET_THE_SERIAL_NUMBER");
            }
            gh.jzxh = jzxh;
            //验证病人性质是否适用于门诊
            CheckBrxz(gh.kh, updateBrxz);
            //保存
            return _outPatientSettleDmnService.Book(gh, ssk, fph, isCkf, isGbf);
        }

        /// <summary>
        /// 判断病人性质是否适用于门诊（挂号页面更改病人性质）
        /// </summary>
        public void CheckBrxz(string kh, string updateBrxz)
        {
            //根据kh获取病人基本信息及卡类型
            var basicInfoVo = _outPatientDmnService.GetBasicInfoByCardNo(kh);
            var ghlx = basicInfoVo.cardtype;
            if (ghlx == "")
            {
                throw new FailedCodeException("SYS_CARD_TYPE_IS_EMPTY");  //卡类型=挂号类型
            }
            var ybtsdy = string.Empty;
            var syybbf = string.Empty;
            var fw = ((int)Enummzzybz.mz).ToString();
            var validBrxzList = _outPatientDmnService.GetValidBrxzList(string.Empty, ghlx, ybtsdy, syybbf, fw);
            //验证病人性质是否包含挂号的病人性质
            var validBrxz = validBrxzList.Any(validBrxzItem => validBrxzItem.brxz == updateBrxz);
            if (!validBrxz)
            {
                throw new FailedCodeException("OUPAT_THE_NATURE_OF_THE_PATIENT_DOES_NOT_APPLY_TO_OUTPATIENT");
            }

        }
    }
}
