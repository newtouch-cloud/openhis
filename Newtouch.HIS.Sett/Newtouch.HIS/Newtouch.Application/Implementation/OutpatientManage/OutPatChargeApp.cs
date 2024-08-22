/**********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description：门诊挂号收费相关业务 
// Author：HLF
// CreateDate： 2016/12/23 13:42:32 
//**********************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Proxy.guian.DTO.S25;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using newtouchyibao;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IRepository.PatientManage;
using Newtouch.HIS.Domain.Entity.PatientManage;
using System.Data.SqlClient;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class OutPatChargeApp : AppBase, IOutPatChargeApp
    {
        //GetOutPatRegEntity
        private readonly IOutpatientRegistRepo _outPatRegRepo; //门诊挂号仓储
        private readonly ISysConfigRepo _sysConfigRepo; //系统门诊配置
        private readonly ISysPatientChargeWaiverRepo _sysPatiChargeWaiRepo; // 减免金额
        private readonly IOutPatientDmnService _outPatientDmnService; //发票号
        private readonly ISysPatientNatureRepo _sysNatRepo; //系统病人性质
        private readonly ISysPatientBasicInfoRepo _sysPatiInfoRepo; //病人基本信息
        private readonly IBookkeepInHosDmnService _hosTempRepo; //收费模版
        private readonly IOutPatChargeDmnService _outChargeDmnService;
	    private readonly IPatientBasicInfoDmnService _patientBasicInfoDmnService;
        private readonly IOutPatientSettleDmnService _outPatientSettleDmnService;
        #region 门诊收费2018
        private readonly IOutPatientUniversalDmnService _outPatientUniversalDmnService;
        private readonly IOutpatientPrescriptionRepo _outpatientPrescriptionRepo;
        #endregion

	    private readonly ICqybUploadInPres04Repo _cqybUploadInPres04Repo;
		/// <summary>
		/// 根据卡号或病历号获取病人信息
		/// </summary>
		/// <param name="kh">卡号</param>
		/// <param name="brxz">病人性质</param>
		/// <returns></returns>
		public OutPatChargeInfoVO GetOutPatChargeInfo(string kh, string brxz)
        {
            OutPatChargeInfoVO outchargeInfoVo;
            if (string.IsNullOrEmpty(kh))
            {
                throw new FailedCodeException("OUTPAT_CARDNO_ISINVALID"); //卡号无效
            }
            var _kh = kh.Trim().ToUpper();
            //初复诊判断卡号是否有效
            var regEntity = _outPatRegRepo.SelectCFZ(_kh, OrganizeId);
            if (regEntity == null)
            {
                throw new FailedCodeException("OUTPAT_CARDNO_ISINVALID");
            }

            //病人性质传入不为空，说明多个病人性质已经选择一个作为参数传入
            if (!string.IsNullOrEmpty(brxz))
            {
                regEntity.brxz = brxz;

                //系统配置
                var confEntity = _sysConfigRepo.GetByCode(Constants.xtmzpz.BRXZ_YBJC, OperatorProvider.GetCurrent().OrganizeId);
                if (confEntity != null && confEntity.Value.Contains(regEntity.brxz))
                {
                    throw new FailedCodeException("OUTPAT_BEDPATIENT_CHARGE");
                }
                //获取挂号收费病人基本信息
                outchargeInfoVo = _outChargeDmnService.GetChargePatInfo(kh, brxz);
                if (outchargeInfoVo == null)
                {
                    throw new FailedCodeException("OUTPAT_PATIENT_BASICINFO_IS_NOT_EXIST");
                    //初始化界面
                }
                if (outchargeInfoVo.dybh == ((int)EnumDY.bd))
                {
                    outchargeInfoVo.dy = EnumDY.bd.GetDescription();
                }
                if (outchargeInfoVo.dybh == ((int)EnumDY.wd))
                {
                    outchargeInfoVo.dy = EnumDY.wd.GetDescription();
                }
            }
            else
            {
                //根据挂号信息查病人性质
                //挂号信息
                var regEntityList = _outPatRegRepo.GetOutPatRegEntityList(_kh, this.OrganizeId);
                if (regEntityList.Count < 1 || string.IsNullOrEmpty(regEntityList[0].brxz))
                {
                    throw new FailedCodeException("OUTPAT_REGIST_ISINVALID");  //挂号信息无效
                }

                //病人性质列表
                var propertyList = new List<OutPatChargeProperty>();
                if (regEntityList.Count > 1)
                {
                    //update by hao 2017-2-28 添加挂号多个病人性质的窗口选择 
                    foreach (var t in regEntityList)
                    {
                        var property = new OutPatChargeProperty
                        {
                            brxz = t.brxz,
                        };
                        property.brxzmc = _sysNatRepo.SelectBrxzByBrxz(property.brxz, OrganizeId).brxzmc;
                        propertyList.Add(property);
                    }

                    for (var j = 0; j < propertyList.Count; j++)
                    {
                        for (var k = propertyList.Count - 1; k > j; k--)
                        {
                            if (propertyList[j].brxz == propertyList[k].brxz)
                            {
                                propertyList.RemoveAt(k);
                            }
                        }
                    }
                }

                outchargeInfoVo = new OutPatChargeInfoVO { brxzList = propertyList };

                if (regEntityList.Count != 1 && propertyList.Count != 1) return outchargeInfoVo;
                if (regEntityList.Count == 1)
                {
                    regEntity.brxz = regEntityList[0].brxz;
                }
                if (propertyList.Count == 1)
                {
                    regEntity.brxz = propertyList[0].brxz;
                }

                //系统配置
                var confEntity = _sysConfigRepo.GetByCode(Constants.xtmzpz.BRXZ_YBJC, OperatorProvider.GetCurrent().OrganizeId);
                if (confEntity != null && confEntity.Value.Contains(regEntity.brxz))
                {
                    throw new FailedCodeException("OUTPAT_BEDPATIENT_CHARGE");
                }
                //获取挂号收费病人基本信息
                outchargeInfoVo = _outChargeDmnService.GetChargePatInfo(kh, regEntity.brxz);
                if (outchargeInfoVo == null)
                {
                    throw new FailedCodeException("OUTPAT_PATIENT_BASICINFO_IS_NOT_EXIST");
                    //初始化界面
                }
                if (outchargeInfoVo.dybh == ((int)EnumDY.bd))
                {
                    outchargeInfoVo.dy = EnumDY.bd.GetDescription();
                }
                if (outchargeInfoVo.dybh == ((int)EnumDY.wd))
                {
                    outchargeInfoVo.dy = EnumDY.wd.GetDescription();
                }
            }

            return outchargeInfoVo;
        }

        /// <summary>
        /// 获取挂号信息
        /// </summary>
        /// <param name="patid">病人patid</param>
        /// <param name="brxz"></param>
        /// <param name="docVo"></param>
        /// <returns></returns>
        public List<OutPatChargeItemVO> GetOutGhInfo(int patid, string brxz, out OutPatChargeDoctorVO docVo)
        {
            docVo = new OutPatChargeDoctorVO();
            var kssj = DateTime.Now;
            var startKssj = kssj.AddDays(-5).ToString("yyyy-MM-dd HH:mm:ss");
            var endKssj = kssj.ToString("yyyy-MM-dd HH:mm:ss");
            //var itemVo = _outChargeDmnService.getOutPatItem_ghlx(patid, brxz, startKssj, endKssj);
            var itemVo = _outChargeDmnService.GetOutPatGhlx(patid, brxz, startKssj, endKssj);
            if (itemVo != null && itemVo.Count > 0)
            {
                for (var i = 0; i < itemVo.Count; i++)
                {
                    if (itemVo[i].gh_ks == null)
                    {
                        itemVo.Remove(itemVo[i]);
                        i--;
                    }
                    else
                    {
                        itemVo[i].ghName = itemVo[i].gh_ks.Split('＞')[0];
                        itemVo[i].ksName = itemVo[i].gh_ks.Split('＞')[1];
                        //门急诊挂号在系统配置表是否有效
                        var isYx = _outChargeDmnService.getActiveDuration(itemVo[i].ks, itemVo[i].mjzbz, itemVo[i].CreateTime.ToString(CultureInfo.InvariantCulture).Trim());
                        if (isYx) continue;
                        itemVo.Remove(itemVo[i]);
                        i--;
                    }
                }
                if (itemVo.Count <= 0) return itemVo;
                //判断病人性质 是否大病  先略
                if (itemVo.Count != 1) return itemVo;
                var item = new OutPatChargeItemVO();

                if (itemVo[0].gh_ks == null)
                {
                    item.ghName = "";
                    item.ksName = "";
                }
                else
                {
                    item.ghName = itemVo[0].gh_ks.Split('＞')[0];
                    item.ksName = itemVo[0].gh_ks.Split('＞')[1];
                }
                item.ghnm = itemVo[0].ghnm;
                item.ksbh = itemVo[0].ksbh;
                item.ks = itemVo[0].ks;
                item.mjzbz = itemVo[0].mjzbz;
                item.ys = itemVo[0].ys;
                //当前为专家挂号时，直接赋值医生并添加记录
                if (string.IsNullOrEmpty(item.ys)) return itemVo;
                var doctorVo = _outChargeDmnService.getDoctorInfo
                    (item.ghnm, "", 3, item.mjzbz, false, false);
                if (doctorVo != null)
                {
                    docVo = doctorVo;
                }
                else
                {
                    throw new FailedCodeException("OUTPAT_REGISTERED_DOCTORINFO_NOT_COMPLETE");
                }
            }
            else
            {
                throw new FailedCodeException("OUTPAT_REGISTERED_ABNORMAL");
            }
            return itemVo;
        }



        /// <summary>
        /// 根据关键字获取药品和收费项目信息
        /// </summary>
        /// <param name="keyword">查询关键字</param>
        /// <returns></returns>
        public List<ChargeItemDetailVO> GetYpItemInfo(string keyword)
        {
            //获取本地配置的药房代码
            var yfpz = Constants.CurrentYfbm.yfbmCode;
            if (string.IsNullOrEmpty(yfpz))
            {
                throw new FailedCodeException("OUTPAT_YP_YF_ISNULL"); //如需查询使用药品相关信息，请先配置药房！
            }
            //查询有关药品和项目
            var itemVo = _outChargeDmnService.getYpItemList(keyword, yfpz, this.OrganizeId);
            return itemVo;
        }

        /// <summary>
        /// 获取处方号和附加费金额
        /// </summary>
        /// <param name="sfxmtype">收费项目类别</param>
        /// <param name="lscfh">临时处方号</param>
        /// <param name="brxz">病人性质</param>
        /// <param name="dl">大类</param>
        /// <param name="sfxm">收费项目</param>
        /// <param name="dj">单价</param>
        /// <param name="sl"></param>
        /// <returns></returns>
        public OutPatChargeCFDto GetCfhAndFjfMoney(string sfxmtype, string lscfh, string brxz, string dl, string sfxm, string dj, string sl)
        {
            var dto = new OutPatChargeCFDto();

            if (sfxmtype == "1")//项目
            {
                dto.cfh = "";
            }
            if (sfxmtype == "2")//药品
            {
                dto.cfh = !string.IsNullOrEmpty(lscfh) ? lscfh : _outChargeDmnService.getCflsh();
            }
            dto.fwf = _outChargeDmnService.Calcfwfjm(brxz, dl, sfxm, dj);//附件金额
            var jeNum = (Convert.ToDouble(dj) + Convert.ToDouble(dto.fwf)) * Convert.ToDouble(sl);
            dto.jeNum = jeNum;

            return dto;
        }

        #region 处方号
        /// <summary>
        /// 获取处方号
        /// </summary>
        /// <param name="sfxmtype"></param>
        /// <returns></returns>
        public string GetCfh(string sfxmtype)
        {
            string cfh = "";
            if (sfxmtype == "1")//项目
            {
                cfh = "";
            }
            if (sfxmtype == "2")//药品
            {
                cfh = _outChargeDmnService.getCflsh(); //处方流水号
            }
            return cfh;
        }

        #endregion

        #region 判断同一药品是否一个处方 获取相同药品的组号 俩方法没有检测到引用，创建者如无需要，请删除 add by sunny

        /// <summary>
        /// 阻止同一处方录入相同药品
        /// </summary>
        /// <param name="strSfxm"></param>
        /// <param name="strYf"></param>
        /// <param name="gridDto"></param>
        /// <param name="lsCfh"></param>
        /// <returns></returns>
        private string ReturnEqual(string strSfxm, string strYf, List<OutpatChargeDto> gridDto, string lsCfh)
        {
            string res = "";
            if (gridDto.Count > 0)
            {
                foreach (OutpatChargeDto dto in gridDto)
                {
                    if (!string.IsNullOrEmpty(dto.cfh))//药品
                    {
                        if (!string.IsNullOrEmpty(lsCfh) && lsCfh.Equals(dto.cfh))
                        {
                            if (strYf.Equals(dto.yfdm))
                            {
                                if (strSfxm.Equals(dto.sfxm))
                                {
                                    res = "当前处方已存在所选药品，确认再添加吗？";
                                }
                            }
                            else
                            {
                                res = "error";
                            }
                        }
                    }
                    else
                    {
                        if (strSfxm.Equals(dto.sfxm))
                        {
                            res = "当前已存在所选项目，确认再添加吗？";
                        }
                    }
                }
            }
            return res;
        }


        /// <summary>
        /// 获取相同药品的组号
        /// </summary>
        /// <param name="code"></param>
        /// <param name="strYf"></param>
        /// <param name="gridDto"></param>
        /// <param name="lsCfh"></param>
        /// <returns></returns>
        private string SetCzh(string code, string strYf, List<OutpatChargeDto> gridDto, string lsCfh)
        {
            string res = "";
            if (gridDto.Count > 0)
            {
                foreach (OutpatChargeDto dto in gridDto)
                {
                    if (!string.IsNullOrEmpty(dto.cfh))//药品
                    {
                        if (!string.IsNullOrEmpty(lsCfh) && lsCfh.Equals(dto.cfh))
                        {
                            if (strYf.Equals(dto.yfdm))
                            {
                                if (code.Equals(dto.sfxm))
                                {
                                    if (res == "")
                                    {
                                        res = dto.czh;
                                    }
                                    else
                                    {
                                        res += "," + dto.czh;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return res;
        }
        #endregion

        #region 结算

        /// <summary>
        /// 门诊收费结算
        /// </summary>
        /// <param name="gridDto">要保存、结算的处方列表</param>
        /// <param name="kh">卡号</param>
        /// <param name="fph">发票号</param>
        /// <param name="xjzffs">现金支付方式</param>
        /// <param name="xjzffsbh"></param>
        /// <param name="ysk">预收款</param>
        /// <param name="ssk">实收款</param>
        /// <param name="zl">找零</param>
        /// <returns></returns>
        public SettSaveSuccessResultDto OutPatChargeSett(List<OutpatChargeDto> gridDto, string kh, string fph, string xjzffs, string xjzffsbh, decimal ysk, decimal ssk, decimal zl)
        {

            //病人性质Entity
            SysPatientNatureEntity brxzEntity;
            //结算Entity
            SettlementEntityVO jsEntity;
            //结算实体 对应表mz_js
            OutpatientSettlementEntity mzjs;
            //结算明细 对应表mz_jsmx
            //结算大类
            List<OutpatientSettlementCategoryEntity> jsdlList;


            //结算判断
            SettSaveCheck(gridDto, fph, zl, out brxzEntity);

            //数据库变更对象
            var sqlVo = GetOutPatChargeSettDbvo(gridDto, kh, fph, xjzffs, xjzffsbh, ysk, ssk, zl, out jsEntity);


            //获得需要结算的项目信息，统计结算大类
            _outChargeDmnService.GetMzJsInfo(jsEntity, brxzEntity, out mzjs, out jsdlList);
            //现金支付
            mzjs.xjzf = ssk;
            mzjs.zje = ssk;
            mzjs.xjzffs = xjzffs;
            mzjs.fph = fph;
            mzjs.ysk = ysk;
            mzjs.zl = zl;
            //新加字段 add
            mzjs.xm = gridDto[0].xm; //姓名
            mzjs.xb = gridDto[0].xb;
            mzjs.blh = gridDto[0].blh;
            mzjs.csny = gridDto[0].csny;
            mzjs.zjh = gridDto[0].zjh;
            mzjs.zjlx = gridDto[0].zjlx;

            sqlVo.mz_js = mzjs;
            sqlVo.mz_jsdlList = jsdlList;

            //获取结算明细
            var jsmxList = _outChargeDmnService.getJSMX(mzjs, jsEntity);
            sqlVo.mz_jsmxList = jsmxList;

            //门诊结算支付方式
            var settPayEntity = _outChargeDmnService.GetMzJsZffs(mzjs.jsnm, xjzffs, xjzffsbh, ysk);
            sqlVo.mz_jszffs = settPayEntity;


            //保存至数据库
            _outChargeDmnService.OutPatChargeSettDB(sqlVo);

            //返回门诊收费结算数据
            var reslt = new SettSaveSuccessResultDto()
            {
                yingshoukuan = sqlVo.mz_js.zje.ToString("0.00"),
                ssk = ssk.ToString("0.00"),
                srce = sqlVo.mz_js.xjwc.ToString("0.00"),
                zhaoling = (sqlVo.mz_js.zl ?? 0).ToString("0.00"),
            };

            return reslt;
        }

        /// <summary>
        /// 结算判断
        /// </summary>
        /// <param name="gridDto"></param>
        /// <param name="fph"></param>
        /// <param name="zl"></param>
        /// <param name="brxzEntity"></param>
        private void SettSaveCheck(IReadOnlyList<OutpatChargeDto> gridDto, string fph, decimal zl, out SysPatientNatureEntity brxzEntity)
        {
            if (string.IsNullOrWhiteSpace(fph))
            {
                throw new FailedCodeException("HOSP_PLEASE_CHOOSE_FINO"); //请选择发票号
            }
            if (_outPatientDmnService.ExistsForInvoiceNo(fph, OrganizeId))
            {
                throw new FailedCodeException("HOSP_FINO_IS_USED"); //发票号已使用
            }
            //找零
            if (zl < 0)
            {
                throw new FailedCodeException("HOSP_ZHAOLING_IS_NEGATIVE"); //找零
            }

            //验证一些信息
            if (gridDto == null || gridDto.Count < 0)
            {
                throw new FailedCodeException("OUTPAT_BILLING_ITEM_CAN_NOT_BE_EMPTY"); //没有要结算项目
            }
            if (gridDto[0].ghnm == 0)
            {
                throw new FailedCodeException("OUTPAT_THE_REGISTERED_MESSAGE_DOES_NOT__EXIST"); //挂号内码为空
            }

            //不同病人性质和门急诊不能一起结算   多个挂号信息一起结算，目前不存在

            //根据挂号内码获取挂号信息 没用到


            //根据病人内码获取病人信息
            var brjbxx = _sysPatiInfoRepo.GetInfoByPatid(gridDto[0].patid.ToString(), OrganizeId);
            if (brjbxx == null) throw new FailedCodeException("OUTPAT_PATIENT_BASICINFO_IS_NOT_EXIST"); //不存在该病人信息
            //根据病人性质判断
            brxzEntity = _sysNatRepo.SelectBrxzByBrxzbh(gridDto[0].brxzbh, OrganizeId);
            if (brxzEntity == null)
            {
                throw new FailedCodeException("HOSP_PATIENT_NATURE_IS_NOT_EXIST"); //该病人性质不存在
            }

            if (brxzEntity.ybjylx == Constants.ybjylx.ybjylx6)
            {
                //新农合交易
            }
            else
            {
                //自费及医保交易 

            }
        }

        /// <summary>
        /// 保存结算实体赋值
        /// </summary>
        /// <param name="gridDto">要保存、结算的处方列表</param>
        /// <param name="kh"></param>
        /// <param name="fph">发票号</param>
        /// <param name="xjzffs">现金支付方式</param>
        /// <param name="xjzffsbh"></param>
        /// <param name="ysk">预收款</param>
        /// <param name="ssk">实收款</param>
        /// <param name="zl"></param>
        /// <param name="jsEntity"></param>
        /// <returns></returns>
        public OutPatChargeSettDataVo GetOutPatChargeSettDbvo(List<OutpatChargeDto> gridDto, string kh, string fph, string xjzffs, string xjzffsbh, decimal ysk, decimal ssk, decimal zl, out SettlementEntityVO jsEntity)
        {
            var sqlVo = new OutPatChargeSettDataVo()
            {
                mz_cf = new List<OutpatientPrescriptionEntity>(),
                mz_cfmxList = new List<OutpatientPrescriptionDetailEntity>(),
                mz_xmList = new List<OutpatientItemEntity>(),
                mz_js = new OutpatientSettlementEntity(),
                mz_jsmxList = new List<OutpatientSettlementDetailEntity>(),
                mz_jsdlList = new List<OutpatientSettlementCategoryEntity>(),
                mz_jszffs = new OutpatientSettlementPaymentModelEntity()
            };

            jsEntity = new SettlementEntityVO(); //暂存结算信息 
            var jsxmList = new List<SettleProjectVO>(); //结算项目List

            var strArray = new List<string>();  //暂存已保存的处方号
            var cFisExit = true; // 默认处方不存在
            var cfnm = 0; //处方内码
            const int ptcfnm = 0;
            var cfh = ""; // 当前处方号

            //是否特殊标识药品
            var istsbzS = false;
            var yxYbdm = false; //有效医保代码
            decimal outJmbl = 0;
            const decimal outJmblZhxz = 0;
            decimal outJmje = 0;
            const decimal outJmjeZhxz = 0;
            //循环要保存的处方药品和项目数据
            if (gridDto.Count > 0)
            {
                var gridindex = 0;
                var cfindex = 0;
                foreach (var dto in gridDto)
                {
                    #region 药品处方
                    if (dto.cfh != "" && dto.xmtype == "2")//处方号不为空，即为药品
                    {

                        #region 药品
                        if (dto.IsZhbrxz)//组合病人性质
                        {
                            if (dto.zh_sbrxz == dto.brxz)
                            {
                                istsbzS = true;
                            }
                            if (istsbzS)//特殊标识
                            {
                                //医保减免算法
                                _sysPatiChargeWaiRepo.Get_Calcjm(dto.zh_sbrxz, dto.dl, dto.sfxm, dto.je, out outJmbl, out outJmje, this.OrganizeId);
                            }
                            else
                            {
                                _sysPatiChargeWaiRepo.Get_Calcjm(dto.zh_mbrxz, dto.dl, dto.sfxm, dto.je, out outJmbl, out outJmje, OrganizeId);

                            }
                        }
                        else
                        {
                            if (dto.sfxm != null)
                            {
                                _sysPatiChargeWaiRepo.Get_Calcjm(dto.brxz, dto.dl, dto.sfxm, dto.je, out outJmbl, out outJmje, OrganizeId);
                            }
                        }

                        if (dto.isYbbr || dto.brxz == "38")
                        {
                            //暂不做处理
                        }
                        else
                        {
                            yxYbdm = true;
                        }

                        //添加处方主表信息
                        if (gridindex == 0 || cfindex == 0 || strArray.Count == 0)
                        {
                            cFisExit = true; //处方不存在，添加处方主表
                        }
                        //添加过处方记录
                        if (strArray != null && strArray.Count > 0)
                        {
                            foreach (var t in strArray)
                            {
                                if (t == dto.cfh)
                                {
                                    //说明已经存在该处方主表信息 
                                    cFisExit = false; // 处方已存在
                                }
                            }
                        }

                        if (cFisExit)
                        {
                            //添加处方主表信息
                            strArray.Add(dto.cfh);

                            #region 添加处方
                            //门诊处方
                            var mzcf = new OutpatientPrescriptionEntity
                            {
                                cfnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cf"),
                                ghnm = dto.ghnm,
                                patid = dto.patid,
                                brxz = !yxYbdm ? "00" : (dto.IsZhbrxz ? dto.zh_sbrxz : dto.brxz),
                                lyyf = Constants.CurrentYfbm.yfbmCode,
                                fybz = "0",
                                mjzbz = dto.mjzbz,
                                ys = dto.ys,
                                ks = dto.ks,
                                zje = ysk,
                                cfzt = "1",
                                jsnm = 0,
                                jsrq = DateTime.Now,
                                cfh = dto.cfh,
                                zt = "1",
                                OrganizeId = OperatorProvider.GetCurrent().OrganizeId
                            };
                            mzcf.Create();


                            cfnm = mzcf.cfnm;
                            cfh = mzcf.cfh;
                            #endregion

                            cfindex++;
                            sqlVo.mz_cf.Add(mzcf);
                        }

                        #endregion

                        #region 处方明细

                        var mzcfmx = new OutpatientPrescriptionDetailEntity
                        {
                            cfmxId = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cfmx"),
                            cfnm = cfnm,
                            yp = dto.xm
                        };
                        //mzcfmx.cfmxId = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyString("mz_cfmx");
                        mzcfmx.yp = dto.sfxm;
                        mzcfmx.dl = dto.dl;
                        mzcfmx.dj = dto.dj;
                        mzcfmx.sl = dto.sl;
                        mzcfmx.je = dto.je;
                        mzcfmx.zfbl = dto.zfbl; //自负比例
                        mzcfmx.zfxz = dto.zfxz;
                        mzcfmx.dw = dto.dw; //单位
                        mzcfmx.yfdm = dto.yfdm;
                        mzcfmx.czh = dto.czh;
                        mzcfmx.zt = "1";
                        mzcfmx.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
                        mzcfmx.Create();
                        #endregion

                        sqlVo.mz_cfmxList.Add(mzcfmx);

                        #region 结算对象赋值 

                        var jsxmDto = new SettleProjectVO
                        {
                            dl = dto.dl,
                            sfxm = mzcfmx.yp,
                            dj = dto.dj,
                            sl = dto.sl,
                            je = dto.je,
                            zfbl = dto.zfbl,
                            zfxz = dto.zfxz,
                            jmbl = outJmbl,
                            jmje = outJmje,
                            mxnm = 0,
                            cf_mxnm = mzcfmx.cfmxId,
                            cfh = cfh,
                            czh = mzcfmx.czh,
                            fwfdj = dto.fwfdj,
                            jzrq = DateTime.Now,
                            Hismc = dto.xmmc,
                            Xmbm = dto.nbdl,
                            pt_cfnm = ptcfnm
                        };
                        //jsxmDto.mxbm = mzcfmx.yp;

                        jsxmList.Add(jsxmDto);
                        #endregion

                    }
                    #endregion

                    #region 项目
                    if (dto.xmtype == "1" || dto.xmtype == "4")//收费项目数据(处方项目除外)//项目
                    {
                        #region 门诊项目

                        var mzxm = new OutpatientItemEntity
                        {
                            xmnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_xm"),
                            ghnm = dto.ghnm,
                            patid = dto.patid
                        };
                        if (dto.IsZhbrxz)//组合病人性质
                        {
                            mzxm.brxz = istsbzS ? dto.zh_sbrxz : dto.zh_mbrxz;
                        }
                        else
                        {
                            mzxm.brxz = dto.brxz;
                        }
                        if (!yxYbdm) mzxm.brxz = "00";
                        mzxm.mjzbz = dto.mjzbz;
                        mzxm.ys = dto.ys;
                        mzxm.ks = dto.ks;
                        mzxm.sfxm = dto.sfxm;
                        mzxm.dl = dto.dl;
                        mzxm.dj = dto.dj;
                        mzxm.sl = dto.sl;
                        mzxm.je = dto.je;
                        mzxm.zfbl = dto.zfbl;
                        mzxm.zfxz = dto.zfxz;
                        mzxm.xmzt = "1";
                        mzxm.ssbz = "0";
                        mzxm.ssrq = DateTime.Now;
                        mzxm.jsnm = 0;
                        mzxm.jsrq = DateTime.Now;
                        if (dto.IsZhbrxz)//组合病人性质
                        {
                            if (istsbzS)//特殊标识
                            {
                                mzxm.jmbl = outJmbl;
                                mzxm.jmje = outJmje;
                            }
                            else
                            {
                                mzxm.jmbl = outJmblZhxz;
                                mzxm.jmje = outJmjeZhxz;
                            }
                        }
                        else
                        {
                            mzxm.jmbl = outJmbl;
                            mzxm.jmje = outJmje;
                        }
                        mzxm.zt = "1";
                        mzxm.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
                        mzxm.Create();

                        #endregion

                        sqlVo.mz_xmList.Add(mzxm);

                        #region 结算对象赋值

                        var jsxmDto = new SettleProjectVO
                        {
                            dl = dto.dl,
                            sfxm = dto.sfxm,
                            dj = dto.dj,
                            sl = dto.sl,
                            je = dto.je,
                            zfbl = dto.zfbl,
                            zfxz = dto.zfxz,
                            jmbl = outJmbl,
                            jmje = outJmje,
                            mxnm = mzxm.xmnm,
                            cf_mxnm = 0,
                            fwfdj = dto.fwfdj,
                            jzrq = DateTime.Now,
                            Hismc = dto.xmmc,
                            Xmbm = dto.nbdl,
                            pt_cfnm = dto.pt_nm
                        };
                        //jsxmDto.mxbm = "";

                        jsxmList.Add(jsxmDto);
                        #endregion
                    }
                    #endregion

                    if (gridindex == 0)
                    {
                        #region 结算基本信息
                        jsEntity.patid = dto.patid;
                        jsEntity.brxz = dto.brxz;
                        jsEntity.ghnm = dto.ghnm;
                        jsEntity.isGh = false;
                        jsEntity.isZf = true;
                        jsEntity.jslx = !string.IsNullOrEmpty(kh) ? "2" : "1";
                        #endregion 
                    }

                    gridindex++;
                }

            }

            jsEntity.jsxmList = jsxmList;

            sqlVo.fph = fph;
            //添加各个实体对象赋值
            return sqlVo;

        }

        #endregion


        #region 收费项目模版

        /// <summary>
        /// 获取收费模板和  模板下所有项目
        /// </summary>
        /// <param name="ks"></param>
        /// <returns></returns>
        public OutPatChargeItemTempVO GetChargeTemplateAll(string ks)
        {
            var tempAll = new OutPatChargeItemTempVO();
            var tempDetailList = new List<OutPatChargeItemDetailVO>();

            var tempList = GetChargeTemplate(ks);
            if (tempList != null && tempList.Count > 0)
            {
                foreach (var item in tempList)
                {
                    var tempDetail = new OutPatChargeItemDetailVO { ItemCode = item.sfmbbh };
                    var tempItemList = GetChargeItemContent(item.sfmbbh);
                    tempDetail.tempDetailList = tempItemList;
                    tempDetailList.Add(tempDetail);
                }
            }

            tempAll.tempList = tempList;
            tempAll.tempDetailList = tempDetailList;
            return tempAll;
        }


        /// <summary>
        /// 收费项目模板列表
        /// </summary>
        /// <param name="ks">科室</param>
        /// <returns></returns>
        private List<ChargeItemTemplateVO> GetChargeTemplate(string ks)
        {
            var tempList = _hosTempRepo.GetChargeTemplate(ks, OrganizeId);
            return tempList;
        }

        /// <summary>
        /// 收费项目
        /// </summary>
        /// <param name="sfmb">收费模板编号</param>
        /// <returns></returns>
        private List<TemplateContentVO> GetChargeItemContent(string sfmb)
        {
            var tempConList = _hosTempRepo.GetChargeItemContent(sfmb, OrganizeId);
            return tempConList;
        }

        #endregion

        #region 门诊记账

        public List<OutpatAccInfoDto> GetOutPatBasicInfoInAcc(string IsBlh, string mzh)
        {

            if (string.IsNullOrWhiteSpace(mzh))
            {
                throw new FailedException("门诊号无效"); //病历号无效
            }

            //获取挂号收费病人基本信息
            var outPatBasicInfoInAcc = _outChargeDmnService.GetChargePatInfoInAcc("", IsBlh, mzh, OrganizeId);
            if (outPatBasicInfoInAcc == null)
            {
                throw new FailedCodeException("OUTPAT_PATIENT_BASICINFO_IS_NOT_EXIST");
                //初始化界面
            }
            return outPatBasicInfoInAcc;
        }
        #endregion

        /// <summary>
        /// 门诊登记和住院登记 根据病历号 或 卡号 查询病人基本信息
        /// </summary>
        /// <param name="blh">病历号</param>
        /// <param name="kh">卡号</param>
        /// <param name="zjh">证件号</param>
        /// <param name="cardType">卡类型</param>
        /// <returns></returns>
        public OutpatAccInfoDto GetOutPatBasicInfoInRegister(string blh, string kh, string zjh, string cardType,string ly,string CardId)
        {
            if (string.IsNullOrWhiteSpace(blh) && string.IsNullOrWhiteSpace(zjh) && (string.IsNullOrWhiteSpace(kh) || string.IsNullOrWhiteSpace(cardType)))
            {
                throw new FailedException("缺少查询参数：病历号或卡号");
            }
            //获取挂号收费病人基本信息
            var outPatBasicInfoInAcc = _outChargeDmnService.GetChargePatInfoInRegister(kh, blh, zjh, OrganizeId, cardType,ly,CardId);
            if (outPatBasicInfoInAcc == null)
            {
                throw new FailedCodeException("OUTPAT_PATIENT_BASICINFO_IS_NOT_EXIST");
                //初始化界面
            }
            outPatBasicInfoInAcc.fzbz = _outPatRegRepo.SelectCFZ(outPatBasicInfoInAcc.kh, this.OrganizeId) == null ? (byte)0 : (byte)1;
            return outPatBasicInfoInAcc;
        }

        #region 门诊收费 Vision-1.3 门诊记账的第三个版本
        /// <summary>
        /// 门诊收费，获取病人基本信息
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public IList<OptimAccInfoDto> GetInfo(string mzh)
        {
            var OptimAccInfoDto = _outChargeDmnService.GetpatientAccountInfo(mzh, OrganizeId);
            return OptimAccInfoDto;
        }
        #endregion

        #region 门诊收费2018

        /// <summary>
        /// 门诊收费2018 提交门诊收费
        /// </summary>
        /// <param name="bacDto"></param>
        /// <param name="feeRelated"></param>
        /// <param name="ybfeeRelated"></param>
        /// <param name="xnhybfeeRelated"></param>
        /// <param name="accDto"></param>
        /// <param name="orgId"></param>
        /// <param name="cfnmList"></param>
        /// <param name="jsnmList"></param>
        /// <param name="extxmnmList">历史已提交（库中已有）</param>
        /// <param name="outTradeNo"></param>
        /// <returns></returns>
        public bool submitOutpatCharge(BasicInfoDto2018 bacDto
            , OutpatientSettFeeRelatedDTO feeRelated
            , CQMzjs05Dto ybfeeRelated
            , S25ResponseDTO xnhybfeeRelated
            , OutServiceInfoDto2018 accDto, string orgId
            , IList<int> cfnmList, out IList<int> jsnmList, IList<int> extxmnmList
            , string outTradeNo)
        {
            if (accDto == null && (cfnmList == null || cfnmList.Count < 1) && (extxmnmList == null || extxmnmList.Count < 1))
            {
                throw new FailedException("不存在记账数据");
            }
            jsnmList = new List<int>();
            var itemsGroupBoList = new List<FeeItemsGroupedBO>();
            if (accDto != null)
            {
                if (accDto.OutxmDto != null && accDto.OutxmDto.Count > 0)
                {
                    //项目（新录入的项目不关联处方）
                    var itemsGroupBo = new FeeItemsGroupedBO
                    {
                        cfsfrq = bacDto.sfrq,
                        xmList = new List<OutpatientItemEntity>(),//新增项目明细
                    };
                    foreach (var item in accDto.OutxmDto)//项目
                    {
                        //门诊项目
                        var mzxm = new OutpatientItemEntity
                        {
                            sfxm = item.sfxmCode,
                            dj = item.dj,
                            sl = item.sl,
                            dw = item.dw,
                            sfrq = bacDto.sfrq,
                            zfbl = item.zfbl,
                            zfxz = item.zfxz
                        };
                        if (item.zlcs != null && item.zll != null)//非常规版本
                        {
                            mzxm.zzll = item.zll * item.zlcs;
                            mzxm.dczll = item.zll;
                            mzxm.zxcs = item.zlcs;
                        }
                        mzxm.Create();
                        itemsGroupBo.xmList.Add(mzxm);
                    }
                    itemsGroupBoList.Add(itemsGroupBo);
                }
                if (accDto.OutcfDto != null && accDto.OutcfDto.Any())
                {
                    //药品处方
                    foreach (var item in accDto.OutcfDto)//处方
                    {
                        var cfGroupBo = new FeeItemsGroupedBO
                        {
                            cfh = item.cfh,
                            cfsfrq = bacDto.sfrq,
                            cflx = (int)EnumPrescriptionType.Medicine,
                            ypList = new List<OutpatientPrescriptionDetailEntity>(),
                        };
                        foreach (var cfmx in item.cfmxDto)
                        {
                            var mzcfmx = new OutpatientPrescriptionDetailEntity
                            {
                                dj = cfmx.dj,
                                dw = cfmx.dw,
                                sl = cfmx.sl,
                                yp = cfmx.sfxmCode,
                                jl = cfmx.jl,
                                jldw = cfmx.jldw,
                                zfbl = cfmx.zfbl,
                                zfxz = cfmx.zfxz
                            };
                            mzcfmx.Create();
                            cfGroupBo.ypList.Add(mzcfmx);
                        }
                        itemsGroupBoList.Add(cfGroupBo);
                    }
                }
                if (!itemsGroupBoList.Any())
                {
                    return false;
                }
            }

            IList<int> xmnmList;
            IList<int> cfnmadd;
            IList<string> updateSkipSettledCfhList;
            //提交明细（处方的话更新主表信息）
            _outPatientUniversalDmnService.SubmitItems(orgId, out cfnmadd, out xmnmList, out updateSkipSettledCfhList, bacDto.mzh, bacDto.ys, itemsGroupBoList);
            //
            if (extxmnmList != null && extxmnmList.Count > 0)
            {
                
                xmnmList = xmnmList.Union(extxmnmList).ToList();
            }
            //
            if (cfnmList != null && cfnmList.Count() > 0)
            {
                cfnmList = cfnmList.ToList().Union(cfnmadd.ToList()).ToList();
                
            }
            else
            {
                cfnmList = cfnmadd.ToList();
            }
            //获取配置 门诊收费成功之后是否自动生成计划
            var autoGenePlan = _sysConfigRepo.GetBoolValueByCode("Outpatient_ChargeFee_AutoGenePlan", orgId) ?? false;
            // 更新门诊处方的结算信息（更新结算信息）
            var settAddBO = new OutpatientSettlementAddBO()
            {
                cfnmList = cfnmList,
                xmnmList = xmnmList,
                autoGenePlan = autoGenePlan,

                isQfyj = bacDto.isQfyj,
                settSfrq = bacDto.sfrq,
                fph = bacDto.fph,
            };
            jsnmList = _outPatientUniversalDmnService.AddSettlement(orgId, bacDto.mzh, settAddBO, feeRelated, ybfeeRelated, xnhybfeeRelated, outTradeNo);
            return true;
        }

        /// <summary>
        /// 补计费提交
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="bacDto"></param>
        /// <param name="xmList"></param>
        /// <returns></returns>
        public bool submitOutpatAdditional(string orgId, BasicInfoDto2018 bacDto,
            OutServiceInfoDto2018 accDto)
        {
            if (!(accDto != null))
            {
                throw new FailedException("失败，缺少计费明细");
            }

            var itemsGroupBoList = new List<FeeItemsGroupedBO>();
            if (accDto != null)
            {
                if (accDto.OutxmDto != null && accDto.OutxmDto.Count > 0)
                {
                    //项目（新录入的项目不关联处方）
                    var itemsGroupBo = new FeeItemsGroupedBO
                    {
                        cfsfrq = bacDto.sfrq,
                        xmList = new List<OutpatientItemEntity>(),//新增项目明细
                    };
                    foreach (var item in accDto.OutxmDto)//项目
                    {
                        //门诊项目
                        var mzxm = new OutpatientItemEntity
                        {
                            sfxm = item.sfxmCode,
                            dj = item.dj,
                            sl = item.sl,
                            dw = item.dw,
                            sfrq = bacDto.sfrq,
                            zfbl = item.zfbl,
                            zfxz = item.zfxz
                        };
                        mzxm.Create();
                        itemsGroupBo.xmList.Add(mzxm);
                    }
                    itemsGroupBoList.Add(itemsGroupBo);
                }
                if (accDto.OutcfDto != null && accDto.OutcfDto.Any())
                {
                    //药品处方
                    foreach (var item in accDto.OutcfDto)//处方
                    {
                        var cfGroupBo = new FeeItemsGroupedBO
                        {
                            cfh = item.cfh,
                            cfsfrq = bacDto.sfrq,
                            cflx = (int)EnumPrescriptionType.Medicine,
                            ypList = new List<OutpatientPrescriptionDetailEntity>(),
                        };
                        foreach (var cfmx in item.cfmxDto)
                        {
                            var mzcfmx = new OutpatientPrescriptionDetailEntity
                            {
                                dj = cfmx.dj,
                                dw = cfmx.dw,
                                sl = cfmx.sl,
                                yp = cfmx.sfxmCode,
                                jl = cfmx.jl,
                                jldw = cfmx.jldw,
                                zfbl = cfmx.zfbl,
                                zfxz = cfmx.zfxz
                            };
                            mzcfmx.Create();
                            cfGroupBo.ypList.Add(mzcfmx);
                        }
                        itemsGroupBoList.Add(cfGroupBo);
                    }
                }
                if (!itemsGroupBoList.Any())
                {
                    return false;
                }
            }

            IList<int> xmnmList;
            IList<int> cfnmadd;
            IList<string> updateSkipSettledCfhList;
            //提交明细（处方的话更新主表信息）
            _outPatientUniversalDmnService.SubmitItems(orgId, out cfnmadd, out xmnmList, out updateSkipSettledCfhList, bacDto.mzh, bacDto.ys, itemsGroupBoList);

            return true;
        }

        /// <summary>
        /// 整合前台数据成后台的理想状态
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public OutServiceInfoDto2018 togetherData(List<OutGridInfoDto2018> dto)
        {
            OutServiceInfoDto2018 OutDto = null;
            if (dto != null && dto.Count() > 0)
            {
                OutDto = new OutServiceInfoDto2018
                {
                    OutcfDto = new List<OutcfDto2018>(),
                    OutxmDto = new List<OutxmDto2018>()
                };
                foreach (var item in dto)
                {
                    if (item.yzlx == "1")//药品
                    {
                        if (OutDto.OutcfDto != null && OutDto.OutcfDto.Count() > 0)
                        {
                            //判断处方是否存在
                            OutcfmxDto2018 mzcfmx = new OutcfmxDto2018();
                            mzcfmx.dj = item.dj;
                            mzcfmx.dw = item.dw;
                            mzcfmx.sl = item.sl;
                            mzcfmx.sfxmCode = item.sfxmCode;
                            mzcfmx.jl = item.jl;
                            mzcfmx.jldw = item.jldw;
                            mzcfmx.zfbl = item.zfbl;
                            mzcfmx.zfxz = item.zfxz;
                            mzcfmx.zje = item.zje;
                            var data = OutDto.OutcfDto.FirstOrDefault(p => p.cfh == item.cfh);
                            if (data != null)
                            {
                                data.cfmxDto.Add(mzcfmx);
                            }
                            else
                            {
                                OutcfDto2018 mzcf = new OutcfDto2018
                                {
                                    cfh = item.cfh,
                                    czlx = item.czlx,
                                    cfnm = item.cfnm,
                                    cfmxDto = new List<OutcfmxDto2018>()
                                };
                                mzcf.cfmxDto.Add(mzcfmx);
                                OutDto.OutcfDto.Add(mzcf);
                            }
                        }
                        else
                        {
                            //第一次
                            OutcfDto2018 mzcf = new OutcfDto2018
                            {
                                cfh = item.cfh,
                                cfnm = item.cfnm,
                                cfmxDto = new List<OutcfmxDto2018>()
                            };
                            OutcfmxDto2018 mzcfmx = new OutcfmxDto2018();
                            mzcfmx.dj = item.dj;
                            mzcfmx.dw = item.dw;
                            mzcfmx.sl = item.sl;
                            mzcfmx.sfxmCode = item.sfxmCode;
                            mzcfmx.jl = item.jl;
                            mzcfmx.jldw = item.jldw;
                            mzcfmx.zfbl = item.zfbl;
                            mzcfmx.zfxz = item.zfxz;
                            mzcfmx.zje = item.zje;
                            mzcf.cfmxDto.Add(mzcfmx);
                            OutDto.OutcfDto.Add(mzcf);
                        }
                    }
                    else
                    {
                        //项目
                        OutxmDto2018 mzxm = new OutxmDto2018();
                        mzxm.sfxmCode = item.sfxmCode;
                        mzxm.sfdlCode = item.sfdlCode;
                        mzxm.dj = item.dj;
                        mzxm.sl = item.sl;
                        mzxm.dw = item.dw;
                        mzxm.zlcs = item.zlcs;
                        mzxm.zll = item.zll;
                        mzxm.dwjls = item.dwjls;
                        mzxm.jjcl = item.jjcl;
                        mzxm.zfbl = item.zfbl;
                        mzxm.zfxz = item.zfxz;
                        mzxm.zje = item.zje;
                        OutDto.OutxmDto.Add(mzxm);
                    }
                }
            }
            return OutDto;
        }

        #endregion

        /// <summary>
        /// 明细上传到医保
        /// </summary>
        /// <param name="mzh"></param>
        public void DetailsUploadYb(string mzh)
        {
            var ghEntity = _outPatRegRepo.SelectOutPatientReg(mzh, this.OrganizeId);
            if (string.IsNullOrWhiteSpace(ghEntity.ybjsh))
            {
                throw new FailedException("明细上传医保失败." + "缺少医保结算号");
            }
            var clearResult = YibaoUniteInterface.ClearDetail(ghEntity.ybjsh);
            if (clearResult.Code != 0)
            {
                throw new FailedException("明细上传医保失败." + clearResult.ErrorMsg);
            }
            var data = _outChargeDmnService.GetAllUnSettedListListByMzh(mzh, OrganizeId);
            var ybDetailList = new List<newtouchyibao.Models.DETAIL>();
            foreach (var item in data)
            {
                if (string.IsNullOrWhiteSpace(item.ybdm))
                {
                    throw new FailedException("明细上传医保失败." + "不能开立非医保项目/药品");
                }
                var detail = new newtouchyibao.Models.DETAIL();
                detail.ITEMID = item.yzlx == "1"
                    ? ("YP" + item.cfmxId.Value.ToString())
                    : ("XM" + item.xmnm.Value.ToString());
                //医保0药品 1项目
                detail.DKE085 = item.yzlx == "1" ? "0" : "1";
                detail.DKE012 = item.sfxmCode;
                detail.AKE227 = item.klsj;
                detail.AKE226 = item.ks;
                detail.AKE228 = item.ys;
                detail.AKE229 = item.ysmc;
                detail.AKE002 = item.sfxmmc;
                //药品规格、生产地
                detail.AKE207 = item.dw;
                detail.AKE212 = item.sl;
                detail.AKE208 = item.dj;
                detail.AKE198 = item.zje;
                //其他药品相关
                ybDetailList.Add(detail);
            }
            var preaccResult = YibaoUniteInterface.PreAccount(ghEntity.ybjsh, ybDetailList);
            if (preaccResult.Code != 0)
            {
                throw new FailedException("明细上传医保失败." + preaccResult.ErrorMsg);
            }
            return;
        }
        /// <summary>
        /// 获取所需结算的项目明细，分医保和自立两部分，贵安医保逻辑：医保的上传医保，自立的现金支付
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public guianMainAllOfMzjs GetGuianMainOfMzjs(string mzh)
        {
            GuiAnMzjsPatInfoDto guiAnMzjsPatInfoDto = _outChargeDmnService.GetGuiAnMzjsPatInfoDto(mzh, this.OrganizeId);

            List<FeejsMx> guianZLMainOfMzjsList = new List<FeejsMx>();
            InPtmzjs guianMainOfMzjsModel = new InPtmzjs();
            List<FeejsMx> guianDetailOfMzjsList = new List<FeejsMx>();

            guianMainOfMzjsModel.prm_akc190 = guiAnMzjsPatInfoDto.prm_akc190;
            guianMainOfMzjsModel.prm_aac001 = guiAnMzjsPatInfoDto.prm_aac001;
            guianMainOfMzjsModel.prm_ykc173 = guiAnMzjsPatInfoDto.prm_ykc173;
            guianMainOfMzjsModel.prm_hisfyze = guiAnMzjsPatInfoDto.prm_hisfyze;
            guianMainOfMzjsModel.prm_aka130 = guiAnMzjsPatInfoDto.prm_aka130;
            guianMainOfMzjsModel.prm_yka110 = guiAnMzjsPatInfoDto.prm_yka110;
            guianMainOfMzjsModel.prm_aae013 = guiAnMzjsPatInfoDto.prm_aae013;
            guianMainOfMzjsModel.prm_aae011 = "0";
            guianMainOfMzjsModel.prm_ykc141 = OperatorProvider.GetCurrent().UserName;
            guianMainOfMzjsModel.prm_ykb065 = guiAnMzjsPatInfoDto.prm_ykb065;
            var data = _outChargeDmnService.GetAllUnSettedListListByMzh(mzh, this.OrganizeId);
            decimal ybzje = Convert.ToDecimal(0.0000);
            decimal zlzje = Convert.ToDecimal(0.0000);
            foreach (var item in data)
            {
                //if (string.IsNullOrWhiteSpace(item.ybdm))
                //{
                //    throw new FailedException("获取医保明细失败." + "不能开立非医保项目/药品");
                //}
                FeejsMx detailOfMzjs = new FeejsMx();
                detailOfMzjs.yka105 = item.yzlx == "1"
                    ? ("YP" + item.cfmxId.ToString())
                    : ("XM" + item.xmnm.ToString()); //记账流水号
                detailOfMzjs.ykd125 = item.sfxmCode.ToString();//
                detailOfMzjs.ykd126 = item.sfxmmc.ToString();//
                detailOfMzjs.yka002 = string.IsNullOrEmpty(item.ybdm) ? " " : item.ybdm;//
                detailOfMzjs.yka003 = item.sfxmmc.ToString();//
                detailOfMzjs.akc226 = Convert.ToDecimal(item.sl);//
                detailOfMzjs.akc225 = Convert.ToDecimal(item.dj);//
                detailOfMzjs.yka315 = Convert.ToDecimal(item.zje);//
                detailOfMzjs.yka097 = "";//
                detailOfMzjs.yka098 = "";//
                detailOfMzjs.ykd102 = "";//
                detailOfMzjs.yka099 = "";//
                detailOfMzjs.yka100 = "";//
                detailOfMzjs.yka101 = "";//
                detailOfMzjs.ykd106 = "";//
                detailOfMzjs.yka102 = "";//
                detailOfMzjs.yke123 = item.klsj.ToString("yyyy-MM-dd");//
                detailOfMzjs.ykc141 = OperatorProvider.GetCurrent().UserName;
                detailOfMzjs.aae036 = DateTime.Now.ToString("yyyy-MM-dd");
                detailOfMzjs.aae013 = "";//
                detailOfMzjs.yke201 = "";//
                detailOfMzjs.yka295 = "";//
                detailOfMzjs.aka074 = "";//
                detailOfMzjs.aka070 = "";//
                detailOfMzjs.yae374 = "";//
                detailOfMzjs.yke009 = "";//
                detailOfMzjs.yke186 = "1";//

                if (detailOfMzjs.akc226 > 0 && detailOfMzjs.yka315 > 0)
                {
                    if (!string.IsNullOrWhiteSpace(item.ybdm) && item.ybbz == "1")
                    {

                        ybzje += detailOfMzjs.yka315;
                        guianDetailOfMzjsList.Add(detailOfMzjs);
                    }
                    else
                    {
                        zlzje += detailOfMzjs.yka315;
                        guianZLMainOfMzjsList.Add(detailOfMzjs);
                    }
                }

            }

            guianMainOfMzjsModel.row = guianDetailOfMzjsList;
            guianMainAllOfMzjs guianMainAllOfMzjsModel = new guianMainAllOfMzjs()
            {
                zlzje = zlzje,
                ybzje = ybzje,
                guianYBMainOfMzjs = guianMainOfMzjsModel,
                guianZLMainOfMzjs = guianZLMainOfMzjsList
            };

            return guianMainAllOfMzjsModel;
        }

        public guianMainAllOfMzjs GetGuiAnDetailsMzjsYbTfh(string jsnm, Dictionary<string, decimal> tjsxmDict, string mzh)
        {
            GuiAnMzjsPatInfoDto guiAnMzjsPatInfoDto = _outChargeDmnService.GetGuiAnMzjsPatInfoDto(mzh, this.OrganizeId);

            List<FeejsMx> guianZLMainOfMzjsList = new List<FeejsMx>();
            InPtmzjs guianMainOfMzjsModel = new InPtmzjs();
            List<FeejsMx> guianDetailOfMzjsList = new List<FeejsMx>();

            guianMainOfMzjsModel.prm_akc190 = guiAnMzjsPatInfoDto.prm_akc190;
            guianMainOfMzjsModel.prm_aac001 = guiAnMzjsPatInfoDto.prm_aac001;
            guianMainOfMzjsModel.prm_ykc173 = guiAnMzjsPatInfoDto.prm_ykc173;
            guianMainOfMzjsModel.prm_hisfyze = guiAnMzjsPatInfoDto.prm_hisfyze;
            guianMainOfMzjsModel.prm_aka130 = guiAnMzjsPatInfoDto.prm_aka130;
            guianMainOfMzjsModel.prm_yka110 = guiAnMzjsPatInfoDto.prm_yka110;
            guianMainOfMzjsModel.prm_aae013 = guiAnMzjsPatInfoDto.prm_aae013;
            guianMainOfMzjsModel.prm_aae011 = "0";
            guianMainOfMzjsModel.prm_ykc141 = OperatorProvider.GetCurrent().UserName;
            guianMainOfMzjsModel.prm_ykb065 = guiAnMzjsPatInfoDto.prm_ykb065;
            var data = _outChargeDmnService.GetAllUnSettedListListByJsnm(jsnm, this.OrganizeId);
            decimal ybzje = Convert.ToDecimal(0.0000);
            decimal zlzje = Convert.ToDecimal(0.0000);
            decimal tkzje = Convert.ToDecimal(0.0000);
            foreach (var item in data)
            {
                //if (string.IsNullOrWhiteSpace(item.ybdm))
                //{
                //    throw new FailedException("获取医保明细失败." + "不能开立非医保项目/药品");
                //}
                FeejsMx detailOfMzjs = new FeejsMx();
                detailOfMzjs.yka105 = item.yzlx == "1"
                    ? ("YP" + item.cfmxId.ToString())
                    : ("XM" + item.xmnm.ToString()); //记账流水号
                detailOfMzjs.ykd125 = item.sfxmCode.ToString();//
                detailOfMzjs.ykd126 = item.sfxmmc.ToString();//
                detailOfMzjs.yka002 = string.IsNullOrEmpty(item.ybdm) ? " " : item.ybdm;//
                detailOfMzjs.yka003 = item.sfxmmc.ToString();//
                detailOfMzjs.akc225 = Convert.ToDecimal(item.dj);//
                detailOfMzjs.akc226 = item.sl;
                foreach (var itemtf in tjsxmDict)
                {
                    if (itemtf.Key == item.jsmxnm.ToString())
                    {
                        detailOfMzjs.akc226 = Convert.ToDecimal(item.sl - itemtf.Value);//
                        tkzje += (itemtf.Value * Convert.ToDecimal(item.dj));
                    }
                }
                detailOfMzjs.yka315 = Convert.ToDecimal(detailOfMzjs.akc226 * item.dj);//
                detailOfMzjs.yka097 = "";//
                detailOfMzjs.yka098 = "";//
                detailOfMzjs.ykd102 = "";//
                detailOfMzjs.yka099 = "";//
                detailOfMzjs.yka100 = "";//
                detailOfMzjs.yka101 = "";//
                detailOfMzjs.ykd106 = "";//
                detailOfMzjs.yka102 = "";//
                detailOfMzjs.yke123 = item.klsj.ToString("yyyy-MM-dd");//
                detailOfMzjs.ykc141 = OperatorProvider.GetCurrent().UserName;
                detailOfMzjs.aae036 = DateTime.Now.ToString("yyyy-MM-dd");
                detailOfMzjs.aae013 = "";//
                detailOfMzjs.yke201 = "";//
                detailOfMzjs.yka295 = "";//
                detailOfMzjs.aka074 = "";//
                detailOfMzjs.aka070 = "";//
                detailOfMzjs.yae374 = "";//
                detailOfMzjs.yke009 = "";//
                detailOfMzjs.yke186 = "1";//
                if (detailOfMzjs.akc226 > 0 && detailOfMzjs.yka315 > 0)
                {
                    if (!string.IsNullOrWhiteSpace(item.ybdm) && item.ybbz == "1")
                    {

                        ybzje += detailOfMzjs.yka315;
                        guianDetailOfMzjsList.Add(detailOfMzjs);
                    }
                    else
                    {
                        zlzje += detailOfMzjs.yka315;
                        guianZLMainOfMzjsList.Add(detailOfMzjs);
                    }
                }
            }

            guianMainOfMzjsModel.row = guianDetailOfMzjsList;
            guianMainAllOfMzjs guianMainAllOfMzjsModel = new guianMainAllOfMzjs()
            {
                tkzje = tkzje,
                zlzje = zlzje,
                ybzje = ybzje,
                //prm_ack190= "GZ190425100277497844",
                guianYBMainOfMzjs = guianMainOfMzjsModel,
                guianZLMainOfMzjs = guianZLMainOfMzjsList
            };

            return guianMainAllOfMzjsModel;
        }
        
        #region 重庆医保

        public void GetChongQingMainOfMzjs(string mzh, string cfnm, out decimal ybzje, out decimal zfzje)
	    {
		    var data = _outChargeDmnService.GetCQZFUnSettedListByMzh( mzh, cfnm, this.OrganizeId);

            ybzje = Convert.ToDecimal(0.0000);
		    zfzje = Convert.ToDecimal(0.0000);
		    foreach (var item in data)
		    {
                if (item.issc == 1)
                {
                    ybzje += Convert.ToDecimal(item.je);
                }
                else
                {
                    zfzje += Convert.ToDecimal(item.je);
                }
		    }
		}

	    public UploadPrescriptionsInPut GetCQDetailsMzjsYbTfh(string mzh, string jsnm,
		    Dictionary<string, decimal> tjsxmDict, out decimal ybzje, out decimal zfzje, out decimal tfzje)
	    {
			var ybdata = _outChargeDmnService.GetCQYBTfListByJsnm(jsnm, mzh, this.OrganizeId);
		    var zfdata = _outChargeDmnService.GetCQZFTfListByJsnm(jsnm, mzh, this.OrganizeId);
		    List<UploadPrescriptionsListInPut> yblist = new List<UploadPrescriptionsListInPut>();
            ybzje = Convert.ToDecimal(0.0000);
		    tfzje = Convert.ToDecimal(0.0000);
			zfzje = Convert.ToDecimal(0.0000);
		    foreach (var item in ybdata)
		    {
			    
				foreach (var itemtf in tjsxmDict)
			    {
				    if (itemtf.Key == item.cxmxlsh)
				    {
					    item.sl = Convert.ToDecimal(item.sl) - itemtf.Value;//
					    tfzje += (itemtf.Value * item.dj);
				    }
			    }
			    item.cxmxlsh = "";//存放jsmxnm，该字段可能恒为null，为了分页获取数据（明细上传个数限制）
			    item.jbr = this.UserIdentity.UserCode;
                if (!string.IsNullOrEmpty(item.sl.ToString()) && !string.IsNullOrEmpty(item.je.ToString()) && Convert.ToDecimal(item.sl) > 0 && Convert.ToDecimal(item.je) > 0)
				{
					item.je = Convert.ToDecimal(item.sl * item.dj);
                    ybzje += Convert.ToDecimal(item.je);
				    yblist.Add(item);
			    }
		    }
		  
				foreach (var item in zfdata)
				{
					foreach (var itemtf in tjsxmDict)
					{
						if (itemtf.Key == item.cxmxlsh)
						{
							item.sl = Convert.ToDecimal(item.sl) - itemtf.Value;//
							tfzje += (itemtf.Value * item.dj);
						}
					}
					if (!string.IsNullOrEmpty(item.sl.ToString()) && !string.IsNullOrEmpty(item.je.ToString()) && Convert.ToDecimal(item.sl) > 0 && Convert.ToDecimal(item.je) > 0)
					{
						item.je = Convert.ToDecimal(item.sl * item.dj);
						zfzje += Convert.ToDecimal(item.je);
					}
				}
			
		   
		    UploadPrescriptionsInPut ChongQingMainAllOfMzjsModel = new UploadPrescriptionsInPut()
		    {
			    zymzh = mzh,
			    cflist = yblist
			};

		    return ChongQingMainAllOfMzjsModel;
		}

	    public void SaveCqybUploadInPres(List<UploadPrescriptionsListInPut> cflist, string zymzh, string jytype)
	    {
		    List<CqybUploadInPres04Entity> entitylist = new List<CqybUploadInPres04Entity>();
			if (cflist != null && cflist.Count>0)
		    {
			    foreach (var item in cflist)
			    {
				    //if (entitylist.FindAll(p=>p.cfh==item.cfh && p.OrganizeId==this.OrganizeId).Count<1)
				    //{

						CqybUploadInPres04Entity entity = new CqybUploadInPres04Entity();
					    entity.OrganizeId = this.OrganizeId;
					    entity.zt = "1";
                        entity.cfh = item.cfh;
					    entity.jytype = jytype;
					    entity.zymzh = zymzh;
                        entity.je = item.je;
                        entity.ysbm = item.ysbm;
                        entity.gjmldm = item.gjmldm;
                        entity.gjysbm = item.gjysbm;
					    entity.Create();
					    entitylist.Add(entity);
				    //}
				}
			    _cqybUploadInPres04Repo.SaveCqybUploadInPres(entitylist);
		    }
        }
        public void SaveCqybUploadInPres(string zymzh, string jytype, string cfh, string pch)
        {
            List<CqybUploadInPres04Entity> entitylist = new List<CqybUploadInPres04Entity>();
            var cfhs = cfh.Split(',');
            for (int i=0;i<cfhs.Length;i++)
            {
                CqybUploadInPres04Entity entity = new CqybUploadInPres04Entity();
                entity.OrganizeId = this.OrganizeId;
                entity.zt = "1";
                entity.cfh = cfhs[i];
                entity.pch = pch;
                entity.jytype = jytype;
                entity.zymzh = zymzh;
                entity.px = 0;
                entity.Create();
                entitylist.Add(entity);
            }
            _cqybUploadInPres04Repo.SaveCqybUploadInPres(entitylist);
        }
        public void UpdateCqyb04InPres(string zymzh, string cfh,string orgId)
        {
            var entity = _cqybUploadInPres04Repo.FindEntity(p => p.zymzh == zymzh && p.cfh == cfh && p.zt == "1" && p.OrganizeId == orgId);
            if (entity!=null)
            {
                entity.zt = "0";
                entity.Modify();
                _cqybUploadInPres04Repo.Update(entity);
            }
        }
        public void UpDateUploadPch(string zymzh, string cfh, string pch, string orgId) {
            var entity = _cqybUploadInPres04Repo.FindEntity(p => p.zymzh == zymzh && p.cfh == cfh && p.pch == pch && p.zt == "1" && p.OrganizeId == orgId);
            if (entity != null)
            {
                entity.zt = "0";
                entity.Modify();
                _cqybUploadInPres04Repo.Update(entity);
            }
        }
        /// <summary>
        /// 获取处方号红冲数据，查询已经上传且未结算的处方号，进行红冲处理
        /// </summary>
        /// <param name="zymzh"></param>
        /// <param name="type">1门诊 2住院</param>
        /// <returns></returns>
        public List<RefundPrescriptionsInPut> GetCqyb10Data(string zymzh,string type)
	    {
		    return _outPatientDmnService.GetCqyb10Data(zymzh, this.OrganizeId, type);
	    }

        public List<Input_2205> GetCqybMxCxData(string zymzh, string type, string ybver) {
            return _outPatientDmnService.GetCqybMxCxData(zymzh, this.OrganizeId, type,ybver);
        }


        public object ZFToYB_Step_1(string mzh)
	    {
		    if (string.IsNullOrWhiteSpace(mzh))
		    {
			    throw new FailedException("缺少参数.门诊号");
		    }
		    var orgId = this.OrganizeId;
			var ghxx = _outPatRegRepo.IQueryable().Where(p => p.mzh == mzh && p.OrganizeId == orgId).FirstOrDefault();
		    if (ghxx == null)
		    {
			    throw new FailedException("住院信息未找到");
		    }
		    if (ghxx.zt == "0")
		    {
			    throw new FailedException("本次挂号已作废");
		    }
		    if (!(ghxx.brxz == "0"))
		    {
			    throw new FailedException("当前非自费");
		    }

		    return new object();
	    }
	    public object ZFToYB_Step_2(string zyh)
	    {
		    var orgId = this.OrganizeId;
		    //var hasNonYbFee = _hospFeeDmnService.CheckHasNonYbFee(zyh, orgId);
		    //if (hasNonYbFee)
		    //{
		    //    throw new FailedException("已产生非医保相关费用");
		    //}

		    return new object();
	    }

	    public object ZFToYB_Step_3(string mzh, string sbbh, string xm)
	    {
		    var orgId = this.OrganizeId;
		    var ghxx = _outPatRegRepo.IQueryable().Where(p => p.mzh == mzh && p.OrganizeId == orgId).FirstOrDefault();

		    int patid = ghxx.patid;
		    SysPatientBasicInfoEntity xtbrxx = null;

		    //var xtbrxxList = _sysPatiInfoRepo.IQueryable().Where(p => p.sbbh == sbbh && p.brxz == "1" && p.OrganizeId == orgId && p.zt == "1").ToList();
		    //if (xtbrxxList.Count == 1)
		    //{
			   // //if (xtbrxxList[0].patid != ghxx.patid)
			   // //{
				  // // throw new FailedException("该社保卡在系统里已绑定其他身份,不能再用来转该患者医保信息，请使用医保卡重新挂号就诊！社保编号：" + sbbh.ToString());
			   // //}
			   // xtbrxx = xtbrxxList[0];
		    //}
		    //else if (xtbrxxList.Count == 0)
		    //{
			   // xtbrxx = _sysPatiInfoRepo.IQueryable().Where(p => p.patid == patid && p.OrganizeId == orgId && p.zt == "1").First();
			   // if (!string.IsNullOrWhiteSpace(xtbrxx.sbbh) && xtbrxx.sbbh != sbbh)
			   // {
				  //  throw new FailedException("该住院患者已绑定其他社保卡");
			   // }
		    //}
		    //else
		    //{
			   // throw new FailedException("数据异常，该社保卡在基础信息中存在多条，无法定位");
		    //}

		    if (xtbrxx.xm != xm)
		    {
			    throw new FailedException("异常，就诊姓名与医保卡姓名不一致，请先修改患者姓名");
		    }

			//var ryzd1 = _hospMultiDiagnosisRepo.IQueryable().Where(p => p.zyh == zyh && p.zt == "1" && p.OrganizeId == orgId).OrderBy(p => p.zdpx).FirstOrDefault();

			//var ksmc = _sysDepartmentRepo.GetNameByCode(zybrxx.ks, orgId);
            //国家编码
            var ysInfo = _outPatientSettleDmnService.GetDepartmentDoctorIdC(orgId, ghxx.ks, ghxx.ys);
            return new
			{
				patid = xtbrxx.patid,//patid,
                ghxx = ghxx,
				xtbrxx = xtbrxx,
                yllb= ghxx.mjzbz,
                rygjysbm= ysInfo
            };
		}

	    public object ZFToYB_Step_6(string mzh, int patid, ZYToYBDto patInfo)
	    {
		    var orgId = this.OrganizeId;

		    _patientBasicInfoDmnService.OutPatZFchangetoYB(orgId, mzh, patid, patInfo);

		    return new object();
	    }

	    public object ZFToYB_Step_8(string mzh)
	    {
		    UpdatebrxzRequest par = new UpdatebrxzRequest { zyh = mzh, brxzCode = "1", brxzmc = "医保病人" };
		    //同步数据至CPOE
		    SiteCISAPIHelper.UpdatebrxzInfo(par);
		    return new object();
	    }

        #endregion

    }
}
