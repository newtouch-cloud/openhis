using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using Newtouch.CIS.APIRequest.Inpatient;
using Newtouch.Common.Operator;
using Newtouch.Common.Web;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.DO;
using Newtouch.HIS.Domain.DTO.OutPatientPharmacy;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.DomainServices;
using Newtouch.HIS.Repository;
using Newtouch.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 住院库存造作
    /// </summary>
    public class HospitalizationPharmacyApp : AppBase, IHospitalizationPharmacyApp
    {
        private readonly IZyYpyzxxRepo _zyYpyzxxRepo;
        private readonly IZyYpyzzxphRepo _zyYpyzzxphRepo;
        private readonly IDispensingDmnService _dispensingDmnService;

        /// <summary>
        /// 住院发药
        /// </summary>
        /// <param name="drugsParam"></param>
        /// <param name="userCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public string HospitalizationDispensing(List<DispensingDrugsParam> drugsParam, string userCode, string yfbmCode, string organizeId, out string fyid)
        {
            fyid = "0";
            if (drugsParam == null || drugsParam.Count == 0)
            {
                return "发药信息不能为空";
            }
            if (string.IsNullOrWhiteSpace(userCode))
            {
                return "当前用户的身份信息失效，请重新登录";
            }
            if (string.IsNullOrWhiteSpace(yfbmCode))
            {
                return "当前用户的药房部门失效，请重新登录";
            }
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                return "当前用户的所属机构失效，请重新登录";
            }

            List<ZyYpyzxxEntity> yzxxLs = new List<ZyYpyzxxEntity>();
            List<ZyYpyzzxphEntity> yzphLs = new List<ZyYpyzzxphEntity>();

            #region old code
            //var locker = new object();
            //Parallel.ForEach(drugsParam, item =>
            //{
            //    item.organizeId = organizeId;
            //    item.yfbmCode = yfbmCode;
            //    var yzph = new ZyYpyzzxphRepo(new DefaultDatabaseFactory()).SelectZyphList(item.yzId, item.zxId, item.ypCode, item.organizeId);
            //    if (yzph != null && yzph.Count > 0)
            //    {
            //        lock (locker)
            //        {
            //            yzphLs.AddRange(yzph);
            //        }
            //    }
            //    else
            //    {
            //        var drug = new SysMedicineExDmnService(new DefaultDatabaseFactory(), false).GetYpDetails(item.organizeId, item.ypCode);
            //        throw new Exception(string.Format("药品【{0}】未找到有效的排药信息", drug != null && !string.IsNullOrWhiteSpace(drug.ypmc) ? drug.ypmc : item.ypCode));
            //    }
            //    var yzxx = new ZyYpyzzxRepo(new DefaultDatabaseFactory()).SelectDataByYzId(item.yzId, item.zxId, item.ypCode, item.organizeId);
            //    if (yzxx != null && yzxx.Count > 0)
            //    {
            //        lock (locker)
            //        {
            //            yzxx.ForEach(p =>
            //            {
            //                if (!yzxxLs.Exists(i => i.Id == p.Id)) yzxxLs.Add(p);
            //            });
            //        }
            //    }
            //    else
            //    {
            //        var drug = new SysMedicineExDmnService(new DefaultDatabaseFactory(), false).GetYpDetails(item.organizeId, item.ypCode);
            //        throw new Exception(string.Format("药品【{0}】未找到要发药的医嘱", drug != null && !string.IsNullOrWhiteSpace(drug.ypmc) ? drug.ypmc : item.ypCode));
            //    }
            //});
            #endregion
            #region new code

            AssembleData2(drugsParam, yzxxLs, yzphLs, yfbmCode, organizeId);

            #endregion
            return _dispensingDmnService.HospitalizationDispensing(yzphLs, yzxxLs, userCode, out fyid,organizeId);
        }

        /// <summary>
        /// 参数验证和数据组装
        /// </summary>
        /// <param name="drugsParam"></param>
        /// <param name="yzxxLs"></param>
        /// <param name="yzphLs"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        private void AssembleData(List<DispensingDrugsParam> drugsParam, List<ZyYpyzxxEntity> yzxxLs, List<ZyYpyzzxphEntity> yzphLs, string yfbmCode, string organizeId)
        {
            var cts = new CancellationTokenSource();
            var errorMsg = "";
            var errorCode = "";
            var patients = new List<InpatientBaseInfoDo>();
            //var token = SiteCisAPIHelper.GetToken();
            var context = HttpContext.Current;
            var parent = new Task(() =>
            {
                var childFactory = new TaskFactory<bool>(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
                drugsParam.ForEach(item =>
                {
                    childFactory.StartNew(() =>
                    {
                        try
                        {
                            if (cts.IsCancellationRequested)
                            {
                                return false;
                            }

                            item.organizeId = organizeId;
                            item.yfbmCode = yfbmCode;
                            var yzph = new ZyYpyzzxphRepo(new DefaultDatabaseFactory()).SelectZyphList(item.yzId,
                                item.zxId, item.ypCode, item.organizeId);
                            if (yzph != null && yzph.Count > 0)
                            {
                                yzphLs.AddRange(yzph);
                            }
                            else
                            {
                                var drug =
                                    new SysMedicineExDmnService(new DefaultDatabaseFactory(), false).GetYpDetails(
                                        item.organizeId, item.ypCode);
                                errorMsg = string.Format("药品【{0}】未找到有效的排药信息",
                                    drug != null && !string.IsNullOrWhiteSpace(drug.ypmc) ? drug.ypmc : item.ypCode);
                                cts.Cancel();
                                return false;
                            }

                            var yzxx = new ZyYpyzzxRepo(new DefaultDatabaseFactory()).SelectDataByYzId(item.yzId,
                                item.zxId, item.ypCode, item.organizeId);
                            if (yzxx != null && yzxx.Count > 0)
                            {
                                yzxx.ForEach(p =>
                                {
                                    if (!yzxxLs.Exists(i => i.Id == p.Id)) yzxxLs.Add(p);
                                    if (patients.Any(o =>
                                        o.xm == p.patientName && o.OrganizeId == p.OrganizeId && o.zyh == p.zyh))
                                        return;
                                    var reqObj = new InpatientBaseInfoRequest
                                    {
                                        zyh = p.zyh,
                                        xm = p.patientName,
                                        OrganizeId = p.OrganizeId,
                                        Timestamp = DateTime.Now
                                    };
                                    var apiResp =
                                        SiteCisAPIHelper.Request<InpatientBaseInfoRequest, APIRequestHelper.DefaultResponse>(
                                            "api/PatInfo/InpatientBaseInfo", reqObj, httpContext: context);
                                    if (apiResp.code != APIRequestHelper.ResponseResultCode.SUCCESS ||
                                        apiResp.data == null)
                                    {
                                        cts.Cancel();
                                        throw new FailedException(string.IsNullOrWhiteSpace(apiResp.sub_msg) ? "从医护协同工作站获取患者信息失败！" : apiResp.sub_msg);

                                    }

                                    var a = apiResp.data.ToString().ToObject<InpatientBaseInfoDo>();
                                    if (a != null)
                                    {
                                        if (a.zybz == 1) return;
                                        cts.Cancel();
                                        throw new FailedException("10001", string.Format("【{0}】非在区状态，不能发药，如有必要可取消排药！", p.patientName));
                                    }

                                    cts.Cancel();
                                    throw new FailedException("患者信息解析失败！");
                                });
                            }
                            else
                            {
                                var drug =
                                    new SysMedicineExDmnService(new DefaultDatabaseFactory(), false).GetYpDetails(
                                        item.organizeId, item.ypCode);
                                errorMsg = string.Format("药品【{0}】未找到要发药的医嘱",
                                    drug != null && !string.IsNullOrWhiteSpace(drug.ypmc) ? drug.ypmc : item.ypCode);
                                cts.Cancel();
                            }
                            return true;
                        }
                        catch (FailedException e)
                        {
                            errorMsg = e.Msg;
                            errorCode = e.Code;
                            cts.Cancel();
                            return false;

                        }
                        catch (Exception e)
                        {
                            errorMsg = e.Message;
                            cts.Cancel();
                            return false;
                        }
                    }, cts.Token);
                });
            });
            parent.Start();
            parent.Wait();
            if (!string.IsNullOrWhiteSpace(errorMsg)) throw new FailedException(errorCode, errorMsg);
        }

        /// <summary>
        /// 参数验证和数据组装(新)(周滋明)
        /// </summary>
        /// <param name="drugsParam"></param>
        /// <param name="yzxxLs"></param>
        /// <param name="yzphLs"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        private void AssembleData2(List<DispensingDrugsParam> drugsParam, List<ZyYpyzxxEntity> yzxxLs, List<ZyYpyzzxphEntity> yzphLs, string yfbmCode, string organizeId)
        {
            string errorMsg = "";
            string errorCode = "";
            List<InpatientBaseInfoDo> patients = new List<InpatientBaseInfoDo>();

            try
            {
                for (int m = 0; m < drugsParam.Count; m ++)
                {
                    DispensingDrugsParam item = drugsParam[m];

                    item.organizeId = organizeId;
                    item.yfbmCode = yfbmCode;
                    List<ZyYpyzzxphEntity> yzph = new ZyYpyzzxphRepo(new DefaultDatabaseFactory()).SelectZyphList(item.yzId,
                        item.zxId, item.ypCode, item.organizeId);
                    if (yzph != null && yzph.Count > 0)
                    {
                        foreach (ZyYpyzzxphEntity zyYpyzzxphEntity in yzph)
                        {
                            if (!yzphLs.Exists(z => z.Id == zyYpyzzxphEntity.Id))
                            {
                                yzphLs.AddRange(yzph);
                            }
                        }
                    }
                    else
                    {
                        var drug =
                            new SysMedicineExDmnService(new DefaultDatabaseFactory(), false).GetYpDetails(
                                item.organizeId, item.ypCode);
                        errorMsg = string.Format("药品【{0}】未找到有效的排药信息",
                            (drug != null && !string.IsNullOrWhiteSpace(drug.ypmc) ? drug.ypmc : item.ypCode));
                        throw new Exception(errorMsg);
                    }

                    List<ZyYpyzxxEntity> yzxx = new ZyYpyzzxRepo(new DefaultDatabaseFactory()).SelectDataByYzId(item.yzId,
                        item.zxId, item.ypCode, item.organizeId);
                    if (yzxx != null && yzxx.Count > 0)
                    {
                        for(int n = 0; n < yzxx.Count; n ++)
                        {
                            ZyYpyzxxEntity px = yzxx[n];

                            if (! yzxxLs.Exists(z => z.Id == px.Id))
                            {
                                yzxxLs.Add(px);
                            }

                            if (patients.Any(o =>
                                o.xm == px.patientName && o.OrganizeId == px.OrganizeId && o.zyh == px.zyh))
                            {
                                continue;
                            }

                            // 验证患者住院状态
                            InpatientBaseInfoRequest reqObj = new InpatientBaseInfoRequest()
                            {
                                zyh = px.zyh,
                                xm = px.patientName,
                                OrganizeId = px.OrganizeId,
                                Timestamp = DateTime.Now
                            };
                            APIRequestHelper.DefaultResponse apiResp =
                                SiteCisAPIHelper.Request<InpatientBaseInfoRequest, APIRequestHelper.DefaultResponse>(
                                    "api/PatInfo/InpatientBaseInfo", reqObj, httpContext: HttpContext.Current);
                            if (apiResp.code != APIRequestHelper.ResponseResultCode.SUCCESS ||
                                apiResp.data == null)
                            {
                                errorMsg = string.IsNullOrWhiteSpace(apiResp.sub_msg) ? ("从CIS获取患者信息【" + px.patientName + "】失败！") : apiResp.sub_msg;
                                throw new Exception(errorMsg);
                            }
                            InpatientBaseInfoDo pif = apiResp.data.ToString().ToObject<InpatientBaseInfoDo>();
                            if (pif == null)
                            {
                                errorMsg = "解析CIS返回的患者信息【" + px.patientName + "】失败！";
                                throw new Exception(errorMsg);
                            }
                            if (pif.zybz != 1)
                            {
                                errorMsg = string.Format("【{0}】非在区状态，不能发药，如有必要可取消排药！", px.patientName);
                                throw new Exception(errorMsg);
                            }

                            // 添加住院患者信息到缓存区
                            patients.Add(new InpatientBaseInfoDo() {
                                xm = px.patientName,
                                OrganizeId = px.OrganizeId,
                                zyh = px.zyh
                            });
                        }
                    }
                    else
                    {
                        var drug =
                            new SysMedicineExDmnService(new DefaultDatabaseFactory(), false).GetYpDetails(
                                item.organizeId, item.ypCode);
                        errorMsg = string.Format("药品【{0}】未找到要发药的医嘱",
                            (drug != null && !string.IsNullOrWhiteSpace(drug.ypmc) ? drug.ypmc : item.ypCode));
                        throw new Exception(errorMsg);
                    }
                }
            }
            catch (Exception e)
            {
                errorMsg = e.Message;
            }

            if (! string.IsNullOrWhiteSpace(errorMsg))
            {
                throw new FailedException(errorCode, errorMsg);
            }
        }
    }
}