/*********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description： 住院管理》账户管理》
// Author：HLF
// CreateDate： 2016/12/7 17:01:42 
//**********************************************************/

using FrameworkBase.MultiOrg.Application;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.DTO.InputDto;
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
using System.Text.RegularExpressions;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountManageApp : AppBase, IAccountManageApp
    {
        private readonly IHosPatAccDmnService _hospatService;   //账户管理综合查询
        private readonly IHospPatientBasicInfoRepo _hospatBasicRepos; //住院病人基本信息
        private readonly IFinanceReceiptRepo _finRecRepos; //收据凭证号记录
        private readonly ISysPatientBasicInfoRepo _SysPatBasicInfoRepository;
        private readonly ISysAccountRepo _sysAccountRepo;
        private readonly IReserveDmnService _reserveDmnService;
        private readonly IPatientBasicInfoDmnService _patientBasicInfoDmnService;

        #region 预交金

        /// <summary>
        /// 查询患者基本信息和账户信息
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        public ReserveDto GetHosPatAccInfo(int patid)
        {
            if (string.IsNullOrEmpty(patid.ToString()))
            {
                throw new FailedException("病人内码不存在");
            }
            //在基本信息表中查询
            OutpatAccInfoDto e = _patientBasicInfoDmnService.GetPatInfoByPatid(patid, OrganizeId);
            if (e == null)
            {
                throw new FailedException("病人不存在");
            }
            var patInfo = e.MapperTo<OutpatAccInfoDto, ReservePatientInfoDto>();
            var zhentity = _sysAccountRepo.FindEntity(p => p.patid == patid && p.OrganizeId == OrganizeId && p.zt == "1");
            if (zhentity == null)
            {
                throw new FailedException("缺少账户");
            }
            var hospatDto = new ReserveDto();
            patInfo.zhCode = zhentity.zhCode;
            hospatDto.patInfo = patInfo;
            hospatDto.accPayInfoList = GetPayList(zhentity.zhCode,null);
            return hospatDto;
        }

        /// <summary>
        /// 查询患者基本信息和账户信息
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        public ReserveDto GetHosPatAccInfo(int patid,string zhxz)
        {
            if (string.IsNullOrEmpty(patid.ToString()))
            {
                throw new FailedException("病人内码不存在");
            }
            //在基本信息表中查询
            OutpatAccInfoDto e = _patientBasicInfoDmnService.GetPatInfoByPatid(patid, OrganizeId);
            if (e == null)
            {
                throw new FailedException("病人不存在");
            }

            int? xz=null;
            if (!string.IsNullOrWhiteSpace(zhxz))
            {
                xz = Convert.ToInt16(zhxz);
            }
            var patInfo = e.MapperTo<OutpatAccInfoDto, ReservePatientInfoDto>();
            var zhentity = _sysAccountRepo.FindEntity(p =>
                p.patid == patid && p.zhxz == xz && p.OrganizeId == OrganizeId && p.zt == "1");
            if (zhentity == null)
            {
                throw new FailedException("缺少账户");
            }
            var hospatDto = new ReserveDto();
            patInfo.zhCode = zhentity.zhCode;
            patInfo.zhxz = zhentity.zhxz;
            hospatDto.patInfo = patInfo;
            hospatDto.accPayInfoList = GetPayList(zhentity.zhCode, zhxz);
            return hospatDto;
        }

        /// <summary>
        /// 获取患者基本信息
        /// add by HLF
        /// 根据住院号查询
        /// </summary>
        /// <param name="zyh">住院号</param>
        /// <param name="kh">卡号</param>
        /// <param name="xm">姓名</param>
        /// <returns></returns>
        public HosPatAccountActionVO GetHosPatInfo(string zyh)
        {
            HosPatAccountActionVO hosPatInfoVo = null;
            if (!string.IsNullOrEmpty(zyh))
            {
                //联合查询病人信息
                hosPatInfoVo = _hospatService.GetHosPatInfo(zyh,OrganizeId);
                if (hosPatInfoVo == null)
                {
                    throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST"); //住院病人基本信息不存在!
                }
                if (hosPatInfoVo.zh < 0)
                {
                    throw new FailedCodeException("HOSP_ERROR_PATIENT_THERE_IS_NO_YJJ_ACCOUNT"); //当前病人不存在预交款帐户信息！
                }

                //添加账户性质名称
                if (hosPatInfoVo.zhxz == ((int)EnumXTZHXZ.ZYYJKZH))
                {
                    hosPatInfoVo.zhxzMC = EnumXTZHXZ.ZYYJKZH.GetDescription();
                }
            }
            else
            {
                throw new FailedException("HOSP_ZYH_CARD_ISNULL"); //请输入有效的住院号或卡号
            }

            return hosPatInfoVo;
        }

        /// <summary>
        /// 获取患者账户收支记录
        /// </summary>
        /// <returns></returns>
        public List<PatAccPayVO> GetPayList(int zh, string zhxz)
        {
            List<PatAccPayVO> hospayList = _reserveDmnService.GetAccPayInfo(zh, this.OrganizeId,zhxz);
            foreach (PatAccPayVO item in hospayList)
            {
                item.szxzmc = ((EnumSZXZ)item.szxz).GetDescription();
            }
            return hospayList;
        }

        /// <summary>
        /// 获取凭证号
        /// </summary>
        public string GetFinRepSJPZH()
        {
            string sjh = _finRecRepos.getDQSJH(UserIdentity.UserCode, this.OrganizeId);
            if (!string.IsNullOrEmpty(sjh))
            {
                return sjh;
            }
            return sjh;
        }

        /// <summary>
        /// 后台验证收据凭证号
        /// </summary>
        /// <returns></returns> 
        public bool CheckSJPZH(string pzh, out FinanceReceiptEntity sjEntity, out string type)
        {
            bool flag = false;
            sjEntity = new FinanceReceiptEntity();
            type = string.Empty;
            
            flag = _finRecRepos.checkSJH(pzh, UserIdentity.UserCode, out sjEntity, out type, this.OrganizeId);
            return flag;
        }

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="zffsbh"></param>
        /// <param name="zffsmc"></param>
        /// <param name="zfje"></param>
        public bool PayDepositPost(DeposDto dto, out string szid)
        {
            
            if (string.IsNullOrEmpty(dto.patid.ToString()))
            {
                throw new FailedException("病人基本信息不存在!"); 
            }
            
            if (string.IsNullOrEmpty(dto.zffsbh.ToString()) || string.IsNullOrEmpty(dto.zffsmc.ToString()))
            {
                throw new FailedException("支付方式不能为空");
            }
            if (string.IsNullOrEmpty(dto.zfje.ToString()) || dto.zfje <= 0)
            {
                throw new FailedException("支付金额格式不正确"); 
            }
            if (string.IsNullOrEmpty(dto.pzh))
            {
                throw new FailedException("凭证号不能为空"); 
            }

            SysAccountRevenueAndExpenseEntity zhszjl = null;
            SysAccountEntity oldzh = null;
            SysAccountEntity newzh = null;
            FinanceReceiptEntity oldcwsj = null;
            FinanceReceiptEntity newcwsj = null;

            AccPayDeposit(dto,out zhszjl, out oldzh, out newzh); //添加收支记录

            //获取原来的收据凭证Entity  
            oldcwsj = _finRecRepos.getSJKEntity(UserIdentity.UserCode, this.OrganizeId);
            _finRecRepos.DetacheEntity(oldcwsj);

            //新的收据凭证信息
            FinanceReceiptEntity sjEntity = null;
            string type = string.Empty;
            CheckSJPZH(dto.pzh, out sjEntity, out type);
            newcwsj = sjEntity;

            var sqlVO = new PatAccDataVO()
            {
                szjl = new SysAccountRevenueAndExpenseEntity(),
                oldzh = new SysAccountEntity(),
                oldcwsj = new FinanceReceiptEntity()
            };
            sqlVO.szjl = zhszjl;
            sqlVO.oldzh = oldzh;
            sqlVO.oldcwsj = oldcwsj;
            //保存至数据库
            _reserveDmnService.PatAccDB(sqlVO, newzh, newcwsj, type);
            szid = zhszjl.Id;
            return true;
        }

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="zffsbh"></param>
        /// <param name="zffsmc"></param>
        /// <param name="zfje"></param>
        public SysAccountEntity PayDepositPostAcnt(DeposDto dto)
        {

            if (string.IsNullOrEmpty(dto.patid.ToString()))
            {
                throw new FailedException("病人基本信息不存在!");
            }

            if (string.IsNullOrEmpty(dto.zffsbh.ToString()) || string.IsNullOrEmpty(dto.zffsmc.ToString()))
            {
                throw new FailedException("支付方式不能为空");
            }
            if (string.IsNullOrEmpty(dto.zfje.ToString()) || dto.zfje <= 0)
            {
                throw new FailedException("支付金额格式不正确");
            }
            if (string.IsNullOrEmpty(dto.pzh))
            {
                throw new FailedException("凭证号不能为空");
            }

            if (dto.zh == -1)
            {
                var accEty = _sysAccountRepo.FindEntity(p =>
                    p.patid == dto.patid && p.zhxz == dto.zhxz && p.OrganizeId == this.OrganizeId && p.zt == "1");
                if (accEty != null)
                {
                    dto.zh = accEty.zhCode;
                }
                else
                {
                    throw new FailedException("账户异常，不可充值");
                }
            }

            SysAccountRevenueAndExpenseEntity zhszjl = null;
            SysAccountEntity oldzh = null;
            SysAccountEntity newzh = null;
            FinanceReceiptEntity oldcwsj = null;
            FinanceReceiptEntity newcwsj = null;

            AccPayDeposit(dto, out zhszjl, out oldzh, out newzh); //添加收支记录

            //获取原来的收据凭证Entity  
            oldcwsj = _finRecRepos.getSJKEntity(UserIdentity.UserCode, this.OrganizeId);
            _finRecRepos.DetacheEntity(oldcwsj);

            //新的收据凭证信息
            FinanceReceiptEntity sjEntity = null;
            string type = string.Empty;
            CheckSJPZH(dto.pzh, out sjEntity, out type);
            newcwsj = sjEntity;

            var sqlVO = new PatAccDataVO()
            {
                szjl = new SysAccountRevenueAndExpenseEntity(),
                oldzh = new SysAccountEntity(),
                oldcwsj = new FinanceReceiptEntity()
            };
            sqlVO.szjl = zhszjl;
            sqlVO.oldzh = oldzh;
            sqlVO.oldcwsj = oldcwsj;
            //保存至数据库
            _reserveDmnService.PatAccDB(sqlVO, newzh, newcwsj, type);
            return newzh;
        }

        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="zffsbh"></param>
        /// <param name="zffsmc"></param>
        /// <param name="zfje"></param>
        public bool ExitDepositPost(DeposDto dto)
        {
            if (string.IsNullOrEmpty(dto.patid.ToString()))
            {
                throw new FailedException("病人内码不存在");
            }
            if (string.IsNullOrEmpty(dto.zffsbh.ToString()) || string.IsNullOrEmpty(dto.zffsmc.ToString()))
            {
                throw new FailedException("支付方式不能为空");
            }
            if (string.IsNullOrEmpty(dto.zfje.ToString()) || dto.zfje >= 0)
            {
                throw new FailedException("支付金额格式不正确");
            }
            if (string.IsNullOrEmpty(dto.pzh))
            {
                throw new FailedException("凭证号不能为空");
            }

            SysAccountRevenueAndExpenseEntity zhszjl = null;
            SysAccountEntity oldzh = null;
            SysAccountEntity newzh = null;
            FinanceReceiptEntity oldcwsj = null;
            FinanceReceiptEntity newcwsj = null;

            AccPayDeposit(dto, out zhszjl, out oldzh, out newzh); //添加收支记录 负的 

            var sqlVO = new PatAccDataVO()
            {
                szjl = new SysAccountRevenueAndExpenseEntity(),
                oldzh = new SysAccountEntity(),
                oldcwsj = new FinanceReceiptEntity()
            };
            sqlVO.szjl = zhszjl;
            sqlVO.oldzh = oldzh;
            sqlVO.oldcwsj = oldcwsj;
            //保存至数据库
            _reserveDmnService.PatAccDB(sqlVO, newzh, newcwsj, "");
            return true;
        }

        /// <summary>
        /// 余额全退
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool RefundAccount(DeposDto dto)
        {

            if (string.IsNullOrEmpty(dto.patid.ToString()))
            {
                throw new FailedException("病人内码不存在");
            }
            if (string.IsNullOrEmpty(dto.zffsbh.ToString()) || string.IsNullOrEmpty(dto.zffsmc.ToString()))
            {
                throw new FailedException("支付方式不能为空");
            }
            if (string.IsNullOrEmpty(dto.zfje.ToString()) || dto.zfje >= 0)
            {
                throw new FailedException("支付金额格式不正确");
            }

            SysAccountRevenueAndExpenseEntity payEntity = null;
            SysAccountEntity oldAccEntity = null;
            SysAccountEntity newAccEntity = null;

            payEntity = new SysAccountRevenueAndExpenseEntity();
            payEntity.szxz = dto.szxz;
            //payEntity.pzh = pzhList;
            payEntity.zhCode = dto.zh;
            payEntity.patid = dto.patid;
            payEntity.szje = dto.zfje;
            payEntity.xjzffs = dto.zffsbh.ToString();
            payEntity.zhye = payEntity.szje + GetzhYe(dto.zh);
            payEntity.zt = "1";
            payEntity.OrganizeId = OrganizeId;
            payEntity.Create(true);
            //更新账户信息
            oldAccEntity = _sysAccountRepo.FindEntity(p => p.zhCode == dto.zh && p.OrganizeId == OrganizeId && p.zt == "1");
            _sysAccountRepo.DetacheEntity(oldAccEntity);
            newAccEntity = oldAccEntity.Clone();
            newAccEntity.zhye = payEntity.zhye;

            var sqlVO = new PatAccDataVO()
            {
                szjl = new SysAccountRevenueAndExpenseEntity(),
                oldzh = new SysAccountEntity(),
                oldcwsj = new FinanceReceiptEntity()
            };
            sqlVO.szjl = payEntity;
            sqlVO.oldzh = oldAccEntity;
            sqlVO.oldcwsj = null;
            //保存至数据库
            _reserveDmnService.PatAccDB(sqlVO, newAccEntity, null, "");
            return true;
        }

        /// <summary>
        /// 添加支付、退款记录
        /// </summary>
        /// <param name="dto"></param>
        private void AccPayDeposit(DeposDto dto, out SysAccountRevenueAndExpenseEntity payEntity, out SysAccountEntity oldAccEntity, out SysAccountEntity newAccEntity)
        {
            
            payEntity = new SysAccountRevenueAndExpenseEntity();
            payEntity.szxz = dto.szxz;
            payEntity.pzh = dto.pzh;
            payEntity.zhCode = dto.zh;
            payEntity.patid = dto.patid;
            payEntity.szje = dto.zfje;
            payEntity.xjzffs = dto.zffsbh.ToString();
            payEntity.zhye = payEntity.szje + GetzhYe(dto.zh);
            payEntity.zt = "1";
            payEntity.OrganizeId = OrganizeId;
            payEntity.Create(true);
            //更新账户信息
            oldAccEntity = _sysAccountRepo.FindEntity(p=>p.zhCode==dto.zh&&p.OrganizeId==OrganizeId&&p.zt=="1");
            _sysAccountRepo.DetacheEntity(oldAccEntity);
            newAccEntity = oldAccEntity.Clone();
            newAccEntity.zhye = payEntity.zhye;
        }

        /// <summary>
        /// 校验现金
        /// </summary>
        /// <returns></returns>
        private bool checkXj(decimal zfje)
        {
            string xj = zfje.ToString();
            if (xj == "-" || xj == "+")
            {
                xj = xj + "0";
            }
            if (string.IsNullOrEmpty(xj))
            {
                return true;
            }
            if (xj.Substring(xj.Length - 1, 1) == ".")
            {
                xj = xj + "0";
            }

            decimal dTemp = 0m;
            if (!decimal.TryParse(xj, out dTemp))
            {
                return false;
            }

            Regex regex = new Regex(@"^[+-]?\d*\.?\d{0,2}$");
            if (!regex.IsMatch(xj))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获得账户余额
        /// </summary>
        public decimal GetzhYe(int zh)
        {
            var zhEntityList = _sysAccountRepo.IQueryable(p => p.zhCode == zh && p.OrganizeId == OrganizeId && p.zt == "1").ToList();
            if (zhEntityList.Count == 0)
            {
                throw new FailedException("病人不存在账户");
            }
            else if (zhEntityList.Count > 1)
            {
                throw new FailedException("账户异常");
            }
            return zhEntityList[0].zhye;
        }

        /// <summary>
        /// 获得账户余额
        /// </summary>
        public decimal GetZhyeByPatid(int patid)
        {
            var zhEntityList = _sysAccountRepo.IQueryable(p => p.patid == patid && p.OrganizeId == OrganizeId && p.zt == "1").ToList();
            if (zhEntityList.Count == 0)
            {
                return 0;
            }
            else if (zhEntityList.Count > 1)
            {
                throw new FailedException("账户异常");
            }
            return zhEntityList[0].zhye;
        }

        public decimal GetZhyeByPatid(int patid,int zhxz)
        {
            var zhEntityList = _sysAccountRepo.IQueryable(p => p.patid == patid && p.zhxz==zhxz && p.OrganizeId == OrganizeId && p.zt == "1").ToList();
            if (zhEntityList.Count == 0)
            {
                return 0;
            }
            else if (zhEntityList.Count > 1)
            {
                throw new FailedException("账户异常");
            }
            return zhEntityList[0].zhye;
        }

        #endregion

    }
}
