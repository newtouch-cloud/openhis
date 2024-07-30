using FrameworkBase.MultiOrg.Domain.IDomainServices;
using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Interface;
using Newtouch.HIS.Application;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.PatientManage;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.HospitalizationManage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.PatientManage;
using Newtouch.HIS.Proxy.guian;
using Newtouch.HIS.Proxy.guian.DTO;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.PatientManage.Controllers
{
    /// <summary>
    /// 住院病人
    /// </summary>
    public class InpatientController : ControllerBase
    {
        private readonly ICache _cache;
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysWardRepo _sysWardRepo;
        private readonly IHospPatientBasicInfoRepo _hospPatientBasicInfoRepo;
        private readonly IBookkeepInHosDmnService _bookkeepInHosDmnService;
        private readonly IPatientBasicInfoDmnService _patientBasicInfoDmnService;
        private readonly IDischargeSettleDmnService _dischargeSettleDmnService;
        private readonly IInpatientApp _inpatientApp;
        private readonly ISysPatientBasicInfoRepo _sysPatientBasicInfoRepo;

        /// <summary>
        /// 获取住院病人 三级（机构+病区+病人）
        /// </summary>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetInpatientSelectorTreeData(string organizeId = null, string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true)
        {
            //if (string.IsNullOrWhiteSpace(organizeId))
            //{
            //    organizeId = base.GetAuthOrganizeId();  //默认 权限OrganizeId
            //}
            ////
            //var organizedata = _sysOrganizeDmnService.GetValidListByParentOrg(organizeId);
            var treeList = new List<TreeViewModel>();
            //foreach (var orgItem in organizedata)
            //{
            //    //机构病区集合
            //    var orgBqData = _sysWardRepo.GetbqList(orgItem.Id);
            //    //机构病人集合
            //    var orgPatientData =_bookkeepInHosDmnService.GetPendingExecutionPatientVOList(orgItem.Id);
            //    #region 机构下所有病区
            //    foreach (var bqItem in orgBqData)
            //    {
            //        var bqPatientData = orgPatientData.Where(p => p.bqCode == bqItem.bqCode).ToList();
            //        #region 机构下所有病人
            //        foreach (var patientItem in bqPatientData)
            //        {
            //            TreeViewModel subsubTree = new TreeViewModel();
            //            subsubTree.id = "br_" + patientItem.zyh;
            //            subsubTree.text = patientItem.xm + "(zyh:" + patientItem.zyh + ")";
            //            if (patientItem.ybsycs.HasValue && patientItem.ybsycs.Value > 0)
            //            {
            //                subsubTree.text += "(医保剩余:" + patientItem.ybsycs + "次)";
            //            }
            //            subsubTree.value = "br_" + patientItem.zyh;
            //            subsubTree.parentId = "bq_" + bqItem.bqCode;
            //            subsubTree.hasChildren = false;
            //            subsubTree.showcheck = true;
            //            subsubTree.checkstate = 0;   //外面赋值，方便缓存

            //            subsubTree.Ex1 = "1";    //标示是病人，用于区分 病人、病区的复选框

            //            treeList.Add(subsubTree);
            //        }
            //        #endregion
            //        TreeViewModel subTree = new TreeViewModel();
            //        subTree.id = "bq_" + bqItem.bqCode;
            //        subTree.text = bqItem.bqmc;
            //        subTree.value = "bq_" + bqItem.bqCode;
            //        subTree.parentId = orgItem.Id;
            //        subTree.isexpand = isExpand;  //默认不展开，人太多
            //        subTree.complete = true;
            //        subTree.showcheck = true;
            //        subTree.hasChildren = bqPatientData.Count > 0;
            //        treeList.Add(subTree);
            //    }
            //    #endregion
            //    TreeViewModel tree = new TreeViewModel();
            //    tree.id = orgItem.Id;
            //    tree.text = orgItem.Name;
            //    tree.value = orgItem.Code;
            //    tree.parentId = orgItem.Id == organizeId ? null : orgItem.ParentId; //特殊
            //    tree.isexpand = true;
            //    tree.complete = true;
            //    tree.showcheck = false;
            //    tree.hasChildren = orgBqData.Count > 0 || orgPatientData.Count > 0 || organizedata.Any(p => p.ParentId == orgItem.Id);
            //    treeList.Add(tree);
            //}
            ////

            ////if (from == "selecttherapist")   //选择系统治疗师
            ////{
            ////    var whereList = _sysUserDmnService.GetStaffIdListByParentOrg(organizeId, "RehabDoctor");   //治疗师
            ////    treeList = treeList.Where(p => !p.showcheck || whereList.Contains(p.id)).ToList();  //只显示治疗师
            ////}

            //IList<string> checkedStaffIdList = null;
            ////if (from == "selecttherapist")   //选择系统治疗师
            ////{

            ////}

            //if (checkedStaffIdList != null && checkedStaffIdList.Count > 0)
            //{
            //    foreach (var treeItem in treeList)
            //    {
            //        if (treeItem.showcheck && checkedStaffIdList.Contains(treeItem.id))
            //        {
            //            treeItem.checkstate = 1;
            //            //同时让上级展开
            //            var thisParentNode = treeList.Where(p => p.id == treeItem.parentId).FirstOrDefault();
            //            if (thisParentNode != null)
            //            {
            //                thisParentNode.isexpand = true;  //下级有选中的，这里一定展开之
            //            }
            //        }
            //    }
            //}
            //if (!isShowEmpty)   //不显示多余的
            //{
            //    var count = 1;  //移除的行数
            //    while (count > 0)
            //    {
            //        count = treeList.RemoveAll(p => !p.showcheck && !p.hasChildren);
            //        //重新调整下树 移除 没有下级（下级刚被移了） 又 不显示showcheck的
            //        treeList.RemoveAll(p => !p.showcheck && !treeList.Any(sub => sub.parentId == p.id));
            //    }
            //}
            return Content(treeList.TreeViewJson(null));
        }

        public ActionResult GetAccountingpatientInfo(string organizeId = null, string from = null)
        {
            List<PendingExecutionPatientVO> data = new List<PendingExecutionPatientVO> { };
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = base.GetAuthOrganizeId();  //默认 权限OrganizeId
            }
            //
            var organizedata = _sysOrganizeDmnService.GetValidListByParentOrg(organizeId);
            foreach (var orgItem in organizedata)
            {
                //机构病区集合
                var orgBqData = _sysWardRepo.GetbqList(orgItem.Id);
                IList<PendingExecutionPatientVO> orgPatientData = new List<PendingExecutionPatientVO>();
                if (from == "mz")
                {
                    orgPatientData = _bookkeepInHosDmnService.GetOutPendingExecutionPatientVOList(orgItem.Id, UserIdentity.DepartmentCode);
                }
                else
                {
                    orgPatientData = _bookkeepInHosDmnService.GetPendingExecutionPatientVOList(orgItem.Id, UserIdentity.DepartmentCode);
                }
                data.AddRange(orgPatientData);
            }
            return Content(data.ToJson());
        }
        /// <summary>
        /// 浮层，数据源 获取住院病人
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="brlx"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetInpatientListJson(string keyword = null, string zybz = "1")
        {
            var list = _hospPatientBasicInfoRepo.GetInpatientList(this.OrganizeId, keyword, zybz);
            return Content(list == null ? "[]" : list.ToJson());
        }

        /// <summary>
        /// 住院患者查询 View
        /// </summary>
        /// <returns></returns>
        public ActionResult InPatientQueryIndex()
        {
            return View();
        }

        /// <summary>
        /// 住院患者查询 数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zybz"></param>
        /// <param name="keyword"></param>
        /// <param name="ryrqkssj"></param>
        /// <param name="ryrqjssj"></param>
        /// <returns></returns>
        public ActionResult GridInPatientGridJson(Pagination pagination
            , string zybz = null
            , string keyword = null, DateTime? ryrqkssj = null, DateTime? ryrqjssj = null,string ksmc=null,string bqmc=null)
        {
            string flag = null;
            string msg = null;
            var data = new
            {
                rows = _patientBasicInfoDmnService.GetInPatientList(this.OrganizeId
                , ref flag, ref msg, zybz: zybz, keyword: keyword, pagination: pagination, ryrqkssj: ryrqkssj, ryrqjssj: ryrqjssj, ksmc:ksmc, bqmc:bqmc),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 自费转医保 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ZFToYBForm()
        {
            return View();
        }

        /// <summary>
        /// 医保转自费 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult YBToZFForm()
        {
            return View();
        }
        public ActionResult dkfsxz()
        {
            return View();
        }
        public ActionResult XNHToZFForm()
        {
            return View();
        }

        #region ZFToYB steps
        /// <summary>
        /// 是否首次医保就诊
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult CheckFistYbVisit(string mzzyh, string sbkh)
        {
            var orgId = this.OrganizeId;
            //int patid =0;
            var code = "0";
            //var xtbrxxList = _sysPatientBasicInfoRepo.IQueryable().Where(p => p.sbbh == sbkh && p.OrganizeId == orgId && p.zt == "1").FirstOrDefault();
            //if (xtbrxxList != null)
            //{
            //    code = "1";
            //}
            return Success(null, code);
        }
        public ActionResult ZFToYB_Step_1(string zyh)
        {
            System.Threading.Thread.Sleep(1000 * 3);
            var data = _inpatientApp.ZFToYB_Step_1(zyh);
            return Success(null, data);
        }

        public ActionResult ZFToYB_Step_2(string zyh)
        {
            var data = _inpatientApp.ZFToYB_Step_2(zyh);
            return Success(null, data);
        }

        public ActionResult ZFToYB_Step_4(string zyh, string sbbh, string xm)
        {
            var data = _inpatientApp.ZFToYB_Step_4(zyh, sbbh, xm);
            return Success(null, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="patid"></param>
        /// <param name="cardInfo"></param>
        /// <param name="ryblInfo">医保卡信息</param>
        /// <returns></returns>
        public ActionResult ZFToYB_Step_6(string zyh, int patid, GACardReadInfoDTO cardInfo, GuianRybl21OutInfoEntity ryblInfo)
        {
            var data = _inpatientApp.ZFToYB_Step_6(zyh, patid, cardInfo, ryblInfo);
            return Success(null, data);
        }
		/// <summary>
		/// 重庆自费转医保--住院
		/// </summary>
		/// <param name="zyh"></param>
		/// <param name="patid"></param>
		/// <param name="cardInfo"></param>
		/// <param name="ryblInfo"></param>
		/// <returns></returns>
	    public ActionResult CQZFToYB_Step_6(string zyh, int patid, ZYToYBDto patInfo, CqybMedicalReg02Entity ryblInfo)
	    {
		    var data = _inpatientApp.CQZFToYB_Step_6(zyh, patid, patInfo, ryblInfo);
		    return Success(null, data);
	    }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="zyh"></param>
		/// <returns></returns>
		public ActionResult ZFToYB_Step_8(string zyh)
        {
            var data = _inpatientApp.ZFToYB_Step_8(zyh);
            return Success(null, data);
        }

        #endregion

        #region YBToZF steps

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult YBToZF_Step_1(string zyh)
        {
            var data = _inpatientApp.YBToZF_Step_1(zyh);
            return Success(null, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult YBToZF_Step_3(string zyh, string sbbh, string xm)
        {
            var data = _inpatientApp.YBToZF_Step_3(zyh, sbbh, xm);
            return Success(null, data);
        }
		/// <summary>
		/// 重庆医保获取患者就诊登记落地数据
		/// </summary>
		/// <param name="zyh"></param>
		/// <returns></returns>
	    public ActionResult CqYBToZF_Step_3(string zyh)
	    {
		    var data = _inpatientApp.CqYBToZF_Step_3(zyh);
		    return Success(null, data);
	    }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="zyh"></param>
		/// <returns></returns>
		public ActionResult YBToZF_Step_4(string zyh)
        {
            var data = _inpatientApp.YBToZF_Step_4(zyh);
            return Success(null, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult YBToZF_Step_5(string zyh, int patid)
        {
            var data = _inpatientApp.YBToZF_Step_5(zyh, patid);
            return Success(null, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult YBToZF_Step_7(string zyh)
        {
            var data = _inpatientApp.YBToZF_Step_7(zyh);
            return Success(null, data);
        }

        #endregion

        #region XNHToZF steps
        public ActionResult GuiAnXnhYbToZf(string zyh)
        {
            try
            {
                bool isSuccess = true;
                string message = string.Empty;
                string state = "1";
                if (string.IsNullOrEmpty(zyh))
                {
                    isSuccess = false;
                    state = "0";
                    message = "住院号不能为空，请重试！";
                }

                InpatientSettXnhPatInfoVO patinfo = null;
                if (isSuccess)
                {
                    patinfo = _dischargeSettleDmnService.GetInpatientSettXnhPatInfo(zyh, this.OrganizeId);
                    if (patinfo == null || string.IsNullOrEmpty(patinfo.inpId))
                    {
                        isSuccess = false;
                        state = "0";
                        message = "获取新农合患者住院补偿序号inpId为空，请联系his处理";
                    }
                    if (patinfo.zybz== ((int)EnumZYBZ.Wry).ToString()|| patinfo.zybz == ((int)EnumZYBZ.Ycy).ToString())
                    {
                        isSuccess = false;
                        state = "0";
                        message = "该患者已出院或者已作废！";
                    }

                    if (isSuccess)
                    {
                        S05RequestDTO S05ReqDTO = new S05RequestDTO()
                        {
                            inpId = patinfo.inpId,
                            areaCode = "",
                            isTransProvincial = "0",
                            cancelCause = ""
                        };
                        Response<string> S05ResDto = HospitalizationProxy.GetInstance(OrganizeId).S05(S05ReqDTO);
                        if (!S05ResDto.state)
                        {
                            isSuccess = false;
                            state = "0";
                            message = "入院办理回退失败，农保返回错误信息为：【" + S05ResDto.message + "】";
                        }
                        else
                        {
                            //1.his医保转自费   2.同步cpoe
                            _inpatientApp.YBToZF_Step_5(zyh, patinfo.patid);
                            YBToZF_Step_7(zyh);
                        }
                    }
                }
                var data = new
                {
                    state = state,
                    message = message
                };
                return Content(data.ToJson());
            }
            catch (Exception e)
            {
                var data = new
                {
                    state = "0",
                    message = "取消出院结算失败：【" + e.Message + "】"
                };
                return Content(data.ToJson());
            }
        }

        #endregion

        #region ZFToXNH steps
        public ActionResult GuiAnZFToXNH(string zyh, HosPatZFToXNHVO patInfo)
        {
            try
            {
                bool isSuccess = true;
                string message = string.Empty;
                string state = "1";
                if (string.IsNullOrEmpty(zyh))
                {
                    isSuccess = false;
                    state = "0";
                    message = "住院号不能为空，请重试！";
                }

                string msg1 = "";
                bool step04 = _inpatientApp.ZFToXNH_Step_4(zyh, patInfo.xnhgrbm, patInfo.xm, out msg1);
                if (!step04)
                {
                    isSuccess = false;
                    state = "0";
                    message = msg1;
                }
                if (isSuccess)
                {

                    S04RequestDTO S04ReqDTO = _inpatientApp.GetZfToXnhPatInfo(zyh);
                    if (S04ReqDTO==null || string.IsNullOrEmpty(S04ReqDTO.memberId) || string.IsNullOrEmpty(S04ReqDTO.inpatientNo) || string.IsNullOrEmpty(S04ReqDTO.admissionDate) || string.IsNullOrEmpty(S04ReqDTO.admissionDepartments))
                    {
                        isSuccess = false;
                        state = "0";
                        message = "入院办理缺少必须的值！";
                    }
                    if (isSuccess)
                    {

                        Response<S04ResponseDTO> S04ResDto = HospitalizationProxy.GetInstance(OrganizeId).S04(S04ReqDTO);
                        if (!S04ResDto.state)
                        {
                            isSuccess = false;
                            state = "0";
                            message = "入院办理失败，农保返回错误信息为：【" + S04ResDto.message + "】";
                        }
                        else
                        {
                            GuianXnhS04InfoEntity S04InfoEntity = new GuianXnhS04InfoEntity()
                            {
                                Id = Guid.NewGuid().ToString(),
                                inpId = S04ResDto.data.inpId,
                                zyh = zyh,
                                xnhgrbm = patInfo.xnhgrbm,
                                xnhylzh = patInfo.xnhylzh
                            };
                            string msg2 = "";
                            if (_patientBasicInfoDmnService.InpatientZFchangetoXNH(this.OrganizeId, zyh, patInfo, S04InfoEntity, out msg2))
                            {
                                _inpatientApp.ZFToXNH_Step_8(zyh);
                            }
                            else
                            {
                                S05RequestDTO S05ReqDTO = new S05RequestDTO()
                                {
                                    inpId = S04ResDto.data.inpId,
                                    areaCode = "",
                                    isTransProvincial = "0",
                                    cancelCause = ""
                                };
                                Response<string> S05ResDto = HospitalizationProxy.GetInstance(OrganizeId).S05(S05ReqDTO);
                                if (!S05ResDto.state)
                                {
                                    isSuccess = false;
                                    state = "0";
                                    message = "自费转农合His本地失败【"+ msg2 + "】，其中新农合远程已成功，入院办理回退失败，农保返回错误信息为：【" + S05ResDto.message + "】";
                                }
                                else
                                {
                                    isSuccess = false;
                                    state = "0";
                                    message = "自费转农合失败【" + msg2 + "】";
                                }
                            }

                            ;
                        }
                    }
                }

               
                var data = new
                {
                    state = state,
                    message = message
                };
                return Content(data.ToJson());
            }
            catch (Exception e)
            {
                var data = new
                {
                    state = "0",
                    message = "自费转新农合失败：【" + e.Message + "】"
                };
                return Content(data.ToJson());
            }
        }

        #endregion

    }
}