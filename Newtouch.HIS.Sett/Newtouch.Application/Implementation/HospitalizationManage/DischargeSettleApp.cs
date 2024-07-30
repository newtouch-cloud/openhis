using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface.HospitalizationManage;
using Newtouch.HIS.Domain.BusinessObjects.HospitalizationManage;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.OutpatientManage;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.HospitalizationManage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.IRepository.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using Newtouch.HIS.Proxy.guian.DTO;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Linq;

namespace Newtouch.HIS.Application.Implementation.HospitalizationManage
{
    /// <summary>
    /// 
    /// </summary>
    public class DischargeSettleApp : AppBase, IDischargeSettleApp
    {
        private readonly IDischargeSettleDmnService _dischargeSettleDmnService;
        private readonly IHospSettlementRepo _hospSettlementRepo;
        private readonly IHospSettlementDetailRepo _hospSettlementDetailRepo;
        private readonly IHospAccountingPlanDetailRepo _hospAccountingPlanDetailRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IHospSettlementGAYBFeeRepo _hospSettlementGAYBFeeRepo;
        private readonly IHospSettlementGAXNHFeeRepo _hospSettlementGAXNHFeeRepo;
        private readonly IHospPatientBasicInfoRepo _hospPatientBasicInfoRepo;
        private readonly ICqybSett05Repo _cqybSett05Repo;
        private readonly IBookkeepInHosDmnService _BookkeepInHosDmnService;
        private readonly IHospItemBillingRepo _hospItemBillingRepo;
        #region 出院结算

        /// <summary>
        /// 住院号查询数据（包括病人信息和计费明细） zyh 或 kh +cardType 或 sfz +cardType
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="sfz"></param>
        /// <param name="kh"></param>
        /// <param name="cardType"></param>
        /// <returns></returns>
        public InpatientSettPatStatusDetailDto GetInpatientSettleStatusDetail(string zyh, string sfz, string kh, string cardType, string jslx, string ver)
        {
            // 获取病人信息
            var settpatInfo = GetInpatientSettlePatInfo(ref zyh, sfz, kh, cardType, jslx);
            // 获取计费明细（包括：项目和药品）
            //var settleItemsBo = GetInpatientSettleItemsDetailList(zyh);
            if (settpatInfo.cyrq.HasValue)
            {
                settpatInfo.zyts = DateTimeHelper.GetInHospDays(settpatInfo.ryrq.Value, settpatInfo.cyrq.Value);
            }
            var resultDto = new InpatientSettPatStatusDetailDto()
            {
                InpatientSettPatInfo = settpatInfo,
                //InpatientSettleItemBO = settleItemsBo
                GroupFeeVOList = _dischargeSettleDmnService.GetHospGroupFeeVOList(zyh, this.OrganizeId, ver),
            };
            return resultDto;
        }


        /// <summary>
        /// 病人基本信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public InpatientSettPatInfoVO GetInpatientSettlePatInfo(ref string zyh, string sfz = null, string kh = null, string cardType = null, string jslx = null, string orgId = null)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                if (!string.IsNullOrWhiteSpace(kh) && !string.IsNullOrWhiteSpace(cardType))
                {
                    //根据卡号和卡类型 换取住院号
                    zyh = _hospPatientBasicInfoRepo.IQueryable().Where(p => p.kh == kh && p.CardType == cardType && p.zybz != ((int)EnumZYBZ.Wry).ToString()).OrderByDescending(p => p.CreateTime).Select(p => p.zyh).FirstOrDefault();
                }
                else if (!string.IsNullOrWhiteSpace(sfz) && !string.IsNullOrWhiteSpace(cardType))
                {
                    //根据身份证和卡类型 换取住院号
                    var zjlx = ((int)EnumZJLX.sfz).ToString();
                    zyh = _hospPatientBasicInfoRepo.IQueryable().Where(p => p.zjlx == zjlx && p.zjh == sfz && p.CardType == cardType).OrderByDescending(p => p.CreateTime).Select(p => p.zyh).FirstOrDefault();
                }
            }

            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }

            var settPatinfoList = _dischargeSettleDmnService.SelectInpatientSettPatInfo(zyh, this.OrganizeId ?? orgId);
            if (settPatinfoList == null || settPatinfoList.Count == 0)
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (settPatinfoList.Count > 1)
            {
                throw new FailedCodeException("HOSP_MATCHED_ERROR_MULTI_MATCHED");
            }
            var settpatinfo = settPatinfoList.First();

            if (settpatinfo == null)
            {
                throw new FailedException("患者住院信息未找到");
            }

            if (settpatinfo.zybz == ((int)EnumZYBZ.Ycy).ToString())
            {
                throw new FailedCodeException("HOSP_CURRENTSTATUS_YCY_CANNOT_DO_OUTHOSPITAL_SETTLEMENT");
            }
            else if (settpatinfo.patId == 0)
            {
                throw new FailedCodeException("HOSP_SYSPATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (!settpatinfo.ryrq.HasValue || settpatinfo.ryrq <= new DateTime(1990, 01, 01))
            {
                throw new FailedCodeException("HOSP_ERROR_THERE_IS_NO_RYRQ");
            }
            //else if (string.IsNullOrWhiteSpace(settpatinfo.brxz))
            //{
            //    throw new FailedCodeException("HOSP_ERROR_PATIENT_NATURE_IS_NO_FOUND");
            //}
            else if (string.IsNullOrWhiteSpace(settpatinfo.ksmc))
            {
                throw new FailedCodeException("HOSP_ERROR_PATIENT_DEPT_IS_NO_FOUND");
            }
            //settpatinfo.zyts = Common.DateTimeHelper.GetInHospDays(settpatinfo.ryrq.Value, settpatinfo.cyrq.Value);
            //zybz 是否可结算 病区中的患者
            var isSettContainsBQZ = _sysConfigRepo.GetBoolValueByCode("HOSP_Sett_Contains_BQZ", this.OrganizeId ?? orgId, false).Value;
            var brzybzType = ((int)EnumZYBZ.Djz).ToString();
            if (isSettContainsBQZ)
            {
                brzybzType += "," + ((int)EnumZYBZ.Bqz).ToString();
            }
            if (!brzybzType.Contains(settpatinfo.zybz) && (string.IsNullOrEmpty(jslx) || (!string.IsNullOrEmpty(jslx) && jslx != "mnjs")))
            {
                throw new FailedException(((EnumZYBZ)(Convert.ToInt32(settpatinfo.zybz))).GetDescription() + "状态不能结算");
            }


            return settpatinfo;
        }

        /// <summary>
        /// 获取(计费和已退合计的)计费明细（包括治疗项目和药品）
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        private InpatientSettleItemBO GetInpatientSettleItemsDetailList(string zyh, DateTime? kssj, DateTime? jssj)
        {
            if (string.IsNullOrEmpty(zyh))
            {
                return null;
            }
            //zy_xmjfb
            var treatmentItemsList = _dischargeSettleDmnService.SelectTreatmentItemList(zyh, this.OrganizeId, kssj, jssj);
            //zy_ypjfb
            var drugList = _dischargeSettleDmnService.SelectDrugList(zyh, this.OrganizeId, kssj, jssj);
            //非治疗收费项目
            //var non_treatmentItemsList = _dischargeSettleDmnService.SelectNonTreatmentItemList(zyh, this.OrganizeId);

            var resultBo = new InpatientSettleItemBO()
            {
                TreatmentItemList = treatmentItemsList,
                DrugList = drugList
                //Non_treatmentItemList = non_treatmentItemsList
            };
            return resultBo;
        }

        private InpatientSettleItemBO GetInpatientSettleItemsDetailList(string zyh, DateTime? kssj, DateTime? jssj, string orgId)
        {
            if (string.IsNullOrEmpty(zyh))
            {
                return null;
            }
            //zy_xmjfb
            var treatmentItemsList = _dischargeSettleDmnService.SelectTreatmentItemList(zyh, orgId, kssj, jssj);
            //zy_ypjfb
            var drugList = _dischargeSettleDmnService.SelectDrugList(zyh, orgId, kssj, jssj);
            //非治疗收费项目
            //var non_treatmentItemsList = _dischargeSettleDmnService.SelectNonTreatmentItemList(zyh, this.OrganizeId);

            var resultBo = new InpatientSettleItemBO()
            {
                TreatmentItemList = treatmentItemsList,
                DrugList = drugList
                //Non_treatmentItemList = non_treatmentItemsList
            };
            return resultBo;
        }
        #region  医保数据传输
        public InpatientSettPatStatusDetailDto GetZyUploadDetail(string zyh, string jslx)
        {
            //获取病人信息
            var settpatInfo = GetInpatientSettlePatInfo(ref zyh, null, null, null, jslx);
            // 获取计费金额（包括：项目和药品）
            //var settleItemsBo = GetInpatientSettleJe(zyh,kssj,jssj,sczt);
            if (settpatInfo.cyrq.HasValue)
            {
                settpatInfo.zyts = DateTimeHelper.GetInHospDays(settpatInfo.ryrq.Value, settpatInfo.cyrq.Value);
            }
            var resultDto = new InpatientSettPatStatusDetailDto()
            {
                //InpatIentFeeSum= settleItemsBo,
                InpatientSettPatInfo = settpatInfo
            };
            return resultDto;

        }
        /// <summary>
        /// 医保费用传输zje
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public InpatIentFeeSumVo GetInpatientSettleJe(string zyh, string sczt, DateTime? kssj, DateTime jssj, string xmmc)
        {
            if (string.IsNullOrEmpty(zyh))
            {
                return null;
            }
            var FeeVo = _dischargeSettleDmnService.GetInpatSettFeeSum(zyh, this.OrganizeId, sczt, xmmc, kssj, jssj);
            var resultVo = new InpatIentFeeSumVo
            {
                zje = FeeVo
            };
            return resultVo;
        }

        #endregion
        /// <summary>
        /// 保存结算
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedcyrq"></param>
        /// <param name="fph"></param>
        /// <param name="feeRelated">金额及支付信息</param>
        /// <param name="ybfeeRelated">医保相关费用信息</param>
        public void SaveSett(string zyh, DateTime expectedcyrq, string fph, InpatientSettFeeRelatedDTO feeRelated
            , CQZyjs05Dto ybfeeRelated, S14OutResponseDTO xnhfeeRelated
            , string outTradeNo, string jslx, out int jsnm)
        {
            if (string.IsNullOrEmpty(zyh))
            {
                throw new FailedCodeException("HOSP_ZYH_IS_EMPTY");
            }

            //检查是否有未完成且未终止的执行计划
            //_hospAccountingPlanDetailRepo.CheckAccountingPlanDetailStatus(zyh, this.OrganizeId);

            // 获取病人信息
            var settpatInfo = GetInpatientSettlePatInfo(ref zyh);
            //获取最后一次结算 排除中途结算
            var dbDt = _BookkeepInHosDmnService.GetLastValidSettTime(zyh, this.OrganizeId);

            // 获取计费明细（包括：项目和药品）
            var settleItemsBo = GetInpatientSettleItemsDetailList(zyh, dbDt, null);

            //保存到数据库
            _dischargeSettleDmnService.SaveSett(settpatInfo, settleItemsBo, expectedcyrq, this.OrganizeId, fph, feeRelated, ybfeeRelated, xnhfeeRelated, outTradeNo, jslx, out jsnm);

        }
        public void SaveSett(string zyh, DateTime expectedcyrq, string fph, InpatientSettFeeRelatedDTO feeRelated
    , CQZyjs05Dto ybfeeRelated, S14OutResponseDTO xnhfeeRelated
    , string outTradeNo, string jslx, string orgId, out int jsnm)
        {
            if (string.IsNullOrEmpty(zyh))
            {
                throw new FailedCodeException("HOSP_ZYH_IS_EMPTY");
            }

            // 获取病人信息
            var settpatInfo = GetInpatientSettlePatInfo(ref zyh, null, null, null, null, orgId);
            //获取最后一次结算 排除中途结算
            var dbDt = _BookkeepInHosDmnService.GetLastValidSettTime(zyh, orgId);

            // 获取计费明细（包括：项目和药品）
            var settleItemsBo = GetInpatientSettleItemsDetailList(zyh, dbDt, null, orgId);

            //保存到数据库
            _dischargeSettleDmnService.SaveSett(settpatInfo, settleItemsBo, expectedcyrq, orgId, fph, feeRelated, ybfeeRelated, xnhfeeRelated, outTradeNo, jslx, out jsnm);

        }
        #endregion


        #region 取消出院结算

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public InpatientCancelSettPatStatusDetailDto GetCancelSettleStatusDetail(string zyh, string sfz, string kh, string cardType)
        {
            // 获取病人信息
            var settpatInfo = GetCancelSettlePatInfo(ref zyh, sfz, kh, cardType);
            //根据住院号 获取 （未退）结算列表， 取其最后一条
            var lastSett = _hospSettlementRepo.GetValidList(zyh, this.OrganizeId).OrderByDescending(p => p.CreateTime).FirstOrDefault();
            var medicalInsurance = _sysConfigRepo.GetValueByCode("Inpatient_MedicalInsurance", this.OrganizeId);
            HospSettlementGAYBFeeEntity ybFee = null;
            HospSettlementGAXNHFeeEntity xnhFee = null;
            CqybSett05Entity cqYbFee = null;
            if (settpatInfo.brxz == "1" && settpatInfo.CardType == ((int)EnumCardType.YBJYK).ToString())//医保患者
            {
                if (medicalInsurance == "guian")
                {
                    ybFee = lastSett != null ? _hospSettlementGAYBFeeRepo.IQueryable().Where(p => p.jsnm == lastSett.jsnm).FirstOrDefault() : null;
                }
                //if (medicalInsurance == "chongqing")
                //{
                // cqYbFee = lastSett != null ? _cqybSett05Repo.IQueryable().Where(p => p.jsnm == lastSett.jsnm && p.zt=="1").FirstOrDefault() : null;
                //}
            }
            if (settpatInfo.brxz == "8" && settpatInfo.CardType == ((int)EnumCardType.XNHJYK).ToString())//新农合患者
            {
                if (medicalInsurance == "guian")
                {
                    xnhFee = lastSett != null ? _hospSettlementGAXNHFeeRepo.IQueryable().Where(p => p.jsnm == lastSett.jsnm).FirstOrDefault() : null;
                }
            }
            var resultDto = new InpatientCancelSettPatStatusDetailDto()
            {
                InpatientSettPatInfo = settpatInfo,
                LastUnCancelledSett = lastSett,
                YbFee = ybFee,
                XnhFee = xnhFee
                //CqYbFee= cqYbFee
            };

            return resultDto;
        }

        /// <summary>
        /// 取消住院结算 获取病人基本信息（基本信息、账户、科室等） 在院状态验证
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public InpatientSettPatInfoVO GetCancelSettlePatInfo(ref string zyh, string sfz = null, string kh = null, string cardType = null)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                if (!string.IsNullOrWhiteSpace(kh) && !string.IsNullOrWhiteSpace(cardType))
                {
                    //根据卡号和卡类型 换取住院号
                    zyh = _hospPatientBasicInfoRepo.IQueryable().Where(p => p.kh == kh && p.CardType == cardType).OrderByDescending(p => p.CreateTime).Select(p => p.zyh).FirstOrDefault();
                }
                else if (!string.IsNullOrWhiteSpace(sfz) && !string.IsNullOrWhiteSpace(cardType))
                {
                    //根据身份证和卡类型 换取住院号
                    var zjlx = ((int)EnumZJLX.sfz).ToString();
                    zyh = _hospPatientBasicInfoRepo.IQueryable().Where(p => p.zjlx == zjlx && p.zjh == sfz && p.CardType == cardType).OrderByDescending(p => p.CreateTime).Select(p => p.zyh).FirstOrDefault();
                }
            }

            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }
            var settpatinfoList = _dischargeSettleDmnService.SelectInpatientSettPatInfo(zyh, this.OrganizeId);
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
            else if (settpatinfo.patId == 0)
            {
                throw new FailedCodeException("HOSP_SYSPATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (settpatinfo.ryrq <= new DateTime(1990, 01, 01))
            {
                throw new FailedCodeException("HOSP_ERROR_THERE_IS_NO_RYRQ");
            }
            //else if (string.IsNullOrWhiteSpace(settpatinfo.brxz))
            //{
            //    throw new FailedCodeException("HOSP_SYSPATIENT_BASICINFO_IS_NOT_EXIST");
            //}
            else if (string.IsNullOrWhiteSpace(settpatinfo.ksCode))
            {
                throw new FailedCodeException("HOSP_ERROR_PATIENT_DEPT_IS_NO_FOUND");
            }
            settpatinfo.zyts = DateTimeHelper.GetInHospDays(settpatinfo.ryrq.Value, settpatinfo.cyrq.Value);

            return settpatinfo;
        }

        /// <summary>
        /// 取消出院结算
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedjsnm"></param>
        /// <param name="cancelReason"></param>
        public void DoCancel(string zyh, int expectedjsnm, string cancelReason, string cancelyblsh, out string outTradeNo, out decimal refundAmount)
        {
            refundAmount = 0;
            outTradeNo = null;

            // 获取病人信息
            var settpatInfo = GetCancelSettlePatInfo(ref zyh);
            //根据住院号 获取 （未退）结算列表， 取其最后一条(zy_js)
            var lastSett = _hospSettlementRepo.GetValidList(zyh, this.OrganizeId).OrderByDescending(p => p.CreateTime).FirstOrDefault();
            if (lastSett.jsnm != expectedjsnm)
            {
                throw new FailedCodeException("HOSP_CANCELSETT_JSNM_IS_CHANGED");
            }
            //zy_jsmx
            var zyjsmxList = _hospSettlementDetailRepo.IQueryable().Where(p => p.jsnm == lastSett.jsnm).ToList();

            //计算退现金金额   //前提：仅一种支付方式    //仅一种支付方式js.xjzffs会有值
            if (!string.IsNullOrWhiteSpace(lastSett.xjzffs) && lastSett.xjzf > 0)
            {
                refundAmount = lastSett.xjzf;  //应该取支付的
                outTradeNo = lastSett.OutTradeNo;
            }

            //保存取消
            _dischargeSettleDmnService.DoCancel(lastSett, zyjsmxList, cancelReason, cancelyblsh, this.OrganizeId);

        }

        #endregion

        #region 模拟结算（GA）



        #endregion

        #region 重庆中途结算
        public void SavePartialSettle(string zyh, DateTime startTime, DateTime endTime, string fph, InpatientSettFeeRelatedDTO feeRelated
            , CQZyjs05Dto ybfeeRelated, string outTradeNo, string jslx, out int jsnm)
        {
            jsnm = 0;
            // 获取病人信息
            var settpatInfo = GetPartialInpatientSettlePatInfo(ref zyh);

            var ypxmlist = GetInpatientSettleItemsDetailList(zyh, startTime, endTime);
            //List<DrugFeeDetailVO> ypjfbList=null;
            //if (list!=null&&list.DrugList!=null&&list.DrugList.Count>0)
            //{
            //    ypjfbList = list.DrugList.ToList();
            //}
            //获取最后一次结算 结束结算日期
            var dbDt = _BookkeepInHosDmnService.GetLastValidMidwaySettTime(zyh, this.OrganizeId);
            if (dbDt.HasValue && dbDt.Value != startTime)
            {
                throw new FailedException("过程中结算状态发生变更，请重试!");
            }

            _dischargeSettleDmnService.commitPartialSettle(zyh, this.OrganizeId, startTime, endTime, feeRelated, ybfeeRelated, jslx, ypxmlist, out jsnm);
        }

        public InpatientSettPatStatusDetailDto GetPartialInpatientSettleStatusDetail(string zyh, string sfz, string kh, string cardType, string jslx, string ver)
        {
            // 获取病人信息
            var settpatInfo = GetPartialInpatientSettlePatInfo(ref zyh, sfz, kh, cardType, jslx);
            // 获取计费明细（包括：项目和药品）
            //var settleItemsBo = GetInpatientSettleItemsDetailList(zyh);
            settpatInfo.zyts = DateTimeHelper.GetInHospDays(settpatInfo.ryrq.Value, DateTime.Now);
            var resultDto = new InpatientSettPatStatusDetailDto()
            {
                InpatientSettPatInfo = settpatInfo,
                //InpatientSettleItemBO = settleItemsBo
                GroupFeeVOList = _dischargeSettleDmnService.GetHospGroupFeeVOList(zyh, this.OrganizeId, ver),
            };
            return resultDto;
        }

        /// <summary>
        /// 中途结算获取病人基本信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public InpatientSettPatInfoVO GetPartialInpatientSettlePatInfo(ref string zyh, string sfz = null, string kh = null, string cardType = null, string jslx = null)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                if (!string.IsNullOrWhiteSpace(kh) && !string.IsNullOrWhiteSpace(cardType))
                {
                    //根据卡号和卡类型 换取住院号
                    zyh = _hospPatientBasicInfoRepo.IQueryable().Where(p => p.kh == kh && p.CardType == cardType).OrderByDescending(p => p.CreateTime).Select(p => p.zyh).FirstOrDefault();
                }
                else if (!string.IsNullOrWhiteSpace(sfz) && !string.IsNullOrWhiteSpace(cardType))
                {
                    //根据身份证和卡类型 换取住院号
                    var zjlx = ((int)EnumZJLX.sfz).ToString();
                    zyh = _hospPatientBasicInfoRepo.IQueryable().Where(p => p.zjlx == zjlx && p.zjh == sfz && p.CardType == cardType).OrderByDescending(p => p.CreateTime).Select(p => p.zyh).FirstOrDefault();
                }
            }

            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }

            var settPatinfoList = _dischargeSettleDmnService.SelectInpatientSettPatInfo(zyh, this.OrganizeId);
            if (settPatinfoList == null || settPatinfoList.Count == 0)
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (settPatinfoList.Count > 1)
            {
                throw new FailedCodeException("HOSP_MATCHED_ERROR_MULTI_MATCHED");
            }
            var settpatinfo = settPatinfoList.First();

            if (settpatinfo == null)
            {
                throw new FailedException("患者住院信息未找到");
            }

            else if (settpatinfo.patId == 0)
            {
                throw new FailedCodeException("HOSP_SYSPATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (!settpatinfo.ryrq.HasValue || settpatinfo.ryrq <= new DateTime(1990, 01, 01))
            {
                throw new FailedCodeException("HOSP_ERROR_THERE_IS_NO_RYRQ");
            }
            else if (string.IsNullOrWhiteSpace(settpatinfo.ksmc))
            {
                throw new FailedCodeException("HOSP_ERROR_PATIENT_DEPT_IS_NO_FOUND");
            }

            return settpatinfo;
        }

        /// <summary>
        /// 转出院结算 预处理
        /// </summary>
        /// <param name="zyh"></param>
        public string PreCYsettle(string zyh)
        {
            //1.未停止的医嘱判断  
            _hospAccountingPlanDetailRepo.CheckAccountingPlanDetailStatus(zyh, this.OrganizeId);
            //2.在院状态判断
            GetInpatientSettlePatInfo(ref zyh);
            //3.是否产生新的费用判断 CheckIsExistUnSetted
            DateTime? startTime = null;
            startTime = _BookkeepInHosDmnService.GetLastValidMidwaySettTime(zyh, this.OrganizeId);
            if (!startTime.HasValue)
            {
                startTime = new DateTime(1970, 01, 01);  //未结算过的话 从入院开始算所有项目
            }
            var jsItemList = _hospItemBillingRepo.GetItemFeeEntityListByTime(zyh, this.OrganizeId, startTime.Value, DateTime.Now);
            if (jsItemList.Count() > 0)
            {
                throw new FailedException("存在未结费用，不能出院");
            }

            //4.更新病人基本信息
            _dischargeSettleDmnService.UpdatePatientInfo(zyh, this.OrganizeId);
            var jsnm = _dischargeSettleDmnService.GetCQLastJsnm(zyh, this.OrganizeId);
            var jylsh = _dischargeSettleDmnService.GetCQLastLsh(jsnm, this.OrganizeId);
            return jylsh;
        }
        #endregion



    }
}
