using System;
using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO.InputDto.OutpatientAccounting;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class BookkeepInpatientApp : AppBase, IBookkeepInpatientApp
    {
        private readonly IBookkeepInHosDmnService _bookkeepInHosDmnService;
        private readonly IHosPatAccDmnService _hosPatAccDmnService;
        private readonly IOutPatientDmnService _outPatientDmnService;
        private readonly IOutpatientAccountDetailRepo _outpatientAccountDetailRepo;
        private readonly IHospPatientBasicInfoRepo _hospPatientBasicInfoRepo;
        private readonly ISysUserDmnService _sysUserDmnService;
        #region 康复项目（药品）记账（由GRS收费项目（药品）扩展而来）

        /// <summary>
        /// 根据住院号 获取 病人 记账 信息
        /// </summary>
        /// <param name="zyh"></param>
        public HosAccountingPatStatusDetailDto GetAccountingStatusDetail(string zyh)
        {
            //获取住院记账病人信息（基本信息、科室等）
            var patInfo = getHospAccountingPatInfo(zyh);

            //zyh可以记账

            //查询此次住院 已 记账 计划
            var jzjhList = _bookkeepInHosDmnService.BeenAccountingPlanQuery(this.OrganizeId, zyh);

            //查询是否有中途结算
            var ztjsrq = _bookkeepInHosDmnService.GetLastValidMidwaySettTime(zyh, this.OrganizeId);

            var resultDto = new HosAccountingPatStatusDetailDto()
            {
                patInfo = patInfo,
                jzjhList = jzjhList,
                zjrq= ztjsrq,
            };
            return resultDto;
        }

        /// <summary>
        /// 提交记账计划
        /// </summary>
        /// <param name="zyh">住院号</param>
        /// <param name="xmList">记账项目（药品）列表（如果是单项目（药品），请先包装成数组）</param>
        public void SubmitAccountingPlan(string zyh, IList<InpatientAccountingPlanItemDto> xmList)
        {
            if (xmList == null || xmList.Count == 0)
            {
                throw new FailedException("缺少记账计划");
            }

            //获取当前住院病人基本信息
            var zyInfo = _hospPatientBasicInfoRepo.IQueryable().FirstOrDefault(p => p.zt == "1" && p.zyh == zyh && p.OrganizeId == this.OrganizeId);

            //if (xmList.Any(p => (p.ysList == null || p.ysList.Count == 0) && p.yzxz == "1"))
            //{
            //    throw new FailedException("请给收费选择治疗师");
            //}

            var jhmxEntityList = new List<HospAccountingPlanDetailEntity>();
            IList<zxGirdDto> itemList = new List<zxGirdDto>();
            var updateToStopJzjhxmIdList = new List<string>();

            //同一次提交
            var jzjhEntity = new HospAccountingPlanEntity()
            {
                jzjhId = Guid.NewGuid().ToString(),
                OrganizeId = this.OrganizeId,
                zyh = zyh,
            };
            jzjhEntity.Create(true);
            foreach (var item in xmList)
            {
                string ys = null;
                string ysmc = null;
                string ks = null;
                string ksmc = null;
                ys = zyInfo.doctor;
                ks = zyInfo.ks;

                int? zzll = item.sl * item.zll;//整个记账
                int dcsl = 0;//单次数量
                int zsl;
                if (item.sfxmCode != null)
                {
                    dcsl = CommmHelper.CalcSfxmSl(item.zll, item.dwjls, item.jjcl);
                    //总量
                    zsl = (dcsl * item.sl).ToInt();
                }
                item.yzxz = (item.yzxz ?? "").Trim();   //


                //if (item.yzxz == ((int)EnumYZXZ.LSYZ).ToString())
                //{
                //    if (item.EndDate.HasValue && item.EndDate.Value != item.StartDate)
                //    {
                //        //异常：临时医嘱， 日期 是 一个范围
                //        throw new FailedException("临时性记账日期范围错误");
                //    }
                //}
                //else
                //{
                //    if (item.EndDate.HasValue && item.EndDate.Value <= item.StartDate)
                //    {
                //        //异常：长期医嘱， 日期 不是 一个范围
                //        throw new FailedException("长期性记账日期范围错误");
                //    }
                //}

                //收费项目
                //新增记录
                var jhmxEntity = new HospAccountingPlanDetailEntity()
                {
                    jzjhmxId = Guid.NewGuid().ToString(),
                    OrganizeId = this.OrganizeId,
                    zyh = zyh,
                    jzjhId = jzjhEntity.jzjhId,
                    yzxz = item.yzxz,
                    yzlx = "2",
                    StartDate = item.StartDate < new DateTime(2000, 01, 01) ? Constants.MaxDate : item.StartDate,
                    EndDate = Constants.MaxDate,
                    sfxmCode = item.sfxmCode,
                    duration = item.duration,
                    sl = dcsl,
                    LastEexcutionTime = null,
                    zxzt = (int)EnumJzjhZXZT.None,
                    ys = ys,
                    ysmc = ysmc,
                    ks = ks,
                    ksmc = ksmc,
                    bz = item.bz,
                    dj = item.dj,
                    kflb = item.kflb,
                    zcs = item.sl,
                    zll = item.zll,
                };
                jhmxEntity.Create(true);
                jhmxEntityList.Add(jhmxEntity);
                if (!string.IsNullOrWhiteSpace(item.jzjhmxId))
                {
                    //这是一条 修改历史 ，更新原纪录状态 至 已停止
                    updateToStopJzjhxmIdList.Add(item.jzjhmxId);
                }
            }

            //去 领域服务层 持续化 数据
            _bookkeepInHosDmnService.SaveAccountingPlan(jzjhEntity, jhmxEntityList, updateToStopJzjhxmIdList);
        }

        /// <summary>
        /// 待执行记账计划 查询
        /// </summary>
        /// <param name="zyhList"></param>
        /// <param name="zxrq"></param>
        public IList<HospAccountingPlanWaitingExeVO> WaitingAccountingPlanQuery(IList<string> zyhList, string from = null)
        {
            if (zyhList == null || zyhList.Count == 0)
            {
                return new List<HospAccountingPlanWaitingExeVO>();
            }
            var zyhStr = "";
            zyhList.ToList().ForEach(p =>
            {
                zyhStr += p + ",";
            });
            zyhStr = zyhStr.Trim(',');
            if (string.IsNullOrWhiteSpace(zyhStr))
            {
                return new List<HospAccountingPlanWaitingExeVO>();
            }
            var isRehabDoctor = _sysUserDmnService.CheckStaffIsBelongDuty(this.UserIdentity.StaffId, "RehabDoctor");
            if (from != "mz")
                return _bookkeepInHosDmnService.WaitingAccountingPlanQuery(OrganizeId, zyhStr, this.UserIdentity.rygh,
                    UserIdentity.DepartmentCode, isRehabDoctor);
            var retdata = _bookkeepInHosDmnService.WaitingOutAccountingPlanQuery(OrganizeId, zyhStr, this.UserIdentity.rygh, this.UserIdentity.DepartmentCode, isRehabDoctor);
            return retdata;
        }

        /// <summary>
        /// 执行记账计划，生成计费
        /// </summary>
        /// <param name="jzjhmxIdList">jzjhmxId列表</param>
        public void ExecuteAccountingPlan(IList<zxGirdDto> zxItem, DateTime? zxrq, string from = null)
        {
            if (zxItem == null || zxItem.Count() == 0)
            {
                throw new FailedException("缺少记账计划参数");
            }
            if (!zxrq.HasValue)
            {
                throw new FailedException("未指定执行日期");
            }
            if (from != null && from == "mz")
            {
                _bookkeepInHosDmnService.ExecuteOutAccountingPlan(this.OrganizeId, zxItem, this.UserIdentity.UserCode, this.UserIdentity.DepartmentCode, zxrq.Value);
                //更新需要停止的记账计划
                zxItem.ToList().ForEach(p =>
                {
                    if (_bookkeepInHosDmnService.StopOutpatientjzjh(p.jzjhmxId, OrganizeId))
                    {
                        _outPatientDmnService.overAccountingPlan(p.jzjhmxId, OrganizeId, UserIdentity.UserCode);
                    }
                });
            }
            else
            {
                Dictionary<string, string> jfbStr = new Dictionary<string, string>();
                _bookkeepInHosDmnService.ExecuteAccountingPlan(this.OrganizeId, zxItem, this.UserIdentity.UserCode, this.UserIdentity.DepartmentCode, zxrq.Value, out jfbStr);
                //刷一下 计划的已执行次数、执行状态
                _bookkeepInHosDmnService.updateHospitaljzjh(OrganizeId, zxItem.Select(p => p.jzjhmxId).ToList());
                //已执行的康复项目同步到秦皇岛中间库 秦皇岛三期功能 jfbStr 才有值，skd_SyncInpatientToInterfaceBasic 调用此存储过程前有判空，GRS不会调用。
                //_bookkeepInHosDmnService.SyncInpatientToInterfaceBasic(jfbStr, OrganizeId, zxrq, UserIdentity.rygh);
            }
        }

        /// <summary>
        /// 停止记账计划
        /// </summary>
        /// <param name="jzjhmxIdList">jzjhmxId列表</param>
        public void StopAccountingPlan(string jzjhmxIdStr, DateTime stopDate)
        {
            var mxIdList = jzjhmxIdStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (mxIdList == null || mxIdList.Length == 0)
            {
                throw new FailedException("缺少记账计划参数");
            }
            _bookkeepInHosDmnService.StopAccountingPlan(mxIdList.ToList(), stopDate, this.UserIdentity.DepartmentCode, this.UserIdentity.UserCode);
        }

        /// <summary>
        /// 取消预停止
        /// </summary>
        /// <param name="jzjhmxIdStr"></param>
        public void CancelPreStopAccountingPlan(string jzjhmxIdStr)
        {
            var mxIdList = jzjhmxIdStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (mxIdList == null || mxIdList.Length == 0)
            {
                throw new FailedException("缺少记账计划参数");
            }
            _bookkeepInHosDmnService.CancelPreStopAccountingPlan(mxIdList.ToList(), this.UserIdentity.DepartmentCode, this.UserIdentity.UserCode);
        }


        #region 住院记账

        /// <summary>
        /// 根据住院号 获取 病人 记账 信息
        /// </summary>
        /// <param name="zyh"></param>
        public InpatientHosAccountingPatStatusDetailDto GetInpatientAccountingStatusDetail(string zyh)
        {
            //获取住院记账病人信息（基本信息、科室等）
            var patInfo = getHospAccountingPatInfo(zyh);

            var resultDto = new InpatientHosAccountingPatStatusDetailDto()
            {
                patInfo = patInfo,
            };

            return resultDto;
        }

        #endregion

        #region private methods

        /// <summary>
        /// 住院记账 获取病人基本信息（基本信息、科室等） 在院状态验证
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        private HospPatientInfoVO getHospAccountingPatInfo(string zyh)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }
            var patinfoList = _hosPatAccDmnService.GetHospPatientInfo(zyh, this.OrganizeId);
            if (patinfoList == null || patinfoList.Count == 0)
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (patinfoList.Count > 1)
            {
                throw new FailedCodeException("HOSP_MATCHED_ERROR_MULTI_MATCHED");
            }
            var patinfo = patinfoList.First();

            if (patinfo.zybz == ((int)EnumZYBZ.Wry).ToString())
            {
                throw new FailedCodeException("HOSP_CURRENTSTATUS_WRY_CANNOT_DO_OUTHOSPITAL_SETTLEMENT");
            }
            else if (patinfo.zybz == ((int)EnumZYBZ.Ycy).ToString())
            {
                throw new FailedCodeException("HOSP_CURRENTSTATUS_YCY_CANNOT_DO_OUTHOSPITAL_SETTLEMENT");
            }
            else if (patinfo.patid == 0)
            {
                throw new FailedCodeException("HOSP_SYSPATIENT_BASICINFO_IS_NOT_EXIST");
            }
            else if (string.IsNullOrWhiteSpace(patinfo.ksmc))
            {
                throw new FailedCodeException("HOSP_ERROR_PATIENT_DEPT_IS_NO_FOUND");
            }
            return patinfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ysList"></param>
        private void getYsKsInfo(IList<InpatientAccountingPlanItemDoctorDto> ysList
            , ref string ys, ref string ysmc, ref string ks, ref string ksmc)
        {
            ys = ysmc = ks = ksmc = "";
            foreach (var ysItem in ysList)
            {
                if (string.IsNullOrWhiteSpace(ysItem.gh)
                    || string.IsNullOrWhiteSpace(ysItem.Name)
                    || string.IsNullOrWhiteSpace(ysItem.ks)
                    || string.IsNullOrWhiteSpace(ysItem.ksmc))
                {
                    throw new FailedException("治疗师不明确");
                }
                ys += ysItem.gh + ",";
                ysmc += ysItem.Name + ",";
                ks += ysItem.ks + ",";
                ksmc += ysItem.ksmc + ",";
            }
            ys = ys.Trim(',');
            ysmc = ysmc.Trim(',');
            ks = ks.Trim(',');
            ksmc = ksmc.Trim(',');
        }

        #endregion

        #endregion

    }
}
