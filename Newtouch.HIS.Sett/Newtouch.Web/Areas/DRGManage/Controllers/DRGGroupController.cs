using FrameworkBase.MultiOrg.Web;
using Newtonsoft.Json;
using Newtouch.Core.Redis;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.DRGManage.Controllers
{
    public class DRGGroupController : OrgControllerBase
    {
        // GET: DRGManage/DRGGroup

        private readonly IPatientCenterDmnService _patientCenterDmnService;
        private readonly ISysWardRepo _sysWardRepo;
        private readonly IMedicalRecordDmnService _medicalRecordDmnService;
        /// <summary>
        /// DRG分组测算
        /// </summary>
        /// <returns></returns>
        public ActionResult PatGrouping()
        {
            return View();
        }
        public ActionResult PatMedicalRecordQuery()
        {
            return View();
        }
        public ActionResult DeptMedicalRecordQuery()
        {
            return View();
        }
        public ActionResult WardPatList(string zyh, string ward)
        {
            var list = _patientCenterDmnService.PatientBasic(zyh, null, null, null, OrganizeId, "zy",ward);
            if (list == null || list.zyinfolist.Count == 0)
            {
                return Content("");
            }
           var bqlist = _sysWardRepo.GetbqList(OrganizeId);//病区
            List<BsTreeViewNodesBO> bqTree = new List<BsTreeViewNodesBO>();
            foreach (var b in bqlist)
            {
                bqTree.Add(new BsTreeViewNodesBO
                {
                    href = b.bqCode,
                    text = b.bqmc,
                    nodes = list.zyinfolist.Where(p => p.ward == b.bqCode).Select(p => new BsTreeViewBO { href = p.zyh, text =$"[ {p.zyh} ] {p.cwmc} - {p.xm }",icon= "fa fa-id-badge" }).OrderBy(p=>p.nodeId).ToList()
                });
            }
            return Content(bqTree.ToJson());
        }
        
        /// <summary>
        /// 根据住院号获取病人诊断信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult PatDiagnosisList(string zyh)
        {
            var list = _medicalRecordDmnService.GetAllMedicalRecordDiagnosisVOList(zyh, OrganizeId);
            return Content(list.ToJson());
        }
        /// <summary>
        /// 根据住院号获取病人手术信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult PatOperationList(string zyh)
        {
            var list = _medicalRecordDmnService.GetAllMedicalRecordOperationVOList(zyh, OrganizeId);
            return Content(list.ToJson());
        }
        ///// <summary>
        ///// 调整诊断顺序
        ///// </summary>
        ///// <param name="zdIds"></param>
        ///// <returns></returns>
        //public ActionResult UpdatePatDiagnosisOrder(string zdIds)
        //{
        //    var message = _medicalRecordDmnService.UpdatePatDiagnosisOrder(zdIds, OrganizeId);
        //    return Success(message);
        //}
        ///// <summary>
        ///// 调整手术排序
        ///// </summary>
        ///// <param name="zdIds"></param>
        ///// <returns></returns>
        //public ActionResult UpdatePatOperationOrder(string ssIds)
        //{
            
        //}
        /// <summary>
        /// 查询病人基本信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult PatInformationList(string zyh)
        {
            var list = _medicalRecordDmnService.GetPatInformationList(zyh, OrganizeId);
            return Content(list.ToJson());
        }
       
        /// <summary>
        /// 查询病人基本信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult DrgGroupByMedRecord(MedicalRecordInputDTO patxx,string zdlist,string sslist)
        {
            string[] zdList = zdlist.Split(',');
            string[] ssList = sslist.Split(',');
            var data = new {
                version = "",
                Index = patxx.bah,
                gender = patxx.xb,
                age = patxx.nl,
                ageDay = patxx.nlt,
                weight = patxx.cstz,
                dept = patxx.ks,
                inHospitalTime = patxx.ts,
                leavingType = patxx.LYFS,
                zdList = zdList,
                ssList= ssList,
                remark="",
            };
            var tokendata = new
            {
                AppId = ConfigurationManager.AppSettings["AppId"],
                OrganizeId = this.OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Data = ""
            };
            string token = "";
            var tokenstr = "";
            tokenstr = RedisHelper.StringGet("DrgGetTokenKey");
            if (tokenstr == null || tokenstr == "" || tokenstr == "null")
            {
                tokenstr = HttpTpClientHelper.Post(tokendata.ToJson(), ConfigurationManager.AppSettings["SiteDRGAPIHost"] + "api/Auth/GetAppToken", null);
                MedicalRecordInputTokenVO Token = JsonConvert.DeserializeObject<MedicalRecordInputTokenVO>(tokenstr);
                RedisHelper.StringSet("DrgGetTokenKey", Token.BusData.data.Token, new TimeSpan(0, 20, 0));
                token = Token.BusData.data.Token;
            }
            else {
                token = tokenstr;
            }
            var reqObj = new
            {
                Data=data,
                AppId= ConfigurationManager.AppSettings["AppId"],
                OrganizeId= OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var outstr = HttpTpClientHelper.Post(reqObj.ToJson(), ConfigurationManager.AppSettings["SiteDRGAPIHost"] +"api/DRGGrouper/DrgGroupByMedRecord", token);
            return Content(outstr);
        }
        /// <summary>
        /// 科室drg测算
        /// </summary>
        /// <param name="ks"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public ActionResult DeptDrgGroupByMedRecord(string ks, string kssj, string jssj)
        {
            List<MedicalRecordPatVO> list = _medicalRecordDmnService.GetPatMedicalRecordList("", kssj, jssj, OrganizeId);
            MedicalRecordDeptVO medicalRecordDeptVOs = new MedicalRecordDeptVO();
            foreach (var item in list)
            {
                string[] zdList = item.zdlist.Split(',');
                string[] ssList = item.sslist.Split(',');
                List<medicalList> medicalList = new List<medicalList>();
                medicalList medicalLists = new medicalList();
                medicalLists.Index = item.bah;
                medicalLists.gender = item.xb;
                medicalLists.age = item.nl;
                medicalLists.ageDay = item.nlt;
                medicalLists.weight = item.cstz;
                medicalLists.dept = item.cykb;
                medicalLists.inHospitalTime = item.zyts;
                medicalLists.leavingType = item.lyfs;
                medicalLists.zdList = zdList;
                medicalLists.ssList = ssList;
                medicalLists.remark = "";
                medicalList.Add(medicalLists);
                medicalRecordDeptVOs.medicalList = medicalList;
            }
            var tokendata = new
            {
                AppId = ConfigurationManager.AppSettings["AppId"],
                OrganizeId = this.OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Data = ""
            };
            string token = "";
            var tokenstr = "";
            tokenstr = RedisHelper.StringGet("DrgGetTokenKey");
            if (tokenstr == null || tokenstr == "" || tokenstr == "null")
            {
                tokenstr = HttpTpClientHelper.Post(tokendata.ToJson(), ConfigurationManager.AppSettings["SiteDRGAPIHost"] +"api/Auth/GetAppToken", null);              MedicalRecordInputTokenVO Token = JsonConvert.DeserializeObject<MedicalRecordInputTokenVO>(tokenstr);
                RedisHelper.StringSet("DrgGetTokenKey", Token.BusData.data.Token, new TimeSpan(0, 20, 0));
                token = Token.BusData.data.Token;
            }
            else
            {
                token = tokenstr;
            }
            var reqObj = new
            {
                Data = medicalRecordDeptVOs,
                AppId = ConfigurationManager.AppSettings["AppId"],
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var outstr = HttpTpClientHelper.Post(reqObj.ToJson(), ConfigurationManager.AppSettings["SiteDRGAPIHost"] + "api/DRGGrouper/DrgGroupByMedRecordList", token);
            return Content(outstr);
        }
        
        public ActionResult InHosPatInfo(string zyh, string ward)
        {
            //var list = _patientCenterDmnService.PatientBasic(zyh, null, null, null, OrganizeId, "zy", ward);
            //if (list == null || list.zyinfolist.Count == 0)
            //{
            //    return Error("未找到患者住院基本信息");
            //}
            //var pat=_patientCenterDmnService.GetDiagLsit()
            return Content("");
        }
        public ActionResult GetOpList(string ssmc)
        {
            var list = _medicalRecordDmnService.GetAllOpVOList(ssmc, OrganizeId);
            return Content(list.ToJson());
        }
        public ActionResult PatMedicalRecordList(string zyh,string kssj,string jssj)
        {
            var list = _medicalRecordDmnService.GetPatMedicalRecordList(zyh, kssj, jssj, OrganizeId);
            return Content(list.ToJson());
        }
    }
}