using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 住院结算 实现
    /// </summary>
    public class HospSettApp : AppBase, IHospSettApp
    {
        private readonly IHospPatientBasicInfoRepo _hospPatientBasicInfoRepo;
        private readonly IHospFeeDmnService _hospFeeDmnService;
        private readonly IHosPatAccDmnService _hosPatAccDmnService;
        private readonly ISysPatientComprehensiveNatureRepo _sysPatientComprehensiveNatureRepo;
        private readonly ISysPatientChargeRangeRepo _sysPatientChargeRangeRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly ISysPatientChargeAlgorithmRepo _sysPatientChargeAlgorithmRepo;
        private readonly ISysPatientNatureRepo _sysPatientNatureRepo;
        private readonly IOutPatientDmnService _outPatientDmnService;
        private readonly ISysCashPaymentModelRepo _sysCashPaymentModelRepo;
        private readonly IHospSettDmnService _hospSettDmnService;
        private readonly IIntegratedMaterialDmnService _integratedMaterialDmnService;

        /// <summary>
        /// 住院结算，查看患者住院 状态信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="kh"></param>
        public HosSettPatStatusDetailDto GetPatHospStatusDetail(string zyh, string kh)
        {
            //获取住院结算病人信息（基本信息、账户、科室等）
            var settpatinfo = getHospSettPatInfo(ref zyh, kh);

            //患者交款历史
            var chargeList = _hosPatAccDmnService.GetAccPayInfo((settpatinfo.zyyjjzh ?? 0), this.OrganizeId, zyh);
            if (chargeList.OrderByDescending(p => p.CreateTime).Select(p => p.zhye).FirstOrDefault() != chargeList.Select(p => p.szje).Sum())
            {
                //判断该次入院 有 预交金充值记录
                throw new FailedException("预交金账户出现收支与余额不一致<br/>请先核查");
            }

            //根据住院号获取计费项目（已按‘大类’分类）
            var dlFeeList = GetHospFeeClassifyWithDLBOList(zyh);
            if (dlFeeList == null || dlFeeList.Count == 0)
            {
                throw new FailedCodeException("HOSP_SETTLEMENTITEM_IS_NULL");
            }

            //验证 医保收费，必须消费医保项目/药品
            checkFeeValidWithBRXZ(dlFeeList, settpatinfo.brxz);

            var resultDto = new HosSettPatStatusDetailDto()
            {
                HospSettPatInfo = settpatinfo,
                ChargeList = chargeList,
                DLFeeList = dlFeeList,
                zhyjj = chargeList.Select(p => p.szje).Sum(),
                jsje = dlFeeList.Select(p => p.jsje).Sum(),
            };
            return resultDto;
        }

        /// <summary>
        /// 住院结算 病人 分类收费 预览
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedjsje">结算金额 防止过程中的费用变更（未除去减免的）</param>
        public HosSettPatClassifyChargePreviewDto GetHospSettPatClassifyChargePreview(string zyh, DateTime expectedcyrq, decimal expectedjsje)
        {
            //获取住院结算病人信息（基本信息、账户、科室等）
            var settpatinfo = getHospSettPatInfo(ref zyh, "");

            if (settpatinfo.cyrq.Value.Date != expectedcyrq.Date)
            {
                throw new FailedCodeException("HOSP_OUTHOSPITAL_CYRQ_IS_CHANGED");
            }

            //住院帐户信息 预交金账户余额    //患者交款历史
            var zhLastCharge = _hosPatAccDmnService.GetAccPayInfo((settpatinfo.zyyjjzh ?? 0), this.OrganizeId, zyh).OrderByDescending(p => p.CreateTime).FirstOrDefault();
            if (zhLastCharge == null)
            {
                //判断该次入院 有 预交金充值记录
                throw new FailedCodeException("HOSP_PATIENTACCOUNT_REVENUEANDEXPENSE_IS_NULL");
            }

            var resultDto = new HosSettPatClassifyChargePreviewDto();

            resultDto.expectedcyrq = settpatinfo.cyrq.Value;

            //need organizeId
            resultDto.fph = _outPatientDmnService.GetInvoiceListByEmpNo(this.UserIdentity.UserCode, this.OrganizeId);

            //根据住院号获取计费项目（已按‘大类’分类）
            var dlFeeList = GetHospFeeClassifyWithDLBOList(zyh);
            if (dlFeeList == null || dlFeeList.Count == 0)
            {
                throw new FailedCodeException("HOSP_SETTLEMENTITEM_IS_NULL");
            }

            if (dlFeeList.Select(p => p.jsje).Sum() != expectedjsje)
            {
                throw new FailedCodeException("HOSP_CHARGEITEM_IS_CHANGED");
            }
            resultDto.expectedjsje = expectedjsje;
            resultDto.zyh = zyh;

            //验证 医保收费，必须消费医保项目/药品
            checkFeeValidWithBRXZ(dlFeeList, settpatinfo.brxz);

            //分组（病人性质）结算
            var zyjyjsList = getHospFeeJieSuanGroupedByBrxzList(settpatinfo, dlFeeList);

            //得 记账、分类自负、自费、自负、其他、应收款、找零、舍入差额、实收款
            //病人性质决定zyjyjsList.Count
            foreach (HospFeeItemOrMedicinListGroupedByBrxzBO zyjyjs in zyjyjsList)
            {
                //即对应了一交易‘zy_jyjs’
                //#########仅自费逻辑
                foreach (var fee in zyjyjs.FeeList)
                {
                    resultDto.yingshoukuan += fee.sfjsfymx.total;   //会因为组合病人性质项目重复计算？
                    resultDto.jzfy += fee.sfjsfymx.jzfy;
                    resultDto.flzf += fee.sfjsfymx.flzf;
                    resultDto.zifu += fee.sfjsfymx.sfzf;
                    resultDto.zifei += fee.sfjsfymx.zl;
                    resultDto.jmje += fee.sfjsfymx.jmje;
                }
                resultDto.yingshoukuan = resultDto.yingshoukuan + zyjyjs.jzce;    //现金加上记账超额部分
                resultDto.jzfy = resultDto.jzfy - zyjyjs.jzce;    //可记账费用减去超额部分
                resultDto.zifu = resultDto.zifu + zyjyjs.jzce;    //自负加上记账超额部分
                resultDto.srce = 0; //住院无现金分币误差
            }

            resultDto.ssk = resultDto.zhye = Convert.ToDecimal(zhLastCharge.zhye);  //实收款
            //如果住院帐户余额<现金支付，则剩余部分用其他支付；如果>现金支付，则多余部分用于找零

            resultDto.zhaoling = resultDto.zhye - resultDto.yingshoukuan;

            resultDto.HospSettPatInfo = settpatinfo;

            return resultDto;
        }

        /// <summary>
        /// 住院结算 提交结算，保存结果
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedjsje">结算金额 防止过程中的费用变更（未除去减免的）</param>
        /// <param name="expectedzhaoling"></param>
        /// <param name="xjzfListStr"></param>
        public SettSaveSuccessResultDto SaveSett(string zyh, DateTime expectedcyrq, string fph, decimal expectedyjjzhye, decimal expectedjsje, decimal expectedzhaoling, string xjzfListStr, decimal shishoukuan)
        {
            if (string.IsNullOrWhiteSpace(fph))
            {
                throw new FailedCodeException("HOSP_PLEASE_CHOOSE_FINO");
            }
            if (_outPatientDmnService.ExistsForInvoiceNo(fph, this.OrganizeId))
            {
                throw new FailedCodeException("HOSP_FINO_IS_USED");
            }
            if (expectedzhaoling < 0)
            {
                throw new FailedCodeException("HOSP_ZHAOLING_IS_NEGATIVE");
            }

            //获取住院结算病人信息（基本信息、账户、科室等）
            var settpatinfo = getHospSettPatInfo(ref zyh, "");

            if (settpatinfo.cyrq.Value.Date != expectedcyrq.Date)
            {
                throw new FailedCodeException("HOSP_OUTHOSPITAL_CYRQ_IS_CHANGED");
            }

            //住院帐户信息 预交金账户余额    //患者交款历史
            var zhLastCharge = _hosPatAccDmnService.GetAccPayInfo((settpatinfo.zyyjjzh ?? 0), this.OrganizeId, zyh).OrderByDescending(p => p.CreateTime).FirstOrDefault();
            if (zhLastCharge == null)
            {
                throw new FailedCodeException("HOSP_PATIENTACCOUNT_REVENUEANDEXPENSE_IS_NULL");
            }

            if (zhLastCharge.zhye != expectedyjjzhye)
            {
                throw new FailedCodeException("HOSP_PATIENTACCOUNT_BALANCE_IS_CHANGED");
            }

            //根据住院号获取计费项目（已按‘大类’分类）
            var dlFeeList = GetHospFeeClassifyWithDLBOList(zyh);
            if (dlFeeList == null || dlFeeList.Count == 0)
            {
                throw new FailedCodeException("HOSP_SETTLEMENTITEM_IS_NULL");
            }

            if (dlFeeList.Select(p => p.jsje).Sum() != expectedjsje)
            {
                throw new FailedCodeException("HOSP_CHARGEITEM_IS_CHANGED");
            }

            //验证 医保收费，必须消费医保项目/药品
            checkFeeValidWithBRXZ(dlFeeList, settpatinfo.brxz);

            //分组（病人性质）结算
            var zyjyjsList = getHospFeeJieSuanGroupedByBrxzList(settpatinfo, dlFeeList);

            //数据库变更对象
            var sqlVO = getSettDBUpdateVO(settpatinfo, fph, expectedyjjzhye, expectedzhaoling, zyjyjsList, xjzfListStr, zhLastCharge.xjzffs);

            if ((sqlVO.zy_js.zl ?? 0) != expectedzhaoling)
            {
                throw new FailedException("找零金额发生变更，请重新结算");
            }

            //保存至数据库
            _hospSettDmnService.SettSyncToDB(sqlVO, this.OrganizeId);

            var reslt = new SettSaveSuccessResultDto()
            {
                yingshoukuan = sqlVO.zy_js.zje.ToString("0.00"),
                ssk = shishoukuan.ToString("0.00"),
                srce = sqlVO.zy_js.xjwc.ToString("0.00"),
                zhaoling = (sqlVO.zy_js.zl ?? 0).ToString("0.00"),
            };

            return reslt;
        }

        /// <summary>
        /// 根据住院号获取 本次结算 计费 按大类（分类项目）
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<HospFeeClassifyWithDLBO> GetHospFeeClassifyWithDLBOList(string zyh)
        {
            var itemsList = getZyItemsVOList(zyh);
            var ypList = getZyMedicinVOList(zyh);

            var itemBOList = itemsList.GroupBy(p => new { p.dl, p.dlmc }).Select(p => new HospFeeClassifyWithDLBO()
            {
                dl = p.Key.dl,
                dlmc = p.Key.dlmc,
                jsje = p.Select(i => (i.fwfdj + i.dj) * i.sl).Sum(),    //结算金额
                ItemFeeList = p.ToList()
            });

            var medicinBOList = ypList.GroupBy(p => new { p.dl, p.dlmc }).Select(p => new HospFeeClassifyWithDLBO()
            {
                dl = p.Key.dl,
                dlmc = p.Key.dlmc,
                jsje = p.Select(i => (i.dj) * i.sl).Sum(),
                MedicinFeeList = p.ToList()
            });

            return itemBOList.Union(medicinBOList).ToList();
        }

        #region private method

        /// <summary>
        /// 获取项目计费列表
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        private IList<HospItemFeeDetailVO> getZyItemsVOList(string zyh)
        {
            return _hospFeeDmnService.GetAllItemFeeDetailVOList(zyh, this.OrganizeId);
        }

        /// <summary>
        /// 获取药品计费列表
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        private IList<HospMedicinFeeDetailVO> getZyMedicinVOList(string zyh)
        {
            return _hospFeeDmnService.GetAllMedicinFeeDetailVOList(zyh, this.OrganizeId);
        }

        /// <summary>
        /// 住院结算 获取病人基本信息（基本信息、账户、科室等） 在院状态验证
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        private HospSettPatInfoVO getHospSettPatInfo(ref string zyh, string kh)
        {
            if (string.IsNullOrWhiteSpace(zyh) && !string.IsNullOrWhiteSpace(kh))
            {
                var zyList = _hospPatientBasicInfoRepo.IQueryable().Where(p => p.OrganizeId == this.OrganizeId && kh.Equals(p.kh)).ToList();
                if (zyList.Count == 0)
                {
                    throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
                }
                if (zyList.Count(p => p.zybz != ((int)EnumZYBZ.Wry).ToString()
                    && p.zybz != ((int)EnumZYBZ.Ycy).ToString()) > 1)
                {
                    throw new FailedCodeException("HOSP_CARDNO_MULTI_ZYH_PLEASE_USE_ZYH");
                }
                else
                {
                    //这里 可结算 记录已经不会多余一条
                    zyh = zyList.Where(p => p.zybz != ((int)EnumZYBZ.Wry).ToString()
                            && p.zybz != ((int)EnumZYBZ.Ycy).ToString()).Select(p => p.zyh).FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(zyh)) //说明没有可结算的记录
                    {
                        zyh = zyList.OrderByDescending(p => p.CreateTime).Select(p => p.zyh).First();
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }
            var settPatinfoList = _hosPatAccDmnService.GetHospSettPatInfo(zyh, this.OrganizeId);
            if (settPatinfoList == null || settPatinfoList.Count == 0)
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (settPatinfoList.Count > 1)
            {
                throw new FailedCodeException("HOSP_MATCHED_ERROR_MULTI_MATCHED");
            }
            var patinfo = settPatinfoList.First();
            if (patinfo.zybz == ((int)EnumZYBZ.Wry).ToString())
            {
                throw new FailedCodeException("HOSP_CURRENTSTATUS_WRY_CANNOT_DO_OUTHOSPITAL_SETTLEMENT");
            }
            else if (patinfo.zybz == ((int)EnumZYBZ.Bqz).ToString())
            {
                //############从其他系统（繁云）同步在院状态 假设同步成功了 可以出院 获取到了‘cybz’为1，且‘出院时间’
                patinfo.zybz = ((int)EnumZYBZ.Djz).ToString(); //待结账
                //throw new FailedCodeException("HOSP_CURRENTSTATUS_BQZ_CANNOT_DO_OUTHOSPITAL_SETTLEMENT");
            }
            else if (patinfo.zybz == ((int)EnumZYBZ.Ycy).ToString())
            {
                throw new FailedCodeException("HOSP_CURRENTSTATUS_YCY_CANNOT_DO_OUTHOSPITAL_SETTLEMENT");
            }
            else if (patinfo.patid == 0)
            {
                throw new FailedCodeException("HOSP_SYSPATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (!patinfo.ryrq.HasValue || patinfo.ryrq <= new DateTime(1990, 01, 01))
            {
                throw new FailedCodeException("HOSP_ERROR_THERE_IS_NO_RYRQ");
            }
            else if (string.IsNullOrWhiteSpace(patinfo.brxz))
            {
                throw new FailedCodeException("HOSP_ERROR_PATIENT_NATURE_IS_NO_FOUND");
            }
            else if (string.IsNullOrWhiteSpace(patinfo.ksmc))
            {
                throw new FailedCodeException("HOSP_ERROR_PATIENT_DEPT_IS_NO_FOUND");
            }
            else if (patinfo.zyyjjzh <= 0)
            {
                throw new FailedCodeException("HOSP_ERROR_PATIENT_THERE_IS_NO_YJJ_ACCOUNT");
            }
            ////############从其他系统（繁云）同步在院状态 假设同步成功了 可以出院 获取到了‘cybz’为1，且‘出院时间’
            //patinfo.zybz = ((int)EnumZYBZ.Djz).ToString(); //待结账

            //不需要同步到数据库中    //不同时辰，会有不同的‘住院天数’
            patinfo.cyrq = Convert.ToDateTime(DateTime.Now.AddMinutes(-30).ToString());

            patinfo.zyts = DateTimeHelper.GetInHospDays(patinfo.ryrq.Value, patinfo.cyrq.Value);

            //只有新入院和待结帐状态才允许结账
            if (patinfo.zybz != ((int)EnumZYBZ.Xry).ToString()
                && patinfo.zybz != ((int)EnumZYBZ.Djz).ToString())
            {
                throw new FailedCodeException("HOSP_CURRENTSTATUS_NOT_XRY_OR_DJX_CANNOT_DO_OUTHOSPITAL_SETTLEMENT");
            }

            if (patinfo.ybjylx == ((int)EnumYBJYLX.ybjylx6).ToString())
            {
                if (patinfo.ryrq.Value.Year != patinfo.cyrq.Value.Year)
                {
                    throw new FailedCodeException("HOSP_NB_PATIENT_CANNOT_CROSS_YEAR_DO_SETTLEMENT");
                }
            }
            return patinfo;
        }

        /// <summary>
        /// 验证 医保收费，必须消费医保项目/药品
        /// </summary>
        /// <param name="dlFeeList"></param>
        /// <param name="brxz"></param>
        private void checkFeeValidWithBRXZ(IList<HospFeeClassifyWithDLBO> dlFeeList, string brxz)
        {
            if (dlFeeList == null || dlFeeList.Count == 0 || string.IsNullOrWhiteSpace(brxz))
            {
                return;
            }
            //住院医保收费病人性质， 医保收费 必须消费医保项目/药品
            var pzzyZYYBSFBRXZ = _sysConfigRepo.GetValueByCode(Constants.xtzypz.ZYYBSFBRXZ, this.OrganizeId);
            if (!string.IsNullOrWhiteSpace(pzzyZYYBSFBRXZ) && pzzyZYYBSFBRXZ.Contains(brxz))
            {
                if (dlFeeList.Any(p => (
                 (p.ItemFeeList != null && p.ItemFeeList.Any(i => string.IsNullOrWhiteSpace(i.ybbm)))
                 || (p.MedicinFeeList != null && p.MedicinFeeList.Any(i => string.IsNullOrWhiteSpace(i.ybbm)))
                 )))
                {
                    throw new FailedCodeException("HOSP_YB_PATIENT_CANNOT_BILLING_NOTYB");
                }
            }
            else
            {
                //非医保收费病人
            }
        }

        /// <summary>
        /// 获取住院费用结算分组 by 病人性质 => 分组（病人性质）结算
        /// </summary>
        /// <param name="settPatInfoVO">病人信息（包括住院基本信息、系统基本信息、账户、科室等）</param>
        /// <param name="classifyFeeList">项目计费、分类计费</param>
        /// <returns></returns>
        private IList<HospFeeItemOrMedicinListGroupedByBrxzBO> getHospFeeJieSuanGroupedByBrxzList(HospSettPatInfoVO settPatInfoVO, IList<HospFeeClassifyWithDLBO> classifyFeeList)
        {
            //病人（多）性质 List ，即 组合病人性质    //这里的排序很重要.OrderByDescending(p => p.jssx)
            var brxzList = _sysPatientComprehensiveNatureRepo.GetListByZbrxz(settPatInfoVO.brxz, this.OrganizeId);
            if (brxzList == null || brxzList.Count == 0)
            {
                brxzList.Add(new SysPatientComprehensiveNatureEntity()
                {
                    zbrxz = settPatInfoVO.brxz,
                    cbrxz = settPatInfoVO.brxz
                });
            }

            //项目/药品 计费 ‘按 病人性质’ 分组结算 列表（只是分组了计费项目/药品）
            var feeGedByBrxzList = getHospFeeListGroupedByBrxzBO(brxzList, classifyFeeList);

            //给项目/药品计费表Detail加上‘是否医保’的标志位（位 赋值）
            divisionToYBandFYB(feeGedByBrxzList);

            //分别计算各项目/药品的费用
            jifeiToBrxzFeeList(feeGedByBrxzList);

            //起付    ############暂未涉及 起付

            //处理报销上限
            getJZCE(settPatInfoVO.ryzdicd10, feeGedByBrxzList);

            return feeGedByBrxzList;
        }

        /// <summary>
        /// 项目/药品 计费 ‘按 病人性质’ 分组结算 列表 。‘如果是单一病人性质（非组合病人性质），会返回一条’
        /// </summary>
        /// <returns></returns>
        private IList<HospFeeItemOrMedicinListGroupedByBrxzBO> getHospFeeListGroupedByBrxzBO(IList<SysPatientComprehensiveNatureEntity> brxzList, IList<HospFeeClassifyWithDLBO> classifyFeeList)
        {
            var brxzFeeGroupList = new List<HospFeeItemOrMedicinListGroupedByBrxzBO>();
            foreach (var zhbrxzEntity in brxzList)
            {
                var brxzFeeGroup = new HospFeeItemOrMedicinListGroupedByBrxzBO()
                {
                    brxzzhEntity = zhbrxzEntity,
                    brxzEntity = _sysPatientNatureRepo.IQueryable().Where(p => p.brxz == zhbrxzEntity.cbrxz && p.zt == "1").FirstOrDefault(),
                    FeeList = new List<HospFeeItemOrMedicinDetailBO>(),
                };
                if (brxzFeeGroup.brxzEntity == null)
                {
                    throw new FailedCodeException("HOSP_PATIENT_NATURE_IS_NOT_EXIST");
                }
                //获取有效的病人收费范围 xt_brsffw
                var patChargeRangeList = _sysPatientChargeRangeRepo.IQueryable().Where(p => p.brxz == zhbrxzEntity.cbrxz && p.OrganizeId == this.OrganizeId && p.zt == "1").ToList();
                //所以这个配置很关键，容易导致项目不计费

                foreach (var dlFee in classifyFeeList)
                {
                    if (dlFee.ItemFeeList != null && dlFee.ItemFeeList.Count > 0)
                    {
                        foreach (var itemFee in dlFee.ItemFeeList)
                        {
                            if (zhbrxzEntity.sfpc == "1" && (
                                brxzFeeGroup.FeeList.Any(p => p.Item != null && p.Item.jfbbh == itemFee.jfbbh)
                                || brxzFeeGroupList.Any(p => p.FeeList.Any(i => i.Item != null && i.Item.jfbbh == itemFee.jfbbh))
                                ))
                            {
                                continue;   //以计划计算 且配置为 不 重复计算
                            }
                            if (patChargeRangeList == null || patChargeRangeList.Count == 0)
                            {
                                brxzFeeGroup.FeeList.Add(new HospFeeItemOrMedicinDetailBO()
                                {
                                    dl = itemFee.dl,
                                    dlmc = itemFee.dlmc,
                                    Item = itemFee
                                });
                            }
                            //有匹配的收费项目
                            else
                            {
                                //sfxm一致 或 dl一样，fw配置的sfxm不作限制
                                if (patChargeRangeList.Any(p => p.sfxm == itemFee.sfxm) || patChargeRangeList.Any(p => p.dl == itemFee.dl && (string.IsNullOrEmpty(p.sfxm) || p.sfxm == "0")))
                                {
                                    brxzFeeGroup.FeeList.Add(new HospFeeItemOrMedicinDetailBO()
                                    {
                                        dl = itemFee.dl,
                                        dlmc = itemFee.dlmc,
                                        Item = itemFee
                                    });
                                }
                                //病人性质有收费范围配置，但该项（收费项目/药品）未配置，则不计算该项
                            }
                        }
                    }
                    if (dlFee.MedicinFeeList != null && dlFee.MedicinFeeList.Count > 0)
                    {
                        foreach (var medicinFee in dlFee.MedicinFeeList)
                        {
                            if (zhbrxzEntity.sfpc == "1" && (
                                brxzFeeGroup.FeeList.Any(p => p.Medicin != null && p.Medicin.jfbbh == medicinFee.jfbbh)
                               || brxzFeeGroupList.Any(p => p.FeeList.Any(i => i.Medicin != null && i.Medicin.jfbbh == medicinFee.jfbbh))
                               ))
                            {
                                continue;
                            }
                            if (patChargeRangeList == null || patChargeRangeList.Count == 0)
                            {
                                brxzFeeGroup.FeeList.Add(new HospFeeItemOrMedicinDetailBO()
                                {
                                    dl = medicinFee.dl,
                                    dlmc = medicinFee.dlmc,
                                    Medicin = medicinFee
                                });
                            }
                            //有匹配的收费项目
                            else
                            {
                                //sfxm一致 或 dl一样，fw配置的sfxm不作限制
                                if (patChargeRangeList.Any(p => p.dl == medicinFee.dl && (string.IsNullOrEmpty(p.sfxm) || p.sfxm == "0")))
                                {
                                    brxzFeeGroup.FeeList.Add(new HospFeeItemOrMedicinDetailBO()
                                    {
                                        dl = medicinFee.dl,
                                        dlmc = medicinFee.dlmc,
                                        Medicin = medicinFee
                                    });
                                }
                                //病人性质有收费范围配置，但该项（收费项目/药品）未配置，则不计算该项
                            }
                        }
                    }
                }
                brxzFeeGroupList.Add(brxzFeeGroup);
            }
            return brxzFeeGroupList;
        }

        /// <summary>
        /// 给‘是否需要医保交易’标志位赋值
        /// </summary>
        /// <param name="boList"></param>
        private void divisionToYBandFYB(IList<HospFeeItemOrMedicinListGroupedByBrxzBO> brxzFeeGroupList)
        {
            //获取门诊配置：不上传医保大类
            var pzzyBSCYBStr = _sysConfigRepo.GetValueByCode(Constants.xtzypz.DL_BSCYB, this.OrganizeId) ?? "";

            string[] arrDl = pzzyBSCYBStr.TrimEnd(',').Split(',').Where(p => !string.IsNullOrWhiteSpace(pzzyBSCYBStr)).ToArray();
            Dictionary<string, string> dlDict = new Dictionary<string, string>();
            foreach (string dl in arrDl)
            {
                if (dlDict.ContainsKey(dl))
                {
                    continue;
                }
                dlDict.Add(dl, dl);
            }

            foreach (var brxzFeeGroup in brxzFeeGroupList)
            {
                foreach (var fee in brxzFeeGroup.FeeList)
                {
                    if (dlDict.Count == 0)
                    {
                        fee.isNeedYBJY = true;
                    }
                    else if (fee.Item != null && dlDict.ContainsKey(fee.dl))
                    {
                        fee.isNeedYBJY = false;
                    }
                    else if (fee.Medicin != null && dlDict.ContainsKey(fee.dl))
                    {
                        fee.isNeedYBJY = false;
                    }
                    else
                    {
                        fee.isNeedYBJY = true;
                    }
                }
            }
        }

        /// <summary>
        /// 单独计算各项目/药品的费用 => 记账、分类自负、自费、自负、其他
        /// </summary>
        /// <param name="brxzFeeGroupList"></param>
        private void jifeiToBrxzFeeList(IList<HospFeeItemOrMedicinListGroupedByBrxzBO> brxzFeeGroupList)
        {
            //一次性材料大类配置
            var pzzyYCXCLDMValue = _sysConfigRepo.GetValueByCode(Constants.xtzypz.YCXCLDM, this.OrganizeId);
            string[] arrYCXCLDl = pzzyYCXCLDMValue.Split(',').Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();

            foreach (var brxzFeeGroup in brxzFeeGroupList)
            {
                foreach (var fee in brxzFeeGroup.FeeList)
                {
                    jifeiToHospFeeItemOrMedicinDetailBO(fee, arrYCXCLDl);   //这一步和当前病人无关
                }
                //切入 收费算法（病人性质） //算法列表按级别排序
                var brxzSfsfList = _sysPatientChargeAlgorithmRepo.IQueryable().Where(p => p.brxz == brxzFeeGroup.brxzzhEntity.cbrxz && p.OrganizeId == this.OrganizeId && p.zt == "1").OrderByDescending(p => p.sfjb).ToList();
                foreach (var fee in brxzFeeGroup.FeeList)
                {
                    jifeiWithBrsfsfToHospFeeItemOrMedicinDetailBO(fee, brxzSfsfList);
                }
            }
        }

        /// <summary>
        /// 简要计算 => 自理金额（自负性质 为 自费） 、可报金额 、分类自负
        /// </summary>
        /// <param name="fee"></param>
        /// <param name="arrYCXCLDl">一次性材料大类</param>
        private void jifeiToHospFeeItemOrMedicinDetailBO(HospFeeItemOrMedicinDetailBO fee, string[] arrYCXCLDl)
        {
            if (fee.Item != null)
            {
                var xm = fee.Item;  //项目计费
                fee.jmhje = xm.je - xm.jmje;    //减免后金额

                #region 一次性材料收费项目判断
                if (arrYCXCLDl != null && arrYCXCLDl.Contains(fee.dl))
                {
                    var sfclxmzhEntity = _integratedMaterialDmnService.GetSfclxmzhEntity(fee.Item.sfxm, this.OrganizeId);
                    if (sfclxmzhEntity != null)
                    {
                        fee.sfclxmzhEntity = sfclxmzhEntity;
                        fee.zlfy = fee.jmhje;   //先全部记入自理
                        return;
                    }
                }
                #endregion

                //根据项目收费性质
                if (xm.zfxz == ((int)EnumZiFuXingZhi.ZF).ToString())
                {
                    fee.zlfy = fee.jmhje;   //自费 则 减免后金额就是 自理金额
                }
                else if (xm.zfxz == ((int)EnumZiFuXingZhi.KB).ToString())
                {
                    fee.kbje = fee.jmhje;   //可记账金额
                }
                else
                {
                    //可报：费用×（1－自负比例）
                    //分类自负：费用×自负比例
                    if (xm.zfbl >= 0)
                    {
                        fee.kbje = (fee.jmhje * (1 - xm.zfbl));    //可记账金额
                        //如果某个药是乙10%，单价100元，意思就是说，这个药首先10%是需要个人承担的
                        //这10块钱就是’分类自负‘
                        //另90块尚不是所谓的’自负‘，    要根据个人的报销比例医保和个人按比例承担
                        fee.flzf = (fee.jmhje * xm.zfbl);  //分类自负金额
                    }
                    //当自负比例为负数时，表示定额自负
                    else
                    {
                        if (xm.jmbl < 0)
                        {
                            throw new FailedException(xm.jfbbh + "定额自负，减免比例不能为负数，请重新配置");
                        }
                        //减免比例通常0，所以 flzf 一般都是 定额*数量
                        //分类自负金额: 定额 * （1-减免比例）* 数量
                        //可说明 .jmbl 是 在 定额自负 时 才有意义
                        fee.flzf = (-xm.zfbl) * (1 - xm.jmbl) * xm.sl;
                        fee.kbje = fee.jmhje - fee.flzf;    //可记账金额
                    }
                }
            }
            else if (fee.Medicin != null)
            {
                var medicin = fee.Medicin;
                fee.jmhje = medicin.je - medicin.jmje;
                if (medicin.zfxz == ((int)EnumZiFuXingZhi.ZF).ToString())
                {
                    fee.zlfy = fee.jmhje;
                }
                else if (medicin.zfxz == ((int)EnumZiFuXingZhi.KB).ToString())
                {
                    fee.kbje = fee.jmhje;
                }
                else
                {
                    if (medicin.zfbl >= 0)
                    {
                        fee.kbje = (fee.jmhje * (1 - medicin.zfbl));
                        fee.flzf = (fee.jmhje * medicin.zfbl);
                    }
                    else
                    {
                        if (medicin.jmbl < 0)
                        {
                            throw new FailedException(medicin.jfbbh + "定额自负，减免比例不能为负数，请重新配置");
                        }
                        fee.flzf = (-medicin.zfbl) * (1 - medicin.jmbl) * medicin.sl;
                        fee.kbje = fee.jmhje - fee.flzf;
                    }
                }
            }

        }

        /// <summary>
        /// 根据收费项目合计和病人‘‘‘‘收费算法’’’’计算
        /// </summary>
        /// <param name="fee"></param>
        /// <param name="brxzSfsfList">病人性质对应的收费算法列表</param>
        private void jifeiWithBrsfsfToHospFeeItemOrMedicinDetailBO(HospFeeItemOrMedicinDetailBO fee, IList<SysPatientChargeAlgorithmEntity> brxzSfsfList)
        {
            //项目（药品）匹配的收费算法
            IList<SysPatientChargeAlgorithmEntity> sfsfList = null;
            if (fee.Item != null)
            {
                var xm = fee.Item;  //项目计费
                sfsfList = brxzSfsfList.Where(p => p.sfxm == xm.sfxm).ToList();
            }
            if (sfsfList == null || sfsfList.Count == 0)
            {
                sfsfList = brxzSfsfList.Where(p => p.sfxm == "" && (p.dl == fee.dl || (p.dl ?? "").Trim() == "*")).ToList();
            }

            if (sfsfList == null || sfsfList.Count == 0)    //如果没有病人收费算法
            {
                //没有设置算法 费用合计 = 可记账＋分类自负＋自理
                fee.sfjsfymx.total = fee.kbje + fee.flzf + fee.zlfy;
                //分类自负
                fee.sfjsfymx.flzf = fee.flzf;
                //记账费用
                fee.sfjsfymx.jzfy = fee.kbje;
                //自理费用
                fee.sfjsfymx.zl = fee.zlfy;
                //现金
                fee.sfjsfymx.xj = fee.flzf + fee.zlfy;  //现金支付 = 分类自负 + 自费
            }
            else
            {
                if (sfsfList.Select(p => p.fyfw).Distinct().Count() > 1)
                {
                    throw new FailedCodeException("HOSP_PATIENT_NATURE_AND_CATEGORY_THE_SUANFALIST_IS_NOT_SAME_RANGE");
                }

                #region 费用合计    根据病人不同的收费算法，=>现金、记账费用、分类自负、自理
                if (sfsfList[0].fyfw == Constants.xtbrsfsffyfw.fyfw0)
                {
                    //交易费用总额 = 医保结算范围费用总额 - 分类自负；现金：分类自负+自理+自负
                    fee.sfjsfymx.jzfy = fee.kbje; //记账费用：可记帐
                    fee.sfjsfymx.flzf = fee.flzf; //分类自负
                    fee.sfjsfymx.zl = fee.zlfy;   //自理费用
                    fee.sfjsfymx.xj = fee.flzf + fee.zlfy;    //现金
                }
                else if (sfsfList[0].fyfw == Constants.xtbrsfsffyfw.fyfw1)
                {
                    //交易费用总额 = 医保结算范围费用总额；现金：自理+自负
                    fee.sfjsfymx.jzfy = fee.kbje + fee.flzf;  //记账费用：可记帐 + 分类自负
                    fee.sfjsfymx.flzf = 0m;   //分类自负
                    fee.sfjsfymx.zl = fee.zlfy;   //自理费用
                    fee.sfjsfymx.xj = fee.zlfy;   //现金
                }
                else if (sfsfList[0].fyfw == Constants.xtbrsfsffyfw.fyfw2)
                {
                    //交易费用总额 = 医保结算范围费用总额；现金：自负
                    fee.sfjsfymx.jzfy = fee.kbje + fee.flzf + fee.zlfy;   //记账费用：可记帐 + 分类自负 + 自理
                    fee.sfjsfymx.flzf = 0m;   //分类自负
                    fee.sfjsfymx.zl = 0m; //自理费用
                    fee.sfjsfymx.xj = 0m; //现金
                }
                else if (sfsfList[0].fyfw == Constants.xtbrsfsffyfw.fyfw3)
                {
                    //交易费用总额 = 医保结算范围费用总额；现金：自负
                    fee.sfjsfymx.jzfy = fee.kbje + fee.flzf + fee.zlfy;   //记账费用：可记帐 + 分类自负 + 自理 + 绝对自费
                    fee.sfjsfymx.flzf = 0m;   //分类自负
                    fee.sfjsfymx.zl = 0m; //自理费用
                    fee.sfjsfymx.xj = 0m; //现金
                }
                #endregion

                #region 费用计算
                decimal jsAmount = fee.sfjsfymx.jzfy;
                decimal jsedAmount = 0m;    //根据费用范围需要计算的金额
                decimal sfzf = 0m;  //算法自负（自负性质为自理时 记入’算法自理‘）
                //如果有费用上限，则根据算法级别计算(实际只需计算自理费用) 
                foreach (var sfEntity in sfsfList)
                {
                    var sl = (fee.Item != null ? fee.Item.sl : fee.Medicin.sl);
                    decimal sffysx = sfEntity.fysx * sl;   //费用上限
                    if (sffysx > fee.sfjsfymx.jzfy)
                    {
                        sfzf += jsAmount == 0 ? 0
                                  : (sfEntity.zfbl >= 0 ? jsAmount * sfEntity.zfbl : (-sfEntity.zfbl * sl));
                        jsedAmount += jsAmount;
                        jsAmount = fee.sfjsfymx.jzfy - jsedAmount;
                        break;
                    }
                    else
                    {
                        if (sffysx > jsAmount)
                        {
                            jsedAmount += sffysx - jsedAmount;
                            sfzf += jsAmount == 0 ? 0
                                : (sfEntity.zfbl >= 0 ? jsAmount * sfEntity.zfbl : (-sfEntity.zfbl * sl));
                        }
                        else
                        {
                            if (sffysx == 0)
                            {
                                sfzf += jsAmount == 0 ? 0
                                    : (sfEntity.zfbl >= 0 ? jsAmount * sfEntity.zfbl : (-sfEntity.zfbl * sl));
                                jsedAmount = jsAmount;
                                break;
                            }
                            else
                            {
                                jsedAmount += sffysx;
                                sfzf += sffysx == 0 ? 0
                                     : (sfEntity.zfbl >= 0 ? sffysx * sfEntity.zfbl : (-sfEntity.zfbl * sl));
                            }
                        }
                        jsAmount = fee.sfjsfymx.jzfy - jsedAmount;
                    }
                }
                if (jsedAmount != fee.sfjsfymx.jzfy)
                {
                    sfzf += fee.sfjsfymx.jzfy - jsedAmount;
                }
                #endregion

                if (sfsfList[0].zfxz == ((int)EnumSuanFaZFXZ.ZL).ToString())
                {
                    fee.sfjsfymx.sfzl += sfzf;  //自负性质为自理
                }
                else
                {
                    fee.sfjsfymx.sfzf += sfzf;
                }
                if (sfsfList[0].fyfw == Constants.xtbrsfsffyfw.fyfw3)
                {
                    //如果是绝对自费,记账金额为自理
                    fee.sfjsfymx.zl += fee.sfjsfymx.jzfy - sfzf;
                    fee.sfjsfymx.jzfy = 0m;
                    fee.sfjsfymx.xj += fee.sfjsfymx.zl;
                }
                else
                {
                    //可记账费用
                    fee.sfjsfymx.jzfy = fee.sfjsfymx.jzfy - sfzf;
                }
                //现金
                fee.sfjsfymx.xj += sfzf;
                //有算法 费用合计 = 现金
                fee.sfjsfymx.total = fee.sfjsfymx.xj;
            }
            fee.ybjyje = fee.sfjsfymx.jzfy;
            fee.ybjyfwje = fee.sfjsfymx.jzfy + fee.sfjsfymx.flzf;

            //一次性材料 //放在这 顺序没问题？
            if (fee.sfclxmzhEntity != null)
            {
                //.jmhje fee.zlfy  fee.sfjsfymx.zl
                if (fee.sfjsfymx.zl > fee.sfclxmzhEntity.kbfwsx)
                {
                    fee.sfjsfymx.zl = fee.sfjsfymx.zl - fee.sfclxmzhEntity.kbfwsx;
                    fee.sfjsfymx.jzfy += fee.sfclxmzhEntity.kbfwsx;
                }
                else
                {
                    fee.sfjsfymx.zl = 0;
                    fee.sfjsfymx.jzfy += fee.sfjsfymx.zl; //全报
                }
            }
        }

        /// <summary>
        /// 记账超额
        /// </summary>
        /// <param name="icd10">入院诊断 的 icd10</param>
        /// <param name="brxzFeeGroupList"></param>
        private void getJZCE(string icd10, IList<HospFeeItemOrMedicinListGroupedByBrxzBO> brxzFeeGroupList)
        {
            //根据病人性质和诊断判断，可保费用的上限，如少儿基金，第一诊断为急性xx阑尾，报销上限为1100
            //例如：[{"brxz":"18","icd10":"A01.001","bxsx":"1100"}]
            var pzzyBRXZZDBXSXValue = _sysConfigRepo.GetValueByCode(Constants.xtzypz.ZY_BRXZ_ZD_BXSX, this.OrganizeId);
            if (string.IsNullOrWhiteSpace(pzzyBRXZZDBXSXValue))
            {
                return;
            }
            var pzedBxsxList = Json.ToObject<List<BrxzZdBxsxBO>>(pzzyBRXZZDBXSXValue);

            foreach (var brxzFeeGroup in brxzFeeGroupList)
            {
                BrxzZdBxsxBO matchedXZPZ = null;
                if (pzedBxsxList == null ||
                    (matchedXZPZ = pzedBxsxList.FirstOrDefault(p => p.brxz == brxzFeeGroup.brxzzhEntity.cbrxz && p.icd10 == icd10)) == null
                    )
                {
                    continue;
                }
                var jzjeHj = getBrxzFeeListJZHJ(brxzFeeGroup);   //计算出记账费用合计（可报金额）
                if (jzjeHj > matchedXZPZ.bxsx)
                {
                    brxzFeeGroup.jzce = jzjeHj - matchedXZPZ.bxsx;
                }
            }
        }

        /// <summary>
        /// 获取记账费用合计
        /// </summary>
        /// <param name="brxzFeeGroup"></param>
        /// <returns></returns>
        private decimal getBrxzFeeListJZHJ(HospFeeItemOrMedicinListGroupedByBrxzBO brxzFeeGroup)
        {
            decimal hj = 0;
            foreach (var fee in brxzFeeGroup.FeeList)
            {
                if (fee.isNeedYBJY) //FYB有jzfy么
                {
                    hj += fee.sfjsfymx.jzfy;
                }
                ////########### 起付金额 ？？   医保有医保的起付金额？？  //记账超额  ？？
            }
            return hj;
        }

        /// <summary>
        /// 获取数据库变更集合对象（以便在事物中一次提交）
        /// </summary>
        /// <returns></returns>
        private OutHospSettDBDataUpdateCollectVO getSettDBUpdateVO(HospSettPatInfoVO settpatinfo, string fph, decimal zhye, decimal zhaoling, IList<HospFeeItemOrMedicinListGroupedByBrxzBO> zyjyjsList, string xjzfListStr, string defaultxjzffs)
        {
            var sqlVO = new OutHospSettDBDataUpdateCollectVO()
            {
                zy_jsmxList = new List<HospSettlementDetailEntity>(),
                zy_jyjsList = new List<HospTransactionSettlementEntity>(),
                zy_jyjsmxList = new List<HospTransactionSettlementDetailEntity>(),

                zy_jszffsList = new List<HospSettlementPaymentModelEntity>(),

                xt_brzhszjlList = new List<SysPatientAccountRevenueAndExpenseEntity>(),

                zy_jsdlList = new List<HospSettlementCategoryEntity>(),
            };

            //zy_js
            var zy_js = new HospSettlementEntity()
            {
                jsnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("zy_js"),
                OrganizeId = this.OrganizeId,
                zyh = settpatinfo.zyh,
                brxz = settpatinfo.brxz,
                zyts = DateTimeHelper.GetInHospDays(settpatinfo.ryrq.Value, settpatinfo.cyrq.Value),
                fph = fph,
                jszt = ((int)EnumJieSuanZT.YJ).ToString(),
                jsksrq = settpatinfo.ryrq,
                jsjsrq = settpatinfo.cyrq,
                jylx = settpatinfo.ybjylx,
                CreatorCode = this.UserIdentity.UserCode,
                CreateTime = DateTime.Now,
                zt = "1",
                zl = zhaoling,   //找零
                xjzffs = string.Empty,
            };

            foreach (HospFeeItemOrMedicinListGroupedByBrxzBO zyjyjs in zyjyjsList)
            {
                //（组合病人性质）即对应了一交易‘zy_jyjs’
                var zy_jyjs = new HospTransactionSettlementEntity()
                {
                    jsnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("zy_jyjs"),
                    OrganizeId = this.OrganizeId,
                    gljsnm = zy_js.jsnm,
                    zyh = settpatinfo.zyh,
                    sbrxz = zyjyjs.brxzEntity.brxz,
                    zyts = zy_js.zyts,
                    xjwc = 0,
                    xjzffs = string.Empty,
                    fph = zy_js.fph,
                    jszt = ((int)EnumJieSuanZT.YJ).ToString(),
                    jsksrq = settpatinfo.ryrq,
                    jsjsrq = settpatinfo.cyrq,
                    fpdm = string.Empty,
                    jylx = settpatinfo.ybjylx,
                    CreatorCode = this.UserIdentity.UserCode,
                    CreateTime = DateTime.Now,
                    zt = "1",
                };

                //#########仅自费逻辑

                foreach (var fee in zyjyjs.FeeList)
                {
                    //zy_js
                    zy_js.zje += fee.jmhje; //总金额 即 减免后金额总和
                    zy_js.zlfy += fee.zlfy; //自理费用  算法前
                    zy_js.zffy += fee.sfjsfymx.sfzf; //自负费用 算法自负
                    zy_js.flzffy += fee.sfjsfymx.flzf;  //分类自负费用
                    zy_js.jzfy += fee.sfjsfymx.jzfy;    //记账费用
                    zy_js.xjzf += fee.sfjsfymx.total;   //现金支付
                    zy_js.jmje += fee.sfjsfymx.jmje; //减免金额
                    //zy_js.ysk   //预收款     //CS没赋值
                    //zy_js.zl  //找零     //CS没赋值

                    //zy_jyjs
                    zy_jyjs.zlfy += fee.zlfy;   //自理费用
                    zy_jyjs.zffy += fee.sfjsfymx.sfzf;  //自负费用
                    zy_jyjs.flzffy = fee.sfjsfymx.flzf;     //分类自负费用
                    zy_jyjs.jzfy += fee.sfjsfymx.jzfy;  //记帐费用
                    zy_jyjs.xjzf += fee.sfjsfymx.total; //现金支付
                    zy_jyjs.jmje += fee.sfjsfymx.jmje;  //减免金额
                    //zy_jyjs.ysk   //预收款   //CS没赋值
                    //zy_jyjs.zl    //找零     //CS没赋值

                    //a new zy_jsmx
                    var zyjsmx = new HospSettlementDetailEntity()
                    {
                        jsnm = zy_js.jsnm,  //住院结算的内容
                        OrganizeId = this.OrganizeId,
                        jyfwje = fee.ybjyfwje,
                        jyje = fee.ybjyje,
                        xmjfbbh = fee.Item != null ? fee.Item.jfbbh : 0,
                        ypjfbbh = fee.Medicin != null ? fee.Medicin.jfbbh : 0,
                        yzlx = (fee.Item != null ? (int)EnumYiZhuXZ.XM : (fee.Medicin != null ? (int)EnumYiZhuXZ.YP : 0)).ToString(),
                        CreatorCode = this.UserIdentity.UserCode,
                        CreateTime = DateTime.Now,
                        zt = "1",
                    };
                    sqlVO.zy_jsmxList.Add(zyjsmx);

                    //a new zy_yjjsmx
                    var zyjyjsmx = new HospTransactionSettlementDetailEntity()
                    {
                        jsnm = zy_jyjs.jsnm,    //住院交易结算的内码
                        OrganizeId = this.OrganizeId,
                        xmjfbbh = fee.Item != null ? fee.Item.jfbbh : 0,
                        ypjfbbh = fee.Medicin != null ? fee.Medicin.jfbbh : 0,
                        yzlx = (fee.Item != null ? (int)EnumYiZhuXZ.XM : (fee.Medicin != null ? (int)EnumYiZhuXZ.YP : 0)).ToString(),
                        jyfwje = fee.ybjyfwje,
                        jyje = fee.ybjyje,
                        CreatorCode = this.UserIdentity.UserCode,
                        CreateTime = DateTime.Now,
                        zt = "1",
                    };
                    sqlVO.zy_jyjsmxList.Add(zyjyjsmx);
                }

                //总金额:自理费用+自负费用+分类自负费用+记帐费用
                zy_jyjs.zje = zy_jyjs.zlfy + zy_jyjs.zffy + zy_jyjs.flzffy + zy_jyjs.jzfy;
                sqlVO.zy_jyjsList.Add(zy_jyjs);

                //住院结算大类

                var ybjsfwze = zyjyjs.FeeList.Sum(p => p.ybjyfwje);

                var dlList = zyjyjs.FeeList.Select(p => p.dl).Distinct().ToList();
                foreach (var dl in dlList)
                {
                    var zy_jsdl = new HospSettlementCategoryEntity()
                    {
                        jsnm = zy_jyjs.jsnm,    //是关联‘住院交易结算’，而非‘住院结算’
                        OrganizeId = this.OrganizeId,
                        dl = dl,
                        jsrq = DateTime.Now,
                        CreatorCode = this.UserIdentity.UserCode,
                        CreateTime = DateTime.Now,
                        zt = "1",
                    };
                    var feeList = zyjyjs.FeeList.Where(p => p.dl == dl && p.isNeedYBJY);
                    if (feeList != null && feeList.Count() > 0)
                    {
                        //医保费用
                        if (ybjsfwze == 0)
                        {
                            zy_jsdl.zffy = 0m;
                            zy_jsdl.kbfy = 0m;
                        }
                        else
                        {
                            //yb.jzfy + yb.flzf/ returnValue.ybjsfwze 仅用做计算百分比
                            decimal percent = feeList.Sum(p => p.ybjyfwje) / ybjsfwze;
                            zy_jsdl.zffy = percent * zy_jyjs.zffy;//分类金额/分类金额合计*自负金额
                            zy_jsdl.kbfy = percent * zy_jyjs.jzfy;//分类金额/分类金额合计*记账金额
                        }
                    }
                    else
                    {
                        feeList = zyjyjs.FeeList.Where(p => p.dl == dl);
                        //非医保费用
                        zy_jsdl.zffy = feeList.Sum(p => p.sfjsfymx.sfzf); //算法自负
                        zy_jsdl.kbfy = feeList.Sum(p => p.sfjsfymx.jzfy);   //可报费用
                    }
                    zy_jsdl.jmfy = feeList.Sum(p => p.sfjsfymx.jmje);   //减免金额
                    zy_jsdl.flzffy = feeList.Sum(p => p.sfjsfymx.flzf); //分类自负费用
                    zy_jsdl.zlfy = feeList.Sum(p => p.sfjsfymx.zl); //自理费用
                    //总金额
                    zy_jsdl.zje = zy_jsdl.flzffy + zy_jsdl.zlfy + zy_jsdl.jmfy + zy_jsdl.zffy + zy_jsdl.kbfy;
                    sqlVO.zy_jsdlList.Add(zy_jsdl);
                }
            }

            #region 系统病人账户收支记录
            var xjzffsList = _sysCashPaymentModelRepo.GetLazyList();
            //系统病人账户收支记录
            var newPaymentBOList = Json.ToObject<IList<HospSettPaymentRecordBO>>(xjzfListStr);
            if (!newPaymentBOList.Any(p => p.xjzffs == Constants.xtzffs.ZYYJZHZF))
            {
                throw new FailedCodeException("HOSP_YJK_PAYMENTMODEL_MISSSING_YJK");
            }
            foreach (var pBo in newPaymentBOList)
            {
                if (pBo.xjzffs == Constants.xtzffs.ZYYJZHZF)
                {
                    continue;
                }
                var xt_brzhszjl = new SysPatientAccountRevenueAndExpenseEntity()
                {
                    OrganizeId = this.OrganizeId,
                    zh = settpatinfo.zyyjjzh ?? 0,
                    patid = settpatinfo.patid ?? 0,
                    szje = pBo.zfje,
                    zhye = (zhye += pBo.zfje),
                    pzh = string.Empty,
                    szxz = ((int)AccSzxz.ZYBJK).ToString(),
                    //xjzffsbh = xjzffsList.Where(p => (p.xjzffs ?? "").Equals(pBo.xjzffs)).Select(p => p.xjzffsbh).FirstOrDefault(),
                    xjzffs = "",    //&update170720
                    jsnm = zy_js.jsnm,
                    zyh = settpatinfo.zyh,
                    CreatorCode = this.UserIdentity.UserCode,
                    CreateTime = DateTime.Now,
                    zt = "1",
                };

                sqlVO.xt_brzhszjlList.Add(xt_brzhszjl);
            }
            //一条 住院支出消费的金额
            sqlVO.xt_brzhszjlList.Add(new SysPatientAccountRevenueAndExpenseEntity()
            {
                OrganizeId = this.OrganizeId,
                zh = settpatinfo.zyyjjzh ?? 0,
                patid = settpatinfo.patid ?? 0,
                szje = (-zy_js.xjzf),
                zhye = (zhye -= zy_js.xjzf),
                pzh = string.Empty,
                szxz = ((int)AccSzxz.ZYJS).ToString(),
                //xjzffsbh = xjzffsList.Any(p => p.xjzffs == defaultxjzffs) ? xjzffsList.Where(p => p.xjzffs == defaultxjzffs).Select(p => p.xjzffsbh).FirstOrDefault() : xjzffsList.Where(p => p.xjzffs == Constants.xtzffs.XJZF).Select(p => p.xjzffsbh).FirstOrDefault(),
                xjzffs = "",    //&update170720
                jsnm = zy_js.jsnm,
                zyh = settpatinfo.zyh,
                CreatorCode = this.UserIdentity.UserCode,
                CreateTime = DateTime.Now,
                zt = "1",
            });
            if (zhye != zhaoling)
            {
                throw new FailedCodeException("HOSP_FEE_IS_CHANGED");
            }
            //添加一条找零的收支记录
            sqlVO.xt_brzhszjlList.Add(new SysPatientAccountRevenueAndExpenseEntity()
            {
                OrganizeId = this.OrganizeId,
                zh = settpatinfo.zyyjjzh ?? 0,
                patid = settpatinfo.patid ?? 0,
                szje = (-zhaoling),
                zhye = (zhye -= zhaoling),  //0
                pzh = string.Empty,
                szxz = ((int)AccSzxz.ZYJS).ToString(),
                //xjzffsbh = xjzffsList.Any(p => p.xjzffs == defaultxjzffs) ? xjzffsList.Where(p => p.xjzffs == defaultxjzffs).Select(p => p.xjzffsbh).FirstOrDefault() : xjzffsList.Where(p => p.xjzffs == Constants.xtzffs.XJZF).Select(p => p.xjzffsbh).FirstOrDefault(),
                xjzffs = "",    //&update170720
                jsnm = zy_js.jsnm,
                zyh = settpatinfo.zyh,
                CreatorCode = this.UserIdentity.UserCode,
                CreateTime = DateTime.Now,
                zt = "1",
            });
            #endregion

            # region 住院结算支付方式
            var zy_jszffs = new HospSettlementPaymentModelEntity()
            {
                jsnm = zy_js.jsnm,
                OrganizeId = this.OrganizeId,
                xjzffs = Constants.xtzffs.ZYYJZHZF,
                zfje = zy_js.xjzf,
                ssry = this.UserIdentity.UserCode,
                ssrq = DateTime.Now,
                zh = settpatinfo.zyyjjzh,
                CreatorCode = this.UserIdentity.UserCode,
                CreateTime = DateTime.Now,
                zt = "1",
            };
            #endregion

            sqlVO.zy_jszffsList.Add(zy_jszffs);

            sqlVO.zy_js = zy_js;

            sqlVO.zyh = settpatinfo.zyh;
            sqlVO.zybz = ((int)EnumZYBZ.Ycy).ToString();
            sqlVO.cyrq = settpatinfo.cyrq;
            sqlVO.fph = fph;

            return sqlVO;
        }

        #endregion

    }
}
