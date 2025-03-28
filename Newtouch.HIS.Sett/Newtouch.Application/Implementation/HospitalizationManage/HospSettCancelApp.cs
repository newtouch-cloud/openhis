using Newtouch.Common;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.DTO.OutputDto;
using FrameworkBase.MultiOrg.Application;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 取消住院结算 实现
    /// </summary>
    public class HospSettCancelApp : AppBase, IHospSettCancelApp
    {
        private readonly IHospPatientBasicInfoRepo _hospPatientBasicInfoRepo;
        private readonly IHosPatAccDmnService _hosPatAccDmnService;
        private readonly ISysCashPaymentModelRepo _sysCashPaymentModelRepo;
        private readonly IHospSettDmnService _hospSettDmnService;
        private readonly IHospSettlementRepo _hospSettlementRepo;
        private readonly IHospSettlementDetailRepo _hospSettlementDetailRepo;

        /// <summary>
        /// 取消住院结算，查看患者住院 状态信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="kh"></param>
        public HospSettCancelPatStatusDetailDto GetPatHospStatusDetail(string zyh, string kh)
        {
            //获取住院结算病人信息（基本信息、账户、科室等）
            var settpatinfo = getHospPatBasicInfo(ref zyh, kh);

            //根据住院号 获取 （未退）结算列表， 取其最后一条
            var lastSett = _hospSettlementRepo.GetValidList(zyh, this.OrganizeId).OrderByDescending(p => p.CreateTime).FirstOrDefault();

            var resultDto = new HospSettCancelPatStatusDetailDto()
            {
                SettPatInfo = settpatinfo,
                LastUnCancelledSett = lastSett,
            };

            return resultDto;
        }

        /// <summary>
        /// 取消住院结算 提交保存操作
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedjsnm"></param>
        /// <param name="cancelReason"></param>
        public void DoCancel(string zyh, int expectedjsnm, string cancelReason)
        {
            //获取住院结算病人信息（基本信息、账户、科室等）
            var settpatinfo = getHospPatBasicInfo(ref zyh, "");

            //根据住院号 获取 （未退）结算列表， 取其最后一条
            var lastSett = _hospSettlementRepo.GetValidList(zyh, this.OrganizeId).OrderByDescending(p => p.CreateTime).FirstOrDefault();

            if (lastSett.jsnm != expectedjsnm)
            {
                throw new FailedCodeException("HOSP_CANCELSETT_JSNM_IS_CHANGED");
            }

            var sqlVO = new HospCancelSettDBDataUpdateCollectVO() {
                cx_zyjsmxList = new List<HospSettlementDetailEntity>(),
                cx_xt_brzhszjlList = new List<SysPatientAccountRevenueAndExpenseEntity>(),
                cx_zy_jszffsList = new List<HospSettlementPaymentModelEntity>(),

            };

            //医保交易取消结算

            sqlVO.zyh = zyh;
            /*********注 此处的zybz应是本系统状态变更为“待结账”，推送给其他系统（繁云），然后（繁云）系统变更为“病区中”，这样，该病人才可记账。 这里先手动赋值 *************/
            sqlVO.zybz = ((int)EnumZYBZ.Bqz).ToString();
            //sqlVO.zybz = ((int)EnumZYBZ.Djz).ToString();
            sqlVO.cyrq = null;

            //住院结算 对应的 撤销记录
            var cxjs = lastSett.Clone();
            sqlVO.cxjs = cxjs;
            cxjs.jsnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("zy_js");
            cxjs.cxjsnm = lastSett.jsnm;
            cxjs.jszt = ((int)EnumJieSuanZT.YT).ToString();
            cxjs.cxjsyy = cancelReason;
            cxjs.CreatorCode = this.UserIdentity.UserCode;
            cxjs.CreateTime = DateTime.Now;
            cxjs.zt = "1";

            //住院结算明细 对应的 撤销记录
            var zyjsmxList = _hospSettlementDetailRepo.IQueryable().Where(p => p.jsnm == lastSett.jsnm).ToList();
            foreach (var zyjsmx in zyjsmxList)
            {
                var cxjsmx = zyjsmx.Clone();
                cxjsmx.jsnm = cxjs.jsnm;
                cxjsmx.CreatorCode = this.UserIdentity.UserCode;
                cxjsmx.CreateTime = DateTime.Now;
                cxjsmx.zt = "1";
                sqlVO.cx_zyjsmxList.Add(cxjsmx);
            }

            //系统病人账户收支记录
            if (!settpatinfo.zyyjjzhbh.HasValue || !settpatinfo.zyyjjzh.HasValue)
            {
                throw new FailedCodeException("HOSP_ERROR_PATIENT_YJJ_ACCOUNT_IS_NOT_FOUND");
            }
            sqlVO.cx_xt_brzhszjlList.Add(new SysPatientAccountRevenueAndExpenseEntity()
            {
                OrganizeId = this.OrganizeId,
                zh = settpatinfo.zyyjjzh ?? 0,
                patid = settpatinfo.patid.Value,
                szje = lastSett.xjzf,
                zhye = lastSett.xjzf,
                pzh = string.Empty,
                szxz = ((int)AccSzxz.ZYJS).ToString(),
                xjzffs = "",    //&update170720
                jsnm = cxjs.jsnm,
                zyh = zyh,
                CreatorCode = this.UserIdentity.UserCode,
                CreateTime = DateTime.Now,
                zt = "1",
            });

            //住院结算支付方式
            sqlVO.cx_zy_jszffsList.Add(new HospSettlementPaymentModelEntity()
            {
                jsnm = cxjs.jsnm,
                OrganizeId = this.OrganizeId,
                xjzffs = Constants.xtzffs.ZYYJZHZF,
                zfje = lastSett.xjzf,
                ssry = this.UserIdentity.UserCode,
                ssrq = DateTime.Now,
                CreatorCode = this.UserIdentity.UserCode,
                CreateTime = DateTime.Now,
                zt = "1",
            });

            _hospSettDmnService.CancelSettSyncToDB(sqlVO, this.OrganizeId);

            //########同步状态至中间库

            return;
        }

        #region private methods

        /// <summary>
        /// 取消住院结算 获取病人基本信息（基本信息、账户、科室等） 在院状态验证
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        private HospSettPatInfoVO getHospPatBasicInfo(ref string zyh, string kh)
        {
            if (string.IsNullOrWhiteSpace(zyh) && !string.IsNullOrWhiteSpace(kh))
            {
                var zyList = _hospPatientBasicInfoRepo.IQueryable().Where(p => kh.Equals(p.kh) && p.OrganizeId == this.OrganizeId).ToList();
                if (zyList.Count == 0)
                {
                    throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
                }
                //zyh = zyList.Where(p => p.zybz == ((int)EnumZYBZ.Ycy).ToString())
                //      .OrderByDescending(p => p.CreateTime).Select(p => p.zyh).First();
                //if (string.IsNullOrWhiteSpace(zyh)) //说明没有可取消结算的记录
                //{
                //    zyh = zyList.OrderByDescending(p => p.CreateTime).Select(p => p.zyh).First();
                //}
                zyh = zyList.OrderByDescending(p => p.CreateTime).Select(p => p.zyh).First();
            }
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }
            var settpatinfoList = _hosPatAccDmnService.GetHospSettPatInfo(zyh, this.OrganizeId);
            if (settpatinfoList == null || settpatinfoList.Count == 0)
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (settpatinfoList.Count > 1)
            {
                throw new FailedCodeException("HOSP_MATCHED_ERROR_MULTI_MATCHED");
            }
            var settpatinfo = settpatinfoList.First();
            if (settpatinfo.zybz != ((int)EnumZYBZ.Ycy).ToString())
            {
                throw new FailedCodeException("HOSP_CURRENTSTATUS_NOT_YCY_CANNOT_DO_CANCEL_SETTLEMENT");
            }
            else if (settpatinfo.patid == 0)
            {
                throw new FailedCodeException("HOSP_SYSPATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (settpatinfo.ryrq <= new DateTime(1990, 01, 01))
            {
                throw new FailedCodeException("HOSP_ERROR_THERE_IS_NO_RYRQ");
            }
            else if (string.IsNullOrWhiteSpace(settpatinfo.brxz))
            {
                throw new FailedCodeException("HOSP_SYSPATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (string.IsNullOrWhiteSpace(settpatinfo.ksCode))
            {
                throw new FailedCodeException("HOSP_ERROR_PATIENT_DEPT_IS_NO_FOUND");
            }
            else if (settpatinfo.zyyjjzh <= 0)
            {
                throw new FailedCodeException("HOSP_ERROR_PATIENT_THERE_IS_NO_YJJ_ACCOUNT");
            }

            settpatinfo.zyts = DateTimeHelper.GetInHospDays(settpatinfo.ryrq.Value, settpatinfo.cyrq.Value);

            return settpatinfo;
        }

        #endregion

    }

}
