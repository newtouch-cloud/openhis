using System;
using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Repository.SystemManage;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Redis.Cache;
using Newtouch.HIS.Application.Implementation.OutpatientManage;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Proxy;
using Newtouch.HIS.Proxy.Common;
using Newtouch.HIS.Proxy.guian;
using Newtouch.HIS.Proxy.guian.DO;
using Newtouch.HIS.Proxy.guian.DTO;
using Newtouch.HIS.Proxy.guian.DTO.S02;
using Newtouch.HIS.Proxy.guian.DTO.S18;
using Newtouch.HIS.Proxy.guian.DTO.S19;
using Newtouch.HIS.Proxy.guian.DTO.S21;
using Newtouch.HIS.Proxy.guian.DTO.S22;
using Newtouch.HIS.Proxy.guian.DTO.S23;
using Newtouch.HIS.Proxy.guian.DTO.S24;
using Newtouch.HIS.Proxy.guian.DTO.S25;
using Newtouch.HIS.Proxy.guian.DTO.S26;
using Newtouch.HIS.Proxy.GuiAnXnhReference;
using Newtouch.HIS.Proxy.Log;
using Newtouch.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 贵安门诊新农合
    /// </summary>
    public class GuiAnOutpatientXnhApp : AppBase, IGuiAnOutpatientXnhApp
    {
        private readonly IOutPatChargeDmnService _outPatChargeDmnService;
        private readonly ITTCataloguesComparisonDmnService _ttCataloguesComparisonDmnService;
        private readonly IOutpatientRegistRepo _outpatientRegistRepo;
        private readonly IOutpatientXnhOutpIdRelRepo _outpatientXnhOutpIdRel;
        private readonly IOutpatientXnhSettlementResultRepo _outpatientXnhSettlementResultRepo;
        private readonly IOutPatChargeDmnService _outChargeDmnService;

        /// <summary>
        /// 模拟结算全流程
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="s24"></param>
        /// <returns></returns>
        public string SimulationSettWholeProcess(string mzh, string userCode, string organizeId, out S24ResponseDTO s24)
        {
            s24 = null;
            DateTime zlkssj;

            var s21Details = AssembleS21Details(mzh, organizeId);
            if (s21Details == null || s21Details.Count == 0) return "未找到需要上传的明细";

            #region 生成补偿序号

            var uploadResult = OutpatientUpload(mzh, organizeId, userCode, out zlkssj);
            if (!string.IsNullOrWhiteSpace(uploadResult)) return uploadResult;
            #endregion

            #region 模拟结算

            Response<S24ResponseDTO> s24Response;
            var simulationSettResult = SimulationSett(mzh, zlkssj, s21Details, organizeId, out s24Response);
            s24 = s24Response == null ? null : s24Response.data;
            #endregion

            #region 模拟结算失败则门诊回退

            if (string.IsNullOrWhiteSpace(simulationSettResult)) return "";
            var returnResult = OutpatientReturn(mzh, organizeId, userCode);
            return string.IsNullOrWhiteSpace(returnResult)
                ? simulationSettResult
                : string.Format("{0} \n {1}", simulationSettResult, returnResult);

            #endregion
        }

        /// <summary>
        /// 模拟结算，单功能
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="zlkssj"></param>
        /// <param name="organizeId"></param>
        /// <param name="s24Response"></param>
        /// <returns></returns>
        public string SimulationSett(string mzh, DateTime zlkssj, string organizeId, out Response<S24ResponseDTO> s24Response)
        {
            s24Response = null;
            var s21Details = AssembleS21Details(mzh, organizeId);
            if (s21Details == null || s21Details.Count == 0) return "未找到需要上传的明细";
            return SimulationSett(mzh, zlkssj, s21Details, organizeId, out s24Response);
        }

        /// <summary>
        /// 模拟结算，单功能
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="zlkssj"></param>
        /// <param name="s21Details">门诊费用上传明细</param>
        /// <param name="organizeId"></param>
        /// <param name="s24Response"></param>
        /// <returns></returns>
        public string SimulationSett(string mzh, DateTime zlkssj, List<S21Detail> s21Details, string organizeId, out Response<S24ResponseDTO> s24Response)
        {
            try
            {
                s24Response = null;
                var settProcessDo = new SimulationSettlementOverallProcessDo
                {
                    OrganizeId = organizeId,
                    outpId = OutpIdQuery(mzh, organizeId),
                    S22Param = new S22Param
                    {
                        startDate = zlkssj.ToString("yyyy-MM-dd"),
                        endDate = DateTime.Now.ToString("yyyy-MM-dd")
                    },
                    S21Details = s21Details
                };
                var processResult = new SimulationSettlementOverallProcess(settProcessDo).Process();
                if (!processResult.IsSucceed)
                {
                    throw new Exception(processResult.ResultMsg);
                }
                s24Response = processResult.Data;
                return "";
            }
            catch (Exception e)
            {
                LogCore.Error("GuiAnOutpatientXnhApp.SimulationSett error", e);
                s24Response = null;
                return e.Message;
            }
        }

        /// <summary>
        /// 回退先前上传的明细
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public string DetailedUploadRetreat(string startDate, string endDate, string outpId, string organizeId)
        {
            var s22RequestDto = new S22RequestDTO
            {
                startDate = startDate,
                endDate = endDate,
                outpId = outpId
            };
            var s22Response = OutpatientProxy.GetInstance(organizeId).S22(s22RequestDto);
            if (s22Response.state)
            {
                if (s22Response.data == null || s22Response.data.Count <= 0) return "";
                var s23Request = new S23RequestDTO
                {
                    outpId = outpId,
                    list = new List<string>()
                };
                s22Response.data.ForEach(p => { s23Request.list.Add(p.detailCode); });
                var s23Response = OutpatientProxy.GetInstance(organizeId).S23(s23Request);
                if (!s23Response.state)
                {
                    return string.Format("新农合医保门诊费用明细退单接口返回异常！异常提示：{0} ;", s23Response.message);
                }
            }
            else
            {
                return string.Format("新农合医保上传明细查询接口返回异常！异常提示：{0} ;", s22Response.message);
            }

            return "";
        }

        /// <summary>
        /// 查询门诊补偿序号
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public string OutpIdQuery(string mzh, string organizeId)
        {
            var mzgh = _outpatientRegistRepo.SelectData(mzh, organizeId);
            if (mzgh == null || mzgh.ghnm <= 0) throw new Exception("未查到有效挂号信息");
            if (string.IsNullOrWhiteSpace(mzgh.outpId)) throw new Exception("门补偿序号未落地，请检查是否有门诊上传操作");
            return mzgh.outpId;
        }

        /// <summary>
        /// 组装新农合医保上传明细 
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        private List<S21Detail> AssembleS21Details(string mzh, string organizeId)
        {
            var data = _outPatChargeDmnService.GetAllUnSettedListListByMzh(mzh, organizeId);
            if (data == null || data.Count <= 0) return null;

            return data.Where(p => p.ybbz == "1" && p.xnhybdm != "")
                .Select(mx => new S21Detail
                {
                    detailName = mx.sfxmmc,
                    detailCode = mx.xnhybdm,
                    hisDetailCode = mx.mxId.ToString(),
                    detailHosCode = mx.xnhybdm,
                    typeCode = mx.sfdlCode,
                    num = mx.sl,
                    price = mx.dj,
                    totalCost = mx.zje,
                    date = mx.klsj.ToString("yyyy-MM-dd"),
                    unit = mx.dw,
                    standard = mx.gg,
                    formulations = ""
                })
                .ToList();
        }

        /// <summary>
        /// 门诊上传
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode">操作员账号</param>
        /// <param name="zlkssj"></param>
        /// <returns></returns>
        public string OutpatientUpload(string mzh, string organizeId, string userCode, out DateTime zlkssj)
        {
            zlkssj = Constants.MinDate;

            var brxx = _outPatChargeDmnService.SelectSysPatientBasicInfoEntities(mzh, organizeId).FirstOrDefault();
           // if (brxx == null || string.IsNullOrWhiteSpace(brxx.xnhgrbm)) return "新农合个人编码丢失，请联系管理员";
            var apiResp = SiteCISAPIHelper.PatientTreatmentInfoQuery(mzh, organizeId);
            var jzxx = new PatientTreatmentInfoQueryResponseDTO();
            if (apiResp != null && apiResp.data != null)
            {
                var tmpJzxxs = apiResp.data.ToString().ToObject<List<PatientTreatmentInfoQueryResponseDTO>>();
                jzxx = tmpJzxxs.FirstOrDefault();
            }
            else
            {
                return "获取就诊信息失败";
            }

            if (jzxx == null || string.IsNullOrWhiteSpace(jzxx.jzId)) return "未查到有效的就诊信息";
            zlkssj = jzxx.zlkssj;

            #region 如若当前挂号已落地outpId则不需上传, 若已结算或退费，则清空原来outpId

            var mzgh = _outpatientRegistRepo.SelectData(mzh, organizeId);
            if (mzgh == null || string.IsNullOrWhiteSpace(mzgh.mzh)) return string.Format("根据门诊号【{0}】未找到挂号信息", mzh);
            if (!string.IsNullOrWhiteSpace(mzgh.outpId))
            {
                var settResults = _outpatientXnhSettlementResultRepo.SelectData(mzgh.outpId, organizeId);
                if (settResults != null && settResults.Count > 0)
                {
                    _outpatientRegistRepo.RecordOutpId(mzh, "", userCode, organizeId);
                }
            }
            #endregion

            #region 若当前患者存在历史未结束结算，则回退历史门诊上传

            var outpIds = _outPatChargeDmnService.SelectNotSettedXnhOutpId(brxx.patid, organizeId);
            if (outpIds != null && outpIds.Count > 0)
            {
                foreach (var outpId in outpIds)
                {
                    var returnResult = OutpatientReturn(mzh, outpId, organizeId, userCode);
                    if (string.IsNullOrWhiteSpace(returnResult)) continue;
                    return returnResult;
                }
            }
            #endregion

            var s18RequestDto = new S18RequestDTO
            {
                //memberId = brxx.xnhgrbm,
                inpatientDate = jzxx.zlkssj.ToString("yyyy-MM-dd"),
                inpatientDepartments = _ttCataloguesComparisonDmnService.GetTTItem(organizeId, "ks", jzxx.jzks, "guianxinnonghe").First,
                treatingPhysician = jzxx.jzys,
                diseaseCode = jzxx.xyzdCode,
                isIncrease = "1",
                initialDiagnosis = jzxx.xyzdmc,
                isInfusion = "0",
                uploadType = "0"
            };
            var s18Response = OutpatientProxy.GetInstance(organizeId).S18(s18RequestDto);
            if (!s18Response.state)
            {
                return string.Format("新农合医保接口返回异常！异常提示：{0} ;", s18Response.message);
            }
            SaveNewOutpId(mzgh.ghnm, userCode, mzh, organizeId, s18Response.data.outpId);
            return "";
        }

        /// <summary>
        /// 保存新门诊补偿序号
        /// </summary>
        /// <param name="ghnm"></param>
        /// <param name="userCode"></param>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="outpId"></param>
        /// <returns></returns>
        public void SaveNewOutpId(int ghnm, string userCode, string mzh, string organizeId, string outpId)
        {
            var ret = _outpatientRegistRepo.RecordOutpId(mzh, outpId, userCode, organizeId);
            if (ret == 0) throw new FailedException("保存门诊补偿序号到挂号表失败");
            var outpIdRel = new OutpatientXnhOutpIdRelEntity
            {
                ghnm = ghnm,
                Id = Guid.NewGuid().ToString(),
                CreatorCode = userCode,
                CreateTime = DateTime.Now,
                LastModifyTime = null,
                LastModifierCode = "",
                mzh = mzh,
                OrganizeId = organizeId,
                outpId = outpId,
                processingStatus = 0,
                zt = "1"
            };
            ret = _outpatientXnhOutpIdRel.Insert(outpIdRel);
            if (ret == 0) throw new FailedException("保存门诊补偿序号到关联关系表失败");
        }

        /// <summary>
        /// 修改门诊补偿序号
        /// </summary>
        /// <param name="ghnm"></param>
        /// <param name="userCode"></param>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="newOutpId"></param>
        /// <returns></returns>
        public void UpdateMzghOutpId(int ghnm, string userCode, string mzh, string organizeId, string newOutpId)
        {
            _outpatientXnhOutpIdRel.UpdateZtDisable(mzh, organizeId, userCode);
            SaveNewOutpId(ghnm, userCode, mzh, organizeId, newOutpId);
        }

        /// <summary>
        /// 门诊上传回退，验证失败后直接抛出异常
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string OutpatientUploadReturn(string mzh, string organizeId, string userCode)
        {
            var mzgh = _outpatientRegistRepo.SelectData(mzh, organizeId);
            if (mzgh == null || string.IsNullOrWhiteSpace(mzgh.mzh)) return string.Format("根据门诊号【{0}】未找到挂号信息", mzh);
            if (string.IsNullOrWhiteSpace(mzgh.outpId)) return "";
            var s19Request = new S19RequestDTO
            {
                outpId = mzgh.outpId
            };
            var s19response = OutpatientProxy.GetInstance(organizeId).S19(s19Request);
            return s19response.state ? "" : s19response.message;
        }

        /// <summary>
        /// 门诊回退，可在取消挂号中使用（自动捕获异常）
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns>失败原因</returns>
        public string OutpatientReturn(string mzh, string organizeId, string userCode)
        {
            var outpId = "";
            try
            {
                outpId = OutpIdQuery(mzh, organizeId);
            }
            catch (Exception e)
            {
                LogCore.Error("GuiAnOutpatientXnhApp.OutpatientReturn error", e);
            }

            return OutpatientReturn(mzh, outpId, organizeId, userCode);
        }

        /// <summary>
        /// 门诊回退，可在取消挂号中使用（自动捕获异常）
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="outpId">门诊补偿序号</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns>失败原因</returns>
        public string OutpatientReturn(string mzh, string outpId, string organizeId, string userCode)
        {
            if (string.IsNullOrWhiteSpace(outpId)) return "";
            var s19Request = new S19RequestDTO
            {
                outpId = outpId
            };
            var s19Response = OutpatientProxy.GetInstance(organizeId).S19(s19Request);
            if (!s19Response.state && s19Response.message.IndexOf("门诊补偿序号不存在", StringComparison.Ordinal) < 0) return s19Response.message;
            _outpatientRegistRepo.RecordOutpId(mzh, "", userCode, organizeId);
            _outpatientXnhOutpIdRel.UpdateProcessingStatus(mzh, 4, outpId, organizeId, userCode);
            return "";
        }

        /// <summary>
        /// 取消结算，门诊红冲
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string CancelSett(string mzh, string organizeId, string userCode)
        {
            string outpId;
            try
            {
                outpId = OutpIdQuery(mzh, organizeId);
            }
            catch (Exception e)
            {
                LogCore.Error("GuiAnOutpatientXnhApp.OutpatientReturn error", e);
                return e.Message + (e.InnerException == null ? "" : e.InnerException.Message);
            }

            return CancelSett(mzh, outpId, organizeId, userCode);
        }

        /// <summary>
        /// 取消结算，门诊红冲
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string CancelSett(string mzh, string outpId, string organizeId, string userCode)
        {
            if (string.IsNullOrWhiteSpace(outpId)) return "请传入门诊补偿序号";
            var s26Request = new S26RequestDTO
            {
                outpId = outpId
            };
            var s26Response = OutpatientProxy.GetInstance(organizeId).S26(s26Request);
            if (!s26Response.state) return s26Response.message;
            _outpatientXnhOutpIdRel.UpdateProcessingStatus(mzh, 3, outpId, organizeId, userCode);
            _outpatientRegistRepo.RecordOutpId(mzh, "", userCode, organizeId);
            _outpatientXnhSettlementResultRepo.UpdateJszt(2, outpId, userCode, organizeId);
            return "";
        }

        /// <summary>
        /// 新农合门诊结算
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="s25"></param>
        /// <param name="outpId"></param>
        /// <returns></returns>
        public string Sett(string mzh, string organizeId, string userCode, out S25ResponseDTO s25, out string outpId)
        {
            s25 = new S25ResponseDTO();
            outpId = "";
            try
            {
                outpId = OutpIdQuery(mzh, organizeId);
            }
            catch (Exception e)
            {
                LogCore.Error("GuiAnOutpatientXnhApp.Sett error", e);
                return e.Message + (e.InnerException == null ? "" : " \n " + e.InnerException.Message);
            }

            return Sett(mzh, outpId, organizeId, userCode, out s25);
        }

        /// <summary>
        /// 新农合门诊结算
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="s25"></param>
        /// <returns></returns>
        public string Sett(string mzh, string outpId, string organizeId, string userCode, out S25ResponseDTO s25)
        {
            s25 = new S25ResponseDTO();
            if (string.IsNullOrWhiteSpace(outpId)) return "门诊补偿序号不能为空！";
            var settResult = _outpatientXnhSettlementResultRepo.SelectData(outpId, organizeId).FirstOrDefault();
            if (settResult != null && settResult.jszt == 2) return "该结算已红冲";
            var s25Request = new S25RequestDTO
            {
                outpId = outpId
            };
            var s25Response = OutpatientProxy.GetInstance(organizeId).S25(s25Request);
            s25 = s25Response.data;
            if (!s25Response.state) return s25Response.message;
            _outpatientXnhOutpIdRel.UpdateProcessingStatus(mzh, 2, outpId, organizeId, userCode);
            return "";
        }

        /// <summary>
        /// 组装退费后要重新生成的结算明细
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="tjsxmDict"></param>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="outpId"></param>
        /// <returns></returns>
        public TFS21RequestDTO AssembleS21RequestDto(string jsnm, Dictionary<string, decimal> tjsxmDict, string mzh, string organizeId, string outpId = "")
        {
            if (string.IsNullOrWhiteSpace(outpId))
            {
                outpId = OutpIdQuery(mzh, organizeId);
            }
            var result = new TFS21RequestDTO
            {
                S21RequestDto = new S21RequestDTO
                {
                    outpId = outpId,
                    list = new List<Proxy.guian.DTO.S21.detail>()
                }
            };
            var jsmxList = _outChargeDmnService.GetAllUnSettedListListByJsnm(jsnm, organizeId);
            if (jsmxList == null || jsmxList.Count <= 0) return result;
            var allTTmllb = _ttCataloguesComparisonDmnService.GetTTItem(organizeId, "mllb", "guianxinnonghe");
            if (allTTmllb == null || allTTmllb.Count <= 0) throw new FailedException("缺少类别对照");

            foreach (var jsmx in jsmxList)
            {
                var tmpTTmllb = allTTmllb.FirstOrDefault(i => i.Code == jsmx.sfdlCode);
                if (tmpTTmllb == null) throw new Exception(string.Format("未找到{0}的项目类别对照", jsmx.sfdlCode));
                decimal num = 0;
                foreach (var itemtf in tjsxmDict)
                {
                    if (itemtf.Key != jsmx.jsmxnm.ToString()) continue;
                    num = Convert.ToDecimal(jsmx.sl - itemtf.Value);//退后剩余数量
                    result.tkzje += (itemtf.Value * Convert.ToDecimal(jsmx.dj));//退总金额
                }

                var je = Convert.ToDecimal(num * jsmx.dj);//剩余数量*单价=剩余总额
                if (!string.IsNullOrWhiteSpace(jsmx.ybdm) && jsmx.ybbz == "1")
                {
                    if (num <= 0 || je <= 0) continue;
                    result.S21RequestDto.list.Add(new Proxy.guian.DTO.S21.detail
                    {
                        detailCode = jsmx.xnhybdm,
                        detailName = jsmx.sfxmmc,
                        detailHosCode = jsmx.xnhybdm,
                        typeCode = tmpTTmllb.TTCode,
                        num = num,
                        price = jsmx.dj,
                        totalCost = je,//剩余数量*单价=总额
                        date = jsmx.klsj.ToString("yyyy-MM-dd"),
                    });
                    result.ybzje += je;
                }
                else
                {
                    result.zlzje += je;
                }
            }

            return result;
        }

        /// <summary>
        /// 退费
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="tjsxmDict"></param>
        /// <param name="mzh"></param>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="tfRequest"></param>
        /// <returns></returns>
        public string XnhTuiFeiProcess(string jsnm, Dictionary<string, decimal> tjsxmDict, string mzh, string outpId, string organizeId, string userCode, out TFS21RequestDTO tfRequest)
        {
            tfRequest = new TFS21RequestDTO();

            //新农合退费
            var s26Response = CancelSett(mzh, outpId, OrganizeId, UserIdentity.UserCode);
            if (!string.IsNullOrWhiteSpace(s26Response) && s26Response.IndexOf("不是结算与暂存状态", StringComparison.Ordinal) < 0) return s26Response;
            try
            {
                //门诊上传
                DateTime zlkssj;
                var uploadResult = OutpatientUpload(mzh, organizeId, userCode, out zlkssj);
                if (!string.IsNullOrWhiteSpace(uploadResult)) return uploadResult;

                // 组装未退费明细
                tfRequest = AssembleS21RequestDto(jsnm, tjsxmDict, mzh, OrganizeId);
                if (tfRequest.S21RequestDto.list.Count == 0)
                {
                    //全退，不需重新上传
                    OutpatientReturn(mzh, tfRequest.S21RequestDto.outpId, organizeId, userCode);
                    tfRequest.S21RequestDto.outpId = "";
                    return "";
                }
                //未退明细上模拟结算
                var tuiFeiSimulationSettResult = TuiFeiSimulationSett(tfRequest, organizeId, zlkssj);
                return !string.IsNullOrWhiteSpace(tuiFeiSimulationSettResult) ? tuiFeiSimulationSettResult : "";
            }
            catch (Exception e)
            {
                _outpatientXnhSettlementResultRepo.UpdateJszt(1, outpId, userCode, organizeId);//还原
                LogCore.Error("XnhTuiFeiProcess error", e);
                throw;
            }
        }

        /// <summary>
        /// 退费重新模拟上传未退费明细
        /// </summary>
        /// <param name="tfxx"></param>
        /// <param name="organizeId"></param>
        /// <param name="zlkssj"></param>
        /// <returns></returns>
        public string TuiFeiSimulationSett(TFS21RequestDTO tfxx, string organizeId, DateTime zlkssj)
        {
            if (tfxx == null || tfxx.S21RequestDto == null) return "未退费信息不能为空";

            #region 回退先前上传的明细

            var s22RequestDto = new S22RequestDTO
            {
                startDate = zlkssj.ToString("yyyy-MM-dd"),
                endDate = DateTime.Now.ToString("yyyy-MM-dd"),
                outpId = tfxx.S21RequestDto.outpId
            };
            var s22Response = OutpatientProxy.GetInstance(organizeId).S22(s22RequestDto);
            if (s22Response.state)
            {
                if (s22Response.data != null && s22Response.data.Count > 0)
                {
                    var s23Request = new S23RequestDTO
                    {
                        outpId = tfxx.S21RequestDto.outpId,
                        list = new List<string>()
                    };
                    s22Response.data.ForEach(p => { s23Request.list.Add(p.detailNo); });
                    var s23Response = OutpatientProxy.GetInstance(organizeId).S23(s23Request);
                    if (!s23Response.state)
                    {
                        return string.Format("新农合医保门诊费用明细退单接口返回异常！异常提示：{0}  ", s23Response.message);
                    }
                }
            }
            else
            {
                return string.Format("新农合医保上传明细查询接口返回异常！异常提示：{0}  ", s22Response.message);
            }
            #endregion

            #region 上传明细
            var s21Response = OutpatientProxy.GetInstance(organizeId).S21(tfxx.S21RequestDto);
            if (!s21Response.state)
            {
                return string.Format("新农合医保门诊费用上传接口返回异常！异常提示：{0}  ", s21Response.message);
            }
            #endregion

            #region 模拟交易
            var s24RequestDto = new S24RequestDTO
            {
                outpId = tfxx.S21RequestDto.outpId
            };
            var s24Response = OutpatientProxy.GetInstance(organizeId).S24(s24RequestDto);
            return !s24Response.state ? string.Format("新农合医保门诊模拟结算接口返回异常！异常提示：{0}  ", s24Response.message) : "";
            #endregion
        }

        /// <summary>
        /// 新农合医保请求
        /// </summary>
        /// <param name="itn">接口名称  例：S02</param>
        /// <param name="body"></param>
        /// <param name="organizeId"></param>
        /// <param name="response">返回报文</param>
        /// <returns>结果</returns>
        public string XnhInterfaceSend(string itn, string body, string organizeId, out string response)
        {
            response = "";
            if (string.IsNullOrWhiteSpace(itn)) return "Intferface不能为空";
            if (string.IsNullOrWhiteSpace(body)) return "BodyXml不能为空";
            try
            {
                if ("S02".Equals(itn.ToUpper()))
                {
                    var requestHead = new RequestHeaderBase
                    {
                        operCode = "S02",
                        rsa = MD5Helper.GetMd5Hash(body)
                    };
                    var inHeadXml = XMLSerializer.Serialize(requestHead);
                    response = new HisServiceClient().request(inHeadXml, body);
                }
                else
                {
                    var sysConfigRepo = new SysConfigRepo(new DefaultDatabaseFactory(), new SERedisCache());
                    var userInfo = sysConfigRepo.GetValueByCode("guian_xnh_userInfo", OrganizeId);
                    var s02Body = new S02RequestDTO();
                    if (!string.IsNullOrWhiteSpace(userInfo))
                    {
                        s02Body = userInfo.ToObject<S02RequestDTO>();
                    }

                    s02Body = s02Body ?? new S02RequestDTO();
                    var userName = s02Body.userName;
                    var passWord = s02Body.passWord;
                    var s02response = CommonProxy.GetInstance(OrganizeId).S02(userName, passWord);
                    var billCode = s02response.state ? s02response.data.billCode : "";

                    var headObj = new RequestHeader
                    {
                        operCode = itn,
                        rsa = MD5Helper.GetMd5Hash(body),
                        billCode = billCode
                    };
                    var inHeadXml = XMLSerializer.Serialize(headObj);
                    response = new HisServiceClient().request(inHeadXml, body);
                }

                return "";
            }
            catch (Exception e)
            {
                LogCore.Error("GuiAnWebServiceFactory call error", e);
                throw;
            }
        }
    }
}