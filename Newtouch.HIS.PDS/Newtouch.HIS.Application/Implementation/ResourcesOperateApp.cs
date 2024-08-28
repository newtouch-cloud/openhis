using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Implementation.Process;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy;
using Newtouch.HIS.DomainServices;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Sett.Request;
using Newtouch.HIS.Sett.Request.Patient;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.Common;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.ResourcesOperate;
using Newtouch.PDS.Requset.Zyypyz;
using Newtouch.Tools;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 取消预定
    /// </summary>
    public class ResourcesOperateApp : AppBase, IResourcesOperateApp
    {
        private readonly IOutpatientPrescriptionDetailBatchNumberRepo _mzCfphRepo;
        private readonly IMzCfRepo _mzCfRepo;
        private readonly IDispensingDmnService _dispensingDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IPyDmnService _pyDmnService;

        #region 门诊

        /// <summary>
        /// 门诊预定
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActResult OutpatientBook(OutpatientBookRequestDTO request)
        {
            var result = new OutpatientBookAppV2(request).Process();
            if (!result.IsSucceed)
            {
                throw new FailedException("", result.ResultMsg);
            }

            return result;
        }

        /// <summary>
        /// 门诊预定修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActResult OutpatientBookModify(OutpatientBookModifyRequestDTO request)
        {
            var result = new ActResult();
            if (request == null || request.Items == null || request.Items.Count == 0)
            {
                result.IsSucceed = false;
                result.ResultMsg = "请传入要修改的处方信息";
                return result;
            }
            request.Items.Select(p => p.Cfh).Distinct().ToList().ForEach(q =>
            {
                var rpInfo = _mzCfRepo.SelectRpList(q, request.OrganizeId);
                if (rpInfo == null || rpInfo.Count == 0)
                {
                    InserNewRp(request, q, result);
                    return;
                }

                if (rpInfo.Exists(r => !((int)EnumFybz.Yp).ToString().Equals(r.fybz)))
                {
                    result.IsSucceed = false;
                    result.ResultMsg += string.Format("处方【{0}】修改失败，被修改处方只能是已排药处方；", q);
                    return;
                }
                string cancelResult;
                try
                {
                    var cancelBookRequestDto = new OutpatientCancelAllBookRequestDTO
                    {
                        OrganizeId = request.OrganizeId,
                        AppId = request.AppId,
                        cfh = q,
                        CreatorCode = request.CreatorCode,
                        fyyf = "",
                        ResponseColumns = request.ResponseColumns,
                        Token = request.Token,
                        Timestamp = request.Timestamp,
                        TokenType = request.TokenType
                    };
                    cancelResult = OutpatientCancelBook(cancelBookRequestDto);
                }
                catch (FailedException e)
                {
                    cancelResult = e.Msg;
                }
                catch (Exception e)
                {
                    cancelResult = e.Message;
                }
                if (!string.IsNullOrWhiteSpace(cancelResult))
                {
                    result.IsSucceed = false;
                    result.ResultMsg += string.Format("处方【{0}】取消原处方失败。{1}；", q, cancelResult);
                    return;
                }

                InserNewRp(request, q, result);
            });
            if (result.IsSucceed) return result;
            throw new FailedException("", result.ResultMsg);
        }

        /// <summary>
        /// 新增处方 （修改是发现没有该处方）
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cfh"></param>
        /// <param name="result"></param>
        private void InserNewRp(OutpatientBookModifyRequestDTO request, string cfh, ActResult result)
        {
            try
            {
                var newbookItems = request.Items.FindAll(p => p.Cfh == cfh);
                var newbookRequestDto = new OutpatientBookRequestDTO
                {
                    AppId = request.AppId,
                    CreatorCode = request.CreatorCode,
                    Items = newbookItems,
                    OrganizeId = request.OrganizeId,
                    ResponseColumns = request.ResponseColumns,
                    Timestamp = request.Timestamp,
                    Token = request.Token,
                    TokenType = request.TokenType
                };
                var newBookResult = OutpatientBook(newbookRequestDto);
                if (newBookResult.IsSucceed)
                {
                    return;
                }

                result.Data = newBookResult.Data;
                result.IsSucceed = false;
                result.ResultMsg += string.Format("处方【{0}】生成新处方失败,{1};", cfh, result.ResultMsg);
            }
            catch (FailedException ex)
            {
                var errorMsg = string.Format("处方【{0}】生成新处方失败; ", cfh) + ex.Msg;
                result.IsSucceed = false;
                result.ResultMsg = errorMsg;
            }
            catch (Exception ex)
            {
                var errorMsg = string.Format("处方【{0}】生成新处方失败,{1}; ", cfh, result.ResultMsg) + ex.Message + (ex.InnerException == null ? "" : ex.InnerException.Message);
                result.IsSucceed = false;
                result.ResultMsg = errorMsg;
            }
        }

        /// <summary>
        /// (未发药前)部分退药
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string OutpatientPartReturnBeforeDispensingMedicine(OutpatientPartReturnBeforeDispensingMedicineRequestDTO request)
        {
            foreach (var item in request.Items)
            {
                int rpInfo = _mzCfRepo.DeleteCf(item.cfh.ToString(), request.OrganizeId.ToString());
                if (rpInfo<=0)
                {
                    throw new FailedException("", "处方状态修改失败");
                }
            }
            return "";
        }

        /// <summary>
        /// 门诊取消预定
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string OutpatientCancelBook(OutpatientCancelPartBookRequestDTO request)
        {
            if (request == null || !request.Items.Any())
            {
                throw new FailedException("", "请传入要取消的明细");
            }
            var result = new StringBuilder();
            Parallel.ForEach(request.Items, p =>
            {
                var errorMsg = new DispensingDmnService(new DefaultDatabaseFactory()).OutPatientBookCancel(p.ypdm, p.sl * p.zhyz, p.yfbmCode, p.cfh, request.OrganizeId, request.CreatorCode);
                if (!string.IsNullOrWhiteSpace(errorMsg))
                {
                    result.Append(errorMsg + ";");
                }
            });
            if (!string.IsNullOrWhiteSpace(result.ToString()))
            {
                throw new FailedException("", result.ToString());
            }

            return "";
        }

        /// <summary>
        /// 门诊取消预定 适用于非结算处方
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string OutpatientCancelBook(OutpatientCancelAllBookRequestDTO request)
        {
            var cfxx = _mzCfRepo.SelectRpList(request.cfh, request.OrganizeId);
            if (cfxx == null || cfxx.Count == 0)
            {
                throw new FailedException("", "未找到指定处方");
            }

            if (cfxx.Exists(p => p.jsnm > 0))
            {
                throw new FailedException("", "处方已结算，不能取消预定");
            }

            var mxphList = _mzCfphRepo.GetList(request.cfh, cfxx[0].lyyf, request.OrganizeId, "0");
            var fybz = cfxx[0].fybz;
            Task updateRpZtTask;
            Task updateRpDetailZt;
            if (mxphList.Count == 0 || ((int)EnumFybz.Wp).ToString().Equals(fybz))
            {
                updateRpZtTask = UpdateRpZt(cfxx, request);
                updateRpDetailZt = UpdateRpDetailZt(request);
                updateRpZtTask.Wait();
                updateRpDetailZt.Wait();
                return "";
            }

            if (((int)EnumFybz.Yp).ToString().Equals(cfxx[0].fybz))
            {
                #region use multi threading
                //try
                //{
                //    Parallel.ForEach(mxphList, p =>
                //    {
                //        var errorMsg = new DispensingDmnService(new DefaultDatabaseFactory(), false).OutPatientBookCancel(p.yp, (int)p.sl, p.fyyf, p.cfh, request.OrganizeId, request.CreatorCode);
                //        if (!string.IsNullOrWhiteSpace(errorMsg))
                //        {
                //            result.Append(errorMsg + ";");
                //        }
                //        else
                //        {
                //            p.zt = "0";
                //            p.LastModifyTime = DateTime.Now;
                //            p.LastModifierCode = request.CreatorCode;
                //            new OutpatientPrescriptionDetailBatchNumberRepo(new DefaultDatabaseFactory()).Update(p);
                //        }
                //    });
                //}
                //catch (Exception e)
                //{
                //    result.Append(e.Message + (e.InnerException != null ? e.InnerException.Message : ""));
                //    throw;
                //}
                #endregion
                #region use single thread

                var result = _dispensingDmnService.OutPatientBookCancel(mxphList, request.OrganizeId, request.CreatorCode);
                #endregion

                if (!string.IsNullOrWhiteSpace(result))
                {
                    throw new FailedException("", result);
                }

                updateRpZtTask = UpdateRpZt(cfxx, request);
                updateRpDetailZt = UpdateRpDetailZt(request);
                updateRpZtTask.Wait();
                updateRpDetailZt.Wait();
            }
            else
            {
                throw new FailedException("", "取消预订只针对已排处方");
            }
            return "";
        }

        /// <summary>
        /// 修改处方状态
        /// </summary>
        /// <param name="rpList"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private Task UpdateRpZt(List<MzCfEntity> rpList, OutpatientCancelAllBookRequestDTO request)
        {
            return Task.Run(() =>
            {
                Thread.CurrentThread.Priority = ThreadPriority.Highest;
                var mzCfRepo = new MzCfRepo(new DefaultDatabaseFactory());
                //var rpInfo = mzCfRepo.IQueryable(p => p.cfh == request.cfh && p.fybz == ((int)EnumFybz.Yp).ToString() && p.zt == "1" && p.jsnm == 0 && p.OrganizeId == request.OrganizeId);
                var rpInfo = rpList.FindAll(p => p.fybz == ((int)EnumFybz.Yp).ToString() && p.jsnm == 0);
                if (rpInfo.Any())
                {
                    rpInfo.ToList().ForEach(p =>
                    {
                        p.zt = "0";
                        p.LastModifyTime = DateTime.Now;
                        p.LastModiFierCode = request.CreatorCode;
                        mzCfRepo.Update(p);
                    });
                }
            });
        }

        /// <summary>
        /// 修改处方状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private Task UpdateRpDetailZt(OutpatientCancelAllBookRequestDTO request)
        {
            return Task.Run(() =>
            {
                var mzCfmxRepo = new MzCfmxRepo(new DefaultDatabaseFactory());
                var mzCfmx = mzCfmxRepo.IQueryable(p => p.cfh == request.cfh && p.zt == "1" && p.OrganizeId == request.OrganizeId);
                if (mzCfmx.Any())
                {
                    mzCfmx.ToList().ForEach(p =>
                    {
                        p.zt = "0";
                        p.LastModifyTime = DateTime.Now;
                        p.LastModiFierCode = request.CreatorCode;
                        mzCfmxRepo.Update(p);
                    });
                }
            });
        }

        /// <summary>
        /// 门诊确定资源（commit）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string OutpatientCommit(OutpatientCommitRequestDTO request)
        {
            if (request == null)
            {
                throw new FailedException("", "请求报文不能为空");
            }

            if (request.Jsnm <= 0)
            {
                throw new FailedException("", "结算内码必填");
            }

            if (request.Sfsj <= Convert.ToDateTime("1971-01-01"))
            {
                throw new FailedException("", "收费时间不合法");
            }

            var rpInfo = _mzCfRepo.SelectRpList(request.Cfh, request.OrganizeId);
            if (rpInfo == null || rpInfo.Count == 0)
            {
                throw new FailedException("", "未找到指定处方信息");
            }

            if (rpInfo[0].fybz == ((int)EnumFybz.Wp).ToString())
            {
                //重新排药

                throw new NotImplementedException();
            }
            rpInfo[0].cfnm = request.Cfnm;
            rpInfo[0].Fph = request.Fph;
            rpInfo[0].jsnm = request.Jsnm;
            rpInfo[0].sfsj = request.Sfsj;
            rpInfo[0].LastModiFierCode = request.CreatorCode;
            rpInfo[0].LastModifyTime = DateTime.Now;
            if (_mzCfRepo.Update(rpInfo[0]) <= 0) throw new FailedException("", "门诊发药资源确认失败");
            var rq = new MqGeneralTaskRequestDto
            {
                Timestamp = DateTime.Now,
                body = new MqGeneralTaskBody
                {
                    OperationType = "supplementPatientBaseInfo",
                    Content = new PatientBaseInfoQueryRequest
                    {
                        cfh = rpInfo[0].cfh,
                        OrganizeId = rpInfo[0].OrganizeId,
                    }.ToJson()
                }.ToJson()
            };
            API.Common.Helper.MqClientHelper.SettGeneralTask(rq);
            return "";

        }

        /// <summary>
        /// 门诊退药
        /// </summary>
        /// <param name="tyInfo"></param>
        /// <param name="returnDrugBillNoList"></param>
        /// <returns></returns>
        public string OutpatientReturnDrugs(tyInfo tyInfo, out List<string> returnDrugBillNoList)
        {
            returnDrugBillNoList = new List<string>();
            if (tyInfo == null || tyInfo.tyDrugDetail == null || tyInfo.tyDrugDetail.Count == 0)
            {
                return "退药明细不能为空";
            }
            var errorMsg = new StringBuilder();
            foreach (var s in tyInfo.tyDrugDetail.Select(p => p.cfh).Distinct())
            {
                var request = new tyInfo
                {
                    userCode = tyInfo.userCode,
                    cfh = s,
                    yfbmCode = tyInfo.yfbmCode,
                    organizeId = tyInfo.organizeId,
                    tyDrugDetail = tyInfo.tyDrugDetail.FindAll(p => p.cfh == s)
                };
                var processResult = new OutpatientReturnDrugsProcess(request).Process();
                if (!processResult.IsSucceed)
                {
                    errorMsg.Append(processResult.ResultMsg + ";");
                }
                else
                {
                    returnDrugBillNoList.Add((string)processResult.Data);
                }
            }

            return errorMsg.ToString();
        }
        #endregion

        #region 住院

        /// <summary>
        /// 住院保存医嘱  落地医嘱信息、排药、冻结库存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActResult HospitalizatiionBook(ZyypyzzxRequest request)
        {
            var result = new YzzxAppV2(request).Process();
            if (result.IsSucceed) return result;
            throw new FailedException("", result.ResultMsg);
        }

        /// <summary>
        /// 住院退药，插入退药申请单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string HospitalizatiionReturnDispensingMedicine(HospitalizatiionReturnDispensingMedicineRequestDTO request)
        {
            var result = new HospitalizatiionReturnDispensingMedicineProcess(request).Process();
            if (!result.IsSucceed)
            {
                throw new FailedException("", result.ResultMsg);
            }
            return result.ResultMsg;
        }

        /// <summary>
        /// 住院退药
        /// </summary>
        /// <param name="tyDetail"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="returnDrugBillNo">退药单号</param>
        /// <returns></returns>
        public string HospitalizatiionReturnMedicine(List<tyParam> tyDetail, string yfbmCode, string organizeId, string userCode, out string returnDrugBillNo)
        {
            var effectiveLs = new List<tyParam>();
            var locker = new object();
            Parallel.ForEach(tyDetail, p =>
            {
                if (p.sl <= 0 || p.zhyz <= 0) return;
                lock (locker)
                {
                    effectiveLs.Add(p);
                }
            });
            var tyInfo = new tyInfo
            {
                returnDrugBillNo = OrderExt.GennerateSimpleOrderNo(),
                organizeId = organizeId,
                yfbmCode = yfbmCode,
                userCode = userCode,
                tyDrugDetail = effectiveLs
            };
            var result = new HospitalizatiionReturnMedicineProcess(tyInfo).Process();
            returnDrugBillNo = tyInfo.returnDrugBillNo;
            return result.IsSucceed ? "" : result.ResultMsg;
        }

        /// <summary>
        /// 住院退药
        /// </summary>
        /// <param name="tyDetail"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="returnDrugBillNo">退药单号</param>
        /// <returns></returns>
        public string HospitalizatiionReturnMedicineV2(List<tyParam> tyDetail, string yfbmCode, string organizeId, string userCode, out string returnDrugBillNo)
        {
            var effectiveLs = new List<tyParam>();
            var locker = new object();
            Parallel.ForEach(tyDetail, p =>
            {
                if (p.sl <= 0 || p.zhyz <= 0) return;
                lock (locker)
                {
                    effectiveLs.Add(p);
                }
            });

            if(effectiveLs[0].zytyapplyno == null || effectiveLs[0].zytyapplyno == "")
            {
                throw new Exception("退药申请单号不能为空！");
            }

            var tyInfo = new tyInfo
            {
                returnDrugBillNo = OrderExt.GennerateSimpleOrderNo(),
                organizeId = organizeId,
                yfbmCode = yfbmCode,
                userCode = userCode,
                tyDrugDetail = effectiveLs,
                zytyapplyno = effectiveLs[0].zytyapplyno
            };
            var result = new HospitalizatiionReturnMedicineProcessV2(tyInfo).Process();
            returnDrugBillNo = tyInfo.returnDrugBillNo;
            return result.IsSucceed ? "" : result.ResultMsg;
        }


        #endregion


        #region 取消排药

        /// <summary>
        /// 门诊取消排药
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string OutpatientCancelArrangement(OutpatientCancelArrangementRequestDTO request)
        {
            OutpatientCancelArrangementValidate(request);

            //取消满足默认要求的处方——已保存，未支付且已过最晚支付时间
            var settExpiredTime = _sysConfigRepo.GetValueByCode("settExpiredTime", request.OrganizeId);
            int tmpHour;
            int.TryParse(settExpiredTime, out tmpHour);
            tmpHour = tmpHour <= 0 ? 24 : tmpHour;
            var expirationDate = DateTime.Now.AddHours(-1 * tmpHour);
            var rps = _pyDmnService.SelectCancelRps(request.cfh, request.processesMaxNum, expirationDate, request.fyyf, request.OrganizeId);
            if (rps == null || rps.Count == 0)
            {
                return string.IsNullOrWhiteSpace(request.cfh) ? "未找到需要取消排药要求的处方" : string.Format("处方【{0}】不符合取消排药要求，该处方可能已取消！", request.cfh);
            }
            var emsg = new StringBuilder();
            var successCfh = new StringBuilder("本次操作成功取消冻结处方号为：");
            rps.ForEach(p =>
            {
                var errorMsg = _pyDmnService.OutPatientCancelArrangement(p, request.CreatorCode);
                if (!string.IsNullOrWhiteSpace(errorMsg)) emsg.Append(errorMsg);
                successCfh.Append(p.cfh).Append("、");
            });
            if (string.IsNullOrWhiteSpace(emsg.ToString())) return successCfh.ToString().Trim('、');
            throw new FailedException("", emsg.ToString());
        }

        /// <summary>
        /// 报文效验
        /// </summary>
        /// <param name="request"></param>
        private void OutpatientCancelArrangementValidate(OutpatientCancelArrangementRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId)) throw new FailedException("", "组织机构不能为空");
            if (string.IsNullOrWhiteSpace(request.fyyf)) throw new FailedException("", "发药药房不能为空");
        }
        #endregion

    }
}